using System;
using NLog;

namespace Abb.SimpleChat.Infrastructure
{
    public class NLogger
    {

        private static NLog.Logger log = LogManager.GetCurrentClassLogger();
        public void MyMethod1()
        {
            log.Info("informational message");
            log.Warn("warning message");
            log.Error("error message");
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex, "error message");
                throw;
            }
        }


    }
}
