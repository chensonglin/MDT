using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    /// <summary>
    /// 处理阶段
    /// </summary>
    [DataContract]
    public enum TraceStage
    {
        /// <summary>
        /// 数据生产
        /// </summary>
        [EnumMember]
        DataProducer,

        /// <summary>
        /// 数据转换 code by wk 注释代码
        /// </summary>
        //[EnumMember]
        //DataTransform,

        /// <summary>
        /// 数据消费
        /// </summary>
        [EnumMember]
        DataConsumer
    }

    /// <summary>
    /// 处理状态
    /// </summary>
    [DataContract]
    public enum TraceStatus
    {
        /// <summary>
        /// 处理
        /// </summary>
        [EnumMember]
        Handle,

        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember]
        Success,

        /// <summary>
        /// 失败
        /// </summary>
        [EnumMember]
        Failed
    }

    /// <summary>
    /// 数据交换跟踪实体
    /// </summary>
    [DataContract]
    public class TraceLogInfo
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        [DataMember]
        public int TaskId { get; set; }

        /// <summary>
        /// 子任务任务ID
        /// </summary>
        [DataMember]
        public int SubTaskId { get; set; }

        ///// <summary>
        ///// 处理批次号
        ///// </summary>
        //[DataMember]
        //public string ProcessLN { get; set; }

        ///// <summary>
        ///// 用户ID
        ///// </summary>
        //[DataMember]
        //public int CustomerId { get; set; }

        /// <summary>
        /// 处理阶段
        /// 1，数据生产
        /// 2，数据转换
        /// 3，数据消费
        /// </summary>
        [DataMember]
        public TraceStage Stage { get; set; }

        /// <summary>
        /// 执行状态
        /// 1，成功--S
        /// 2，失败--E
        /// 3，处理--H
        /// </summary>
        [DataMember]
        public TraceStatus Status { get; set; }

        /// <summary>
        /// 运行信息
        /// </summary>
        [DataMember]
        public string RunInfo { get; set; }

        /// <summary>
        /// 数据信息
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        /// 数据条数
        /// </summary>
        [DataMember]
        public int DataCount { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public Nullable<DateTime> EndTime { get; set; }
    }
}
