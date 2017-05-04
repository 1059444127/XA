/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System;
using UIH.Mcsf.Core;
using System.ComponentModel.Composition;
using UIH.XA.GlobalParameter;

namespace UIH.XA.Core.XApp
{
    [Export(typeof(ICommunicator))]
    public class XCommunicator : ICommunicator
    {
        const int NO_ERROR = 0;

        private ICommunicationProxy _communicationProxy;

        [ImportingConstructor]
        public XCommunicator(IAppContext appContext)
        {
            //_communicationProxy = appContext.DefaultCLRCommunicationProxy as ICommunicationProxy;
            _communicationProxy = appContext.GetObject<ICommunicationProxy>(AppContextObjectName.DefaultCommunicationProxy);
        }

        public void SendEvent(string sender, int eventID, string eventContent)
        {
            int errorCode = _communicationProxy.SendEvent(sender, eventID, eventContent);
            if (errorCode != NO_ERROR)
            {
                string msg = string.Format("{0} fails to send event with event id = {1}, errorCode = {2}, content = {3}", sender, eventID, errorCode, eventContent);
                throw new CommunicationException(errorCode, msg);
            }
        }

        public void BroadcastEvent(string sender, int eventID, string eventContent)
        {
            int errorCode = _communicationProxy.BroadcastEvent(sender, eventID, eventContent);
            if (errorCode != NO_ERROR)
            {
                string msg = string.Format("{0} fails to broadcast event with event id = {1}, errorCode = {2}, content = {3}", sender, eventID, errorCode, eventContent);
                throw new CommunicationException(errorCode, msg);
            }
        }

        public string SyncSendCommand(int cmdID, string receiver, string content,
            uint waitTime = 0, byte[] contentAsBytes = null)
        {
            CommandContext cmdContext = new CommandContext();
            cmdContext.iCommandId = cmdID;
            cmdContext.sReceiver = receiver;
            cmdContext.sStringObject = content;
            cmdContext.sSerializeObject = contentAsBytes;
            if (waitTime != 0) cmdContext.iWaitTime = waitTime;
            ISyncResult result = _communicationProxy.SyncSendCommand(cmdContext);
            if (result.GetCallResult() != NO_ERROR)
            {
                string msg = string.Format("{0} fails to send sync command with command id = {1}, errorCode = {2}, content = {3}",
                    _communicationProxy.GetName(), cmdID, result.GetCallResult(), content);
                throw new CommunicationException(result.GetCallResult(), msg);
            }
            return result.GetSerializedString();
        }

        public byte[] SyncSendCommand(int cmdID, string receiver, byte[] content,
            uint waitTime = 0, string contentAsString = null)
        {
            CommandContext cmdContext = new CommandContext();
            cmdContext.iCommandId = cmdID;
            cmdContext.sReceiver = receiver;
            cmdContext.sSerializeObject = content;
            cmdContext.sStringObject = contentAsString;
            if (waitTime != 0) cmdContext.iWaitTime = waitTime;
            ISyncResult result = _communicationProxy.SyncSendCommand(cmdContext);
            if (result.GetCallResult() != NO_ERROR)
            {
                string msg = string.Format("{0} fails to sync send command with command id = {1}, errorCode = {2}, content = {3}",
                    _communicationProxy.GetName(), cmdID, result.GetCallResult(), content);
                throw new CommunicationException(result.GetCallResult(), msg);
            }
            return result.GetSerializedObject();
        }

        public void AsyncSendCommand(int cmdID, string receiver, string content,
            Action<byte[]> callbackOnReplyBytes = null, Action<string> callbackOnReplyString = null)
        {
            CommandContext context = new CommandContext();
            context.iCommandId = cmdID;
            context.sReceiver = receiver;
            context.sStringObject = content;
            context.pCommandCallback = new DelegateCommandCallbackHandler(callbackOnReplyString, callbackOnReplyBytes);
            int errorCode = _communicationProxy.AsyncSendCommand(context);
            if (errorCode != NO_ERROR)
            {
                string msg = string.Format("{0} fails to async send command with command id = {1}, errorCode = {2}, content = {3}",
                    _communicationProxy.GetName(), cmdID, errorCode, content);
                throw new CommunicationException(errorCode, msg);
            }
        }

        public void AsyncSendCommand(int cmdID, string receiver, byte[] content,
            Action<byte[]> callbackOnReplyBytes = null, Action<string> callbackOnReplyString = null)
        {
            CommandContext context = new CommandContext();
            context.iCommandId = cmdID;
            context.sReceiver = receiver;
            context.sSerializeObject = content;
            context.pCommandCallback = new DelegateCommandCallbackHandler(callbackOnReplyString, callbackOnReplyBytes);
            int errorCode = _communicationProxy.AsyncSendCommand(context);
            if (errorCode != NO_ERROR)
            {
                string msg = string.Format("{0} fails to async send command with command id = {1}, errorCode = {2}, content = {3}",
                    _communicationProxy.GetName(), cmdID, errorCode, content);
                throw new CommunicationException(errorCode, msg);
            }
        }

        public void RegisterCommandHandler(int cmdID, HandleMessageDelegate<string> handler)
        {
            _communicationProxy.RegisterCommandHandlerEx(cmdID, new CmdHandlerEx(args => handler(args.sSender, args.sStringObject)));
        }

        public void RegisterCommandHandler(int cmdID, HandleMessageDelegate<byte[]> handler)
        {
            _communicationProxy.RegisterCommandHandlerEx(cmdID, new CmdHandlerEx(args => handler(args.sSender, args.sSerializeObject)));
        }

        public void RegisterEventHandler(int channelID, int eventID, HandleMessageDelegate<string> handler)
        {
            _communicationProxy.RegisterEventHandler(channelID, eventID, new StringEventHandler(handler));
        }

        public void RegisterEventHandler(int channelID, int eventID, HandleMessageDelegate<byte[]> handler)
        {
            _communicationProxy.RegisterEventHandler(channelID, eventID, new ByteEventHandler(handler));
        }

        public void SetChannelID(int channelID)
        {
            _communicationProxy.SetSenderChannelId(channelID);
        }
    }
}
