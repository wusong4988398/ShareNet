using System;
using System.IO;
using log4net;
using log4net.Config;
using ShareNet.Infrastructure.Utilities;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Logging.Log4Net
{
    public class Log4NetLoggerFactoryAdapter:ILoggerFactoryAdapter
    {
        // Fields
        private static bool _isConfigLoaded;


        public Log4NetLoggerFactoryAdapter()
            : this("~/Config/log4net.config")

        {
            
        }

        public Log4NetLoggerFactoryAdapter(string configFileName)
        {
            if (!_isConfigLoaded)
            {
                IRunningEnvironment environment = DIContainer.Resolve<IRunningEnvironment>();
                if (string.IsNullOrEmpty(configFileName))
                {
                    configFileName = "~/Config/log4net.config";

                }
                FileInfo configFile = new FileInfo(WebUtility.GetPhysicalFilePath(configFileName));
                if (!configFile.Exists)
                {
                    throw new ApplicationException(string.Format("log4net配置文件 {0} 未找到", configFile.FullName));

                }
                if (environment.IsFullTrust)
                {
                    XmlConfigurator.ConfigureAndWatch(configFile);

                }
                else
                {
                    XmlConfigurator.Configure(configFile);

                }
                _isConfigLoaded = true;


            }
        }

        public ILogger GetLogger(string loggerName)
        {
            return new Log4NetLogger(LogManager.GetLogger(loggerName));
        }
    }
}
