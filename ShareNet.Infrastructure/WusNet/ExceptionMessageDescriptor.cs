using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WusNet.Infrastructure.Globalization;

namespace WusNet.Infrastructure.WusNet
{
    public class ExceptionMessageDescriptor
    {
        // Properties
        public int ApplicationId { get; set; }

        public object[] Arguments { get; set; }

        public string MessageFormat { get; set; }

        public string MessageFormatResourceKey { get; set; }

        public ExceptionMessageDescriptor()
        {
            
        }
        public ExceptionMessageDescriptor(string messageFormatResourceKey, params object[] args)
            : this(messageFormatResourceKey, 0, args)
        {
        }


        public ExceptionMessageDescriptor(string messageFormatResourceKey, int applicationId = 0, params object[] args)
        {
            this.MessageFormatResourceKey = messageFormatResourceKey;
            if (applicationId>0)
            {
                this.ApplicationId = applicationId;

            }
            this.Arguments = args;

        }

        internal string GetExceptionMeassage()
        {
            string format = null;
            if (!string.IsNullOrEmpty(this.MessageFormatResourceKey))
            {
                if (this.ApplicationId > 0)
                {
                    format = ResourceAccessor.GetString(this.MessageFormatResourceKey, this.ApplicationId);
                }
                else
                {
                    format = ResourceAccessor.GetString(this.MessageFormatResourceKey);
                }
            }
            else if (!string.IsNullOrEmpty(this.MessageFormat))
            {
                format = this.MessageFormat;
            }
            if (format != null)
            {
                return string.Format(format, this.Arguments);
            }
            return string.Empty;
        }


    }
}
