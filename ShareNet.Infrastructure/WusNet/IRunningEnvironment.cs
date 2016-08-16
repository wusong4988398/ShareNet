namespace WusNet.Infrastructure.WusNet
{
    public interface IRunningEnvironment
    {
        void RestartAppDomain();
        bool IsFullTrust { get; }
    }
}
