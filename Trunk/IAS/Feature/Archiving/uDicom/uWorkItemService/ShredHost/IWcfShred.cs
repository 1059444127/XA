namespace uDicom.WorkQueue.Service.ShredHost
{
    public interface IWcfShred
    {
        int SharedHttpPort
        {
            get;
            set;
        }

        int SharedTcpPort
        {
            get;
            set;
        }

        string ServiceAddressBase
        {
            get;
            set;
        }
    }
}