using System.ServiceModel;
using System.ServiceModel.Channels;

namespace uDicom.WorkQueue.Service.ShredHost
{
    public class ServiceEndpointDescription
    {
        public ServiceEndpointDescription(string name, string description)
        {
            ServiceName = name;
            ServiceDescription = description;
        }

        public ServiceHost ServiceHost { get; set; }

        public Binding Binding { get; set; }

        public string ServiceName { get; }

        public string ServiceDescription { get; }
    }
}