using uDicom.Common;

namespace UIH.Dicom.Log
{
    public static class LogAdapter
    {
        public static ILog Logger
        {
            get { return LogManager.GetLog("UIH.Dicom"); }
        }
    }
}
