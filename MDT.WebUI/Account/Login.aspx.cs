using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDT.ManageCenter.DAL;
using System.Text;

namespace MDT.WebUI.Account
{
    public partial class Login : System.Web.UI.Page
    {
        EUserDAL eUserDAL;
        string IP = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            IP = Request.UserHostAddress;
            eUserDAL = new EUserDAL();
            if (!Page.IsPostBack)
            {
                this.txtUserName.Text = getCookie("LoginName");
                this.txtPwd.Attributes.Add("value", getCookie("LoginPwd"));
                if (txtUserName.Text.Trim() != "" && getCookie("LoginPwd") != "")
                {
                    this.ckKeepLoined.Checked = true;
                }
            }
        }

        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        private bool setCookie(string strName, string strValue, int strDay)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        private string getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        private bool delCookie(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strAccount = this.txtUserName.Text.Trim();
            string strPassword = this.txtPwd.Text.Trim();
            bool chkValue = this.ckKeepLoined.Checked;
            if (String.IsNullOrEmpty(strAccount))
            {
                txtUserName.Focus();
                txtPwd.Text = "";
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('用户名不能为空！');</script>", false);
            }
            else if (String.IsNullOrEmpty(strPassword))
            {
                txtPwd.Focus();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('密码不能为空！');</script>", false);
            }
            else
            {
                try
                {
                    var dtLogin = eUserDAL.VerfiyLogin(strAccount, CryptStringMD5(strPassword), "WEB");
                    EUser[] listUser = dtLogin.ToArray<EUser>();

                    if (listUser.Length == 0)
                    {
                        txtPwd.Text = "";
                        txtPwd.Focus();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('用户名或密码错误，请重新输入！');</script>", false);
                    }
                    else
                    {
                        //修改用户登录信息
                        EUser eUser = listUser[0];
                        eUser.LoginTime = DateTime.Now;
                        eUser.LoginIP = IP;
                        eUserDAL.ModifyEUser(eUser);

                        StringBuilder sbRole = new StringBuilder("");
                        EUserRole[] listUser_Role = eUser.EUserRole.ToArray<EUserRole>();
                        if (listUser_Role.Length == 0)
                        {
                            throw new Exception("没有权限访问");
                        }
                        for (int i = 0; i < listUser.Count(); i++)
                        {
                            EUserRole user_role = listUser_Role[i];
                            sbRole.Append(user_role.ERole.RoleType + "*");
                        }
                        string strRole = sbRole.ToString().TrimEnd('*');
                        //加入表单认证
                        string strUserInfo = string.Empty;
                        strUserInfo = "MDT2.0|" + eUser.UserID + "|" + eUser.UserName + "|" + eUser.UserPassword + "|" + strRole;
                        //表单认证的格式：MDT2.0|ID|LoginName|name
                        System.Web.Security.FormsAuthentication.SetAuthCookie(strUserInfo, false);

                        //判断是否选择记录用户密码
                        if (chkValue == true)
                        {
                            setCookie("LoginName", strAccount, 30);		//登录用户名
                            setCookie("LoginPwd", strPassword, 30);		//登录密码
                        }
                        else
                        {
                            delCookie("LoginName");		//删除记录的COOKIES信息
                            delCookie("LoginPwd");
                        }
                        Response.Redirect("../Management/Log/LogMasterBrowser.aspx");
                    }
                }
                catch (Exception ex)
                {
                    txtUserName.Text = "";
                    txtUserName.Focus();
                    txtPwd.Text = "";
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message + "');</script>", false);
                }
            }
        }


        private string CryptStringMD5(string str)
        {
            string pwd = "";
            //实例化一个md5对像
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}
