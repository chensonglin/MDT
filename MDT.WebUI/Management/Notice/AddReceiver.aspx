<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReceiver.aspx.cs" Inherits="MDT.WebUI.Management.Notice.AddReceiver" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <script type="text/javascript">
        // 手机号码验证 
        function checkCondition() {
            var name = document.getElementById("txtName").value.Trim();
            var phone = document.getElementById("txtPhone").value.Trim();
            var email = document.getElementById("txtEmail").value.Trim();
            if (name == "") {
                alert('请输入姓名');
                document.getElementById("txtName").focus();
                return false;
            }
            if (phone == "" && email == "") {
                alert('请输入手机号码或邮件地址');
                document.getElementById("txtPhone").focus();
                return false;
            }
            if (phone != "") {
                var length = phone.length;
                var mobile = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(14[0-9]{1}))+\d{8})$/;
                if (length != 11 || !mobile.test(phone)) {
                    alert("请输入正确的手机号码");
                    return false;
                }
            }
            if (email != "") {
                var emailReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
                if (!emailReg.test(email)) {
                    alert("请输入正确的邮件地址");
                    return false;
                }
            }
            return true;
        }
    </script>
</head>
<body style="margin:0px 20px 0px 10px;">
    <form id="form1" runat="server">
        <table id="tbMain"  cellpadding="2" cellspacing="5" border="0" 
            style="font-size:9pt; width:340px; float:left; ">
            <tr>
                <td style="width:120px;" align="right">
                   姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 名：
                </td>
                <td style="width:280px" align="left"> 
                    <asp:TextBox ID="txtName" CssClass="textField220" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   手机号码：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtPhone" CssClass="textField220" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   邮件地址：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtEmail" runat="server" CssClass="textField220"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                   备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 注：</td>
                <td align="left"> 
                   <asp:TextBox ID="txtRemark" runat="server" CssClass="textField220"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">启&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用：</td>
                <td align="left">
                    <asp:RadioButton ID="RdioYes" GroupName="rdioEnable" Text="是"  runat="server" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="RdioNo" GroupName="rdioEnable" Text="否" runat="server" />
                </td>
            </tr>
             <tr>
                <td></td>
                <td align="left" valign="bottom" style="height:30px;"> 
                     <asp:Button ID="btnSave" CssClass="btnTeoWords" Text="保存" OnClick="btnSave_Click" runat="server" OnClientClick="return checkCondition();"/>&nbsp;&nbsp;
                     <asp:Button ID="btnExit" CssClass="btnTeoWords" Text="关闭"  runat="server" 
                         OnClientClick="parent.closeDiv('hideReceiver','iframeReceiver')"/>&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
