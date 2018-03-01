using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Configuration;

namespace Common {
    [Obsolete]
    public static class ConfigHelper2 {
        public static T GetValue<T>(string name) {
            var value = ConfigurationManager.AppSettings[name];
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Get<T>(string key, T defaultValue) where T : struct {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null) {
                return defaultValue;
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string Get(string key) {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool TryGet<T>(string key, out T result) {
            result = default(T);
            try {
                result = GetValue<T>(key);
            } catch (Exception) {
                return false;
            }
            return true;
        }

        public static string GetConnectionString(string connectionName) {
            string connectionString;
            var processName = Process.GetCurrentProcess().ProcessName;
            if (processName == "iisexpress" || processName.Contains(".vshost")) {
                return ConfigurationManager.ConnectionStrings[connectionName].ToString();
            }

            var configuration = Process.GetCurrentProcess().ProcessName == "w3wp"
                ? WebConfigurationManager.OpenWebConfiguration("~")
                : ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var configSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
            if (configSection == null) {
                throw new ArgumentNullException("connectionStrings");
            }

            if (configSection.ElementInformation.IsLocked || configSection.SectionInformation.IsLocked) {
                connectionString = ConfigurationManager.ConnectionStrings[connectionName].ToString();
                return connectionString;
            }

            if (!configSection.SectionInformation.IsProtected) {
                configSection.SectionInformation.ProtectSection("RSAProtectedConfigurationProvider");
                configSection.SectionInformation.ForceSave = true;
                configuration.Save(ConfigurationSaveMode.Full);
            }
            connectionString = ConfigurationManager.ConnectionStrings[connectionName].ToString();
            return connectionString;
        }
    }
}