<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMapping.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Task.MainMapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配置映射关系</title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/trHighLight.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <script type="text/javascript" src="/js/jQuery.PromptInputSelect.js"></script>
    <script type="text/javascript" src="/js/MainMappingSelect.js"></script>
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
        top: 64px;
        left: 381px;
        border:1px #F4AA2E solid; /*#A3C0E8 #F4AA2E  #D4E7FF*/
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
    .imBtn{
	vertical-align:bottom;
	+vertical-align:text-bottom;}
    </style>
     <script type="text/javascript">
         //关闭本页同时刷新父页面
         function openerRefresh() {
             parent.bindData();
             parent.hide('hideMapping', 'iframeMapping');
         }

         function scrollBottom() {
             var div = G("divScroll");
             div.scrollTop = div.scrollHeight;
         }

         function checkDelete() {
             if (confirm('确定删除所选行？')) {
                 return true;
             }
             return false;
         }
         function checkClose() {
             if (confirm('关闭本页面并不保存数据，是否确定？')) {
                 parent.hide('hideMapping', 'iframeMapping');
             }
         }
         function divScrollEvent() {
             $('.coldiv').hide();//隐藏下拉选项的div
         }

         //设置列的宽度  使表头表cflDataTable的列宽 等于 数据表dataTable的列宽
         function colunmWidth() {
             var objTable1 = G("cflDataTable");
             var objTable2 = G("dataTable");
             if (objTable1 && objTable2 && objTable2.rows.length > 0) {
                 for (var i = 0; i < objTable2.rows[0].cells.length; i++) {
                     var widthTest = objTable2.rows[0].cells[i].offsetWidth - 3;
                     if (widthTest < 1) {
                         return;
                     }
                     objTable1.rows[0].cells[i].width = widthTest;
                     if (i == 5) {
                         var sbarWidth = G('divScroll').offsetWidth - G('divScroll').scrollWidth;
                         objTable1.rows[0].cells[i].width = objTable2.rows[0].cells[i].offsetWidth + sbarWidth - 3;
                     }
                 }
             }
         }
         //如果来源列和目标列的 列名不一致，则行被标识为红色背景
         function changeColor() {
             var objTable = G("dataTable");
             if (objTable) {
                 for (var i = 0; i < objTable.rows.length; i++) {
                     var txtS = G("Repeater1_txtSourceCol_" + i);
                     var s = txtS.value.toUpperCase();
                     var txtT = G("Repeater1_txtTargetCol_" + i);
                     var t = txtT.value.toUpperCase();
                     if (s != t) {
                         objTable.rows[i].className = "cflLogFailedTr";
                     }
                 }
             }
         }
         function AddDefaultValue(obj) {//添加默认值
             if (obj.value != "") {
                 var id = obj.id;
                 var index = id.lastIndexOf('_');
                 var num = id.substring(index + 1, id.length);
                 G('Repeater1_txtSourceCol_' + num).value = "";
                 G('Repeater1_txtSourceType_' + num).value = "";
             }
         }

         function init() {
            $('.sdatabase').PromptInputSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true,  //加载时生成下拉节点
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'coldiv',	//下拉选项外围的类，默认为conDiv
                callback: sDataBaseEvent
            });
             
            $('.tdatabase').PromptInputSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true,  //加载时生成下拉节点
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                callback: tDataBaseEvent
            });
          
            $('.stable').PromptInputSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true,  //加载时不生成下拉节点
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                callback: sTableEvent
            });

            $('.ttable').PromptInputSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
                beforehand: true,  //加载时不生成下拉节点
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'coldiv', //下拉选项外围的类，默认为conDiv
                callback: sTableEvent
            });

//            $('.scol').PromptInputSelect({
//                L: 0, //下拉选型的左偏移量，默认为0
//                T: 1, //下拉选型的上偏移量，默认为0
//                W: 20, //下拉选型的宽偏移量，默认为0
//                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
//                beforehand: false,  //加载时不生成下拉节点
//                liClass: 'liA12', 	//选项li的类，默认为5
//                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
//                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
//                conDiv: 'coldiv'	//下拉选项外围的类，默认为conDiv
//            });

//            $('.stype').PromptInputSelect({
//                L: 0, //下拉选型的左偏移量，默认为0
//                T: 1, //下拉选型的上偏移量，默认为0
//                W: 20, //下拉选型的宽偏移量，默认为0
//                N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
//                beforehand: false,  //加载时不生成下拉节点
//                liClass: 'liA12', 	//选项li的类，默认为5
//                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
//                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
//                conDiv: 'coldiv'	//下拉选项外围的类，默认为conDiv
//            });

            $('.scol').MainMappingSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'coldiv'	//下拉选项外围的类，默认为conDiv
            });

            $('.stype').MainMappingSelect({
                L: 0, //下拉选型的左偏移量，默认为0
                T: 1, //下拉选型的上偏移量，默认为0
                W: 20, //下拉选型的宽偏移量，默认为0
                liClass: 'liA12', 	//选项li的类，默认为5
                liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
                liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
                conDiv: 'coldiv'	//下拉选项外围的类，默认为conDiv
            });
            
        }
        function sDataBaseEvent() {
            eval(document.getElementById('lbtnSD').href);
        }
        function tDataBaseEvent() {
            eval(document.getElementById('lbtnTD').href);
        }
        function sTableEvent() {
            eval(document.getElementById('lbtnST').href);
        }
     </script>
