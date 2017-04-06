using System.Runtime.Serialization;

namespace uDicom.WorkQueue.Service.Common
{
    /// <summary>
	/// Base class for all objects that serve as WCF data contracts.
	/// </summary>
	[DataContract]
    public abstract class DataContractBase : IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }
}
