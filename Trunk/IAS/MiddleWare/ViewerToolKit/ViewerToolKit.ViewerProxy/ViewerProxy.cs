using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Events;
using UIH.Mcsf.Viewer;
using UIH.XA.ViewerToolKit.Interface;
using UIH.XA.ViewerToolKit.Utility;

namespace UIH.XA.ViewerToolKit.ViewerProxy
{
    public class ViewerProxy : IViewerProxy
    {
        private MedViewerControl _viewerControl;
        private string _configPath;

        private Thread _moviePlayingThread;
        private Stopwatch _stopwatch;
        private bool _isPlaying = false;
        private int _speed = 5;
        private int _imageCount;

        public IEventAggregator EventAggregator
        {
            get { return EventAggregatorRepository.Instance().EventAggregator; }
        }

        public void Initialize(MedViewerControl viewerControl, string configPath)
        {
            _viewerControl = viewerControl;
            _configPath = configPath;

            _viewerControl.InitializeWithoutCommProxy(_configPath);
            _viewerControl.LayoutManager.SetLayout(1, 1);

            this.RegisterViewerControlEvents();
            this.SubscribeCinePlayerEvents();

            _stopwatch = new Stopwatch();
        }

        private void RegisterViewerControlEvents()
        {
            _viewerControl.LayoutManager.RootCell.FirstDisplayedCellIndexChanged +=
                ViewerControlFirstDisplayedCellChangedHandler;
        }

        private void SubscribeCinePlayerEvents()
        {
            this.EventAggregator.GetEvent<CinePlayerPlayEvent>().Subscribe(CinePlayerPlayEventHandler);
            this.EventAggregator.GetEvent<CinePlayerStopEvent>().Subscribe(CinePlayerStopEventHandler);
            this.EventAggregator.GetEvent<CinePlayerValueChangedEvent>().Subscribe(CinePlayerValueChangedEventHandler);
            this.EventAggregator.GetEvent<CinePlayerSpeedChangedEvent>().Subscribe(CinePlayerSpeedChangedEventHandler);
        }

        private void ViewerControlFirstDisplayedCellChangedHandler(object sender, MedViewerEventArgs args)
        {
            int index = _viewerControl.LayoutManager.RootCell.FirstDisplayedCellIndex;
            this.EventAggregator.GetEvent<ProxyFistDisplayCellChangedEvent>().Publish(index);
        }

        private void CinePlayerPlayEventHandler(int speed)
        {
            _speed = speed;
            _isPlaying = true;
            _moviePlayingThread = new Thread(new ThreadStart(PlayMovie));
            _moviePlayingThread.Start();
        }

        private void PlayMovie()
        {
            while (_isPlaying)
            {
                _stopwatch.Restart();

                int oldIndex = _viewerControl.LayoutManager.RootCell.FirstDisplayedCellIndex;
                int newIndex = oldIndex + 1 < _imageCount ? oldIndex + 1 : 0;
                _viewerControl.LayoutManager.RootCell.FirstDisplayedCellIndex = newIndex;
                _viewerControl.LayoutManager.RootCell.Refresh();

                _stopwatch.Stop();

                int frameInterval = 1000 / _speed;
                int sleepTime = (int)(frameInterval - _stopwatch.ElapsedMilliseconds);
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                else
                {
                    throw new Exception("ViewerDisplayProxy PlayMovie() Exception: the frame rate" + _speed.ToString() + "fp/s is too high.");
                }
            }
        }

        private void CinePlayerStopEventHandler(int speed)
        {
            _isPlaying = false;
        }

        private void CinePlayerValueChangedEventHandler(int newIndex)
        {
            int oldIndex = _viewerControl.LayoutManager.RootCell.FirstDisplayedCellIndex;
            int dragDis = newIndex - oldIndex;
            int absDragDis = Math.Abs(dragDis);

            if (dragDis > 0)
            {
                _viewerControl.LayoutManager.RootCell.PageDown(absDragDis);
            }
            else if (dragDis < 0)
            {
                _viewerControl.LayoutManager.RootCell.PageUp(absDragDis);
            }
        }

        private void CinePlayerSpeedChangedEventHandler(int newSpeed)
        {
            _speed = newSpeed;
        }

        public void SetImageCount(int imageCount)
        {
            _imageCount = imageCount;

            this.EventAggregator.GetEvent<ProxyImageCountChangedEvent>().Publish(imageCount);
        }

