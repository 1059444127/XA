using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Database;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons
{
    public static class LockCommentDefinition
    {
        /// <summary>
        /// ToComment
        /// </summary>
        /// <param name="comment">thisLockComment</param>
        /// <returns>string</returns>
        public static string ToComment(this LockComment comment)
        {
            switch (comment)
            {
                case LockComment.StudySplit:
                    return "UID_PA_StudySplit_LockComment";
                case LockComment.StudyMerge:
                    return "UID_PA_StudyMerge_LockComment";
                case LockComment.StudyModify:
                    return "UID_PA_StudyModify_LockComment";
                case LockComment.StudyCopy:
                    return "UID_PA_StudyCopy_LockComment";
                case LockComment.StudyExportToCsv:
                    return "UID_PA_StudyExportToCsv_LockComment";
            }
            return string.Empty;
        }

        /// <summary>
        /// ToLockMsg
        /// </summary>
        /// <param name="lockItem">thisLockItem</param>
        /// <returns>string</returns>
        public static string ToLockMsg(this LockItem lockItem)
        {
            if (null == lockItem)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("[ToLockMsg][lockItem == null]");
                return string.Empty;
            }

            GlobalDefinition.LoggerWrapper.LogDevWarning("[ToLockMsg][Comment:" + lockItem.Comment + "]");

            var lockTip = GlobalDefinition.ToLocalLanguge("UID_PA_Item_LockedInfo");
            if (string.IsNullOrEmpty(lockTip))
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("[ToLockMsg][lockTip is empty]");
                return string.Empty;
            }

            if (string.IsNullOrEmpty(lockItem.Comment))
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("[ToLockMsg][lockItem.Comment is empty]");
                return lockTip;
            }

            var c = GlobalDefinition.ToLocalLanguge(lockItem.Comment);
            if (string.IsNullOrEmpty(c))
            {
                GlobalDefinition.LoggerWrapper.LogDevWarning("[ToLockMsg][not find resource:" + lockItem.Comment + "]");
                return lockTip;
            }

            return string.Format("{0} ({1}...)", lockTip, c);
        }
    }
}
