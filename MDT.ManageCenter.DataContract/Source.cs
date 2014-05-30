using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [DataContract]
    public class Source
    {
        /// <summary>
        /// 主要任务集合
        /// </summary>
        [DataMember]
        public List<TaskUnit> MainTasks { get; set; }

        /// <summary>
        /// 事后任务集合
        /// </summary>
        [DataMember]
        public List<TaskUnit> PostTasks { get; set; }

        /// <summary>
        /// 结果集
        /// </summary>
        [DataMember]
        public List<Result> Results { get; set; }
    }
}
