using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;

namespace MDT.Console
{
    public partial class UCDatabase : UserControl
    {
        private List<EDatabase> list;
        private EDatabaseDAL databaseDAL;

        public UCDatabase()
        {
            InitializeComponent();

            list = new List<EDatabase>();
            databaseDAL = new EDatabaseDAL();
            //dataGridView1.AutoGenerateColumns = false;
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
        /// 加载事件
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
                list.Clear();
                list.AddRange(databaseDAL.GetDatabases().OrderBy(p => p.Alias));

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = list;

                gridDatabase.DataSource = null;
                gridDatabase.DataSource = bindingSource;

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
        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            FormDatabaseSet frmDatabaseSet = new FormDatabaseSet(new EDatabase(), true);
            frmDatabaseSet.ShowDialog();
            frmDatabaseSet.Dispose();

            bindDataSource();
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void tsbtnModify_Click(object sender, EventArgs e)
        {
            if (gvDatabase.SelectedRowsCount > 0 && list.Count > 0)
            {
                int index = gvDatabase.GetFocusedDataSourceRowIndex();

                EDatabase database = new EDatabase();
                database.ID = list[index].ID;
                database.Alias = list[index].Alias;
                database.DatabaseType = list[index].DatabaseType;
                database.Server = list[index].Server;
                database.Port = list[index].Port;
                database.Database = list[index].Database;
                database.UserId = list[index].UserId;
                database.Password = list[index].Password;

                FormDatabaseSet frmDatabaseSet = new FormDatabaseSet(database, false);
                frmDatabaseSet.ShowDialog();
                frmDatabaseSet.Dispose();

                bindDataSource();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (gvDatabase.SelectedRowsCount > 0 && list.Count > 0)
            {
                DialogResult result = MessageBox.Show("是否删除选中的配置信息！", "信息提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        databaseDAL.DeleteObject(list[gvDatabase.GetFocusedDataSourceRowIndex()]);
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
    }
}
