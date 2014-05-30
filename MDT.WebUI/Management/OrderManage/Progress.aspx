<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Progress.aspx.cs" Inherits="MDT.WebUI.Management.OrderManage.Progress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="padding:0px; margin:0px; background-color:transparent;">
        <form id="Form1" method="post" runat="server" style="padding:0px; margin:0px;">
            <div style="width:360px;height:60px;text-align:center;filter:alpha(Opacity=80); -moz-opacity:0.8;opacity: 0.8;background-color:#FFE7A2;font-size:larger;font-weight:bolder; font-size:12px; ">
            
            <br /><img src="/images/loading.gif" border="0" alt="" />&nbsp;&nbsp;
            <asp:Label ID="lblWait" runat="server" Text="正在同步订单，请稍后...."></asp:Label>
               
            </div>
        </form>
</body>
</html>
