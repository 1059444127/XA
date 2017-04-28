using System.Collections.Generic;
using UIH.Mcsf.Core;
using UIH.XR.Basis.Proto;
using System;
using UIH.XR.GlobalParameter;
using UIH.Mcsf.Log;

namespace UIH.XR.SessionManager
{
    public class ProcessCacheCommandHandler : ICLRCommandHandler
    {
        private Dictionary<string, string> _processStatusDic = new Dictionary<string, string>();
        private Dictionary<int, Func<SessionRequestCommand, string>> _funDic = new Dictionary<int, Func<SessionRequestCommand, string>>();

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
                    CLRLogger.GetInstance().LogDevError("commandID or paramater error.");
                    result.SetSerializedString("commandID or paramater error.");
                    return -1;
                }
                string ret = _funDic[sCommand.CommandId].Invoke(sCommand);
                if (null == ret)
                {
                    return -1;
                }
                result.SetSerializedString(ret);
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
        private string QueryProcessStatus(SessionRequestCommand sRequestCommand)
        {
            string proxyName = sRequestCommand.GetParameters(0);
            return _processStatusDic.ContainsKey(proxyName) ? _processStatusDic[proxyName] : null;
        }

        /// <summary>
        /// Updata processStatus
        /// </summary>
        /// <param name="generalRequestCommand">GeneralRequestCommand object</param>
        /// <returns></returns>
        private string UpdateProcessStatus(SessionRequestCommand sRequestCommand)
        {
            if (sRequestCommand.ParametersCount < 2)
            {
                return null;
            }
            string proxyName = sRequestCommand.GetParameters(0);
            string processStatus = sRequestCommand.GetParameters(1);
            if (_processStatusDic.ContainsKey(proxyName))
            {
                _processStatusDic[proxyName] = processStatus;
            }
            else
            {
                _processStatusDic.Add(proxyName, processStatus);
            }
            return string.Empty;
        }
    }
}
