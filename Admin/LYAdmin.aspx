<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="LYAdmin.aspx.cs" Inherits="Admin_LY" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:Panel ID="NewsListPanel" runat="server">
        <div class="savePage">
            <asp:Button ID="Button1" runat="server" Text="标记全部已读" OnClick="MarkAllReaded" /></div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="20" AllowSorting="True"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
            BorderWidth="1px" CellPadding="4" DataKeyNames="编号" DataSourceID="SqlDataSource1"
            EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" Width="100%"
            OnDataBound="GridView1_DataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" Text="删除" OnClientClick="return confirm( '确定删除该记录? ');"
                            CommandName="doDelete" CommandArgument='<%#Eval("编号")%>' OnCommand="Article_Command" /></ItemTemplate>
                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="doView" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command">
                            <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToBoolean(Eval("LYRead")) ? Eval("LYTitle") : "<font color=\"red\">[未读]</font>" + Eval("LYTitle")%>'></asp:Label></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="时间">
                    <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%#Eval("LYTime","{0:yyyy-MM-dd}")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                </asp:TemplateField>
                
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <PagerTemplate>
                <asp:Button ID="Button5" runat="server" Text="标记全部已读" OnClick="MarkAllReaded" />
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
            SelectCommand="SELECT * FROM [LYTable] where LYDelete<>true ORDER BY LYRead DESC, LYTime DESC"
            ProviderName="<%$ ConnectionStrings:mydbConnectionString.ProviderName %>">
        </asp:SqlDataSource>
    </asp:Panel>
    <asp:Panel ID="ViewPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="viewDoEditButton" runat="server" Text="编辑文章" CommandName="doEdit"
                OnCommand="Article_Command" Visible="false" />
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
