<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" 
    CodeFile="TechDetails.aspx.cs" Inherits="TechDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BorderContentPlaceHolder" Runat="Server">
    <img height="195" alt="富藤科技技术支持" src="images/b/tech.jpg" width="965" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftContentPlaceHolder" Runat="Server">
     <div class="stitle">
        <div class="stitle1">技术支持</div>
    </div>
    <div class="defaltContainer">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 700px;">
            <tr>
                <td style="text-align: left">
                    <br />
                    <h2>
                        <asp:Label ID="ArticleTitleLabel" runat="server" Text="标题" CssClass="newsTitle"></asp:Label>
                    </h2>
                    <asp:Label ID="ArticleTimeLabel" runat="server" Text="时间"></asp:Label>
                    <p>&nbsp;</p>
                </td>
                
            </tr>
            <tr>
                <td class="newsContent" >
                    <%=strArticleContents%>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;padding-top:20px;">
                    <p style="padding-right: 20px;">浏览次数:<asp:Label ID="ArticleClickTimesLabel" runat="server"
                        Text="浏览次数"></asp:Label>&nbsp;&nbsp;&nbsp;<a href="Tech.aspx">返回列表</a></p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

