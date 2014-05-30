using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ManageCenter.DAL
{
    public class ESchemaDAL
    {
        public ManageCenterDBEntities _db;

        public ESchemaDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        /// <summary>
        /// 添加Schema文件
        /// </summary>
        /// <param name="xsd"></param>
        /// <returns></returns>
        public ESchema AddObject(ESchema xsd)
        {
            _db.AddToeschema(xsd);
            _db.SaveChanges();
            return xsd;
        }
    }
}
