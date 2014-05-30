using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using MDT.Utility;

namespace MDT.DatabaseFactory
{
    public class DbFactory
    {
        public static Database CreateDatabase(MySourceType dbType, string connString)
        {
            //szq modify at 20110913 加密连接字符串
            //if (dbType != MySourceType.Http && dbType != MySourceType.Tcp)
            connString = SecurityHelper.Decode(connString);

            if (dbType == MySourceType.SqlServer)
            {
                return new SqlDatabase(connString);
            }
            else if (dbType == MySourceType.Oracle)
            {
                return new OracleDatabase(connString);
            }
            else
            {
                string providerName = String.Empty;

                if (dbType == MySourceType.MySql)
                {
                    providerName = "MySql.Data.MySqlClient";
                }
                else if (dbType == MySourceType.DB2)
                {
                    providerName = String.Empty;
                }
                else if (dbType == MySourceType.Sybase)
                {
                    providerName = String.Empty;
                }
                else
                {
                    throw new Exception("不支持此数据库类型！");
                }

                return new GenericDatabase(connString, DbProviderFactories.GetFactory(providerName));
            }
        }

    }


}
