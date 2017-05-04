using System;
using System.Windows.Input;
using UIH.XA.Common.MVVM;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    public class SingleToolViewModel : ToolViewModel
    {
        private readonly IViewerTool _tool;

        public SingleToolViewModel(IViewerTool tool)
        {
            _tool = tool;
            _tool.CanActChanged += ToolOnCanActChanged;
        }

        ~SingleToolViewModel()
        {
            _tool.CanActChanged -= ToolOnCanActChanged;
        }

        private void ToolOnCanActChanged(object sender, EventArgs eventArgs)
        {
            _toolCommand.RaiseCanExecuteChanged();
        }

        #region [--ToolCommand--]

        private RelayCommand _toolCommand;

        public ICommand ToolCommand
        {
            get { return _toolCommand = _toolCommand ?? new RelayCommand(OnToolPicked, CanToolExecute); }
        }

        private bool CanToolExecute()
        {
            return _tool.CanAct;
        }

        private void OnToolPicked()
        {
            _tool.Act();
            ;
        }

        #endregion [--ToolCommand--]

        #region Overrides of ToolViewModel

        public override string Name
        {
            get { return _tool.Name; }
        }

        #endregion
    }
}