using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MDT.ManageCenter.DataContract
{
    [Serializable]
    [DataContract]
    public class EColumn
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int Size { get; set; }
    }
}
