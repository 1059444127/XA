using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIH.XA.ViewerToolKit.Interface
{
    public interface IViewerProxy
    {
        void SetImageCount(int imageCount);

        void SetLayout(int rows, int columns);
        void SetEmpty();
        void SetPointer();
        void SetPan();
        void SetZoom();
        void SetWindowing();
        void SetAnnotationLine();
        void SetAnnotationArrow();
        void SetAnnotationAngle();
        void SetAnnotationText();
        void SetRegionCircle();
        void SetRegionRectangle();
        void SetRegionFreehand();
        void SetDrawLLable();
        void SetDrawRLable();
        void SetZoomBox();
        void SetMagnifier();
        void SetPixelLens();
        void SetImageProfile();
        void SetEllipseHistogram();
        void SetFreehandHistogram();
        void SetFlipHorizontal();
        void SetFlipVertical();
        void SetRotateClockwise();
        void SetRotateAntiClockwise();
        void SetInverse();

        void Reset();
        void Reload();

    }
}
