using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class ECommand
    {
        //[DataMember]
        //public string Id { get; set; }//唯一标识一个命令，修改时使用

        [DataMember]
        public SourceType SourceType { get; set; }

        [DataMember]
        public string SourceLink { get; set; }

        [DataMember]
        public string CommandName { get; set; }

        [DataMember]
        public string CommandText { get; set; }

        [DataMember]
        public ECommandType CommandType { get; set; }

        [DataMember]
        public List<EParameter> Parameters { get; set; }

        [DataMember]
        public string ParameterValueOjbectName { get; set; }

        [DataMember]
        public string ParameterValue { get; set; }

        [DataMember]
        public string ParameterValueFrom { get; set; }

        [DataMember]
        public bool HasResult { get; set; }
    }
}
