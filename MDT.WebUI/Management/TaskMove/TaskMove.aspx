<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="TaskMove.aspx.cs" Inherits="MDT.WebUI.Management.TaskMove.TaskMove" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<style type="text/css">
    .ShowTaskdiv  /*下拉框显示选项的div*/
	{
		position:absolute;
		height:auto;
		max-height:249px;
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
    function makeSure() {
        if (confirm('是否确定迁移！')) {
            return true;
        }
        return false;
    }

    $(function () {//构建下拉列表框
        $('.mtd').PromptInputSelect({
            L: 0, //下拉选型的左偏移量，默认为0
            T: 0, //下拉选型的上偏移量，默认为0
            W: 20, //下拉选型的宽偏移量，默认为0
            N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
            beforehand: true,  //加载时生成下拉节点
            liClass: 'liA12', 	//选项li的类，默认为5
            liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
            liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
            conDiv: 'ShowTaskdiv', //下拉选项外围的类，默认为conDiv
            callback: GetID
        });
    })
    function GetID() {
        document.getElementById('MainContent_txtDataBaseID').value = $('#MainContent_txtDataBase').next().val();
    }
</script>
     <div id="search">
     <table width="100%">
     <tr>
        <td align="right" style="width:120px;">目标数据库：</td>
        <td align="left">
            <input id="txtDataBase" type="text" class="textField30 mtd" value="全部" to="MainContent_hdb" runat="server"/>
            
            <input id="txtDataBaseID" class="hide" type="text" runat="server" value="0"/>
            
             
        </td>
        <td align="right">
            <asp:LinkButton ID="lbtnMove" CssClass="aBtn" runat="server" 
                onclick="lbtnMove_Click" OnClientClick="return makeSure()"><span><em>迁移</em></span></asp:LinkButton>
        </td>
     </tr>
     </table>
     </div>
     <div>
      <asp:Repeater ID="Repeater1" runat="server" 
             onitemdatabound="Repeater1_ItemDataBound"  >
        <HeaderTemplate>
         <table id="dataTable">
        <thead>
          <tr>
            <th>选择</th>
            <th>序号</th>
            <th>任务名称</th>
            <th>任务描述</th>
            <th>操作人</th>
            <th>创建日期</th>
            <th>时间间隔</th>
            <th>是否启用</th>
          </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' CssClass="hide"></asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="lblTaskName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName")%>'></asp:Label>
             </td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.Note")%></td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.EUser.UserName")%></td>
            <td align="left"><%#FormatDate(DataBinder.Eval(Container, "DataItem.OperationDate"))%></td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.Interval")%></td>
            <td>
                <asp:Label ID="lblEnable" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Enable")%>' Visible="true"></asp:Label>
            </td>
          </tr>
        </ItemTemplate>
        <FooterTemplate>
             </tbody>
        </table>
            
      <!--表底分页结束--> 
        </FooterTemplate>
        </asp:Repeater>
     </div>
    <!--表底分页开始-->
     <div class="page" align="center">
        <ul class="rightPosition">
        <li>共有
        <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
        条信息，当前
        <asp:Label ID="lblCurrent" runat="server" Text="0"></asp:Label>/
        <asp:Label ID="lblCountPage" runat="server" Text="0"></asp:Label>页</li>
        <li><webdiyer:AspNetPager ID="AspNetPager1" runat="server" UrlPaging="false" OnPageChanged="AspNetPager1_PageChanged" AlwaysShow="True"></webdiyer:AspNetPager></li>
        <li>转到</li>
            <li><asp:TextBox ID="txtPageForSearch" CssClass="PIC" runat="server"></asp:TextBox></li>
            <li>页</li>
             <li>
                <asp:Button ID="btnSearchByPage" CssClass="SBC" runat="server" Text="go" 
                    onclick="btnSearchByPage_Click" />
            </li>
        </ul>
	 </div>
     <input type="hidden" id="hdb" runat="server" name="hdb" />
</asp:Content>

