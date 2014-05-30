using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;
using DevExpress.XtraGrid;

namespace MDT.Console
{
    public partial class FormTraceLog : Form
    {
        private string traceLogMasterId = String.Empty;

        public FormTraceLog(string id)
        {
            InitializeComponent();

            // 设置背景颜色
            StyleFormatCondition styleCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            styleCondition.Appearance.BackColor = Color.FromArgb(255, 192, 192);
            styleCondition.Appearance.Options.UseBackColor = true;
            styleCondition.Condition = FormatConditionEnum.Expression;
            styleCondition.Expression = "[Status] == 'Failed'";
            gvTraceLog.FormatConditions.Add(styleCondition);

            traceLogMasterId = id;
            //dataGridView1.AutoGenerateColumns = false;
        }

        private void FormTraceLogs_Load(object sender, EventArgs e)
        {
            TraceLogDAL traceLogDAL = new TraceLogDAL();
            gridTraceLog.DataSource = from t in traceLogDAL.Read()
                                      where t.TraceLogMaster_ID == traceLogMasterId
                                      select new
                                      {
                                          ID = t.ID,
                                          ETask_ID = t.ETask_ID,
                                          Stage = t.Stage,
                                          Status = t.Status,
                                          RunInfo = t.RunInfo,
                                          Data = t.Data,
                                          Data2 = "<DataMessage>...",
                                          StartTime = t.StartTime,
                                          EndTime = t.EndTime
                                      };
        }

        private void gvTraceLog_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                ucTextEditor1.Message = gvTraceLog.GetRowCellValue(e.FocusedRowHandle, "Data").ToString();
            }
        }
    }
}
