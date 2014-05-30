using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class EParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        public string Value { get; set; }
    }
}
