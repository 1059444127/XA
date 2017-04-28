using UIH.Mcsf.Core;
using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Pipeline.Dictionary;
using System.Text;
using UIH.XR.GlobalParameter;
using UIH.Mcsf.Log;

namespace UIH.XR.SessionManager
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
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.StoreSessionDataID, new string[] { SessionUtility.SerializeDataHead(_dataHead) });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.IAS_SESSION_CACHE_SYSTEM_DATA, buf);
            int result = _systemCommunicationProxy.AsyncSendCommand(context);
            CLRLogger.GetInstance().LogDevInfo(string.Format("Commit result:{0},proxyName:{1}", result.ToString(), _systemCommunicationProxy.GetName()));
            return 0 == result ? true : false;
        }

        /// <summary>
        /// send command to session to get the dataHead
        /// </summary>
        /// <returns></returns>
        public bool Refresh()
        {
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.QuerySessionDataID, new string[] { });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.IAS_SESSION_CACHE_SYSTEM_DATA, buf);
            ISyncResult result = _systemCommunicationProxy.SyncSendCommand(context);
            CLRLogger.GetInstance().LogDevInfo(string.Format("Commit result:{0},proxyName:{1}", result.GetCallResult().ToString(), _systemCommunicationProxy.GetName()));
            _dataHead = SessionUtility.DeserializeDataHead(Encoding.Default.GetString(result.GetSerializedObject()));
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
        public void Reset()
        {
            _dataHead = new DicomAttributeCollection();
        }

        public bool SetImageHeadData(uint tagCode, string value)//该方法需要重载
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead[tagCode].SetString(0, value);
            }
            else
            {
                DicomVR TagVR = new DicomVR(VR.CS);
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
