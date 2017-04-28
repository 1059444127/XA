using UIH.Mcsf.Core;
using UIH.XR.GlobalParameter;
using UIH.Mcsf.Pipeline.Data;
using System.Text;
using UIH.Mcsf.Log;

namespace UIH.XR.SessionManager
{
    public class ProcessCacheProxy
    {
        private static readonly object locker = new object();
        private volatile static ProcessCacheProxy _processCache = null;
        private ICommunicationProxy _processCommunicationProxy;
        public DicomAttributeCollection _dataHead { get; private set; }

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
        public ProcessCacheProxy GetInstance()
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

        public void Commit(string proxyName)
        {
            if (string.IsNullOrEmpty(proxyName))
            {
                CLRLogger.GetInstance().LogDevError("Commit false,proxyName is null or empty.");
                return;
            }
            string serilizedDataHead = SessionUtility.SerializeDataHead(_dataHead);
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.UpdateProcessStatusID, new string[] { proxyName, serilizedDataHead });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.IAS_SESSION_CACHE_PROCESS_STATUS, buf);
            ISyncResult result = _processCommunicationProxy.SyncSendCommand(context);
            if (result.GetCallResult() != 0)
            {
                CLRLogger.GetInstance().LogDevError(string.Format("ProcessCacheProxy Commit failed, proxyName is {0},GetCallResult is {1}.", proxyName, result.GetCallResult()));
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
            return true;
        }

        public void Add(SerializableBase serializedObj)
        {
            if (null == serializedObj)
            {
                CLRLogger.GetInstance().LogDevError("SerializableBase is null.");
            }
            serializedObj.SetImageHead(_dataHead);
        }

        /// <summary>
        /// Get datahead form session manager,this datahead is process's status
        /// </summary>
        /// <param name="proxyName">proxy name</param>
        public bool Refresh(string proxyName)
        {
            if (string.IsNullOrEmpty(proxyName))
            {
                CLRLogger.GetInstance().LogDevError("Refresh false,proxyName is null or empty.");
                return false;
            }
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.QueryProcessStatusID, new string[] { proxyName });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.IAS_SESSION_CACHE_PROCESS_STATUS, buf);
            ISyncResult result = _processCommunicationProxy.SyncSendCommand(context);
            if (0 == result.GetCallResult())
            {
                _dataHead = DicomAttributeCollection.Deserialize(Encoding.Default.GetBytes(result.GetSerializedString()));//应用进程拿到该datahead， 需要通过SetImageHead方法获取其状态
                return true;
            }
            CLRLogger.GetInstance().LogDevError(string.Format("ProcessCacheProxy Refresh failed, proxyName is {0},GetCallResult is {1}.", proxyName, result.GetCallResult()));
            return false;
        }
    }
}
