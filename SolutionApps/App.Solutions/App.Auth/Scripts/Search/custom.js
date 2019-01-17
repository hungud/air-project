jQuery(window).load(function () {

});
function OnLoadGetDataList() {
    try {

        var RequestContentData = '';
        var Reqst_Resource = '';
        switch ($("input[name='radioval']:checked").val()) {
            case "O":
                Reqst_Resource = {
                    BookingType: "A",
                    SearchType: $("input[name='radioval']:checked").val(),
                    origin: $('#OBdep1').val(),
                    destination: $('#OBarr1').val(),
                    departuredate: $('#DepDt').val().split('/')[2] + '-' + $('#DepDt').val().split('/')[0] + '-' + $('#DepDt').val().split('/')[1],
                    Airline: $('#Airline').val(),
                    FareType: $('#FareType').val(),
                    numadults: $('#numadults').val(),
                    nbchilds: $('#nbchilds').val(),
                    numinfants: $('#numinfants').val(),
                    directOnly: $('#directOnly').val(),
                    numinfants: $('#numinfants').val()
                };
                break;
            case "R":
                Reqst_Resource = {
                    BookingType: "A",
                    SearchType: $("input[name='radioval']:checked").val(),
                    origin: $('#OBdep1').val(),
                    destination: $('#OBarr1').val(),
                    departuredate: $('#DepDt').val().split('/')[2] + '-' + $('#DepDt').val().split('/')[0] + '-' + $('#DepDt').val().split('/')[1],
                    returndate: $('#ArrDt').val().split('/')[2] + '-' + $('#ArrDt').val().split('/')[0] + '-' + $('#ArrDt').val().split('/')[1],
                    Airline: $('#Airline').val(),
                    FareType: $('#FareType').val(),
                    numadults: $('#numadults').val(),
                    nbchilds: $('#nbchilds').val(),
                    numinfants: $('#numinfants').val(),
                    directOnly: $('#directOnly').val(),
                    numinfants: $('#numinfants').val()
                };
                break;
            case "M":
                Reqst_Resource = {
                    BookingType: "A",
                    SearchType: $("input[name='radioval']:checked").val(),

                    origin1: $('#OBdep1').val(),
                    destination1: $('#OBarr1').val(),
                    departuredate1: $('#DepDt').val().split('/')[2] + '-' + $('#DepDt').val().split('/')[0] + '-' + $('#DepDt').val().split('/')[1],

                    origin2: $('#OBdep2').val(),
                    destination2: $('#OBarr2').val(),
                    departuredate2: $('#DepDt2').val().split('/')[2] + '-' + $('#DepDt2').val().split('/')[0] + '-' + $('#DepDt2').val().split('/')[1],

                    origin3: $('#OBdep3').val(),
                    destination3: $('#OBarr3').val(),
                    departuredate3: $('#DepDt3').val().split('/')[2] + '-' + $('#DepDt3').val().split('/')[0] + '-' + $('#DepDt3').val().split('/')[1],

                    origin4: $('#OBdep4').val(),
                    destination4: $('#OBarr4').val(),
                    departuredate4: $('#DepDt4').val().split('/')[2] + '-' + $('#DepDt4').val().split('/')[0] + '-' + $('#DepDt4').val().split('/')[1],

                    Airline: $('#Airline').val(),
                    FareType: $('#FareType').val(),
                    numadults: $('#numadults').val(),
                    nbchilds: $('#nbchilds').val(),
                    numinfants: $('#numinfants').val(),
                    directOnly: $('#directOnly').val(),
                    numinfants: $('#numinfants').val()
                };
                break;
        }

        //$(JSON.stringify(Reqst_Resource)).serialize();
        //alert(JSON.stringify(Reqst_Resource));
        //alert('Hi');
        window.location.href = "http://air.nanojot.com/?Search=" + JSON.stringify(Reqst_Resource);
        //window.location.href = "http://localhost:11011/?Search=" + JSON.stringify(Reqst_Resource);

        //window.location.href = 'http://www.google.com';
    } catch (e) {
        var error = e;
        error = error;
    }
}
$(document).ready(function () {
    $("#btnSearch").on('click', function () {
        OnLoadGetDataList();
    });
});
function selectWidth() {
    $(".select_option").find("select").css('width', '');
    $(".select_option").each(function () {
        var selWidth = $(this).width();
        $(this).find("select").css('width', (selWidth - 1));
    });
}

var nowTemp = new Date();
var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);

var checkin = $('#DepDt').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > checkout.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkout.setValue(newDate);
    }
    checkin.hide();
    $('#ArrDt')[0].focus();
}).data('datepicker');
var checkout = $('#ArrDt').datepicker({
    onRender: function (date) {
        return date.valueOf() <= checkin.date.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    checkout.hide();
}).data('datepicker');

var checkin2 = $('#DepDt2').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    checkin2.hide();
}).data('datepicker');

var checkin3 = $('#DepDt3').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    checkin3.hide();
}).data('datepicker');

var checkin4 = $('#DepDt4').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    checkin4.hide();
}).data('datepicker');

