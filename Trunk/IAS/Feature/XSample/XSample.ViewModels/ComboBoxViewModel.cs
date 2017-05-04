using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace UIH.XA.XSample.ViewModels
{
    [Export("ComboBoxViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ComboBoxViewModel : NotificationObject
    {
        #region [---Constructors---]

        public ComboBoxViewModel()
        {
            this._items = new ObservableCollection<string> {"1X1", "2X2", "3X3", "4X4", "5X5"};
        }

        #endregion



        #region [---Properties---]

        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get { return this._items; }
        }

        #endregion

    }
}
