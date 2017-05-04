using System;
using System.ComponentModel.Composition;

namespace UIH.XA.ViewerToolKit.Config
{
    public class ViewerToolBoxConfig
    {
        [Export("ViewerToolBoxConfig", typeof(string))]
        private readonly string _configPath;

        public ViewerToolBoxConfig()
        {
            //TODO: ViewerToolBoxConfig Get ConfigPath from clr_system_config
        }
    }
}