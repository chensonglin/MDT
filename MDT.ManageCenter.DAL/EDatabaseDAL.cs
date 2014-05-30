using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Data;
using MDT.ManageCenter.DataContract;
using MDT.Utility;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace MDT.ManageCenter.DAL
{
    public class EDatabaseDAL
    {
        public ManageCenterDBEntities _db;

        public EDatabaseDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        public List<EDatabase> GetDatabases()
        {
            _db.Refresh(RefreshMode.ClientWins, _db.esourcebaseinfo);
            var l = _db.esourcebaseinfo.OrderBy(d => d.ID).ToList();
            List<EDatabase> nl = new List<EDatabase>();
            foreach (var m in l)
            {
                try
                {
                    nl.Add(CommonUtility.UnserializeXml<EDatabase>(m.Config));
                }
                catch { }
            }
            return nl;
        }

        public EDatabase GetDatabase(int id)
        {
            var l = _db.esourcebaseinfo.SingleOrDefault(d => d.ID == id);
            return CommonUtility.UnserializeXml<EDatabase>(l.Config);
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="database"></param>
        public void AddObject(EDatabase database)
        {
            ESourceBaseInfo n = new ESourceBaseInfo();
            _db.esourcebaseinfo.AddObject(n);
            _db.SaveChanges();
            database.ID = n.ID;
            n.Config = CommonUtility.SerializeXml<EDatabase>(database);
            _db.SaveChanges();
        }

        /// <summary>
        /// 删除对接
        /// </summary>
        /// <param name="database"></param>
        public void DeleteObject(EDatabase database)
        {
            var n = _db.esourcebaseinfo.Single(c => c.ID == database.ID);
            _db.esourcebaseinfo.DeleteObject(n);
            _db.SaveChanges();
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="database"></param>
        public void ModifyObject(EDatabase database)
        {
            var v = _db.esourcebaseinfo.Where(p => p.ID == database.ID).SingleOrDefault();

            if (v != null)
            {
                v.Config = CommonUtility.SerializeXml<EDatabase>(database);
                _db.SaveChanges();
            }
        }

        public bool ContainAlias(string alias)
        {
            return false;
            //var database = _db.ESourceBaseInfo.Where(p => CommonUtility.Unserialize<EDatabase>(p.Config).Alias== alias).FirstOrDefault();

            //if (database != null)
            //    return true;
            //elseuuuu
            //    return false;
        }
    }
}
