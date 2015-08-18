<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="藤仓气缸 | 日本藤仓气缸 | 低摩擦气缸 | 韩国SEBA - 上海富藤机械科技有限公司" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BorderContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftContentPlaceHolder" runat="Server">
    <div id="focus" class="focus">
        <script type="text/javascript"> 
            var imgInfor = JSON.parse('<%=GetBannerImageInfor()%>');
            var pics="",links="",texts="";
            var i = 0;            
            if(<%=IsShowBannerImage.ToString().ToLower()%>)
            {
           
                for (i = 0; i < imgInfor.length; i++)
            {

                pics = pics + imgInfor[i].ImageUrl + "|";
                links = links + imgInfor[i].ImageLink + "|";               
                texts = texts + imgInfor[i].ImageText + "|";               
                }
            pics = pics.replace(/\|$/g,"");
            links = links.replace(/\|$/g,"");
            texts = texts.replace(/\|$/g,"");

                
            var focus_width = 785;
            var focus_height = 199;
            var text_height = 0;
            var swf_height = focus_height + text_height;

            document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + focus_width + '" height="' + swf_height + '">');
            document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="images/focus.swf"><param name="quality" value="high"><param name="bgcolor" value="#F0F0F0">');
            document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
            document.write('<param name="FlashVars" value="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '">');
            document.write('<embed src="images/focus.swf" wmode="opaque" FlashVars="pics=' + pics + '&links=' + links + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '" menu="false" bgcolor="#F0F0F0" quality="high" width="' + focus_width + '" height="' + focus_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); document.write('</object>');
            }
            else
            {
                document.getElementById("focus").style.display="none";
            }
        </script>
    </div>
    <div class="ltitle">
        <div class="ltitle1"><a href="Products.aspx">精品展示</a></div>
    </div>
    <div class="ProductsShowImg">
        <ul>
        <asp:Repeater ID="StarProductsRepeater" runat="server">
            <ItemTemplate>
                <li><a target="_blank" href="<%#DataBinder.Eval(Container.DataItem,"LinkURL") %>" title="<%#DataBinder.Eval(Container.DataItem,"Remarks") %>">
                    <img alt="<%#DataBinder.Eval(Container.DataItem,"AltValue") %>" src="<%#DataBinder.Eval(Container.DataItem,"PicURL") %>"></a>
                    <div class="text">
                        <a target="_blank" href="<%#DataBinder.Eval(Container.DataItem,"LinkURL") %>" title="<%#DataBinder.Eval(Container.DataItem,"Remarks") %>">
                            <%#DataBinder.Eval(Container.DataItem,"ProductName") %>
                        </a>
                    </div>
                </li>

            </ItemTemplate>
        </asp:Repeater>
        </ul>
    </div>
    <div class="ltitle">
        <div class="ltitle1"><a href="AboutUS.aspx">关于我们</a></div>
    </div>
    <div class="aboutUS">
        <asp:Label ID="AboutUSLiteral" runat="server"></asp:Label>
        <a href="AboutUS.aspx" class="read-more">Read more</a>
    </div>
    <table style="width: 760px">
        <tr>
            <td style="width: 50%; padding-bottom: 18px;" valign="top">
                <div class="ltitle">
                    <div class="ltitle1 ltitle_no_border"><a href="News.aspx">最新动态</a></div>
                </div>
                <div class="small-post">
                    <img alt="News" src="images/clean.jpg" class="postimg" />
                    <asp:Repeater ID="NewsRepeater" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 260px;">
                                <tr>
                                    <td style="vertical-align: bottom; width: 260px; height: 30px;">

                                        <%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "isTop")) ? "<img  src='images/hot.gif' style='display:inline;float:left;border:0px none;' />" : "<img  src='images/notHot.gif' style='display:inline;float:left;border:0px none;' />"%>
                                        <a class="one" target="_blank"
                                            href="NewsDetails.aspx?NewsID=<%#DataBinder.Eval(Container.DataItem,"编号") %>"
                                            title="<%#DataBinder.Eval(Container.DataItem,"newsTitle")%>">
                                            <%#DataBinder.Eval(Container.DataItem, "newsTitle").ToString().Length > 36 ? DataBinder.Eval(Container.DataItem, "newsTitle").ToString().Substring(0,34)+"…" : DataBinder.Eval(Container.DataItem, "newsTitle")%>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: bottom; text-align: right; width: 260px; list-style-position: outside; border-bottom: silver 1px dotted; list-style-type: disc;">
                                        <%#DataBinder.Eval(Container.DataItem,"newsTime","{0:yyyy-MM-dd}") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </td>
            <td style="padding-bottom: 18px;" valign="top">
                <div class="ltitle">
                    <div class="ltitle1 ltitle_no_border"><a href="ContactUS.aspx">联系我们</a></div>
                </div>
                <div class="small-post">
                    <img src="images/contact.jpg" alt="Contuct Us" />
                    <p>
                        <asp:Label ID="ContactInf" runat="server"></asp:Label>
                    </p>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="ScrollScriptContent" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="Server">
        <script type="text/javascript">
            //滚动
            (function($){

                $.fn.kxbdMarquee = function(options){
                    var opts = $.extend({},$.fn.kxbdMarquee.defaults, options);
        
                    return this.each(function(){
                        var $marquee = $(this);//滚动元素容器
                        var _scrollObj = $marquee.get(0);//滚动元素容器DOM
                        var scrollW = $marquee.width();//滚动元素容器的宽度
                        var scrollH = $marquee.height();//滚动元素容器的高度
                        var $element = $marquee.children(); //滚动元素
                        var $kids = $element.children();//滚动子元素
                        var scrollSize=0;//滚动元素尺寸
                        var _type = (opts.direction == 'left' || opts.direction == 'right') ? 1:0;//滚动类型，1左右，0上下

                        //防止滚动子元素比滚动元素宽而取不到实际滚动子元素宽度
                        $element.css(_type?'width':'height',10000);
                        //获取滚动元素的尺寸
                        if (opts.isEqual) {
                            scrollSize = $kids[_type?'outerWidth':'outerHeight']() * $kids.length;
                        }else{
                            $kids.each(function(){
                                scrollSize += $(this)[_type?'outerWidth':'outerHeight']();
                            });
                        }
                        //滚动元素总尺寸小于容器尺寸1/2，不滚动
                        if (scrollSize<=((_type?scrollW:scrollH))/2){ 
                            return clearInterval(moveId);
                        }
                        //滚动元素总尺寸小于容器尺寸,但是大于1/2，则复制四份子元素
                        if (scrollSize<(_type?scrollW:scrollH)){                            
                            $element.append($kids.clone()).css(_type?'width':'height',scrollSize*2);
                            scrollSize=scrollSize*2;
                            $element.append($kids.clone()).css(_type?'width':'height',scrollSize*2);
                        }                        
                        //当滚动元素大于容器时，克隆滚动子元素将其插入到滚动元素后，并设定滚动元素宽度                       
                        $element.append($kids.clone()).css(_type?'width':'height',scrollSize*2);                      

            
                        var numMoved = 0;
                        var a = 0;
                        function scrollFunc(){
                            var _dir = (opts.direction == 'left' || opts.direction == 'right') ? 'scrollLeft':'scrollTop';
                            if (opts.loop > 0) {
                                numMoved+=opts.scrollAmount;
                                
                                if(numMoved>scrollSize*opts.loop){
                                    _scrollObj[_dir] = 0;
                                    a = 0;
                                    //console.log("hahaha");
                                    return clearInterval(moveId);
                                } 
                            }
                            if(opts.direction == 'left' || opts.direction == 'up'){                                
                                var newPos = _scrollObj[_dir] + opts.scrollAmount;
                                //console.log(newPos + "--" +  opts.scrollAmount + "--" + _scrollObj[_dir] + "--" + scrollSize + "--" + scrollW + "--" + _scrollObj["id"]);
                                if(newPos>=scrollSize){
                                    //console.log("hah2");
                                    newPos -= scrollSize;                                    
                                }
                                _scrollObj[_dir] = newPos;
                            }else{
                                var newPos = _scrollObj[_dir] - opts.scrollAmount;
                                if(newPos<=0){
                                    newPos += scrollSize;
                                }
                                _scrollObj[_dir] = newPos;
                            }
                        };
                        //滚动开始
                        var moveId = setInterval(scrollFunc, opts.scrollDelay);
                        //鼠标划过停止滚动
                        $marquee.hover(
                            function(){
                                clearInterval(moveId);
                            },
                            function(){
                                clearInterval(moveId);
                                moveId = setInterval(scrollFunc, opts.scrollDelay);
                            }
                        );
            
                        //控制加速运动
                        if(opts.controlBtn){
                            $.each(opts.controlBtn, function(i,val){
                                $(val).bind(opts.eventA,function(){
                                    opts.direction = i;
                                    opts.oldAmount = opts.scrollAmount;
                                    opts.scrollAmount = opts.newAmount;
                                }).bind(opts.eventB,function(){
                                    opts.scrollAmount = opts.oldAmount;
                                });
                            });
                        }
                    });
                };
                $.fn.kxbdMarquee.defaults = {
                    isEqual:true,//所有滚动的元素长宽是否相等,true,false
                    loop: 0,//循环滚动次数，0时无限
                    direction: 'left',//滚动方向，'left','right','up','down'
                    scrollAmount:1,//步长
                    scrollDelay:40,//时长
                    newAmount:3,//加速滚动的步长
                    eventA:'mousedown',//鼠标事件，加速
                    eventB:'mouseup'//鼠标事件，原速
                };
    
                $.fn.kxbdMarquee.setDefaults = function(settings) {
                    $.extend( $.fn.kxbdMarquee.defaults, settings );
                };
    
            })(jQuery);
            <%if(IsScrollStarProducts)
              {
                  string Js = @"
            $(document).ready(function(){
                $('.ProductsShowImg').kxbdMarquee({
                    direction:'left',
                    scrollAmount:[Speed]
                });
            });";
                  Js=Js.Replace("[Speed]", ScrollSpeed.ToString());
                  Response.Write(Js);
            }
            %>
</script> 
</asp:Content>


