using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public class Layout : ViewerTool
    {
        #region Overrides of ViewerTool

        public override string Name
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool CanAct
        {
            get { throw new System.NotImplementedException(); }
        }

        protected override void ActOn(IViewDisplay viewDisplay)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private int _row;
        private int _col;
    }
}