<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskBrowser.aspx.cs"  MasterPageFile="~/Site.master" Title="五洲在线数据交换平台-任务分配"  Inherits="MDT.WebUI.Management.Configuration.Task.TaskBrowser" %>

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
	    background-color:#FFE7A2;
	    z-index:100011;
	    font-size:larger;
	    font-weight:bolder;
        top: 109px;
        left: 511px;
        border:1px #F4AA2E solid; 
                }
   
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
    .imBtn
    {
        width:20px;
        height:23px;
        display:inline-block;
	    vertical-align:bottom;
	    +vertical-align:text-bottom;}	
</style>
    <script language="javascript" type="text/javascript">
     function SingleSelect(o) {
         if (o == "") {
             var selectID = document.getElementById("MainContent_hiddenSelectID");
             selectID.value = "";
        }
        else {
            var lblID = o.id.replace("TR", "span");
            var lbl = document.getElementById(lblID); //获取选中行内的  lblID
            var id = lbl.innerHTML;  //获取记录id
            if (id != "") {
                var selectID = document.getElementById("MainContent_hiddenSelectID");
                selectID.value = id;
            }
        }
     }
     //选择一条记录
     function checkCondition(type) {
         var selectID = document.getElementById("MainContent_hiddenSelectID").value;
         if (type == "Delete") {
             if (selectID == "") 
             {
                 alert("请选择需要删除的ID.");
                 return false;
             }
             else
             {
                 if (confirm("是否删除所选项？")) {
                     return true;
                 }
             }
         }
         if (type == "Modify") {
             if (selectID == "") {
                 alert("请选择需要修改的ID.");
                 return false;
             }
             window.location.href = "TaskAdd.aspx?Id="+selectID;
         }
         if (type == "ModifyXML") {
             if (selectID == "") {
                 alert("请选择需要修改的ID.");
                 return false;
             }
             show('hideModifyXML', 'iframeModifyXML', 'TaskModify.aspx?ID=' + selectID);
         }
         if (type == "xml") {
             if (selectID == "") {
                 alert("请选择需要预览的ID.");
                 return false;
             }
             show('hideView', 'iframeView', 'XMLFile.aspx?ID=' + selectID);
         }
         return true;
     }

     //刷新本页面
     function RefreshWindows() {
         var linkButtonRefresh = document.getElementById("<%=LinkButton1.ClientID%>");
         eval(linkButtonRefresh.href);
     }
     //是否启用
     function Enable(id, state, o) {
        var strConfirm = "";
        if (state == 0) {
            strConfirm = "启用所选项？";
        }
        else {
            strConfirm = "不启用所选项？";
        }
         if (confirm(strConfirm)) {
             var enable = document.getElementById("<%= hiddenEnable.ClientID %>");
             enable.value = id + "^" + state;
             var linkButtonEnable = document.getElementById("<%=linkButtonEnable.ClientID%>");
             eval(linkButtonEnable.href);
         }
         else {
             if (state==1)
                 o.checked = true;
             else
                 o.checked = false; 
         }
     }
     //验证输入的查询条件
     function checkSearch() {
         if (G('MainContent_txtStartTime').value != "" && G('MainContent_txtEndTime').value != "") {
             var strSDate = G('MainContent_txtStartTime').value.replace("-", "/");
             var startdate = new Date(strSDate);
             var strEDate = G('MainContent_txtEndTime').value.replace("-", "/");
             var enddate = new Date(strEDate);
             if (startdate > enddate) {
                 alert('开始日期不能大于截止日期，请重新输入！');
                 G('MainContent_txtStartTime').value = "";
                 G('MainContent_txtEndTime').value = "";
                 G('MainContent_txtStartTime').focus();
                 return false;
             }
         }
         document.getElementById("<%= hiddenSelectID.ClientID %>").value = "";
         return true;
     }
     function searchDisplay() {//显示查询条件
         displayLbtn("MainContent_lbtnRTaskName", "MainContent_txtRTaskName", "任务名称=\"");
         displayLbtn("MainContent_lbtnOperater", "MainContent_txtOperater", "操作人=\"");
         displayLbtn("MainContent_lbtnStartTime", "MainContent_txtStartTime", "开始时间=\"");
         displayLbtn("MainContent_lbtnEndTime", "MainContent_txtEndTime", "结束时间=\"");
         displayDivR();
     }
     function displayLbtn(lbtn, txt, str) {//显示查询条件
        var l = G(lbtn);
        var condition = G(txt).value;
        if (txt == "MainContent_txtRTaskName") {
            condition = condition.toUpperCase();
        }
        if (condition == "全部") {
            condition = "";
        }
         if (condition.Trim() == "") {
             l.style.display = "none";
         }
         else {
             l.innerHTML = str + condition + "\"";
            l.style.display = "inline";
         }
     }
     function displayDivR(){
        if (G("MainContent_lbtnRTaskName").style.display == "none" && G("MainContent_lbtnOperater").style.display == "none" && G("MainContent_lbtnStartTime").style.display == "none" && G("MainContent_lbtnEndTime").style.display == "none" ) {
            G("MainContent_divR").style.display = "none";
        }
        else {
            G("MainContent_divR").style.display = "block";
        }
    }
    var noneTaskName = 1;
    function displayLbtnByClick(lbtn, txt, lbtn2) {
        document.getElementById("<%= hiddenSelectID.ClientID %>").value = "";
        G(lbtn).style.display = "none";
        if (txt == "MainContent_txtRTaskName") {
            noneTaskName = 2;
            G(txt).value = "全部";
        }
        else {
            G(txt).value = "";
        }
        displayDivR();
        eval(G(lbtn2).href);
    }
    function closeAll() {
        document.getElementById("<%= hiddenSelectID.ClientID %>").value = "";
        noneTaskName = 2;
        G("MainContent_txtRTaskName").value = "全部";
        G("MainContent_txtOperater").value = "";
        G("MainContent_txtStartTime").value = "";
        G("MainContent_txtEndTime").value = "";
        G("MainContent_divR").style.display = "none";
        eval(G("MainContent_hideLbtnCloseAll").href);
    }
    function DisplayDiv1() {
        var div1 = G("MainContent_div1");
        if (div1.style.display == "none") {
            div1.style.display = "block";
        }
        else {
            div1.style.display = "none";
            closeAll();
        }
    }

    $(function () {//初始化下拉列表
        document.getElementById('MainContent_div1').style.display = 'block';
        $('.rTaskName').CustomSelect({
            L: 0, //下拉选型的左偏移量，默认为0
            T: 1, //下拉选型的上偏移量，默认为0
            W: 20, //下拉选型的宽偏移量，默认为0
            N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
            liClass: 'liA12', 	//选项li的类，默认为5
            liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
            liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
            conDiv: 'ShowTaskdiv'	//下拉选项外围的类，默认为conDiv
        });
        document.getElementById('MainContent_div1').style.display = 'none';
    })

    //刷新页面
    function bindData(){
        G('MainContent_lbtnRefresh').click();
    }
    //显示或隐藏任务类别下的任务
    function displayTr(rownum) {
        $('#row_' + rownum).siblings('.child_row_' + rownum).toggle();
        var imgA = document.getElementById("imgA_" + rownum);
        var imgB = document.getElementById("imgB_" + rownum);
        if (imgA.style.display == "none") {
            imgA.style.display = "block";
            imgB.style.display = "none";
        }
        else {
            imgA.style.display = "none";
            imgB.style.display = "block";
        }
    }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="right" style=" margin:-20px 0px 5px 0px; ">
          <asp:LinkButton ID="lbtnRefresh" runat="server" onclick="lbtnRefresh_Click" CssClass="hide">刷新</asp:LinkButton>
          <a onclick="DisplayDiv1()">快速查询</a>&nbsp;&nbsp;
          <a href="../../Notice/NoticeAllocate.aspx" id="aNotice" runat="server">预警管理</a>&nbsp;&nbsp;
          <%--<asp:LinkButton ID="lbtnAdd" runat="server" onclick="lbtnAdd_Click"> /新增</asp:LinkButton>&nbsp;&nbsp;--%>
          &nbsp;&nbsp;<a href="javascript:show('hideAdd', 'iframeAdd', 'TaskNew.aspx')" runat="server" id="aAdd">新增</a>&nbsp;&nbsp;&nbsp;&nbsp;
          <%--<asp:LinkButton ID="lbtnModify" runat="server" onclick="lbtnModify_Click">修改</asp:LinkButton>&nbsp;&nbsp;--%>
          <a onclick="return checkCondition('ModifyXML');" id="aModifyXML" runat="server">修改</a>&nbsp;&nbsp;
          <a onclick="return checkCondition('xml');" id="aViewXML" runat="server">预览</a>&nbsp;&nbsp;
          <asp:LinkButton ID="lbtnDelete" runat="server" Visible="true"
              OnClick="btnDelete_Click" OnClientClick="return checkCondition('Delete');" >删除</asp:LinkButton>
      </div>

    <div id="divR" style="border:1px #FDB45B solid; padding:8px 8px 5px 8px; border-bottom:none; background-color:#FFF7E6; position:relative;" runat="server">
        您已选择的查询条件：&nbsp;&nbsp;&nbsp;&nbsp;
        <a ID="lbtnRTaskName" runat="server" title="点击可取消此查询条件" 
             class="ml15" onclick="displayLbtnByClick('MainContent_lbtnRTaskName','MainContent_txtRTaskName','MainContent_hideLbtnTaskName')"></a>
        <a ID="lbtnOperater" runat="server" title="点击可取消此查询条件" 
             class="ml15" onclick="displayLbtnByClick('MainContent_lbtnOperater','MainContent_txtOperater','MainContent_hideLbtnOperater')"></a>
        <a ID="lbtnStartTime" runat="server" title="点击可取消此查询条件" 
             class="ml15" onclick="displayLbtnByClick('MainContent_lbtnStartTime','MainContent_txtStartTime','MainContent_hideLbtnStartTime')"></a>
        <a ID="lbtnEndTime" runat="server" title="点击可取消此查询条件" 
             class="ml15" onclick="displayLbtnByClick('MainContent_lbtnEndTime','MainContent_txtEndTime','MainContent_hideLbtnEndTime')"></a>
        <div style="width:20px; position:absolute;right:0;top:5px;">
        <a ID="lbtnCloseAll" runat="server" onclick="closeAll();" >
                <img src="/images/searchClose.gif" alt="删除所有查询条件" />
        </a>
        </div>
            <asp:LinkButton ID="hideLbtnTaskName" onclick="lbtnRTaskName_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="hideLbtnOperater" onclick="lbtnOperater_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="hideLbtnStartTime" onclick="lbtnStartTime_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="hideLbtnEndTime" onclick="lbtnEndTime_Click" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="hideLbtnCloseAll" runat="server" 
              onclick="hideLbtnCloseAll_Click"></asp:LinkButton>
      </div>
    <div id="div1" runat="server"> 
      <div id="search">
          <table style="width:100%" cellpadding="0">
          <tr>
            <td align="right">任务名称：</td>
            <td align="left">
                <input id="txtRTaskName" name="" type="text" class="textSelect260 rTaskName" to="MainContent_hiddenTask" runat="server" value="全部" style="text-transform:uppercase"/>
            </td>
            <td align="right">操作人：</td>
            <td align="left"><input type="text" id="txtOperater" name="txtOperater" runat="server" class="textField26" /></td>
            <td align="right">操作时间：</td>
            <td align="left">
                <input type="text" id="txtStartTime" name="txtOperationDate" runat="server" class="textField14" onfocus="WdatePicker();" />
                至
                <input type="text" id="txtEndTime" name="txtOperationDate" runat="server" class="textField14" onfocus="WdatePicker();" />
            </td>
            <td>
                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="aBtn" 
                    onclick="lbtnSearch_Click" OnClientClick="return checkSearch()"><span><em>查询</em></span></asp:LinkButton></td>
          </tr>
          </table>
      </div>
      </div>
        
    <asp:Timer id="timer1" Interval="1"  runat="server" ontick="timer1_Tick" >
    </asp:Timer>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:UpdateProgress runat="server">
                <progresstemplate>
                    <div id="bgDiv" class="bgDiv" style="height:230px"></div>
                       <div id="messagediv">
                        <table>
                        <tr>
                            <td class="style1"><br />&nbsp;&nbsp;&nbsp;<img src="../../../images/loading.gif" border="0" alt="" />&nbsp;&nbsp;&nbsp; 数据正在加载中，请稍候........</td>
                            </tr>
                        </table>
                    </div>
                </progresstemplate>
                </asp:UpdateProgress>
       
       <div id="dataGrid" style=" margin:0px 0px 0px 0px;">
       <table width="100%">
         <tr>
           <td>
             <div id="divTaskList" runat="server"></div>
           </td>
         </tr>

       </table>
       </div>
    <!--表底分页开始-->
      <div class="page" align="center">
	  </div>
            </ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="timer1" EventName="Tick" />
                <asp:AsyncPostBackTrigger ControlID="lbtnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="hideLbtnTaskName" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="hideLbtnOperater" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="hideLbtnStartTime" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="hideLbtnEndTime" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="hideLbtnCloseAll" EventName="Click" />
            </triggers>
        </asp:UpdatePanel>

    <input id="hiddenSelectID" type="hidden" name="hiddenSelectID" runat="server" />
    <input id="hiddenEnable" type="hidden" name="hiddenEnable" runat="server" />
    <asp:linkbutton id="LinkButton1" runat="server" OnClick="LinkButtonRefresh_Click" ></asp:linkbutton>
    <asp:linkbutton id="linkButtonEnable" runat="server" OnClick="linkButtonEnable_Click" ></asp:linkbutton>
    <input id="hiddenTask" type="hidden" name="hiddenTask" runat="server" />   


    <!--------         弹出Div            ---------->
    <div id="hidebg"></div>
    <div id="hideView" class="openDiv modalInfor" style="width:820px; height:506px;">  
      <h1 class="modalHeader"> <b class="fl pl10">预览xml</b> <span class="fr modalClose" onclick="closeDiv2('hideView','iframeView')"></span> </h1>  
      <div class="modalBody" style="width:818px; height:506px;">  
        <div class="modalContent" style="height:460px;">
            <iframe id="iframeView" src="" frameborder="0" scrolling="no" width="100%" height="457px" ></iframe>
        </div>  
      </div>  
     </div>
    <div id="hideModifyXML" class="openDiv modalInfor" style="width:900px; height:566px;">
        <h1 class="modalHeader"> <b class="fl pl10">手动修改xml数据</b> <span class="fr modalClose" onclick="closeDiv('hideModifyXML','iframeModifyXML')" /></span> </h1> 
        <div class="modalBody" style="width:898px; height:566px;">  
        <div class="modalContent" style="height:520px;">
        <iframe id="iframeModifyXML" src="" frameborder="0" scrolling="no" width="100%" height="518px"></iframe>
        </div>
        </div>
    </div>
    <div id="hideMapping" class="openDiv modalInfor" style="width:760px; height:500px;">  
      <h1 class="modalHeader"> <b class="fl pl10">映射关系</b> <span class="fr modalClose" onclick="closeDiv2('hideMapping','iframeMapping')"></span> </h1>  
      <div class="modalBody" style="width:758px; height:500px;">  
        <div class="modalContent" style=" height:454px;">
            <iframe id="iframeMapping" src="" frameborder="0" scrolling="auto" width="100%" height="451px" ></iframe>
        </div>  
      </div>  
     </div>
     <div id="hideAdd" class="openDiv modalInfor" style="width:1100px; height:586px;">  
      <h1 class="modalHeader"> <b class="fl pl10">新增任务</b> <span class="fr modalClose" onclick="closeDiv('hideAdd','iframeAdd')"></span> </h1>  
      <div class="modalBody" style="width:1098px; height:586px;">  
        <div class="modalContent" style=" height:540px;">
            <iframe id="iframeAdd" src="" frameborder="0" scrolling="no" width="100%" height="547px" ></iframe>
        </div>  
      </div>  
     </div>
     <div id="hideNoticeReceiver" class="openDiv modalInfor" style="width:1010px; height:560px;">  
      <h1 class="modalHeader"> <b class="fl pl10">查看预警通知人员</b> <span class="fr modalClose" onclick="closeDiv2('hideNoticeReceiver','iframeNoticeReceiver')"></span> </h1>  
      <div class="modalBody" style="width:1008px; height:560px;">  
        <div class="modalContent" style=" height:514px;">
            <iframe id="iframeNoticeReceiver" src="" frameborder="0" scrolling="auto" width="100%" height="511px" ></iframe>
        </div>  
      </div>  
     </div>
</asp:Content>