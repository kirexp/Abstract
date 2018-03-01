using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Common {
    public static class SerializationHelper {
        public static string ToXml<T>(this T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            var ms = new MemoryStream();
            serializer.Serialize(ms, obj);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        //public static string ToXml<T>(this T obj)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    using (var writer = new StreamWriter(stream, Encoding.UTF8))
        //    {
        //        var xml = new XmlSerializer(typeof(T));
        //        xml.Serialize(writer, obj);
        //        var result = Encoding.UTF8.GetString(stream.ToArray());
        //        return result;
        //    }
        //}

        public static T FromXml<T>(string obj) {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(obj);
            return (T)serializer.Deserialize(reader);
        }

        public static string ToJson<T>(this T obj) {
            return JsonConvert.SerializeObject(obj,new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public static T DeserializeFromJson<T>(string value) {
            //var x = Activator.CreateInstance<T>();
            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }

        public static string Serialize<T>(T obj) {
            string result;
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter()) {
                serializer.Serialize(writer, obj);
                result = writer.ToString();
            }
            return result;
        }
    }
}