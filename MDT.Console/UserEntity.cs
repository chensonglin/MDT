using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MDT.Console
{
    public class UserEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public static string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public static string Email { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public static string NoWebLastVisitTime { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public static string UserType { get; set; }

        /// <summary>
        /// 上次登陆IP
        /// </summary>
        public static string NoWebVisitIP { get; set; }

        /// <summary>
        /// 上次登陆主机名
        /// </summary>
        public static string HostName { get; set; }

        /// <summary>
        /// 上次登陆物理地址
        /// </summary>
        public static string MacAddress { get; set; }

        /// <summary>
        /// 账户创建时间
        /// </summary>
        public static string CreateTime { get; set; }

        /// <summary>
        /// 当前环境
        /// </summary>
        public static string CurentEnvironment { get; set; }

    }
}
