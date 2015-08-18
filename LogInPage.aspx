<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogInPage.aspx.cs" Inherits="LogInPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>藤仓气缸 | 日本藤仓气缸 | 低摩擦气缸 - 上海富藤机械科技有限公司</title>
    <link rel="stylesheet" type="text/css" href="DesignHQ/css/main.css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
    <div style=" margin-top:60px;text-align:center; font-size:28px; font-weight:bolder;">
    <a href="Default.aspx">上海富藤机械科技有限公司</a> 管理后台登陆
    </div>
    <div id="login">
    <table>
        <tr>
        <td>用户名：</td>
        <td style="width: 172px">
            <asp:TextBox ID="AdminNameTextBox" runat="server" Width="160px"></asp:TextBox></td>
        </tr>
        <tr>
        <td>密码：</td>
        <td style="width: 172px">
            <asp:TextBox ID="PasswordTextBox" Width="160px" TextMode="Password" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
        <td colspan="2">
            <asp:Button ID="OKButton" runat="server" Text="登 陆" OnClick="OKButton_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="退 出" OnClick="CancelButton_Click" />
        </td>
        </tr>
    
    </table>
    
    </div>
    
    </div>
    </form>
</body>
</html>
