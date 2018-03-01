using System;
using log4net;
using log4net.Config;

namespace Common {
    public static class GenLogger {
        static GenLogger() {
            XmlConfigurator.Configure();
        }

        private static readonly ILog Log = log4net.LogManager.GetLogger("General");

        public static void Error(string message, Exception exception) {
            Log.Error(message, exception);
        }

        public static void Error(string message) {
            Log.Error(message);
        }

        public static void Info(string message) {
            Log.Info(message);
        }

        public static void Fatal(string message, Exception exception) {
            Log.Fatal(message, exception);
        }

        public static void Debug(string message) {
            Log.Debug(message);
        }

        public static void Warn(string message) {
            Log.Warn(message);
        }       
    }
    public static class NHibLogger
    {
        static NHibLogger()
        {
            XmlConfigurator.Configure();
        }

        private static readonly ILog Log = log4net.LogManager.GetLogger("NHLog");

        public static void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Warn(string message)
        {
            Log.Warn(message);
        }
    }
    public static class EventLogger
    {
        static EventLogger()
        {
            XmlConfigurator.Configure();
        }

        private static readonly ILog Log = log4net.LogManager.GetLogger("System");

        public static void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Warn(string message)
        {
            Log.Warn(message);
        }
    }
    public static class AllocateLogger
    {
        static AllocateLogger()
        {
            XmlConfigurator.Configure();
        }

        private static readonly ILog Log = log4net.LogManager.GetLogger("Allocator");

        public static void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Warn(string message)
        {
            Log.Warn(message);
        }
    }
}