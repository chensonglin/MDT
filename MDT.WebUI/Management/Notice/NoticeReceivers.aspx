<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="NoticeReceivers.aspx.cs" Inherits="MDT.WebUI.Management.ErrorWarn.ErrorWarnList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/js/trHighLight.js"></script>
<script type="text/javascript">
    $(function () {
        $('#dataTable').trOddHilight();
        $('#dataTable').trClick();
    })

</script>
</head>
<body style="margin:0px 20px 0px 10px; width:800px; height:460px;">
    <form id="form1" runat="server">
     <div>
       <table id="cflDataTable">
        <thead>
        <tr>
        <th>序号</th>
        <th>姓名</th>
        <th>手机号码</th>
        <th>邮箱</th>
        <th>预警方式</th>
        <th>备注</th>
        </tr>
        </thead>
        </table>
        <div id="divScroll" style="OVERFLOW-Y:auto; OVERFLOW-X:hidden; vertical-align:text-top; max-height:392px; border-bottom:1px solid #a3c0e8;">
           <table id="dataTable" width="100%" style=" margin-top:-1px;">
           <tbody>
       <asp:Repeater ID="Repeater1" runat="server" 
             onitemdatabound="Repeater1_ItemDataBound">
         <ItemTemplate>
             <tr>
               <td><asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label></td>
               <td><%#DataBinder.Eval(Container,"DataItem.name") %></td>
               <td><%#DataBinder.Eval(Container,"DataItem.phone") %></td>
               <td><%#DataBinder.Eval(Container,"DataItem.email") %></td>
               <td><%#DataBinder.Eval(Container, "DataItem.noticemode")%></td>
               <td><%#DataBinder.Eval(Container,"DataItem.remark") %></td>
             </tr>
         </ItemTemplate>
       </asp:Repeater>
        </tbody>
       </table>
       </div>
       <div class="page" align="center" style="margin:-1px 0px 0px 0px;" >
	    </div>
     </div>
     <input id="hideNoticeModel" type="hidden" value="0*请选择|1*邮件|2*短信|3*邮件和短信" runat="server" />
     <script type="text/jscript">
         //设置列的宽度  使表头表cflDataTable的列宽 等于 数据表dataTable的列宽
         function colunmWidth() {
             var objTable1 = G("cflDataTable");
             var objTable2 = G("dataTable");
             if (objTable1 && objTable2 && objTable2.rows.length > 0) {
                 for (var i = 0; i < objTable2.rows[0].cells.length; i++) {
                     var widthTest = objTable2.rows[0].cells[i].offsetWidth - 3;
                     if (widthTest < 1) {
                         return;
                     }
                     objTable1.cells[i].width = widthTest;
                     if (i == 5) {
                         var sbarWidth = G('divScroll').offsetWidth - G('divScroll').scrollWidth;
                         objTable1.cells[i].width = objTable2.rows[0].cells[i].offsetWidth + sbarWidth - 3;
                     }
                 }
             }
         }
         colunmWidth();
    </script>
 </form>
</body>
</html>