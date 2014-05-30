<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskAdd.aspx.cs" MasterPageFile="~/Site.master" Title="五洲在线数据交换平台-新增任务" Inherits="MDT.WebUI.Management.Configuration.Task.TaskAdd" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
    .ShowTaskdiv  /*下拉框显示选项的div*/
	{
		position:absolute;
		height:auto;
		max-height:90px;
		text-align:left;
		background-color:#fff; /*#C6DDFC  #D4E7FF #F3FAFA #EAF4FF #FFE7A2 #ffffff*/
		border:1px #587897 solid; /*#A3C0E8 #F4AA2E  #D4E7FF*/
		overflow-y:auto;
		display:none;
		font-size:12px;
		left:0;
		top:0;
   }
   .imBtn{width:20px;height:23px;display:inline-block;vertical-align:bottom;+vertical-align:text-bottom;}
    </style>
<script language="javascript" type="text/javascript">
    function checkInput() {
        if (G('MainContent_txtTaskName').value.Trim() == "") {
            G('MainContent_txtTaskName').focus();
            alert("请输入任务名称！");
            return false;
        }
        if (G('MainContent_txtNode').value.Trim() == "") {
            G('MainContent_txtNode').focus();
            alert("请输入任务描述！");
            return false;
        }
        if (G('MainContent_txtInterval').value.Trim() == "") {
            G('MainContent_txtInterval').focus();
            alert("请输入时间间隔！");
            return false;
        }
        return true;
    }

    function checkDelete() {
        if (confirm("是否确定删除！")) {
            return true;
        }
        return false;
    }
    function bindData() {
        G('MainContent_lbtnRefresh').click();
    }

    function closeWindow() {
        if (confirm('关闭本页面并不保存设置，是否确定！')) {
            window.location.href = "TaskBrowser.aspx";
        }
    }

    window.onbeforeunload = function () {
        if ((event.clientX > document.body.clientWidth && event.clientY < 0) || event.altKey) {
            G('MainContent_lbtnClose').click();
        }
    }

    $(function () {
        $('.mtd').PromptInputSelect({
            L: 0, //下拉选型的左偏移量，默认为0
            T: 1, //下拉选型的上偏移量，默认为0
            W: 20, //下拉选型的宽偏移量，默认为0
            N: 5, //上下键操作时滚动条跟随移动的选项数，默认为5
            beforehand: true,  //加载时生成下拉节点
            liClass: 'liA12', 	//选项li的类，默认为5
            liActive: 'liA12On', //下拉选项li被选中时的状态，默认为liActive
            liOver: 'liOver', //鼠标移动至下拉选项li上的状态，默认为liOver
            conDiv: 'ShowTaskdiv'	//下拉选项外围的类，默认为conDiv
        });
    })
