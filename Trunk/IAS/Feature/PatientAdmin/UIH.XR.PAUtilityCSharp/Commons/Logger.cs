using System;
using System.Linq;
using UIH.Mcsf.Log;

namespace UIH.XA.PAUtilityCSharp.Commons
{
    public class Logger
    {
        public Logger()
        {
            this.clrLogger = CLRLogger.GetInstance();
        }

        private readonly CLRLogger clrLogger;


        /// <summary>
        /// LogTraceInfo
        /// </summary>
        /// <param name="msg">string</param>
        public void LogTraceInfo(string msg)
        {
            this.clrLogger.LogTraceInfo(msg);
        }

        /// <summary>
        /// LogSvcInfo
        /// </summary>
        /// <param name="msg">string</param>
        public void LogSvcInfo(string msg)
        {
            this.clrLogger.LogSvcInfo(msg);
        }

        /// <summary>
        /// LogSvcInfo
        /// </summary>
        /// <param name="msgs">paramsstring[]</param>
        public void LogSvcInfo(params string[] msgs)
        {
            string msg = msgs.Aggregate("[loggwrapper - params]", (current, m) => current + m);
            this.clrLogger.LogSvcInfo(msg);
        }


        /// <summary>
        /// LogDevInfo
        /// </summary>
        /// <param name="msg">string</param>
        public void LogDevInfo(string msg)
        {
            this.clrLogger.LogDevInfo(msg);
        }

        /// <summary>
        /// LogDevInfo
        /// </summary>
        /// <param name="msgs">paramsstring[]</param>
        public void LogDevInfo(params string[] msgs)
        {
            string msg = msgs.Aggregate("[loggwrapper - params]", (current, m) => current + m);
            this.clrLogger.LogDevInfo(msg);
        }

        /// <summary>
        /// LogDevWarning
        /// </summary>
        /// <param name="msg">string</param>
        public void LogDevWarning(string msg)
        {
            this.clrLogger.LogDevWarning(msg);
        }

        /// <summary>
        /// LogDevWarning
        /// </summary>
        /// <param name="msgs">paramsstring[]</param>
        public void LogDevWarning(params string[] msgs)
        {
            string msg = msgs.Aggregate("[loggwrapper - params]", (current, m) => current + m);
            this.clrLogger.LogDevWarning(msg);
        }

        /// <summary>
        /// LogDevError
        /// </summary>
        /// <param name="msg">string</param>
        public void LogDevError(string msg)
        {
            this.clrLogger.LogDevError(msg);
        }


        /// <summary>
        /// LogDevError
        /// </summary>
        /// <param name="msgs">paramsstring[]</param>
        public void LogDevError(params string[] msgs)
        {
            string msg = msgs.Aggregate("[loggwrapper - params]", (current, m) => current + m);
            this.clrLogger.LogDevError(msg);
        }

        /// <summary>
        /// LogException
        /// </summary>
        /// <param name="ex">Exception</param>
        public void LogException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            this.LogDevError(ex.Message);
            this.LogDevError(ex.StackTrace);

            if (ex.InnerException != null)
            {
                LogException(ex.InnerException);
            }
        }

        /// <summary>
        /// LogException
        /// </summary>
        /// <param name="preComment">string</param>
        /// <param name="ex">Exception</param>
        public void LogException(string preComment, Exception ex)
        {
            this.LogDevError(preComment);
            this.LogException(ex);
        }
    }
}
