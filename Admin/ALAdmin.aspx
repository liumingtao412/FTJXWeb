<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="ALAdmin.aspx.cs" Inherits="admin_Default2" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:Panel ID="NewsListPanel" runat="server">
        <div class="savePage">
            <asp:Button ID="Button1" runat="server" Text="添加案例" OnClick="Article_New" /></div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
            BorderWidth="1px" CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1"
            EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" Width="100%"
            OnDataBound="GridView1_DataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Button ID="Button6" runat="server" Text="编辑" CommandName="doEdit" CommandArgument='<%#Eval("ID") %>'
                            OnCommand="Article_Command" />
                        <asp:Button ID="Button1" runat="server" Text="删除" OnClientClick="return confirm( '确定删除该记录? ');"
                            CommandName="doDelete" CommandArgument='<%#Eval("ID")%>' OnCommand="Article_Command" /></ItemTemplate>
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题" HeaderStyle-Width="80%">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="doView" CommandArgument='<%#Eval("ID") %>'
                            OnCommand="Article_Command">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("ALName")%>'></asp:Label></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <PagerTemplate>
                <asp:Button ID="Button5" runat="server" Text="添加文章" OnClick="Article_New" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server">第<%#this.GridView1.PageIndex+1%>页 / 共<%#this.GridView1.PageCount%>页</asp:Label>
                <asp:Button ID="Button3" runat="server" Text="首页" CommandName="Page" CommandArgument="First" />
                <asp:Button ID="Button2" runat="server" Text="上一页" CommandName="Page" CommandArgument="Prev" />
                <asp:Button ID="Button1" runat="server" Text="下一页" CommandName="Page" CommandArgument="Next" />
                <asp:Button ID="Button4" runat="server" Text="末页" CommandName="Page" CommandArgument="Last" />
            </PagerTemplate>
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:mydbConnectionString %>"
            SelectCommand="SELECT * FROM [ALTable]"
            ProviderName="<%$ ConnectionStrings:mydbConnectionString.ProviderName %>">
        </asp:SqlDataSource>
    </asp:Panel>
    <asp:Panel ID="EditPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="doInsertButton" runat="server" Text="确认添加" OnClick="Article_Insert" />
            <asp:Button ID="doUpdateButton" runat="server" Text="保存文章" CommandName="doUpdate"
                OnCommand="Article_Command" />
            <asp:Button ID="Button7" runat="server" Text="返回列表" OnClick="Back_List" />
        </div>
        <asp:Table ID="editTable" runat="server" BorderColor="#999999" BorderWidth="0px"
            Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" width="10%">标题</asp:TableCell>
                <asp:TableCell runat="server" Width="90%">
                    <asp:TextBox ID="titleTextBox" runat="server" Width="98%"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:TextBox runat="server" ID="contentTextBox" TextMode="MultiLine">
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <script type="text/javascript">
            if (typeof CKEDITOR != 'undefined') {
                var editor = CKEDITOR.replace('<%=contentTextBox.ClientID %>');
                CKFinder.setupCKEditor(editor, 'ckfinder');
            }
        </script>
    </asp:Panel>
    <asp:Panel ID="ViewPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="viewDoEditButton" runat="server" Text="编辑文章" CommandName="doEdit"
                OnCommand="Article_Command" />
            <asp:Button ID="Button8" runat="server" Text="返回列表" OnClick="Back_List" />
        </div>
        <div class="article_body">
            <asp:Label ID="ArticleViewTitle" runat="server" Text="" CssClass="article_title"></asp:Label>
            <asp:Label ID="ArticleViewInfo" runat="server" Text="" CssClass="article_info"></asp:Label>
            <div id="article_content">
                <asp:Label ID="ArticleView" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
