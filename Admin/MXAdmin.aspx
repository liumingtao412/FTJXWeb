 <%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" 
     CodeFile="MXAdmin.aspx.cs" Inherits="Admin_MXAdmin" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" Runat="Server">
     <asp:Panel ID="NewsListPanel" runat="server">
        <div class="savePage">
            <asp:Button ID="Button1" runat="server" Text="添加明星产品" OnClick="Article_New" /></div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="20" AllowSorting="True"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
            BorderWidth="1px" CellPadding="4" DataKeyNames="CPID" DataSourceID="SqlDataSource1"
            EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" Width="100%"
            OnDataBound="GridView1_DataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button6" runat="server" Text="编辑" CommandName="doEdit" CommandArgument='<%#Eval("CPID") %>'
                            OnCommand="Article_Command" />
                        <asp:Button ID="Button1" runat="server" Text="删除" OnClientClick="return confirm( '确定删除该记录? ');"
                            CommandName="doDelete" CommandArgument='<%#Eval("CPID")%>' OnCommand="Article_Command" /></ItemTemplate>
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="明星产品名称">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="doEdit" CommandArgument='<%#Eval("CPID") %>'
                            OnCommand="Article_Command">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remarks" HeaderText="描述" />
                 <asp:BoundField DataField="AltValue" HeaderText="ALT" />
                 <asp:TemplateField HeaderText="链接URL">
                    <ItemTemplate>
                        <a href='<%#Eval("LinkURL")%>' target="_blank">
                            <asp:Label  runat="server" Text='<%#Eval("LinkURL")%>'></asp:Label>
                            </a>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:BoundField DataField="PosIndex" HeaderText="显示顺序" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <PagerTemplate>
                <asp:Button ID="Button5" runat="server" Text="添加明星产品" OnClick="Article_New" />
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
            SelectCommand="SELECT * FROM [ProductsShowTable] WHERE IsDel=False ORDER BY PosIndex DESC"
            ProviderName="<%$ ConnectionStrings:mydbConnectionString.ProviderName %>">
        </asp:SqlDataSource>
    </asp:Panel>
        <asp:Panel ID="EditPanel" runat="server" Visible="false">
        <div class="savePage">
            <asp:Button ID="doInsertButton" runat="server" Text="确认添加" OnClick="Article_Insert" />
            <asp:Button ID="doUpdateButton" runat="server" Text="保存" CommandName="doUpdate"
                OnCommand="Article_Command" />
            <asp:Button ID="Button7" runat="server" Text="返回列表" OnClick="Back_List" />
        </div>
        
<asp:Table ID="editTable" runat="server" BorderColor="#999999" BorderWidth="0px"
            Width="100%">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" Width="100px">明星产品名称</asp:TableCell>
                <asp:TableCell runat="server" ColumnSpan="2">
                    <asp:TextBox ID="productNameTextBox" runat="server" Width="98%"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
    <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" Width="100px">图片URL</asp:TableCell>
                <asp:TableCell runat="server" Width="500px">
                    <asp:TextBox ID="picURLTextBox" runat="server" Width="480px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <input type="button" ID="Btn_changeStarProductPic" value="更换图片(分辨率171*116)" onclick="BrowseServer('<%=picURLTextBox.ClientID %>')" /></asp:TableCell>
            </asp:TableRow>
         
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True" Width="100px">预览</asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Image ID="Image_preview" runat="server" />
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:Button runat="server" ID="Btn_RefreshPreview" Text ="刷新预览图片" OnClick="Btn_RefreshPreview_Click" />
                </asp:TableCell>
               
            </asp:TableRow>
            <asp:TableRow  runat="server">
                <asp:TableCell  runat="server" Font-Bold="True" Width="100px">简介</asp:TableCell>
                <asp:TableCell  runat="server" ColumnSpan="2">
                    <asp:TextBox runat="server" ID="remarksTextBox" TextMode="MultiLine" Width="99%" Height="50px">
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow  runat="server">
                <asp:TableCell  runat="server" Font-Bold="True" Width="100px">ALT</asp:TableCell>
                <asp:TableCell  runat="server" ColumnSpan="2">
                    <asp:TextBox runat="server" ID="altTextBox" Width="99%" >
                    </asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                   <asp:TableCell runat="server" Font-Bold="True" Width="100px">链接URL</asp:TableCell>
                <asp:TableCell runat="server"  ColumnSpan="2">
                    <asp:TextBox Width="90%" ID="linkURLTextBox" runat="server" ></asp:TextBox></asp:TableCell>
            </asp:TableRow>
     <asp:TableRow>
                   <asp:TableCell runat="server" Font-Bold="True" Width="100px">显示排序</asp:TableCell>
                <asp:TableCell runat="server"  ColumnSpan="2">
                    <asp:TextBox ID="posIndexTextBox" runat="server" ></asp:TextBox>(越大越优先)</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    
</asp:Content>

