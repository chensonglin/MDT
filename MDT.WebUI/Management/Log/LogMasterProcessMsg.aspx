<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="LogMasterProcessMsg.aspx.cs" Title="五洲在线数据交换平台-查看执行过程" Inherits="MDT.WebUI.Management.Log.LogMasterProcessMsg" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 
  <script language="javascript" type="text/javascript">
      function ShowDetail(id) {
          var url;
          url = "LogMessageDetail.aspx?ID=" + id + "&TYPE=DataMessageProcess";
          show('hideDataMessage','iframeDataMessage',url);
      }
  </script>
        <div>
            <asp:Repeater ID="Repeater1" runat="server" 
                onitemcommand="Repeater1_ItemCommand">
            <HeaderTemplate>
             <table id="dataTable" style=" width:100%;">
                <thead>
                  <tr>
                    <th>ID</th> 
                    <th style="width:8%;">处理阶段</th>
                    <th>执行状态</th>
                    <th>数据信息</th>
                    <th>开始时间</th>
                    <th>结束时间</th>
                    <th style="width:56%;">运行信息</th>
                  </tr>
                </thead>
                <tbody>
            </HeaderTemplate>
            <ItemTemplate>
              <tr id="rTr" runat="server">
            <td>
                <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
            </td>
            <td><%# DataBinder.Eval(Container, "DataItem.Stage")%></td>
            <td>
                <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.State")%>'></asp:Label>
            </td>
            <td>
                <asp:LinkButton ID="lbtnData" CommandName="DataMessage" runat="server">DataMessage...</asp:LinkButton>
            </td>
            <td><%# DataBinder.Eval(Container, "DataItem.StartTime")%></td>
            <td><%# DataBinder.Eval(Container, "DataItem.EndTime")%></td>
            <td><%# DataBinder.Eval(Container, "DataItem.RunInfo")%></td>
          </tr>
            </ItemTemplate>
            <FooterTemplate>
                 </tbody>
            </table>
            
            </FooterTemplate>
            </asp:Repeater>
        </div>
       
    <div id="hidebg"></div>
    <div id="hideDataMessage" class="openDiv modalInfor" style="width:760px; height:480px;">  
      <h1 class="modalHeader"> <b class="fl pl10">数据信息</b> <span class="fr modalClose" onclick="closeDiv2('hideDataMessage','iframeDataMessage')"></span> </h1>  
      <div class="modalBody" style="width:758px; height:480px;">  
        <div class="modalContent" style="height:434px;">
            <iframe id="iframeDataMessage" src="" frameborder="0" scrolling="auto" width="100%" height="431px" ></iframe>
        </div>  
      </div>  
     </div>
 </asp:Content>