</head>
<body style="margin:0px 20px 0px 20px; width:1000px;OVERFLOW-Y:auto; OVERFLOW-X:hidden;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
         <div>
         <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <progresstemplate>
                <div id="bgDiv" class="bgDiv" style="height:230px"></div>
                <div id="messagediv">
                    <table>
                    <tr>
                        <td class="style1"><br />&nbsp;&nbsp;&nbsp;<img src="/images/loading.gif" alt="" border="0" />&nbsp;&nbsp;&nbsp; 数据正在加载中，请稍候........</td>
                    </tr>
                    </table>
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <table id="tbMain"  cellpadding="2" cellspacing="5" border="0" style="font-size:9pt; width:1000px; ">
        <tr>
            <td align="right">来&nbsp;&nbsp; 源&nbsp;&nbsp;数&nbsp;&nbsp; 据：</td>
            <td align="left">
               <input id="txtSDataBase" name="" type="text" class="textField260 sdatabase" to="hdb" runat="server"/>
              
            </td>
            <td align="right">目&nbsp;&nbsp;标 &nbsp;&nbsp;数&nbsp; &nbsp;据：</td>
            <td align="left">
                <input id="txtTDataBase" type="text" class="textField260 tdatabase" to="hdb" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">来源数据实体：</td>
            <td align="left">
                <input id="txtSTable" type="text" class="textField260 stable" to="hideSTable" runat="server"/>
            </td>
            <td align="right">目标数据实体：</td>
            <td align="left">
                <input id="txtTTable" type="text" class="textField260 ttable" to="hideTTable" runat="server"/>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="cflDataTable">
                <thead>
                    <tr>
                        <th>选择</th>
                        <th>来源列名</th>
                        <th>数据类型</th>
                        <th>目标列名</th>
                        <th>数据类型</th>
                        <th>默认值</th>
                    </tr>
                </thead>
                </table>
                <asp:Repeater ID="Repeater1" runat="server" >
                <HeaderTemplate>
                    <div id="divScroll" onscroll="divScrollEvent();" style="OVERFLOW-Y:auto; OVERFLOW-X:hidden; vertical-align:text-top; max-height:356px; border-bottom:1px solid #a3c0e8;">
                    <table id="dataTable" width="100%" style=" margin-top:-1px; margin-bottom:2px;">
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style=" height:35px;">
                        <td style="width:30px;">
                            <asp:CheckBox ID="ckDelete" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSourceCol" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.sourceName")%>' CssClass="textField26 scol" to="hideSCol"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSourceType" runat="server" Text='<%#FormatDataType(DataBinder.Eval(Container, "DataItem.sourceType").ToString())%>' class="textField2 stype" to="hideDataType"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTargetCol" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.targetName")%>' CssClass="textField26 scol" to="hideTCol"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTargetType" runat="server" value='<%#FormatDataType(DataBinder.Eval(Container, "DataItem.targetType").ToString())%>' class="textField2 stype" to="hideDataType" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDefault" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.defaultValue") %>' CssClass="textField" onblur="AddDefaultValue(this);"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                    </div>
                </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:LinkButton ID="lbtnAdd" CssClass="aBtn" runat="server" 
                        onclick="lbtnAdd_Click"><span><em>新增对应列</em></span></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="aBtn" OnClientClick="return checkDelete()" 
                    onclick="lbtnDelete_Click"><span><em>删除所选行</em></span></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="aBtn" 
                    onclick="LinkButton2_Click"><span><em>保存</em></span></asp:LinkButton>&nbsp;&nbsp;
                <a href="javascript:checkClose();" class="aBtn"><span><em>关闭</em></span></a>
            </td>
        </tr>
        </table>
        
            <input id="hdb" type="hidden" runat="server" />
            <input id="hideSTable" type="hidden" runat="server" />
            <input id="hideTTable" type="hidden" runat="server" />
            <input id="hideSCol" type="hidden" runat="server" />
            <input id="hideTCol" type="hidden" runat="server" />
            <input id="hideName" type="hidden" runat="server" value="txtSDataBase" />
            <input id="hideID" type="hidden" runat="server" value="txtSID" />
            <input id="hideHdb" type="hidden" runat="server" value="hdb" />
            <input id="hideLbtn" type="hidden" runat="server" value="" />
            <input id="hideDataType"  type="hidden" runat="server" value="0*字符型|0*数字型|0*日期型" />
            <input id="hideFileName" type="hidden" runat="server" />

        </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbtnST" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnSD" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lbtnTD" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel> 

    <div>
        <asp:LinkButton ID="lbtnSD" runat="server" onclick="lbtnSD_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnTD" runat="server" onclick="lblTD_Click"></asp:LinkButton>
        <asp:LinkButton ID="lbtnST" runat="server" onclick="lblST_Click"></asp:LinkButton>
    </div>
    </form>
</body>
</html>
