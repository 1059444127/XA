using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using Microsoft.Practices.Prism.Events;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.Utility;

namespace UIH.XA.ViewerToolKit.Model
{
    [Export(typeof(ICinePlayerModel))]
    public class CinePlayerModel : ICinePlayerModel
    {
        private IEventAggregator EventAggregator
        {
            get { return EventAggregatorRepository.Instance().EventAggregator; }
        }

        public event EventHandler<CinePlayerEventArgs<int>> FirstDisplayCellChangedEvent;
        public event EventHandler<CinePlayerEventArgs<string>> LayoutChangedEvent;
        public event EventHandler<CinePlayerEventArgs<int>> ImageCountChangedEvent;

        public CinePlayerModel()
        {
            this.SubscribeProxyEvent();
        }

        private void SubscribeProxyEvent()
        {
            this.EventAggregator.GetEvent<ProxyFistDisplayCellChangedEvent>().Subscribe(ProxyFirstDisplayCellChangedEventHandler);
            this.EventAggregator.GetEvent<ProxyLayoutChangedEvent>().Subscribe(ProxyLayoutChangedEventHandler);
            this.EventAggregator.GetEvent<ProxyImageCountChangedEvent>().Subscribe(ProxyImageCountChangedEventHandler);
        }

        public void Play(int speed)
        {
            EventAggregator.GetEvent<CinePlayerPlayEvent>().Publish(speed);
        }

        public void Stop(int speed)
        {
            EventAggregator.GetEvent<CinePlayerStopEvent>().Publish(speed);
        }

        public void ValueChanged(int currentIndex)
        {
            EventAggregator.GetEvent<CinePlayerValueChangedEvent>().Publish(currentIndex);
        }

        public void SpeedChanged(int newSpeed)
        {
            EventAggregator.GetEvent<CinePlayerSpeedChangedEvent>().Publish(newSpeed);
        }

        private void ProxyFirstDisplayCellChangedEventHandler(int currentIndex)
        {
            this.RaiseFirstDisplayCellChangedEvent(currentIndex);
        }

        private void ProxyLayoutChangedEventHandler(string newLayout)
        {
            this.RaiseLayoutChangedEvent(newLayout);
        }

        private void ProxyImageCountChangedEventHandler(int cellCount)
        {
            this.RaiseCellCountChanedEventHandler(cellCount);
        }

        private void RaiseFirstDisplayCellChangedEvent(int target)
        {
            var handler = this.FirstDisplayCellChangedEvent;
            if (null != handler)
            {
                var args = new CinePlayerEventArgs<int>(target);
                handler(this, args);
            }
        }

        private void RaiseLayoutChangedEvent(string newLayout)
        {
            var handler = this.LayoutChangedEvent;
            if (null != handler)
            {
                var args = new CinePlayerEventArgs<string>(newLayout);
                handler(this, args);
            }
        }

        private void RaiseCellCountChanedEventHandler(int cellCount)
        {
            var handler = this.ImageCountChangedEvent;
            if (null != handler)
            {
                var args = new CinePlayerEventArgs<int>(cellCount);
                handler(this, args);
            }
        }
    }
}
