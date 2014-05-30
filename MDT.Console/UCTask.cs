using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;

namespace MDT.Console
{
    public partial class UCTask : UserControl
    {
        private ETaskDAL taskDAL;
        private List<ETask> lstTask;
        //private List<ETask> taskList;

        public UCTask()
        {
            InitializeComponent();

            lstTask = new List<ETask>();
            taskDAL = new ETaskDAL();
            splitContainer1.Panel1Collapsed = true;
        }

        /// <summary>
        /// 设置是否允许访问
        /// </summary>
        public bool IsAllowed 
        {
            set 
            {
                if (value == false)
                {
                    tsbtnModify.Enabled = false;
                    tsbtnAdd.Enabled = false;
                    tsbtnDelete.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 加载数据信息
        /// </summary>
        public void LoadData()
        {
            bindDataSource();
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void bindDataSource()
        {
            try
            {
                if (chkEnableTask.Checked)
                    lstTask = taskDAL.GetTasks().Where(c => c.Enable == true).OrderBy(p => p.TaskName).ToList();
                else
                    lstTask = taskDAL.GetTasks().OrderBy(p => p.TaskName).ToList();

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = lstTask;
                grdTask.DataSource = bindingSource;
                bindingNavigator1.BindingSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            FormTaskSet frmTask = new FormTaskSet();
            frmTask.ShowDialog();
            frmTask.Dispose();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (gvTask.SelectedRowsCount > 0 && lstTask.Count > 0)
            {
                DialogResult result = MessageBox.Show("是否删除选中的配置信息！", "信息提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        taskDAL.DeleteObject(lstTask[gvTask.GetFocusedDataSourceRowIndex()]);
                        bindDataSource();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnModify_Click(object sender, EventArgs e)
        {
            if (gvTask.SelectedRowsCount > 0 && lstTask.Count > 0)
            {
                FormTaskEdit form = new FormTaskEdit(lstTask[gvTask.GetFocusedDataSourceRowIndex()]);
                form.ShowDialog();
                form.Dispose();
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bindDataSource();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnQuery_Click(object sender, EventArgs e)
        {
            tsbtnQuery.Checked = !tsbtnQuery.Checked;
            splitContainer1.Panel1Collapsed = !tsbtnQuery.Checked;
            Application.DoEvents();

            if (tsbtnQuery.Checked)
            {
                cboTaskName.Properties.NullText = "请选择......";
                foreach (ETask task in lstTask)
                {
                    cboTaskName.Properties.Items.Add(task.TaskName);
                }
            }
        }

        /// <summary>
        /// 修改任务描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTask_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                taskDAL.ModifyObject(lstTask[gvTask.GetDataSourceRowIndex(e.RowHandle)]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK);
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 停用或启用任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTask_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle > 0 && e.Column.Name == "gcolIsEnable")
            {
                object o = e.CellValue;
                string message = o.ToString() == Boolean.TrueString ? "是否停用选中任务？" : "是否启用选中任务？";

                if (MessageBox.Show(message, "信息提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (o.ToString() == Boolean.TrueString)
                    {
                        lstTask[gvTask.GetDataSourceRowIndex(e.RowHandle)].Enable = false;

                    }
                    else
                    {
                        lstTask[gvTask.GetDataSourceRowIndex(e.RowHandle)].Enable = true;
                    }

                    taskDAL.ModifyObject(lstTask[gvTask.GetDataSourceRowIndex(e.RowHandle)]);
                }
            }
        }

        /// <summary>
        /// 是否显示停用任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCheckEnable_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bindDataSource();
            Cursor.Current = Cursors.Default;
        }

        private void cboTaskName_EditValueChanged(object sender, EventArgs e)
        {
            if (chkTaskName.Checked
                && cboTaskName.Properties.Items.Contains(cboTaskName.EditValue))
            {
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = lstTask.Where(c => c.TaskName == cboTaskName.EditValue.ToString()).ToList();
                grdTask.DataSource = bindingSource;
                bindingNavigator1.BindingSource = bindingSource;
            }
        }
    }
}