var checkinHotel = $('#checkIn').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > checkoutHotel.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkoutHotel.setValue(newDate);
    }
    checkinHotel.hide();
    $('#checkOut')[0].focus();
}).data('datepicker');
var checkoutHotel = $('#checkOut').datepicker({
    onRender: function (date) {
        return date.valueOf() <= checkinHotel.date.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    checkoutHotel.hide();
}).data('datepicker');

var pickUp = $('#Pick-upDate').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > dropOff.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        dropOff.setValue(newDate);
    }
    pickUp.hide();
    $('#Drop-OffDate')[0].focus();
}).data('datepicker');
var dropOff = $('#Drop-OffDate').datepicker({
    onRender: function (date) {
        return date.valueOf() <= pickUp.date.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    dropOff.hide();
}).data('datepicker');

$(document).ready(function () {
    selectWidth();

    $(".search_widget").hide();
    $(".search_widget:first-child").show();
    $(".search_tab li:first-child").addClass("active");

    $('.search_tab li').click(function () {
        $('.search_tab li').removeClass("active");
        $(this).addClass("active");
        $(".search_widget").hide();
        var activeTab = $(this).find('a').attr("href");
        $(activeTab).show();
        return false;
    });

    $('.multicity_options').hide();
    $('input[name=radioval]').click(function () {
        if ($(this).attr("value") == "R") {
            $('.one_way').fadeIn('fast');
            $('.multicity').fadeIn('fast');
            $('.multicity_options').fadeOut('fast');
        }
        if ($(this).attr("value") == "O") {
            $('.one_way').fadeOut('fast');
            $('.multicity_options').fadeOut('fast');
            $('.multicity').fadeIn('fast');
        }

        if ($(this).attr("value") == "M") {
            $('.multicity').fadeOut('fast');
            $('.one_way').fadeOut('fast');
            $('.multicity_options').fadeIn('fast');
        }
    });

    jQuery(function () {
        jQuery(window).scroll(function () {
            jQuery("#DepDt, #ArrDt, #DepDt2, #DepDt3, #DepDt4").blur();
        });
    });

    $("#room2, #room3, #room4, .deleteRoom").hide();
    $('.addRoom').click(function () {
        if ($(this).hasClass("room1Opt")) {
            $(this).removeClass("room1Opt");
            $(this).addClass("room2Opt");
            $('#room2').fadeIn('fast');
            $('.deleteRoom').fadeIn('fast');
            $('.deleteRoom').addClass("deleteRoom2");
        }
        else if ($(this).hasClass("room2Opt")) {
            $(this).removeClass("room2Opt");
            $(this).addClass("room3Opt");
            $('#room3').fadeIn('fast');
            $('.deleteRoom').addClass("deleteRoom3");
            $('.deleteRoom').removeClass("deleteRoom2");
        }
        else if ($(this).hasClass("room3Opt")) {
            $(this).removeClass("room3Opt");
            $(this).addClass("room4Opt");
            $('#room4').fadeIn('fast');
            $('.addRoom').hide();
            $('.deleteRoom').addClass("deleteRoom4");
            $('.deleteRoom').removeClass("deleteRoom3");
        }
    });
    $('.deleteRoom').click(function () {
        if ($(this).hasClass("deleteRoom2")) {
            $('#room2').fadeOut('fast');
            $('.deleteRoom').removeClass("deleteRoom2");
            $('.deleteRoom').hide();
            $('.addRoom').addClass("room1Opt");
            $('.addRoom').removeClass("room2Opt");
        }
        else if ($(this).hasClass("deleteRoom3")) {
            $('#room3').fadeOut('fast');
            $('.deleteRoom').addClass("deleteRoom2");
            $('.deleteRoom').removeClass("deleteRoom3");
            $('.addRoom').addClass("room2Opt");
            $('.addRoom').removeClass("room3Opt");
        }
        else if ($(this).hasClass("deleteRoom4")) {
            $('#room4').fadeOut('fast');
            $('.deleteRoom').addClass("deleteRoom3");
            $('.deleteRoom').removeClass("deleteRoom4");
            $('.addRoom').addClass("room3Opt");
            $('.addRoom').removeClass("room4Opt");
            $('.addRoom').show();
        }
    });

    $(".Drop-off-Location, .same-location").hide();
    $('.PickDropLocation').click(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $('.Drop-off-Location').fadeOut('fast');
            $('.same-location').hide();
            $('.different-location').fadeIn('fast');

        }
        else {
            $(this).addClass("active");
            $('.Drop-off-Location').fadeIn('fast');
            $('.different-location').hide();
            $('.same-location').fadeIn('fast');
        }
    });

    $(".CarOption, .less-options").hide();
    $('.car-options').click(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $('.CarOption').fadeOut('fast');
            $('.less-options').hide();
            $('.more-options').fadeIn('fast');

        }
        else {
            $(this).addClass("active");
            $('.CarOption').fadeIn('fast');
            $('.more-options').hide();
            $('.less-options').fadeIn('fast');
        }
    });

    $('[data-toggle="tooltip"]').tooltip();
});

$(window).resize(function () {
    selectWidth();
});