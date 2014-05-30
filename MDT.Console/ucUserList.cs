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
    public partial class UCUserList : UserControl
    {
        private EUserDAL userDAL;
        private List<EUser> userList;

        public UCUserList()
        {
            InitializeComponent();
            userDAL = new EUserDAL();
        }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void bindDataSource()
        {
            try
            {
                userList = userDAL.GetUserByUserType("MDT").ToList<EUser>();
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = userList;
                grdUserList.DataSource = bindingSource;
                bingdingUser.BindingSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }
        }

        private void ucUserList_Load(object sender, EventArgs e)
        {
            bindDataSource();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (gvUserList.SelectedRowsCount > 0 && userList.Count > 0)
            {
                DialogResult result = MessageBox.Show("是否删除所选的用户！", "信息提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        userDAL.DeleteObject(userList[gvUserList.GetFocusedDataSourceRowIndex()]);
                        bindDataSource();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void gvUserList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.Name == "gcolIsLocked")
            {
                object o = e.CellValue;
                string message = o.ToString() == "0" ? "是否锁定所选的用户？" : "是否启用所选的用户？";

                if (MessageBox.Show(message, "信息提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (o.ToString() == "1")
                    {
                        userList[gvUserList.GetDataSourceRowIndex(e.RowHandle)].IsLocked = "0";

                    }
                    else
                    {
                        userList[gvUserList.GetDataSourceRowIndex(e.RowHandle)].IsLocked = "1";
                    }

                    userDAL.ModifyEUser(userList[gvUserList.GetDataSourceRowIndex(e.RowHandle)]);
                }
            }
        }

        private void gvUserList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                userDAL.ModifyEUser(userList[gvUserList.GetDataSourceRowIndex(e.RowHandle)]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK);
            }
            Cursor.Current = Cursors.Default;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            using (FormAddNewUser frm = new FormAddNewUser())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bindDataSource();
                }
            }
        }
    }
}
