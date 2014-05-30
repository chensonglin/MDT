using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using MDT.DataProducer.ServiceImplement.DataProducerCenter;

namespace MDT.DataProducer.ServiceImplement
{
    /// <summary>
    /// 数据格式转换服务
    /// </summary>
    public interface IDataTransformService
    {
        void Send(int taskId, string processLN, string data);

        void ReSend(int traceId);

        IDataProducerCenterService Center { get; set; }
    }
}
