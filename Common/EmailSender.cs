using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Common {
    public class EmailSender {
        private readonly string _mailAddress;
        private readonly string _mailHost;
        private readonly string _mailCredentialsUserName;
        private readonly string _mailCredentialsPassword;
        private readonly bool _mailSslEnabled;

        public EmailSender()
            : this(
                ConfigurationManager.AppSettings["MailHost"],
                ConfigurationManager.AppSettings["MailCredentialsUserName"],
                ConfigurationManager.AppSettings["MailCredentialsPassword"],
                ConfigurationManager.AppSettings["MailAddress"],
                bool.Parse(ConfigurationManager.AppSettings["MailSslEnabled"])) {
        }

        public EmailSender(string host, string userName, string password, string mailAddress, bool sslEnabled) {
            this._mailAddress = mailAddress;
            this._mailHost = host;
            this._mailCredentialsUserName = userName;
            this._mailCredentialsPassword = password;
            this._mailSslEnabled = sslEnabled;

            if (string.IsNullOrEmpty(this._mailAddress) || string.IsNullOrEmpty(this._mailHost) ||
                string.IsNullOrEmpty(this._mailCredentialsUserName) || string.IsNullOrEmpty(this._mailCredentialsPassword))
                throw new ConfigurationErrorsException("SMTP сервис не сконфигурирован.");
        }

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="toEmail">Кому</param>
        /// <param name="subject">Заголовок</param>
        /// <param name="body">Содержимое</param>
        public void SendEmail(string toEmail, string subject, string body) {
            using (var mailMessage = new MailMessage {
                Subject = subject,
                From = new MailAddress(this._mailAddress),
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            }) {
                var mimeType = new ContentType("text/html");
                var alternate = AlternateView.CreateAlternateViewFromString(string.Format(HmtlTemplate, body), mimeType);

                mailMessage.AlternateViews.Add(alternate);
                mailMessage.To.Add(new MailAddress(toEmail));
                mailMessage.Bcc.Add("meruertmyrzabekova@gmail.com"); // адрес тестировщика

                using (var smtp = new SmtpClient {
                    Host = this._mailHost,
                    EnableSsl = this._mailSslEnabled,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(this._mailCredentialsUserName, this._mailCredentialsPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                }) {
                    try {
                        GenLogger.Info(string.Format("НАЧАЛО ОТПРАВКИ. Email: {0}, Host: {1}, SSL: {2}", toEmail,
                            this._mailHost, this._mailSslEnabled));
                        smtp.Send(mailMessage);
                        GenLogger.Info(string.Format("КОНЕЦ ОТПРАВКИ. Email: {0}, Host: {1}, SSL: {2}", toEmail, this._mailHost,
                            this._mailSslEnabled));
                    }
                    catch (Exception ex) {
                        GenLogger.Error(string.Format("ОШИБКА ОТПРАВКИ. Email: {0}, Host: {1}, SSL: {2}", toEmail,
                            this._mailHost,
                            this._mailSslEnabled));
                    }
                }
            }
        }

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        /// <param name="toEmail">Кому</param>
        /// <param name="subject">Заголовок</param>
        /// <param name="body">Содержимое</param>
        public void SendEmailAsync(string toEmail, string subject, string body) {
            var mailMessage = new MailMessage {
                Subject = subject,
                From = new MailAddress(this._mailAddress),
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };
            var mimeType = new ContentType("text/html");
            var alternate = AlternateView.CreateAlternateViewFromString(string.Format(HmtlTemplate, body), mimeType);

            mailMessage.AlternateViews.Add(alternate);
            mailMessage.To.Add(new MailAddress(toEmail));

            var smtp = new SmtpClient {
                Host = this._mailHost,
                EnableSsl = this._mailSslEnabled,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this._mailCredentialsUserName, this._mailCredentialsPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                GenLogger.Info(string.Format("НАЧАЛО ОТПРАВКИ. " + subject));
                smtp.SendCompleted += (sender, args) => {
                    mailMessage.Dispose();
                    smtp.Dispose();
                    var token = args.UserState.ToString();
                    if (args.Cancelled)
                    {
                        Console.WriteLine("SENDINGS STATUS: CANCELLED. " + token);
                        GenLogger.Info(string.Format("SENDINGS STATUS (3): CANCELLED. Email: {0}, Host: {1}, SSL: {2}",
                        toEmail, this._mailHost,
                        this._mailSslEnabled, token));
                    }
                    if (args.Error != null)
                    {
                        Console.WriteLine("SENDINGS STATUS: ERROR. " + token + " " + args.Error.Message);
                        GenLogger.Error(string.Format("SENDINGS STATUS (3): ERROR. Email: {0}, Host: {1}, SSL: {2}",
                        toEmail, this._mailHost,
                        this._mailSslEnabled, token), args.Error);
                    }
                    else
                    {
                        Console.WriteLine("SENDINGS STATUS: SUCCESS. " + token);
                        GenLogger.Info(string.Format("SENDINGS STATUS (3): SUCCESS. Email: {0}, Host: {1}, SSL: {2}",
                        toEmail, this._mailHost, this._mailSslEnabled, token));
                    }
                };
                smtp.SendAsync(mailMessage, subject);
                GenLogger.Info(string.Format("КОНЕЦ ОТПРАВКИ. Email: {0}, Host: {1}, SSL: {2}", toEmail, this._mailHost,
                    this._mailSslEnabled));
            }
            catch (Exception ex)
            {
                GenLogger.Error(string.Format("ОШИБКА ОТПРАВКИ. Email: {0}, Host: {1}, SSL: {2}", toEmail,
                    this._mailHost,
                    this._mailSslEnabled));
            }
        }

        private const string HmtlTemplate =
            "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=UTF-8\"></HEAD><BODY><DIV>{0}</DIV></BODY></HTML>";
    }
}