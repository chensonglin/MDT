using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.DataContract;

namespace MDT.ManageCenter.ServiceContract
{
    /// <summary>
    /// 数据交换日志服务
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface ITraceLogCenterService
    {
        [OperationContract]
        void Write(TraceLogInfo traceInfo);

        [OperationContract]
        TraceLogInfo Read(int traceId);
    }
}
