using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public enum SourceType
    {
        [EnumMember]
        SqlServer = 1,
        [EnumMember]
        Oracle,
        [EnumMember]
        MySql,
        [EnumMember]
        DB2,
        [EnumMember]
        Sybase,
        [EnumMember]
        Http,
        [EnumMember]
        Tcp,
        [EnumMember]
        DLL
    }
}
