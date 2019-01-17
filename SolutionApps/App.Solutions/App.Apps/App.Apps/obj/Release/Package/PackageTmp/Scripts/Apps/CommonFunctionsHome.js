/*******************************************************
********************************************************
***Function      : Common Function *********************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 04/09/2015*** ***********************
********************************************************
********************************************************/



/////////////////////////////////////////Master User Login Validation//////////////////////////////////////////////////////////////////////////////////////////////

jQuery(function ($) {
    $(document).on('click', '.toolbar a[data-target]', function (e) {
        e.preventDefault();
        var target = $(this).data('target');
        $('.widget-box.visible').removeClass('visible');//hide others
        $(target).addClass('visible');//show target
    });
});
//you don't need this, just used for changing background
jQuery(function ($) {
    $('#btn-login-dark').on('click', function (e) {
        $('body').attr('class', 'login-layout');
        $('#id-text2').attr('class', 'white');
        $('#id-company-text').attr('class', 'blue');
        e.preventDefault();
    });
    $('#btn-login-light').on('click', function (e) {
        $('body').attr('class', 'login-layout light-login');
        $('#id-text2').attr('class', 'grey');
        $('#id-company-text').attr('class', 'blue');
        e.preventDefault();
    });
    $('#btn-login-blur').on('click', function (e) {
        $('body').attr('class', 'login-layout blur-login');
        $('#id-text2').attr('class', 'white');
        $('#id-company-text').attr('class', 'light-blue');
        e.preventDefault();
    });
    // scrollables
    $(function () {
        $('.scrollable').slimScroll({
            height: '90%'
        });
    });
    $('.ChangeAR').on('click', function () {
        $('body').addClass('rtl');
    });
    $('.ChangeEN').on('click', function () {
        $('body').removeClass('rtl');
        location.reload();
    });

    function tick() {
        $('#ticker li:first').slideUp(function () { $(this).appendTo($('#ticker')).slideDown(); });
    }
    setInterval(function () { tick() }, 3000);


    function announcement() {
        $('#ticker_1 li:first').slideUp(function () { $(this).appendTo($('#ticker_1')).slideDown(); });
    }
    setInterval(function () { announcement() }, 3000);

});

$(document).ready(function () {
    //alert('Hi Login');
    //var val = '@Html.Raw((string)TempData["ErrorMessage"])';
    //var ErrorMessagesss ='@Html.Raw(Json.Encode(TempData["ErrorMessage"]))';
    //var ErrorMessage ='@TempData["ErrorMessage"]';
    //ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
});

function ValidateUser(QPROID) {
    var ErrorMessage = "The user name or password not provided.";
    if ($('#txtEmailID').val() == '' && $('#txtPassword').val() == '')
    {
        ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
        return false;
    }
    else if ($('#txtEmailID').val()=='')
    {
        ErrorMessage = "The User Name not provided.";
        ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
        return false;
    }
    else if ($('#txtPassword').val()=='')
    {
        ErrorMessage = "The Password not provided.";
        ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
        return false;
    }
    return true;
}
/////////////////////////////////////////Master User Login Validation//////////////////////////////////////////////////////////////////////////////////////////////
