using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.EntityClient;

namespace MDT.ManageCenter.DAL
{
    public class ETaskAllocationDAL
    {
        public ManageCenterDBEntities _db;

        public ETaskAllocationDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alls"></param>
        public void AddObject(ETaskAllocation[] alls)
        {
            foreach (var p in alls)
            {
                var task = _db.etaskallocation.FirstOrDefault(c => c.ETask_ID == p.ETask_ID);
                if (task != null)
                {
                    task.EClient_ID = p.EClient_ID;
                }
                else
                {
                    _db.AddToetaskallocation(p);
                }
            }

            _db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        public void DeleteObject(int clientId)
        {
            var tasks = _db.etaskallocation.Where(c => c.EClient_ID == clientId);
            foreach (var t in tasks)
            {
                _db.etaskallocation.DeleteObject(t);
            }

            _db.SaveChanges();
        }

        /// <summary>
        /// 查询分配给指定客户端的任务
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetETaskByClientID(int clientId)
        {
            EntityConnectionStringBuilder entityConn = new EntityConnectionStringBuilder();
            entityConn.ConnectionString = ConfigurationManager.ConnectionStrings["ManageCenterDBEntities"].ToString();
            Database db = new GenericDatabase(entityConn.ProviderConnectionString
                                              , DbProviderFactories.GetFactory(entityConn.Provider));
            string commandText =@"select id,taskname,xsltinfo,mapping,note,category,type from etask where id 
                                in ( SELECT etask_id from etaskAllocation where eclient_id = ?clientid) order by category,taskname"; 
            DbCommand comm = db.GetSqlStringCommand(commandText);
            db.AddInParameter(comm, "?clientid", DbType.AnsiString, clientId);

            try
            {
                return db.ExecuteDataSet(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询任务的分配信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetETaskAllocation()
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            string commandText = "select t.ID,t.TaskName,t.Note,c.id,c.Name from ETask as t left join ETaskAllocation as a on t.ID=a.ETask_ID left join EClient as c on a.EClient_ID=c.id where t.enable='1' order by t.TaskName";
            DbCommand comm = db.GetSqlStringCommand(commandText);
            try
            {
                return db.ExecuteDataSet(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }  
}
