using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate
{
    public sealed class ConfigManager : AbStractConfigManager
    {
        private ConfigManager()
        {
        }

        private static ConfigManager _cfgManager = new ConfigManager();

        public static T Load<T>(PaConfigType type)
        {
            return _cfgManager.GetConfig<T>(type);
        }

        public static void Save<T>(
            PaConfigType type,
            T obj)
        {
            _cfgManager.SaveConfig(type, obj);
        }

        public static void SetConfigureToDefault(PaConfigType type)
        {
            _cfgManager.SetToDefault(type);
        }

        protected override void LogError(
            string content,
            params object[] param)
        {
            GlobalDefinition.LoggerWrapper.LogDevError(string.Format(content, param));
        }

        protected override T GetConfig<T>(string relativePath)
        {
            return FileParserWrapper.GetUserSetting<T>(relativePath);
        }

        protected override void SaveConfig<T>(
            T config,
            string relativePath)
        {
            FileParserWrapper.SaveUserSetting(relativePath, config);
        }

        protected override void SetToDefault(
            string absolutePathFactory,
            string absolutePath)
        {
            var factorycfgcontent = File.ReadAllText(absolutePathFactory);
            if (!File.Exists(absolutePath))
            {
                using (var fs = File.Create(absolutePath))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.Write(factorycfgcontent);
                    }
                }
            }
            else
            {
                File.WriteAllText(absolutePath, factorycfgcontent);
            }
        }
    }
}
