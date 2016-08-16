using System;
using WusNet.Infrastructure.Logging.Repositories;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Logging
{
    public class OperationLogService
    {
        // Fields
        private IOperationLogRepository repository;

        public OperationLogService():this(new OperationLogRepository())
        {
            
        }
        public OperationLogService(IOperationLogRepository repository)
        {
            this.repository = repository;
        }

        public int Clean(DateTime? startDate, DateTime? endDate)

        {
            return this.repository.Clean(startDate, endDate);
        }

        public long Create(OperationLogEntry entry)
        {
            this.repository.Insert(entry);
            return entry.Id;
        }

        public OperationLogEntry Create<TEntity>(TEntity entity, string operationType) where TEntity : class
        {
            return this.Create<TEntity>(entity, operationType, default(TEntity));
        }

        public OperationLogEntry Create<TEntity>(TEntity entity, string operationType, TEntity historyData) where TEntity : class
        {
            IOperatorInfoGetter getter = DIContainer.Resolve<IOperatorInfoGetter>();
            if (getter == null)
            {
                throw new ApplicationException("IOperatorInfoGetter not registered to DIContainer");
            }
            OperationLogEntry operationLogSpecificPart = new OperationLogEntry(getter.GetOperatorInfo());
            IOperationLogSpecificPartProcesser<TEntity> processer = DIContainer.Resolve<IOperationLogSpecificPartProcesser<TEntity>>();
            if (processer == null)
            {
                throw new ApplicationException(string.Format("IOperationLogSpecificPartProcesser<{0}> not registered to DIContainer", typeof(TEntity).Name));
            }
            if (historyData == null)
            {
                processer.Process(entity, operationType, operationLogSpecificPart);
            }
            else
            {
                processer.Process(entity, operationType, historyData, operationLogSpecificPart);
            }
            this.repository.Insert(operationLogSpecificPart);
            return operationLogSpecificPart;
        }

    }
}
