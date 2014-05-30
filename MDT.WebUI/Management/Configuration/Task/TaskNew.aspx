<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskNew.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Task.TaskNew" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/trHighLight.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <script language="javascript" type="text/javascript" src="/js/editarea_0_8_2/edit_area/edit_area_loader.js"></script>
    <script src="/js/jQuery.CustomSelect.js" type="text/javascript"></script>
    <script src="/js/jquery.LargeAndFastSelect.js" type="text/javascript"></script>
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
	        margin:150px 0px 150px auto;
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
            top: 133px;
            left: 187px;
            border:1px #F4AA2E solid; 
          }
        .coldiv  /*下拉框显示选项的div*/
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
       <%--.coldiv2  /*下拉框显示选项的div*/
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
       }--%>
        .imBtn{
	    vertical-align:bottom;
	    +vertical-align:text-bottom;}
    </style>
     <!--页内脚本开始-->
     <script type="text/javascript">
         $(function () {
             $('#dataTable').trOddHilight();
         })
         //关闭本页同时刷新父页面
         function openerRefresh() {
             parent.bindData();
             parent.hide('hideAdd', 'iframeAdd');
         }
         function checkDelete() {if (confirm('确定删除所选行？')) {return true;}return false;}
         function checkClose() {if (confirm('关闭本页面并不保存数据，是否确定？')) {parent.hide('hideMapping', 'iframeMapping');}}
         function divScrollEvent() { $('.coldiv').hide(); } //隐藏下拉选项的div
         //如果来源列和目标列的 列名不一致，则行被标识为红色背景
         function changeColor() {
             var objTable = document.getElementById("dataTable");
             if (objTable) {
                 for (var i = 0; i < objTable.rows.length; i++) {
                     var txtS = document.getElementById("Repeater1_txtSourceCol_" + i);
                     var s = txtS.value.toUpperCase();
                     var txtT = document.getElementById("Repeater1_txtTargetCol_" + i);
                     var t = txtT.value.toUpperCase();
                     if (s != t) {
                         objTable.rows[i].className = "cflLogFailedTr";
                     }
                 }
             }
         }
        function colunmWidth() {//设置列的宽度  使表头表cflDataTable的列宽 等于 数据表dataTable的列宽
            var objTable1 = document.getElementById("cflDataTable");
            var objTable2 = document.getElementById("dataTable");
            if (objTable1 && objTable2 && objTable2.rows.length > 0) {
                for (var i = 0; i < objTable2.rows[0].cells.length; i++) {
                    var widthTest = objTable2.rows[0].cells[i].offsetWidth - 3;
                    if (widthTest < 1) {
                        return;
                    }
                    objTable1.cells[i].width = widthTest;
                    if (i == 5) {
                        var sbarWidth = document.getElementById('divScroll').offsetWidth - document.getElementById('divScroll').scrollWidth;
                        objTable1.cells[i].width = objTable2.rows[0].cells[i].offsetWidth + sbarWidth - 3;
                    }
                }
            }
        }
         function AddDefaultValue(obj) {//添加默认值
             if (obj.value != "") {
                 var id = obj.id;
                 var index = id.lastIndexOf('_');
                 var num = id.substring(index + 1, id.length);
                 document.getElementById('Repeater1_txtSourceCol_' + num).value = "";
                 document.getElementById('Repeater1_txtSourceType_' + num).value = "";
             }
         }
         function initTable() {
             $('.sdatabase').CustomSelect({
                 L: 0, //下拉选型的左偏移量，默认为0
                 T: 1, //下拉选型的上偏移量，默认为0
                 W: 20, //下拉选型的宽偏移量，默认为0
                 N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                 isuppercase: false, //是否只能输入大小写 ，默认为“false”
                 imBtn: 'imBtn', 	//下拉按钮的类，默认为'imBtn'
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                 conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                 callback: sDataBaseEvent
             });
             $('.tdatabase').CustomSelect({
                 L: 0, //下拉选型的左偏移量，默认为0
                 T: 1, //下拉选型的上偏移量，默认为0
                 W: 20, //下拉选型的宽偏移量，默认为0
                 N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                 isuppercase: false, //是否只能输入大小写 ，默认为“false”
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                 conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                 callback: tDataBaseEvent
             });
             $('.stable').CustomSelect({   //TMSelectDataTable   PromptInputSelect3
                 L: 0, //下拉选型的左偏移量，默认为0
                 T: 1, //下拉选型的上偏移量，默认为0
                 W: 20, //下拉选型的宽偏移量，默认为0
                 N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                 isuppercase: false, //是否只能输入大小写 ，默认为“false”
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                 conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                 callback: sTableEvent
             });
             $('.ttable').CustomSelect({
                 L: 0, //下拉选型的左偏移量，默认为0
                 T: 1, //下拉选型的上偏移量，默认为0
                 W: 20, //下拉选型的宽偏移量，默认为0
                 N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                 isuppercase: false, //是否只能输入大小写 ，默认为“false”
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                 conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                 callback: sTableEvent
             });
         }
         function sDataBaseEvent() {eval(document.getElementById('lbtnSD').href);}
         function tDataBaseEvent() {eval(document.getElementById('lbtnTD').href);}
         function sTableEvent() { eval(document.getElementById('lbtnST').href); }
         function initColAndType() {
             $('.scol').ShowDivAndSelect({
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 displayDiv: 'divSourceCol',
                 hideTxtBox: 'hideSoureColTxt'
             });
             $('.tcol').ShowDivAndSelect({
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 displayDiv: 'divTargetCol',
                 hideTxtBox: 'hideSoureColTxt'
             });
             $('.stype').ShowDivAndSelect({
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 displayDiv: 'divColType',
                 hideTxtBox: 'hideSoureColTxt'
             });
         }
         function initDiv() {
             $('#divSourceCol').MadeDiv({
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 hideTxt: 'hideSCol',
                 txtBox: 'hideSoureColTxt'
             });
             $('#divTargetCol').MadeDiv({
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 hideTxt: 'hideTCol',
                 txtBox: 'hideSoureColTxt'
             });
             $('#divColType').MadeDiv({
                 liClass: 'liA12', 	//选项li的类，默认为5
                 liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                 hideTxt: 'hideDataType',
                 txtBox: 'hideSoureColTxt'
             });
         }
         function clientSave() {//保存时获取 文本域的值
             var array = editAreaLoader.getAllFiles("txtXMLData"); //获取在txtData文本域中打开的所有文件
             var objMapping = array['mapping']; //获取id 为mapping的文件
             if (objMapping != null) {
                 document.getElementById('hideMapping').value = objMapping.text;  //将修改过的值保存在相应的hidden里面
             }
             return true;
         }
         editAreaLoader.init({  //初始化xml编辑器
             id: "txtXMLData"	// id of the textarea to transform	
			, start_highlight: true
			, allow_toggle: false
			, language: "en"
			, syntax: "xml"
			, toolbar: "search, |, undo, redo, |, select_font, |, change_smooth_selection, highlight, reset_highlight, |, help"
			, syntax_selection_allow: "css,html,js,python,vb,xml,c,cpp,sql,basic,pas,brainfuck"
			, is_multi_files: true
			, EA_load_callback: "editAreaLoaded"
			, show_line_colors: true
         });
         function editAreaLoaded(id) {if (id == "txtXMLData") {open_file1('mapping', 'hideMapping', 'Mapping');}}
         function open_file1(i, hide, t) {
             var str = document.getElementById(hide).value;
             var source = new ActiveXObject("Msxml2.DOMDocument");
             source.async = false;
             source.loadXML(str); //加载xml数据
             // 装载样式单
             var stylesheet = new ActiveXObject("Msxml2.DOMDocument");
             stylesheet.async = false;
             stylesheet.resolveExternals = false;
             stylesheet.load("/Style.xsl");
             // 创建结果对象
             var result = new ActiveXObject("Msxml2.DOMDocument");
             result.async = false;
             // 把解析结果放到结果对象中方法1
             source.transformNodeToObject(stylesheet, result);
             var reg = /\t/g;
             var xmlR = result.xml.replace(reg, "    ");
             var new_file = { id: i, text: xmlR, syntax: 'xml', title: t };
             editAreaLoader.openFile('txtXMLData', new_file);
         }
         function uppercase(txt) {
             var evt = window.event;
             var nKeyCode = evt.keyCode || evt.which;
             var sInput = txt.value;
             if (nKeyCode >= 65 && nKeyCode <= 90) {
                 txt.value = sInput.toUpperCase();
             }
         }
         function checkDelete() {
             var isChecked = false;
             $('input:checkbox').each(function () {
                 if (this.checked) { isChecked = true; return; }
             });
             if (isChecked) {
                 if (!confirm("是否确定删除选中行？")) {
                     return false;
                 }
                 return true;
             }
             alert("请选择需要删除的行！");
             return false;
         }
         function checkSave() {
             var v = document.getElementById("txtTaskName").value;
             if (v.Trim() == "") {
                 document.getElementById("txtTaskName").focus();
                 alert("请输入任务名称！");
                 return false;
             }
             return true;
         }
     </script>
