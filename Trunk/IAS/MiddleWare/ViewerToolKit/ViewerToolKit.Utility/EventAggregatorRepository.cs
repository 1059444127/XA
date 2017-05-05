using Microsoft.Practices.Prism.Events;

namespace UIH.XA.ViewerToolKit.Utility
{
    public class EventAggregatorRepository
    {
        public EventAggregatorRepository()
        {
            _eventAggregator = new EventAggregator();
        }

        private IEventAggregator _eventAggregator;
        public IEventAggregator EventAggregator
        {
            get { return _eventAggregator; }
        }

        private static EventAggregatorRepository _aggregatorRepository = null;

        public static EventAggregatorRepository Instance()
        {
            if (null == _aggregatorRepository)
            {
                _aggregatorRepository = new EventAggregatorRepository();
            }
            return _aggregatorRepository;
        }
    }
}
