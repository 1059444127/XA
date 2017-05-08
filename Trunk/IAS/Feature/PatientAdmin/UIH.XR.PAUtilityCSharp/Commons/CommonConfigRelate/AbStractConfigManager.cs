using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate
{
    public abstract class AbStractConfigManager
    {
        protected virtual void LogError(
            string content,
            params object[] param)
        {
        }

        protected abstract T GetConfig<T>(string relativePath);

        public T GetConfig<T>(PaConfigType cfgType)
        {
            return GetConfig<T>(ConfigPathHelper.RelativeUserSettingPath(cfgType));
        }

        protected abstract void SaveConfig<T>(
            T config,
            string relativePath);

        protected void SaveConfig<T>(
            PaConfigType cfgType,
            T config)
        {
            SaveConfig(config, ConfigPathHelper.RelativeUserSettingPath(cfgType));
        }

        protected abstract void SetToDefault(
            string absolutePathFactory,
            string absolutePath);

        public void SetToDefault(PaConfigType cfgType)
        {
            SetToDefault(
                         ConfigPathHelper.AbsoluteFactoryUserSettingPath(cfgType),
                         ConfigPathHelper.AbsoluteUserSettingPath(cfgType));
        }
    }
}
