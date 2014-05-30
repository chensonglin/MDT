<%@ Page Title="注册" Language="C#" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="MDT.WebUI.Account.Register" %>
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
    <!--页内脚本开始-->

        
    <!--页内脚本结束-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="loginLogo">
 <a href="#">五洲在线数据交换平台MDT</a>
</div>
<div id="content">
	<div class="logBody">
    	<div id="login" class="loginBoard">
        	<%--<img src="../images/imgLogin.jpg" width="170" height="45" class="ml45" />--%>
            <table>
            <tr>
                <td>用&nbsp; 户&nbsp; 名：</td>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="textField26"></asp:TextBox></td>
            </tr>
            <tr>
                <td>电子邮箱：</td>
                <td><asp:TextBox ID="txtUserEmail" runat="server" CssClass="textField26"></asp:TextBox></td>
            </tr>
            <tr>
                <td>密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 码：</td>
                <td><asp:TextBox ID="txtPwd" runat="server" CssClass="textField26" 
                        TextMode="Password" ></asp:TextBox></td>
            </tr>
            <tr>
                <td>确认密码：</td>
                <td><asp:TextBox ID="txtRePwd" runat="server" CssClass="textField26" 
                        TextMode="Password" ></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td align="left"><a class="aBtn"><span><em>
                <asp:Button ID="btnRegister" runat="server" CssClass="cflBtn"  Text="创建新用户" onclick="btnRegister_Click" 
                      />
                </em></span></a></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
            </tr>
            </table>
            <%--<ul>
                <li>
                    用&nbsp; 户&nbsp; 名：
                    
                </li>
                <li>
                    电子邮箱：
                    
                </li>
                <li>
                    密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 码：
                    
                </li>
                <li>
                    确认密码：
                    
                </li>    
                <li>
                <label></label>&nbsp;&nbsp;
                
                </li>
            </ul> --%>              
        </div>
  </div>
</div>
<div id="fooder">
<div id="copyright"> &copy;2010五洲在线电子商务 -<a href="#">条款</a>-<a href="#">隐私权政策</a>-<a href="http://www.mic.cn">www.mic.cn</a> </div>
</div>
 </form>
</body>
</html>



