using Microsoft.Practices.Prism.Events;

namespace UIH.XA.ViewerToolKit.Utility
{
    public class CinePlayerPlayEvent : CompositePresentationEvent<int> { }

    public class CinePlayerStopEvent : CompositePresentationEvent<int> { }

    public class CinePlayerValueChangedEvent : CompositePresentationEvent<int> { }

    public class CinePlayerSpeedChangedEvent : CompositePresentationEvent<int> { }


    public class ProxyFistDisplayCellChangedEvent : CompositePresentationEvent<int> { }

    public class ProxyLayoutChangedEvent : CompositePresentationEvent<string> { }

    public class ProxyImageCountChangedEvent : CompositePresentationEvent<int> { }
}
