using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using MDT.ManageCenter.DataContract;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections;

namespace MDT.ManageCenter.DAL
{
    public class ENoticeServiceDAL
    {
        public List<ENoticeService> GetNoticeServiceList(int taskID)
        {
            List<ENoticeService> lstService = new List<ENoticeService>();
            ENoticeService service = null;
            string connString = ConfigurationManager.ConnectionStrings["MDT_ConString"].ConnectionString;
            string strSql = "select distinct etaskid,noticemode,name,phone,email from v_noticeService where etaskid='" + taskID + "'";

            try
            {
                Database db = new SqlDatabase(connString);
                DbCommand comm = db.GetSqlStringCommand(strSql);
                DataSet ods = db.ExecuteDataSet(comm);
                if (ods != null && ods.Tables.Count > 0 && ods.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ods.Tables[0].Rows)
                    {
                        service = new ENoticeService();
                        service.TaskID = taskID;
                        service.Name = (dr["name"] == null ? "" : dr["name"].ToString());
                        service.Phone = (dr["phone"] == null ? "" : dr["phone"].ToString());
                        service.Email = (dr["email"] == null ? "" : dr["email"].ToString());
                        service.NoticeMode = (dr["noticemode"] == null ? 0 : Convert.ToInt32(dr["noticemode"].ToString()));
                        lstService.Add(service);
                    }
                }
            }
            catch (Exception)
            {
                lstService.Clear();
            }
            return lstService;
        }

        /// <summary>
        /// 设置预警通知
        /// </summary>
        /// <param name="listTaskId">任务id集合</param>
        /// <param name="listReceiverEmail">邮件通知人员集合</param>
        /// <param name="listReceiverPhone">短信通知人员集合</param>
        public void NoticeAllocate(ArrayList listTaskId, ArrayList listReceiverEmail,ArrayList listReceiverPhone )
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            DbConnection dbConn = db.CreateConnection();
            dbConn.Open();
            DbTransaction dbTrans = dbConn.BeginTransaction();
            try
            {
                string sql = "";
                DbCommand dbComm = null;
                for (int i = 0; i < listTaskId.Count; i++)
                {
                    int taskId = Convert.ToInt32(listTaskId[i]);
                    for (int j = 0; j < listReceiverEmail.Count; j++)
                    {
                        int receiverId = Convert.ToInt32(listReceiverEmail[j]);
                        sql = "select nid from noticeallocate where etaskid=@etaskid and receiverid=@receiverid and noticemode=1";
                        dbComm = db.GetSqlStringCommand(sql);
                        db.AddInParameter(dbComm, "etaskid", DbType.Int32, taskId);
                        db.AddInParameter(dbComm, "receiverid", DbType.Int32, receiverId);
                        DataSet ds = db.ExecuteDataSet(dbComm);//查询是否重复设置
                        if (ds.Tables[0].Rows.Count < 1)//如果没有重复
                        {
                            sql = "insert into noticeallocate(etaskid,noticemode,receiverid,enable) values(@etaskid,1,@receiverid,1)";
                            dbComm = db.GetSqlStringCommand(sql);
                            db.AddInParameter(dbComm, "etaskid", DbType.Int32, taskId);
                            db.AddInParameter(dbComm, "receiverid", DbType.Int32, receiverId);
                            db.ExecuteNonQuery(dbComm);
                        }
                    }
                    for (int k = 0; k < listReceiverPhone.Count; k++)
                    {
                        int receiverId = Convert.ToInt32(listReceiverPhone[k]);
                        sql = "select nid from noticeallocate where etaskid=@etaskid and receiverid=@receiverid and noticemode=2";
                        dbComm = db.GetSqlStringCommand(sql);
                        db.AddInParameter(dbComm, "etaskid", DbType.Int32, taskId);
                        db.AddInParameter(dbComm, "receiverid", DbType.Int32, receiverId);
                        DataSet ds = db.ExecuteDataSet(dbComm);//查询是否重复设置
                        if (ds.Tables[0].Rows.Count < 1)//如果没有重复
                        {
                            sql = "insert into noticeallocate(etaskid,noticemode,receiverid,enable) values(@etaskid,2,@receiverid,1)";
                            dbComm = db.GetSqlStringCommand(sql);
                            db.AddInParameter(dbComm, "etaskid", DbType.Int32, taskId);
                            db.AddInParameter(dbComm, "receiverid", DbType.Int32, receiverId);
                            db.ExecuteNonQuery(dbComm);
                        }
                    }
                }
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }

        /// <summary>
        /// 取消对人员的预警通知
        /// </summary>
        /// <param name="listTaskId">任务id集合</param>
        /// <param name="listReceiverEmail">邮件通知人员集合</param>
        /// <param name="listReceiverPhone">短信通知人员集合</param>
        public void CancelAllocate(ArrayList listTaskId, ArrayList listReceiverEmail, ArrayList listReceiverPhone)
        {
            string connString = ConfigurationManager.AppSettings["MDT_ConString"];
            Database db = new SqlDatabase(connString);
            DbConnection dbConn = db.CreateConnection();
            dbConn.Open();
            DbTransaction dbTrans = dbConn.BeginTransaction();
            try
            {
                string sql = "";
                DbCommand dbComm = null;
                for (int i = 0; i < listTaskId.Count; i++)
                {
                    int taskId = Convert.ToInt32(listTaskId[i]);
                    for (int j = 0; j < listReceiverEmail.Count; j++)//循环取消邮件通知
                    {
                        int receiverId = Convert.ToInt32(listReceiverEmail[j]);
                        sql = "delete from noticeallocate where etaskid=@etaskid and receiverid=@receiverid and noticemode=1";
                        dbComm = db.GetSqlStringCommand(sql);
                        db.AddInParameter(dbComm, "etaskid", DbType.Int32, taskId);
                        db.AddInParameter(dbComm, "receiverid", DbType.Int32, receiverId);
                        db.ExecuteNonQuery(dbComm);
                    }
                    for (int k = 0; k < listReceiverPhone.Count; k++)//循环取消短信通知
                    {
                        int receiverId = Convert.ToInt32(listReceiverPhone[k]);
                        sql = "delete from noticeallocate where etaskid=@etaskid and receiverid=@receiverid and noticemode=2";
                        dbComm = db.GetSqlStringCommand(sql);
                        db.AddInParameter(dbComm, "etaskid", DbType.Int32, taskId);
                        db.AddInParameter(dbComm, "receiverid", DbType.Int32, receiverId);
                        db.ExecuteNonQuery(dbComm);
                    }
                }
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConn.Close();
            }
        }
    }
}
