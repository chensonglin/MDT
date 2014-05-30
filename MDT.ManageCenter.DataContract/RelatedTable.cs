using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class RelatedTable : Table
    {
        //[DataMember]
        //public ColumnInfo LocalColumn { get; set; }

        [DataMember]
        public EColumn ForeignColumn { get; set; }
    }
}
