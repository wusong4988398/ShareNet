namespace WusNet.Infrastructure.WusNet
{
    public interface IEntity
    {
        object EntityId { get; }
        bool IsDeletedInDatabase { get; set; }
    }
}
