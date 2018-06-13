using System;

namespace Abb.SimpleChat.Infrastructure.Logger
{
    public class NLogLogger : ILogger
    {
        private const string LoggerName = "SimpleChatLogger";

        private readonly NLog.Logger log; 

        public NLogLogger()
        {
            log = NLog.LogManager.GetLogger(LoggerName);
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



        public void Warn(string message)
        {
            log.Warn(message);
        }
    }
}
