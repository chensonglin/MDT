<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Title="五洲在线数据交换平台-数据库配置" CodeBehind="DatabaseBrowser.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Database.DatabaseBrowser" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<style type="text/css">
          #bgDiv   /*页面中实现等待提示的div样式*/
    {
	    position:absolute;
	    background-color:White;
	    filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);
	    opacity:0.5;
	    left:0;
	    top:0;
	    z-index:10000;
	    width:1%;
    }
    #messagediv  /*页面中实现等待提示的div样式*/
    {
	    position:absolute;
	    margin:150px auto;
	    width:300px;
	    height:60px;
	    text-align:center;
	    filter:alpha(Opacity=80);
	    -moz-opacity:0.5;
	    opacity: 0.5;
	    background-color:#FFE7A2; /*#C6DDFC  #D4E7FF #F3FAFA #EAF4FF #FFE7A2 #ffffff*/
	    z-index:100011;
	    font-size:larger;
	    font-weight:bolder;
        top: 104px;
        left: 580px;
        border:1px #F4AA2E solid; /*#A3C0E8 #F4AA2E  #D4E7FF*/
                }
</style>
    <script language="javascript" type="text/javascript">
     function SingleSelect(o) {
         var index = o.rowIndex; //获取行的索引值
         index = index - 1;
         var lblID = document.getElementById("MainContent_Repeater1_lblID_"+index);//获取选中行内的  lblID
         var id = lblID.innerHTML;  //获取记录id
         if (id != "") {
             var selectID = document.getElementById("<%= hiddenSelectID.ClientID %>");
             selectID.value = id;
         }
     }
     //选择一条记录
     function checkCondition(type) {
         //return false;
            var selectID = document.getElementById("<%= hiddenSelectID.ClientID %>").value;
            if (type == "Delete") {
                 if (selectID == "") {
                     alert("请选择需要删除的ID.");
                     return false;
                 }
                 if (confirm("是否删除所选项？")) {
                     return true;
                 }
             }
             if (type == "Modify") {
                 if (selectID == "") {
                     alert("请选择需要修改的ID.");
                     return false;
                 }
                 show("hideDataBase","iframeDataBase","DatabaseInput.aspx?TYPE=MODIFY" + "&ID=" + selectID);
             }
             if (type == "Add") {
                 show("hideDataBase", "iframeDataBase", "DatabaseInput.aspx?TYPE=ADD");
             }
         }
         //刷新本页面
         function RefreshWindows() {
             var linkButtonRefresh = document.getElementById("<%=LinkButton1.ClientID%>");
             eval(linkButtonRefresh.href);
         }
    </script>
    <!--数据区开始-->
    
      <!--表顶分页开始-->
      <div class="rightPosition mb5" align="right" style="margin-top:-20px;">
          <a id="lbtnAdd" runat="server" onclick="checkCondition('Add');">新增</a>&nbsp;&nbsp;
          <a id="lbtnModify" runat="server" onclick="checkCondition('Modify');">修改</a>&nbsp;&nbsp;
          <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnDelete_Click" OnClientClick="return checkCondition('Delete')">删除</asp:LinkButton>
      </div>
      <div id="dataGrid" style=" margin:0px 0px 0px 0px;"> 
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <asp:Timer id="timer1" Interval="1" runat="server" ontick="timer1_Tick">
        </asp:Timer>
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:UpdateProgress runat="server">
                <progresstemplate>
                    <div id="bgDiv" class="bgDiv" style="height:230px"></div>
                       <div id="messagediv">
                        <table>
                        <tr>
                            <td class="style1"><br />&nbsp;&nbsp;&nbsp;<img src="../../../images/loading.gif" border=0 />&nbsp;&nbsp;&nbsp; 数据正在加载中，请稍候........</td>
                            </tr>
                        </table>
                    </div>
                </progresstemplate>
                </asp:UpdateProgress>
           
      <asp:Repeater ID="Repeater1" runat="server" 
            onitemdatabound="Repeater1_ItemDataBound" >
        <HeaderTemplate>
         <table id="dataTable">
        <thead>
          <tr>
            <th>ID</th>
            <th>别名</th>
            <th>数据库类型</th>
            <th>服务名</th>
            <th>端口号</th>
            <th>数据库名</th>
            <th>用户名</th>
            <th>密码</th>
          </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
        <ItemTemplate>
          <tr onclick="SingleSelect(this)">
            <td>
                <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' CssClass="hide"></asp:Label>
            </td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.Alias")%></td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.DatabaseType")%></td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.Server")%></td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.Port")%></td>
            <td align="left"><%# DataBinder.Eval(Container, "DataItem.Database")%></td>
            <td align="left">******</td>
            <td align="left">******</td>
          </tr>
        </ItemTemplate>
        <FooterTemplate>
             </tbody>
        </table>
            
      <!--表底分页结束--> 
        </FooterTemplate>
        </asp:Repeater>
   
    <!--表底分页开始-->
      <div class="page" align="center">
        <ul class="rightPosition">
        <li>共有<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>条信息，当前
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
     </ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="timer1" EventName="Tick" />
            </triggers>
        </asp:UpdatePanel>
    </div>

        <input id="hiddenSelectID" type="hidden" name="hiddenSelectID" runat="server" />
         <asp:linkbutton id="LinkButton1" runat="server" OnClick="LinkButtonRefresh_Click" ></asp:linkbutton>
          <asp:Label ID="lbResult" runat="server"></asp:Label>
    
      <!---------------- 弹出窗口 ---------------------->
    <div id="hidebg"></div>
     <div id="hideDataBase" class="openDiv modalInfor" style="width:390px; height:336px;">  
      <h1 class="modalHeader"> <b class="fl pl10">数据库编辑</b> <span class="fr modalClose" onclick="closeDiv2('hideDataBase','iframeDataBase')"></span> </h1>  
      <div class="modalBody" style=" width:388px; height:336px;">  
        <div class="modalContent" style="height:290px;" >
            <iframe id="iframeDataBase" src="" frameborder="0" scrolling="no" width="99%" height="286px" ></iframe>
        </div>  
      </div>  
     </div>
</asp:Content>
