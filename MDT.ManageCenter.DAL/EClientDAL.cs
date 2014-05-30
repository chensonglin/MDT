using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace MDT.ManageCenter.DAL
{
    public class EClientDAL
    {
        public ManageCenterDBEntities _db;

        public EClientDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <returns></returns>
        public List<EClient> GetEClients()
        {
            _db.Refresh(RefreshMode.ClientWins, _db.eclient);
            return _db.eclient.OrderBy(d => d.ID).ToList();
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <param name="id">客户端ID</param>
        /// <returns></returns>
        public EClient GetEClientByID(int id)
        {
            return _db.eclient.SingleOrDefault(d => d.ID == id);
        }

        public List<EClient> GetEClientByName(string name, int id)
        {
            return _db.eclient.Where(d => d.Name == name).Where(d => d.ID != id).ToList();
        }

        public List<EClient> GetEClientByServerIP(string ip, int id)
        {
            return _db.eclient.Where(d => d.ServerIP == ip).Where(d => d.ID != id).ToList();
        }

        public EClient AddObject(EClient eClient)
        {
            _db.AddToeclient(eClient);
            _db.SaveChanges();
            return eClient;
        }

        public void ModifyObject(int id, string name, string serverIP)
        {
            var v = _db.eclient.Where(d => d.ID == id).SingleOrDefault();
            if (v != null)
            {
                v.Name = name;
                v.ServerIP = serverIP;
                _db.SaveChanges();
            }
        }

        public void DeleteObject(EClient eClient)
        {
            _db.DeleteObject(eClient);
            _db.SaveChanges();
        }

    }
}
