<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="OrderList.aspx.cs"
    Inherits="MDT.WebUI.Management.OrderManage.OrderList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    #bgDiv   /*页面中实现等待提示的div样式*/
    {
	    position:absolute;
	    background-color:White;
	    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);
	    opacity:0.5;
	    left:0;
	    top:0;
	    z-index:10000;
	    width:1%;
    }
    #messagediv  /*页面中实现等待提示的div样式*/
    {
	    position:absolute;
	    margin:150px auto;
	    width:300px;
	    height:60px;
	    text-align:center;
	    filter:alpha(Opacity=80);
	    -moz-opacity:0.5;
	    opacity: 0.5;
	    background-color:#FFE7A2; /*#C6DDFC  #D4E7FF #F3FAFA #EAF4FF #FFE7A2 #ffffff*/
	    z-index:100011;
	    font-size:larger;
	    font-weight:bolder;
        top: 104px;
        left: 580px;
        border:1px #F4AA2E solid; /*#A3C0E8 #F4AA2E  #D4E7FF*/
                }
    .ShowTaskdiv  /*下拉框显示选项的div*/
	{
		position:absolute;
		height:auto;
		max-height:249px;
		text-align:left;
		background-color:#fff; /*#C6DDFC  #D4E7FF #F3FAFA #EAF4FF #FFE7A2 #ffffff*/
		border:1px #587897 solid; /*#A3C0E8 #F4AA2E  #D4E7FF*/
		overflow-y:auto;
		display:none;
		font-size:12px;
		left:0;
		top:0;
    }
    .imBtn{
	    vertical-align:bottom;
	    +vertical-align:text-bottom;}
