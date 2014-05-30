using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDT.ManageCenter.DAL;
using System.Text.RegularExpressions;

namespace MDT.WebUI.Account
{
    public partial class Register : System.Web.UI.Page
    {
        EUserDAL eUserDAL;
        protected void Page_Load(object sender, EventArgs e)
        {

            //RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        //protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        //{
        //    FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

        //    string continueUrl = RegisterUser.ContinueDestinationPageUrl;
        //    if (String.IsNullOrEmpty(continueUrl))
        //    {
        //        continueUrl = "~/";
        //    }
        //    Response.Redirect(continueUrl);
        //}

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            eUserDAL = new EUserDAL();
            string strAccount = this.txtUserName.Text.Trim();
            string strPassword = this.txtPwd.Text.Trim();
            string strRePassword = this.txtRePwd.Text.Trim();
            string strEmail = this.txtUserEmail.Text.Trim();
            if (strAccount == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请输入用户名！');</script>");
                return;
            }
            if (strPassword == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请输入密码！');</script>");
                return;
            }
            if (strRePassword == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请输入确认密码！');</script>");
                return;
            }
            if (GetStringBytelength(strAccount) > 50)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('用户名过长，长度不能超过50个字节！');</script>");
                return;
            }
            if (strPassword.Length<6 || GetStringBytelength(strPassword) >50)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('密码不能少于6个字，大于50个字节！');</script>");
                return;
            }
            if (strEmail != "" && !IsValidEmail(strEmail))
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('输入的电子邮件格式不正确！');</script>");
                return;
            }
            if (strPassword != strRePassword)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('密码和确认密码不一致，请重新输入！');</script>");
                return;
            }
            try
            {
                EUser eUser = new EUser();
                eUser.UserName = strAccount;
                eUser.UserPassword = strPassword;
                eUser.UserEmail = strEmail;
                var user = eUserDAL.AddObject(eUser);
                if (user == null)
                {
                    txtUserName.Text = "";
                    txtUserName.Focus();
                    txtPwd.Text = "";
                    txtRePwd.Text = "";
                    txtUserEmail.Text = "";
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('注册失败！');</script>");
                    return;
                }
            }
            catch (Exception ex)
            {
                txtUserName.Text = "";
                txtUserName.Focus();
                txtPwd.Text = "";
                txtRePwd.Text = "";
                txtUserEmail.Text = "";
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message + "！');</script>");
                return;
            }
            Response.Redirect("Login.aspx");
        }

        /// <summary>
        ///功　　能：判断指定字符串是否为有效电子邮件格式
        ///输入参数：string strIn，指定字符串
        ///输出参数：bool，是否为有效电子邮件格式
        /// </summary>
        /// <param name="strIn">指定字符串</param>
        public bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        ///功　　能：返回的指定字符串的内容的字节数。
        ///输入参数：string cOriginalityString，指定字符串的内容
        ///输出参数：int iReturnBytelength，返回的字符串的内容的字节数。
        /// </summary>
        public int GetStringBytelength(string cOriginalityString)
        {
            int iReturnBytelength = 0;
            iReturnBytelength = System.Text.ASCIIEncoding.Default.GetByteCount(cOriginalityString);		//此方法区分汉字，一个汉字算2
            return iReturnBytelength;
        }
    }
}
