using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.ServiceModel;
using MDT.Utility;
using MDT.ManageCenter.ServiceContract;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.DAL;

namespace MDT.ManageCenter.ServiceImplement
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DataConsumerCenterService : IDataConsumerCenterService
    {
        public Source GetSourceInfo(int taskId)
        {
            ETaskDAL taskDAL = new ETaskDAL();
            var esource = taskDAL.GetTSource(taskId);
            return CommonUtility.UnserializeXml<Source>(esource.SourceConfig);
        }

        public string GetSchema(int taskId)
        {
            ETaskDAL taskDAL = new ETaskDAL();
            return taskDAL.GetTSchema(taskId).Schema;
        }
    }
}