</script>
        <div id="dataGrid">
            <asp:LinkButton ID="lbtnRefresh" runat="server" onclick="lbtnRefresh_Click" CssClass="hide">刷新</asp:LinkButton>

    <table id="tbMain" width="100%" cellpadding="2" cellspacing="8" border="0" >
        <tr>
            <td class="titleTR" colspan="2">&nbsp;&nbsp;<img src="/images/icon.png" alt="" style="margin-top:2px;" />&nbsp;&nbsp;基本信息 </td>
        </tr>
        <tr>
            <td style="width:10px;">&nbsp;</td>
            <td align="left">
            <table style="width:100%;">
                <tr>
                    <td align="left" >任务名称：<asp:TextBox ID="txtTaskName" CssClass="textField44" runat="server"></asp:TextBox></td>
                    <td align="left">任务描述：<asp:TextBox ID="txtNode" CssClass="textField44" runat="server"></asp:TextBox></td>
                    <td align="left">时间间隔：<asp:TextBox ID="txtInterval" CssClass="textField26" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            </td>
            
        </tr>
        <tr>
            <td class="titleTR" colspan="2">&nbsp;&nbsp;<img src="/images/icon.png" alt="" style="margin-top:2px;" />&nbsp;&nbsp;映射关系
            
                <a id="addMapping" onclick="show('hideMapping','iframeMapping','MainMapping.aspx?fileName=<%=BasePath %>')" style="float:right;">新增</a>
                <!--[if IE 7]> 
                    <script language="javascript" type="text/javascript">
                        G('addMapping').style.marginTop = -10;
                    </script>
                 <![endif]-->
             </td>
        </tr>
        <tr>
            <td style="width:10px;">&nbsp;</td>
            <td>
                <asp:Repeater ID="Repeater1" runat="server" 
                    onitemcommand="Repeater1_ItemCommand">
                <HeaderTemplate>
                    <table id="dataTable" style="width:100%;">
                    <thead>
                    <th>来源数据</th>
                    <th>来源数据实体</th>
                    <th>目标数据</th>
                    <th>目标数据实体</th>
                    <th style="width:50px;">修改</th>
                    <th style="width:50px;">删除</th>
                    </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#DataBinder.Eval(Container,"DataItem.SourceDb") %></td>
                        <td><%#DataBinder.Eval(Container,"DataItem.SourceTable") %></td>
                        <td><%#DataBinder.Eval(Container,"DataItem.TargetDb") %></td>
                        <td><%#DataBinder.Eval(Container,"DataItem.TargetTable") %></td>
                        <td>
                            <img src="/images/textedit.gif" class="mt5" onclick="show('hideMapping','iframeMapping','MainMapping.aspx?Id=<%#DataBinder.Eval(Container,"DataItem.Id") %>&fileName=<%=BasePath %>')" alt="" style="cursor:hand;" />
                        </td>
                        <td>
                            <asp:Label ID="lblTId" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' CssClass="hide" ></asp:Label>
                            <asp:LinkButton ID="lbtnDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody></table>
                </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td class="titleTR" colspan="2">&nbsp;&nbsp;<img src="/images/icon.png" alt="" style="margin-top:2px;" />&nbsp;&nbsp;配置文件</td>
        </tr>
        <tr>
            <td style="width:10px;">&nbsp;</td>
            <td>
            
            <table id="dataTable" cellpadding="0" cellspacing="0" style="width:100%;">
                <thead>
                <th>配置文件</th>
                <th>任务单元</th>
                <th>命令</th>
                <th>事后任务</th>
                <th>结果集</th>
                <th>记录日志</th>
                <th>启用事务</th>
                <th>添加命令</th>
                <th>添加事后任务</th>
                <th>添加结果集</th>
                </thead>
                <tbody>
                <asp:Repeater ID="Repeater2" runat="server" onitemdatabound="Repeater2_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>源</td>
                        <td>
                            <%#DataBinder.Eval(Container, "DataItem.Name")%>
                            <asp:Label ID="lblId" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' CssClass="hide"></asp:Label>
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater3" runat="server" OnItemCommand="Repeater3_ItemCommand">
                            <ItemTemplate>
                                <div style=" width:180px;">
                                <asp:Label ID="lblSCommand" Width="132px" runat="server" Text='<%#GetStringPartContent(DataBinder.Eval(Container,"DataItem.CommandName").ToString(),18,true) %>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.CommandName") %>'></asp:Label>
                                <asp:Label ID="lblId2" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' Visible="false"></asp:Label>
                                 
                                <asp:LinkButton ID="lbtnSCEdit" CommandName="Modify" runat="server" ToolTip="修改命令"><img src="/images/textedit.gif" class="mt5" alt=""/></asp:LinkButton>
                                <asp:LinkButton ID="lbtnSCDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除命令"><img src="/images/no2.gif" class="mt5" alt=""/></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater6" runat="server" OnItemCommand="Repeater6_ItemCommand">
                            <ItemTemplate>
                                <div style="width:180px;">
                                <asp:Label ID="lblSPost" Width="120px" runat="server" Text='<%#GetStringPartContent(DataBinder.Eval(Container,"DataItem.CommandName").ToString(),18,true) %>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.CommandName") %>'></asp:Label>
                                <asp:Label ID="lblId2" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' CssClass="hide"></asp:Label>
                                <asp:LinkButton ID="lbtnSPEdit" CommandName="Modify" runat="server" ToolTip="修改事后任务"><img src="/images/textedit.gif" class="mt5" alt="" /></asp:LinkButton>
                                <asp:LinkButton ID="lbtnSPDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除事后任务"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater7" runat="server" OnItemCommand="Repeater7_ItemCommand">
                            <ItemTemplate>
                                <div style="width:120px;">
                                <asp:Label ID="lblResult" runat="server" Text='<%#GetStringPartContent(DataBinder.Eval(Container,"DataItem.CommandName").ToString(),8,true)%>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.CommandName") %>'></asp:Label>
                                <asp:Label ID="lblResultId" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' CssClass="hide"></asp:Label>
                                <asp:LinkButton ID="lbtnSREdit" CommandName="Modify" runat="server" ToolTip="修改结果集"><img src="/images/textedit.gif" class="mt5" alt="" /></asp:LinkButton>
                                <asp:LinkButton ID="lbtnSRDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除结果集"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                            
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtSLog" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTraceLog") %>' CssClass="textField2"></asp:TextBox>
                            <asp:Image ID="imgSLog" ImageUrl="/images/selectRight.png" style="margin-left:-3px; margin-top:1px;" runat="server" />--%>
                            <asp:TextBox ID="txtSLog" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTraceLog") %>' CssClass="textField2 mtd" to="MainContent_hideData"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtSTran" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTransaction") %>' CssClass="textField2"></asp:TextBox>
                            <asp:Image ID="imgSTran" ImageUrl="/images/selectRight.png" style="margin-left:-3px; margin-top:1px;" runat="server" />--%>
                            <asp:TextBox ID="txtSTran" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTransaction") %>' CssClass="textField2 mtd" to="MainContent_hideData" ></asp:TextBox>
                        </td>
                        <td valign="middle">
                            <img src="/images/2.gif" alt="" onclick="SourceConfigTitle('配置命令');show('hideCommand','iframeCommand','SourceConfig.aspx?tid=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=s&fileName=<%=BasePath %>');" title="添加命令" class="mt5" style="cursor:hand;" />
                        </td>
                        <td valign="middle">
                            <%--<a href="javascript:openwindow('SourceConfig.aspx?tid=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=sp','配置Command','1000','600');" title="添加事后任务"><img src="/images/2.gif" alt="" /></a>--%>
                            <img src="/images/2.gif" alt="" onclick="SourceConfigTitle('配置事后任务');show('hideCommand','iframeCommand','SourceConfig.aspx?tid=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=sp&fileName=<%=BasePath %>');" title="添加事后任务" class="mt5" style="cursor:hand;" />
                        </td>
                        <td valign="middle">
                            <%--<a href="javascript:openwindow('ResultEdit.aspx?ID=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=s','','760','300');" title="添加结果集"><img src="/images/2.gif" alt="" /></a>--%>
                            <img src="/images/2.gif" alt="" onclick="show('hideResults','iframeResults','ResultEdit.aspx?ID=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=s&fileName=<%=BasePath %>');" title="添加结果集" class="mt5" style="cursor:hand;" />
                        </td>
                    </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="Repeater4" runat="server" onitemdatabound="Repeater4_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>目标</td>
                        <td>
                            <%#DataBinder.Eval(Container, "DataItem.Name")%>
                            <asp:Label ID="lblId" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' CssClass="hide"></asp:Label>
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater5" runat="server" OnItemCommand="Repeater5_ItemCommand">
                            <ItemTemplate>
                                <div style="width:180px;">
                                <asp:Label ID="lblTCommand" Width="132px" runat="server" Text='<%#GetStringPartContent(DataBinder.Eval(Container, "DataItem.CommandName").ToString(),18,true)%>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.CommandName") %>'></asp:Label>
                                <asp:Label ID="lblId2" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' Visible="false"></asp:Label>
                               
                                <asp:LinkButton ID="lbtnTCEdit" CommandName="Modify" runat="server" ToolTip="修改命令"><img src="/images/textedit.gif" class="mt5" alt="" /></asp:LinkButton>
                                <asp:LinkButton ID="lbtnTCDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除命令"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater8" runat="server" OnItemCommand="Repeater8_ItemCommand">
                            <ItemTemplate>
                                <div style="width:180px;">
                                <asp:Label ID="lblTPostCommand" Width="120px" runat="server" Text='<%#GetStringPartContent(DataBinder.Eval(Container,"DataItem.CommandName").ToString(),18,true) %>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.CommandName") %>'></asp:Label>
                                <asp:Label ID="lblId2" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.Id") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lbtnTCEdit" CommandName="Modify" runat="server" ToolTip="修改事后任务"><img src="/images/textedit.gif" class="mt5" alt="" /></asp:LinkButton>
                                  
                                <asp:LinkButton ID="lbtnTCDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除事后任务"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:Repeater ID="Repeater9" runat="server" OnItemCommand="Repeater9_ItemCommand">
                            <ItemTemplate>
                                <div style="width:120px;">
                                <asp:Label ID="lblTResult" runat="server" Text='<%#GetStringPartContent(DataBinder.Eval(Container,"DataItem.CommandName").ToString(),8,true) %>' ToolTip='<%#DataBinder.Eval(Container,"DataItem.CommandName") %>'></asp:Label>
                                <asp:LinkButton ID="lbtnTCEdit" CommandName="Modify" runat="server" ToolTip="修改结果集"><img src="/images/textedit.gif" class="mt5" alt="" /></asp:LinkButton>
                                <asp:LinkButton ID="lbtnTCDelete" CommandName="Delete" runat="server" OnClientClick="return checkDelete()" ToolTip="删除结果集"><img src="/images/no2.gif" class="mt5" alt="" /></asp:LinkButton>
                                </span>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                            
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtTLog" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTraceLog") %>' CssClass="textField2"></asp:TextBox>
                            <asp:Image ID="imgTLog" ImageUrl="/images/selectRight.png" style="margin-left:-3px; margin-top:1px;" runat="server" />--%>
                            <asp:TextBox ID="txtTLog" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTraceLog") %>' CssClass="textField2 mtd" to="MainContent_hideData"  onfocus="this.blur();" ></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtTTran" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTransaction") %>' CssClass="textField2"></asp:TextBox>
                            <asp:Image ID="imgTTran" ImageUrl="/images/selectRight.png" style="margin-left:-3px; margin-top:1px;" runat="server" />--%>
                            <asp:TextBox ID="txtTTran" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.HasTransaction") %>' CssClass="textField2 mtd" to="MainContent_hideData"  onfocus="this.blur();"  ></asp:TextBox>
                        </td>
                        <td valign="middle">
                            <img src="/images/2.gif" alt="" onclick="SourceConfigTitle('配置命令');show('hideCommand','iframeCommand','SourceConfig.aspx?tid=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=t&fileName=<%=BasePath %>');" title="添加命令" class="mt5" style="cursor:hand;" />
                        </td>
                        <td valign="middle">
                            <img src="/images/2.gif" alt="" onclick="SourceConfigTitle('配置事后任务');show('hideCommand','iframeCommand','SourceConfig.aspx?tid=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=tp&fileName=<%=BasePath %>');" title="添加命令" class="mt5" style="cursor:hand;" />
                        </td>
                        <td valign="middle">
                            <img src="/images/2.gif" alt="" onclick="show('hideResults','iframeResults','ResultEdit.aspx?ID=<%#DataBinder.Eval(Container,"DataItem.Id") %>&Type=t&fileName=<%=BasePath %>');" title="添加结果集" class="mt5" style="cursor:hand;" />
                        </td>
                    </tr>    
                </ItemTemplate>
                </asp:Repeater>
                </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:LinkButton ID="lbtnXMLView" CssClass="aBtn" runat="server" ToolTip="预览配置生产的xml数据"
                    onclick="lbtnXMLView_Click"><span><em>预览xml数据</em></span></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbtnXMLModify" CssClass="aBtn" runat="server" onclick="lbtnXMLModify_Click" 
                    ToolTip="手动修改配置生产的xml数据" ><span><em>手动修改xml数据</em></span></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lbtnSave" CssClass="aBtn" runat="server" ToolTip="保存编辑的任务信息" 
                    onclick="lbtnSave_Click" OnClientClick="return checkInput()"><span><em>保存</em></span></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="LbtnClose2" runat="server" ToolTip="关闭本页面并不保存设置" 
                    CssClass="aBtn" onclick="LbtnClose2_Click"><span><em>关闭页面</em></span></asp:LinkButton>
                <%--<a id="LbtnClose2" title="关闭本页面并不保存设置" onclick="closeWindow();" class="aBtn"><span><em>关闭页面</em></span></a>--%>

            </td>
        </tr>
        </table>
    </div>

    <div id="divShow" class="ShowTaskdiv" runat="server"></div>
    <input id="hideName" type="hidden" runat="server" value="" />
    <input id="hideData" type="hidden" runat="server" value="0*True|0*False" />
    <input id="hideHdb" type="hidden" runat="server" />
    <input id="hideFileName" type="hidden" runat="server"/>
    <asp:LinkButton ID="lbtnClose" runat="server" onclick="lblClose_Click"></asp:LinkButton>



    <!--------         弹出Div            ---------->
    <script language="ecmascript" type="text/javascript">
        function SourceConfigTitle(title) {
            G('sourceConfigTitle').innerHTML = title;
        }
    </script>

    <div id="hidebg"></div>
    <div id="hideMapping" class="openDiv" style="width:1040px; height:555px;">        <div class="columnTitle"><div class="columnTitle_t">配置映射关系</div><div class="columnTitle_r"><img src="/images/searchClose.gif" alt="关闭" title="关闭" onclick="closeDiv('hideMapping','iframeMapping')" /></div></div>
        <div class="columnContent" style=" text-align:center; height:290px;">        <iframe id="iframeMapping" src="" frameborder="0" scrolling="yes" width="1040px" height="526px"></iframe>        </div>    </div>    <div id="hideCommand" class="openDiv" style="width:920px; height:500px;">        <div class="columnTitle"><div id="sourceConfigTitle" class="columnTitle_t">配置命令</div><div class="columnTitle_r"><img src="/images/searchClose.gif" alt="关闭" title="关闭" onclick="closeDiv('hideCommand','iframeCommand')" /></div></div>
        <div class="columnContent" style=" text-align:center; height:290px;">        <iframe id="iframeCommand" src="" frameborder="0" scrolling="no" width="100%" height="460px"></iframe>        </div>    </div>
    <div id="hideResults" class="openDiv" style="width:660px; height:164px;">        <div class="columnTitle"><div id="Div1" class="columnTitle_t">配置结果集</div><div class="columnTitle_r"><img src="/images/searchClose.gif" alt="关闭" title="关闭" onclick="closeDiv('hideResults','iframeResults')" /></div></div>
        <div class="columnContent" style=" text-align:center; height:100px;">        <iframe id="iframeResults" src="" frameborder="0" scrolling="no" width="660px" height="100px"></iframe>        </div>    </div>
    <div id="hideView" class="openDiv" style="width:850px; height:506px;">        <div class="columnTitle"><div id="Div2" class="columnTitle_t">预览XML</div><div class="columnTitle_r"><img src="/images/searchClose.gif" alt="关闭" title="关闭" onclick="closeDiv('hideView','iframeView')" /></div></div>
        <div class="columnContent" style=" text-align:center; height:100px;">        <iframe id="iframeView" src="" frameborder="0" scrolling="no" width="850px" height="460px"></iframe>        </div>    </div>
    <div id="hideModifyXML" class="openDiv" style="width:900px; height:570px;">        <div class="columnTitle2"><div id="Div3" class="columnTitle_t">手动修改xml数据</div><div class="columnTitle_r"><img src="/images/searchClose.gif" alt="关闭" title="关闭" onclick="closeDiv('hideModifyXML','iframeModifyXML')" /></div></div>
        <div class="columnContent" style=" text-align:center; height:100px;">        <iframe id="iframeModifyXML" src="" frameborder="0" scrolling="no" width="900px" height="540px"></iframe>        </div>    </div>
</asp:Content>