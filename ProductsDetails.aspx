<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="ProductsDetails.aspx.cs" Inherits="ProductsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1"><a href="Products.aspx">产品信息</a></div>
    </div>
    <div class="defaltContainer">
        <h3>产品名称：<asp:Label ID="CPNameLabel" runat="server" Text="产品名称"></asp:Label></h3>
        <h3>产品类别：<asp:Label ID="CPLBLabel" runat="server" Text="产品类别"></asp:Label></h3>
        <table>
            <tr>
                <td>
                    <div class="ProductDetail">
                        <asp:Label ID="CPDetailsLabel" runat="server" Text="产品信息"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="margin-top: 20px; padding: 5px; text-align: center">
                        <asp:Button Height="40px" Width="120px" runat="server" Text="留言咨询此产品" ID="AskForButton" OnClick="AskForButton_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
    <img alt="富藤科技产品信息" height="195" src="images/b/products.jpg" width="965" />
</asp:Content>
