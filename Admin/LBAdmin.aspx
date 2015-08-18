<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="LBAdmin.aspx.cs" Inherits="Admin_LB" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:Panel ID="NewsListPanel" runat="server">
        <div class="savePage">
            <asp:Button ID="Button1" runat="server" Text="添加类别" OnClick="Article_New" /></div>
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
                <asp:BoundField DataField="编号" HeaderText="编号" />
                <asp:TemplateField HeaderText="标题">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="doEdit" CommandArgument='<%#Eval("编号") %>'
                            OnCommand="Article_Command">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("LBName")%>'></asp:Label></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="LBHasChild" HeaderText="是否有子级" />
                <asp:BoundField DataField="LBParentID" HeaderText="父级ID" />
                <asp:BoundField DataField="PosIndex" HeaderText="同级别显示顺序" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <PagerTemplate>
                <asp:Button ID="Button5" runat="server" Text="添加类别" OnClick="Article_New" />
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
            SelectCommand="SELECT * FROM [LBTable] WHERE LBDel=False ORDER BY LBParentID,PosIndex DESC"
            ProviderName="<%$ ConnectionStrings:mydbConnectionString.ProviderName %>">
        </asp:SqlDataSource>
    </asp:Panel>
    <asp:Panel ID="EditPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="doInsertButton" runat="server" Text="确认添加" OnClick="Article_Insert" />
            <asp:Button ID="doUpdateButton" runat="server" Text="保存修改" CommandName="doUpdate"
                OnCommand="Article_Command" />
            <asp:Button ID="Button7" runat="server" Text="返回列表" OnClick="Back_List" />
        </div>
        <asp:Table ID="editTable" runat="server" BorderColor="#999999" BorderWidth="0px"
            Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" width="10%">标题</asp:TableCell>
                <asp:TableCell runat="server" Width="90%" ColumnSpan="2">
                    <asp:TextBox ID="titleTextBox" runat="server" Width="90%"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" Font-Bold="True" Width="10%">父级</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" Width="40%">
                    <asp:DropDownList ID="parentIDDropDown" runat="server"></asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server">
                    <asp:CheckBox ID="hasChildCheckBox" runat="server" text="存在子级"/></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                   <asp:TableCell runat="server" Font-Bold="True" Width="10%">显示排序</asp:TableCell>
                <asp:TableCell runat="server"  ColumnSpan="2" Width="10%">
                    <asp:TextBox ID="IndexTextBox" runat="server" ></asp:TextBox>(越大越优先)</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell ID="TableCell5" runat="server" Font-Bold="True">类别说明</asp:TableCell>
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:TextBox runat="server" ID="contentTextBox" TextMode="MultiLine" Width="99%" Height="100px">
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <!--<script type="text/javascript">
            if (typeof CKEDITOR != 'undefined') {
                var editor = CKEDITOR.replace('<%=contentTextBox.ClientID %>');
                CKFinder.setupCKEditor(editor, 'ckfinder');
            }
        </script>-->
    </asp:Panel>
</asp:Content>
