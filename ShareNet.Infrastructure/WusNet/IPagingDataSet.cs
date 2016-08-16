namespace WusNet.Infrastructure.WusNet
{
    public interface IPagingDataSet
    {
        // Properties
        int PageIndex { get; set; }
        int PageSize { get; set; }
        long TotalRecords { get; set; }

    }
}
