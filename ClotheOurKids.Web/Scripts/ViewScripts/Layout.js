function afterReveal(el) { el.addEventListener('animationend', function (event) { $('.wow').each(function () { $(this).css('opacity', 1); }); }); } new WOW({ callback: afterReveal }).init();


$(document).ready(function () {

    var yCoordinate;

    new WOW().init();

    //Load Facebook JavaScript SDK
    $.ajaxSetup({ cache: true });
    $.getScript('//connect.facebook.net/en_US/sdk.js', function(){
        FB.init({
            appId: '676045709255023',
            version: 'v2.10' // or v2.1, v2.2, v2.3, ...
        });
    });


    $("#mobileMenu").append($("#mainSiteMenu").clone().attr("id", "mobileMenu").attr("class", ""));
    var $menu = $('#mobileMenu').mmenu({
        //options
        wrappers: ["bootstrap4"],
        navbar: {
            "title": "",
            "titleLink": "parent"
        },
        extensions: {
            "all": ["border-none", "position-right", "position-front"],
            "(max-width: 768px)": ["fullscreen"]
        }
            
    }, {
            //configuration
            offCanvas: {
                pageSelector: '#page-wrapper'
            }
        });

    var $icon = $('#mMenuToggle');

    var API = $menu.data("mmenu");

    $icon.on('click', function () {          

        yCoordinate = document.body.scrollTop + document.documentElement.scrollTop;

        API.open();

        //ttps://www.codingforums.com/javascript-programming/220170-setting-documentelement-style-overflow-=-hidden-jolts-top-page-undesirably.html


    });

    API.bind("openPanel:start", function ($panel) {
        var id = $panel.attr("id");

        toggleMobileInnerNavbar(id);

    });

    API.bind("open:start", function () {
        toggleMobileInnerNavbar();
    });

    API.bind("open:finish", function () {       

        setTimeout(function () {
            $icon.addClass("is-active");
        }, 0);
    });

    API.bind("open:after", function () {
        $('body').animate({ scrollTop: yCoordinate }, 0);
    });

    API.bind("close:finish", function () {
        setTimeout(function () {
            $icon.removeClass("is-active");
        }, 0);
    });

    //API.bind("closePanel:after", function () {
    //    setTimeout(function () {
    //        $('body').animate({ scrollTop: yCoordinate }, 0);
    //        //window.scrollTo(0, yCoordinate);
    //    }, 2);
    //    //window.scrollTo(0, yCoordinate);
    //    //$('body').animate({ scrollTop: yCoordinate }, 0);
    //});

    $('#loginError').hide();

    $('.preloader-wrapper').hide();

    $("#btnLogIn").click(function () {
        $(this).prop('disabled', true);

        var data = {};
        data.Email = $('#Email').val();
        data.Password = $('#Password').val();
        data.RememberMe = $('#RememberMe').is(':checked');

        var currentUrl = window.location.href;

        var token = $('input[name = "__RequestVerificationToken"]').val();

        $('.preloader-wrapper').show();

        $.ajax({
            url: "/Account/Login",
            method: "POST",
            data: {
                model: data,
                __RequestVerificationToken: token                
            },
            success: function (result) {

                $('#btnLogIn').prop('disabled', false);

                $('.preloader-wrapper').hide();

                if (result.Success == "1") {
                    $('#loginError').hide();       
                    window.location = "/";
                }
                else {
                    $("#loginError").html(result.errorMsg);
                    $('#loginError').show();
                }
            },
            error: function (err) {

                $('#btnLogIn').prop('disabled', false);
                $('.preloader-wrapper').hide();

                $("#loginError").html(err.errorMsg);
                $('#loginError').show();

                return false;
            }
        })

        return false;
    });

    var year = new Date().getFullYear();
    $('.thisYear').html(year);

});



/*Code pulled from the mdb.js file for scrolling navbars. Add class to MMenu to ensure it has the right top style*/
var OFFSET_TOP = 50;

$(window).scroll(function () {
    if ($('.navbar').length) {
        if ($('.navbar').offset().top > OFFSET_TOP) {
            $('#mobileMenu').addClass("mmenu-top-collapse");
            $('#mobileMenu').removeClass("mmenu-top");
        } else {
            $('#mobileMenu').addClass("mmenu-top");
            $('#mobileMenu').removeClass("mmenu-top-collapse");
        }
    }
});

function toggleMobileInnerNavbar(panelId) {
    if (typeof panelId === 'undefined') {
        panelId = 'mm-0';
    }

    if (panelId == 'mm-0') {
        $('#mobileMenu .mm-navbar').addClass('hideNav');
    }
    else {
        $('#mobileMenu .mm-navbar').removeClass('hideNav');
    }

}

//Load Twitter API
window.twttr = (function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0],
      t = window.twttr || {};
    if (d.getElementById(id)) return t;
    js = d.createElement(s);
    js.id = id;
    js.src = "https://platform.twitter.com/widgets.js";
    fjs.parentNode.insertBefore(js, fjs);

    t._e = [];
    t.ready = function (f) {
        t._e.push(f);
    };

    return t;
}(document, "script", "twitter-wjs"));