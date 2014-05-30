using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    /// <summary>
    /// 任务类型
    /// </summary>
    [DataContract]
    public enum TaskType
    {
        /// <summary>
        /// 交换任务
        /// </summary>
        [EnumMember]
        ET,

        /// <summary>
        /// 服务任务
        /// </summary>
        [EnumMember]
        ST,

        /// <summary>
        /// 日志任务
        /// </summary>
        [EnumMember]
        LT
    }

    /// <summary>
    /// 任务实体
    /// </summary>
    [DataContract]
    public class TaskInfo
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [DataMember]
        public virtual int ID { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [DataMember]
        public string TaskName { get; set; }

        /// <summary>
        /// SourceXSD_ID
        /// </summary>
        [DataMember]
        public virtual int SourceXSD_ID { get; set; }

        /// <summary>
        /// TargetXSD_ID
        /// </summary>
        [DataMember]
        public virtual int TargetXSD_ID { get; set; }

        /// <summary>
        /// 样式信息
        /// </summary>
        [DataMember]
        public virtual string XSLTInfo { get; set; }

        /// <summary>
        /// 映射信息
        /// </summary>
        [DataMember]
        public virtual string Mapping { get; set; }

        /// <summary>
        /// 时间间隔
        /// </summary>
        [DataMember]
        public int Interval { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [DataMember]
        public bool Enable { get; set; }

        /// <summary>
        /// 事后任务
        /// </summary>
        [DataMember]
        public List<TaskUnit> PostTasks { get; set; }

        /// <summary>
        /// 任务类型（ET-交换任务，ST-服务任务， LT-日志任务）
        /// </summary>
        [DataMember]
        public TaskType Type { get; set; }

        /// <summary>
        /// 分类信息
        /// </summary>
        [DataMember]
        public string Category { get; set; }

    }
}
