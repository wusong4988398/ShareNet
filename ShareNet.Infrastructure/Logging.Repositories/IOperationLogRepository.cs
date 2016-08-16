using System;
using WusNet.Infrastructure.Repositories;
using WusNet.Infrastructure.WusNet;

namespace WusNet.Infrastructure.Logging.Repositories
{
    public interface IOperationLogRepository : IRepository<OperationLogEntry>
    {
        // Methods
        int Clean(DateTime? startDate, DateTime? endDate);
        PagingDataSet<OperationLogEntry> GetLogs(OperationLogQuery query, int pageSize, int pageIndex);

    }
}
