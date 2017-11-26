<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="XWAdmin.aspx.cs" Inherits="Admin_XW" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:Panel ID="NewsListPanel" runat="server">
        <div class="savePage">
            <asp:Button ID="Button1" runat="server" Text="添加文章" OnClick="Article_New" /></div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
            BorderWidth="1px" CellPadding="4" DataKeyNames="编号" DataSourceID="SqlDataSource1"
            EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" Width="100%"
            OnDataBound="GridView1_DataBound" PageSize="15">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button6" runat="server" Text="编辑" CommandName="doEdit" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command" />
                        <asp:Button ID="Button1" runat="server" Text="删除" OnClientClick="return confirm( '确定删除该记录? ');"
                            CommandName="doDelete" CommandArgument='<%#Eval("编号")%>' OnCommand="Article_Command" />
                        <asp:Button ID="Button_publish" runat="server" Text='<%# Convert.ToBoolean(Eval("isPublish"))?"撤销":"发布"%>' CommandName="doPublish" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command" />
                    </ItemTemplate>
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题">
                    <ItemTemplate>
                        <a href="/NewsDetails.aspx?NewsID=<%#Eval("编号") %>" target="_blank">
                            <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToBoolean(Eval("IsTop")) ? "<font color=\"red\">[置顶]</font>" + Eval("newsTitle") : Eval("newsTitle") %>'>

                            </asp:Label>

                        </a>
                       <%--  <asp:LinkButton ID="LinkButton1" runat="server" CommandName="doView" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command">
                            <asp:Label ID="Label2" runat="server" Text='<%#Convert.ToBoolean(Eval("IsTop")) ? "<font color=\"red\">[置顶]</font>" + Eval("newsTitle") : Eval("newsTitle") %>'>

                            </asp:Label>

                        </asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="newsClickTimes" HeaderText="点击量" ReadOnly="True" SortExpression="clicks">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="newsTime" HeaderText="添加时间" ReadOnly="True" SortExpression="addTime">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
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
            SelectCommand="SELECT * FROM [XWTable] Where newsDelete=False ORDER BY isTop,newsTime DESC"
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
                <asp:TableCell runat="server" Font-Bold="True" Width="10%">标题</asp:TableCell>
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:TextBox ID="titleTextBox" runat="server" Width="90%"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
               <asp:TableCell   runat="server">
                    <asp:CheckBox ID="isTopCheckBox" runat="server" text="是否置顶"/></asp:TableCell>
            </asp:TableRow>
        <%--     <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" Width="100px">新闻图片</asp:TableCell>
                <asp:TableCell runat="server" Width="400px">
                    <asp:TextBox ID="TextBox1" runat="server" Width="380px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <input type="button" ID="Btn_changeNewsPic" value="更换图片" onclick="BrowseServer('<%=Txt_newsPicURL.ClientID %>    ')" /></asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" ColumnSpan="3">
                    <asp:TextBox runat="server" ID="contentTextBox" TextMode="MultiLine">
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <script type="text/javascript">
            if (typeof CKEDITOR != 'undefined') {
                var editor = CKEDITOR.replace('<%=contentTextBox.ClientID %>', {height:'500px'});
                CKFinder.setupCKEditor(editor, 'ckfinder');
            }
            $(function () {
                CKEDITOR.replace('txtEditMsg', { height: '900px', width: '552px' });
            });
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
                <asp:Image ID="ArticlePic" runat="server" Width="300px"
                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" /><asp:Label ID="ArticleView" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
