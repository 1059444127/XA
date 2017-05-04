using System.Diagnostics;
using UIH.Mcsf.Core;

namespace ViewerToolKit.ContaineeStub
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