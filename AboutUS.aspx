<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="AboutUS.aspx.cs" Inherits="AboutUS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1"><asp:Label ID="stitle" runat="server"></asp:Label></div>
    </div>
    <div class="defaltContainer">

    <asp:Label ID="AboutUsInfLabel" runat="server" Text="公司简介"></asp:Label>
    <asp:Label ID="JoinUsLabel" runat="server" Text="招聘信息" Visible="false"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
   <%-- <img height="195" src="images/b/aboutus.jpg" width="960">--%>
    <asp:Image runat="server" Height="195" Width="960" ID="bannerImage" />
</asp:Content>

