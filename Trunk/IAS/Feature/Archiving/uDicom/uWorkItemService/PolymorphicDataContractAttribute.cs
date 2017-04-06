using System;

namespace uDicom.WorkQueue.Service
{
    public class PolymorphicDataContractException : Exception
    {
        public PolymorphicDataContractException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
