<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Title="五洲在线数据交换平台-日志管理"
    CodeBehind="LogMasterBrowser.aspx.cs" Inherits="MDT.WebUI.Management.Log.LogMasterBrowser"
    ValidateRequest="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    /*页面中实现等待提示的div样式*/ 
   #bgDiv{position:absolute;background-color:#DBDBDB;filter:alpha(opacity=40);
	  opacity:0.4;left:0;top:0;z-index:10000;width:1%;}
   #messagediv{position:absolute;margin:150px auto;width:300px;height:60px;text-align:center;filter:alpha(Opacity=80);
	  -moz-opacity:0.8;opacity: 0.8;background-color:#FFE7A2;z-index:100011;font-size:larger;font-weight:bolder;top: 136px;left: 529px;border:1px #F4AA2E solid; }
   /*下拉框显示选项的div*/
   .ShowTaskdiv{position:absolute;height:auto;max-height:249px;text-align:left;background-color:#fff;border:1px #587897 solid;
		overflow-y:auto;display:none;font-size:12px;left:0;top:0;}
   .imBtn{vertical-align:bottom;+vertical-align:text-bottom;}
   .ckAll{vertical-align:text-bottom;+vertical-align:middle;}
 </style>
    <script type="text/javascript" language="javascript">
        function changeRowColor() {
            var objTable = G("dataTable");
            if (searchState == "3") {
                for (var i = 0; i < objTable.rows.length - 1; i++) {
                    objTable.rows[i + 1].className = "cflLogFailedTr";
                }
                return;
            }
            if (objTable) {
                for (var i = 0; i < objTable.rows.length - 1; i++) {
                    var labelID = "MainContent_Repeater1_lblState_" + i;
                    var lbl = G(labelID);
                    var state = $(lbl).text();
                    if (state == "Failed") {
                        objTable.rows[i + 1].className = "cflLogFailedTr";
                    }
                }
            }
        }
        $(function () {
            G('MainContent_div1').style.display = 'block';
            G('divRepSearch').style.display = 'block';
            $('.taskName').CustomSelect({
                L: 4, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 260, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'ShowTaskdiv', //下拉选项外围的类，默认为conDiv
                callback: GetTaskID
            });
            $('.col').ShowDivAndSelect({
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                displayDiv: 'divSourceCol',
                hideTxtBox: 'hideSoureColTxt'
            });
            $('.repTaskName').CustomSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'ShowTaskdiv' //下拉选项外围的类，默认为conDiv
            });
            G('MainContent_div1').style.display = 'none';
            G('divRepSearch').style.display = 'none';
        })
        function GetTaskID() {
            var id = G('MainContent_txtRTaskID').value;
            if (G('MainContent_hiddenState').value == "1") {//如果是快速查询则不需要绑定数据字段
                return;
            }
            var strAllCol = G('MainContent_hiddenAllCol').value; //hiddenAllCol保存了所有任务的数据字段
            var arrayCol = strAllCol.split('^');
            for (var i = 0; i < arrayCol.length; i++) {
                if (arrayCol[i].indexOf(id + '*') == 0) {
                    G('MainContent_hiddenCol').value = arrayCol[i];
                    var firstCol = arrayCol[i].split('|')[0];
                    var index = firstCol.indexOf('*');
                    firstCol = firstCol.substring(index + 1);
                    G('MainContent_txtSDataCol').value = firstCol;
                    $('#divSourceCol').MadeDiv({
                        liClass: 'liA12', 	//选项li的类，默认为5
                        liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                        hideTxt: 'MainContent_hiddenCol',
                        txtBox: 'hideSoureColTxt'
                    });
                    return;
                }
            }
            G('MainContent_hiddenCol').value = "*无选项";
            G('MainContent_txtSDataCol').value = "";
            $('#divSourceCol').MadeDiv({
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                hideTxt: 'MainContent_hiddenCol',
                txtBox: 'hideSoureColTxt'
            });
        }

        //提交查询前检查
        function checkQueryCondition() {
            var state = G('MainContent_hiddenState').value;
            var taskName = G("MainContent_txtRTaskName").value;
            var taskState = $('input:radio:checked').attr('value');
            var startTime = G("MainContent_StartDate").value;
            var endTime = G("MainContent_EndDate").value;
            var dataCol = "";
            var dataMsg = "";
            if (taskName == "") {
                alert("请选择任务名称！");
                G("MainContent_txtRTaskName").focus();
                return false;
            }
            if (taskName == "全部") {
                taskName = "";
            }
            if (state == "2") {  //如果是高级查询
                dataCol = G('MainContent_txtSDataCol').value;
                dataMsg = G('MainContent_txtSDataMsg').value;
                if (taskName == "") {
                    alert("请选择任务！");
                    G('MainContent_txtRTaskName').focus();
                    return false;
                }
                if (dataCol == "" || dataCol == "无") {
                    alert("请输入数据字段！");
                    G('MainContent_txtSDataCol').focus();
                    return false;
                }
                if (dataMsg == "") {
                    alert("请输入数据信息！");
                    G('MainContent_txtSDataMsg').focus();
                    return false;
                }
            } else {
                if (taskName == "" && taskState == "" && startTime == "" && endTime == "") {
                    //return true;
                }
            }
            if (G('MainContent_StartDate').value != "" && G('MainContent_EndDate').value != "") {
                var strSDate = G('MainContent_StartDate').value.replace("-", "/");
                var startdate = new Date(strSDate);
                var strEDate = G('MainContent_EndDate').value.replace("-", "/");
                var enddate = new Date(strEDate);
                if (startdate > enddate) {
                    alert('开始日期不能大于截止日期，请重新输入！');
                    G('MainContent_StartDate').value = "";
                    G('MainContent_EndDate').value = "";
                    G('MainContent_StartDate').focus();
                    return false;
                }
            }
            if (state == "1") {
                G('lbtnRTaskName').style.display = 'none';
                G('lbtnRTaskState').style.display = 'none';
                G('lbtnRStartTime').style.display = 'none';
                G('lbtnREndTime').style.display = 'none';
                if (taskName != "") {
                    taskName = '任务名称="' + taskName + '"';
                    G('lbtnRTaskName').innerHTML = taskName;
                    G('lbtnRTaskName').style.display = 'inline';
                }
                if (taskState != "") {
                    taskState = '执行状态="' + taskState + '"';
                    G('lbtnRTaskState').innerHTML = taskState;
                    G('lbtnRTaskState').style.display = 'inline';
                }
                if (startTime != "") {
                    startTime = '开始时间="' + startTime + '"';
                    G('lbtnRStartTime').innerHTML = startTime;
                    G('lbtnRStartTime').style.display = 'inline';
                }
                if (endTime != "") {
                    endTime = '结束时间="' + endTime + '"';
                    G('lbtnREndTime').innerHTML = endTime;
                    G('lbtnREndTime').style.display = 'inline';
                }
                G('divR').style.display = 'block';
                var array = $('a[name="rA"]:visible');
                if (array.length == 0) {
                    G('divR').style.display = 'none';
                }
                G('MainContent_hiddenState').value = "1"; //标识当前属于快速查询
                return true;
            }
            else {
                G('lbtnSTaskState').style.display = 'none';
                G('lbtnSStartTime').style.display = 'none';
                G('lbtnSEndTime').style.display = 'none';

                taskName = '任务名称="' + taskName + '"';
                G('lbtnSTaskName').innerHTML = taskName;
                G('lbtnSTaskName').style.display = 'inline';
                dataCol = '数据字段="' + dataCol + '"';
                G('lbtnSDataCol').innerHTML = dataCol;
                G('lbtnSDataCol').style.display = 'inline';
                dataMsg = '数据信息="' + dataMsg + '"';
                G('lbtnSDataMsg').innerHTML = dataMsg;
                G('lbtnSDataMsg').style.display = 'inline';

                if (taskState != "") {
                    taskState = '执行状态="' + taskState + '"';
                    G('lbtnSTaskState').innerHTML = taskState;
                    G('lbtnSTaskState').style.display = 'inline';
                }
                if (startTime != "") {
                    startTime = '开始时间="' + startTime + '"';
                    G('lbtnSStartTime').innerHTML = startTime;
                    G('lbtnSStartTime').style.display = 'inline';
                }
                if (endTime != "") {
                    endTime = '结束时间="' + endTime + '"';
                    G('lbtnSEndTime').innerHTML = endTime;
                    G('lbtnSEndTime').style.display = 'inline';
                }
                G('divS').style.display = 'block';
                G('MainContent_hiddenState').value = "2"; //标识当前属于快速查询
                return true;
            }
        }

        function closeRSearch(txt, lbtn) {//快速查询和高级查询时，取消查询条件
            var state = G('MainContent_hiddenState').value;
            if (txt == "All") {
                G('MainContent_txtRTaskName').value = "全部";
                G('MainContent_txtRTaskID').value = "";
                $('#MainContent_txtRTaskName').next().val(''); //清空任务名称的对应id隐藏框
                $('input:radio[value=""]').attr('checked', true);
                G('MainContent_StartDate').value = "";
                G('MainContent_EndDate').value = "";
                if (state == "1") {
                    G('lbtnRTaskName').style.display = 'none';
                    G('lbtnRTaskState').style.display = 'none';
                    G('lbtnRStartTime').style.display = 'none';
                    G('lbtnREndTime').style.display = 'none';
                    G('divR').style.display = 'none';
                }
                else {
                    G('MainContent_hiddenCol').value = "*无选项";
                    G('MainContent_txtSDataCol').value = "";
                    G('MainContent_txtSDataMsg').value = "";
                    G('lbtnSTaskName').style.display = 'none';
                    G('lbtnSTaskState').style.display = 'none';
                    G('lbtnSStartTime').style.display = 'none';
                    G('lbtnSEndTime').style.display = 'none';
                    G('lbtnSDataCol').style.display = 'none';
                    G('lbtnSDataMsg').style.display = 'none';
                    G('divS').style.display = 'none';
                    eval(G('MainContent_hideLbtnSearch').href);
                    return;
                }
            } else if (txt == "MainContent_txtRTaskName") {
                G(txt).value = "全部";
                G('MainContent_txtRTaskID').value = "";
                $('#MainContent_txtRTaskName').next().val(''); //清空任务名称的对应id隐藏框
                G(lbtn).style.display = 'none';
            } else if (txt == "taskState") {
                $('input:radio[value=""]').attr('checked', true);
                G(lbtn).style.display = 'none';
            } else {
                G(txt).value = "";
                G(lbtn).style.display = 'none';
            }
            if (state == "1") {
                var array = $('a[name="rA"]:visible');
                if (array.length == 0) {
                    G('divR').style.display = 'none';
                }
            }
            eval(G('MainContent_btnSearch').href);
        }

        var searchState = '1';
        function trSeniorShow(state) {
            if (state == searchState) {
                if (G('MainContent_div1').style.display == 'block') {
                    G('MainContent_div1').style.display = 'none';
                    G('MainContent_hideBlockOrNone').value = 'none';
                }
                else {
                    G('MainContent_div1').style.display = 'block';
                    G('MainContent_hideBlockOrNone').value = 'block';
                }
            }
            else {
                G('MainContent_div1').style.display = 'block';
                G('MainContent_hideBlockOrNone').value = 'block';
                G('MainContent_hiddenState').value = state;
                searchState = state;
            }
            if (state == "2") {
                G('MainContent_trSenior').style.display = '';  //table-row
                G('MainContent_spanTaskName').style.display = "inline";
            }
            else {
                G('MainContent_trSenior').style.display = "none";
                G('MainContent_spanTaskName').style.display = "none";
            }
            G('MainContent_txtRTaskName').value = "全部";
            G('MainContent_txtRTaskID').value = "";
            $('#MainContent_txtRTaskName').next().val(''); //清空任务名称的对应id隐藏框
            G('MainContent_StartDate').value = "";
            G('MainContent_EndDate').value = "";
            G('MainContent_txtSDataCol').value = "";
            G('MainContent_txtSDataMsg').value = "";
            G('MainContent_rblState_0').checked = "checked";
            G('divR').style.display = "none";
            G('divS').style.display = "none";
            G('MainContent_hiddenCol').value = "*无选项"; //清空hiddenCol（保存了当前选中任务的数据字段）
            eval(G('MainContent_hideLbtnSearch').href);
        }

        //全选的方法
        function ckAllClick(obj) {
            var boolC = $(obj).attr('checked');
            $('input:checkbox').each(function () { if (!this.disabled) this.checked = boolC; });
        }
        function repSearch() {  //数据重发的查询事件触发前执行的操作
            var taskName = G("MainContent_txtRepTaskName").value;
            var startTime = G("MainContent_txtRepStartTime").value;
            var endTime = G("MainContent_txtRepEndTime").value;
            if (taskName == "") {
                G("MainContent_txtRepTaskName").focus();
                alert("请选择任务名称！");
                return false;
            }
            if (taskName == "全部") {
                taskName = "";
            }
            if (taskName == "" && startTime == "" && endTime == "") {
                G('MainContent_divRep').style.display = 'none';
                return;
            }
            G('lbtnRepTaskName').style.display = 'none';
            G('lbtnRepStartTime').style.display = 'none';
            G('lbtnRepEndTime').style.display = 'none';
            if (startTime != "" && endTime != "") {
                var strSDate = startTime.replace("-", "/");
                var startdate = new Date(strSDate);
                var strEDate = endTime.replace("-", "/");
                var enddate = new Date(strEDate);
                if (startdate > enddate) {
                    alert('开始日期不能大于截止日期，请重新输入！');
                    G('MainContent_txtRepStartTime').value = "";
                    G('MainContent_txtRepEndTime').value = "";
                    G('MainContent_txtRepStartTime').focus();
                    return false;
                }
            }
            if (taskName != "" && taskName != "全部") {
                taskName = '任务名称="' + taskName + '"';
                G('lbtnRepTaskName').innerHTML = taskName;
                G('lbtnRepTaskName').style.display = 'inline';
            }
            if (startTime != "") {
                startTime = '开始时间="' + startTime + '"';
                G('lbtnRepStartTime').innerHTML = startTime;
                G('lbtnRepStartTime').style.display = 'inline';
            }
            if (endTime != "") {
                endTime = '结束时间="' + endTime + '"';
                G('lbtnRepEndTime').innerHTML = endTime;
                G('lbtnRepEndTime').style.display = 'inline';
            }
            G('MainContent_divRep').style.display = 'block';
            G('MainContent_hiddenState').value = "1"; //标识当前属于快速查询
            return true;
        }
        function closeRepSearch(txt, lbtn) {
            if (txt == "All") {
                G('MainContent_txtRepTaskName').value = "全部";
                G('MainContent_txtRepStartTime').value = "";
                G('MainContent_txtRepEndTime').value = "";
                G('MainContent_txtRepTaskId').value = "";
                G('lbtnRepTaskName').style.display = 'none';
                G('lbtnRepStartTime').style.display = 'none';
                G('lbtnRepEndTime').style.display = 'none';
            }
            else if (txt == "MainContent_txtRepTaskName") {
                G(txt).value = "全部";
                G('MainContent_txtRepTaskId').value = "";
                G(lbtn).style.display = 'none';
            }
            else {
                G(txt).value = "";
                G(lbtn).style.display = 'none';
            }
            var array = $('a[name="repA"]:visible');
            if (array.length == 0) {
                G('MainContent_divRep').style.display = 'none';
            }
            eval(G('MainContent_lbtnRepSearch').href);
        }
        function showRep(s) {
            if (searchState == s) {
                if (G('divRep2').style.display == 'block') {
                    G('divRep2').style.display = 'none';
                } else {
                    G('divRep2').style.display = 'block';
                }
            } else {
                G('divRep2').style.display = 'block';
                searchState = s;
            }
            G('MainContent_txtRepTaskName').value = "全部";
            G('MainContent_txtRepTaskId').value = "";
            G('MainContent_txtRepStartTime').value = "";
            G('MainContent_txtRepEndTime').value = "";
            eval(G('MainContent_lbtnRepSearch').href);
        }
        function showDiv(div1, div2, div3, div4) {
            G(div1).style.display = 'block';
            G(div2).style.display = 'none';
        }

        function refresh() {//刷新
            eval(G('MainContent_lbtnRefresh').href);
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManagerLog" runat="server" AsyncPostBackTimeout="500">
    </asp:ScriptManager>
    <div align="right" style="margin: -20px 0px 5px 0px;">
        <asp:LinkButton ID="lbtnRefresh" runat="server" OnClick="lbtnRefresh_Click">刷新</asp:LinkButton>&nbsp;&nbsp;
        <a href="javascript:trSeniorShow('1');showDiv('divRSSearch','divRepSearch');">快速查询</a>&nbsp;&nbsp;
        <a href="javascript:trSeniorShow('2');showDiv('divRSSearch','divRepSearch');">高级查询</a>&nbsp;&nbsp;
        <a href="javascript:showRep('3');showDiv('divRepSearch','divRSSearch');" id="aRepeating"
            runat="server">数据重发</a>
    </div>
    <div id="divRSSearch" style="padding: 0px 0px 5px 0px;">
        <div>
            <div id="divR" style="border: 1px #FDB45B solid; padding: 5px 5px 5px 5px; border-bottom: none;
                background-color: #FFF7E6; position: relative; display: none;">
                您已选择的查询条件： <a id="lbtnRTaskName" name="rA" onclick="closeRSearch('MainContent_txtRTaskName','lbtnRTaskName');"
                    title="点击可取消此查询条件" class="ml5">任务名称：</a> <a id="lbtnRTaskState" name="rA" onclick="closeRSearch('taskState','lbtnRTaskState')"
                        title="点击可取消此查询条件" class="ml10">执行状态：</a> <a id="lbtnRStartTime" name="rA" onclick="closeRSearch('MainContent_StartDate','lbtnRStartTime')"
                            title="点击可取消此查询条件" class="ml10">开始时间：</a> <a id="lbtnREndTime" name="rA" onclick="closeRSearch('MainContent_EndDate','lbtnREndTime')"
                                title="点击可取消此查询条件" class="ml10">结束时间：</a>
                <div style="width: 20px; position: absolute; right: 0; top: 5px;">
                    <a id="lbtnRCloseAll" onclick="closeRSearch('All','');">
                        <img src="/images/searchClose.gif" alt="" /></a>
                </div>
            </div>
            <div id="divS" style="border: 1px #FFEDC8 solid; padding: 5px 5px 5px 5px; border-bottom: none;
                background-color: #FFF7E6; position: relative; display: none;">
                您已选择的查询条件： <a id="lbtnSTaskName" name="sA" title="高级查询时为必输项,不能取消" class="ml5">任务名称：</a>
                <a id="lbtnSDataCol" name="sA" title="高级查询时为必输项,不能取消" class="ml10">数据字段</a> <a id="lbtnSDataMsg"
                    name="sA" title="高级查询时为必输项,不能取消" class="ml10">数据信息</a> <a id="lbtnSTaskState" name="sA"
                        onclick="closeRSearch('taskState','lbtnSTaskState');" title="点击可取消此查询条件" class="ml10">
                        执行状态：</a> <a id="lbtnSStartTime" name="sA" onclick="closeRSearch('MainContent_StartDate','lbtnSStartTime');"
                            title="点击可取消此查询条件" class="ml10">开始时间：</a> <a id="lbtnSEndTime" name="sA" onclick="closeRSearch('MainContent_EndDate','lbtnSEndTime');"
                                title="点击可取消此查询条件" class="ml10">结束时间：</a>
                <div style="width: 20px; position: absolute; right: 0; top: 5px;">
                    <a id="lbtnSCloseAll" onclick="closeRSearch('All','');">
                        <img src="/images/searchClose.gif" alt="" /></a>
                </div>
            </div>
            <div id="div1" runat="server" style="height: auto; padding: 5px 0; background-color: #d7e9fd;
                border: #a3c0e8 solid 1px;">
                <table style="width: 100%;" cellpadding="0" cellspacing="5">
                    <tbody>
                        <tr>
                            <td valign="middle" align="right">
                                任务名称：
                            </td>
                            <td align="left" valign="middle">
                                <input id="txtRTaskName" type="text" runat="server" class="textField260 taskName"
                                    to="MainContent_hiddenTask" txtid="MainContent_txtRTaskID" value="全部" style="text-transform: uppercase;
                                    ime-mode: disabled" />
                                <span style="color: #ff0000;" id="spanTaskName" runat="server">*</span>
                                <input id="txtRTaskID" type="text" runat="server" class="hide" value="" />
                            </td>
                            <td valign="middle" align="right">
                                执行状态：
                            </td>
                            <td align="left" valign="middle">
                                <asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal" CellPadding="0"
                                    CellSpacing="5">
                                    <asp:ListItem Selected="True" Value="">全部</asp:ListItem>
                                    <asp:ListItem Value="Success">成功</asp:ListItem>
                                    <asp:ListItem Value="Failed">失败</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td valign="middle" align="right">
                                操作时间：
                            </td>
                            <td align="left">
                                <input id="StartDate" type="text" class="textField14" runat="server" onfocus="WdatePicker();" />
                                至
                                <input id="EndDate" type="text" class="textField14" runat="server" onfocus="WdatePicker();" />
                            </td>
                            <td>
                                <asp:LinkButton ID="btnSearch" CssClass="aBtn" runat="server" OnClick="btnSearch_Click"
                                    OnClientClick="return checkQueryCondition();"><span><em>查询</em></span></asp:LinkButton>
                                <asp:LinkButton ID="hideLbtnSearch" runat="server" OnClick="hideLbtnSearch_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="trSenior" runat="server">
                            <td valign="middle" align="right">
                                数据字段：
                            </td>
                            <td align="left" valign="middle">
                                <input id="txtSDataCol" runat="server" class="textField260 col" value="" />
                                <span style="color: #ff0000; display: inline;">*</span>
                            </td>
                            <td valign="middle" align="right">
                                数据信息：
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSDataMsg" runat="server" CssClass="textField26"></asp:TextBox>
                                <span style="color: #ff0000; display: inline;">*</span>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div id="divRepSearch" style="padding: 5px 0px;">
        <div id="divRep2">
            <div id="divRep" style="border: 1px #FDB45B solid; padding: 5px 5px 5px 5px; border-bottom: none;
                background-color: #FFF7E6; position: relative; display: none" runat="server">
                您已选择的查询条件： <a id="lbtnRepTaskName" name="repA" title="点击可取消此查询条件" onclick="closeRepSearch('MainContent_txtRepTaskName','lbtnRepTaskName')">
                    任务名称：</a> <a id="lbtnRepStartTime" name="repA" title="点击可取消此查询条件" onclick="closeRepSearch('MainContent_txtRepStartTime','lbtnRepStartTime')">
                        开始时间：</a> <a id="lbtnRepEndTime" name="repA" title="点击可取消此查询条件" onclick="closeRepSearch('MainContent_txtRepEndTime','lbtnRepEndTime')">
                            结束时间：</a>
                <div style="width: 20px; position: absolute; right: 0; top: 5px;">
                    <a id="lbtnRepCloseAll" onclick="closeRepSearch('All','')">
                        <img src="/images/searchClose.gif" alt="" /></a>
                </div>
            </div>
            <div style="height: auto; padding: 5px 0; background-color: #d7e9fd; border: #a3c0e8 solid 1px;">
                <table style="width: 100%;" cellpadding="0" cellspacing="5">
                    <tbody>
                        <tr>
                            <td valign="middle" align="right">
                                任务名称：
                            </td>
                            <td align="left" valign="middle">
                                <input id="txtRepTaskName" type="text" runat="server" class="textField260 repTaskName"
                                    to="MainContent_hiddenTask" txtid="MainContent_txtRepTaskId" value="全部" style="text-transform: uppercase" />
                                <input id="txtRepTaskId" type="text" runat="server" class="hide" value="" />
                            </td>
                            <td valign="middle" align="right">
                                操作时间：
                            </td>
                            <td align="left">
                                <input id="txtRepStartTime" type="text" class="textField14" runat="server" onfocus="WdatePicker();" />
                                至
                                <input id="txtRepEndTime" type="text" class="textField14" runat="server" onfocus="WdatePicker();" />
                            </td>
                            <td style="width: 20%">
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnRepSearch" CssClass="aBtn" runat="server" OnClientClick="return repSearch();"
                                    OnClick="lbtnRepSearch_Click"><span><em>查询</em></span></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnRepeating" CssClass="aBtn" runat="server" OnClick="lbtnRepeating_Click"><span><em>重发</em></span></asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <asp:Timer runat="server" ID="Timer1" Interval="1" OnTick="Timer1_Tick">
    </asp:Timer>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true">
                <ProgressTemplate>
                    <div id="bgDiv" class="bgDiv" style="height: 230px">
                    </div>
                    <div id="messagediv">
                        <table>
                            <tr>
                                <td>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;<img src="/images/loading.gif" border="0" alt="" />&nbsp;&nbsp;&nbsp;<span
                                        id="spanWait"> 数据正在加载中，请稍候........</span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <!-------快速查询和高级查询的日志列表----------------------->
            <div id="divSearchMain" runat="server" style="overflow-x: scroll; margin: 0px 0px 0px 0px;
                width: 100%;">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <table id="dataTable" style="width: 126%;">
                            <thead>
                                <tr>
                                    <th>
                                        ID
                                    </th>
                                    <th>
                                        任务名称
                                    </th>
                                    <th>
                                        执行状态
                                    </th>
                                    <th>
                                        总数量
                                    </th>
                                    <th>
                                        开始时间
                                    </th>
                                    <th>
                                        结束时间
                                    </th>
                                    <th>
                                        数据信息
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                    <th style="width: 10%;">
                                        说明
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="rTr" runat="server">
                            <td align="left">
                                <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblETask_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ETask_ID")%>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblETaskName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.DataCount")%>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.StartTime")%>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.EndTime")%>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblData" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblProcess" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.Note")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <!-------数据重发的日志列表----------------------------------------->
            <div id="divRepMan" runat="server" style="overflow-x: scroll; margin: 0px 0px 0px 0px;
                display: none; width: 100%;">
                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                    <HeaderTemplate>
                        <table id="dataTable" style="width: 126%;">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">
                                        <input id="ckAll" type="checkbox" class="ckAll" onclick="ckAllClick(this)" />全选
                                    </th>
                                    <th style="width: 80px">
                                        序号
                                    </th>
                                    <th>
                                        任务名称
                                    </th>
                                    <th>
                                        处理批次
                                    </th>
                                    <th>
                                        执行状态
                                    </th>
                                    <th>
                                        总数量
                                    </th>
                                    <th>
                                        开始时间
                                    </th>
                                    <th>
                                        结束时间
                                    </th>
                                    <th>
                                        数据信息
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                    <th style="width: 10%;">
                                        说明
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="rTr" runat="server">
                            <td>
                                <asp:CheckBox ID="ckRepeating" name="ckRepeating" runat="server" AutoPostBack="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblETask_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ETask_ID")%>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblETaskName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskName")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.ProcessLN")%>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.State")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDataCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DataCount")%>'></asp:Label>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.StartTime")%>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.EndTime")%>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblData" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblProcess" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="left">
                                <%# DataBinder.Eval(Container, "DataItem.Note")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="page" align="center" style="margin: 0px 0px 0px 0px;">
                <ul class="rightPosition">
                    <li>共有<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>条信息， 当前
                        <asp:Label ID="lblCurrentPage" runat="server" Text="0"></asp:Label>/
                        <asp:Label ID="lblPageCount" runat="server" Text="0"></asp:Label>页 </li>
                    <li>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" UrlPaging="false" OnPageChanged="AspNetPager1_PageChanged"
                            AlwaysShow="True" ShowPageIndexBox="Never">
                        </webdiyer:AspNetPager>
                    </li>
                    <li>转到</li>
                    <li>
                        <asp:TextBox ID="txtPageForSearch" CssClass="PIC" runat="server"></asp:TextBox></li>
                    <li>页</li>
                    <li>
                        <asp:Button ID="btnSearchByPage" CssClass="SBC" runat="server" Text="go" OnClick="btnSearchByPage_Click" /></li>
                </ul>
            </div>
            <input id="hiddenCol" type="hidden" name="hiddenCol" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnRepSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="hideLbtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnRepeating" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <input id="hiddenState" type="hidden" name="hiddenState" value="1" runat="server" />
    <input id="hideBlockOrNone" type="hidden" name="hiddenState" value="None" runat="server" />
    <input id="hiddenMsg" type="hidden" name="hiddenMsg" value="" runat="server" />
    <input id="hiddenTask" type="hidden" name="hiddenTask" runat="server" />
    <input id="hiddenAllCol" type="hidden" name="hiddenAllCol" runat="server" />
    <!---------------- 弹出窗口 ---------------------->
    <div id="hidebg">
    </div>
    <div id="hideErrorMsg" class="openDiv modalInfor" style="width: 710px; height: 426px;">
        <h1 class="modalHeader">
            <b class="fl pl10">失败信息</b> <span class="fr modalClose" onclick="closeDiv2('hideErrorMsg','iframeErrorMsg')">
            </span>
        </h1>
        <div class="modalBody" style="width: 708px; height: 426px;">
            <div class="modalContent" style="height: 380px;">
                <iframe id="iframeErrorMsg" src="" frameborder="0" scrolling="auto" width="99%" height="377px">
                </iframe>
            </div>
        </div>
    </div>
    <div id="hideDataMessage" class="openDiv modalInfor" style="width: 760px; height: 480px;">
        <h1 class="modalHeader">
            <b class="fl pl10">数据信息</b> <span class="fr modalClose" onclick="closeDiv2('hideDataMessage','iframeDataMessage')">
            </span>
        </h1>
        <div class="modalBody" style="width: 758px; height: 480px;">
            <div class="modalContent" style="height: 434px;">
                <iframe id="iframeDataMessage" src="" frameborder="0" scrolling="auto" width="100%"
                    height="431px"></iframe>
                <%--<div id="divDataMessage" runat="server" style="margin:10px 10px 10px 10px;"></div>--%>
            </div>
        </div>
    </div>
    <div id="hideProgress" class="openDiv" style="width: 360px; height: 60px; padding: 0px;">
        <iframe id="iframeProgress" src="" frameborder="0" scrolling="no" width="360" height="60px;"
            allowtransparency="true" style="border: 1px #F4AA2E solid;"></iframe>
    </div>
    <div id="divSourceCol" class="ShowTaskdiv">
        <ul>
        </ul>
    </div>
    <input id="hideColData" type="hidden" value="MainContent_hiddenCol" />
    <input id="hideSoureColTxt" type="hidden" value="MainContent_txtSDataCol" />
</asp:Content>
