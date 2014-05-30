using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Data.Objects;
using MDT.ManageCenter.DataContract;
using MDT.Utility;
using System.Text.RegularExpressions;

namespace MDT.ManageCenter.DAL
{
    public class ESourceDAL
    {
        private ManageCenterDBEntities _db;

        public ESourceDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="sourceConfig"></param>
        public void ModifyObject(int sourceId, string sourceConfig)
        {
            var v = _db.esource.Where(p => p.ID == sourceId).SingleOrDefault();
            if (v != null)
            {
                v.SourceConfig = sourceConfig;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="prevdb"></param>
        /// <param name="db"></param>
        public void ModifyObject(EDatabase prevdb, EDatabase db)
        {
            string prevConnString = DALUtility.BuildConnString((MySourceType)Enum.Parse(typeof(MySourceType)
                                                        , prevdb.DatabaseType)
                                                        , prevdb.Server, prevdb.Port
                                                        , prevdb.Database, prevdb.UserId, prevdb.Password);
            string connString = DALUtility.BuildConnString((MySourceType)Enum.Parse(typeof(MySourceType)
                                                        , db.DatabaseType)
                                                        , db.Server, db.Port
                                                        , db.Database, db.UserId, db.Password);
            if (prevConnString == connString)
            {
                return;
            }
            else
            {
                XmlNodeList nodeList = null;
                XmlDocument doc = new XmlDocument();
                //szq modify at 20110922 加密连接字符串
                string strText = prevConnString; // 明文
                connString = Utility.SecurityHelper.Encode(connString);
                prevConnString = Utility.SecurityHelper.Encode(prevConnString);

                foreach (ESource source in _db.esource)
                {
                    doc.LoadXml(source.SourceConfig);
                    // 修改数据库连接字符串
                    nodeList = doc.GetElementsByTagName("SourceLink");
                    foreach (XmlNode node in nodeList)
                    {
                        if (node.InnerText == prevConnString || node.InnerText == strText)
                        {
                            node.InnerText = connString;
                            // 判断数据库类型是否改变
                            if (prevdb.DatabaseType == Enum.GetName(typeof(MySourceType), MySourceType.Oracle)
                                && db.DatabaseType == Enum.GetName(typeof(MySourceType), MySourceType.MySql))
                            {
                                // 修改执行命令
                                string commandTextStr = node.ParentNode["CommandText"].InnerText.Replace(':', '?').ToLower();
                                commandTextStr = commandTextStr.Replace("where rownum <=", "limit");
                                commandTextStr = commandTextStr.Replace("and rownum <=", "limit");
                                commandTextStr = commandTextStr.Replace("rownum <=", "limit");
                                node.ParentNode["CommandText"].InnerText = commandTextStr;
                                // 修改数据库类型
                                node.ParentNode["SourceType"].InnerText = db.DatabaseType;
                            }
                            else if (prevdb.DatabaseType == Enum.GetName(typeof(MySourceType), MySourceType.MySql)
                                && db.DatabaseType == Enum.GetName(typeof(MySourceType), MySourceType.Oracle))
                            {
                                // 修改执行命令
                                string commandTextStr = node.ParentNode["CommandText"].InnerText.Replace('?', ':').ToLower();
                                if (commandTextStr.Contains("where"))
                                    commandTextStr = commandTextStr.Replace("limit", "and rownum <=");
                                else
                                    commandTextStr = commandTextStr.Replace("limit", "where rownum <=");
                                node.ParentNode["CommandText"].InnerText = commandTextStr;
                                // 修改数据库类型
                                node.ParentNode["SourceType"].InnerText = db.DatabaseType;
                            }
                        }
                    }

                    source.SourceConfig = doc.OuterXml;
                }

                _db.SaveChanges();
            }
        }
    }
}
