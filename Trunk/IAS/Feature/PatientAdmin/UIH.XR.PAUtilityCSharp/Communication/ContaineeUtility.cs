using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using UIH.Mcsf.Core;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Communication
{
    public class ContaineeUtility
    {
        public const int CommandTimeOut = 30000; //unit: ms

        public static ObservableCollection<FECommandCallbackHandler> _feCmdCallbackList =
            new ObservableCollection<FECommandCallbackHandler>();

        public static ObservableCollection<FEEventHandler> _feEvtHandlerList =
            new ObservableCollection<FEEventHandler>();

        public static ICommunicationProxy _feCommProxy { get; set; }

        public ICommunicationMap _commHelper { get; set; }

        #region Mehtod

        /// <summary>
        /// Register containee events to communication with mainFrame by communication proxy.
        /// </summary>
        /// <param name="commProxy">Use a interface to communication with mainFrame.</param>
        public void Register(ICommunicationProxy commProxy, ICommunicationMap commMap)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo(
                "enter function --- ContaineeUtility.Register(ICommunicationProxy commProxy, ICommunicationMap commMap)");

            if (null == commProxy)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("PRFE Containee CommunicationProxy is null.");

                return;
            }

            _commHelper = commMap;

            if (null != _commHelper)
            {
                _commHelper.RegisterCommand();
            }
        }

        /// <summary>
        /// Call this method send command to other containees.
        /// </summary>
        /// <returns>null or -1: send command failed.Otherwise, it is a ISyncResult object that included detail info.</returns>
        public static object SendCommand(CommandContext cmdCtx, CommandType cmdType)
        {
            return SendCommand(_feCommProxy, cmdCtx, cmdType);
        }

        /// <summary>
        /// Call this method send command to other containees.
        /// </summary>
        /// <returns>null or -1: send command failed.Otherwise, it is a ISyncResult object that included detail info.</returns>
        public static object SendCommand(ICommunicationProxy commProxy, CommandContext cmdCtx, CommandType cmdType)
        {
            GlobalDefinition.LoggerWrapper.LogDevInfo("enter ContaineeUtility.SendCommand");
            if (null == commProxy)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("Containee CommunicationProxy is null.");
                return null;
            }

            try
            {
                if (null != cmdCtx)
                {
                    string content = String.Format("CommandID: {0} ||| Receiver: {1} ||| Sender: {2}",
                        cmdCtx.iCommandId.ToString(),
                        //cmdCtx.sStringObject,
                        cmdCtx.sReceiver,
                        cmdCtx.sSender);

                    GlobalDefinition.LoggerWrapper.LogDevInfo(content);


                    ISyncResult pResult = null;
                    int result = -1;
                    switch (cmdType)
                    {
                        case CommandType.AsyncCommand:
                            GlobalDefinition.LoggerWrapper.LogDevInfo("case CommandType.AsyncCommand");
                            result = commProxy.AsyncSendCommand(cmdCtx);
                            break;
                        case CommandType.SyncCommand:
                            GlobalDefinition.LoggerWrapper.LogDevInfo("case CommandType.SyncCommand");
                            pResult = commProxy.SyncSendCommand(cmdCtx);
                            result = pResult.GetCallResult();
                            break;
                        default:
                            GlobalDefinition.LoggerWrapper.LogDevError("case default");
                            result = commProxy.AsyncSendCommand(cmdCtx);
                            break;
                    }

                    if (result == 0)
                    {
                        GlobalDefinition.LoggerWrapper.LogDevInfo("Send command " + cmdCtx.iCommandId.ToString() +
                                                                  " to " + cmdCtx.sReceiver + " successfully.");

                        if (cmdType == CommandType.SyncCommand)
                        {
                            return pResult;
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        ContaineeUtility._feCmdCallbackList.Remove(cmdCtx.pCommandCallback as FECommandCallbackHandler);
                        GlobalDefinition.LoggerWrapper.LogDevError(
                            string.Format("Send command {0} to {1} failed. Result = {2}", cmdCtx.iCommandId.ToString(),
                                cmdCtx.sReceiver, result.ToString()));
                        return null;
                    }
                }
                else
                {
                    GlobalDefinition.LoggerWrapper.LogDevError("Command param is error.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// CreateCommandConttext
        /// </summary>
        /// <returns>CommandContext</returns>
        public static CommandContext CreateCommandConttext()
        {
            var cmdCtx = new CommandContext();
            cmdCtx.sSender = ContaineeUtility._feCommProxy.GetName();
            cmdCtx.pCommandCallback = null;
            cmdCtx.sStringObject = "";

            return cmdCtx;
        }

        #endregion
    }
}
