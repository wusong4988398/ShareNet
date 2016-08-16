using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WusNet.Infrastructure.Logging;

namespace WusNet.Infrastructure.WusNet
{
    [Serializable]
    public class ExceptionFacade:Exception,ISerializable
    {
        // Fields
        private readonly ExceptionDescriptor exceptionDescriptor;

        // Methods
        public ExceptionFacade(string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            this.exceptionDescriptor = new CommonExceptionDescriptor(message);
        }
        public ExceptionFacade(ExceptionDescriptor exceptionDescriptor, Exception innerException = null)
            : base(null, innerException)
        {
            this.exceptionDescriptor = exceptionDescriptor;
        }
        public void Log()
        {
            if ((this.exceptionDescriptor != null) && this.exceptionDescriptor.IsLogEnabled)
            {
                if (base.InnerException != null)
                {
                    LoggerFactory.GetLogger().Log(this.exceptionDescriptor.LogLevel, this.exceptionDescriptor.GetLoggingMessage(), new object[] { base.InnerException });
                }
                else
                {
                    LoggerFactory.GetLogger().Log(this.exceptionDescriptor.LogLevel, this.exceptionDescriptor.GetLoggingMessage(), new object[0]);
                }
            }
        }

        // Properties
        public override string Message
        {
            get
            {
                if (this.exceptionDescriptor != null)
                {
                    return this.exceptionDescriptor.GetFriendlyMessage();
                }
                return base.Message;
            }
        }
        public string OperationContextMessage
        {
            get
            {
                return this.exceptionDescriptor.GetOperationContextMessage();
            }
        }



    }
}
