function afterReveal(el) { el.addEventListener('animationend', function (event) { $('.wow').each(function () { $(this).css('opacity', 1); }); }); } new WOW({ callback: afterReveal }).init();


$(document).ready(function () {

    //$("#mobileMenu").append($("#mainSiteMenu").clone().attr("id", "mobileMenu").attr("class", ""));
    var $menu = $('#mobileMenu').mmenu({
        //options
        offCanvas: {
            "zposition": "front",
            "position": "right"
        },
        navbar: {
            "title": "",
            "titleLink": "parent"
        },
        navbars: [
            {
                "position": "top",
                "content": [
                    "prev",
                    '<a id="mobile-navBack" href="#mm-0" aria-owns="mm-0"><span>Back</span></a>'
                ]
            }
        ],
        extensions: [
            "fullscreen",
            "border-full"
        ]
    }, {
            //configuration
            offCanvas: {
                pageSelector: '#page-wrapper'
            }
        });

    var $icon = $('#mMenuToggle');

    var API = $menu.data("mmenu");

    $icon.on('click', function () {
        API.open();
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
    API.bind("close:finish", function () {
        setTimeout(function () {
            $icon.removeClass("is-active");
        }, 0);
    });

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