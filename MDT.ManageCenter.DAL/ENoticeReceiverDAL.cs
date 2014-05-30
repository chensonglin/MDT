using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace MDT.ManageCenter.DAL
{
    public class ENoticeReceiverDAL
    {
        public ManageCenterDBEntities _db;

        public ENoticeReceiverDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        public NoticeReceiver GetReceiverByUid(int uid)
        {
            var l = _db.noticereceiver.Where(d => d.uid == uid).FirstOrDefault(); ;
            return l;
        }

        public List<NoticeReceiver> GetAllReceiver()
        {
            var receivers = _db.noticereceiver.OrderBy(d => d.name).ToList();
            return receivers;
        }

        public List<NoticeReceiver> GetReceiverByName(int uid,string name,string remark)
        {
            if (uid == 0)
	        {
                var receivers = _db.noticereceiver.Where(d => d.name == name).Where(d => d.remark == remark).ToList();
                return receivers;
	        }
            else
            {
                var receivers = _db.noticereceiver.Where(d => d.name == name).Where(d => d.remark == remark).Where(d => d.uid != uid).ToList();
                return receivers;
            }
        }

        /// <summary>
        /// 添加预警通知人员
        /// </summary>
        /// <param name="receiver"></param>
        public void AddObject(NoticeReceiver receiver)
        {
            _db.noticereceiver.AddObject(receiver);
            _db.SaveChanges();
        }

        /// <summary>
        /// 修改预警通知人员信息
        /// </summary>
        /// <param name="receiver"></param>
        public void ModifyObject(NoticeReceiver receiver)
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string commandText = "update noticereceivers set name=@name,phone=@phone,email=@email,enable=@enable,remark=@remark where uid=@uid";
                DbCommand comm = db.GetSqlStringCommand(commandText);
                db.AddInParameter(comm, "name", DbType.AnsiString, receiver.name);
                db.AddInParameter(comm, "phone", DbType.AnsiString, receiver.phone);
                db.AddInParameter(comm, "email", DbType.AnsiString, receiver.email);
                db.AddInParameter(comm, "enable", DbType.Int32, receiver.enable);
                db.AddInParameter(comm, "remark", DbType.AnsiString, receiver.remark);
                db.AddInParameter(comm, "uid", DbType.Int32, receiver.uid);
                int receiverCount = db.ExecuteNonQuery(comm);
                string commUpdateAllocate = "update noticeallocate set enable=@enable where receiverid=@uid";
                DbCommand commAllocate = db.GetSqlStringCommand(commUpdateAllocate);
                db.AddInParameter(commAllocate, "enable", DbType.Int32, receiver.enable);
                db.AddInParameter(commAllocate, "uid", DbType.Int32, receiver.uid);
                int allocateCount = db.ExecuteNonQuery(commAllocate);
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 删除预警通知人员
        /// </summary>
        /// <param name="uid"></param>
        public void DeleteObject(int uid)
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string commandText = "delete from noticereceivers where uid=@uid";
                DbCommand comm = db.GetSqlStringCommand(commandText);
                db.AddInParameter(comm, "uid", DbType.Int32,uid);
                int receiverCount = db.ExecuteNonQuery(comm);
                string commUpdateAllocate = "delete from noticeallocate where receiverid=@uid";
                DbCommand commAllocate = db.GetSqlStringCommand(commUpdateAllocate);
                db.AddInParameter(commAllocate, "uid", DbType.Int32,uid);
                int allocateCount = db.ExecuteNonQuery(commAllocate);
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 查询任务对应的预警通知人员
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns></returns>
        public DataSet GetNoticeReceiver(int taskId)
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            //string commandText = "select r.uid,r.name,r.phone,r.email,r.enable,r.remark,a.etaskid,noticemode = case when a.noticemode='1' then '邮件' when a.noticemode='2' then '短信' end from noticereceivers r,NoticeAllocate a where r.enable=1 and a.receiverid=r.uid and a.etaskid=@etaskid";
            string commandText = "select r.uid,r.name,r.phone,r.email,r.enable,r.remark,LEFT(noticeMOdelAll,LEN(noticeMOdelAll)-1) as noticemode FROM noticereceivers r inner join (SELECT n.receiverid,(SELECT case when noticemode=1 then '邮件' else '短信' end +',' FROM noticeallocate WHERE receiverid=n.receiverid and etaskid=n.etaskid FOR XML PATH('')) AS noticeMOdelAll FROM noticeallocate n  where n.etaskid=@etaskid GROUP BY receiverid,etaskid)B  on r.uid=b.receiverid";
            DbCommand comm = db.GetSqlStringCommand(commandText);
            db.AddInParameter(comm, "etaskid", DbType.Int32, taskId);
            DataSet ds = db.ExecuteDataSet(comm);
            return ds;
        }

        /// <summary>
        /// 查询多个任务对应的预警通知人员
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="noticeModel">预警类型</param>
        /// <returns></returns>
        public DataSet GetNoticeReceiver()
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            string commandText = "";
            DbCommand comm = null;
            commandText = "select r.uid,r.name,r.phone,r.email,enable=case when r.enable=1 then '启用' else '停用' end ,r.remark from noticereceivers r";
            comm = db.GetSqlStringCommand(commandText);
            DataSet ds = db.ExecuteDataSet(comm);
            return ds;
        }
    }
}
