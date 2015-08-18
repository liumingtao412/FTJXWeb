var isChrome = navigator.userAgent.toLowerCase().match(/chrome/) != null; 

$(function() {
    bindCloseBtn('#service_bar_close');
    bindMiniBtn('#online_service_minibar', '#online_service_fullbar');
    bindGroupBtn('.service_menu li dl dt');
    showDefaultView();
    $('.service_menu li.hover dl dd').show();
    scrollAd('#online_service_bar');
    $(window).scroll(function() {
        scrollAd('#online_service_bar');
    });
});

function showDefaultView(status) {
    if (default_view) {
        $('#online_service_minibar').hide();
        $('#online_service_fullbar').show();
    } else {
        $('#online_service_fullbar').hide();
        $('#online_service_minibar').show();
    }
}

function bindCloseBtn(obj) {
    $(obj).click(function() {
        $('#online_service_fullbar').hide(1000, function() {
            if (isChrome) {
                $('#online_service_minibar').show();
            }
            else {
                $('#online_service_minibar').show(500);
            }
        });
    });
}

function bindMiniBtn(hideObj, showObj) {
    $(hideObj).bind('mouseover', function() {
        showMiniBar(hideObj, showObj);
    });
}

function bindGroupBtn(obj) {
    $(obj).hover(function() {
        var pobj = $(this).parent().parent();
        $(pobj).stop();
        $(pobj).siblings(".hover").removeClass('hover');
        showServiceMenu(pobj);
    }, function() {
        $(this).parent().parent().stop();
    });
}

function showMiniBar(hideObj, showObj) {
    $(hideObj).hide(500, function() {
        if (isChrome) {
            $(showObj).show();
        }
        else {
            $(showObj).show(500);
        }
    });
}

function showServiceMenu(obj, speed) {
    speed = speed || 500;
    $(obj).addClass('hover').children('dl').children('dd').slideDown(speed);
    $(obj).siblings().children('dl').children('dd').slideUp(speed);
}

function scrollAd(obj) {
    var offset = $(obj).height() + $(document).scrollTop() - 30;
    $(obj).stop().animate({ top: offset }, 1000);
}