using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ManageCenter.DataContract
{
    
    public class EDatabase
    {
        public int ID { get; set; }

        //public int Customer_ID { get; set; }
        public string Alias { get; set; }

        public string DatabaseType { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        public string Database { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }
    }
}
