<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1"><a href="Products.aspx">产品信息</a><%=string.IsNullOrEmpty(strLBName)?"":" - " + strLBName %></div>
    </div>

    <div class="defaltContainer">
        <asp:Panel ID="SearchPanel" runat="server">       
        <div class="LBInfLabel">
            您可在此搜索产品：<asp:TextBox runat="server" ID="SearchTextbox" CssClass="searchTextbox" MaxLength="30" /><asp:Button ID="searchButton" runat="server" Text="搜索" OnClick="searchButton_Click" />
        </div>           
        </asp:Panel>

          <asp:Panel ID="ProductTreePanel" runat="server" >
               <div  class="LBInfLabel">请在此或右侧<b>产品分类</b>列表中选择分类浏览</div>
              <div id="ProductTreeContent"></div>
              </asp:Panel>
      
        <asp:Repeater ID="ProductsRepeater" runat="server">
            <ItemTemplate>
                <div class="ProductsList">
                    <a href="ProductsDetails.aspx?CPID=<%#DataBinder.Eval(Container.DataItem,"编号") %>"
                        target="_blank">


                        <img src=" <%#DataBinder.Eval(Container.DataItem, "CPPicUrl").ToString() == "" ? "/images/nopic.gif" : DataBinder.Eval(Container.DataItem, "CPPicUrl").ToString() %>" alt="暂无图片" style="width: 80px; height: 60px; padding: 5px; border: solid 1px #333;" /></a>
                    <br />
                    <a href="ProductsDetails.aspx?CPID=<%#DataBinder.Eval(Container.DataItem,"编号") %>"
                        target="_blank">
                        <%#DataBinder.Eval(Container.DataItem,"CPName") %></a>


                </div>

            </ItemTemplate>
        </asp:Repeater>
        <div class="Clear"></div>
         <asp:Panel ID="PagingPanel" runat="server">
        <div class="Paging">
            当前页:<asp:Label ID="CurrentPageLabel" runat="server" Text="1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
    总页数:<asp:Label ID="TotalPageLabel" runat="server" Text="1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="FirstLinkButton" runat="server" OnClick="FirstLinkButton_Click">首页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="PreLinkButton" runat="server" OnClick="PreLinkButton_Click">上一页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="NextLinkButton" runat="server" OnClick="NextLinkButton_Click">下一页</asp:LinkButton>&nbsp;
    <asp:LinkButton ID="LastLinkButton" runat="server" OnClick="LastLinkButton_Click">末页</asp:LinkButton>&nbsp;&nbsp;
    转到第<asp:DropDownList ID="DropDownListPageNum" runat="server" AutoPostBack="true"
        OnSelectedIndexChanged="DropDownListPageNum_SelectedIndexChanged">
        <asp:ListItem>1</asp:ListItem>
    </asp:DropDownList>
            页
        </div>
             </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
    <img height="195" src="images/b/products.jpg" width="965" alt="富藤科技产品中心" />
</asp:Content>

<asp:Content ID="ScrollScriptContent" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="Server">
        <script type="text/javascript">
            $(document).ready(function(){
                $('#ProductTreeContent').append($('#ProductTreeRightBar').clone());
            });
            
            </script>
    </asp:Content>
