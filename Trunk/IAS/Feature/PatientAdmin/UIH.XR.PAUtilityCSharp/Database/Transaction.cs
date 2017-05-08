using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Database;

namespace UIH.XA.PAUtilityCSharp.Database
{
    public class Transaction
    {
        public Transaction()
        {
            this._dbWrapper = DataAccessHelper.Default.GetDBInstance();
        }

        private readonly DBWrapper _dbWrapper;

        /// <summary>
        /// Begin
        /// </summary>
        public void Begin()
        {
            if (null != _dbWrapper)
                _dbWrapper.BeginTransaction();
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            if (null != _dbWrapper)
                _dbWrapper.Commit();
        }

        /// <summary>
        /// Rollback
        /// </summary>
        public void Rollback()
        {
            if (null != _dbWrapper)
                _dbWrapper.Rollback();
        }
    }
}
