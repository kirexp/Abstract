using System;
using System.IO;
using Newtonsoft.Json.Linq;
namespace Common
{
    public class ConfigHelper
    {
        private static object Get(string key) {
            var filePath = $"{Directory.GetCurrentDirectory()}\\appsettings.json";
            var configuration = File.ReadAllText(filePath);
            if (configuration == null) {
                throw new NullReferenceException("appsettings.json shouldnt be null");
            } else {
                var value = JObject.Parse(configuration)[key];
                if (value == null) return null;
                return value.ToObject<object>();
            }

        }
        public static T Get<T>(string key) where T:class {
            var value = Get(key);
            if (value != null) 
                return (T)Convert.ChangeType(value, typeof(T));
            return null;
        }
        public static T Get<T>(string key,T defaultValue) where T : struct{
            var value = Get(key);
            if (value != null)
                return (T)Convert.ChangeType(value, typeof(T));
            return defaultValue;
        }
        public static string GetConnectionString(string key) {
            var section = Get("ConnectionStrings");
            var currentConnectionString = JObject.Parse(section.ToJson())[key];
            return currentConnectionString.ToString();
        }
    }
}
