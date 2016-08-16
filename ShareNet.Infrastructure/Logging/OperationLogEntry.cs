using System;
using PetaPoco;

using WusNet.Infrastructure.Caching;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Logging
{
    [Serializable, PrimaryKey("Id", AutoIncrement = true), CacheSetting(false), TableName("sh_OperationLogs")]
    public class OperationLogEntry : IEntity, IOperationLogSpecificPart
    {
        public OperationLogEntry()
        {
            
        }

        public OperationLogEntry(OperatorInfo operatorInfo)
        {
            this.OperatorUserId = operatorInfo.OperatorUserId;
            this.OperatorIP = operatorInfo.OperatorIP;
            this.Operator = operatorInfo.Operator;
            this.AccessUrl = operatorInfo.AccessUrl;
            this.DateCreated = DateTime.UtcNow;

        }
        // Properties
        public string AccessUrl { get; set; }

        public int ApplicationId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Description { get; set; }

        public long Id { get; protected set; }

        public long OperationObjectId { get; set; }

        public string OperationObjectName { get; set; }

        public string OperationType { get; set; }

        public string Operator { get; set; }

        public string OperatorIP { get; set; }

        public long OperatorUserId { get; set; }

        public string Source { get; set; }

        object IEntity.EntityId
        {
            get
            {
                return this.Id;
            }
        }

        bool IEntity.IsDeletedInDatabase { get; set; }

       


    }
}
