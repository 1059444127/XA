using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.ViewerToolKit.Interface
{
    public interface ICinePlayerModel
    {
        event EventHandler<CinePlayerEventArgs<int>> FirstDisplayCellChangedEvent;
        event EventHandler<CinePlayerEventArgs<string>> LayoutChangedEvent;
        event EventHandler<CinePlayerEventArgs<int>> ImageCountChangedEvent;

        void Play(int speed);
        void Stop(int speed);
        void ValueChanged(int currentIndex);
        void SpeedChanged(int speed);
    }

    public class CinePlayerEventArgs<T> : EventArgs
    {
        public CinePlayerEventArgs(T target)
        {
            Target = target;
        }

        public T Target { get; private set; }
    }
}
