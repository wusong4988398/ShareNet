using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WusNet.Infrastructure.Globalization;
using WusNet.Infrastructure.Logging;

namespace WusNet.Infrastructure.WusNet
{
    public abstract class ExceptionDescriptor
    {
        // Properties
        public bool IsLogEnabled { get; protected set; }

        public LogLevel LogLevel { get; protected set; }

        public string Message { get; set; }

        public ExceptionMessageDescriptor MessageDescriptor { get; set; }

        public abstract string GetLoggingMessage();
        public virtual string GetOperationContextMessage()
        {
            StringBuilder builder = new StringBuilder();
            HttpContext current = HttpContext.Current;
            if ((current != null) && (current.Request != null))
            {
                if (current.Request.Url != null)
                {
                    builder.AppendLine(string.Format(ResourceAccessor.GetString("Common_ExceptionUrl"), current.Request.Url.AbsoluteUri));
                }
                if (current.Request.RequestType != null)
                {
                    builder.AppendLine(string.Format(ResourceAccessor.GetString("Common_HttpMethod"), current.Request.RequestType));
                }
                if (current.Request.UserHostAddress != null)
                {
                    builder.AppendLine(string.Format(ResourceAccessor.GetString("Common_UserIP"), current.Request.UserHostAddress));
                }
                if (current.Request.UserAgent != null)
                {
                    builder.AppendLine(string.Format(ResourceAccessor.GetString("Common_UserAgent"), current.Request.UserAgent));
                }
            }
            return builder.ToString();
        }

        public virtual string GetFriendlyMessage()
        {
            if (!string.IsNullOrEmpty(this.Message))
            {
                return this.Message;
            }
            if (this.MessageDescriptor != null)
            {
                return this.MessageDescriptor.GetExceptionMeassage();
            }
            return string.Empty;
        }



    }
}
