using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace MDT.ManageCenter.DAL
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public class ETaskDAL
    {
        public ManageCenterDBEntities _db;

        public ETaskDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        /// <summary>
        /// 根据任务ID获取配置任务信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ETask GetTask(int taskId)
        {
            return _db.etask.Single(c => c.ID == taskId);
        }

        /// <summary>
        /// 获取配置任务信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<ETask> GetTasks()
        {
            return _db.etask;
        }

        /// <summary>
        /// 获取配置任务信息
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<ETask> GetTasks(string taskName, int id)
        {
            if (id == 0)
            {
                return _db.etask.Where(p => p.TaskName == taskName);
            }
            else
            {
                return _db.etask.Where(p => p.TaskName == taskName).Where(t => t.ID != id);
            }
        }

        public ETask AddObject(ETask task)
        {
            _db.AddToetask(task);
            _db.SaveChanges();
            return task;
        }

        public void DeleteObject(ETask task)
        {
            _db.esource.DeleteObject(task.SourceESchema.ESource);
            _db.esource.DeleteObject(task.TargetESchema.ESource);

            _db.eschema.DeleteObject(task.SourceESchema);
            _db.eschema.DeleteObject(task.TargetESchema);

            _db.etask.DeleteObject(task);

            _db.SaveChanges();
        }

        public void ModifyObject(ETask task)
        {
            _db.SaveChanges();
            _db.Refresh(RefreshMode.ClientWins, _db.etask);
        }

        public void ModifyObject(int taskId, string mapping, string xsltInfo)
        {
            var v = _db.etask.Where(p => p.ID == taskId).SingleOrDefault();
            if (v != null)
            {
                v.Mapping = mapping;
                v.XSLTInfo = xsltInfo;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="task"></param>
        public void ModifyTask(ETask task)
        {
            var esource = _db.esource.Where(p => p.ID == task.SourceESchema.ESource.ID).SingleOrDefault();
            if (esource != null)
            {
                esource.SourceConfig = task.SourceESchema.ESource.SourceConfig;
                _db.SaveChanges();
            }
            var etarget = _db.esource.Where(p => p.ID == task.TargetESchema.ESource.ID).SingleOrDefault();
            if (etarget != null)
            {
                etarget.SourceConfig = task.TargetESchema.ESource.SourceConfig;
                _db.SaveChanges();
            }
            var sschema = _db.eschema.Where(p => p.ID == task.SourceESchema.ID).SingleOrDefault();
            if (sschema != null)
            {
                sschema.Schema = task.SourceESchema.Schema;
                _db.SaveChanges();
            }
            var tschema = _db.eschema.Where(p => p.ID == task.TargetESchema.ID).SingleOrDefault();
            if (tschema != null)
            {
                tschema.Schema = task.TargetESchema.Schema;
                _db.SaveChanges();
            }

            var v = _db.etask.Where(p => p.ID == task.ID).SingleOrDefault();
            if (v != null)
            {
                v.TaskName = task.TaskName;
                v.Note = task.Note;
                v.Interval = task.Interval;
                v.Mapping = task.Mapping;
                v.XSLTInfo = task.XSLTInfo;
                v.OperationDate = task.OperationDate;
                //v.Modified = task.Modified;
                //v.SourceESchema = task.SourceESchema;
                //v.TargetESchema = task.TargetESchema;
                //_db.Refresh(RefreshMode.ClientWins, _db.EDatabase);
                _db.SaveChanges();
            }
        }

        public List<string> GetExsistPlatform()
        {
            var v = (from c in _db.etask
                     select String.IsNullOrEmpty(c.Category) ? "" : c.Category).Distinct();
            return v.ToList();
        }

        public ESource GetSSource(int taskId)
        {
            return _db.etask.Single(c => c.ID == taskId).SourceESchema.ESource;
        }

        public ESource GetTSource(int taskId)
        {
            return _db.etask.Single(c => c.ID == taskId).TargetESchema.ESource;
        }

        public ESchema GetSSchema(int taskId)
        {
            return _db.etask.Single(c => c.ID == taskId).SourceESchema;
        }

        public ESchema GetTSchema(int taskId)
        {
            return _db.etask.Single(c => c.ID == taskId).TargetESchema;
        }

        public void TaskMove(string connStr, ArrayList listId)
        {
            try
            {
                Database db = new SqlDatabase(connStr);
                DbConnection connection = db.CreateConnection();
                connection.Open();
                for (int i = 0; i < listId.Count; i++)
                {
                    int taskId = Convert.ToInt32(listId[i]);
                    ETask etask = GetTask(taskId);
                    string commSelectByName = "select ID from ETask where TaskName=@taskName";
                    DbCommand commSelect = db.GetSqlStringCommand(commSelectByName);
                    db.AddInParameter(commSelect, "taskName", DbType.AnsiString, etask.TaskName);
                    object selectedId = db.ExecuteScalar(commSelect);
                    if (selectedId != null)  //如果已经存在 该名字的任务则不再向数据库新增数据。
                    {
                        continue;
                    }
                    ESchema sSchema = etask.SourceESchema;
                    ESchema tSchema = etask.TargetESchema;
                    ESource sSource = sSchema.ESource;
                    ESource tSource = tSchema.ESource;
                    string commTextSSource = "insert into ESource(ESourceBaseInfo_ID,Customer_ID,SourceConfig,SourceType,OriginalConfig) values(@baseInfoID,@customerID,@sourceConfig,@sourceType,@originalConfig)  select scope_identity()";
                    string commTextTSource = "insert into ESource(ESourceBaseInfo_ID,Customer_ID,SourceConfig,SourceType,OriginalConfig) values(@baseInfoID,@customerID,@sourceConfig,@sourceType,@originalConfig)  select scope_identity()";
                    string commTextSSchema = "insert into ESchema(Customer_ID,ESource_ID,[Schema],Type) values(@customerID,@eSourceID,@schema,@type)  select scope_identity()";
                    string commTextTSchema = "insert into ESchema(Customer_ID,ESource_ID,[Schema],Type) values(@customerID,@eSourceID,@schema,@type)  select scope_identity()";
                    string commTextTask = "insert into ETask(SourceESchema_ID,TargetESchema_ID,TaskName,XSLTInfo,Mapping,Enable,Note,Interval) values(@sSchemaID,@tSchemaID,@taskName,@xslt,@mapping,@enable,@note,@interval) ";

                    DbTransaction trans = connection.BeginTransaction();
                    try
                    {
                        DbCommand comm = db.GetSqlStringCommand(commTextSSource);
                        db.AddInParameter(comm, "baseInfoID", DbType.Int32, sSource.ESourceBaseInfo_ID);
                        //db.AddInParameter(comm, "customerID", DbType.Int32, sSource.Customer_ID);
                        db.AddInParameter(comm, "sourceConfig", DbType.Xml, sSource.SourceConfig);
                        db.AddInParameter(comm, "sourceType", DbType.AnsiString, sSource.SourceType);
                        db.AddInParameter(comm, "originalConfig", DbType.Xml, sSource.OriginalConfig);
                        int sSourceId = Convert.ToInt32(db.ExecuteScalar(comm));

                        comm = db.GetSqlStringCommand(commTextTSource);
                        db.AddInParameter(comm, "baseInfoID", DbType.Int32, tSource.ESourceBaseInfo_ID);
                        //db.AddInParameter(comm, "customerID", DbType.Int32, tSource.Customer_ID);
                        db.AddInParameter(comm, "sourceConfig", DbType.Xml, tSource.SourceConfig);
                        db.AddInParameter(comm, "sourceType", DbType.AnsiString, tSource.SourceType);
                        db.AddInParameter(comm, "originalConfig", DbType.Xml, tSource.OriginalConfig);
                        int tSourceId = Convert.ToInt32(db.ExecuteScalar(comm));

                        comm = db.GetSqlStringCommand(commTextSSchema);
                        //db.AddInParameter(comm, "customerID", DbType.Int32, sSchema.Customer_ID);
                        db.AddInParameter(comm, "eSourceID", DbType.Int32, sSourceId);
                        db.AddInParameter(comm, "schema", DbType.Xml, sSchema.Schema);
                        db.AddInParameter(comm, "type", DbType.Int32, sSchema.Type);
                        int sSchemaId = Convert.ToInt32(db.ExecuteScalar(comm));

                        comm = db.GetSqlStringCommand(commTextTSchema);
                        //db.AddInParameter(comm, "customerID", DbType.Int32, tSchema.Customer_ID);
                        db.AddInParameter(comm, "eSourceID", DbType.Int32, tSourceId);
                        db.AddInParameter(comm, "schema", DbType.Xml, tSchema.Schema);
                        db.AddInParameter(comm, "type", DbType.Int32, tSchema.Type);
                        int tSchemaId = Convert.ToInt32(db.ExecuteScalar(comm));

                        comm = db.GetSqlStringCommand(commTextTask);
                        db.AddInParameter(comm, "sSchemaID", DbType.Int32, sSchemaId);
                        db.AddInParameter(comm, "tSchemaID", DbType.Int32, tSchemaId);
                        db.AddInParameter(comm, "taskName", DbType.AnsiString, etask.TaskName);
                        db.AddInParameter(comm, "xslt", DbType.Xml, etask.XSLTInfo);
                        db.AddInParameter(comm, "mapping", DbType.Xml, etask.Mapping);
                        db.AddInParameter(comm, "enable", DbType.Byte, false);
                        db.AddInParameter(comm, "note", DbType.AnsiString, etask.Note);
                        db.AddInParameter(comm, "interval", DbType.Int32, etask.Interval);
                        db.ExecuteNonQuery(comm);
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
