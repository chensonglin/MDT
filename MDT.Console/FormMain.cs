using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDT.Console
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 是否允许访问
        /// </summary>
        public bool IsAllowed { get; set; }

        public FormMain()
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(UserEntity.NoWebLastVisitTime))
            {
                statusLblLastVistTime.Text = "上次登陆时间：" + UserEntity.NoWebLastVisitTime;
            }
            if (!String.IsNullOrEmpty(UserEntity.UserName))
            {
                statusLblUserName.Text = "当前用户：" + UserEntity.UserName;
            }
            this.Text = String.Format("{0}: 【{1}】", this.Text, UserEntity.CurentEnvironment);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            pnlRight.Controls.Clear();
            lblTip.Text = "日志信息";

            UCTraceLogMaster ucTraceLogsMaster = new UCTraceLogMaster();
            ucTraceLogsMaster.LoadData();
            ucTraceLogsMaster.Dock = DockStyle.Fill;
            pnlRight.Controls.Add(ucTraceLogsMaster);

            Cursor.Current = Cursors.Default;
        }

        private void addUserControl(string text)
        {
            switch (text)
            {
                case "日志信息":
                    lblTip.Text = "日志信息";
                    UCTraceLogMaster ucTraceLogsMaster = new UCTraceLogMaster();
                    ucTraceLogsMaster.LoadData();
                    ucTraceLogsMaster.Dock = DockStyle.Fill;
                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(ucTraceLogsMaster);
                    break;
                case "数据源配置":
                    lblTip.Text = "数据源配置";
                    UCDatabase ucDatabase = new UCDatabase();
                    ucDatabase.IsAllowed = IsAllowed;
                    ucDatabase.LoadData();
                    ucDatabase.Dock = DockStyle.Fill;
                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(ucDatabase);
                    break;
                case "任务配置":
                    lblTip.Text = "任务配置";
                    UCTask ucTask = new UCTask();
                    ucTask.IsAllowed = IsAllowed;
                    ucTask.LoadData();
                    ucTask.Dock = DockStyle.Fill;
                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(ucTask);
                    break;
                case "任务分配":
                    lblTip.Text = "任务分配";
                    UCTaskAllocation ucTaskAllocation = new UCTaskAllocation();
                    ucTaskAllocation.IsAllowed = IsAllowed;
                    ucTaskAllocation.LoadData();
                    ucTaskAllocation.Dock = DockStyle.Fill;
                    pnlRight.Controls.Clear(); 
                    pnlRight.Controls.Add(ucTaskAllocation);
                    break;
                case "用户中心":
                    lblTip.Text = "用户中心";
                    UCUserList userList = new UCUserList();
                    userList.Dock = DockStyle.Fill;
                    pnlRight.Controls.Clear();
                    pnlRight.Controls.Add(userList);
                    break;
                case "修改密码":
                    using (FormModifyPwd frm = new FormModifyPwd())
                    {
                        frm.ShowDialog();
                    }
                    break;

            }
        }

        private void nbi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;
            addUserControl(e.Link.Caption);
            Cursor.Current = Cursors.Default;
        }


        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupInfo_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
