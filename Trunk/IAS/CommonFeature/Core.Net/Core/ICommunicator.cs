/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System;
namespace UIH.XA.Core
{
    /// <summary>
    /// Wrapper of the CommunicationProxy
    /// </summary>
    public interface ICommunicator
    {
        /// <summary>
        /// Send event in current channel limit.
        /// </summary>
        void SendEvent(string sender, int eventID, string eventContent);

        /// <summary>
        /// Broadcast the event without channel limit
        /// </summary>
        void BroadcastEvent(string sender, int eventID, string eventContent);

        /// <summary>
        /// Send command syncronized with string response needed.
        /// </summary>
        /// <param name="cmdID">command ID</param>
        /// <param name="receiver">the target receiver</param>
        /// <param name="content">command content as string</param>
        /// <param name="waitTime">the timeout interval in ms</param>
        /// <param name="contentAsBytes">command content as bytes, default null</param>
        /// <returns></returns>
        string SyncSendCommand(int cmdID, string receiver, string content,
            uint waitTime = 0, byte[] contentAsBytes = null);

        /// <summary>
        /// Send command syncronized with byte array response needed.
        /// </summary>
        /// <param name="cmdID">command ID</param>
        /// <param name="receiver">the target receiver</param>
        /// <param name="content">command content as byte[]</param>
        /// <param name="waitTime">the timeout interval in ms</param>
        /// <param name="contentAsString">command content as string, default null</param>
        /// <returns></returns>
        byte[] SyncSendCommand(int cmdID, string receiver, byte[] content,
            uint waitTime = 0, string contentAsString = null);

        /// <summary>
        /// Send command asyncronized with string content
        /// </summary>
        /// <param name="cmdID">command ID</param>
        /// <param name="receiver">the target receiver</param>
        /// <param name="content">command content as string</param>
        /// <param name="callbackOnReplyBytes">callback on bytes replied</param>
        /// <param name="callbackOnReplyString">callback on string replied</param>
        void AsyncSendCommand(int cmdID, string receiver, string content,
            Action<byte[]> callbackOnReplyBytes = null, Action<string> callbackOnReplyString = null);

        /// <summary>
        /// Send command asyncronized with byte array content
        /// </summary>
        /// <param name="cmdID">command ID</param>
        /// <param name="receiver">the target receiver</param>
        /// <param name="content">command content as byte[]</param>
        /// <param name="callbackOnReplyBytes">callback on bytes replied</param>
        /// <param name="callbackOnReplyString">callback on string replied</param>
        void AsyncSendCommand(int cmdID, string receiver, byte[] content,
            Action<byte[]> callbackOnReplyBytes = null, Action<string> callbackOnReplyString = null);

        /// <summary>
        /// Register command handler to handle command with string content replied.
        /// </summary>
        /// <param name="cmdID">command ID</param>
        /// <param name="handler">delegate to handle the command with string content</param>
        void RegisterCommandHandler(int cmdID, HandleMessageDelegate<string> handler);

        /// <summary>
        /// Register command handler to handle command with byte[] content replied.
        /// </summary>
        /// <param name="cmdID">command ID</param>
        /// <param name="handler">delegate to handle the command with byte[] content</param>
        void RegisterCommandHandler(int cmdID, HandleMessageDelegate<byte[]> handler);

        /// <summary>
        /// Register event handler on specified channel
        /// </summary>
        /// <param name="channelID">channel ID</param>
        /// <param name="eventID">event ID</param>
        /// <param name="handler">delegate handler to handle string replied</param>
        void RegisterEventHandler(int channelID, int eventID, HandleMessageDelegate<string> handler);

        /// <summary>
        /// Register event handler on specified channel
        /// </summary>
        /// <param name="channelID">channel ID</param>
        /// <param name="eventID">event ID</param>
        /// <param name="handler">delegate handler to handle byte[] replied</param>
        void RegisterEventHandler(int channelID, int eventID, HandleMessageDelegate<byte[]> handler);

        /// <summary>
        /// The channel ID for send event
        /// </summary>
        /// <param name="channelID">the channel ID</param>
        void SetChannelID(int channelID);
    }

    public delegate void HandleMessageDelegate<T>(string sender, T message);
}
