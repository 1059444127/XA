using System;

namespace UIH.XA.ViewerToolKit.Interface
{
    public interface IViewerTool
    {
        string Name { get; }
        bool CanAct { get; }
        void Act();
        event EventHandler CanActChanged;
    }
}