<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true" CodeFile="XXAdmin.aspx.cs" Inherits="Admin_XX" validateRequest= "false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentPlaceHolder" Runat="Server">
    <div class="savePage"><asp:Button ID="Button1" runat="server" Text="保存" 
            onclick="saveButton_Click" /> &nbsp; <asp:Button ID="Button2" 
            runat="server" Text="重新设置" onclick="reloadButton_Click" /></div>


    <div class="split">公司简介</div>
    <asp:TextBox ID="GSJJTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <script type="text/javascript">
     if(typeof CKEDITOR != 'undefined') {
         var editor = CKEDITOR.replace('<%=GSJJTextBox.ClientID %>');
        CKFinder.setupCKEditor(editor, 'ckfinder');
    }
    </script>
    <div class="split">联系方式</div>
    <asp:TextBox ID="LXFSTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <script type="text/javascript">
        if (typeof CKEDITOR != 'undefined') {
            var editor2 = CKEDITOR.replace('<%=LXFSTextBox.ClientID %>');
            CKFinder.setupCKEditor(editor2, 'ckfinder');
        }
    </script>
    <div class="split">招聘信息</div>
    <asp:TextBox ID="ZPXXTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <script type="text/javascript">
        if (typeof CKEDITOR != 'undefined') {
            var editor2 = CKEDITOR.replace('<%=ZPXXTextBox.ClientID %>');
            CKFinder.setupCKEditor(editor2, 'ckfinder');
        }
    </script>
    <div class="split">公司详细介绍</div>
    <asp:TextBox ID="GSXJTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <script type="text/javascript">
        if (typeof CKEDITOR != 'undefined') {
            var editor2 = CKEDITOR.replace('<%=GSXJTextBox.ClientID %>');
            CKFinder.setupCKEditor(editor2, 'ckfinder');
        }
    </script>
   <%-- <div class="split">页面左侧联系方式</div>
    <asp:TextBox ID="ZCLXFSTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <script type="text/javascript">
        if (typeof CKEDITOR != 'undefined') {
            var editor2 = CKEDITOR.replace('<%=ZCLXFSTextBox.ClientID %>');
            CKFinder.setupCKEditor(editor2, 'ckfinder');
        }
    </script>--%>
    <div class="split">其它</div>
    <asp:Label ID="Label1" runat="server" Text="电子邮件："></asp:Label><asp:TextBox ID="GSEMAILTextBox" runat="server" TextMode="SingleLine" Width="500px"></asp:TextBox>    
    <br />
    <asp:CheckBox ID="isShowBannerCheckBox" runat="server" text="显示Banner图片"/>
      <br />
    <asp:CheckBox ID="IsScrollStarProductsCheckBox" runat="server" text="滚动显示明星产品(若不滚动，则固定显示前四个明星产品)"/>
      <br />
    <asp:Label ID="Label2" runat="server" Text="滚动速度："></asp:Label><asp:TextBox ID="ScrollSpeedTextBox" runat="server" TextMode="SingleLine" Width="500px"></asp:TextBox>    
      <br />
    <asp:Label ID="Label3" runat="server" Text="滚动明星产品个数："></asp:Label><asp:TextBox ID="ScrollPicNumTextBox" runat="server" TextMode="SingleLine" Width="500px"></asp:TextBox>    

    
    <div class="savePage"><asp:Button ID="saveButton" runat="server" Text="保存" 
            onclick="saveButton_Click" /> &nbsp; <asp:Button ID="reloadButton" 
            runat="server" Text="重新设置" onclick="reloadButton_Click" /></div>
</asp:Content>

