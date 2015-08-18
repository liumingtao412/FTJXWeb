<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" runat="Server">
    <p>
        您好，<span class="font_hl"><%=Session["userName"]%></span>!欢迎登录管理后台。</p>
    <p>
        您上次上次登录时间： <span class="font_hl"><%=Session["lastLoginTime"]%></span></p>
    <div class="split">
        修改密码</div>
    <table border="0" style="margin:12px 24px 0;">
        <tr>
            <td align="right">
                旧密码：
            </td>
            <td>
                <asp:TextBox ID="OldPsd_TextBox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                新密码：
            </td>
            <td>
                <asp:TextBox ID="NewPsd_TextBox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                确认密码：
            </td>
            <td>
                <asp:TextBox ID="NewPsdRe_TextBox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                    <center><asp:Button ID="ModifyMyPassword" runat="server" Text="修改密码" 
                            style="padding:2px 6px;margin:4px;" onclick="ModifyMyPassword_Click"/></center>
            </td>
        </tr>
    </table>

</asp:Content>
