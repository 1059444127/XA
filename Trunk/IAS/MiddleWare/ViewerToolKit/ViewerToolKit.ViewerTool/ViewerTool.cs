using System;
using System.Collections.Generic;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public abstract class ViewerTool : IViewerTool
    {
        #region Implementation of IViewerTool

        public virtual string Name { get { return _name; } set { _name = value; } }

        public void Act()
        {
            foreach (var viewDisplay in ViewDisplays)
            {
                ActOn(viewDisplay);
            }
        }
        public event EventHandler CanActChanged = delegate { };
        public abstract bool CanAct { get; }

        #endregion

        #region [--Protected Template--]

        protected string _name;

        public IList<IViewDisplay> ViewDisplays { protected get; set; }

        protected void RaiseCanActChangedEvent()
        {
            CanActChanged(this, new EventArgs());
        }

        protected abstract void ActOn(IViewDisplay viewDisplay);

        #endregion
    }
}