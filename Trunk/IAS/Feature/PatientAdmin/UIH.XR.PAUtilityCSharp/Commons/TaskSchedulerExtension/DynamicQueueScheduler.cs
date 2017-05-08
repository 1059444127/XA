using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons.TaskSchedulerExtension
{
    public class DynamicQueueScheduler : IScheduler
    {
        protected List<Task> _queue;

        public DynamicQueueScheduler()
        {
            _queue = new List<Task>();
        }

        public virtual void Add(Task t)
        {
            Task oldTask = null;
            if (Contains(t, out oldTask))
            {
                _queue.Remove(oldTask);
            }

            _queue.Add(t);
        }

        public virtual void Remove(Task t)
        {
            _queue.Remove(t);
        }

        public virtual bool Contains(Task t)
        {
            Task oldTask = null;
            bool found = Contains(t, out oldTask);
            return found;
        }

        public virtual bool Contains(
            Task t,
            out Task oldTask)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public virtual void Contains() start");
            bool found = false;
            oldTask = null;

            foreach (var task in _queue)
            {
                if (t.AsyncState != null
                    && t.AsyncState.Equals(task.AsyncState))
                {
                    oldTask = task;
                    found = true;

                    break;
                }
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public virtual void Contains() end");
            return found;
        }

        public virtual bool Contains(
            Task t,
            EqualityComparer<Task> comp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>IEnumerator<Task></returns>
        public IEnumerator<Task> GetEnumerator()
        {
            return new SchedulerEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SchedulerEnumerator(this);
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }

        public Task this[int index]
        {
            get
            {
                return (Task) _queue[index];
            }
            set
            {
                _queue[index] = value;
            }
        }
    }
}
