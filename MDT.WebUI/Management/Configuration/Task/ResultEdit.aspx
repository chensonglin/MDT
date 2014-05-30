<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultEdit.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Task.ResultEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/trHighLight.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <!--页内脚本开始-->
     <script type="text/javascript">
         $(function () {
             $('#dataTable').trOddHilight();
             $('#dataTable').trClick();
         })
         

         function checkInput() {
             var commandName = document.getElementById("txtCommandName").value;
             var xmlPath = document.getElementById("txtXmlPath").value;
             if (commandName.Trim() == "") {
                 alert('请输入CommandName');
                 document.getElementById("txtCommandName").focus();
                 return false;
             }
             if (xmlPath.Trim() == "") {
                 alert('请输入XmlPath');
                 document.getElementById("txtXmlPath").focus();
                 return false;
             }
             return true;
         }

         //关闭本页同时刷新父页面
         function openerRefresh() {
             parent.bindData();
             parent.hide('hideResults', 'iframeResults');
         }

         function checkClose() {
             if (confirm('关闭本页面并不保存数据，是否确定？')) {
                 parent.hide('hideResults', 'iframeResults');
             }
         }
     </script>
<!--页内脚本结束-->
</head>
<body style="margin:10px 20px 0px 20px;">
    <form id="form1" runat="server">
    <div>
        <table id="tbMain"  cellpadding="0" cellspacing="5" border="0" style="font-size:9pt; width:600px; float:left;">
        <tr>
            <td align="right">操作名称：</td>
            <td style="width:500px;">            
                <asp:TextBox ID="txtCommandName" runat="server" CssClass="textFieldFull"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">Xml路径：</td>
            <td><asp:TextBox ID="txtXmlPath" runat="server" CssClass="textFieldFull"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:LinkButton ID="lbtnSave" CssClass="aBtn" runat="server" onclick="lbtnSave_Click" 
                 OnClientClick="return checkInput();"><span><em>保存</em></span></asp:LinkButton>&nbsp;&nbsp;
            <a class="aBtn" href="javascript:checkClose();"><span><em>关闭</em></span></a>
            </td>
        </tr>
        </table>
    </div>
    <input id="hideFileName" type="hidden" runat="server" />

    </form>
</body>
</html>
