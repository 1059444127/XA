/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;

namespace UIH.XA.Core.XApp
{
    class DelegateCommandCallbackHandler : ICommandCallbackHandler
    {
        public const int NO_ERROR = 0;

        public const int TIMEOUT = 1;

        public DelegateCommandCallbackHandler(Action<string> callbackOnString, Action<byte[]> callbackOnBytes)
        {
            _callbackOnBytes = callbackOnBytes;
            _callbackOnString = callbackOnString;
        }

        public override int HandleReply(UIH.Mcsf.Core.IAsyncResult pAsyncResult)
        {
            if (pAsyncResult.GetCallResult() == Mcsf.Core.IAsyncResult.CallResult.ReceiveResponse)
            {
                if (_callbackOnBytes != null) _callbackOnBytes(pAsyncResult.GetSerializedObject());
                if (_callbackOnString != null) _callbackOnString(pAsyncResult.GetStringObject());
                return NO_ERROR;
            }
            else
            {
                return TIMEOUT;
            }
        }

        Action<byte[]> _callbackOnBytes;
        Action<string> _callbackOnString;
    }
}
