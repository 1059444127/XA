using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons
{
    public static class ClassExtend
    {
        /// <summary>
        /// if enumber is null or count = 0
        /// </summary>
        public static bool IsNullorEmpty(this IList list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// if enumber is null or count = 0
        /// </summary>
        public static bool IsNullorEmpty<T>(this IEnumerable<T> enumber)
        {
            return null == enumber || !enumber.Any();
        }

        /// <summary>
        /// GetCopy<T>
        /// </summary>
        /// <param name="collection">thisIEnumerable<T></param>
        /// <returns>List<T></returns>
        public static List<T> GetCopy<T>(this IEnumerable<T> collection)
        {
            if (null == collection)
                return new List<T>();
            return new List<T>(collection);
        }

        /// <summary>
        /// SplitToEach
        /// </summary>
        /// <param name="lists">thisList<string></param>
        /// <param name="eachCount">int</param>
        /// <returns>List<string></returns>
        public static List<string> SplitToEach(this List<string> lists, int eachCount, string split = ",")
        {
            if (null == lists)
                return null;
            var backList = new List<string>();
            int i = 0;
            int start, end;
            while (true)
            {
                start = i * eachCount;
                if (start + eachCount < lists.Count)
                {
                    end = eachCount;
                }
                else
                {
                    end = lists.Count - start;
                }

                if (end == 0)
                    break;
                var interlist = lists.GetRange(start, end);
                backList.Add(string.Join(split, interlist));
                if (end < eachCount)
                {
                    break;
                }
                i++;
            }
            return backList;
        }

        /// <summary>
        /// CustomJoin<T>
        /// </summary>
        /// <param name="list">thisIEnumerable<T></param>
        /// <param name="flagBegin">string</param>
        /// <param name="flagEnd">string</param>
        /// <param name="split">string</param>
        /// <returns>string</returns>
        public static string CustomJoin<T>(this IEnumerable<T> list, string flagBegin, string flagEnd, string split,
            Func<T, string> itemProFunc = null)
        {
            if (null == list)
                return string.Empty;
            if (null == itemProFunc)
            {
                var copylist = list.Where(c => c != null).Select(c => string.Concat(flagBegin, c, flagEnd));

                return string.Join(split, copylist);
            }
            else
            {
                var copylist =
                    list.Select(itemProFunc).Where(c => c != null).Select(c => string.Concat(flagBegin, c, flagEnd));
                return string.Join(split, copylist);
            }
        }

        /// <summary>
        /// InvokeWithTryCatch
        /// </summary>
        /// <param name="action">thisAction</param>
        public static void InvokeWithTryCatch(this Action action, string actionName = "")
        {
            if (null == action)
                return;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var msg = string.Format("[{0}][Exception][{1}]", actionName, ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(msg);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
        }
    }

    public static class SelectorExtend
    {
        public delegate Point GetPosition(IInputElement element);

        private static DependencyObject GetItemByIndex(this Selector selector, int index)
        {
            if (null == selector)
                return null;
            if (selector.ItemContainerGenerator.Status
                != GeneratorStatus.ContainersGenerated)
                return null;
            return selector.ItemContainerGenerator.ContainerFromIndex(index);
        }

        private static bool IsMouseTargetItem(Visual theTarget, GetPosition pos)
        {
            if (null == theTarget)
                return false;
            Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point point = pos((IInputElement)theTarget);
            return rect.Contains(point);
        }

        /// <summary>
        /// GetMouseTargetItem
        /// </summary>
        /// <param name="selector">thisSelector</param>
        /// <param name="pos">GetPosition</param>
        /// <returns>DependencyObject</returns>
        public static DependencyObject GetMouseTargetItem(this Selector selector, GetPosition pos)
        {
            if (null == selector)
                return null;
            for (int i = 0; i < selector.Items.Count; i++)
            {
                var itm = selector.GetItemByIndex(i);
                if (IsMouseTargetItem(itm as Visual, pos))
                {
                    return itm;
                }
            }
            return null;
        }
    }
}