<!--页内脚本结束-->
</head>
<body style="margin:0px 0px 0px 10px; height:547px;">
  <form id="form1" runat="server"> 
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
    <table id="Table1"  cellpadding="0" cellspacing="0" border="0" width="1060px">
    <tr>
    <td style="width:65%;" valign="top">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
          <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
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
        <table id="Table2"  cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size:9pt;">
        <tr style="height:30px;">
            <td style="width:15%;" align="right">任务&nbsp;&nbsp; 名称：</td>
            <td style="width:35%;" align="left">
                <asp:TextBox ID="txtTaskName" class="textField220" runat="server" onkeyup="uppercase(this)"></asp:TextBox>
            </td>
            <td style="width:15%;" align="right">任务&nbsp;&nbsp; 描述：</td>
            <td style="width:35%;" align="left">
                <asp:TextBox ID="txtTaskDesc" class="textField220" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="height:30px;">
            <td align="right">来源数据库：</td>
            <td align="left">
                <input id="txtSDataBase" name="" type="text" class="textSelect220 sdatabase" to="hdb" txtid="txtSDataBaseId" readonly="readonly" runat="server"/>
                <input id="txtSDataBaseId" name="txtSDataBaseId" type="text" runat="server" class="hide" />
            </td>
            <td  align="right">目标数据库：</td>
            <td align="left">
                <input id="txtTDataBase" type="text" class="textSelect220 tdatabase" to="hdb" txtid="txtTDataBaseId" runat="server" readonly="readonly" />
                <input id="txtTDataBaseId" name="txtTDataBaseId" type="text" runat="server" class="hide" />
            </td>
        </tr>
        
            
            <tr style="height:30px;">
                <td align="right">表&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 名：</td>
                <td align="left">
                    <input id="txtSTable" type="text" class="textSelect220 stable" to="hideSTable" runat="server"/>
                </td>
                <td  align="right">表&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 名：</td>
                <td align="left">
                    <input id="txtTTable" type="text" class="textSelect220 ttable" to="hideTTable" runat="server"/>
                </td>
            </tr>
            <tr style="height:30px;">
                <td align="right">主&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 键：</td>
                <td align="left">
                    <input id="txtSPrimaryKeys" type="text" class="textSelect220 scol" runat="server"/>
                </td>
                <td  align="right">主&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 键：</td>
                <td align="left">
                    <input id="txtTPrimaryKeys" type="text" class="textSelect220 tcol" runat="server"/>
                </td>
            </tr>
            <tr style="height:30px;">
                <td align="right">外&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 键：</td>
                <td align="left">
                    <input id="txtSForeignKeys" type="text" class="textSelect220 scol" runat="server"/>
                </td>
                <td  align="right">外&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 键：</td>
                <td align="left">
                    <input id="txtTForeignKeys" type="text" class="textSelect220 tcol" runat="server"/>
                </td>
            </tr>
            <tr style="height:30px;">
                <td align="right">WHERE：</td>
                <td align="left">
                    <asp:TextBox ID="txtAdditionalWhere" class="textField220" runat="server"></asp:TextBox>
                </td>
                <td align="right">事后任务名称：</td>
                <td align="left">
                    <asp:TextBox ID="txtPostTaskName" class="textField220" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                      <table id="cflDataTable" style=" width:100%px;" >
                        <thead>
                          <tr>
                            <th width="25px" align="center">选择</th> 
                            <th width="25%">来源列名</th> 
                            <th width="15%">数据类型</th>
                            <th width="25%">目标列名</th>
                            <th width="15%">数据类型</th>
                            <th width="12%">默认值</th>
                          </tr>
                         </thead>
                       </table>          
                       <asp:Repeater ID="Repeater1" runat="server">
                         <HeaderTemplate>
                            <div id="divScroll" onscroll="divScrollEvent();" style="OVERFLOW-Y:scroll; overflow-X:scroll; WIDTH:100%; HEIGHT:300px;">
                            <table  id="dataTable" style=" width:100%px; margin-top:-1px;"><tbody>  
                         </HeaderTemplate>
                         <ItemTemplate>
                            <tr  style=" height:35px;">
                                <td width="25px"><asp:CheckBox ID="ckDelete" runat="server" AutoPostBack="false" /></td>
                                <td width="25%">
                                    <asp:TextBox ID="txtSourceCol" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.sourceName")%>' CssClass="textField14 scol"></asp:TextBox>
                                </td>
                                <td width="15%">
                                    <asp:TextBox ID="txtSourceType" runat="server" Text='<%#FormatDataType(DataBinder.Eval(Container, "DataItem.sourceType").ToString())%>' class="textField2 stype"></asp:TextBox>
                                </td>
                                <td width="25%">
                                    <asp:TextBox ID="txtTargetCol" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.targetName")%>' CssClass="textField14 tcol"></asp:TextBox>
                                </td>
                                <td width="15%">
                                    <asp:TextBox ID="txtTargetType" runat="server" value='<%#FormatDataType(DataBinder.Eval(Container, "DataItem.targetType").ToString())%>' class="textField2 stype" ></asp:TextBox>
                                </td>
                                <td width="12%">
                                    <asp:TextBox ID="txtDefault" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.defaultValue") %>' CssClass="textField10" onblur="AddDefaultValue(this);"></asp:TextBox>
                                </td>
                              </tr>                
                          </ItemTemplate>
                          <FooterTemplate> 
                            </tbody></table></div>
                          </FooterTemplate>
                        </asp:Repeater>
                  </td>
               </tr>
               <input id="hideSTable" type="hidden" runat="server" />
            <input id="hideTTable" type="hidden" runat="server" />
            <input id="hideSCol" type="hidden" runat="server" />
            <input id="hideTCol" type="hidden" runat="server" />
                    
            </table>
       </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbtnSD" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnTD" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnST" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnDeleteColunm" EventName="Click" />
        </Triggers>
      </asp:UpdatePanel>     
    </td>
    <td style="width:35%; margin-left:10px;" align="left" valign="top">
        <div style="margin-left:10px;">
        <div>
            <textarea id="txtXMLData" runat="server" style="height:500px; width: 98%;" name="test_2">
		    </textarea>
        </div>
            <input id="hideMapping" type="hidden" value="" runat="server" />
        </div>
    </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
            <tr>
                <td align="right">
                    <asp:LinkButton ID="lbtnDeleteColunm" CssClass="aBtn" runat="server" OnClientClick="return checkDelete();" 
                        onclick="lbtnDeleteColunm_Click"><span><em>删除对应列</em></span></asp:LinkButton>
                </td>
            </tr>
            </table>
        </td>
        <td align="right">
            <%--<asp:LinkButton ID="lbtnDeleteTask" CssClass="aBtn" runat="server" 
                onclick="lbtnDeleteTask_Click"><span><em>删除</em></span></asp:LinkButton>&nbsp;&nbsp;--%>
            <asp:LinkButton ID="lbtnAdd" CssClass="aBtn" runat="server"  OnClientClick="return clientSave();"
                onclick="lbtnAdd_Click"><span><em>添加</em></span></asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnSave" CssClass="aBtn" runat="server" OnClientClick="return checkSave();" 
                onclick="lbtnSave_Click"><span><em>保存</em></span></asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnSaveNullTask" CssClass="aBtn" runat="server"  OnClientClick="return checkSave();" 
                onclick="lbtnSaveNullTask_Click"><span><em>生成空任务</em></span></asp:LinkButton>&nbsp;&nbsp;
        </td>
    </tr>
    </table>
            
    <div>
        <input id="hdb" type="hidden" runat="server" />
        <input id="hideDataType"  type="hidden" runat="server" value="0*字符型|0*数字型|0*日期型" />
        <%--<input id="hideName" type="hidden" runat="server" value="txtSDataBase" />--%>
        <%--<input id="hideID" type="hidden" runat="server" value="txtSID" />--%>
        <%--<input id="hideHdb" type="hidden" runat="server" value="hdb" />--%>
        <%--<input id="hideLbtn" type="hidden" runat="server" value="" />--%>
        <%--<input id="hideFileName" type="hidden" runat="server" />--%>
        <asp:LinkButton ID="lbtnSD" runat="server" onclick="lbtnSD_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnTD" runat="server" onclick="lblTD_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnST" runat="server" onclick="lblST_Click"></asp:LinkButton>
    </div>

     <div id="divSourceCol" class="coldiv"><ul></ul></div>
     <input id="hideSoureColTxt" type="hidden" value="" />
     <div id="divTargetCol" class="coldiv"><ul></ul></div>
     <div id="divColType" class="coldiv"><ul></ul></div>
  </form>
</body>
</html>
