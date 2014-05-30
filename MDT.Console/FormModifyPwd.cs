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
    public partial class FormModifyPwd : Form
    {
        public FormModifyPwd()
        {
            InitializeComponent();
            txtOldPassword.Text = string.Empty;
            txtNewPwd1.Text = string.Empty;
            txtNewPwd2.Text = string.Empty;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOldPassword.Text))
            {
                lblTip.Text = "请输入旧密码";
                return;
            }
            if (String.IsNullOrEmpty(txtNewPwd1.Text))
            {
                lblTip.Text = "请输入新密码";
                return;
            }
            if (String.IsNullOrEmpty(txtNewPwd2.Text))
            {
                lblTip.Text = "请再次输入新密码";
                return;
            }
            if (txtNewPwd1.Text.Trim() != txtNewPwd2.Text.Trim())
            {
                lblTip.Text = "两次输入的密码不一致";
                return;
            }
            if (txtNewPwd1.Text.Trim() == txtOldPassword.Text.Trim())
            {
                lblTip.Text = "新密码不能与旧密码相同";
                return;
            }
            string strUserName = UserEntity.UserName;
            string strOldPwd = txtOldPassword.Text.Trim();
            strOldPwd = Utility.SecurityHelper.CryptStringMD5(strOldPwd);
            IQueryable<EUser> lstUser = new EUserDAL().VerfiyLogin(strUserName, strOldPwd, "MDT");
            if (lstUser.Count<EUser>() > 0)
            {
                EUser currentUser = new EUser();
                currentUser.UserID = Convert.ToInt32(UserEntity.UserID);
                currentUser.UserPassword = Utility.SecurityHelper.CryptStringMD5(txtNewPwd1.Text.Trim());
                new EUserDAL().ModifyEUser(currentUser);
                lblTip.Text = "密码修改成功";
                txtOldPassword.Text = string.Empty;
                txtNewPwd1.Text = string.Empty;
                txtNewPwd2.Text = string.Empty;
            }
            else
            {
                lblTip.Text = "密码不正确";
            }
        }

        private void txtNewPwd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnOk_Click(null, null);
        }


    }
}
