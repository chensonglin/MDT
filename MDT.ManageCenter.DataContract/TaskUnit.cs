using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class TaskUnit
    {
        /// <summary>
        /// 任务ID（子任务）
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        //[DataMember]
        //public string name { get; set; }  //2011-8-8日cfl注释掉的

        /// <summary>
        /// 是否记录日志
        /// </summary>
        [DataMember]
        public bool HasTraceLog { get; set; }

        /// <summary>
        /// 是否启用事务
        /// </summary>
        [DataMember]
        public bool HasTransaction { get; set; }

        /// <summary>
        /// 执行命令
        /// </summary>
        [DataMember]
        public List<ECommand> Commands { get; set; }

        ////事后任务的  执行命令
        //[DataMember]
        //public List<ECommand> PostTaskCommands { get; set; } //2011-8-8日cfl注释掉的

        ////结果集
        //[DataMember]
        //public List<Result> Results { get; set; }//cfl  
        //[DataMember]
        //public List<ETransaction> Transactions { get; set; }
    }
}
