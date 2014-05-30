using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using MDT.ManageCenter.DataContract;
using MDT.Utility;

namespace MDT.ManageCenter.DAL
{
    public class EUserDAL
    {
        public ManageCenterDBEntities _db;

        public EUserDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        /// <summary>
        /// 根据用户类型查询用户列表 ADM为公共用户
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IQueryable<EUser> GetUserByUserType(string userType)
        {
            return _db.euser.Where((c => (c.UserType == "ADM") || (c.UserType == userType)));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="task"></param>
        public void DeleteObject(EUser user)
        {
            _db.euser.DeleteObject(user);
            _db.SaveChanges();
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="userType">用户类型 ADM为公共用户 MDT 为控制台用户  WEB为网页用户</param>
        /// <returns></returns>
        public IQueryable<EUser> VerfiyLogin(string userName, string password, string userType)
        {
            return _db.euser.Where(c => c.UserName == userName).Where(c => c.UserPassword == password).Where(c => c.IsLocked == "1").Where(c => (c.UserType == userType) || (c.UserType == "ADM"));
        }

        /// <summary>
        /// 检查用户是否已经存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IQueryable<EUser> CheckUserExsit(string userName)
        {
            return _db.euser.Where(c => c.UserName == userName);
        }

        /// <summary>
        /// 返回主键列
        /// </summary>
        /// <returns></returns>
        public Int32 GetKeyValue()
        {
            IQueryable<Int32> lstUserID = _db.euser.Select(c => c.UserID);
            return lstUserID.Max() + 1;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="eUser"></param>
        public EUser AddObject(EUser eUser)
        {
            _db.AddToeuser(eUser);
            _db.SaveChanges();
            return eUser;
        }

        public void ModifyEUser(EUser eUser)
        {
            var v = _db.euser.Where(p => p.UserID == eUser.UserID).SingleOrDefault();

            if (v != null)
            {
                if (eUser.LoginTime != null)
                    v.LoginTime = eUser.LoginTime;
                if (!String.IsNullOrEmpty(eUser.LoginIP))
                    v.LoginIP = eUser.LoginIP;
                //MDT
                if (!String.IsNullOrEmpty(eUser.HostName))
                    v.HostName = eUser.HostName;
                if (!String.IsNullOrEmpty(eUser.NoWebVisitIP))
                    v.NoWebVisitIP = eUser.NoWebVisitIP;
                if (!String.IsNullOrEmpty(eUser.MacAddress))
                    v.MacAddress = eUser.MacAddress;
                if (eUser.NoWebLastVisitTime != null)
                    v.NoWebLastVisitTime = eUser.NoWebLastVisitTime;
                if (!String.IsNullOrEmpty(eUser.UserPassword))
                    v.UserPassword = eUser.UserPassword;
                if (!String.IsNullOrEmpty(eUser.IsLocked))
                    v.IsLocked = eUser.IsLocked;
                _db.SaveChanges();
            }
        }
    }
}
