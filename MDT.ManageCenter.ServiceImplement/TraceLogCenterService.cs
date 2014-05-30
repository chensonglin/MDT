using System;
using System.Linq;
using System.ServiceModel;
using MDT.ManageCenter.ServiceContract;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.ServiceImplement.EmailCenter;
using MDT.Utility;
namespace MDT.ManageCenter.ServiceImplement
{
    /// <summary>
    /// 数据交换跟踪服务
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TraceLogCenterService : ITraceLogCenterService
    {
        #region IExchangeTraceService Members

        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="tracelog"></param>
        public void Write(TraceLogInfo tracelog)
        {
            try
            {
                TraceLogDAL dal = new TraceLogDAL();
                dal.Write(tracelog);
            }
            catch (Exception ex)
            {
                string strMsg = String.Format("\r\nID:{0}\r\nData:{1}", tracelog.TaskId, tracelog.Data);
                TextWriter.WriteExceptionLog(ex, strMsg, true);

                //ExceptionMsg msg = new ExceptionMsg();
                //msg.Message = String.Format("\r\n{0},\r\n{1}", ex.Message, ex.StackTrace);
                //throw new FaultException<ExceptionMsg>(msg, "MDT001");
            }

            // 重写记录日志代码 modify by wk 2013-01-23
            //try
            //{
            //    TraceLog log = new TraceLog()
            //    {
            //        Data = tracelog.Data,
            //        DataCount = tracelog.DataCount,
            //        EndTime = tracelog.EndTime,
            //        ProcessLN = tracelog.ProcessLN,
            //        RunInfo = tracelog.RunInfo,
            //        Stage = Enum.GetName(typeof(TraceStage), tracelog.Stage),
            //        Status = Enum.GetName(typeof(TraceStatus), tracelog.Status),
            //        ETask_ID = tracelog.TaskId,
            //        StartTime = tracelog.StartTime
            //    };

            //    dal.Write(log);

            //    // 发生错误信息（邮件方式）
            //    //if (traceInfo.State == TraceState.Failed && !String.IsNullOrEmpty(traceInfo.RunInfo))
            //    //{
            //    //    string message = String.Format("TaskId：{0}  Stage：{1}\n\n{2}"
            //    //                                   , traceInfo.TaskId
            //    //                                   , traceInfo.Stage
            //    //                                   , traceInfo.RunInfo);
            //    //using (EmailCenterServiceClient service = new EmailCenterServiceClient())
            //    //{
            //    //集成邮件短信
            //    //service.Send(message);
            //    //service.ActiveNoticeService(traceInfo.TaskId, message);
            //    //service.Close();
            //    //}
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    string strMsg = String.Format("\r\nID:{0}\r\nData:{1}", tracelog.TaskId, tracelog.Data);
            //    TextWriter.WriteExceptionLog(ex, strMsg, true);
            //}
        }

        /// <summary>
        /// 读取日志信息
        /// </summary>
        /// <param name="traceId"></param>
        /// <returns></returns>
        public TraceLogInfo Read(int traceId)
        {
            TraceLogInfo traceInfo = null;
            TraceLogDAL dal = new TraceLogDAL();
            var trace = dal.Read().FirstOrDefault(c => c.ID == traceId);

            if (trace != null)
            {
                traceInfo = new TraceLogInfo();
                traceInfo.Data = trace.Data;
                //traceInfo.DataCount = trace.DataCount;
                traceInfo.EndTime = trace.EndTime;
                //traceInfo.ID = trace.ID;
                //traceInfo.ProcessLN = trace.ProcessLN;
                traceInfo.RunInfo = trace.RunInfo;
                traceInfo.Stage = (TraceStage)Enum.Parse(typeof(TraceStage), trace.Stage);
                traceInfo.Status = (TraceStatus)Enum.Parse(typeof(TraceStatus), trace.Status);
                traceInfo.StartTime = trace.StartTime;
                //traceInfo.TaskId = trace.ETask_ID;
            }

            return traceInfo;
        }
        
        #endregion
    }
}
