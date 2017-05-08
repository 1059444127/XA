using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewerTool
{
    public class Layout : ViewerTool
    {
        #region Overrides of ViewerTool

        public override string Name
        {
            get { return string.Format("{0}{1}X{2}", _name, Row, Col); }
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

        public int Row { get; private set; }
        public int Col { get; private set; }
    }
}