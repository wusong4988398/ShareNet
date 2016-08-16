using System;


namespace WusNet.Infrastructure.Logging
{
    public interface ILogger
    {
        // Methods
        bool IsEnabled(LogLevel level);
        void Log(LogLevel level, object message);
        void Log(LogLevel level, Exception exception, object message);
        void Log(LogLevel level, string format, params object[] args);

    }
}
