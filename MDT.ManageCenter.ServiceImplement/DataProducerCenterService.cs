using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.ServiceModel;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceContract;
using MDT.ManageCenter.DataContract;
using MDT.Utility;

namespace MDT.ManageCenter.ServiceImplement
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DataProducerCenterService : IDataProducerCenterService
    {
        public Source GetSourceInfo(int taskId)
        {
            ETaskDAL taskDAL = new ETaskDAL();
            var esource = taskDAL.GetSSource(taskId);
            return CommonUtility.UnserializeXml<Source>(esource.SourceConfig);          
        }

        public Table GetTableInfo(int taskId)
        {
            ETaskDAL taskDAL = new ETaskDAL();
            var esource = taskDAL.GetSSource(taskId);
            return CommonUtility.UnserializeXml<Table>(esource.OriginalConfig);          
        }

        public string GetSchema(int taskId)
        {
            ETaskDAL taskDAL = new ETaskDAL();
            return taskDAL.GetSSchema(taskId).Schema;
        }

        public string GetXSLT(int taskId)
        {
            ETaskDAL d = new ETaskDAL();
            return d.GetTask(taskId).XSLTInfo;
        }

        public List<TaskInfo> GetTasks(int clientId)
        {
            List<TaskInfo> taskList = new List<TaskInfo>();
            ETaskDAL taskDAL = new ETaskDAL();
            var p = taskDAL.GetTasks().Where(c => c.Enable == true && c.ETaskAllocation.EClient_ID == clientId);
            foreach (var t in p)
            {
                taskList.Add(new DataContract.TaskInfo()
                {
                    ID = t.ID,
                    TaskName = t.TaskName,
                    Mapping = t.Mapping,
                    SourceXSD_ID = t.SourceESchema_ID,
                    TargetXSD_ID = t.TargetESchema_ID,
                    XSLTInfo = t.XSLTInfo,
                    Interval = t.Interval,
                    Type = (TaskType)Enum.Parse(typeof(TaskType), t.Type),
                });
            }
            return taskList;
        }

        //public bool IsSucess(string processLN)
        //{
        //    TraceLogDAL d = new TraceLogDAL();
        //    string state = TraceState.Success.ToString();
        //    string stage = TraceStage.DataConsumer.ToString();
        //    return d.Read().Where(c => c.ProcessLN == processLN && c.State == state && c.Stage == stage).Count() > 0;
        //}
    }
}
