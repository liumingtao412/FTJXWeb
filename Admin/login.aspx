<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeFile="login.aspx.cs" Inherits="admin_Default2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>后台登录</title>
<style type="text/css">
a {text-decoration: none}
body {text-align:center;background-color: #EFF9FE;}
.f1 { font-size: 8pt; font-family: Verdana; line-height: 14pt ;  color:#5F6468}
.f2 { font-size: 9pt; font-family: Verdana; line-height: 14pt; color:#5A7E98}
.input-border{border:1px solid #b8b8b8;	width:168px; line-height:20px; font-size:14px; height:22px;}
.con {width: 800px; height:450px; margin-right: auto; margin-left: auto; margin-top: 10px;}
.login {background-image: url('images/a01.jpg'); background-repeat: no-repeat; float: left;	height: 450px; width: 800px}
.login .b_left {float: left;height: 450px;width: 400px}
.login .b_cernet {float: left;height: 450px;width: 336px;}
.login .b_right {float: left;height: 450px;	width: 64px;}
.login-b, .login-b2, .login-b3{background-image: url('images/a04.gif');height: 26px;width: 77px;border:0 none;cursor:move !important}
.login-b2{background-image: url('images/a04.gif');	background-position: 0px -27px}
.login-b3{background-image: url('images/a04.gif'); background-position: 0px -54px}
.login-a, .login-a2, .login-a3{background-image: url('images/a05.gif');height: 26px;width: 77px;border:0 none;cursor:move !important}
.login-a2{background-image: url('images/a05.gif');	background-position: 0px -27px}
.login-a3{background-image: url('images/a05.gif'); background-position: 0px -54px}
</style>
</head>
<body>
<div class="con">
<div class="login">
<div class="b_left">
<asp:Label ID="wantmd5" runat="server" Text="Label" Visible="false"></asp:Label>
</div>
<div class="b_cernet" style="width: 325px; height: 450px">
<form runat="server">
<table border="0" width="100%" cellspacing="1" cellpadding="0" height="100%" class="f1">
	<tbody><tr>
		<td height="75" colspan="2">　</td>
	</tr>
	<tr>
		<td height="17" colspan="2"></td>
	</tr>
	<tr>
		<td height="66" colspan="2">
		<p align="center" style="font-size:20px;"><asp:Label ID="Please_Login" runat="server" Text="后台管理登录页面"></asp:Label><br />
		</p></td>
	</tr>
	<tr>
		<td height="20" colspan="2">　</td>
	</tr>
	<tr>
		<td height="34" width="35%" align="right">帐号 Account </td>
		<td height="34" width="64%">&nbsp;<asp:TextBox ID="Txt_UserName" runat="server" MaxLength="26" CssClass="input-border"></asp:TextBox></td>
	</tr>
	<tr>
		<td height="32" width="35%" align="right">密码 Password </td>
		<td height="32" width="64%">&nbsp;<asp:TextBox ID="Txt_Password" runat="server" TextMode="Password" MaxLength="16" CssClass="input-border"></asp:TextBox></td>
	</tr>
	<tr>
		<td height="57" width="35%">　</td>
		<td height="57" width="64%">&nbsp;<asp:Button ID="Btn_Login" runat="server" Text="" onclick="Btn_Login_Click" CssClass="login-b"  onMouseOver="this.className='login-b2'" onMouseDown="this.className='login-b3'" onMouseOut="this.className='login-b'" />&nbsp;&nbsp;
        <input type="reset" class="login-a" value="" onMouseOver="this.className='login-a2'" onMouseDown="this.className='login-a3'" onMouseOut="this.className='login-a'" /></td>
	</tr>
	<tr>
		<td height="45" colspan="2">
		
		<font color="#5A7E98">
		&nbsp;<span lang="zh-cn">
		<a href="#"><font color="#5A7E98">Any problems，Please contact Liu Mingtao</font></a></span></font></td>
	</tr>
	<tr>
		<td height="50" colspan="2">　</td>
	</tr>
	<tr>
		<td valign="top" colspan="2">
		　</td>
	</tr>
</tbody></table>
</form>

</div>
<div class="b_right" style="width: 69px; height: 450px">
</div>
</div>
</div>
</body>

</html>