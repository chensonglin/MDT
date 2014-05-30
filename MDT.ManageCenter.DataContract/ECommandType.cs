using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [DataContract]
    public enum ECommandType
    {
        [EnumMember]
        Text,
        [EnumMember]
        TableDirect,
        [EnumMember]
        StoredProcedure,
        [EnumMember]
        Get,
        [EnumMember]
        Post
    }
}
