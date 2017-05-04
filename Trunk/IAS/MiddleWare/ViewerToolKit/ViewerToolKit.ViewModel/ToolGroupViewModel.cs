using System.Collections.ObjectModel;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    public class ToolGroupViewModel : ToolViewModel
    {
        private readonly string _name;
        private readonly ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>(); 

        public ToolGroupViewModel(string name)
        {
            _name = name;
        }

        public ObservableCollection<ToolViewModel> Tools { get {return _tools;} }

        #region Overrides of ToolViewModel

        public override string Name
        {
            get { return _name;}
        }

        #endregion


    }
}