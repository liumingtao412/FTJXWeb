<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="YHAdmin.aspx.cs" Inherits="Admin_YH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        DataSourceID="SqlDataSource1" Width="100%" 
    EnableModelValidation="True" PageSize="20"
        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
        CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound" DataKeyNames="编号">
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerSettings Mode="NextPreviousFirstLast" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <PagerTemplate>
            <asp:Button ID="Button5" runat="server" Text="添加用户" OnClick="Button5_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server">第<%#this.GridView1.PageIndex+1%>页 / 共<%#this.GridView1.PageCount%>页</asp:Label>
            <asp:Button ID="Button3" runat="server" Text="首页" CommandName="Page" CommandArgument="First" />
            <asp:Button ID="Button2" runat="server" Text="上一页" CommandName="Page" CommandArgument="Prev" />
            <asp:Button ID="Button1" runat="server" Text="下一页" CommandName="Page" CommandArgument="Next" />
            <asp:Button ID="Button4" runat="server" Text="末页" CommandName="Page" CommandArgument="Last" />
        </PagerTemplate>
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                <ItemTemplate>
                    <asp:Button ID="ModifyButton" runat="server" Text="修改" CommandName="ShowModifyPage" CommandArgument='<%#Container.DataItemIndex%>' oncommand="ModifyButton_Command" Visible='<%#Eval("YHName").ToString()!=Session["userName"].ToString()%>'/>
                    <asp:Button ID="Button6" runat="server" Text="删除" OnClientClick="return confirm( '确定删除该用户? '); "
                        CommandName="Delete" Visible='<%#Eval("YHName").ToString()!=Session["userName"].ToString()%>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="YHName" HeaderText="帐号" SortExpression="userName"></asp:BoundField>
            <asp:BoundField DataField="YHAuthority" HeaderText="权限" SortExpression="userAuthority">
            </asp:BoundField>
            <asp:BoundField DataField="lastlogintime" HeaderText="最后一次登录时间" SortExpression="lastLoginTime"
                InsertVisible="False" ReadOnly="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:mydbConnectionString %>"
        ProviderName="<%$ ConnectionStrings:mydbConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [YHTable]" 
        DeleteCommand="DELETE FROM [YHTable] WHERE [编号] = ?" 
        >
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
       
    </asp:SqlDataSource>
    <asp:Table ID="InsertTable" runat="server" border="0" BorderStyle="None" BorderWidth="0px"
        Visible="false">
        <asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="3">添加用户</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>帐号</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="UserNameTextBoxLabel" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>密码</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell>
                    <asp:Label ID="PasswordTextBoxLabel" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>权限</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="AuthorityTextBox" runat="server" Text="1"></asp:TextBox></asp:TableCell><asp:TableCell>
                    <asp:Label ID="AuthorityTextBoxLabel" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="2">
                <asp:Button ID="DoInsertButton" runat="server" Text="确认添加" OnClick="DoInsertButton_Click" /><asp:Button
                    ID="ClearInsertTable" runat="server" Text="返回" OnClick="ClearInsertTable_Click" /></asp:TableHeaderCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
    </asp:Table>

     <asp:Table ID="ModifyTable" runat="server" border="0" BorderStyle="None" 
        BorderWidth="0px" Visible="false" >
        <asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="3">修改用户</asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>帐号</asp:TableCell><asp:TableCell>
                <asp:Label ID="mUserNameLabel" runat="server" Text="Label"></asp:Label>
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>密码(不修改请留空)</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="mPasswordTextBox" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell>
                    <asp:Label ID="mPasswordTextBoxLabel" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>权限</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="mAuthorityTextBox" runat="server" Text="1"></asp:TextBox></asp:TableCell><asp:TableCell>
                    <asp:Label ID="mAuthorityTextBoxLabel" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell ColumnSpan="2">
                <asp:Button ID="DoModifyButton" runat="server" Text="确认修改" OnClick="DoModifyButton_Click" /><asp:Button
                    ID="ClearModifyTable" runat="server" Text="返回" OnClick="ClearModifyTable_Click" /></asp:TableHeaderCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
