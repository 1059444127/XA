
namespace UIH.XR.Core
{
    public interface IAppReady
    {
        /// <summary>
        /// app needs to realize the function when app is ready,and regesite it for RMI
        /// </summary>
        /// <param name="receiver">app's proxyName</param>
        /// <returns>true: app is ready
        ///          false： app is not ready
        /// </returns>
        bool AppReady(string receiver);
    }
}
