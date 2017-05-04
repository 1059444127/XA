using System.Collections.Generic;
using UIH.Mcsf.Core;
using UIH.XR.Basis.Proto;
using System;
using UIH.XA.GlobalParameter;
using UIH.Mcsf.Log;
using Google.ProtocolBuffers;

namespace UIH.XA.SessionManager
{
    public class ProcessCacheCommandHandler : ICLRCommandHandler
    {
        private Dictionary<string, ByteString> _processStatusDic = new Dictionary<string, ByteString>();
        private Dictionary<int, Func<SessionRequestCommand, ByteString>> _funDic = new Dictionary<int, Func<SessionRequestCommand, ByteString>>();
       
        public ProcessCacheCommandHandler()
        {
            _funDic.Add(SessionManagerFuncID.QueryProcessStatusID, QueryProcessStatus);
            _funDic.Add(SessionManagerFuncID.UpdateProcessStatusID, UpdateProcessStatus);
        }

        /// <summary>
        /// handle command
        /// </summary>
        /// <param name="context">CommandContext</param>
        /// <param name="result">ISyncResult</param>
        /// <returns></returns>
        public override int HandleCommand(CommandContext context, ISyncResult result)
        {
            try
            {
                SessionRequestCommand sCommand = SessionRequestCommand.ParseFrom(context.sSerializeObject);
                if (!sCommand.HasCommandId || sCommand.ParametersCount < 1)
                {
                    result.SetSerializedObject(new byte[]{});
                    return -1;
                }
                ByteString invokeResult = _funDic[sCommand.CommandId].Invoke(sCommand);
                if (null == invokeResult)
                {
                    return -1;
                }
                result.SetSerializedObject(invokeResult.ToByteArray());
                return 0;
            }
            catch (Exception ex)
            {
                CLRLogger.GetInstance().LogDevError("HandleCommand error:" + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Query processStatus by proxyName
        /// </summary>
        /// <param name="generalRequestCommand">GeneralRequestCommand object</param>
        /// <returns></returns>
        private ByteString QueryProcessStatus(SessionRequestCommand sRequestCommand)
        {
            ByteString proxyName = sRequestCommand.GetParameters(0);
            return _processStatusDic.ContainsKey(proxyName.ToStringUtf8()) ? _processStatusDic[proxyName.ToStringUtf8()] : null;
        }

        /// <summary>
        /// Updata processStatus
        /// </summary>
        /// <param name="generalRequestCommand">GeneralRequestCommand object</param>
        /// <returns></returns>
        private ByteString UpdateProcessStatus(SessionRequestCommand sRequestCommand)
        {
            if (sRequestCommand.ParametersCount < 2)
            {
                return null;
            }
            string proxyName = sRequestCommand.GetParameters(0).ToStringUtf8();
            ByteString processStatus = sRequestCommand.GetParameters(1);

            if (_processStatusDic.ContainsKey(proxyName))
            {
                _processStatusDic[proxyName] = processStatus;
            }
            else
            {
                _processStatusDic.Add(proxyName, processStatus);
            }
            return ByteString.CopyFromUtf8(string.Empty);
        }
    }
}
