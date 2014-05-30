using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;

namespace MDT.Console
{
    public partial class UCTraceLog : UserControl
    {
        private int pageIndex;// 页码
        private int pageCount;// 每页数量
        private int totalCount;// 总数量
        private string predicate;
        private List<object> values;
        private TraceLogDAL traceLogDAL;

        public UCTraceLog()
        {
            InitializeComponent();

            pageCount = 40;
            predicate = String.Empty;
            values = new List<object>();
            traceLogDAL = new TraceLogDAL();

            cboTaskNames.DisplayMember = "TaskName";
            cboTaskNames.ValueMember = "ID";
            splitContainer1.Panel1Collapsed = true;
            dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 加载数据信息
        /// </summary>
        public void LoadData()
        {
            pageIndex = 1;
            bindDataSource(values.ToArray());
        }

        private void UCTraceLogs_Load(object sender, EventArgs e)
        {
            ETaskDAL taskDAL = new ETaskDAL();
            cboTaskNames.DataSource = taskDAL.GetTasks().OrderBy(p => p.TaskName).ToArray();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 设置页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsTxtPageIndex_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tsTxtPageIndex.Text.Trim()))
            {
                pageIndex = 1;
                bindDataSource(values.ToArray());
            }
            else
            {
                int result = 0;
                Int32.TryParse(tsTxtPageIndex.Text.Trim(), out result);

                if (result < 2 || result > totalCount)
                    pageIndex = 1;
                else
                    pageIndex = result;

                bindDataSource(values.ToArray());
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex--;
                bindDataSource(values.ToArray());
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (pageIndex < totalCount)
            {
                pageIndex++;
                bindDataSource(values.ToArray());
            }
        }

        /// <summary>
        /// 绑定数据信息
        /// </summary>
        private void bindDataSource(params object[] v)
        {
            int count;
            int skipCount;

            try
            {
                var traceLogs = from t in traceLogDAL.Read()
                                select new
                                {
                                    ID = t.ID,
                                    ETask_ID = t.ETask_ID,
                                    //ProcessLN = t.ProcessLN,
                                    TaskName = t.ETask.TaskName,
                                    Stage = t.Stage,
                                    State = t.Status,
                                    RunInfo = t.RunInfo,
                                    Data = "<DataMessage>...",
                                    //DataCount = t.DataCount,
                                    StartTime = t.StartTime,
                                    EndTime = t.EndTime
                                };

                if (!String.IsNullOrEmpty(predicate) && values != null && v.Length > 0)
                {
                    traceLogs = traceLogs.Where(predicate, v);
                }

                // 设置总页数
                count = traceLogs.Count();
                totalCount = count / pageCount;
                if ((count % pageCount) != 0)
                {
                    totalCount++;
                }

                // 
                skipCount = (pageIndex == 1) ? 0 : ((pageIndex - 1) * pageCount);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = traceLogs.OrderByDescending(p => p.ID).Skip(skipCount).Take(pageCount);
         
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = bindingSource;

                bindingNavigator1.BindingSource = bindingSource;

                tsTxtPageIndex.Text = pageIndex.ToString();
                tslblInfo.Text = String.Format("共{0}页", totalCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 高级查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnQuery_Click(object sender, EventArgs e)
        {
            predicate = String.Empty;
            values.Clear();

            tsbtnQuery.Checked = !tsbtnQuery.Checked;
            splitContainer1.Panel1Collapsed = !tsbtnQuery.Checked;

            Application.DoEvents();
 
            if (tsbtnQuery.Checked == false)
            {
                chkTaskNames.Checked = false;
                chkState.Checked = false;
                chkDateTime.Checked = false;

                LoadData();
            }
        }

        /// <summary>
        /// 设置错误信息的背景颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
            {
                if (dgvRow.Cells["state"].Value.ToString() == "Failed")
                {
                    dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(225, 192, 192);
                }
                else
                {
                    dgvRow.DefaultCellStyle.BackColor = SystemColors.Window;
                }
            }
        }

        /// <summary>
        /// 重发数据
        /// </summary>
        private void tsmiRepeatData_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvRow = dataGridView1.SelectedRows[0];

                if (dgvRow.Cells["state"].Value.ToString() == "Success")
                {
                    MessageBox.Show("不能进行重发，数据已发送成功！", "信息提示",MessageBoxButtons.OK);
                }
                else if (dgvRow.Cells["data"].Value == null)
                {
                    MessageBox.Show("不能进行重发，数据信息为空！", "信息提示", MessageBoxButtons.OK);
                }
                else
                {
                    int id = Int32.Parse(dgvRow.Cells["id"].Value.ToString());
                    string stage = dgvRow.Cells["stage"].Value.ToString();
                    string processln = dgvRow.Cells["processln"].Value.ToString();

                    if (stage == "DataProducer" || stage == "DataTransform" || stage == "DataConsumer")
                    {
                        try
                        {
                            if (stage == "DataConsumer")
                            {
                                // Modify by wk 2013-1-21
                                //using (DataTransformServiceClient client = new DataTransformServiceClient())
                                //{
                                //    client.ReSend(id);
                                //}
                            }
                            else
                            {
                                // Modify by wk 2013-1-21
                                //using (DataConsumerServiceClient client = new DataConsumerServiceClient())
                                //{
                                //    client.ReSend(id);
                                //}
                            }

                            // 删除此条记录

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == taskname.Index)
                    tsmiTask_Click(null, null);
                else if (e.ColumnIndex == runInfo.Index)
                    tsmiRuninfo_Click(null, null);
                else if (e.ColumnIndex == data.Index)
                    tsmiData_Click(null, null);
            }
        }

        /// <summary>
        /// 任务信息
        /// </summary>
        private void tsmiTask_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ETask task = null;
                ETaskDAL taskDAL = null;

                try
                {
                    int etaskId;
                    Int32.TryParse(dataGridView1.SelectedRows[0].Cells["etask_id"].Value.ToString(), out etaskId);

                    taskDAL = new ETaskDAL();
                    task = taskDAL.GetTasks().Where(p => p.ID == etaskId).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                }

                if (task != null && !String.IsNullOrEmpty(task.Mapping))
                {
                    FormMessage form = new FormMessage();
                    form.Caption = "任务信息";
                    form.Message = task.Mapping;
                    form.ShowDialog();
                    form.Dispose();
                }
            }
        }

