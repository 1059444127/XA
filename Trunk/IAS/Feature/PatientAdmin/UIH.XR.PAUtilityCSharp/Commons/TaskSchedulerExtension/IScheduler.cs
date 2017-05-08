using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIH.XA.PAUtilityCSharp.Commons.TaskSchedulerExtension
{
    public interface IScheduler : IEnumerable<Task>
    {
        void Add(Task t);
        void Remove(Task t);

        int Count { get; }
        Task this[int index] { get; set; }
    }

    public class SchedulerEnumerator : IEnumerator<Task>
    {
        private IScheduler _collection;
        private int _currentIndex;
        private Task _currentTask;


        public SchedulerEnumerator(IScheduler collection)
        {
            _collection = collection;
            _currentIndex = -1;
            _currentTask = default(Task);
        }

        /// <summary>
        /// MoveNext
        /// </summary>
        /// <returns>bool</returns>
        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++_currentIndex >= _collection.Count)
            {
                return false;
            }
            else
            {
                // Set current box to next item in collection.
                _currentTask = _collection[_currentIndex];
            }
            return true;
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }

        void IDisposable.Dispose()
        {
        }

        public Task Current
        {
            get { return _currentTask; }
        }


        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