        public void SetLayout(int rows, int columns)
        {
            _viewerControl.LayoutManager.SetLayout(rows, columns);

            string layout = rows.ToString() + ":" + columns.ToString();
            this.EventAggregator.GetEvent<ProxyLayoutChangedEvent>().Publish(layout);
        }

        private void SetAction(ActionType actionType)
        {
            var cells = new List<IViewerControlCell>();
            cells.AddRange(_viewerControl.Cells.Where(cell => !cell.IsEmpty));
            _viewerControl.SetAction(actionType, cells);
        }

        public void SetEmpty()
        {
            this.SetAction(ActionType.Empty);
        }

        public void SetPointer()
        {
            this.SetAction(ActionType.Pointer);
        }

        public void SetPan()
        {
            this.SetAction(ActionType.Pan);
        }

        public void SetZoom()
        {
            this.SetAction(ActionType.Zoom);
        }

        public void SetWindowing()
        {
            this.SetAction(ActionType.Windowing);
        }

        public void SetAnnotationLine()
        {
            this.SetAction(ActionType.AnnotationLine);
        }

        public void SetAnnotationArrow()
        {
            this.SetAction(ActionType.AnnotationArrow);
        }

        public void SetAnnotationAngle()
        {
            this.SetAction(ActionType.AnnotationAngle);
        }

        public void SetAnnotationText()
        {
            this.SetAction(ActionType.AnnotationText);
        }

        public void SetRegionCircle()
        {
            this.SetAction(ActionType.RegionCircle);
        }

        public void SetRegionRectangle()
        {
            this.SetAction(ActionType.RegionRectangle);
        }

        public void SetRegionFreehand()
        {
            this.SetAction(ActionType.RegionFreehand);
        }

        public void SetDrawLLable()
        {
            this.SetAction(ActionType.DrawLLable);
        }

        public void SetDrawRLable()
        {
            this.SetAction(ActionType.DrawRLable);
        }

        public void SetZoomBox()
        {
            this.SetAction(ActionType.ZoomBox);
        }

        public void SetMagnifier()
        {
            this.SetAction(ActionType.Magnifier);
        }

        public void SetImageProfile()
        {
            this.SetAction(ActionType.ImageProfile);
        }

        public void SetPixelLens()
        {
            this.SetAction(ActionType.PixelLens);
        }

        public void SetEllipseHistogram()
        {
            this.SetAction(ActionType.EllipseHistogram);
        }

        public void SetFreehandHistogram()
        {
            this.SetAction(ActionType.FreehandHistogram);
        }

        public void SetFlipHorizontal()
        {
            var cellList = new List<MedViewerControlCell>();
            cellList.AddRange(_viewerControl.SelectedCells.Where(cell => !cell.IsEmpty));
            foreach (var cell in cellList)
            {
                var cmd = new CmdFlipHorizontal();
                cell.ExecuteCommand(cmd);
            }
        }

        public void SetFlipVertical()
        {
            var cellList = new List<MedViewerControlCell>();
            cellList.AddRange(_viewerControl.SelectedCells.Where(cell => !cell.IsEmpty));
            foreach (var cell in cellList)
            {
                var cmd = new CmdFlipVertical();
                cell.ExecuteCommand(cmd);
            }
        }

        public void SetRotateClockwise()
        {
            var cellList = new List<MedViewerControlCell>();
            cellList.AddRange(_viewerControl.SelectedCells.Where(cell => !cell.IsEmpty));
            foreach (var cell in cellList)
            {
                var cmd = new CmdRotateClockwise();
                cell.ExecuteCommand(cmd);
            }
        }

        public void SetRotateAntiClockwise()
        {
            var cellList = new List<MedViewerControlCell>();
            cellList.AddRange(_viewerControl.SelectedCells.Where(cell => !cell.IsEmpty));
            foreach (var cell in cellList)
            {
                var cmd = new CmdRotateCounterClockwise();
                cell.ExecuteCommand(cmd);
            }
        }

        public void SetInverse()
        {
            var cellList = new List<MedViewerControlCell>();
            cellList.AddRange(_viewerControl.SelectedCells.Where(cell => !cell.IsEmpty));
            foreach (var cell in cellList)
            {
                foreach (var displayData in cell.Image.Pages)
                {
                    displayData.PState.IsPaletteReversed = !displayData.PState.IsPaletteReversed;
                }
                cell.Refresh();
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

    }
}
