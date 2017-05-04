using UIH.Mcsf.Core;
using UIH.XA.GlobalParameter;
using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Log;
using Google.ProtocolBuffers;

namespace UIH.XA.SessionManager
{
    public class ProcessCacheProxy
    {
        private static readonly object locker = new object();
        private volatile static ProcessCacheProxy _processCache = null;
        private ICommunicationProxy _processCommunicationProxy;
        public DicomAttributeCollection _dataHead { get;private set; }
        private string _proxyName;

        /// <summary>
        /// private construcr
        /// </summary>
        private ProcessCacheProxy()
        {
            _dataHead = new DicomAttributeCollection();
        }

        /// <summary>
        /// Get ProcessCacheProxy single instance
        /// </summary>
        /// <returns>ProcessCacheProxy single instance</returns>
        public static ProcessCacheProxy GetInstance()
        {
            if (null == _processCache)
            {
                lock (locker)
                {
                    if (null == _processCache)
                    {
                        _processCache = new ProcessCacheProxy();
                    }
                }
            }
            return _processCache;
        }

        public void Commit()
        {
            byte[] serilizedDataHead = SessionUtility.SerializeDataHead(_dataHead);
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.UpdateProcessStatusID, new ByteString[] { ByteString.CopyFromUtf8(_proxyName), ByteString.CopyFrom(serilizedDataHead) });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.XA_SYSTEMSESSION_PROCESS_CACHE, buf);
            ISyncResult result = _processCommunicationProxy.SyncSendCommand(context);
            if (result.GetCallResult() != 0)
            {
                CLRLogger.GetInstance().LogDevError(string.Format("ProcessCacheProxy Commit failed, proxyName is {0},GetCallResult is {1}.", _proxyName, result.GetCallResult()));
            }
        }

        /// <summary>
        /// Init proxy
        /// </summary>
        /// <param name="processCommunicationProxy">proxy object</param>
        /// <returns></returns>
        public bool Initialize(ICommunicationProxy processCommunicationProxy)
        {
            if (null == processCommunicationProxy)
            {
                CLRLogger.GetInstance().LogDevError("ProcessCacheProxy is null.");
                return false;
            }
            _processCommunicationProxy = processCommunicationProxy;
            _proxyName = _processCommunicationProxy.GetName();
            return true;
        }

        /// <summary>
        /// add方法是每个class将自身的datahead merge进cache proxy中的datahead中
        /// </summary>
        /// <param name="serializedObj"></param>
        public void Add(SerializableBase serializedObj)
        {
            if (null == serializedObj)
            {
                CLRLogger.GetInstance().LogDevError("SerializableBase is null.");
            }
            foreach (DicomAttribute dicomAttr in serializedObj._dataHead)
            {
                if (_dataHead.Contains(dicomAttr.Tag))
                {
                    string value = null;
                    dicomAttr.GetString(0, out value);
                    _dataHead[dicomAttr.Tag.Value].SetString(0, value);
                }
                else
                {
                    _dataHead.AddDicomAttribute(dicomAttr);
                }
            }
        }

        /// <summary>
        /// Get datahead form session manager,this datahead is process's status
        /// </summary>
        public bool Refresh()
        {
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.QueryProcessStatusID, new ByteString[] { ByteString.CopyFromUtf8(_proxyName) });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.XA_SYSTEMSESSION_PROCESS_CACHE, buf);
            ISyncResult result = _processCommunicationProxy.SyncSendCommand(context);
            if (0 == result.GetCallResult())
            {
                _dataHead = DicomAttributeCollection.Deserialize(result.GetSerializedObject());
                return true;
            }
            CLRLogger.GetInstance().LogDevError(string.Format("ProcessCacheProxy Refresh failed, proxyName is {0},GetCallResult is {1}.", _proxyName, result.GetCallResult()));
            return false;
        }
    }
}
