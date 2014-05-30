using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using MDT.ManageCenter.DataContract;

namespace MDT.ManageCenter.ServiceContract
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IDbSchemaService
    {
        [OperationContract]
        DataTable GetTableInfo(SourceType dataBaseType, string server, int port, string database, string userId, string userPass);

        [OperationContract]
        DataTable GetColumnInfo(SourceType dataBaseType, string server, int port, string database, string userId, string userPass, string tableName);

        [OperationContract]
        string GetDataTableSchema(SourceType dataBaseType, string server, int port, string database, string userId, string userPass, string[] sql, string[] tableNames);

        [OperationContract]
        DataSet GetDataSet(SourceType dataBaseType, string server, int port, string database, string userId, string userPass, string[] sql, string[] tableNames);
    }
}
