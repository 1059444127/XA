using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate
{
    public sealed class ConfigPathHelper
    {
        private static string AbsoluteUserSettingPath(string relativePath)
        {
            return Path.Combine(mcsf_clr_systemenvironment_config.GetDefaultUserSettingsDir(), relativePath);
        }

        private static string AbsoluteFactoryUserSettingPath(string relativePath)
        {
            return Path.Combine(mcsf_clr_systemenvironment_config.GetFactoryDefaultSettingsDir(), relativePath);
        }

        public static string AbsoluteUserSettingPath(PaConfigType cfgType)
        {
            return AbsoluteUserSettingPath(RelativeUserSettingPath(cfgType));
        }

        public static string AbsoluteFactoryUserSettingPath(PaConfigType cfgType)
        {
            return AbsoluteFactoryUserSettingPath(RelativeUserSettingPath(cfgType));
        }

        public static string RelativeUserSettingPath(PaConfigType cfgType)
        {
            return ConfigurePath.ConfigPathDic[cfgType];
        }
    }
}
