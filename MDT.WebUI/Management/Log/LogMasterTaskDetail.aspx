<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogMasterTaskDetail.aspx.cs" Title="五洲在线数据交换平台-任务查询" Inherits="MDT.WebUI.Management.Log.LogMasterTaskDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <table class="ml45" cellpadding="0" cellspacing="0" border="0" width="95%">
            <tr>
                <td>
                    <asp:RadioButtonList ID="rbtType" runat="server" 
                        OnSelectedIndexChanged="rbtType_Click" 
                        RepeatDirection="Horizontal" AutoPostBack="True" 
                        ontextchanged="rbtType_Click" CellPadding="20">
                        <asp:ListItem Text="映射信息" Value="Mapping"></asp:ListItem>
                        <asp:ListItem Text="原配置文件" Value="SourceConfig"></asp:ListItem>
                        <asp:ListItem Text="目标配置文件" Value="TSourceConfig"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                <div style="border:1px #B6D5FF solid; width:780px; height:500px;">
                    <iframe id="iframeDetail" runat="server" width="780" height="500" 
                      marginwidth="0" MARGINHEIGHT="0" FRAMEBORDER='0'  hspace="0" vspace="0"></iframe>
                </div>
                </td>
            </tr>
         </table>
    </form>
</body>
</html>
