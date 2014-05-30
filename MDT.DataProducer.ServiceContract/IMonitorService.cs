using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MDT.DataProducer.ServiceContract
{
    /// <summary>
    /// 源数据监控服务，负责监控数据并将数据发送到数据交换服务
    /// </summary>
    [ServiceContract]
    public interface IMonitorService
    {
        int ServiceId { get; set; }

        [OperationContract(IsOneWay = true)]
        void Start();

        [OperationContract]
        void Stop();
    }
}
