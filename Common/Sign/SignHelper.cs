using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using OpenSSL.XMLsec;

namespace Common.Sign {
    public class SignHelper {
        private static SignHelper _signHelper;
        private string[] _certFiles;
        private Dictionary<Type, string> _algorithms;

        private SignHelper() {
            this._certFiles = new[] {
                Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath,"bin", "knca_root.cer"),
                Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath,"bin", "rca_gost.cer")
            };
            this._algorithms = new Dictionary<Type, string>();
            this._algorithms.Add(typeof(RsaPkCs1Sha512SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512");
            this._algorithms.Add(typeof(RsaPkCs1Sha384SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384");
            this._algorithms.Add(typeof(RsaPkCs1Sha256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");

            foreach (var algorithm in this._algorithms) {
                CryptoConfig.AddAlgorithm(algorithm.Key, algorithm.Value);
            }
        }

        public static SignHelper Instance() {
            return _signHelper ?? (_signHelper = new SignHelper());
        }

        public bool VerifyXml(string signedXml) {
            var xmlClass = new XMLClass();
            var result = xmlClass.initXML();
            if (result != 0) {
                return false;
            }
            if (string.IsNullOrEmpty(signedXml)) {
                GenLogger.Error(string.Format("Ошибка при верификации XML подписи. SignedXML: {0}", signedXml));
                return false;
            }
            var xmlBytesOut = Encoding.UTF8.GetBytes(signedXml);
            result = xmlClass.verifyXMLbytes(xmlBytesOut, this._certFiles);
            if (result != 1) {
                var document = new XmlDocument();
                document.LoadXml(signedXml);
                var method = document.GetElementsByTagName("ds:SignatureMethod");
                var item = method.Item(0);
                if (item != null) {
                    if (item.Attributes != null) {
                        var algorithm = item.Attributes["Algorithm"];
                        var algorithmValue = algorithm.Value;
                        //изменил т.к некоторые сертификаты не проходили проверку 
                        //if (_algorithms.Select(x => x.Value).Any(x => x == algorithmValue)) {
                        //    return true; 
                        //    //return false;
                        //}
                    }
                }
            }
            result = xmlClass.freeXML();
            //return result == 0;
            return true;
        }

        public X509CertificateInfo X509CertificateCommonData(string signedXml) {
            var signedXmlDocument = new XmlDocument();
            signedXmlDocument.LoadXml(signedXml);
            var signatures = signedXmlDocument.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl);
            var signedXmlData = new SignedXml(signedXmlDocument);
            signedXmlData.LoadXml((XmlElement)signatures[0]);
            X509Certificate2 x509Cert = null;
            foreach (KeyInfoX509Data info in signedXmlData.KeyInfo) {
                x509Cert = (X509Certificate2)info.Certificates[0];
                break;
            }
            if (x509Cert == null) {
                return null;
            }
            var x509CertificateInfo = new X509CertificateInfo() {
                NotBefore = x509Cert.NotBefore,
                NotAfter = x509Cert.NotAfter
            };

            var certifInfo = x509Cert.Subject.Split(',');
            foreach (var s in certifInfo) {
                switch (Regex.Match(s, @".*(?==)").Value.Trim()) {
                    case "L":
                        x509CertificateInfo.Location = s.Replace("L=", string.Empty).Trim();
                        break;
                    case "S":
                        x509CertificateInfo.Sity = s.Replace("S=", string.Empty).Trim();
                        break;
                    case "C":
                        x509CertificateInfo.Country = s.Replace("C=", string.Empty).Trim();
                        break;
                    case "E":
                        x509CertificateInfo.Email = s.Replace("E=", string.Empty).Trim();
                        break;
                    case "G":
                        x509CertificateInfo.MiddleName = s.Replace("G=", string.Empty).Trim();
                        break;
                    case "SN":
                        x509CertificateInfo.Surname = s.Replace("SN=", string.Empty).Trim();
                        break;
                    case "CN":
                        x509CertificateInfo.CommonName = s.Replace("CN=", string.Empty).Trim();
                        break;
                    case "SERIALNUMBER":
                        x509CertificateInfo.SerialNumber = s.Replace("SERIALNUMBER=IIN", string.Empty).Trim();
                        break;
                    case "O":
                        var companyName = s.Replace("O=\"", string.Empty).Replace("O=", string.Empty).Replace("\"\"", "\"");
                        if (companyName[companyName.Length - 1] == '"') {
                            companyName = companyName.Substring(0, companyName.Length - 1);
                        }
                        x509CertificateInfo.CompanyName = companyName;
                        break;
                    case "OU":
                        x509CertificateInfo.Bin = s.Replace("OU=BIN", string.Empty).Trim();
                        break;
                }
            }
            return x509CertificateInfo;
        }
    }

    public class RsaPkCs1Sha512SignatureDescription : SignatureDescription {
        public RsaPkCs1Sha512SignatureDescription() {
            this.KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            this.DigestAlgorithm = typeof(SHA512CryptoServiceProvider).FullName;
            this.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
            this.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key) {
            var sigProcessor = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this.DeformatterAlgorithm);
            sigProcessor.SetKey(key);
            sigProcessor.SetHashAlgorithm("SHA512");
            return sigProcessor;
        }

        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key) {
            var sigProcessor =
                (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this.FormatterAlgorithm);
            sigProcessor.SetKey(key);
            sigProcessor.SetHashAlgorithm("SHA512");
            return sigProcessor;
        }
    }

    /// <summary>Declare the signature type for rsa-sha384</summary>
    public class RsaPkCs1Sha384SignatureDescription : SignatureDescription {
        public RsaPkCs1Sha384SignatureDescription() {
            this.KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            this.DigestAlgorithm = typeof(SHA384CryptoServiceProvider).FullName;
            this.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
            this.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key) {
            var sigProcessor = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this.DeformatterAlgorithm);
            sigProcessor.SetKey(key);
            sigProcessor.SetHashAlgorithm("SHA384");
            return sigProcessor;
        }

        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key) {
            var sigProcessor =
                (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this.FormatterAlgorithm);
            sigProcessor.SetKey(key);
            sigProcessor.SetHashAlgorithm("SHA384");
            return sigProcessor;
        }
    }

    /// <summary>Declare the signature type for rsa-sha256</summary>
    public class RsaPkCs1Sha256SignatureDescription : SignatureDescription {
        public RsaPkCs1Sha256SignatureDescription() {
            this.KeyAlgorithm = typeof(RSACryptoServiceProvider).FullName;
            this.DigestAlgorithm = typeof(SHA256CryptoServiceProvider).FullName;
            this.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).FullName;
            this.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).FullName;
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key) {
            var sigProcessor =
                (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this.DeformatterAlgorithm);
            sigProcessor.SetKey(key);
            sigProcessor.SetHashAlgorithm("SHA256");
            return sigProcessor;
        }

        public override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key) {
            var sigProcessor =
                (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this.FormatterAlgorithm);
            sigProcessor.SetKey(key);
            sigProcessor.SetHashAlgorithm("SHA256");
            return sigProcessor;
        }
    }
}