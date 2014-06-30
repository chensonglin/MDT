using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using DevExpress.XtraGrid;
using MDT.DataConsumer.ServiceContract;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.ServiceContract;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.Console
{
    public partial class UCTaskAllocation : UserControl
    {
        private List<TaskItem> lstTasks;
        private IManageCenterService service;

        public UCTaskAllocation()
        {
            InitializeComponent();

            // 设置背景颜色
            StyleFormatCondition styleCondition = new StyleFormatCondition();
            gvTask.FormatConditions.Add(styleCondition);
            styleCondition.Column = gvTask.Columns["IsAllocation"];
            styleCondition.Condition = FormatConditionEnum.Expression;
            styleCondition.Expression = "[IsAllocation] == true";
            styleCondition.Appearance.BackColor = Color.FromArgb(255, 192, 192);

            lstTasks = new List<TaskItem>();
            service = new ManageCenterService();
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
                    checkEditAll.Enabled = false;
                    sbtnSave.Enabled = false;
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
                // 加载任务列表
                ETaskDAL taskDAL = new ETaskDAL();
                List<ETask> tasks = taskDAL.GetTasks().Where(c => c.Enable == true).OrderBy(p => p.Category).ThenBy(t => t.TaskName).ToList();
                if (tasks != null)
                {
                    foreach (var v in tasks)
                    {
                        lstTasks.Add(new TaskItem
                        {
                            ID = v.ID,
                            TaskName = v.TaskName,
                            Category = v.Category,
                            Note = v.Note
                        });
                    }
                }

                // 加载客户端列表
                EClientDAL clientDAL = new EClientDAL();
                gridClient.DataSource = clientDAL.GetEClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvClient_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                int clientId = Convert.ToInt32(gvClient.GetRowCellValue(e.FocusedRowHandle, "ID").ToString());
                var tasks = service.GetTasks(clientId);
                foreach (var v in lstTasks)
                {
                    if (tasks.Exists(c => c.ID == v.ID))
                        v.IsAllocation = true;
                    else
                        v.IsAllocation = false;
                }
                gridTask.DataSource = null;
                gridTask.DataSource = lstTasks;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (var v in lstTasks)
            {
                v.IsAllocation = checkEditAll.Checked;
            }
            gridTask.DataSource = null;
            gridTask.DataSource = lstTasks;
            Cursor.Current = Cursors.Default;
        }
    
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if (gvClient.SelectedRowsCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                int clientId = Convert.ToInt32(gvClient.GetRowCellValue(gvClient.GetFocusedDataSourceRowIndex(), "ID").ToString());
                List<int> ids = new List<int>();
                List<TaskItem> list = (List<TaskItem>)gvTask.DataSource;
                if (list != null && list.Count > 0)
                {
                    foreach (TaskItem t in list)
                    {
                        if (t.IsAllocation)
                            ids.Add(t.ID);
                    }
                }
                service.AllocateTask(clientId, ids.ToArray());
                MessageBox.Show("保存成功！");
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCleanCache_Click(object sender, EventArgs e)
        {
            if (gvClient.SelectedRowsCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                int clientId = Convert.ToInt32(gvClient.GetRowCellValue(gvClient.GetFocusedDataSourceRowIndex(), "ID").ToString());
                List<int> ids = new List<int>();
                List<TaskItem> list = (List<TaskItem>)gvTask.DataSource;
                if (list != null && list.Count > 0)
                {
                    foreach (TaskItem t in list)
                    {
                        if (t.IsAllocation)
                            ids.Add(t.ID);
                    }
                }
                WcfServiceFactory.Invoke<IDataConsumerService>(client => client.CleanCache(ids), "dataConsumerService");
                MessageBox.Show("清除完成！");
                Cursor.Current = Cursors.Default;
            }
        }
    }

    [DataContract]
    public class TaskItem
    {
        public int ID { get; set; }

        public int ClientId { get;set; }

        public string TaskName { get; set; }

        public string Category { get; set; }

        public string Note { get; set; }

        public bool IsAllocation { get; set; }
    }
}
