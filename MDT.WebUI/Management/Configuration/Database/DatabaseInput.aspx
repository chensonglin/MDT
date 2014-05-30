<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseInput.aspx.cs" Title="五洲在线数据交换平台-数据库配置" Inherits="MDT.WebUI.Management.Configuration.Database.DatabaseInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/jQuery.CustomSelect.js"></script>

    <style type="text/css">
    .ShowTaskdiv  /*下拉框显示选项的div*/
	{
		position:absolute;
		height:auto;
		max-height:126px;
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
    <script language="javascript" type="text/javascript">
        function checkCondition() {
            if (document.getElementById('txtDataBaseAlias').value == "") {
                alert('请输入数据库别名！');
                document.getElementById('txtDataBaseAlias').focus();
                return false;
            }
            if (document.getElementById('txtServer').value == "") {
                alert('请输入服务名称！');
                document.getElementById('txtServer').focus();
                return false;
            }
            if (document.getElementById('txtPort').value == "") {
                alert('请输入端口号！');
                document.getElementById('txtPort').focus();
                return false;
            }
            if (document.getElementById('txtDatabase').value == "") {
                alert('请输入数据库名！');
                document.getElementById('txtDatabase').focus();
                return false;
            }
            if (document.getElementById('txtUserName').value == "") {
                alert('请输入用户名！');
                document.getElementById('txtUserName').focus();
                return false;
            }
            if (document.getElementById('txtPwd').value == "") {
                alert('请输入密码！');
                document.getElementById('txtPwd').focus();
                return false;
            }
            return true;
        }

        $(function () {
            $('.mtd').CustomSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'ShowTaskdiv' //下拉选项外围的类，默认为conDiv
            });
        })


    </script>
</head>
<body style="margin:0px 20px 0px 20px;">
    <form id="form1" runat="server">
        <table id="tbMain"  cellpadding="2" cellspacing="5" border="0" 
            style="font-size:9pt; width:340px; float:left; ">
            <tr>
                <td style="width:180px;" align="right">
                   数据库类型：
                </td>
                <td style="width:280px" align="left"> 
                    <input id="txtDataBaseType" name="" type="text" class="textSelect220 mtd" to="hidden_DataBaseType" runat="server" readonly="readonly" onfocus="this.blur();"/>
                    <input value="" type="text" class="hide" id="hidden_DataBaseType" runat="server"/>
                </td>
            </tr>
            <tr>
                <td align="right">
                   数据库别名：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtDataBaseAlias" CssClass="textField220" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   服务名称：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtServer" runat="server" CssClass="textField220"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                   端&nbsp;&nbsp;口&nbsp;&nbsp;号：</td>
                <td align="left"> 
                   <asp:TextBox ID="txtPort" runat="server" CssClass="textField220"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                   数据库名：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtDatabase" runat="server" CssClass="textField220"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                   用&nbsp;&nbsp;户&nbsp;&nbsp;名：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtUserName" runat="server" CssClass="textField220"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right">
                   密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：
                </td>
                <td align="left"> 
                   <asp:TextBox ID="txtPwd" runat="server" CssClass="textField220" EnableViewState="true" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                   
                </td>
                <td align="left" valign="bottom" style="height:30px;"> 
                    <asp:Button ID="btnTest" CssClass="btnTeoWords" Text="测试" OnClick="btnTest_Click" runat="server" OnClientClick="return checkCondition();"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnSave" CssClass="btnTeoWords" Text="保存" OnClick="btnSave_Click" runat="server" OnClientClick="return checkCondition();"/>
                </td>
            </tr>
            <asp:Label ID="lbResult" runat="server"></asp:Label>
           
        </table>
    </form>
</body>
</html>
