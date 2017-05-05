using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using UIH.XA.Common.MVVM;
using UIH.XA.ViewerToolKit.Interface;

namespace UIH.XA.ViewerToolKit.ViewModel
{
    [Export("CinePlayerViewModel")]
    public class CinePlayerViewModel : NotificationObject
    {
        [ImportingConstructor]
        public CinePlayerViewModel(ICinePlayerModel model)
        {
            _model = model;

            this.InitSpeedCollection();
            this.RegisterModelEvents();
        }

        private ICinePlayerModel _model;
        private bool _isSliderDragOrClick = true;

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible == value)
                    return;

                _isVisible = value;
                RaisePropertyChanged(() => this.IsVisible);
            }
        }

        private int _imageCount = 10;
        public int ImageCount
        {
            get { return _imageCount; }
            set
            {
                if (_imageCount == value)
                    return;

                _imageCount = value;
                RaisePropertyChanged(() => this.ImageCount);
            }
        }

        private int _currentIndex = 1;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                if (_currentIndex == value)
                    return;

                _currentIndex = value;
                RaisePropertyChanged(() => this.CurrentIndex);

                if (_isSliderDragOrClick)
                {
                    _model.ValueChanged(_currentIndex);
                }
            }
        }

        private bool _isPlaying = false;
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying == value)
                    return;

                _isPlaying = value;
                RaisePropertyChanged(() => this.IsPlaying);

                SpeedType speedType;
                Enum.TryParse(_speed, out speedType);
                if (_isPlaying)
                {
                    _model.Play((int)speedType);
                }
                else
                {
                    _model.Stop((int)speedType);
                }
            }
        }

        private String _speed;
        public String Speed
        {
            get { return _speed; }
            set
            {
                if (_speed == value)
                    return;

                _speed = value;
                RaisePropertyChanged(() => this.Speed);

                SpeedType speedType;
                Enum.TryParse(_speed, out speedType);
                _model.SpeedChanged((int)speedType);
            }
        }

        private ObservableCollection<String> _speedCollection;
        public ObservableCollection<String> SpeedCollection
        {
            get { return _speedCollection; }
            set
            {
                _speedCollection = value;
                RaisePropertyChanged(() => this.SpeedCollection);
            }
        }

        private void InitSpeedCollection()
        {
            SpeedCollection = new ObservableCollection<String>();
            foreach (var type in Enum.GetValues(typeof(SpeedType)))
            {
                SpeedCollection.Add((string)type);
            }

            Speed = SpeedType.Slow.ToString();
        }

        private void RegisterModelEvents()
        {
            this._model.ImageCountChangedEvent += ModelCellCountChangedEventHandler;
            this._model.FirstDisplayCellChangedEvent += ModelFirstDisplayCellChangedEventHandler;
            this._model.LayoutChangedEvent += ModelLayoutChangedEvent;
        }

        private void ModelCellCountChangedEventHandler(object sender, CinePlayerEventArgs<int> args)
        {
            this.ImageCount = args.Target;
        }

        private void ModelFirstDisplayCellChangedEventHandler(object sender, CinePlayerEventArgs<int> args)
        {
            this.SetCurrentIndex(args.Target);
        }

        private void ModelLayoutChangedEvent(object sender, CinePlayerEventArgs<string> args)
        {
            this.IsVisible = args.Target == "1:1";
        }

        private void SetCurrentIndex(int currentIndex)
        {
            _isSliderDragOrClick = false;
            this.CurrentIndex = currentIndex;
            _isSliderDragOrClick = true;
        }
    }

    public enum SpeedType
    {
        Slow = 5,
        Medium = 12,
        Fast = 24
    }
}
