﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WusNet.Infrastructure.Logging;

namespace WusNet.Infrastructure.WusNet
{
    public class CommonExceptionDescriptor:ExceptionDescriptor
    {
        // Methods
        public CommonExceptionDescriptor()
            : this(null)
        {
        }

        public CommonExceptionDescriptor(string message)
        {
            base.IsLogEnabled = true;
            base.LogLevel = LogLevel.Warning;
            base.Message = message;
        }

        public CommonExceptionDescriptor(string messageFormat, params object[] args)
            : this(string.Format(messageFormat, args))
        {
        }

        public override string GetLoggingMessage()
        {
            return (this.GetFriendlyMessage() + "--" + this.GetOperationContextMessage());
        }

        public CommonExceptionDescriptor WithEmailUnableToSend(string message)
        {
            string messageFormatResourceKey = "Exception_EmailUnableToSend";
            base.MessageDescriptor = new ExceptionMessageDescriptor(messageFormatResourceKey, new object[] { new { message = message } });
            return this;
        }

        public CommonExceptionDescriptor WithFloodDenied()
    {
        string messageFormatResourceKey = "Exception_FloodDenied";
        base.MessageDescriptor = new ExceptionMessageDescriptor(messageFormatResourceKey, new object[] {  });
        return this;
    }

        public CommonExceptionDescriptor WithRegisterDenied()
    {
        string messageFormatResourceKey = "Exception_RegisterDenied";
        base.MessageDescriptor = new ExceptionMessageDescriptor(messageFormatResourceKey, new object[] {  });
        return this;
    }

        public CommonExceptionDescriptor WithUnauthorizedAccessException()
    {
        string messageFormatResourceKey = "Exception_UnauthorizedAccessException";
        base.MessageDescriptor = new ExceptionMessageDescriptor(messageFormatResourceKey, new object[] { });
        return this;
    }

        public CommonExceptionDescriptor WithUnknownError()
    {
        string messageFormatResourceKey = "Exception_UnknownError";
        base.MessageDescriptor = new ExceptionMessageDescriptor(messageFormatResourceKey, new object[] {  });
        return this;
    }

    }
}
