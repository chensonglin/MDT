using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.DataContract;

namespace MDT.ManageCenter.ServiceContract
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IDataProducerCenterService
    {
        [OperationContract]
        Source GetSourceInfo(int taskId);

        [OperationContract]
        Table GetTableInfo(int taskId);
       
        [OperationContract]
        string GetSchema(int taskId);

        [OperationContract]
        string GetXSLT(int taskId);

        [OperationContract]
        List<TaskInfo> GetTasks(int clientId);

        //[OperationContract]
        //bool IsSucess(string processLN);
    }
}