</style>
    <script type="text/javascript">
        function changeRowColor() {//改变行的颜色
            var objTable = G("dataTable");
            if (objTable) {
                for (var i = 0; i < objTable.rows.length - 1; i++) {
                    var lblOrderID = G("MainContent_Repeater1_lblOrderID_" + i);
                    var orderID = $(lblOrderID).text();
                    if (orderID.Trim() == "") {
                        objTable.rows[i + 1].className = "cflLogFailedTr";
                    }
                }
            }
        }



        $(function () {//初始化下拉列表
            $('.platform').CustomSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true,  //加载时生成下拉节点
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'ShowTaskdiv', //下拉选项外围的类，默认为conDiv
                callback: GetPlatformID
            });
        })
        function init() {//构建下拉列表框
            $('.shop').CustomSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true,  //加载时生成下拉节点
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'ShowTaskdiv' //下拉选项外围的类，默认为conDiv
            });
        }
        function GetPlatformID() {
            eval(document.getElementById('MainContent_lbtnGetShop').href);
        }
        //全选的方法
        function ckAllClick(obj) {
            var boolC = $(obj).attr('checked');
            $('input:checkbox').each(function () { if (!this.disabled) this.checked = boolC; });
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <!---------------- 弹出窗口 ---------------------->
    <div id="hidebg">
    </div>
    <div id="hideProgress" class="openDiv" style="width: 360px; height: 60px; padding: 0px;">
        <iframe id="iframeProgress" src="" frameborder="0" scrolling="no" width="360" height="60px;"
            allowtransparency="true" style="border: 1px #F4AA2E solid;"></iframe>
    </div>
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="timer1" Interval="1" runat="server" OnTick="timer1_Tick">
    </asp:Timer>
    <div>
        <div id="search">
            <table style="width: 100%;">
                <tr>
                    <td align="right">
                        &nbsp;&nbsp;平台：
                    </td>
                    <td align="left">
                        <input id="txtPlatformName" name="" type="text" class=" textField14 platform" to="MainContent_hidePlatform"
                            txtid="MainContent_txtPlatformId" runat="server" />
                        <input id="txtPlatformId" class="hide" type="text" runat="server" value="" />
                    </td>
                    <td align="right">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;店铺：
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <input id="txtShopName" name="" type="text" class="textField220 shop" to="MainContent_hideTopShop"
                                    txtid="MainContent_txtShopID" txtouterid = "MainContent_txtOuterShopID" runat="server" />
                                <input id="txtShopID" class="hide" type="text" runat="server" value="" />
                                <input id="txtOuterShopID" class="hide" type="text" runat="server" value="" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbtnGetShop" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="right">
                        交易创建时间：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="textField14" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'});"></asp:TextBox>
                        至：
                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="textField14" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'});"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="lbtnSearch" CssClass="aBtn" runat="server" OnClick="lbtnSearch_Click"><span><em>查询</em></span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnTongbu" runat="server" CssClass="aBtn" OnClick="lbtnTongbu_Click"><span><em>同步</em></span></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="bgDiv" class="bgDiv" style="height: 230px">
                        </div>
                        <div id="messagediv">
                            <table>
                                <tr>
                                    <td class="style1">
                                        <br />
                                        &nbsp;&nbsp;&nbsp;<img src="../../images/loading.gif" border="0" />&nbsp;&nbsp;&nbsp;
                                        数据正在加载中，请稍候........
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="dataGrid">
                    <table id="dataTable">
                        <thead>
                            <tr>
                                <th>
                                    <asp:CheckBox ID="ckAll" runat="server" Enabled="false" onclick="ckAllClick(this)" />
                                    全选
                                </th>
                                <th>
                                    序号
                                </th>
                                <th>
                                    店铺ID
                                </th>
                                <th>
                                    外部店铺ID
                                </th>
                                <th>
                                    外部订单号(销售平台)
                                </th>
                                <th>
                                    订单状态
                                </th>
                                <th>
                                    外部订单号(SCM系统)
                                </th>
                                <th>
                                    订单状态
                                </th>
                                <th>
                                    交易创建时间
                                </th>
                                <th>
                                    交易修改时间
                                </th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" Enabled="false" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblShopID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ShopId")%>'></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblOuterShopID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.OuterShopId")%>'></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblTOrderID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.OrderId")%>'></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblTStatus" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Status")%>'></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblOrderID" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.OuterSorderId")%>'></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.ScmOrderStatus")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCreated" runat="server" Text='<%#FormatDateTime(DataBinder.Eval(Container, "DataItem.CreateTime").ToString())%>'></asp:Label>
                                        <asp:Label ID="lblCreatedComplete" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.CreateTime")%>'
                                            CssClass="hide"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblModified" runat="server" Text='<%#FormatDateTime(DataBinder.Eval(Container, "DataItem.LastUpdateTime").ToString())%>'></asp:Label>
                                        <asp:Label ID="lblModifiedComplete" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.LastUpdateTime")%>'
                                            CssClass="hide"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="page" align="center" style="margin: 0px 0px 0px 0px;">
                        <caption>
                            <ul class="rightPosition">
                                <li>共有<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                    条信息， 当前
                                    <asp:Label ID="lblCurrentPage" runat="server" Text="0"></asp:Label>
                                    /
                                    <asp:Label ID="lblPageCount" runat="server" Text="0"></asp:Label>
                                    页</li>
                                <li>
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" OnPageChanged="AspNetPager1_PageChanged"
                                        ShowPageIndexBox="Never" UrlPaging="false">
                                    </webdiyer:AspNetPager>
                                </li>
                                <li>转到</li>
                                <li>
                                    <asp:TextBox ID="txtPageForSearch" runat="server" CssClass="PIC"></asp:TextBox>
                                </li>
                                <li>页</li>
                                <li>
                                    <asp:Button ID="btnSearchByPage" runat="server" CssClass="SBC" OnClick="btnSearchByPage_Click"
                                        Text="go" />
                                </li>
                            </ul>
                        </caption>
                    </div>
                </div>
                <input type="hidden" id="hideTopShop" runat="server" value="" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lbtnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                <asp:AsyncPostBackTrigger ControlID="lbtnTongbu" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lbtnGetShop" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <input type="hidden" id="hidePlatform" runat="server" value="" />
    <asp:LinkButton ID="lbtnGetShop" runat="server" OnClick="lbtnGetShop_Click"></asp:LinkButton>
</asp:Content>
