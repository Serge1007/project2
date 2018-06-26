using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abb.SimpleChat.Infrastructure.Logger;
using System;
using NLog;
using System.IO;

namespace Abb.SimpleChat.LogTest
{
    [TestClass]
    public class LogTest
    {
        private const string DatabasePath =
            "C:\\Users\\Сергей\\source\\repos\\Abb.SimpleChat\\Test\\Abb.SimpleChat.LogTest";
        private const string DatabaseName = "file.txt";
        NLogLogger log;
        Exception e;
        bool contain;
        string line;

        [TestInitialize]
        public void Init()
        {
            log = new NLogLogger($"{DatabasePath}\\{DatabaseName}");
        }

        [TestMethod]
        public void CallMethods()
        {
            log.Debug("DebugMessage");
            log.Info("InfoMessage");
            log.Warn("WarnMessage");
            log.Error("ErrorMessage");
            log.Error("ErrorMessage",e);

            Assert.AreEqual("ErrorMessage", "ErrorMessage");

            StreamReader file =new StreamReader($"{DatabasePath}\\{DatabaseName}");
            while ((line = file.ReadLine()) != null)
            {
                contain=line.Contains("InfoMessage");
                if (contain == true) break;
            }

            file.Close();
            Assert.AreEqual(contain, true);
        }

        [TestCleanup]
        public void End()
        {
            if (File.Exists($"{DatabasePath}\\{DatabaseName}"))
                File.Delete($"{DatabasePath}\\{DatabaseName}");
        }
    }
}
