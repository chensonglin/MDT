<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskAssignment.aspx.cs" MasterPageFile="~/Site.master" Title="五洲在线数据交换平台-任务分配" Inherits="MDT.WebUI.Management.Configuration.Assignment.TaskAssignment" %>

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
	    -moz-opacity:0.8;
	    opacity: 0.8;
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
     //选择一条记录
     function SingleSelect(id, o) {
         if (id != "") {
             var selectID = document.getElementById("<%= hiddenSelectID.ClientID %>");
             selectID.value = id;
         }
     }

     function refresh() {
        eval(G('MainContent_lbtnRefresh').href);
     }
 </script>
    
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        

        <div class="rightPosition" align="right" style="margin-top:-20px;">
            <%--<a id="refresh" href="TaskAssignment.aspx" >刷新</a>&nbsp;&nbsp;--%>
            <asp:LinkButton ID="lbtnRefresh" runat="server" onclick="lbtnRefresh_Click" CssClass="hide">刷新</asp:LinkButton>
            <a id="aClientManager" runat="server" href="javascript:show('hideClientManage','iframeClientManage','/Management/Configuration/Assignment/ClientManage.aspx')">客户端管理</a>&nbsp;&nbsp;
            <a id="aTaskAlloat" runat="server" href="javascript:show('hideAllotTask','iframeAllotTask','/Management/Configuration/Assignment/AllotTask.aspx')">任务分配</a>
            
        </div>
        <div id="dataGrid">
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
                            <td class="style1"><br />&nbsp;&nbsp;&nbsp;<img src="/images/loading.gif" border=0 />&nbsp;&nbsp;&nbsp; 数据正在加载中，请稍候........</td>
                            </tr>
                        </table>
                    </div>
         
         </progresstemplate>
        </asp:UpdateProgress>
        <div style="padding:0px 0px 0px 0px">
    <table id="tb" cellpadding="0" cellspacing="0" border="0" width="100%">
       <tr>
            <td style="width:8%" valign="top">
            
                <table id="cflDataTable">
                    <thead>
                        <tr>
                        <th>客户端</th>
                        </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td>
                            <div style="OVERFLOW-Y:auto;vertical-align=text-top; max-height:800px;" align="left;">
                            <asp:Repeater ID="Repeater2" runat="server" 
                                onitemcommand="Repeater2_ItemCommand">
                            <HeaderTemplate><table style="width:90%"></HeaderTemplate>
                            <ItemTemplate>
                                <tr><td>
                                    <asp:LinkButton ID="lbtnClient" Font-Overline="false" Width="140px" CssClass="cflA" runat="server" CommandName="clientCommand" Text='<%# DataBinder.Eval(Container, "DataItem.name")%>'></asp:LinkButton>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id")%>' Visible="false"></asp:Label>
                                </td></tr>
                            </ItemTemplate>
                            <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </td>
            <td style="width:92%;" valign="top" align="left">
                <div style="margin-left:5px;">
                   <asp:Repeater ID="Repeater1" runat="server" 
                        onitemdatabound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                       <table id="dataTable" width="100%" style="border-top:none;">
                       <thead>
                        <tr>
                       <th>
                            ID    
                       </th>
                       <th>任务名称</th>
                       <th>任务描述</th>
                       <th width="30px">任务类型</th>
                       </tr></thead>
                       <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td width="10%">
                            <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>' Visible="false"></asp:Label>
                        </td>
                        <td align="left" width="45%"><%# DataBinder.Eval(Container, "DataItem.TaskName")%></td>
                        <td align="left" width="35%"><%# DataBinder.Eval(Container, "DataItem.Note")%></td>
                        <td align="left" width="15%"><%# DataBinder.Eval(Container, "DataItem.Type")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>

                    <div class="page" align="center" style="margin:0px 0px 0px 0px;" >
                        <ul class="rightPosition">
                        <li>共有<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>条信息，
                        当前
                        <asp:Label ID="lblCurrentPage" runat="server" Text="0"></asp:Label>/
                            <asp:Label ID="lblPageCount" runat="server" Text="0"></asp:Label>页</li>
                        <li>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" UrlPaging="false" 
                                 AlwaysShow="True" 
                                ShowPageIndexBox="Never" onpagechanged="AspNetPager1_PageChanged"></webdiyer:AspNetPager>
                        </li>
        
                            <li>转到</li>
                            <li>
                                <asp:TextBox ID="txtPageForSearch" CssClass="PIC" runat="server"></asp:TextBox>
                            </li>
                            <li>页</li>
                            <li>
                                <asp:Button ID="btnSearchByPage" CssClass="SBC" runat="server" Text="go" 
                                    onclick="btnSearchByPage_Click"/>
                            </li>
                        </ul>
	                </div>
                    </div>
            </td>
        </tr>
       
        <input id="hiddenSelectID" type="hidden" name="hiddenSelectID" runat="server" />
          <asp:Label ID="lbResult" runat="server"></asp:Label>
    </table> 
    </div>
      
    </ContentTemplate>
    <triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
    </triggers>
        </asp:UpdatePanel>
        </div>
      
      <!--------         弹出Div            ---------->
    <div id="hidebg"></div>
    <div id="hideClientManage" class="openDiv modalInfor" style="width:860px; height:542px;">  
      <h1 class="modalHeader"> <b class="fl pl10">客户端管理</b> <span class="fr modalClose" onclick="closeDiv3('hideClientManage','iframeClientManage',refresh);"></span> </h1>  
      <div class="modalBody" style=" width:858px; height:536px;">  
        <div class="modalContent" style="height:496px;" >
            <iframe id="iframeClientManage" src="" frameborder="0" scrolling="no" width="99%" height="486px" ></iframe>
        </div>  
      </div>  
     </div>
    <div id="hideAllotTask" class="openDiv modalInfor" style="width:860px; height:542px;">  
      <h1 class="modalHeader"> <b class="fl pl10">任务分配</b> <span class="fr modalClose" onclick="closeDiv3('hideAllotTask','iframeAllotTask',refresh);"></span> </h1>  
      <div class="modalBody" style=" width:858px; height:536px;">  
        <div class="modalContent" style="height:496px;" >
            <iframe id="iframeAllotTask" src="" frameborder="0" scrolling="no" width="99%" height="486px" ></iframe>
        </div>  
      </div>  
     </div>
</asp:Content>
