<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SourceConfig.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Task.SourceConfig" %>

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
    <script type="text/javascript" src="/js/jQuery.PromptInputSelect.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>

    <style type="text/css">
    .ShowTaskdiv  /*下拉框显示选项的div*/
	{
		position:absolute;
		height:auto;
		max-height:90px;
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
     <script type="text/javascript">
         $(function () {
             $('#dataTable').trOddHilight();
             $('#dataTable').trClick();
         })
         function checkInput() {
             var txtName = G("txtName");
             if (txtName.value.Trim() == "") {
                 txtName.focus();
                 alert("请输入名称！");
                 return false;
             }
             var txtCommandType = G("txtCommandType");
             if (txtCommandType.value.Trim() == "") {
                 txtName.focus();
                 txtCommandType("请输入CommandType！");
                 return false;
             }
             var txtContent = G("txtContent");
             if (txtContent.value.Trim() == "") {
                 txtContent.focus();
                 alert("请输入内容！");
                 return false;
             }
             var txtSourceLink = G("txtSourceLink");
             if (txtSourceLink.value.Trim() == "") {
                 txtSourceLink.focus();
                 alert("请输入SourceLink！");
                 return false;
             }
             var txtSourceType = G("txtSourceType");
             if (txtSourceType.value.Trim() == "") {
                 txtSourceType.focus();
                 alert("请输入SourceType！");
                 return false;
             }
             return true;
         }

         //关闭本页同时刷新父页面
         function openerRefresh() {
             parent.bindData();
             parent.hide('hideCommand', 'iframeCommand');
         }

         function checkClose() {
             if (confirm('关闭本页面并不保存数据，是否确定？')) {
                 parent.hide('hideCommand', 'iframeCommand');
             }
         }
         //让滚动条显示在页面最底部
         function scrollBottom(){
             G('divScroll').scrollTop = G('divScroll').scrollHeight;
         }

         $(function () {
             $('.mtd').PromptInputSelect({
                 L: 0, //下拉选型的左偏移量，默认为0
                 T: 1, //下拉选型的上偏移量，默认为0
                 W: 20, //下拉选型的宽偏移量，默认为0
                 N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                 beforehand: true,  //加载时生成下拉节点
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                 conDiv: 'ShowTaskdiv'	//下拉选项外围的类，默认为conDiv
             });
             //alert($($('#txtDataBaseType').parent('wrap').find('input')[1]).val());
         })
     </script>
<!--页内脚本结束-->
</head>
<body style=" margin:0px 20px 0px 20px;">
    <form id="form1" runat="server">
    <div>
        <div id="divScroll" style="width:870px; overflow-Y:auto; overflow-X:hidden; max-height:450px; margin-top:10px;">
            <table>
            <tr>
                <td align="right" >名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 称：</td>
                <td align="left"><asp:TextBox ID="txtName" runat="server" CssClass="textField44"></asp:TextBox></td>
                <td align="right" style="width:100px;">类型：</td>
                <td align="left">
                    <asp:TextBox ID="txtCommandType" runat="server" CssClass="textField"></asp:TextBox>
                </td>
                <td style=" width:100px;" align="right"> 结果集：</td>
                <td align="left">
                    <%--<input type="text" id="txtHasResult" runat="server" value='false' readonly="readonly" class="textField60" />
                    <img src="/images/selectRight.png" alt="" onclick="ShowDiv('txtHasResult','hideData')" style="margin-left:-3px;" />--%>
                    <input id="txtHasResult" name="" type="text" class="textField60 mtd" value='false' to="hideData" runat="server"/>
                </td>
            </tr>
            <tr>
        
            </tr>
            <tr>
                <td align="right" valign="top"> 内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 容：</td>
                <td align="left" colspan="5" style=" width:780px;">
                    <textarea id="txtContent" rows="12" style="width:100%;" runat="server" class="textarea"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="参&nbsp;数&nbsp;值：" ToolTip="ParameterValue"></asp:Label></td>
                <td  align="left" colspan="5">
                    <asp:TextBox ID="txtPValue" runat="server" CssClass="textFieldFull"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="参数来源：" ToolTip="ParameterValueFrom"></asp:Label></td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtPFrom" runat="server" CssClass="textFieldFull" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="对象名称：" ToolTip="ParameterValueOjbectName"></asp:Label></td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtPOjbectName" runat="server" CssClass="textFieldFull" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">参数集：</td>
                <td colspan="5">
                    <div class="rightPosition">
                        <asp:LinkButton ID="lbtnAddP" runat="server" CssClass="aBtn" 
                            onclick="lbtnAddP_Click"><span><em>新增参数</em></span></asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <asp:Repeater ID="Repeater1" runat="server" 
                        onitemcommand="Repeater1_ItemCommand">
                    <HeaderTemplate>
                        <table id="dataTable" style="width:100%;">
                        <thead>
                        <th>参数名称</th>
                        <th>参数类型</th>
                        <th>删除</th>
                        </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style=" height:30px;">
                            <td valign="middle">            
                                <asp:TextBox ID="txtPName" runat="server" CssClass="textFieldFull" Text='<%#DataBinder.Eval(Container,"DataItem.Name") %>'></asp:TextBox>
                            </td>
                            <td valign="middle">
                                <asp:TextBox ID="txtPType" runat="server" CssClass="textFieldFull" Text='<%#DataBinder.Eval(Container,"DataItem.Type") %>'></asp:TextBox>
                            </td>
                            <td style="width:60px">
                                <asp:LinkButton ID="lbtnDelete" CommandName="Delete" runat="server" ToolTip="删除"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td align="right">来源链接：</td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtSourceLink" runat="server" CssClass="textField30"></asp:TextBox>
                </td>
                <td align="right">
                    来源类型：
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtSourceType" runat="server" CssClass="textField26"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="right" style="height:40px;">
                    <asp:LinkButton ID="lbtnSave" CssClass="aBtn" runat="server" 
                         OnClientClick="return checkInput();" onclick="lbtnSave_Click"><span><em>保存</em></span></asp:LinkButton>&nbsp;&nbsp;
                    <a class="aBtn" href="javascript:checkClose();" ><span><em>关闭</em></span></a>
                </td>
            </tr>
            </table>

        </div>
    </div>
    <div id="divShow" class="ShowTaskdiv" runat="server"></div>
    <input id="hideName" type="hidden" runat="server" value="" />
    <input id="hideData" type="hidden" runat="server" value="0*true|0*false" />
    <input id="hideFileName" type="hidden" runat="server" />

    </form>
</body>
</html>
