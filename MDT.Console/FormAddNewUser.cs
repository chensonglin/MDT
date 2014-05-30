using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;
namespace MDT.Console
{
    public partial class FormAddNewUser : Form
    {
        public FormAddNewUser()
        {
            InitializeComponent();
            txtUserName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            chkIsLocked.Checked = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserName.Text))
            {
                lblTip.Text = "用户名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblTip.Text = "密码不能为空";
                return;
            }
            EUserDAL userDal = new EUserDAL();

            if (userDal.CheckUserExsit(txtUserName.Text).ToList<EUser>().Count > 0)
            {
                lblTip.Text = "该用户已经被注册";
                return;
            }

            EUser user = new EUser();
            //user.UserID = userDal.GetKeyValue(); 自增列
            user.UserName = txtUserName.Text;
            user.UserPassword = Utility.SecurityHelper.CryptStringMD5(txtPassword.Text.TrimEnd());
            user.IsLocked = chkIsLocked.Checked ? "0" : "1";
            user.UserType = "MDT";
            user.CreateBy = UserEntity.UserName;
            user.CreateTime = DateTime.Now;
            user.UserEmail = txtMail.Text.TrimEnd();
            try
            {
                userDal.AddObject(user);
            }
            catch (Exception)
            {
                throw;
            }
            lblTip.Text = "用户添加成功！";
            txtUserName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            chkIsLocked.Checked = false;
            DialogResult = DialogResult.OK;
        }
    }
}
