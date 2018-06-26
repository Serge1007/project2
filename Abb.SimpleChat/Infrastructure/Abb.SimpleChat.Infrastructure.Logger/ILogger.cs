using System;

namespace Abb.SimpleChat.Infrastructure.Logger
{
    public interface ILogger
    {
        
        void Warn(string message);
        void Info(string message);
        void Error(string message, Exception e);
        void Error(string message);
        void Debug(string message);
    }
}
