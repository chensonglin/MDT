using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MDT.Utility
{
    public class DALUtility
    {
        public static object[] GetColumnValues(DataTable dt, int columnIndex)
        {
            int count = dt.Rows.Count;
            object[] objectArr = new object[count];

            for (int i = 0; i < count; i++)
            {
                objectArr[i] = dt.Rows[i][columnIndex];
            }

            return objectArr;
        }

        public static object[] GetColumnValues(DataTable dt, string columnName)
        {
            int index = dt.Columns.IndexOf(columnName);
            if (index == -1)
            {
                throw new Exception("指定列名[" + columnName + "]不存在");
            }
            return GetColumnValues(dt, index);
        }

        public static string GenerateSQLValue(object[] array)
        {
            if (array.Length == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            bool isnum = IsNumber(array[0].GetType().ToString());
            if (isnum)
            {
                foreach (var ob in array)
                {
                    sb.Append(ob).Append(",");
                }
            }
            else
            {
                foreach (var ob in array)
                {
                    sb.Append("'").Append(ob).Append("',");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GenerateSQLValue(DataTable dt, int columnIndex)
        {
            return GenerateSQLValue(GetColumnValues(dt, columnIndex));
        }

        /// <summary>
        /// 构造数据字段
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static string BuildFields(DataTable table, string fieldName, string fieldType)
        {
            string separator = String.Empty;
            StringBuilder sb = new StringBuilder();

            if (IsNumber(fieldType))
                separator = String.Empty;
            else
                separator = "'";

            foreach (DataRow row in table.Rows)
            {
                sb.Append(separator);
                sb.Append(row[fieldName].ToString());
                sb.Append(separator);
                sb.Append(",");
            }

            return sb.ToString().TrimEnd(new char[] { ',' });
        }

        public static bool IsNumber(string type)
        {
            string s = type;
            switch (s)
            {
                case "System.Int32":
                case "System.Int64":
                case "System.Decimal":
                case "System.Int16"://TODO:加类型
                    return true;
                default:
                    return false;
            }
        }

        public static string BuildConnString(MySourceType dataBaseType, string ip,int port, string database, string userId, string userPass)
        {
            //userId = userId.ToUpper();
            string connString = String.Empty;

            switch (dataBaseType)
            {
                case MySourceType.SqlServer:
                    connString = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User Id={2};Password={3};", ip, database, userId, userPass);
                    break;
                case MySourceType.Oracle:
                    connString = String.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={1})));User Id={2};Password={3};", ip, database, userId, userPass);
                    break;
                case MySourceType.MySql:
                    connString = String.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};Max Pool Size=200;", ip, port, database, userId, userPass);
                    break;
            }

            return connString;
        }
    }

    public enum MySourceType
    {
        SqlServer = 1,

        Oracle,

        MySql,

        DB2,

        Sybase,

        Http,

        Tcp
    }
}
