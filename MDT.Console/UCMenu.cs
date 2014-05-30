using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDT.Console
{
    public partial class UCMenu : UserControl
    {
        public UCMenu()
        {
            InitializeComponent();
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //lblInfo.Text = "日志信息";
            //UCTraceLogMaster ucTraceLogsMaster = new UCTraceLogMaster();
            //ucTraceLogsMaster.LoadData();
            //ucTraceLogsMaster.Dock = DockStyle.Fill;
            //pnlRight.Controls.Clear();
            //pnlRight.Controls.Add(ucTraceLogsMaster);
        }
    }
}
