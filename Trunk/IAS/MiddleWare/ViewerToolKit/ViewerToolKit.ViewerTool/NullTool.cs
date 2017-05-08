using System;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public class NullTool : IViewerTool
    {
        #region Implementation of IViewerTool

        public string Name
        {
            get { return string.Empty; }
        }

        public bool CanAct
        {
            get { return false; }
        }

        public void Act()
        {
        }

        public event EventHandler CanActChanged;

        #endregion
    }
}