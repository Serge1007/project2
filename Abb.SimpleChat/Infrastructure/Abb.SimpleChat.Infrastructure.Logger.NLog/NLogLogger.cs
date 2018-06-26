using NLog;
using System;

namespace Abb.SimpleChat.Infrastructure.Logger
{
    public class NLogLogger : ILogger
    {
        private const string LoggerName = "SimpleChatLogger";

        private readonly NLog.Logger log; 

        public NLogLogger(string derictory)
        {
             log = NLog.LogManager.GetLogger(LoggerName);
             var config = new NLog.Config.LoggingConfiguration();

             var logfile = new NLog.Targets.FileTarget("logfile") { FileName = derictory };

             config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

             NLog.LogManager.Configuration = config;
        }

        public void Error(string message, Exception e)
        {
            log.Error(e, message);
            //+вывод сообщений пользователю
            
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }
    }
}
