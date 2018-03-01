using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Common {
    public static class EnumExtensions<T> {
        private static readonly Dictionary<T, DisplayAttribute> Cache = new Dictionary<T, DisplayAttribute>();

        static EnumExtensions() {
            var type = typeof(T);
            foreach (var member in type.GetFields()) {
                var custAttr = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (!custAttr.Any())
                    continue;
                Cache[(T)Enum.Parse(type, member.Name)] = (DisplayAttribute)custAttr.First();
            }
        }

        private static DisplayAttribute GetDisplayAttribute(T value) {
            DisplayAttribute result;
            Cache.TryGetValue(value, out result);
            return result;
        }

        public static string GetDisplayName(T value) {
            var attr = GetDisplayAttribute(value);
            return attr == null ? value.ToString() : attr.GetName();
        }
        public static string GetDisplayName(T value, string culture)
        {
            var attr = GetDisplayAttribute(value);
            var cult = CultureInfo.CreateSpecificCulture(culture);
            var tempCult = Thread.CurrentThread.CurrentUICulture;
            // TODO: ПЕРЕДЕЛАТЬ ЛОКАЛИЗАЦИЮ GetDisplayName
            Thread.CurrentThread.CurrentUICulture = cult;
            var result = attr == null ? value.ToString() : attr.GetName();
            Thread.CurrentThread.CurrentUICulture = tempCult;
            return result;
        }
    }

    public static class EnumExtensions {
        public static string GetDisplayNameSlim<T>(/*this*/ T value) where T : struct, IConvertible, IComparable, IFormattable {
            if (!typeof(T).IsEnum) {
                throw new ArgumentException("T must be an enum.");
            }
            return EnumExtensions<T>.GetDisplayName(value);
        }

        public static string GetDisplayNameWithParamSlim<T>(/*this*/ T value, string culture) where T : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum.");
            }
            return EnumExtensions<T>.GetDisplayName(value, culture);
        }

        public static string GetDisplayName(this Enum value) {
            var property = typeof(EnumExtensions).GetMethod("GetDisplayNameSlim");
            var generic = property.MakeGenericMethod(value.GetType());
            return (string)generic.Invoke(null, new object[] { value });
        }

        public static string GetDisplayName(this Enum value, string culture)
        {
            var property = typeof(EnumExtensions).GetMethod("GetDisplayNameWithParamSlim");
            var generic = property.MakeGenericMethod(value.GetType());
            return (string)generic.Invoke(null, new object[] { value, culture });
        }

        public static T ToEnum<T>(this string value) where T : struct, IConvertible, IComparable, IFormattable {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ToCurrentType<T>(this Enum value) {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
