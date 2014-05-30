using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.DataContract;

namespace MDT.ManageCenter.ServiceContract
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IDataConsumerCenterService
    {
        [OperationContract]
        Source GetSourceInfo(int taskId);

        [OperationContract]
        string GetSchema(int taskId);
    }
}
