using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Communication
{
    public class FEEventHandler : IEventHandler
    {
        public override int HandleEvent(string sSender, int iChannelId, int iEventId, string sEvent)
        {
            var guid = Guid.NewGuid().ToString();

            GlobalDefinition.LoggerWrapper.LogDevInfo("####################Receive Event.#######################" + guid +
                                                      "########eventId:" + iEventId);

            if (String.IsNullOrEmpty(sSender))
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Event.#######################" + guid + "########eventId:" + iEventId);
                GlobalDefinition.LoggerWrapper.LogDevWarning("PRFE Event sender is empty ....");
                return 0;
            }

            if (null == EventMap || !EventMap.ContainsKey(iEventId))
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Event.#######################" + guid + "########eventId:" + iEventId);
                GlobalDefinition.LoggerWrapper.LogDevWarning("EventMap is null or not contains this event id");
                return 0;
            }

            if (null == EventMap[iEventId])
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Event.#######################" + guid + "########eventId:" + iEventId);
                GlobalDefinition.LoggerWrapper.LogDevWarning("There's no event handler of this event id");
                return 0;
            }

            try
            {
                EventMap[iEventId](sEvent);
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "####################processed Event exception.#######################" + guid + "########eventId:" +
                    iEventId);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
                return 0;
            }

            GlobalDefinition.LoggerWrapper.LogDevInfo("####################processed Event.#######################" +
                                                      guid + "########eventId:" + iEventId);

            return 0;
        }

        public delegate void EventHandler(string param);

        private static Dictionary<int, EventHandler> eventMap = new Dictionary<int, EventHandler>();

        public static Dictionary<int, EventHandler> EventMap
        {
            get { return eventMap; }
            set { eventMap = value; }
        }

        /// <summary>
        /// AddEventHandler
        /// </summary>
        /// <param name="key">int</param>
        /// <param name="handler">EventHandler</param>
        public static void AddEventHandler(int key, EventHandler handler)
        {
            if (null == EventMap)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("EventMap is null");
                return;
            }

            if (!EventMap.ContainsKey(key))
            {
                EventMap.Add(key, handler);
            }
        }
    }
}
