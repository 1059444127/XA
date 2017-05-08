using System.Diagnostics;
using UIH.Mcsf.Core;

namespace UIH.XA.ViewerToolKit.ContaineeStub
{
    public class ContaineeStub : CLRContaineeBase
    {
        #region Overrides of CLRContaineeBase

        public override void DoWork()
        {
            Debug.WriteLine("ContaineeStub.DoWork");
        }

        #endregion
    }
}