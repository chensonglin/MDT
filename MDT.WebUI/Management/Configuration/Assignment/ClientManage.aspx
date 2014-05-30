<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientManage.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Assignment.ClientManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户端管理</title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/trHighLight.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>

    <!--页内脚本开始-->
     <script type="text/javascript">
         $(function () {
             $('#dataTable').trOddHilight();
             //             $('#dataTable').trHilight();
             $('#dataTable').trClick();
         })

         //选择一条记录
         function SingleSelect(o) {
             if (G("search").style.display == "block") {
                 return;
             }
             var index = o.rowIndex; //获取行的索引值
             var hiddenSelectRow = document.getElementById("hiddenSelectRow");
             hiddenSelectRow.value = index;
             //index = index - 1;
             var lblID = document.getElementById("Repeater1_lblID_" + index); //获取选中行内的  lblID
             var id = lblID.innerHTML;  //获取记录id
             if (id != "") {
                var selectID = document.getElementById("<%= hiddenSelectID.ClientID %>");
                selectID.value = id;
             }
         }

         function MarkTrClick() {
             var hiddenSelectRow = document.getElementById("hiddenSelectRow");
             var rowIndex = hiddenSelectRow.value;
             if (rowIndex != "") {
                 var objTable = G("dataTable");
                 addClass(objTable.rows[rowIndex], "cflAllotTask");
             }
         }
         function addClass(element, className) {
             if (!this.hasClass(element, className)) {
                 element.className += " " + className;
             }
         }
         function hasClass(element, className) {
             var reg = new RegExp('(\\s|^)' + className + '(\\s|$)');
             return element.className.match(reg);
         }
         //验证 新增、修改、删除 操作
         function checkCondition(type) {
             if (G("search").style.display == "block") {//如果正在进行编辑操作，则提示用户先保存或取消编辑
                 alert("请先保存或取消当前客户端编辑，再进行下一步操作！");
                 return false;
             }
             var selectID = document.getElementById("<%= hiddenSelectID.ClientID %>").value;
             if (type == "Delete") {
                 if (selectID == "") {
                     alert("请选择需要删除的ID.");
                     return false;
                 }
                 if (confirm("是否删除所选项？")) {
                     return true;
                 }
                 return false;
             }
             if (type == "Modify") {
                 if (selectID == "") {
                     alert("请选择需要修改的客户端.");
                     return false;
                 }
             }
             return true;
         }
         function Exit() {//点击取消按钮时提示
             if (confirm("是否确定取消编辑！")) {
                 G("search").style.display = "none";
             }
         }
     </script>
</head>
<body style="margin:0px 20px 0px 20px; overflow-Y:auto;">
    <form id="form1" runat="server">
    <div >
         <table id="tbMain"  cellpadding="2" cellspacing="5" border="0" 
            style="font-size:9pt; width:800px; float:left;" align="center">
            <tr>
                <td align="right">
                    <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="aBtn" 
                        onclick="lbtnAdd_Click"  OnClientClick="return checkCondition('Add');"><span><em>新增</em></span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnModify" runat="server" CssClass="aBtn" 
                        onclick="lbtnModify_Click" OnClientClick="return checkCondition('Modify');"><span><em>修改</em></span></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="aBtn" 
                        onclick="lbtnDelete_Click" OnClientClick="return checkCondition('Delete');"><span><em>删除</em></span></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>    
                <div id="search" runat="server">
                    <table style="width:100%; height: 30px;">
                        <tr>
                        <td>客户端名称：</td>
                        <td><asp:TextBox ID="txtName" CssClass="textField26" runat="server"></asp:TextBox></td>
                        <td>客户端IP：</td>
                        <td><asp:TextBox ID="txtIP" runat="server" CssClass="textField44"></asp:TextBox></td>
                        <td>
                            <asp:LinkButton ID="lbtnSave" runat="server" CssClass="aBtn" 
                                onclick="lbtnSave_Click"><span><em>保存</em></span></asp:LinkButton>
                            <asp:LinkButton ID="lbtnExit" runat="server" CssClass="aBtn" 
                                onclick="lbtnExit_Click"><span><em>取消</em></span></asp:LinkButton>
                        </td>
                        </tr>
                    </table>
                </div>
                    <table id="cflDataTable">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>客户端名称</th>
                            <th>客户端IP</th>
                        </tr>
                    </thead>
                    </table>
                    <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <%--<table id="dataTable">
                        <thead>
                        <th>ID</th>
                        <th>客户端名称</th>
                        <th>客户端IP</th>
                        </thead>--%>
                        <div id="divScroll" style="OVERFLOW-Y:auto; OVERFLOW-X:hidden; vertical-align:text-top; max-height:392px; border-bottom:1px solid #a3c0e8;">
                        <table id="dataTable" width="100%" style=" margin-top:-1px; margin-bottom:2px;">
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr onclick="SingleSelect(this)">
                        <td><asp:Label ID="lblID" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Name") %>'></asp:Label>
                        </td>
                        <td><%#DataBinder.Eval(Container,"DataItem.serverIP") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%--</table>--%>
                        </tbody>
                        </table>
                        </div>
                    </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            </table>
    </div>
    <input id="hiddenSelectID" type="hidden" name="hiddenSelectID" runat="server" />
    <input id="hiddenSelectRow" type="hidden" name="hiddenSelectRow" runat="server" />

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
                    if (i == 2) {
                        var sbarWidth = G('divScroll').offsetWidth - G('divScroll').scrollWidth;
                        objTable1.cells[i].width = objTable2.rows[0].cells[i].offsetWidth + sbarWidth-3 ;
                    }
                    //alert(objTable1.cells[i].offsetWidth + "         " + objTable2.rows[0].cells[i].offsetWidth + "     " + i);
                }
            }
        }
        colunmWidth();
    </script>
    </form>
</body>
</html>
