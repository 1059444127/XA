using UIH.Mcsf.Core;
using UIH.XR.Basis.Proto;
using System.Collections.Generic;
using System;
using UIH.XA.GlobalParameter;
using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Log;

namespace UIH.XA.SessionManager
{
    public class SystemCacheCommandHandler : ICLRCommandHandler
    {
        private DicomAttributeCollection _dataHead = new DicomAttributeCollection();
        private Dictionary<int, Func<SessionRequestCommand, byte[]>> _dicFunc = new Dictionary<int, Func<SessionRequestCommand, byte[]>>();

        public SystemCacheCommandHandler()
        {
            _dicFunc.Add(SessionManagerFuncID.QuerySessionDataID, QuerySystemSessionData);
            _dicFunc.Add(SessionManagerFuncID.DeliverSessionDataID, DeliverSystemSessionData);
            _dicFunc.Add(SessionManagerFuncID.StoreSessionDataID, StoreSystemSessionData);
        }

        public override int HandleCommand(CommandContext context, ISyncResult result)
        {
            try
            {
                SessionRequestCommand sRequestCommand = SessionRequestCommand.ParseFrom(context.sSerializeObject);
                if (!sRequestCommand.HasCommandId)
                {
                    CLRLogger.GetInstance().LogDevError("commandID error.");
                    result.SetSerializedString("commandID error.");
                    return -1;
                }
                byte[] invokeResult = _dicFunc[sRequestCommand.CommandId].Invoke(sRequestCommand);
                if (null == invokeResult)
                {
                    return -1;
                }
                result.SetSerializedObject(invokeResult);
                return 0;
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("HandleCommand error:" + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将deliver deadhead to fpd
        /// </summary>
        /// <param name="generalRequestCommand"></param>
        /// <returns></returns>
        private byte[] DeliverSystemSessionData(SessionRequestCommand sRequestCommand)
        {
            byte[] serializedDataHead = SessionUtility.SerializeDataHead(_dataHead);
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.FPD, CommunicationCommandID.XA_SYSTEMSESSION_DATA_CACHE, serializedDataHead);
            int result = new CLRContaineeBase().GetCommunicationProxy().AsyncSendCommand(context);
            return (0 == result) ? new byte[]{} : null;
        }

        /// <summary>
        /// Query system status by proxy name
        /// </summary>
        /// <param name="generalRequestCommand"></param>
        /// <returns></returns>
        private byte[] QuerySystemSessionData(SessionRequestCommand sRequestCommand)
        {
            return SessionUtility.SerializeDataHead(_dataHead);
        }

        /// <summary>
        /// Merge datahead
        /// </summary>
        /// <param name="generalRequestCommand"></param>
        /// <returns></returns>
        private byte[] StoreSystemSessionData(SessionRequestCommand sRequestCommand)//deadhead的merge,注意tag的vr
        {
            if (sRequestCommand.ParametersCount < 1)
            {
                return null;
            }
            byte[] serializeDataHead = sRequestCommand.GetParameters(0).ToByteArray();
            DicomAttributeCollection dAttributeCollection = SessionUtility.DeserializeDataHead(serializeDataHead);

            foreach (DicomAttribute dAttribute in dAttributeCollection)
            {
                if (_dataHead.Contains(dAttribute.Tag))
                {
                    string value = null;
                    dAttribute.GetString(0, out value);
                    _dataHead[dAttribute.Tag.Value].SetString(0, value);
                }
                else
                {
                    _dataHead.AddDicomAttribute(dAttribute);
                }
            }
            return new byte[] { };
        }
    }
}
