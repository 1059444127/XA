using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons.TaskSchedulerExtension
{
    /// <summary>
    /// 1.查询数据的逻辑是Study>Series/RawData/Report>Image
    /// 2.当父节点查询更改时，子节点的查询操作应该取消
    /// 3.当有查询时，前期同级别的查询应该取消
    /// 4.切换数据源时，前期所有查询应该取消。
    /// </summary>
    public class DynamicDataQueryQueueScheduler : DynamicQueueScheduler
    {
        public enum QueryLevel
        {
            Study,
            Series,
            Rawdata,
            Report,
            Image,
            Rtss
        }

        public enum SourceFlag
        {
            RIS,
            LocalDB,
        }

        public class DBQueryArgs
        {
            public QueryLevel Level { get; set; }
            public SourceFlag Source { get; set; }
            public DBQueryArgs Parent { get; set; }

            public override bool Equals(object obj)
            {
                DBQueryArgs query = obj as DBQueryArgs;

                if (null == query)
                {
                    return false;
                }

                if (query.Source != Source && query.Level == QueryLevel.Study && this.Level == QueryLevel.Study)
                {
                    return true;
                }

                if (query.Level == Level)
                {
                    return true;
                }

                if (null != Parent && Parent.Equals(query))
                {
                    return true;
                }

                return false;
            }

            public override int GetHashCode()
            {
                return (Level.GetHashCode() + Source.GetHashCode());
            }
        }

        public override bool Contains(Task t, out Task oldTask)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("public void Contains() start");

            bool found = false;
            oldTask = null;

            var newDBQuery = t.AsyncState as DBQueryArgs;

            foreach (var task in _queue)
            {
                var dbQuery = task.AsyncState as DBQueryArgs;
                if (newDBQuery != null
                    && dbQuery != null)
                {
                    if (newDBQuery.Equals(dbQuery))
                    {
                        oldTask = task;
                        found = true;

                        break;
                    }
                }
            }

            GlobalDefinition.LoggerWrapper.LogTraceInfo("public void Contains() end");
            return found;
        }
    }
}
