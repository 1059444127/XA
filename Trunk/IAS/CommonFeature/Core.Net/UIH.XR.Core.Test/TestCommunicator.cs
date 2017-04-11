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
using System.ComponentModel.Composition;

namespace UIH.XR.Core.Test
{
    [Export(typeof(ICommunicator))]
    public class TestCommunicator : ICommunicator
    {
        public void SendEvent(string sender, int eventID, string eventContent)
        {
            throw new NotImplementedException();
        }

        public void BroadcastEvent(string sender, int eventID, string eventContent)
        {
            throw new NotImplementedException();
        }

        public string SyncSendCommand(int cmdID, string receiver, string content, uint waitTime = 0, byte[] contentAsBytes = null)
        {
            throw new NotImplementedException();
        }

        public byte[] SyncSendCommand(int cmdID, string receiver, byte[] content, uint waitTime = 0, string contentAsString = null)
        {
            throw new NotImplementedException();
        }

        public void AsyncSendCommand(int cmdID, string receiver, string content, Action<byte[]> callbackOnReplyBytes = null, Action<string> callbackOnReplyString = null)
        {
            throw new NotImplementedException();
        }

        public void AsyncSendCommand(int cmdID, string receiver, byte[] content, Action<byte[]> callbackOnReplyBytes = null, Action<string> callbackOnReplyString = null)
        {
            throw new NotImplementedException();
        }

        public void RegisterCommandHandler(int cmdID, HandleMessageDelegate<string> handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterCommandHandler(int cmdID, HandleMessageDelegate<byte[]> handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterEventHandler(int channelID, int eventID, HandleMessageDelegate<string> handler)
        {
            throw new NotImplementedException();
        }

        public void RegisterEventHandler(int channelID, int eventID, HandleMessageDelegate<byte[]> handler)
        {
            throw new NotImplementedException();
        }

        public void SetChannelID(int channelID)
        {
            throw new NotImplementedException();
        }
    }
}
