using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.ServiceModel;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using MDT.Utility;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.ServiceContract;

namespace MDT.ManageCenter.ServiceImplement
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DbSchemaService : IDbSchemaService
    {
        public DataTable GetTableInfo(SourceType type, string server, int port, string database, string userId, string userPass)
        {
            DataTable dt = null;
            string[] restrictionValues;
            string connString = DALUtility.BuildConnString((MySourceType)type, server, port, database, userId, userPass);
            Database db = getDatabase(type, connString);

            if (type == SourceType.Oracle)
                restrictionValues = new string[] { userId.ToUpper(), null };
            else
                restrictionValues = null;

            try
            {
                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    dt = conn.GetSchema("Tables", restrictionValues);
                    conn.Close();
                }
            }
            catch (Exception ex) { throw ex; }

            return dt;
        }

        public DataTable GetColumnInfo(SourceType type, string server, int port, string database, string userId, string userPass, string tableName)
        {
            DataTable dt = null;
            string[] restrictionValues = null;
            string connString = DALUtility.BuildConnString((MySourceType)type, server, port, database, userId, userPass);
            Database db = getDatabase(type, connString);

            if (type == SourceType.SqlServer || type == SourceType.MySql)
            {
                restrictionValues = new string[] { null, null, tableName };
            }
            else if (type == SourceType.Oracle)
            {
                restrictionValues = new string[] { userId.ToUpper(), tableName };
            }

            try
            {
                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    dt = conn.GetSchema("Columns", restrictionValues);
                    conn.Close();
                }
            }
            catch (Exception ex) { throw ex; }

            return dt;
        }

        public string GetDataTableSchema(SourceType type, string server, int port, string database, string userId, string userPass, string[] sql, string[] tableNames)
        {
            int i = 0;
            DataSet ds = new DataSet();
            string connString = DALUtility.BuildConnString((MySourceType)type, server, port, database, userId, userPass);
            Database db = getDatabase(type, connString);
            
            foreach (string s in sql)
            {
                DataSet t = db.ExecuteDataSet(CommandType.Text, s);
                DataTable table = t.Tables[0];
                table.TableName = tableNames[i];
                t.Tables.Remove(table);
                ds.Tables.Add(table);
                i++;
            }

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ds.WriteXmlSchema(sw);
            sw.Flush();
            sw.Close();

            return sb.ToString();
        }

        public DataTable GetDataTableSchema(SourceType type, string server, int port, string database, string userId, string userPass, string sql, string tableName)
        {
            string connString = DALUtility.BuildConnString((MySourceType)type, server, port, database, userId, userPass);
            Database db = getDatabase(type, connString);
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            ds.Tables[0].TableName = tableName;

            return ds.Tables[0];
        }

        public DataSet GetDataSet(SourceType type, string server, int port, string database, string userId, string userPass, string[] sql, string[] tableNames)
        {
            int i = 0;
            DataSet ds = new DataSet();
            string connString = DALUtility.BuildConnString((MySourceType)type, server, port, database, userId, userPass);
            Database db = getDatabase(type, connString);

            foreach (string s in sql)
            {
                DataSet t = db.ExecuteDataSet(CommandType.Text, s);
                DataTable table = t.Tables[0];
                table.TableName = tableNames[i];
                t.Tables.Remove(table);
                ds.Tables.Add(table);
                i++;
            }

            return ds;
        }

        private Database getDatabase(SourceType type, string connString)
        {
            Database db = null;

            switch (type)
            {
                case SourceType.SqlServer:
                    db = new SqlDatabase(connString);
                    break;
                case SourceType.Oracle:
                    db = new OracleDatabase(connString);
                    break;
                case SourceType.MySql:
                    db = new GenericDatabase(connString, DbProviderFactories.GetFactory("MySql.Data.MySqlClient"));
                    break;
                default:
                    throw new Exception("数据类型不支持");
            }

            return db;
        }
    }
}
