<%@ Page Title="五洲在线数据交换平台-OAuth申请" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MDT.WebUI.Management.OAuth.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" src="/js/jquery.cookies.js"></script>
    <style type="text/css">
        #bgDiv /*页面中实现等待提示的div样式*/
        {
            position: absolute;
            background-color: White;
            filter: progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);
            opacity: 0.5;
            left: 0;
            top: 0;
            z-index: 10000;
            width: 1%;
        }
        #messagediv /*页面中实现等待提示的div样式*/
        {
            position: absolute;
            margin: 150px auto;
            width: 300px;
            height: 60px;
            text-align: center;
            filter: alpha(Opacity=80);
            -moz-opacity: 0.8;
            opacity: 0.8;
            background-color: #FFE7A2; /*#C6DDFC  #D4E7FF #F3FAFA #EAF4FF #FFE7A2 #ffffff*/
            z-index: 100011;
            font-size: larger;
            font-weight: bolder;
            top: 104px;
            left: 580px;
            border: 1px #F4AA2E solid; /*#A3C0E8 #F4AA2E  #D4E7FF*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="">
        <table id="dataTable" style="width: 800px;">
            <thead>
                <tr>
                    <th colspan="2">
                        &nbsp;
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td style="width: 300px;">
                        App Key
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtAppKey" runat="server" CssClass="textField30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        App Secret
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtAppSecret" runat="server" CssClass="textField30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Code
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="textField30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        RedirectUrl</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtRedirectUrl" runat="server" CssClass="textField30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Session Key
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSessionKey" runat="server" CssClass="textField30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        平台
                    </td>
                    <td style="text-align: left;">
                        <asp:RadioButtonList ID="rbtPlatfrom" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="0001">淘宝</asp:ListItem>
                            <asp:ListItem  Value="0004" >京东</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <a id="lbtnSubmit" class="aBtn" href="javascript:lbtnSubClick();"><span><em>提交</em></span></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#<%=rbtPlatfrom.ClientID%>").children(":radio").click(function () {
                switch ($(this).val()) {
                    case "0001":
                        $("#lbtnSubmit").attr("href", "javascript:lbtnSubClick();");
                        break;
                    case "0004":
                        $("#lbtnSubmit").attr("href", "javascript:lbtnJosLink();");
                        break;
                }
            });
        });

        function lbtnSubClick() {
            var appkey = $("#<%=txtAppKey.ClientID%>").val();
            var appsecret = $("#<%=txtAppSecret.ClientID%>").val();
            var code = $("#<%=txtCode.ClientID%>").val();
            var redirectUrl = $("#<%=txtRedirectUrl.ClientID%>").val();
            $.ajax({
                url: "TopOAuth.ashx",
                type: "POST",
                data: "appkey=" + appkey + "&appsecret=" + appsecret + "&code=" + code + "&redirectUrl=" + redirectUrl,
                success: function (e) {
                    //alert("申请成功!");
                    $("#<%=txtSessionKey.ClientID%>").val(e);
                }
            });
        }

        function lbtnJosLink() {
            var appkey = $("#<%=txtAppKey.ClientID%>").val();
            var redirectUrl = $("#<%=txtRedirectUrl.ClientID%>").val();
            var hrefStr = "http://auth.360buy.com/oauth/authorize?response_type=code&client_id=" + appkey
                + "&redirect_uri=" + redirectUrl + "&state=myststeid&scope=read";
            show('hideView', 'iframeView', hrefStr);
        }

        function closeFrame() {
            closeDiv2('hideView', 'iframeView');

            var code = $.cookies.get('JosCode');
            var state = $.cookies.get('JosState');

            $("#<%=txtCode.ClientID%>").val(code);

            var appkey = $("#<%=txtAppKey.ClientID%>").val();
            var appsecret = $("#<%=txtAppSecret.ClientID%>").val();
            var redirectUrl = $("#<%=txtRedirectUrl.ClientID%>").val();


            $.ajax({
                url: "JosOAuth.ashx",
                type: "GET",
                data: { grant_type: "authorization_code", client_id: appkey, client_secret: appsecret, code: code, redirect_uri: redirectUrl, state: state},
                dataType: "json",
                success: function (e) {
                    //alert("申请成功!");
                    alert(e["access_token"]);
                    $("#<%=txtSessionKey.ClientID%>").val(e["access_token"]);
                }
            });

        }
    </script>
    <div id="hidebg"></div>
    <div id="hideView" class="openDiv modalInfor" style="width:850px; height:626px;">  
      <h1 class="modalHeader"> <b class="fl pl10">授权</b> <span class="fr modalClose" onclick="closeDiv2('hideView','iframeView')"></span> </h1>  
      <div class="modalBody" style="width:848px; height:626px;">  
        <div class="modalContent" style="height:580px;">
            <iframe id="iframeView" src="" frameborder="0" scrolling="no" width="100%" height="607px" ></iframe>
        </div>  
      </div>  
     </div>
</asp:Content>
