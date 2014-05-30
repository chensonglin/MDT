<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLFile.aspx.cs" Inherits="MDT.WebUI.XMLFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/layout.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/js/mic.main.js"></script>
    <script type="text/javascript" src="/js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="/js/OpenWindow.js"></script>
    <script language="javascript" type="text/javascript">
        //点击标题行  显示或隐藏相关信息
        var arrayTr = new Array('trMapping', 'trXSLT', 'trSourceConfig', 'trTargetConfig');
        var arrayLbl = new Array('lblMapping', 'lblXSLT', 'lblSourceConfig', 'lblTargetConfig');
        function displayTr(lbl, tr) {
            var trD = G(tr);
            if (trD.style.display == "none") {
                for (var i = 0; i < arrayTr.length; i++) {
                    if (tr == arrayTr[i]) {
                        trD.style.display = "block";
                        G(lbl).innerHTML = "-";
                    }
                    else {
                        G(arrayTr[i]).style.display = "none";
                        G(arrayLbl[i]).innerHTML = "+";
                    }
                }
            }
            else {
                trD.style.display = "none";
                G(lbl).innerHTML = "+";
            }
        }

        function ifSrc(id) {
            if (id == "") {
                var fileName = document.getElementById('hideFileName').value;
                document.getElementById("ifMapping").src = "XmlFile2.aspx?Type=Mapping&fileName=" + fileName;
                document.getElementById("ifXSLT").src = "XmlFile2.aspx?Type=XSLT&fileName=" + fileName;
                document.getElementById("ifSConfig").src = "XmlFile2.aspx?Type=SourceConfig&fileName=" + fileName;
                document.getElementById("ifTConfig").src = "XmlFile2.aspx?Type=TargetConfig&fileName=" + fileName;
            }
            else {
                document.getElementById("ifMapping").src = "XmlFile2.aspx?Type=Mapping&id=" + id;
                document.getElementById("ifXSLT").src = "XmlFile2.aspx?Type=XSLT&id=" + id;
                document.getElementById("ifSConfig").src = "XmlFile2.aspx?Type=SourceConfig&id=" + id;
                document.getElementById("ifTConfig").src = "XmlFile2.aspx?Type=TargetConfig&id=" + id;
            }
        }

        function checkClose() {
            if (confirm('是否确定关闭本页面？')) {
                parent.hide('hideView', 'iframeView');
            }
        }
    </script>
</head>
<body style="margin:0px 0px 0px 0px; width:800px;">
    <form id="form1" runat="server">
    <div>
    <table id="tbMani" width="800px" cellpadding="2" cellspacing="5">
    <tr>
        <td>
        <div style="width:100%; overflow-Y:auto; overflow-X:hidden; max-height:450px;">
            <table id="tableWidth" width="100%">
            <!--[if IE 7]> 
                <script language="javascript" type="text/javascript">
                    G('tableWidth').width = 780+'px';
                </script>
            <![endif]-->
            <tr onclick="displayTr('lblMapping','trMapping')">
                <td class="titleTR">&nbsp;&nbsp;<asp:Label ID="lblMapping" runat="server" Text="-"></asp:Label>&nbsp;&nbsp;Mapping</td>
            </tr>
            <tr id="trMapping" runat="server">
                <td>
                    <iframe id="ifMapping" style="width:100%; height:300px; border:solid #DBDBDB 1px;" class="textarea"
                        scrolling="auto" frameborder="0"></iframe>
                </td>
            </tr>
            <tr onclick="displayTr('lblXSLT','trXSLT')">
                <td class="titleTR">&nbsp;&nbsp;<asp:Label ID="lblXSLT" runat="server" Text="+"></asp:Label>&nbsp;&nbsp;XSLT</td>
            </tr>
            <tr id="trXSLT" runat="server">
                <td>
                    <iframe id="ifXSLT" style="width:100%; height:300px; border:solid #DBDBDB 1px;" class="textarea"
                        scrolling="auto" frameborder="0"></iframe>
                </td>
            </tr>
            <tr onclick="displayTr('lblSourceConfig','trSourceConfig')">
                <td class="titleTR">&nbsp;&nbsp;<asp:Label ID="lblSourceConfig" runat="server" Text="+"></asp:Label>&nbsp;&nbsp;源SourceConfig</td>
            </tr>
            <tr id="trSourceConfig" runat="server">
                <td>
                    <iframe id="ifSConfig" class="textarea" style="width:100%; height:300px; border:solid #DBDBDB 1px;" 
                        scrolling="auto" frameborder="0"></iframe>
                </td>
            </tr>
            <tr onclick="displayTr('lblTargetConfig','trTargetConfig')">
                <td class="titleTR">&nbsp;&nbsp;<asp:Label ID="lblTargetConfig" runat="server" Text="+"></asp:Label>&nbsp;&nbsp;目标SourceConfig</td>
            </tr>
            <tr id="trTargetConfig" runat="server">
                <td>
                    <iframe id="ifTConfig" style="width:100%; height:300px; border:solid #DBDBDB 1px;" class="textarea"
                        scrolling="auto" frameborder="0"></iframe>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <a class="aBtn" href="javascript:checkClose();"><span><em>关闭</em></span></a>&nbsp;
                </td>
            </tr>
            </table>
        
        </div>
        </td>
    </tr>
    </table>
    </div>
    <input id="hideFileName" type="hidden" runat="server" />
    </form>
</body>
</html>
