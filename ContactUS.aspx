<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="ContactUS.aspx.cs" Inherits="ContactUS" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <!--引用百度地图API-->
<style type="text/css">
    html,body{margin:0;padding:0;}
    .iw_poi_title {color:#CC5522;font-size:14px;font-weight:bold;overflow:hidden;padding-right:13px;white-space:nowrap}
    .iw_poi_content {font:12px arial,sans-serif;overflow:visible;padding-top:4px;white-space:-moz-pre-wrap;word-wrap:break-word}
</style>
<script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div class="stitle">
        <div class="stitle1">
            <asp:Label ID="stitle" runat="server"></asp:Label></div>
    </div>
    <div class="defaltContainer">
        <asp:Label ID="ContectInfLabel" runat="server"></asp:Label>
        <%--<asp:Image ID="EMapImage" runat="server" Visible="false" />--%>
         <asp:Panel ID="EMapPanel" runat="server" Width="600px" Visible="false">
        <div style="width:593px;height:550px;border:#ccc solid 1px;" id="dituContent"></div>
             </asp:Panel>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
    <script type="text/javascript">
    //创建和初始化地图函数：
    function initMap(){
        createMap();//创建地图
        setMapEvent();//设置地图事件
        addMapControl();//向地图添加控件
        addMarker();//向地图中添加marker
    }
    
    //创建地图函数：
    function createMap(){
        var map = new BMap.Map("dituContent");//在百度地图容器中创建一个地图
        var point = new BMap.Point(121.601119,31.323099);//定义一个中心点坐标
        map.centerAndZoom(point,17);//设定地图的中心点和坐标并将地图显示在地图容器中
        window.map = map;//将map变量存储在全局
    }
    
    //地图事件设置函数：
    function setMapEvent(){
        map.enableDragging();//启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom();//启用地图滚轮放大缩小
        map.enableDoubleClickZoom();//启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard();//启用键盘上下左右键移动地图
    }
    
    //地图控件添加函数：
    function addMapControl(){
        //向地图中添加缩放控件
	var ctrl_nav = new BMap.NavigationControl({anchor:BMAP_ANCHOR_TOP_LEFT,type:BMAP_NAVIGATION_CONTROL_LARGE});
	map.addControl(ctrl_nav);
        //向地图中添加缩略图控件
	var ctrl_ove = new BMap.OverviewMapControl({anchor:BMAP_ANCHOR_BOTTOM_RIGHT,isOpen:1});
	map.addControl(ctrl_ove);
        //向地图中添加比例尺控件
	var ctrl_sca = new BMap.ScaleControl({anchor:BMAP_ANCHOR_BOTTOM_LEFT});
	map.addControl(ctrl_sca);
    }
    
    //标注点数组
    var markerArr = [{title:"上海富藤机械科技有限公司",content:"上海市浦东新区兰嵩路555号2幢A座森兰美伦大厦1003",point:"121.600894|31.322744",isOpen:0,icon:{w:21,h:21,l:0,t:0,x:6,lb:5}}
		 ];
    //创建marker
    function addMarker(){
        for(var i=0;i<markerArr.length;i++){
            var json = markerArr[i];
            var p0 = json.point.split("|")[0];
            var p1 = json.point.split("|")[1];
            var point = new BMap.Point(p0,p1);
			var iconImg = createIcon(json.icon);
            var marker = new BMap.Marker(point,{icon:iconImg});
			var iw = createInfoWindow(i);
			var label = new BMap.Label(json.title,{"offset":new BMap.Size(json.icon.lb-json.icon.x+10,-20)});
			marker.setLabel(label);
            map.addOverlay(marker);
            label.setStyle({
                        borderColor:"#808080",
                        color:"#333",
                        cursor:"pointer"
            });
			
			(function(){
				var index = i;
				var _iw = createInfoWindow(i);
				var _marker = marker;
				_marker.addEventListener("click",function(){
				    this.openInfoWindow(_iw);
			    });
			    _iw.addEventListener("open",function(){
				    _marker.getLabel().hide();
			    })
			    _iw.addEventListener("close",function(){
				    _marker.getLabel().show();
			    })
				label.addEventListener("click",function(){
				    _marker.openInfoWindow(_iw);
			    })
				if(!!json.isOpen){
					label.hide();
					_marker.openInfoWindow(_iw);
				}
			})()
        }
    }
    //创建InfoWindow
    function createInfoWindow(i){
        var json = markerArr[i];
        var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>"+json.content+"</div>");
        return iw;
    }
    //创建一个Icon
    function createIcon(json){
        var icon = new BMap.Icon("http://app.baidu.com/map/images/us_mk_icon.png", new BMap.Size(json.w,json.h),{imageOffset: new BMap.Size(-json.l,-json.t),infoWindowOffset:new BMap.Size(json.lb+5,1),offset:new BMap.Size(json.x,json.h)})
        return icon;
    }
    
    initMap();//创建和初始化地图
</script>
</asp:Content>