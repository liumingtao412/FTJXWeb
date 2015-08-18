<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="News.aspx.cs" Inherits="News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1">公司新闻</div>
    </div>
    <div class="defaltContainer">
        <asp:Repeater ID="NewsRepeater" runat="server" OnItemCommand="NewsRepeater_ItemCommand">
            <ItemTemplate>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 700px;">
                    <tr>
                        <td style="width: 20px; vertical-align: bottom;">
                            <img height="16px" width="16px" src="images/news2.gif" alt="newsarrer" /></td>
                        <td style="vertical-align: bottom; width: 500px; height: 30px; list-style-position: outside;
                            border-bottom: silver 1px dotted; list-style-type: disc;">
                                <%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "isTop")) ? "<img  src='images/hot.gif' />" : " "%>
                            <a target="_blank" href="NewsDetails.aspx?NewsID=<%#DataBinder.Eval(Container.DataItem,"编号") %>"
                                title="<%#DataBinder.Eval(Container.DataItem,"newsTitle")%>">
                                <%#DataBinder.Eval(Container.DataItem, "newsTitle").ToString().Length > 28 ? DataBinder.Eval(Container.DataItem, "newsTitle").ToString().Substring(0,28)+"…" : DataBinder.Eval(Container.DataItem, "newsTitle")%>
                            </a>
                        </td>
                        <td style="vertical-align: bottom; text-align: right; width: 100px; list-style-position: outside;
                            border-bottom: silver 1px dotted; list-style-type: disc;">
                            <%#DataBinder.Eval(Container.DataItem, "newsTime", "{0:yyyy-MM-dd}")%>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
           <div class="Clear"></div>
        <div class="Paging">
         当前页:<asp:Label ID="CurrentPageLabel" runat="server" Text="1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
    总页数:<asp:Label ID="TotalPageLabel" runat="server" Text="1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="FirstLinkButton" runat="server" OnClick="FirstLinkButton_Click">首页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="PreLinkButton" runat="server" OnClick="PreLinkButton_Click">上一页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="NextLinkButton" runat="server" OnClick="NextLinkButton_Click">下一页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LastLinkButton" runat="server" OnClick="LastLinkButton_Click">末页</asp:LinkButton>&nbsp;&nbsp;
    转到第<asp:DropDownList ID="DropDownListPageNum" runat="server" AutoPostBack="true"
            onselectedindexchanged="DropDownListPageNum_SelectedIndexChanged"  >
            <asp:ListItem>1</asp:ListItem>
        </asp:DropDownList>页   
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
    <img height="195" alt="富藤科技公司新闻" src="images/b/news.jpg" width="960" />
</asp:Content>
