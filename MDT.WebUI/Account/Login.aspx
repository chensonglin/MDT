<%@ Page Title="登录" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="MDT.WebUI.Account.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <link href="../css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../css/calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../js/mic.main.js"></script>
    <script type="text/javascript" src="../js/jquery-calendar.js"></script>
    <script type="text/javascript" src="../js/jquery.validate.min.js"></script>
    <!--页内脚本开始-->
    <script language="javascript" type="text/javascript">
        function onfo() {
            document.getElementById('txtUserName').focus();
        }
    </script>
    <!--页内脚本结束-->
</head>
<body onload="onfo();">
    <form id="form1" runat="server">
<div id="loginLogo">
 <a href="#">五洲在线数据交换平台MDT</a>
</div>
<div id="content">
	<div class="logBody">
    	<div id="login" class="loginBoard">
            <asp:ScriptManager runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel runat="server" id="UpdatePanel1" ChildrenAsTriggers="true">
                <ContentTemplate>
            <table cellpadding="4" cellspacing="4"s>
            <tr>
                <td>用户名：</td>
                <td align="left"><asp:TextBox ID="txtUserName" runat="server" CssClass="textField26"></asp:TextBox></td>
            </tr>
            <tr>
                <td>密&nbsp;&nbsp; 码：</td>
                <td align="left"> <asp:TextBox ID="txtPwd" runat="server" CssClass="textField26" 
                        TextMode="Password" ></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td align="left"> &nbsp;<asp:CheckBox ID="ckKeepLoined" runat="server" Text="" />
                  <span>保持登录状态</span></td>
            </tr>
            <tr>
                <td></td>
                <td align="left">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btnTeoWords" Text="登&ensp;录" 
                            onclick="btnLogin_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2"><%-- 你还没有账号？<b class="ml20"><a>
                    <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">立即注册</asp:HyperLink>
                    </a></b>--%></td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
  </div>
</div>
<div id="fooder">
<div id="copyright"> &copy;2010五洲在线电子商务 -<a href="#">条款</a>-<a href="#">隐私权政策</a>-<a href="http://www.mic.cn">www.mic.cn</a> </div>
   </div> </form>
</body>
</html>
