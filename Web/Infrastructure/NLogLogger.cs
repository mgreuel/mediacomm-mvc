using System;

using NLog;

namespace MediaCommMvc.Web.Infrastructure
{
    public class NLogLogger : ILogger
    {
        private static readonly Logger InternalLogger = LogManager.GetLogger("default");

        public void Debug(string message)
        {
           InternalLogger.Debug(message);
        }

        public void Info(string message)
        {
            InternalLogger.Info(message);
        }

        public void Warn(string message)
        {
            InternalLogger.Warn(message);
        }

        public void Error(string message)
        {
            InternalLogger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            InternalLogger.Error(exception, message);
        }
    }
}