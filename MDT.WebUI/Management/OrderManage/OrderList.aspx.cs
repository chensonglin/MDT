using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using MDT.WebUI.AppCode;
using MOP.API.Model;


namespace MDT.WebUI.Management.OrderManage
{
    public partial class OrderList : System.Web.UI.Page
    {
        private int pageIndex;// 页码
        private int pageCount = 50;// 每页数量
        private int totalCount;// 总数量
        //DataTable dtTrade = null;
        List<OrderIdInfo> dtList;
        string shopId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["num"] = "0";
            if (!Page.IsPostBack)
            {
                GetPlatform();
                txtStartTime.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now) + " 00:00";
                txtEndTime.Text = String.Format("{0:yyyy-MM-dd HH:mm}", DateTime.Now);
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script> $(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();});changeRowColor();init();</script>", false);
            ScriptManager.RegisterStartupScript(UpdatePanel2, typeof(UpdatePanel), "onload", "init();</script>", false);
        }
        private void GetPlatform()
        {
            try
            {
                GetShopInfo shopInfo = new GetShopInfo();
                List<global::MOP.API.Model.BasePlatform> platform = shopInfo.GetBasePlatform(null);
                if (platform != null && platform.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (global::MOP.API.Model.BasePlatform item in platform)
                    {
                        if (item.PlatformId != "0002")
                            sb.Append(item.PlatformId + "*" + item.PlatformName + "|");
                    }
                    sb.Append("fenxiao*分销");
                    txtPlatformName.Value = platform[0].PlatformName;
                    txtPlatformId.Value = platform[0].PlatformId;
                    hidePlatform.Value = sb.ToString().TrimEnd('|');
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('无平台信息！');</script>", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\n", "") + "');</script>", false);
                return;
            }
            GetTopShop();
        }

        private void GetTopShop()
        {
            try
            {
                string platformId = txtPlatformId.Value;
                bool isFenxiao = false;
                if (platformId == "fenxiao")
                {
                    platformId = "0001";
                    isFenxiao = true;
                }
                GetShopInfo shopBll = new GetShopInfo();
                List<global::MOP.API.Model.Shop> shopInfo = shopBll.GetBaseShopPlatform(platformId, true, isFenxiao);
                StringBuilder strB = new StringBuilder();
                if (shopInfo != null && shopInfo.Count > 0)
                {
                    foreach (global::MOP.API.Model.Shop item in shopInfo)
                    {
                        strB.Append(item.ShopId + "*" + item.OuterShopId + "*" + item.ShopName + "|");
                    }
                    txtShopName.Value = shopInfo[0].ShopName;
                    txtShopID.Value = shopInfo[0].ShopId;
                    txtOuterShopID.Value = shopInfo[0].OuterShopId;
                }
                hideTopShop.Value = strB.ToString().TrimEnd('|');
            }
            catch (Exception ex)
            {
                hideTopShop.Value = "";
                txtShopName.Value = "";
                txtShopID.Value = "";
                txtOuterShopID.Value = "";
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\n", "") + "');</script>", false);
                return;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (txtShopID.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请选择店铺！');</script>", false);
                return;
            }
            string shopID = txtShopID.Value;
            string outerShopId = txtOuterShopID.Value;
            ViewState["shopID"] = shopID;
            ViewState["outerShopId"] = outerShopId;
            if (txtStartTime.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请输入开始时间！');</script>", false);
                return;
            }
            else if (!IsDate(txtStartTime.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('查询日期格式不正确！');</script>", false);
                return;
            }
            DateTime startTime = Convert.ToDateTime(txtStartTime.Text.Trim() + ":00");
            DateTime endTime = Convert.ToDateTime(txtEndTime.Text.Trim() + ":59");

            if (startTime > endTime)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('开始时间不能大于结束时间！');</script>", false);
                return;
            }
            ViewState["startTime"] = startTime;
            ViewState["endTime"] = endTime;
            ViewState["tCList"] = "";
            ViewState["isNew"] = "1";
            this.AspNetPager1.CurrentPageIndex = 1;

        }

        private void BindData()
        {
            List<global::MOP.API.Model.OrderIdInfo> list = new List<global::MOP.API.Model.OrderIdInfo>();
            int count;
            string shopID = ViewState["shopID"].ToString();
            string outerShopId = ViewState["outerShopId"].ToString();
            DateTime startTime = Convert.ToDateTime(ViewState["startTime"]);
            DateTime endTime = Convert.ToDateTime(ViewState["endTime"]);
            try
            {
                if (startTime < DateTime.Now.Date.AddMonths(-1))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('只能比对一个月之以的交易信息');</script>", false);
                    return;
                }
                
                    OrderContrast contrast = new OrderContrast(txtPlatformId.Value, shopID);
                    list = contrast.GetTradeCompList(startTime, endTime, global::MOP.API.Model.ParamDateType.Create);

                if (AspNetPager1.CurrentPageIndex != 1)
                    pageIndex = AspNetPager1.CurrentPageIndex;
                else
                    pageIndex = 1;
                if (list == null)
                    return;
                // 设置总页数
                count = list.Count;
                totalCount = count / pageCount;
                if ((count % pageCount) != 0)
                {
                    totalCount++;
                }
                if (totalCount == 0)
                {
                    totalCount = 1;
                }
                lblCount.Text = count.ToString();
                lblCurrentPage.Text = pageIndex.ToString();
                lblPageCount.Text = totalCount.ToString();
                var list2 = list.Skip((pageIndex - 1) * pageCount).Take(pageCount);

                ckAll.Enabled = false;
                ckAll.Checked = false;
                this.Repeater1.DataSource = list2;
                this.Repeater1.DataBind();
                this.AspNetPager1.PageSize = pageCount;
                this.AspNetPager1.RecordCount = count;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('错误信息：" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\n", "").Replace("\r", "") + "');</script>", false);
                return;
            }
        }
            

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            ViewState["num"] = (AspNetPager1.CurrentPageIndex - 1) * pageCount;
            BindData();

        }
        /// <summary>
        /// 功　　能：判断指定字符串是否为日期型
        /// </summary>
        /// <param name="strDate">指定字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public bool IsDate(string strDate)
        {
            //DateTime dtDate;
            DateTime dtDate = DateTime.Now;
            bool bValid = true;
            try
            {
                dtDate = DateTime.Parse(strDate);
            }
            catch (FormatException)
            {
                bValid = false;// 如果解析方法失败则表示不是指定类型的数据
                return bValid;
            }
            return bValid;
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            string shopID = txtShopID.Value.Trim();
            string outerShopId = txtOuterShopID.Value.Trim();
            ViewState["shopID"] = shopID;
            ViewState["outerShopId"] = outerShopId;
            DateTime startTime = Convert.ToDateTime(txtStartTime.Text.Trim() + ":00");
            DateTime endTime = Convert.ToDateTime(txtEndTime.Text.Trim() + ":59");
            ViewState["startTime"] = startTime;
            ViewState["endTime"] = endTime;
            ViewState["tCList"] = "";
            ViewState["isNew"] = "1";
            this.AspNetPager1.CurrentPageIndex = 1;
        }

        protected void btnSearchByPage_Click(object sender, EventArgs e)
        {
            if (txtPageForSearch.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请输入要查询的页次');</script>", false);
                txtPageForSearch.Focus();
            }
            else if (!IsPlusInt(txtPageForSearch.Text.Trim()))
            {
                txtPageForSearch.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('查询的页次必须为正整数！');</script>", false);
                txtPageForSearch.Focus();
            }
            else
            {
                this.AspNetPager1.CurrentPageIndex = int.Parse(txtPageForSearch.Text.Trim());
            }
        }
        /// <summary>
        /// 功　　能：判断指定字符串是否为正整数
        /// </summary>
        /// <param name="strData">指定字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public bool IsPlusInt(string strData)
        {
            int iData = 0;
            bool bValid = false;
            try
            {
                iData = int.Parse(strData);
            }
            catch (Exception)
            {
                bValid = false;
            }
            if (iData > 0)//此处不取等于0的数
            {
                //是正整数
                bValid = true;
            }
            else
            {
                //不是正整数
                bValid = false;
            }
            return bValid;
        }

        public string FormatDateTime(string dateTime)
        {
            string dtStr = "";
            if (!string.IsNullOrEmpty(dateTime))
            {
                DateTime dt = Convert.ToDateTime(dateTime);
                dtStr = String.Format("{0:yy-MM-dd HH:mm:ss}", dt);
            }
            return dtStr;
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblOrderID = (Label)e.Item.FindControl("lblOrderID");
                if (lblOrderID.Text.Trim() == "")
                {
                    CheckBox ck = (CheckBox)e.Item.FindControl("CheckBox1");
                    ck.Enabled = true;
                    ckAll.Enabled = true;
                }

                int num = Convert.ToInt32(ViewState["num"]);
                num++;
                Label lblNum = (Label)e.Item.FindControl("LblNum");
                if (lblNum != null)
                {
                    lblNum.Text = num.ToString();
                }
                ViewState["num"] = num;
            }
        }

        /// <summary>
        /// 同步订单信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void lbtnTongbu_Click(object sender, EventArgs e)
        {
            try
            {
                dtList = new List<OrderIdInfo>();

                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    RepeaterItem ri = Repeater1.Items[i];
                    CheckBox ck = (CheckBox)ri.FindControl("CheckBox1");
                    if (ck.Checked)
                    {
                        Label lblShopID = (Label)ri.FindControl("lblShopID");
                        shopId = lblShopID.Text;
                        
                        Label lblOuterShopID = (Label)ri.FindControl("lblOuterShopID"); 
                        Label lblTOrderID = (Label)ri.FindControl("lblTOrderID");
                        Label lblTStatus = (Label)ri.FindControl("lblTStatus");
                        Label lblCreatedComplete = (Label)ri.FindControl("lblCreatedComplete");
                        Label lblModifiedComplete = (Label)ri.FindControl("lblModifiedComplete");

                        OrderIdInfo mod = new OrderIdInfo();
                        if (txtPlatformId.Value == "fenxiao")
                        {
                            mod.OrderFrom = "TBFX";
                        }
                        mod.ShopId = shopId;
                        mod.OuterShopId = lblOuterShopID.Text;
                        mod.OrderId = lblTOrderID.Text;
                        mod.Status = lblTStatus.Text;

                        DateTime outTime = new DateTime();
                        if(DateTime.TryParse(lblCreatedComplete.Text,out outTime))
                            mod.CreateTime = outTime;
                        if (DateTime.TryParse(lblModifiedComplete.Text, out outTime))
                            mod.LastUpdateTime = outTime;

                        dtList.Add(mod);
                    }
                }
                if (dtList.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请选择需要同步的订单！');</script>", false);
                    return;
                }
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(SyncTrade));
                thread.Start();
                Session["State"] = "正在准备同步订单，请稍后...";
                Session["isComplete"] = "false";
                OpenProgressBar(this.Page);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\n", "") + "');</script>", false);
                return;
            }
        }

        public void OpenProgressBar(System.Web.UI.Page Page)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "", "<script>show('hideProgress', 'iframeProgress', '/Management/OrderManage/Progress.aspx');</script>", false);
            return;
        }

        public void SyncTrade()
        {
            int faileNum = 0;
            int successNum = 0;
            OrderContrast contrast = new OrderContrast();
            try
            {
                contrast.SaveOrderToScm(dtList.ToArray());
                successNum++;
            }
            catch
            {
                faileNum++;
            }
            finally
            {
                Session["State"] = "共" + dtList.Count + "条数据已同步。";
            }
            Session["isComplete"] = "true";//任务结束
        }

        protected void lbtnGetShop_Click(object sender, EventArgs e)
        {
            GetTopShop();
        }
    }
}