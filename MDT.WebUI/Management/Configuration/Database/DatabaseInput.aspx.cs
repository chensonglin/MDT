using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections;

using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.WebUI.Management.Configuration.Database
{
    public partial class DatabaseInput : System.Web.UI.Page
    {
        private EDatabaseDAL databaseDAL;
        private List<EDatabase> list;
        private EDatabase dataBase;
        private EDatabase prevDatabase;
        private int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            list = new List<EDatabase>();
            databaseDAL = new EDatabaseDAL();
            dataBase = new EDatabase();
            if (Page.IsPostBack)
            {
                return;
            }

            InitDataBaseType();//绑定数据库类型下拉列表框
            ViewState["pwd"] = "";
            if (Request["TYPE"].ToString() == "MODIFY")
            {
                id = int.Parse(Request["ID"].ToString());
                ViewState["id"] = id;
                dataBase = databaseDAL.GetDatabase(id);
                InitData();//绑定数据
            }
        }

        private void InitData()
        {
            this.txtDataBaseType.Value = dataBase.DatabaseType;
            this.txtDataBaseAlias.Text = dataBase.Alias;
            this.txtServer.Text = dataBase.Server;
            this.txtPort.Text = dataBase.Port.ToString();
            this.txtDatabase.Text = dataBase.Database;
            this.txtUserName.Text = dataBase.UserId;
            txtPwd.Attributes.Add("value", dataBase.Password);//绑定密码
        }

        private void InitDataBaseType()
        {
            var dataType = Enum.GetNames(typeof(SourceType)).ToArray();
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < dataType.Length; i++)
            {
                strB.Append("0*" + dataType[i].ToString() + "|");
            }
            string str = strB.ToString().TrimEnd('|');
            txtDataBaseType.Value = dataType[0].ToString();
            hidden_DataBaseType.Value = str;
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            ViewState["pwd"] = txtPwd.Text;
            string message = String.Empty;
            int port = 0;
            Int32.TryParse(txtPort.Text, out port);
            DbSchemaService dbSchema = new DbSchemaService();

            try
            {
                DataTable table = dbSchema.GetTableInfo((SourceType)Enum.Parse(typeof(SourceType), 
                                                        this.txtDataBaseType.Value)
                                                        , txtServer.Text
                                                        , port
                                                        , this.txtDatabase.Text
                                                        , this.txtUserName.Text
                                                        , this.txtPwd.Text);

                if (table != null && table.Rows.Count > 0)
                    message = "连接成功！";
                else
                    message = "连接失败！";
            }
            catch (Exception ex)
            {
                message = ex.Message.Replace("'","").Replace("\"","").Replace("\n","");
            }
            txtPwd.Attributes.Add("value", ViewState["pwd"].ToString());
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + message + "');</script>");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESourceDAL sourceDAL = new ESourceDAL();
            EDatabaseDAL databaseDAL = new EDatabaseDAL();

            if (Request["TYPE"].ToString() == "ADD" && databaseDAL.ContainAlias(this.txtDataBaseAlias.Text))
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('此数据库别名已存在，请重新输入！');</script>");
                txtDataBaseAlias.Focus();
                return;
            }
            else
            {
                int port = 0;
                Int32.TryParse(txtPort.Text, out port);
                dataBase.Alias = txtDataBaseAlias.Text;
                dataBase.DatabaseType = this.txtDataBaseType.Value;
                dataBase.Server = txtServer.Text;
                dataBase.Port = port;
                dataBase.Database = this.txtDatabase.Text;
                dataBase.UserId = this.txtUserName.Text;
                dataBase.Password = this.txtPwd.Text;
                try
                {
                    if (Request["TYPE"].ToString() == "ADD")
                        databaseDAL.AddObject(dataBase);
                    else
                    {
                        if (ViewState["id"] == null)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('未找到信息编号，修改失败！');</script>");
                            return;
                        }
                        int dataBaseId = Convert.ToInt32(ViewState["id"]);
                        dataBase.ID = dataBaseId;
                        EDatabase prevDatabase = databaseDAL.GetDatabase(dataBaseId);//查询原数据库信息

                        databaseDAL.ModifyObject(dataBase);
                        // 修改任务配置信息（数据库连接字符串）
                        sourceDAL.ModifyObject(prevDatabase, dataBase);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\n","")+"');</script>");
                    return;
                }
            }
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存成功！');parent.RefreshWindows();parent.hide('hideDataBase', 'iframeDataBase');;</script>");
        }
    }
}