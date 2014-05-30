<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="TaskModify.aspx.cs" Inherits="MDT.WebUI.Management.Configuration.Task.TaskModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>

    <script language="javascript" type="text/javascript" src="/js/editarea_0_8_2/edit_area/edit_area_loader.js"></script>
    <script language="javascript" type="text/javascript">
        //保存时获取 文本域的值
        function clientSave() { 
            var array = editAreaLoader.getAllFiles("txtData");//获取在txtData文本域中打开的所有文件
            var objMapping = array['mapping']; //获取id 为mapping的文件
            var objXSLT = array['xslt']; //获取id为xslt的文件
            var objSConfig = array['sconfig'];  //获取id为sconfig的文件
            var objTConfig = array['tconfig'];  //获取id为tconfig的文件
            if (objMapping != null) {
                document.getElementById('hideMapping').value = objMapping.text;  //将修改过的值保存在相应的hidden里面
            }
            if (objXSLT != null) {
                document.getElementById('hideXSLT').value = objXSLT.text;
            }
            if (objSConfig != null) {
                document.getElementById('hideSConfig').value = objSConfig.text;
            }
            if (objTConfig != null) {
                document.getElementById('hideTConfig').value = objTConfig.text;
            }
            return true;
        }

        editAreaLoader.init({  //初始化xml编辑器
            id: "txtData"	// id of the textarea to transform	
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

        // callback functions
        function my_save(id, content) {
            alert("Here is the content of the EditArea '" + id + "' as received by the save callback function:\n" + content);
        }

        function my_load(id) {
            editAreaLoader.setValue(id, "The content is loaded from the load_callback function into EditArea");
        }

        function test_setSelectionRange(id) {
            editAreaLoader.setSelectionRange(id, 100, 150);
        }

        function test_getSelectionRange(id) {
            var sel = editAreaLoader.getSelectionRange(id);
            alert("start: " + sel["start"] + "\nend: " + sel["end"]);
        }

        function test_setSelectedText(id) {
            text = "[REPLACED SELECTION]";
            editAreaLoader.setSelectedText(id, text);
        }

        function test_getSelectedText(id) {
            alert(editAreaLoader.getSelectedText(id));
        }
        function editAreaLoaded(id) {
            if (id == "txtData") {
                open_file1('mapping', 'hideMapping', 'Mapping');
                open_file1('xslt', 'hideXSLT', 'XSLTInfo');
                open_file1('sconfig', 'hideSConfig', 'SourceConfigFrom');
                open_file1('tconfig', 'hideTConfig', 'SourceConfigTo');
            }
        }

        function open_file1(i,hide,t) {
            var str = document.getElementById(hide).value;
            var source = new ActiveXObject("Msxml2.DOMDocument");
            source.async = false;
            source.loadXML(str);//加载xml数据

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
            editAreaLoader.openFile('txtData', new_file);
        }

        function toogle_editable(id) {
            editAreaLoader.execCommand(id, 'set_editable', !editAreaLoader.execCommand(id, 'is_editable'));
        }

        function close_file1() {
            editAreaLoader.closeFile('txtData', "mapping");
        }

        //关闭本页同时刷新父页面
        function openerRefresh(type) {
            if (type == "add") {
//                this.opener.bindData();
                parent.bindData();
                parent.hide('hideModifyXML', 'iframeModifyXML');
            }
            else if (type == "browser") {
                //this.opener.RefreshWindows();
                //parent.RefreshWindows();
                parent.hide('hideModifyXML', 'iframeModifyXML');
            }
        }

        function checkClose() {
            if (confirm('关闭本页面并不保存数据，是否确定？')) {
                parent.hide('hideModifyXML', 'iframeModifyXML');
            }
        }
    </script>
</head>
<body style="margin:0px 10px 0px 10px;">
    <form id="form1" runat="server">
        <table cellpadding="2" cellspacing="5" border="0" width="860px">
        <tr>
            <td>
                <div>
		            <textarea id="txtData" runat="server" style="height:460px; width: 99%;" name="test_2">
		            </textarea>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="lbtnSave" CssClass="aBtn" runat="server" OnClientClick="return clientSave();" OnClick="btnSave_Click"><span><em>保存</em></span></asp:LinkButton>&nbsp;&nbsp;
                <a class="aBtn" onclick="checkClose();" ><span><em>关闭页面</em></span></a>&nbsp;&nbsp;
            </td>
        </tr>
        </table>
    <input id="hideMapping" type="hidden" value="" runat="server" />
    <input id="hideXSLT" type="hidden" value="" runat="server" />
    <input id="hideSConfig" type="hidden" value="" runat="server"/>
    <input id="hideTConfig" type="hidden" value="" runat="server" />
    <input id="hideData" type="hidden" value="" runat="server" />
    <input id="hideFileName" type="hidden" runat="server" />
    </form>
</body>
</html>
