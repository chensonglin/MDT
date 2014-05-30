using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class Result
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        [DataMember(Order = 1)]
        public string CommandName { get; set; }

        /// <summary>
        /// 查询路径
        /// </summary>
        [DataMember(Order = 2)]
        public string XmlPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 3)]
        public string ValueFromField { get; set; }

        /// <summary>
        /// 子查询路径
        /// </summary>
        [DataMember(Order = 4)]
        public string SubXmlPath { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        [DataMember(Order = 5)]
        public string PrimaryKey { get; set; }

        //[DataMember(Order = 6)]
        //public string Id { get; set; }//唯一标识result，修改时使用
    }
}
