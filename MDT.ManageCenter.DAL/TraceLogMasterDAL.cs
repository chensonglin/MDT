using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace MDT.ManageCenter.DAL
{
    public class TraceLogMasterDAL
    {
        ManageCenterDBEntities _db;

        public TraceLogMasterDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        public void Update(TraceLogMaster log)
        {
            _db.SaveChanges();
        }

        public IQueryable<TraceLogMaster> GetTraceLogs()
        {
            return this._db.tracelogmaster;
        }
    }
}

