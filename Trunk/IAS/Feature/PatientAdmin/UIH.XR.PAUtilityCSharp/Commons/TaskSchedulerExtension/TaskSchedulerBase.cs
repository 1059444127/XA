using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons.TaskSchedulerExtension
{
    public class TaskSchedulerBase<T> : TaskScheduler
        where T : IScheduler, new()
    {
        private readonly Thread _processThread;
        private readonly object _lock = new object();

        public TaskSchedulerBase()
        {
            _processThread = new Thread(this.Process);

            _processThread.Start();

            UnobservedTaskException +=
                new EventHandler<UnobservedTaskExceptionEventArgs>(TaskSchedulerBase_UnobservedTaskException);
        }

        private void TaskSchedulerBase_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            GlobalDefinition.LoggerWrapper.LogDevError("Task executed failed.");
            if (null != e.Exception)
            {
                GlobalDefinition.LoggerWrapper.LogException("TaskSchedulerBase_UnobservedTaskException", e.Exception);
                if (null != e.Exception.InnerExceptions)
                {
                    foreach (var exception in e.Exception.InnerExceptions)
                    {
                        GlobalDefinition.LoggerWrapper.LogException("TaskSchedulerBase_UnobservedTaskException",
                            exception);
                    }
                }
            }

            GlobalDefinition.LoggerWrapper.LogDevError("TaskSchedulerBase_UnobservedTaskException::end");

            e.SetObserved();
        }

        private void Process()
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("private void Process() start");

            while (true)
            {
                var t = GetScheduledTasks().FirstOrDefault();

                if (null != t)
                {
                    try
                    {
                        TryExecuteTask(t);
                    }
                    catch (System.Exception ex)
                    {
                        GlobalDefinition.LoggerWrapper.LogException(ex);
                    }
                    finally
                    {
                        TryDequeue(t);
                    }
                }

                Thread.Sleep(1);
            }
        }

        protected override void QueueTask(Task task)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("protected override void QueueTask() start");

            lock (_lock)
            {
                Scheduler.Add(task);
            }

            GlobalDefinition.LoggerWrapper.LogTraceInfo("protected override void QueueTask() end");
        }

        protected override bool TryDequeue(Task task)
        {
            lock (_lock)
            {
                Scheduler.Remove(task);
            }

            return true;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (_lock)
            {
                return Scheduler.ToArray();
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("protected override bool TryExecuteTaskInline() start");
            if (taskWasPreviouslyQueued)
            {
                if (TryDequeue(task))
                {
                    return base.TryExecuteTask(task);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("protected override bool TryExecuteTaskInline() end");
                return base.TryExecuteTask(task);
            }
        }

        private readonly T _scheduler = new T();

        public T Scheduler
        {
            get { return _scheduler; }
        }
    }
}
