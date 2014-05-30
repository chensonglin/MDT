using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.Console
{
    public partial class FormDatabaseSet : Form
    {
        private bool isAdd;
        private EDatabase database;
        private EDatabase prevDatabase;

        public FormDatabaseSet(EDatabase database, bool isAdd)
        {
            InitializeComponent();

            this.isAdd = isAdd;
            this.database = database;
            cboDatabaseType.Properties.NullText = "请选择......";
            foreach (string sourceTypeStr in Enum.GetNames(typeof(SourceType)))
            {
                cboDatabaseType.Properties.Items.Add(sourceTypeStr);
            }

            if (!isAdd)
            {
                txtAlias.Text = database.Alias;
                txtServer.Text = database.Server;
                txtPort.Text = database.Port.ToString();
                txtDataBase.Text = database.Database;
                txtUserId.Text = database.UserId;
                txtPassword.Text = database.Password;
                cboDatabaseType.EditValue = database.DatabaseType;

                prevDatabase = new EDatabase();
                prevDatabase.Alias = database.Alias;
                prevDatabase.Server = database.Server;
                prevDatabase.Port = database.Port;
                prevDatabase.Database = database.Database;
                prevDatabase.UserId = database.UserId;
                prevDatabase.Password = database.Password;
                prevDatabase.DatabaseType = database.DatabaseType;
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            string message = String.Empty;
            int port = 0;
            Int32.TryParse(txtPort.Text, out port);
            DbSchemaService dbSchema = new DbSchemaService();
            
            try
            {
                DataTable table = dbSchema.GetTableInfo((SourceType)Enum.Parse(typeof(SourceType), cboDatabaseType.Text)
                                                        , txtServer.Text
                                                        , port
                                                        , txtDataBase.Text
                                                        , txtUserId.Text
                                                        , txtPassword.Text);

                if (table != null && table.Rows.Count > 0)
                    message = "连接成功！";
                else
                    message = "连接失败！";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            MessageBox.Show(message, "信息提示", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAlias.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入数据库别名！", "信息提示", MessageBoxButtons.OK);
                txtAlias.Focus();
            }
            else if (txtServer.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入服务名称！", "信息提示", MessageBoxButtons.OK);
                txtServer.Focus();
            }
            else if (txtDataBase.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入数据库名称！", "信息提示", MessageBoxButtons.OK);
                txtDataBase.Focus();
            }
            else if (txtUserId.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入用户名称！", "信息提示", MessageBoxButtons.OK);
                txtUserId.Focus();
            }
            else if (txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入用户密码！", "信息提示", MessageBoxButtons.OK);
                txtPassword.Focus();
            }
            else
            {
                ESourceDAL sourceDAL = new ESourceDAL();
                EDatabaseDAL databaseDAL = new EDatabaseDAL();

                if (isAdd && databaseDAL.ContainAlias(txtAlias.Text))
                {
                    MessageBox.Show("此数据库别名已存在，请从新输入！", "信息提示", MessageBoxButtons.OK);
                    txtAlias.Focus();
                    txtAlias.SelectAll();
                }
                else
                {
                    int port = 0;
                    Int32.TryParse(txtPort.Text, out port);

                    //database.Customer_ID = 1;
                    database.Alias = txtAlias.Text;
                    database.DatabaseType = cboDatabaseType.Text;
                    database.Server = txtServer.Text;
                    database.Port = port;
                    database.Database = txtDataBase.Text;
                    database.UserId = txtUserId.Text;
                    database.Password = txtPassword.Text;

                    try
                    {
                        if (isAdd)
                        {
                            databaseDAL.AddObject(database);
                        }
                        else
                        {
                            databaseDAL.ModifyObject(database);
                            // 修改任务配置信息（数据库连接字符串）
                            sourceDAL.ModifyObject(prevDatabase, database);
                        }

                        MessageBox.Show("保存成功！");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                    }
                }
            }
        }
    }
}
