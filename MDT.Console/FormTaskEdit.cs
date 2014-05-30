using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;
using MDT.Utility;
using MDT.ManageCenter.DataContract;

namespace MDT.Console
{
    public partial class FormTaskEdit : Form
    {
        private ETask task;
        private UCTextEditor uc;

        public FormTaskEdit(ETask task)
        {
            InitializeComponent();

            this.task = task;
        }

        private void FormTaskSet2_Load(object sender, EventArgs e)
        {
            uc = new UCTextEditor();
            uc.Dock = DockStyle.Fill;
            panel1.Controls.Add(uc);
            rbtnTaskInfo.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择任务信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnTaskInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnTaskInfo.SelectedIndex == 0)
                uc.Message = task.Mapping;
            else if (rbtnTaskInfo.SelectedIndex == 1)
                uc.Message = task.XSLTInfo;
            else if (rbtnTaskInfo.SelectedIndex == 2)
                uc.Message = task.SourceESchema.ESource.SourceConfig;
            else if (rbtnTaskInfo.SelectedIndex == 3)
                uc.Message = task.TargetESchema.ESource.SourceConfig;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(uc.Message))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (rbtnTaskInfo.SelectedIndex == 0)
                    {
                        ETaskDAL dal = new ETaskDAL();
                        dal.ModifyObject(task.ID, uc.Message, task.XSLTInfo);
                        task.Mapping = uc.Message;
                    }
                    else if (rbtnTaskInfo.SelectedIndex == 1)
                    {
                        ETaskDAL dal = new ETaskDAL();
                        dal.ModifyObject(task.ID, task.Mapping, uc.Message);
                        task.XSLTInfo = uc.Message;
                    }
                    else if (rbtnTaskInfo.SelectedIndex == 2)
                    {
                        Source s = CommonUtility.UnserializeXml<Source>(uc.Message);
                        if (s != null)
                        {
                            ESourceDAL dal = new ESourceDAL();
                            dal.ModifyObject(task.SourceESchema.ESource.ID, uc.Message);
                            task.SourceESchema.ESource.SourceConfig = uc.Message;
                        }
                    }
                    else if (rbtnTaskInfo.SelectedIndex == 3)
                    {
                        Source s = CommonUtility.UnserializeXml<Source>(uc.Message);
                        if (s != null)
                        {
                            ESourceDAL dal = new ESourceDAL();
                            dal.ModifyObject(task.TargetESchema.ESource.ID, uc.Message);
                            task.TargetESchema.ESource.SourceConfig = uc.Message;
                        }
                    }
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("保存成功！", "信息提示", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                }
            }
        }
       
    }
}
