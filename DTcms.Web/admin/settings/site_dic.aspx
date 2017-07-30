<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_dic.aspx.cs" Inherits="DTcms.Web.admin.settings.site_dic" %>

<%@ Import Namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>字典管理</title>
    <link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
            <i class="arrow"></i>
            <span>字典管理</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div id="floatHead" class="toolbar-wrap">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"></a>
                    <div class="l-list">
                        <div class="menu-list">
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <ul class="icon-list">
                            <li><a class="add" href="sitedic_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&pid=<%=ddltype.SelectedValue %>"><i></i><span>新增</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="save" OnClick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton></li>
                            <li style="display: none;"><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                            <li style="display: none;">
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','本操作会删除本导航及下属子导航，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <div class="table-container">
            <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                        <tr>
                            <th width="8%"></th>
                            <th align="left" width="25%">字典名称</th>
                            <th align="left">备注说明</th>
                            <th align="left" width="65">排序</th>
                            <th width="12%">操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle; display: none" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                            <asp:HiddenField ID="hidLayer" Value='<%#Eval("class_layer") %>' runat="server" />
                        </td>
                        <td style="white-space: nowrap; word-break: break-all; overflow: hidden;" title="<%#Eval("dicname")%>">
                            <asp:Literal ID="LitFirst" runat="server"></asp:Literal>
                            <%#Eval("dicname").ToString().Length>50?Eval("dicname").ToString().Substring(0,50)+"...":Eval("dicname")%>
                        </td>
                        <td align="left" title="<%#Eval("detail")%>">
                            <%#Eval("detail").ToString().Length<50?Eval("detail").ToString():Eval("detail").ToString().Substring(0,50)+"..."%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" /></td>
                        <td align="center" style="white-space: nowrap; word-break: break-all; overflow: hidden;">
                            <a style="<%#Eval("class_layer").ToString()=="10"?"display:none;": ""%>" href="sitedic_edit.aspx?action=<%#DTEnums.ActionEnum.Add %>&id=<%#Eval("id")%>&pid=<%=ddltype.SelectedValue %>">添加子级</a>
                            <a href="sitedic_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>&pid=<%=ddltype.SelectedValue %>">修改</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无记录</td></tr>" : ""%>
</table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!--/列表-->
    </form>
</body>
</html>
