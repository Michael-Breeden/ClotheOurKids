function afterReveal(el) { el.addEventListener('animationend', function (event) { $('.wow').each(function () { $(this).css('opacity', 1); }); }); } new WOW({ callback: afterReveal }).init();


$(document).ready(function () {

    new WOW().init();

    // SideNav Button Initialization
    $(".button-collapse").sideNav();
    // SideNav Scrollbar Initialization
    var sideNavScrollbar = document.querySelector('.custom-scrollbar');
    Ps.initialize(sideNavScrollbar);

    //$("#btnLogIn").click(function () {
    //    $(this).prop('disabled', true);

    //    var data = {};
    //    data.Email = $('#Email').val();
    //    data.Password = $('#Password').val();
    //    data.RememberMe = $('#RememberMe').is(':checked');

    //    var currentUrl = window.location.href;

    //    var token = $('input[name = "__RequestVerificationToken"]').val();

    //    $('.preloader-wrapper').show();

    //    $.ajax({
    //        url: "/Account/Login",
    //        method: "POST",
    //        data: {
    //            model: data,
    //            __RequestVerificationToken: token                
    //        },
    //        success: function (result) {

    //            $('#btnLogIn').prop('disabled', false);

    //            $('.preloader-wrapper').hide();

    //            if (result.Success == "1") {
    //                $('#loginError').hide();       
    //                window.location = "/";
    //            }
    //            else {
    //                $("#loginError").html(result.errorMsg);
    //                $('#loginError').show();
    //            }
    //        },
    //        error: function (err) {

    //            $('#btnLogIn').prop('disabled', false);
    //            $('.preloader-wrapper').hide();

    //            $("#loginError").html(err.errorMsg);
    //            $('#loginError').show();

    //            return false;
    //        }
    //    })

    //    return false;
    //});

    var year = new Date().getFullYear();
    $('.thisYear').html(year);

});