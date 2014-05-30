<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllotTask.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Assignment.AllotTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>任务分配</title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/trHighLight.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <script type="text/javascript" src="/js/jQuery.CustomSelect.js"></script>
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
        top: 49px;
        left: 347px;
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
    <!--页内脚本开始-->
     <script type="text/javascript" language="javascript">
         $(function () {
             $('#dataTable').trOddHilight();
             $('#dataTable').trClick();
         })
         var oddName = "";
         function txtNameChange() {//当选择的客户端改变时  在属于该客户端的任务前 打钩
             var txtname = G('txtName').value;
             oddName = txtname;
             var objTable = G("dataTable");
             if (objTable) {
                 for (var i = 0; i < objTable.rows.length - 1; i++) {
                     var lbl = G("Repeater1_lblClientName_" + i);
                     var lblName = $(lbl).text();
                     var cb = G("Repeater1_cb_" + i)
                     if (lblName == txtname) {
                         cb.checked = "checked";
                         objTable.rows[i].className = "cflAllotTask";
                     }
                     else {
                         cb.checked = "";
                         objTable.rows[i].className = "";
                     }
                 }
                 $('#dataTable').trOddHilight();
                 $('#dataTable').trClick();
             }
         }
         //设置列的宽度  使表头表cflDataTable的列宽 等于 数据表dataTable的列宽
         function colunmWidth() {
             var objTable1 = document.getElementById("cflDataTable");
             var objTable2 = document.getElementById("dataTable");
             if (objTable1 && objTable2 && objTable2.rows.length > 0) {
                 for (var i = 0; i < objTable2.rows[0].cells.length; i++) {
                     var widthTest = objTable2.rows[0].cells[i].offsetWidth - 3;
                     if (widthTest < 1) {
                         return;
                     }
                     objTable1.rows[0].cells[i].width = widthTest;
                     if (i == 4) {
                         var sbarWidth = G('divScroll').offsetWidth - G('divScroll').scrollWidth;
                         objTable1.rows[0].cells[i].width = objTable2.rows[0].cells[i].offsetWidth + sbarWidth - 3;
                     }
                 }
             }
         }

         function init() {
             $('.mtd').CustomSelect({
                 L: 0, //下拉选型的左偏移量，默认为0
                 T: 0, //下拉选型的上偏移量，默认为0
                 W: 20, //下拉选型的宽偏移量，默认为0
                 N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                 conDiv: 'ShowTaskdiv', //下拉选项外围的类，默认为conDiv
                 callback: GetID
             });
         }

         function GetID() {
             eval(document.getElementById('lbtnRefresh').href);
         }
     </script>
      
</head>
<body style="margin:0px 20px 0px 20px;">
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <asp:Timer id="timer1" Interval="1" runat="server" ontick="timer1_Tick">
        </asp:Timer>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:UpdateProgress runat="server" DisplayAfter="1">
         <progresstemplate>
         <div id="bgDiv" class="bgDiv" style="height:230px"></div>
                       <div id="messagediv">
                        <table>
                        <tr>
                            <td class="style1"><br />&nbsp;&nbsp;&nbsp;<img src="/images/loading.gif" border=0 />&nbsp;&nbsp;&nbsp; 数据正在加载中，请稍候........</td>
                            </tr>
                        </table>
                    </div>
         </progresstemplate>
        </asp:UpdateProgress>
        <table id="tbMain"  cellpadding="2" cellspacing="5" border="0" 
            style="font-size:9pt; width:800px; float:left;" align="center">
            <tr>
                <td>
                 <div id="search">
                 <table style="width:100%;">
                 <tr>
                    <td align="right">客户端：</td>
                    <td align="left">
                      <input id="txtName" type="text" class="textField44 mtd" runat="server"  value="" to="hiddenStr" txtid="txtID" />  
                      <input id="txtID" type="text" runat="server" class="hide" value="" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSave" CssClass="aBtn" runat="server" 
                            onclick="lbtnSave_Click" OnClientClick=""><span><em>保存</em></span></asp:LinkButton>
                    </td>
                 </tr>
                 </table>
                 </div>
                    <table id="cflDataTable">
                    <thead>
                        <tr>
                            <th>选择</th>
                            <th>序号</th>
                            <th>任务名称</th>
                            <th>任务描述</th>
                            <th>客户端</th>
                        </tr>
                    </thead>
                    </table>
                    <asp:Repeater ID="Repeater1" runat="server" 
                        onitemdatabound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <div id="divScroll" style="OVERFLOW-Y:auto; OVERFLOW-X:hidden; vertical-align:text-top; max-height:400px; border-bottom:1px solid #a3c0e8;">
                        <table id="dataTable" width="100%" style=" margin-top:-1px; margin-bottom:2px;">
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td style="width:40px;"><asp:CheckBox ID="cb" runat="server" /></td>
                        <td><asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label></td>
                        <td align="left">
                            <asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ID") %>' Visible="false"></asp:Label>
                            <%#DataBinder.Eval(Container,"DataItem.TaskName") %>
                        </td>
                        <td align="left"><%#DataBinder.Eval(Container,"DataItem.Note") %></td>
                        <td align="left">
                            <asp:Label ID="lblClientName" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Name") %>'></asp:Label>
                        </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                        </div>
                    </FooterTemplate>
                    </asp:Repeater>
        
                </td>
            </tr>
        </table>
        <input type="hidden" id="hiddenStr" name="hiddenStr" runat="server" />
            <asp:LinkButton ID="lbtnRefresh" runat="server" onclick="lbtnRefresh_Click"></asp:LinkButton>
        </ContentTemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </triggers>
        </asp:UpdatePanel>
    </div>

    </form>
</body>
</html>
