namespace WusNet.Infrastructure.Logging
{
    public interface IOperationLogSpecificPartProcesser<TEntity>
    {
        // Methods
        void Process(TEntity entity, string eventOperationType, IOperationLogSpecificPart operationLogSpecificPart);
        void Process(TEntity entity, string eventOperationType, TEntity historyData, IOperationLogSpecificPart operationLogSpecificPart);

    }
}
