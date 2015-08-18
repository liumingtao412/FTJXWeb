<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="CPAdmin.aspx.cs" Inherits="Admin_CP" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:Panel ID="NewsListPanel" runat="server">
        <div class="savePage">
            <asp:Button ID="Button1" runat="server" Text="添加产品" OnClick="Article_New" /></div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
            BorderWidth="1px" CellPadding="4" DataKeyNames="编号" DataSourceID="SqlDataSource1"
            EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" Width="100%"
            OnDataBound="GridView1_DataBound" PageSize="20">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button6" runat="server" Text="编辑" CommandName="doEdit" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command" />
                        <asp:Button ID="Button1" runat="server" Text="删除" OnClientClick="return confirm( '确定删除该记录? ');"
                            CommandName="doDelete" CommandArgument='<%#Eval("编号")%>' OnCommand="Article_Command" /></ItemTemplate>
                    <HeaderStyle Width="16%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                    <%--    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="doView" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command">--%>
                        <a href='/ProductsDetails.aspx?CPID=<%#Eval("编号") %>' target="_blank">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("CPName")%>'></asp:Label>
                        </a>
                       <%-- </asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LBName" HeaderText="所属类别" />
                
               <%-- <asp:CheckBoxField DataField="CPInMainPage" HeaderText="首页显示">
                <HeaderStyle Width="12%" />
                </asp:CheckBoxField>--%>
                
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <PagerTemplate>
                <asp:Button ID="Button5" runat="server" Text="添加产品" OnClick="Article_New" />
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
            SelectCommand="SELECT CPTable.编号 AS 编号,CPTable.CPName AS CPName,LBTable.LBName AS LBName FROM [CPTable] LEFT JOIN [LBTable] ON CPTable.CPLBID=LBTable.编号 Where CPTable.CPDel<>true ORDER BY CPTable.编号 DESC"
            ProviderName="<%$ ConnectionStrings:mydbConnectionString.ProviderName %>">
        </asp:SqlDataSource>
    </asp:Panel>
    <asp:Panel ID="EditPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="doInsertButton" runat="server" Text="确认添加" OnClick="Article_Insert" />
            <asp:Button ID="doUpdateButton" runat="server" Text="保存产品信息" CommandName="doUpdate"
                OnCommand="Article_Command" />
            <asp:Button ID="Button7" runat="server" Text="返回列表" OnClick="Back_List" />
        </div>
        <asp:Table ID="editTable" runat="server" BorderColor="#999999" BorderWidth="0px"
            Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" Width="100px">名称</asp:TableCell>
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:TextBox ID="titleTextBox" runat="server" Width="98%"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" Width="100px">图片</asp:TableCell>
                <asp:TableCell runat="server" Width="400px">
                    <asp:TextBox ID="Txt_newsPicURL" runat="server" Width="380px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <input type="button" ID="Btn_changeNewsPic" value="更换图片(80*60)" onclick="BrowseServer('<%=Txt_newsPicURL.ClientID %>')" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" Font-Bold="True" Width="100px">分类</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" Width="400px">
                    <asp:DropDownList ID="parentIDDropDown" runat="server"></asp:DropDownList>
                </asp:TableCell>
               <%-- <asp:TableCell ID="TableCell3" runat="server">
                    <asp:CheckBox ID="inMainPageCheckBox" runat="server" Text="首页展示"></asp:CheckBox></asp:TableCell>--%>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell5" runat="server" Font-Bold="True" Width="100px">简介</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" ColumnSpan="2">
                    <asp:TextBox runat="server" ID="infTextBox" TextMode="MultiLine" Width="99%" Height="50px">
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" ColumnSpan="3">
                    <asp:TextBox runat="server" ID="contentTextBox" TextMode="MultiLine" class="ckeditor">
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <script type="text/javascript">
            if (typeof CKEDITOR != 'undefined') {
                var editor = CKEDITOR.replace('<%=contentTextBox.ClientID %>',{height:'500px'});
                CKFinder.setupCKEditor(editor, 'ckfinder');
            }
        </script>
    </asp:Panel>
    <asp:Panel ID="ViewPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="viewDoEditButton" runat="server" Text="编辑产品信息" CommandName="doEdit"
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
