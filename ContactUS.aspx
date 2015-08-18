<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="ContactUS.aspx.cs" Inherits="ContactUS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1">
            <asp:Label ID="stitle" runat="server"></asp:Label></div>
    </div>
    <div class="defaltContainer">
        <asp:Label ID="ContectInfLabel" runat="server"></asp:Label>
        <asp:Image ID="EMapImage" runat="server" Visible="false" />
        <asp:Panel ID="MessagePanel" runat="server" Width="125px">
            <table cellpadding="0" cellspacing="0" border="0" style="width: 680px; vertical-align: middle;"
                runat="server">
                <tr>
                    <td colspan="2" style="padding: 12px 16px;">
                        <h3>您如果对我们有任何的意见或建议，请在此告诉我们，谢谢！</h3>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" 
                        style="text-align: right; padding: 0px; height: 30px; width: 82px;">留言标题：
                    </td>
                    <td>
                        <asp:TextBox ID="TitleTextBox" runat="server" Width="464px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" 
                        style="text-align: right; padding: 0px; height: 30px; width: 82px;">您的姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Width="252px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ControlToValidate="NameTextBox"
                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">* 请输入您的姓名</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" 
                        style="text-align: right; padding: 0px; height: 30px; width: 82px;">您的Email：
                    </td>
                    <td>
                        <asp:TextBox ID="EmailTextBox" runat="server" Width="252px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailTextBox"
                            Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">* 请输入您的Email地址</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                            ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                            SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">无效的Email地址</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" 
                        style="text-align: right; padding: 0px; height: 30px; width: 82px;">你的电话：
                    </td>
                    <td>
                        <asp:TextBox ID="TelTextBox" runat="server" Width="252px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" 
                        style="text-align: right; padding: 0px; height: 230px; width: 82px;">留言内容：</td>
                    <td>
                        <asp:TextBox ID="ContentsTextBox" runat="server" TextMode="MultiLine" Height="230px"
                            Width="559px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td  colspan="2" style=" height: 41px;">
                        <div>
                        请输入图片验证码（点击图片可刷新）：</div>
                   <div>
                          <asp:Image runat="server" ID="valiCodeImage" style="cursor: pointer;"  alt="验证码" ImageUrl="~/ValidateImage.aspx"  OnClick="this.src = '/ValidateImage.aspx?time='+ (new Date()).getTime()"/>
                       <asp:TextBox runat="server" ID="UserCodeTextbox" MaxLength="5" />
                       <asp:Label ID="ValicodeWrongLabel" runat="server" Text="* 验证码错误，请重新输入" ForeColor="Red" Visible="false" />
                        <asp:RequiredFieldValidator ID="valiCodeRequiredFieldValidator" runat="server" ControlToValidate="UserCodeTextbox"
                            Display="Dynamic" ErrorMessage="* 请输入验证码" SetFocusOnError="True" />
                       <%--<asp:CompareValidator ID="valiCodeCompareValidator" runat="server"  Display="Dynamic"  Operator="Equal" ErrorMessage="验证码错误！" ControlToValidate="UserCodeTextbox" ValueToCompare='<%#Session["ValidateCode"]==null?"":Session["ValidateCode"].ToString()%>' Type="String" SetFocusOnError="True" />--%>

                       </div>
                    </td>
                </tr>
                

                <tr>
                    <td colspan="2" style="text-align: center; height: 41px;">
                        <hr />
                        <asp:Button ID="OKButton" runat="server" Text="提交留言" OnClick="OKButton_Click" />
                        &nbsp;&nbsp; </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
    <asp:Image Height="195" Width="960" ImageUrl="images/b/contactus.jpg" runat="server"
        ID="borderImg" />
</asp:Content>
