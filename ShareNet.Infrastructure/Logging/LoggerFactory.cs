using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Logging
{
    public static class LoggerFactory
    {
        public static ILogger GetLogger()
        {
            return GetLogger("tunynet");
        }

        public static ILogger GetLogger(string loggerName)
        {
            return DIContainer.Resolve<ILoggerFactoryAdapter>().GetLogger(loggerName);
        }

    }
}
