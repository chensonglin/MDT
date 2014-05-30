using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class Table
    {
        /// <summary>
        /// 表名称
        /// </summary>
        [DataMember(Order = 1)]
        public string TableName { get; set; }

        /// <summary>
        /// 主键列
        /// </summary>
        [DataMember(Order = 2)]
        public EColumn PKColumn { get; set; }

        /// <summary>
        /// 非主键列
        /// </summary>
        [DataMember(Order = 3)]
        public List<EColumn> Columns { get; set; }

        /// <summary>
        /// 关联表
        /// </summary>
        [DataMember(Order = 4)]
        public List<RelatedTable> RelatedTables { get; set; }

        /// <summary>
        /// 检索条件
        /// </summary>
        [DataMember(Order = 5)]
        public string AdditionalWhere { get; set; }

        /// <summary>
        /// 事后存储过程名
        /// </summary>
        [DataMember(Order = 6)]
        public string PostStoredProcedure { get; set; }
    }
}
