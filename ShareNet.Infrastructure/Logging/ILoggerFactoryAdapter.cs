namespace WusNet.Infrastructure.Logging
{
    public interface ILoggerFactoryAdapter
    {
        // Methods
        ILogger GetLogger(string loggerName);

    }
}
