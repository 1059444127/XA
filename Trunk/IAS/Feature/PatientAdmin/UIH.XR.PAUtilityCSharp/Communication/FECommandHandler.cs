using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Communication
{
    public class FECommandHandler : ICLRCommandHandler
    {
        override /// <summary>
            /// HandleCommand
            /// </summary>
            /// <param name="pContext">CommandContext</param>
            /// <param name="pSyncResult">ISyncResult</param>
            /// <returns>int</returns>
        public int HandleCommand(CommandContext pContext, ISyncResult pSyncResult)
        {
            var guid = Guid.NewGuid().ToString();

            string commandId = pContext != null ? pContext.iCommandId.ToString() : string.Empty;

            GlobalDefinition.LoggerWrapper.LogDevInfo("####################Receive Command.#######################" +
                                                      guid + "########commandid:" + commandId);

            if (null == pContext || null == pSyncResult)
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Command.#######################" + guid + "########commandid:" +
                    commandId);
                GlobalDefinition.LoggerWrapper.LogDevError("ICLRCommandHandler's param is null.");
                return 0;
            }

            if (null == CmdMap || null == CmdMapByte)
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Command.#######################" + guid + "########commandid:" +
                    commandId);
                GlobalDefinition.LoggerWrapper.LogDevError("Not initialize the CmdMap");
                return 0;
            }

            if (CmdMap.ContainsKey(pContext.iCommandId))
            {
                string result = CmdMap[pContext.iCommandId](pContext);

                pSyncResult.SetSerializedString(result);
            }
            else if (CmdMapByte.ContainsKey(pContext.iCommandId))
            {
                byte[] result = CmdMapByte[pContext.iCommandId](pContext);

                pSyncResult.SetSerializedObject(result);
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Command.#######################" + guid + "########commandid:" +
                    commandId);
                GlobalDefinition.LoggerWrapper.LogDevError("Not process this command id: " +
                                                           pContext.iCommandId.ToString());
                return 0;
            }

            GlobalDefinition.LoggerWrapper.LogDevInfo(pSyncResult.GetSerializedString());

            GlobalDefinition.LoggerWrapper.LogDevInfo("####################processed Command.#######################" +
                                                      guid + "########commandid:" + commandId);
            return 0;
        }

        public delegate string CmdHandler(CommandContext param);

        public delegate byte[] CmdHandlerByte(CommandContext param);

        private static Dictionary<int, CmdHandler> cmdMap = new Dictionary<int, CmdHandler>();

        private static Dictionary<int, CmdHandlerByte> cmdMapByte = new Dictionary<int, CmdHandlerByte>();

        /// <summary>
        /// AddItemToCommandMap
        /// </summary>
        /// <param name="key">int</param>
        /// <param name="command">CmdHandler</param>
        public static void AddItemToCommandMap(int key, CmdHandler command)
        {
            if (null == CmdMap)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("CmdMap is  null");
                return;
            }

            if (!CmdMap.ContainsKey(key))
            {
                CmdMap.Add(key, command);
            }
        }

        /// <summary>
        /// AddItemToByteCommandMap
        /// </summary>
        /// <param name="key">int</param>
        /// <param name="command">CmdHandlerByte</param>
        public static void AddItemToByteCommandMap(int key, CmdHandlerByte command)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static void AddItemToByteCommandMap() start");
            if (null == CmdMapByte)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("CmdmapByte is  null");
                return;
            }

            if (!CmdMapByte.ContainsKey(key))
            {
                CmdMapByte.Add(key, command);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public static void AddItemToByteCommandMap() end");
        }

        public static Dictionary<int, CmdHandler> CmdMap
        {
            get { return cmdMap; }
            set { cmdMap = value; }
        }

        public static Dictionary<int, CmdHandlerByte> CmdMapByte
        {
            get { return cmdMapByte; }
            set { cmdMapByte = value; }
        }
    }
}
