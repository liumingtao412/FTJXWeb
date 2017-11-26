<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="NewsDetails.aspx.cs" Inherits="NewsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1"><a href="News.aspx">公司新闻</a></div>
    </div>
    <div class="defaltContainer">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 700px;">
            <tr>
                <td style="text-align: left">
                    <br />
                    <h2>
                        <asp:Label ID="NewsTitleLabel" runat="server" Text="新闻标题" CssClass="newsTitle"></asp:Label>
                    </h2>                    
                    <asp:Label ID="NewsTimeLabel" runat="server" Text="新闻时间"></asp:Label>
                    <p>&nbsp;</p>
                </td>
                
            </tr>
            <tr>
                <td class="newsContent" >
                    <small>
                    <%=strNewsContents%>
                        </small>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;padding-top:20px;">
                    <p style="padding-right: 20px;">浏览次数:<asp:Label ID="NewsClickTimesLabel" runat="server"
                        Text="浏览次数"></asp:Label>&nbsp;&nbsp;&nbsp;<a href="News.aspx">返回列表</a></p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
    <img height="195" alt="富藤科技公司新闻" src="images/b/news.jpg" width="965" />
</asp:Content>
