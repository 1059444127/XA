using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Communication
{
    public class FECommandCallbackHandler : ICommandCallbackHandler
    {
        public override int HandleReply(UIH.Mcsf.Core.IAsyncResult pAsyncResult)
        {
            if (null == pAsyncResult)
            {
                if (null != ContaineeUtility._feCmdCallbackList)
                {
                    ContaineeUtility._feCmdCallbackList.Remove(this);
                }

                GlobalDefinition.LoggerWrapper.LogDevError("ICommandCallbackHandler's pAsyncResult is null.");

                return 0;
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo("Receive the Callback Command from other containee.");
            }

            if (handler != null)
            {
                handler(pAsyncResult.GetStringObject());

                GlobalDefinition.LoggerWrapper.LogDevInfo(pAsyncResult.GetStringObject());

                handler = null;
            }

            if (null != ContaineeUtility._feCmdCallbackList)
            {
                ContaineeUtility._feCmdCallbackList.Remove(this);
            }
            return 0;
        }

        public event CmdCallbackHandler handler;

        public delegate void CmdCallbackHandler(object param);
    }
}
