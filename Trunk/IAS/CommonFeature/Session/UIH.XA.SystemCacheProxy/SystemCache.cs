using UIH.Mcsf.Core;
using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Pipeline.Dictionary;
using UIH.XA.GlobalParameter;
using UIH.Mcsf.Log;
using Google.ProtocolBuffers;

namespace UIH.XA.SessionManager
{
    public class SystemCacheProxy
    {
        private ICommunicationProxy _systemCommunicationProxy;
        private DicomAttributeCollection _dataHead;//session模块的tag应该需要申请，防止tag号相同

        /// <summary>
        /// send command to session to set the dataHead
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.StoreSessionDataID, new ByteString[] { ByteString.CopyFrom(SessionUtility.SerializeDataHead(_dataHead)) });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.XA_SYSTEMSESSION_DATA_CACHE, buf);
            ISyncResult result = _systemCommunicationProxy.SyncSendCommand(context);
            return (0 == result.GetCallResult()) ? true : false;
        }

        /// <summary>
        /// send command to session to get the dataHead
        /// </summary>
        /// <returns></returns>
        public bool Refresh()
        {
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.QuerySessionDataID, new ByteString[] { });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.XA_SYSTEMSESSION_DATA_CACHE, buf);
            ISyncResult result = _systemCommunicationProxy.SyncSendCommand(context);
            CLRLogger.GetInstance().LogDevInfo(string.Format("Commit result:{0},proxyName:{1}", result.GetCallResult().ToString(), _systemCommunicationProxy.GetName()));
            _dataHead = SessionUtility.DeserializeDataHead(result.GetSerializedObject());
            return (0 == result.GetCallResult()) ? true : false;
        }

        /// <summary>
        /// Init proxy
        /// </summary>
        /// <param name="processCommunicationProxy">proxy object</param>
        /// <returns></returns>
        public bool Initialize(ICommunicationProxy proxy)
        {
            if (null == proxy)
            {
                CLRLogger.GetInstance().LogDevError("proxy is null");
                return false;
            }
            _systemCommunicationProxy = proxy;
            return true;
        }

        /// <summary>
        /// Create a new dataHead
        /// </summary>
        public SystemCacheProxy()
        {
            _dataHead = new DicomAttributeCollection();
        }

        /// <summary>
        /// 这里性能有消耗 大约 100ms,可以考虑申请固定的tag值分配给其余模块，session模块启动时或者初始化时 将这些 tag就add进
        /// </summary>
        /// <param name="tagCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetImageHeadData(uint tagCode, string value)//该方法需要重载
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead[tagCode].SetString(0, value);
            }
            else
            {
                DicomVR TagVR = new DicomVR(VR.CS);//此处有性能损耗可以优化
                DicomTag tag = new DicomTag(tagCode);
                tag.TagVR = TagVR;
                DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
                dicomAttr.SetString(0, value);
                _dataHead.AddDicomAttribute(dicomAttr);
            }
            return true;
        }

        public string GetImageHeadData(uint tagCode)//该方法需要重载
        {
            string value = null;
            if (_dataHead.Contains(tagCode))
            {
                _dataHead[tagCode].GetString(0, out value);
            }
            return value;
        }
    }
}