        /// <summary>
        /// 运行信息
        /// </summary>
        private void tsmiRuninfo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["runinfo"].Value != null)
                {
                    FormMessage form = new FormMessage();
                    form.Caption = "运行信息";
                    form.Message = dataGridView1.SelectedRows[0].Cells["runinfo"].Value.ToString();
                    form.ShowDialog();
                    form.Dispose();
                }
            }
        }

        /// <summary>
        /// 数据信息
        /// </summary>
        private void tsmiData_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["data"].Value != null)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    int traceLogId;
                    Int32.TryParse(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString(), out traceLogId);

                    var message = from t in traceLogDAL.Read()
                                  where t.ID == traceLogId
                                  select t.Data;

                    FormMessage form = new FormMessage();
                    form.Caption = "数据信息";
                    form.Message = message.SingleOrDefault();
                    form.ShowDialog();
                    form.Dispose();

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void cboTaskNames_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chkTaskNames.Checked)
            {
                chkSearch_Click(sender, e);
            }
        }

        private void rbtnSuccess_CheckedChanged(object sender, EventArgs e)
        {
            if (chkState.Checked)
            {
                chkSearch_Click(sender, e);
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (chkDateTime.Checked)
            {
                if (dtpStartTime.Value <= dtpEndTime.Value)
                {
                    chkSearch_Click(sender, e);
                }
            }
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (chkDateTime.Checked)
            {
                if (dtpEndTime.Value > dtpStartTime.Value)
                {
                    chkSearch_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_Click(object sender, EventArgs e)
        {
            Application.DoEvents();

            predicate = String.Empty;
            values.Clear();

            if (chkTaskNames.Checked || chkState.Checked || chkDateTime.Checked)
            {
                int index = 0;

                if (chkTaskNames.Checked && cboTaskNames.SelectedValue != null)
                {
                    predicate = "ETask_ID = @" + index.ToString();

                    values.Add(cboTaskNames.SelectedValue);

                    index++;
                }

                if (chkState.Checked)
                {
                    if (String.IsNullOrEmpty(predicate))
                        predicate = "State = @0 ";
                    else
                        predicate += String.Format(" and State = @{0}", index);

                    if (rbtnSuccess.Checked)
                        values.Add(rbtnSuccess.Tag.ToString());
                    else
                        values.Add(rbtnFailed.Tag.ToString());

                    index++;
                }

                if (chkDateTime.Checked)
                {
                    if (String.IsNullOrEmpty(predicate))
                        predicate = "StartTime >= @0 and EndTime <= @1 ";
                    else
                        predicate += String.Format(" and StartTime >= @{0} and EndTime <= @{1}", index, index + 1);

                    if (dtpStartTime.Value <= dtpStartTime.Value)
                    {
                        values.Add(dtpStartTime.Value.Date);
                        values.Add(dtpEndTime.Value.Date);
                    }
                }

                pageIndex = 1;
                bindDataSource(values.ToArray());
            }
            else
            {
                LoadData();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string processln = dataGridView1.Rows[e.RowIndex].Cells["processln"].Value.ToString();

                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    if (dgvRow.Cells["processln"].Value.ToString() == processln)
                    {
                        dgvRow.DefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
                    }
                    else
                    {
                        if (dgvRow.Cells["state"].Value.ToString() != "Failed")
                        {
                            dgvRow.DefaultCellStyle.BackColor = SystemColors.Window;
                        }
                        else
                        {
                            dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(225, 192, 192);
                        }
                    }
                }
            }
        }
    }
}
