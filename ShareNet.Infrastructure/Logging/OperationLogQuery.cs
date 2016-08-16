using System;

namespace WusNet.Infrastructure.Logging
{
    public class OperationLogQuery
    {
        // Fields
        public int? ApplicationId;
        public DateTime? EndDateTime;
        public string Keyword;
        public string OperationType;
        public string Operator;
        public DateTime? StartDateTime;

        // Properties
        public long? OperatorUserId { get; set; }

        public string Source { get; set; }

    }
}
