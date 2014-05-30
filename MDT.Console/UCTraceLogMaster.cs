using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Data.Objects;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using MDT.ManageCenter.DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace MDT.Console
{
    public partial class UCTraceLogMaster : UserControl
    {
        private int pageIndex;// 页码
        private int pageCount;// 每页数量
        private int totalCount;// 总数量
        private string predicate;
        private List<object> values;
        private ETaskDAL taskDAL;
        private TraceLogDAL traceLogDAL;
        private TraceLogMasterDAL traceLogMasterDAL;
        private List<ETask> lstTask = new List<ETask>();

        public UCTraceLogMaster()
        {
            InitializeComponent();

            // 设置背景颜色
            StyleFormatCondition styleCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            styleCondition.Appearance.BackColor = Color.FromArgb(255, 192, 192);
            styleCondition.Appearance.Options.UseBackColor = true;
            styleCondition.Condition = FormatConditionEnum.Expression;
            styleCondition.Expression = "[Status] == 'Failed'";
            gvTraceLogMaster.FormatConditions.Add(styleCondition);

            pageCount = 40;
            predicate = String.Empty;
            values = new List<object>();
            taskDAL = new ETaskDAL();
            traceLogDAL = new TraceLogDAL();
            traceLogMasterDAL = new TraceLogMasterDAL();
            splitContainer1.Panel1Collapsed = true;
            dateEditStartTime.EditValue = DateTime.Now;
            dateEditEndTime.EditValue = DateTime.Now;
            //dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 加载数据信息
        /// </summary>
        public void LoadData()
        {
            pageIndex = 1;
            bindDataSource(values.ToArray());
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadData();
            Cursor.Current = Cursors.Default;
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
                //var traceLogs = traceLogDAL.GetTraceLogs().Where("data.exist(' for $s in //@id where $s= @value return $s') = 1"
                //                                                 , new ObjectParameter("id", "SKUID")
                //                                                 , new ObjectParameter("value", "010001226910319201545"));

                var traceLogsMaster = from t in traceLogMasterDAL.GetTraceLogs()
                                      select new
                                      {
                                          ID = t.ID,
                                          ETask_ID = t.ETask_ID,
                                          TaskName = t.ETask.TaskName,
                                          Status = t.Status,
                                          Data = "<DataMessage>...",
                                          DataCount = t.DataCount,
                                          StartTime = t.StartTime,
                                          EndTime = t.EndTime,
                                          Note = t.Note
                                      };

                if (!String.IsNullOrEmpty(predicate) && values != null && v.Length > 0)
                {
                    traceLogsMaster = traceLogsMaster.Where(predicate, v);
                }

                // 设置总页数
                count = traceLogsMaster.Count();
                totalCount = count / pageCount;
                if ((count % pageCount) != 0)
                {
                    totalCount++;
                }
                skipCount = (pageIndex == 1) ? 0 : ((pageIndex - 1) * pageCount);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = traceLogsMaster.OrderByDescending(p => p.StartTime).Skip(skipCount).Take(pageCount);

                //dataGridView1.DataSource = null;
                //dataGridView1.DataSource = bindingSource;
                gridTraceLogMaster.DataSource = null;
                gridTraceLogMaster.DataSource = bindingSource;
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
                chkTaskName.Checked = false;
                chkStatus.Checked = false;
                chkDateTime.Checked = false;
                LoadData();
            }
            else
            {
                grdPopTask.DataSource = taskDAL.GetTasks().Where(c => c.Enable == true).OrderBy(p => p.TaskName).ToList();
            }
        }

        /// <summary>
        /// 重发数据
        /// </summary>
        private void tsmiRepeatData_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow dgvRow = dataGridView1.SelectedRows[0];

            //    if (dgvRow.Cells["state"].Value.ToString() == "Success")
            //    {
            //        MessageBox.Show("不能进行重发，数据已发送成功！", "信息提示", MessageBoxButtons.OK);
            //    }
            //    else
            //    {
            //        Cursor.Current = Cursors.WaitCursor;

            //        try
            //        {
            //            //int traceLogMasterId = Int32.Parse(dgvRow.Cells["id"].Value.ToString());
            //            string traceLogMasterId = dgvRow.Cells["id"].Value.ToString();

            //            var traceLogTemp = (from t in traceLogDAL.Read()
            //                                where t.TraceLogMaster_ID == traceLogMasterId && t.Status == "Failed"
            //                                select t
            //                            ).FirstOrDefault();

            //            //szq modify at 20111021 从生产节点重发数据
            //            if (traceLogTemp != null)
            //            {
            //                TraceLog traceLog = null;
            //                if (traceLogTemp.Stage != "DataProducer")
            //                {
            //                    //traceLog = (from t in traceLogDAL.Read()
            //                    //            where t.ProcessLN == traceLogTemp.ProcessLN && t.TraceLogMaster_ID == traceLogTemp.TraceLogMaster_ID && t.Stage == "DataProducer"
            //                    //            select t
            //                    //            ).FirstOrDefault();
            //                    traceLog = (from t in traceLogDAL.Read()
            //                                where t.TraceLogMaster_ID == traceLogTemp.TraceLogMaster_ID && t.Stage == "DataProducer"
            //                                select t
            //                                ).FirstOrDefault();
            //                }
            //                else
            //                {
            //                    traceLog = traceLogTemp;
            //                }
            //                if (traceLog != null && !String.IsNullOrEmpty(traceLog.Data))
            //                {
            //                    //using (DataTransformServiceClient client = new DataTransformServiceClient())
            //                    //{
            //                    //    client.ReSend(traceLog.ID);
            //                    //}
            //                }

            //                // 保存处理状态
            //                var traceLogMaster = (from t in traceLogMasterDAL.GetTraceLogs()
            //                                      where t.ID == traceLogMasterId
            //                                      select t).FirstOrDefault();

            //                traceLogMaster.Note = "手动触发";
            //                traceLogMasterDAL.Update(traceLogMaster);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            //        }

            //        Cursor.Current = Cursors.Default;
            //    }
            //}
        }

        /// <summary>
        /// 选择开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEditStartTime_EditValueChanged(object sender, EventArgs e)
        {
            if (chkDateTime.Checked)
            {
                DateTime dtStart = DateTime.Parse(dateEditStartTime.EditValue.ToString());
                DateTime dtEnd = DateTime.Parse(dateEditEndTime.EditValue.ToString());
                if (dtStart.Date <= dtEnd.Date)
                {
                    chkSearch_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// 选择结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateEditEndTime_EditValueChanged(object sender, EventArgs e)
        {
            if (chkDateTime.Checked)
            {
                DateTime dtStart = DateTime.Parse(dateEditStartTime.EditValue.ToString());
                DateTime dtEnd = DateTime.Parse(dateEditEndTime.EditValue.ToString());
                if (dtEnd.Date >= dtStart.Date)
                {
                    chkSearch_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// 选择任务名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTaskName_CheckedChanged(object sender, EventArgs e)
        {
            chkSearch_Click(sender, e);
        }

        /// <summary>
        /// 任务名称列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPopTask_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                string strCode = gvPopTask.GetFocusedRowCellValue("ID").ToString();
                string strName = gvPopTask.GetFocusedRowCellValue("TaskName").ToString();
                popTaskChoose.EditValue = strName;
                popTaskChoose.ToolTip = strCode;
                popTaskChoose.ClosePopup();

                if (chkTaskName.Checked)
                {
                    chkSearch_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// 选择执行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            chkSearch_Click(sender, e);
        }

        /// <summary>
        /// 选择执行状态（成功或失败）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnStatus_EditValueChanged(object sender, EventArgs e)
        {
            chkSearch_Click(sender, e);
        }

        /// <summary>
        /// 选择开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDateTime_CheckedChanged(object sender, EventArgs e)
        {
            chkSearch_Click(sender, e);
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

            if (chkTaskName.Checked || chkStatus.Checked || chkDateTime.Checked)
            {
                int index = 0;

                // 选择任务名称
                if (chkTaskName.Checked && !String.IsNullOrEmpty(popTaskChoose.ToolTip))
                {
                    predicate = "ETask_ID = @" + index.ToString();
                    values.Add(Convert.ToInt32(popTaskChoose.ToolTip));
                    index++;
                }

                // 选择执行状态
                if (chkStatus.Checked)
                {
                    if (String.IsNullOrEmpty(predicate))
                        predicate = "Status = @0 ";
                    else
                        predicate += String.Format(" and Status = @{0}", index);

                    if (String.IsNullOrEmpty(predicate))
                        predicate = "Status = @0 ";
                    else
                        predicate += String.Format(" and Status = @{0}", index);

                    values.Add(rbtnStatus.EditValue.ToString());
                    index++;
                }

                // 选择传输日期
                if (chkDateTime.Checked)
                {
                    if (String.IsNullOrEmpty(predicate))
                        predicate = "EndTime >= @0 and EndTime <= @1 ";
                    else
                        predicate += String.Format(" and EndTime >= @{0} and EndTime <= @{1}", index, index + 1);

                    DateTime dtStart = DateTime.Parse(dateEditStartTime.EditValue.ToString());
                    DateTime dtEnd = DateTime.Parse(dateEditEndTime.EditValue.ToString());
                    if (dtStart.Date <= dtEnd.Date)
                    {
                        values.Add(dtStart.Date);
                        values.Add(dtEnd.Date.AddDays(1));
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

        /// <summary>
        /// 单击快速查询错误信息与日志信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTraceLogMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks >= 2)
            {
                if (e.Column.Name == data.Name)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    tsmiData_Click(sender, new EventArgs());
                    Cursor.Current = Cursors.Default;
                }
                else if (e.Column.Name == status.Name)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    tsmiErrorMsg_Click(sender, new EventArgs());
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 数据信息
        /// </summary>
        private void tsmiData_Click(object sender, EventArgs e)
        {
            if (gvTraceLogMaster.SelectedRowsCount > 0)
            {
                try
                {
                    string traceLogMasterId = gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), "ID").ToString();
                    TraceLog traceLog = (from t in traceLogDAL.Read()
                                         where t.TraceLogMaster_ID == traceLogMasterId && t.Stage == "DataProducer"
                                         select t).FirstOrDefault();
                    if (traceLog != null && !String.IsNullOrEmpty(traceLog.Data))
                    {
                        showFormMessage("数据信息", traceLog.Data);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        private void tsmiErrorMsg_Click(object sender, EventArgs e)
        {
            if (gvTraceLogMaster.SelectedRowsCount > 0)
            {
                if (gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), "Status").ToString() == "Failed")
                {
                    try
                    {
                        string traceLogMasterId = gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), "ID").ToString();
                        TraceLog traceLog = (from t in traceLogDAL.Read()
                                             where t.TraceLogMaster_ID == traceLogMasterId && t.Status == "Failed"
                                             select t).OrderByDescending(per => per.ID).FirstOrDefault();
                        if (traceLog != null && !String.IsNullOrEmpty(traceLog.RunInfo))
                        {
                            showFormMessage("错误信息", traceLog.RunInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                    }
                }
            }
        }

        /// <summary>
        ///  查看日志明细
        /// </summary>
        private void tsmiTraceLog_Click(object sender, EventArgs e)
        {
            if (gvTraceLogMaster.SelectedRowsCount > 0)
            {
                string id = gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), "ID").ToString();
                FormTraceLog frmTraceLogs = new FormTraceLog(id);
                frmTraceLogs.ShowDialog();
                frmTraceLogs.Dispose();
                frmTraceLogs.Close();
            }
        }

        /// <summary>
        /// 任务信息-Mapping
        /// </summary>
        private void tsmiMapping_Click(object sender, EventArgs e)
        {
            if (gvTraceLogMaster.SelectedRowsCount > 0)
            {
                string taskId = gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), etask_id).ToString();
                ETask task = getETask(taskId);
                if (task != null)
                {
                    showFormMessage("任务信息 - Mapping", task.Mapping);
                }
            }
        }

        /// <summary>
        /// 任务信息-ESourceConfig
        /// </summary>
        private void tsmiESourceConfig_Click(object sender, EventArgs e)
        {
            if (gvTraceLogMaster.SelectedRowsCount > 0)
            {
                string taskId = gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), etask_id).ToString();
                ETask task = getETask(taskId);
                if (task != null)
                {
                    showFormMessage("任务信息 - ESourceConfig", task.SourceESchema.ESource.SourceConfig);
                }
            }
        }

        /// <summary>
        /// 任务信息-TSourceConfig
        /// </summary>
        private void tsmiTSourceConfig_Click(object sender, EventArgs e)
        {
            if (gvTraceLogMaster.SelectedRowsCount > 0)
            {
                string taskId = gvTraceLogMaster.GetRowCellValue(gvTraceLogMaster.GetFocusedDataSourceRowIndex(), etask_id).ToString();
                ETask task = getETask(taskId);
                if (task != null)
                {
                    showFormMessage("任务信息 - TSourceConfig", task.TargetESchema.ESource.SourceConfig);
                }
            }
        }

        private ETask getETask(string taskId)
        {
            ETask task = null;
            ETaskDAL taskDAL = null;

            try
            {
                int id = Convert.ToInt32(taskId);
                taskDAL = new ETaskDAL();
                task = taskDAL.GetTasks().Where(p => p.ID == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }

            return task;
        }

        private void showFormMessage(string caption, string message)
        {
            FormMessage form = new FormMessage();
            form.Caption = caption;
            form.Message = message;
            form.ShowDialog();
            form.Dispose();
        }

        
    }
}
