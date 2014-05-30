using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.Configuration;
using ICSharpCode.Setting;
using MDT.ManageCenter.DAL;
using System.Data.EntityClient;

namespace MDT.Console
{
    public partial class FormLogin : Form
    {
        /// <summary>
        /// 是否允许访问
        /// </summary>
        public bool IsAllowed { get; set; }

        public FormLogin()
        {
            InitializeComponent();
            this.txtUserName.Text = String.Empty;
            this.txtPassword.Text = String.Empty;
            dropdownServer.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择服务器列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropdownServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownServer.SelectedIndex >= 0)
            {
                string selectedServerName = dropdownServer.Properties.Items[dropdownServer.SelectedIndex].Value.ToString();
                string selectedConnStrings = ConfigurationManager.ConnectionStrings[selectedServerName].ConnectionString;
                if (!String.IsNullOrEmpty(selectedConnStrings))
                {
                    ConnectionStringSettings connSettings = new ConnectionStringSettings();
                    connSettings.Name = "ManageCenterDBEntities";
                    connSettings.ConnectionString = selectedConnStrings;
                    connSettings.ProviderName = "System.Data.EntityClient";

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.ConnectionStrings.ConnectionStrings.Remove("ManageCenterDBEntities");
                    config.ConnectionStrings.ConnectionStrings.Add(connSettings);
                    config.Save(ConfigurationSaveMode.Modified);

                    ConfigurationManager.RefreshSection("connectionStrings");
                    ConfigSetting.ConnectionStringProtection(false);
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                sbtnLogin_Click(null, null);
        }

        private void sbtnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserName.Text))
            {
                lblTip.Text = "用户名不能为空";
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                lblTip.Text = "密码不能为空";
                return;
            }
            if (dropdownServer.EditValue == null || String.IsNullOrEmpty(dropdownServer.EditValue.ToString()))
            {
                lblTip.Text = "请选择服务器";
                return;
            }
            string errorMsg = String.Empty;
            string cyptString = Utility.SecurityHelper.CryptStringMD5(txtPassword.Text.Trim());
            if (VerifyUserPwd(txtUserName.Text.Trim(), cyptString, dropdownServer.Text, ref errorMsg))
            {
                // 设置用户访问权限
                if (txtUserName.Text.ToLower() == "guest")
                    IsAllowed = false;
                else
                    IsAllowed = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMsg);
                lblTip.Text = String.Format("用户名或密码错误！{0}！", errorMsg);
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// 验证用户与密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="Environment"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool VerifyUserPwd(string userName, string userPassword, string Environment, ref string errorMsg)
        {
            try
            {
                IQueryable<EUser> lstUser = new EUserDAL().VerfiyLogin(userName, userPassword, "MDT");
                if (lstUser.Count<EUser>() > 0)
                {
                    UserEntity.UserID = lstUser.ToList()[0].UserID.ToString();
                    UserEntity.UserName = lstUser.ToList()[0].UserName;
                    UserEntity.UserType = lstUser.ToList()[0].UserType;
                    UserEntity.HostName = lstUser.ToList()[0].HostName;
                    UserEntity.MacAddress = lstUser.ToList()[0].MacAddress;
                    UserEntity.NoWebVisitIP = lstUser.ToList()[0].NoWebVisitIP;
                    if (!String.IsNullOrEmpty(lstUser.ToList()[0].NoWebLastVisitTime.ToString()))
                        UserEntity.NoWebLastVisitTime = lstUser.ToList()[0].NoWebLastVisitTime.ToString();
                    UserEntity.Email = lstUser.ToList()[0].UserEmail.ToString();
                    if (!String.IsNullOrEmpty(lstUser.ToList()[0].CreateTime.ToString()))
                        UserEntity.CreateTime = lstUser.ToList()[0].CreateTime.ToString();
                    UserEntity.CurentEnvironment = Environment;

                    //更新
                    EUser CurrentUser = new EUser();
                    CurrentUser.UserID = Convert.ToInt32(UserEntity.UserID);
                    CurrentUser.HostName = System.Net.Dns.GetHostName();
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(CurrentUser.HostName);
                    CurrentUser.NoWebVisitIP = ipEntry.AddressList[0].ToString();
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    if (nics != null || nics.Length > 0)
                    {
                        CurrentUser.MacAddress = nics[0].GetPhysicalAddress().ToString();
                    }
                    CurrentUser.NoWebLastVisitTime = DateTime.Now.ToLocalTime();

                    new EUserDAL().ModifyEUser(CurrentUser);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
