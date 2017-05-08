using System;
using System.Threading;
using System.Windows;
using UIH.Mcsf.Core;
using UIH.Mcsf.NLS;
using UIH.XA.PAUtilityCSharp.Commons;

namespace UIH.XA.PAUtilityCSharp.Global
{
    public class GlobalDefinition
    {

        #region Const Variables

        public static readonly string PAFE = "PA@FE@";
        public static readonly string PAModulePath = "PatientAdminPath";
        public static readonly string PAModuleConfigurationPath = "PatientAdminConfigPath";

        public static readonly string PRConfigureFileName = "PR_Configure.xml";
        public static readonly string DataGridConfigureFileName = "PA_DataGrid_Configure.xml";
        public static readonly int PAFEChannelID = 11;
        public static readonly int ServicePageChangedID = 10;
       
        #endregion

        #region Properties

        /// <summary>
        /// The path  is located to PR assembly. 
        /// </summary>
        public static string AppPath { get; set; }

        public static string AppDataPath { get; set; }


        /// <summary>
        /// The path is located to PR config files.
        /// </summary>
        public static string ConfigPath { get; set; }

        

        /// <summary>
        /// Global App entry.
        /// </summary>
        public static object Main { get; set; }

        /// <summary>
        /// Global Main window instance.
        /// </summary>
        public static FrameworkElement MainWnd { get; set; }

        /// <summary>
        /// Global main data context instance.
        /// </summary>
        public static object MainDataContext { get; set; }

        /// <summary>
        /// Global main UI thread.
        /// </summary>
        public static Thread MainThread { get; set; }

        /// <summary>
        /// Global logger instance.
        /// </summary>
        public static Logger LoggerWrapper { get; set; }

        #endregion

        public static ResourceDictionary ResDic { get; private set; }

        public static int MaxWeightKg = 205;

        public static int MaxWeightLb = 450;

        public static int MaxHeightCm = 305;

        public static int MaxHeightInch = 120;

        public static int CtMaxHeightCm = 240;

        public static int CtMaxHeightInch = 94;

        private static void InitNls()
        {
            ResourceMgr nls = ResourceMgr.Instance();
            ResDic = nls.Init("PA");
        }

        /// <summary>
        /// ToLocalLanguge
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>string</returns>
        public static string ToLocalLanguge(string key)
        {
            if (null == MainWnd || null == MainWnd.Resources)
            {
                return String.Empty;
            }
            return (string)MainWnd.Resources[key] ?? String.Empty;
        }

        /// <summary>
        /// ToLocalLanguageForDose
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>string</returns>

        static GlobalDefinition()
        {
            try
            {
                LoggerWrapper = new Logger();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Initialize Logger failed!\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                LoggerWrapper.LogDevError(msg);
            }

            try
            {
                InitNls();
            }
            catch (Exception ex)
            {
                LoggerWrapper.LogException(ex);
                ResDic = new ResourceDictionary();
                LoggerWrapper.LogDevError("Initialize nls resource failed!");
            }

            try
            {
                AppPath = mcsf_clr_systemenvironment_config.GetApplicationPath(PAModulePath);
                ConfigPath = mcsf_clr_systemenvironment_config.GetApplicationPath(PAModuleConfigurationPath);
                AppDataPath = mcsf_clr_systemenvironment_config.GetApplicationPath();
            }
            catch (Exception ex)
            {
                LoggerWrapper.LogException(ex);
            }
        }
    }
}
