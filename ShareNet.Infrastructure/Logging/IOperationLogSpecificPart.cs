namespace WusNet.Infrastructure.Logging
{
    /// <summary>
    /// 操作日志定义部分
    /// </summary>
    public interface IOperationLogSpecificPart
    {
        // Properties
        int ApplicationId { get; set; }
        string Description { get; set; }
        long OperationObjectId { get; set; }
        string OperationObjectName { get; set; }
        string OperationType { get; set; }
        string Source { get; set; }

    }
}
