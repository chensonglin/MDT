using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;

namespace MDT.ManageCenter.ServiceContract
{
    /// <summary>
    /// 数据交换管理服务
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IManageCenterService
    {
        [OperationContract]
        void AddTask(ETask task);

        // 数据转换服务使用 modify by wangk 2012-12-07
        //[OperationContract]
        //string GetXSLT(int taskId);

        [OperationContract]
        void AllocateTask(int clientId, int[] taskIds);

        [OperationContract]
        List<TaskInfo> GetTasks();

        [OperationContract(Name = "GetTasksByClient")]
        List<TaskInfo> GetTasks(int clientId);
    }
}
