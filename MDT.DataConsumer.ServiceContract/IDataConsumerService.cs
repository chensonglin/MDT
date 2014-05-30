using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MDT.DataConsumer.ServiceContract
{
    /// <summary>
    /// 数据发送服务
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IDataConsumerService
    {
        [OperationContract]
        void Send(int taskId, string processLN, string data);

        [OperationContract]
        void ReSend(int traceId);
    }
}
