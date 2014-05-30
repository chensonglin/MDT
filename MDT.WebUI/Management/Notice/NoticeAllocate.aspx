<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="NoticeAllocate.aspx.cs" Inherits="MDT.WebUI.Management.Notice.NoticeAllocate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
<script type="text/javascript">
    $(function () {
        $('.taskType').CustomSelect({
            L: 0, //下拉选型的左偏移量，默认为0
            T: 1, //下拉选型的上偏移量，默认为0
            W: 20, //下拉选型的宽偏移量，默认为0
            N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
            liClass: 'liA12', 	//选项li的类，默认为5
            liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
            liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
            conDiv: 'ShowTaskdiv', //下拉选项外围的类，默认为conDiv
            callback: GetTaskName
        });
        $('.taskName').CustomSelect({
            L: 0, //下拉选型的左偏移量，默认为0
            T: 1, //下拉选型的上偏移量，默认为0
            W: 20, //下拉选型的宽偏移量，默认为0
            N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
            liClass: 'liA12', 	//选项li的类，默认为5
            liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
            liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
            conDiv: 'ShowTaskdiv' //下拉选项外围的类，默认为conDiv
        });
        $('.noticeModel').CustomSelect({
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
    function GetTaskName() {
        var linkButtonRefresh = document.getElementById("MainContent_lbtnTaskTypeSelect");
        eval(linkButtonRefresh.href);
    }
    //全选任务的方法
    function ckAll(obj,className) {
        var boolC = $(obj).attr('checked');
        $('.' + className + ' input').each(function () {if (!this.disabled) this.checked = boolC; });
    }
    ///保存后取消原来选中的复选框
    function cancelChecked() {
        $('.ckReceiverEmail input').each(function () { if (!this.disabled) this.checked = false; });
        $('.ckReceiverPhone input').each(function () { if (!this.disabled) this.checked = false; });
    }



    function SingleSelect(o) {
        var index = o.rowIndex; //获取行的索引值
        index = index - 1;
        var lblID = document.getElementById("MainContent_Repeater1_lblID_" + index); //获取选中行内的  lblID
        var id = lblID.innerHTML;  //获取记录id
        if (id != "") {
            var selectID = document.getElementById("MainContent_hiddenUid");
            selectID.value = id;
        }
    }
    //选择一条记录
    function checkCondition(type) {
        var selectID = document.getElementById("MainContent_hiddenUid").value;
        if (type == "Delete") {
            if (selectID == "") {
                alert("请选择需要删除的ID.");
                return false;
            }
            if (confirm("本次操作将同时删除与该人员相关的预警通知信息，确定删除所选项？")) {
                return true;
            }
        }
        if (type == "Modify") {
            if (selectID == "") {
                alert("请选择需要修改的ID.");
                return;
            }
            show("hideReceiver", "iframeReceiver", "AddReceiver.aspx?uid=" + selectID);
        }
        if (type == "Add") {
            show("hideReceiver", "iframeReceiver", "AddReceiver.aspx");
        }
    }
    //刷新本页面
    function RefreshWindows() {
        var linkButtonRefresh = document.getElementById("MainContent_lbtnRefresh");
        eval(linkButtonRefresh.href);
    }
</script>
    <div class="rightPosition" align="right" style="margin-top:-20px;">
        <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="hide" onclick="lbtnRefresh_Click">刷新</asp:LinkButton>
        <a id="addReceiver" runat="server" href="javascript:checkCondition('Add')">新增人员</a>&nbsp;&nbsp;
        <a id="modifyReceiver" runat="server" href="javascript:checkCondition('Modify')">修改人员</a>&nbsp;&nbsp;
        <asp:LinkButton ID="lbtnDelReceiver" 
              OnClientClick="return checkCondition('Delete')" runat="server" 
              onclick="lbtnDelReceiver_Click">删除人员</asp:LinkButton>
    </div>
     <div>
      <table cellpadding="5px" width="100%">
       <tr>
        <td valign="top" style="width:25%">
          <div style="text-align:left; margin-bottom:5px;">
            任务类别： <input id="txtTaskType" type="text" class="textField26 taskType" value="" to="MainContent_hideTaskType" runat="server"/>
            <asp:LinkButton ID="lbtnTaskTypeSelect" runat="server" 
                onclick="lbtnTaskTypeSelect_Click" CssClass="hide"></asp:LinkButton>
          </div>
            <table id="dataTable" class="dataTable" style="width:100%">
            <thead><tr><td align="left"><input id="ckTaskAll" type="checkbox" onclick="ckAll(this,'ckTask')" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;全选</td></tr></thead>
            </table>
            <div style="overflow-y:scroll; max-height:400px;">
                <table id="dataTable" style="width:100%; margin:-1px 0px 0px 0px;">
                  <tbody>
                    <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <tr>
                        <td align="left">
                            <asp:CheckBox ID="ckTask" CssClass="ckTask" runat="server" />&nbsp;&nbsp;
                            <asp:Label ID="lblTaskName" runat="server" Text='<%#FormatString(DataBinder.Eval(Container,"DataItem.TaskName").ToString(),22)%>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.TaskName") %>'></asp:Label>
                            <asp:Label ID="lblTaskId" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.TaskId")%>' CssClass="hide"></asp:Label>
                        </td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                  </tbody>
               </table>
            </div> 
        </td>
        <td valign="top" colspan="2">
            <table id="dataTable" class="dataTable">
            <thead>
            <tr>
            <td ><input id="ckAllEmail" onclick="ckAll(this,'ckReceiverEmail')" type="checkbox" />&nbsp;&nbsp;邮件</td>
            <td ><input id="ckAllPhone" onclick="ckAll(this,'ckReceiverPhone')" type="checkbox" />&nbsp;&nbsp;短信</td>
            <td>序号</td>
            <td>姓名</td>
            <td>手机号码</td>
            <td>邮箱</td>
            <td>备注</td>
            <td>是否启用</td>
            <td><asp:LinkButton ID="lbtnNotice" ToolTip="对选中的多个人员设置预警通知" runat="server" 
                    onclick="lbtnNotice_Click">通知</asp:LinkButton></td>
            <td><asp:LinkButton ID="lbtnExit" ToolTip="对选中的多个人员取消预警通知" runat="server" 
                    onclick="lbtnExit_Click">取消</asp:LinkButton></td>
            </tr>
            </thead>
            <tbody>
           <asp:Repeater ID="Repeater1" runat="server" 
                 onitemdatabound="Repeater1_ItemDataBound" 
                    onitemcommand="Repeater1_ItemCommand">
             <ItemTemplate>
                 <tr onclick="SingleSelect(this)">
                   <td>
                       <asp:CheckBox ID="ckReceiverEmail" CssClass="ckReceiverEmail" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                   <td>
                       <asp:CheckBox ID="ckReceiverPhone" CssClass="ckReceiverPhone" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                   <td>
                      <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label>
                      <asp:Label ID="lblID" runat="server" CssClass="hide" Text='<%#DataBinder.Eval(Container,"DataItem.uid")%>'></asp:Label>
                     <%-- <asp:Label ID="lblIsAllocate" runat="server" CssClass="hide" Text='<%#DataBinder.Eval(Container,"DataItem.isallocate") %>'></asp:Label>--%>
                   </td>
                   <td><%#DataBinder.Eval(Container,"DataItem.name") %></td>
                   <td><asp:Label ID="lblPhone" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.phone") %>'></asp:Label></td>
                   <td><asp:Label ID="lblEmail" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.email") %>'></asp:Label></td>
                   <td><%#DataBinder.Eval(Container,"DataItem.remark") %></td>
                   <td><asp:Label ID="lblEnable" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.enable")%>'></asp:Label></td>
                   <td><asp:LinkButton ID="lbtnNotice" ToolTip="设置预警通知" CommandName="Notice" runat="server">通知</asp:LinkButton></td>
                   <td><asp:LinkButton ID="lbtnCancel" ToolTip="取消预警通知" CommandName="Cancel" runat="server">取消</asp:LinkButton></td>
                 </tr>
             </ItemTemplate>
           </asp:Repeater>
           </tbody>
          </table>
          <div class="page" align="center" style="margin:0px 0px 0px 0px;" >
         </div>
        </td>
     </tr>
     </table>
     </div>
     <input id="hiddenUid" value="" runat="server" type="hidden" />
     <input id="hideTaskType" type="hidden" value="" runat="server" />
     <!---------------- 弹出窗口 ---------------------->
    <div id="hidebg"></div>
     <div id="hideReceiver" class="openDiv modalInfor" style="width:360px; height:246px;">  
      <h1 class="modalHeader"> <b class="fl pl10">预警通知人员编辑</b> <span class="fr modalClose" onclick="closeDiv('hideReceiver','iframeReceiver')"></span> </h1>  
      <div class="modalBody" style=" width:358px; height:246px;">  
        <div class="modalContent" style="height:200px;" >
            <iframe id="iframeReceiver" src="" frameborder="0" scrolling="no" width="99%" height="196px" ></iframe>
        </div>  
      </div>  
     </div>
</asp:Content>
