using System;

namespace UIH.XA.PAUtilityCSharp.Global
{
    public class ThreadHelper
    {
        /// <summary>
        /// OperateMultiThreadSafety
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns>bool</returns>
        public static bool OperateMultiThreadSafety(Action action)
        {
            bool isSuccess = false;
            if (null != GlobalDefinition.MainWnd && null != GlobalDefinition.MainWnd.Dispatcher)
            {
                GlobalDefinition.MainWnd.Dispatcher.Invoke(new Action(delegate
                {
                    try
                    {
                        action.Invoke();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        GlobalDefinition.LoggerWrapper.LogException(ex);
                    }
                }));
            }

            return isSuccess;
        }

        /// <summary>
        /// OperateMultiThreadSafetyAsync
        /// </summary>
        /// <param name="action">Action</param>
        /// <returns>bool</returns>
        public static bool OperateMultiThreadSafetyAsync(Action action)
        {
            bool isSuccess = false;
            if (null != GlobalDefinition.MainWnd && null != GlobalDefinition.MainWnd.Dispatcher)
            {
                GlobalDefinition.MainWnd.Dispatcher.BeginInvoke(new Action(delegate
                {
                    try
                    {
                        action.Invoke();
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        GlobalDefinition.LoggerWrapper.LogException(ex);
                    }
                }));
            }

            return isSuccess;
        }
    }
}
