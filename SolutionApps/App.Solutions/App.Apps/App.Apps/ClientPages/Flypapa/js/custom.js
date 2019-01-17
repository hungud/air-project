jQuery(window).load(function () {

});
function OnLoadGetDataList() {
    try {
debugger;
        var RequestContentData = '';
        var Reqst_Resource = '';
var selectiontype=$("#selectedtype").val();
        var dataonly = ($('#'+selectiontype+'directOnly').is(':checked')==true?"on":"off");
console.log("Dataonly"+dataonly);

if($('#DepDt4').val()==undefined){
    $('#DepDt4').val("");
}
if($('#DepDt3').val()==undefined){
    $('#DepDt3').val("");
}
if($('#DepDt2').val()==undefined){
    $('#DepDt2').val("");
}
if($('#DepDt1').val()==undefined){
    $('#DepDt1').val("");
}
switch (selectiontype) {
            case "O":
                Reqst_Resource = {
                    BookingType: "A",
                    SearchType: selectiontype,
                    origin: $('#OBdep1').val(),
                    destination: $('#OBarr1').val(),
                    departuredate: $('#ODepDt').val().split('/')[2] + '-' + $('#ODepDt').val().split('/')[0] + '-' + $('#ODepDt').val().split('/')[1],
                    Airline: $('#OAirline').val(),
                    FareType: $('#OFareType').val(),
                    numadults: $($('#adultsO').find('#nbadults')).val(),
                    nbchilds: $($('#childsO').find('#nbchilds')).val(),
                    numinfants: $($('#infantsO').find('#numinfants')).val(),
                    directOnly: dataonly
                };
                break;
            case "R":
                Reqst_Resource = {
                    BookingType: "A",
                    SearchType: selectiontype,
                    origin: $('#RBdep1').val(),
                    destination: $('#RBarr1').val(),
                    departuredate: $('#RDepDt').val().split('/')[2] + '-' + $('#RDepDt').val().split('/')[0] + '-' + $('#RDepDt').val().split('/')[1],
                    returndate: $('#ArrDt').val().split('/')[2] + '-' + $('#ArrDt').val().split('/')[0] + '-' + $('#ArrDt').val().split('/')[1],
                    Airline: $('#RAirline').val(),
                    FareType: $('#RFareType').val(),
                    numadults: $($('#adultsR').find('#nbadults')).val(),
                    nbchilds: $($('#childsR').find('#nbchilds')).val(),
                    numinfants: $($('#infantsR').find('#numinfants')).val(),
                    directOnly:  dataonly
                };
                break;
            case "M":
                Reqst_Resource = {
                    BookingType: "A",
                    SearchType: selectiontype,

                    origin1: $('#MBdep1').val(),
                    destination1: $('#MBarr1').val(),
                    departuredate1: $('#DepDt1').val().length>0?$('#DepDt1').val().split('/')[2] + '-' + $('#DepDt1').val().split('/')[0] + '-' + $('#DepDt1').val().split('/')[1]:"",

                    origin2: $('#MBdep2').val(),
                    destination2: $('#MBarr2').val(),
                    departuredate2: $('#DepDt2').val().length>0?$('#DepDt2').val().split('/')[2] + '-' + $('#DepDt2').val().split('/')[0] + '-' + $('#DepDt2').val().split('/')[1]:"",

                    origin3: $('#MBdep3').val(),
                    destination3: $('#MBarr3').val(),
                    departuredate3: $('#DepDt3').val().length>0?$('#DepDt3').val().split('/')[2] + '-' + $('#DepDt3').val().split('/')[0] + '-' + $('#DepDt3').val().split('/')[1]:"",

                    origin4: $('#MBdep4').val(),
                    destination4: $('#MBarr4').val(),
                    departuredate4: $('#DepDt4').val().length>0?$('#DepDt4').val().split('/')[2] + '-' + $('#DepDt4').val().split('/')[0] + '-' + $('#DepDt4').val().split('/')[1]:"",

                    Airline: $('#MAirline').val(),
                    FareType: $('#MFareType').val(),
                    numadults: $($('#adultsM').find('#nbadults')).val(),
                    nbchilds: $($('#childsM').find('#nbchilds')).val(),
                    numinfants: $($('#infantsM').find('#numinfants')).val(),
                    directOnly:  dataonly
                    
                };
                break;
        }

        //$(JSON.stringify(Reqst_Resource)).serialize();
        //console.log(JSON.stringify(Reqst_Resource));
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
        console.log("search click");
        OnLoadGetDataList();
    });
     $("#ObtnSearch").on('click', function () {
        console.log("search click");
        OnLoadGetDataList();
    });
     $("#MbtnSearch").on('click', function () {
        console.log("search click");
        OnLoadGetDataList();
    });

    $(document).on('click', '.triptype', function () {
    var triptype=$(this).attr('data-val');
    $('#selectedtype').val(triptype);
    });

     $('.addAdults').click(function add() {
        
        var $rooms = $(this).parent('div').find('#nbadults');
        var a = $rooms.val();
            a++;
            $rooms.val(a);
    });

     $('.addChildrens').click(function add() {
        var $rooms = $(this).parent('div').find('#nbchilds');
        var a = $rooms.val();
            a++;
            $rooms.val(a);
    });
     $('.addInfants').click(function add() {
        var $rooms = $(this).parent('div').find('#numinfants');
        var a = $rooms.val();
            a++;
            $rooms.val(a);
    });

     $('.subAdults').click(function subst() {
        var $rooms = $(this).parent('div').find('#nbadults');
        var b = $rooms.val();
        if (b > 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subAdults").prop("disabled", true);
        }
    });
      $('.subChildrens').click(function subst() {
        var $rooms = $(this).parent('div').find('#nbchilds');
        var b = $rooms.val();
        if (b >= 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subChildrens").prop("disabled", true);
        }
    });

       $('.subInfants').click(function subst() {
        
        var $rooms = $(this).parent('div').find('#numinfants');
        var b = $rooms.val();
        if (b >= 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subChildrens").prop("disabled", true);
        }
    });

    var remove_button = $(".btn-remove-field"); //Fields wrapper
    var max_fields = 5; //maximum input boxes allowed
    var add_button = $(".add_more_field_button"); //Add button ID
    var x = 1; //initlal text box count

    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $("#search-field-section-" + x).show();
        }
    });
    $(remove_button).click(function (e) { //on add input button click
        e.preventDefault();
        $(this).parent('div').parent('div').parent('div').parent('div').hide(); x--; AppActionData--;
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

var checkinR = $('#RDepDt').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > checkout.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkout.setValue(newDate);
    }
    checkinR.hide();
    $('#ArrDt')[0].focus();
}).data('datepicker');

var checkinO = $('#ODepDt').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > checkout.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkout.setValue(newDate);
    }
    checkinO.hide();
    $('#ArrDt')[0].focus();
}).data('datepicker');

var checkinM = $('#DepDt1').datepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > checkout.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkout.setValue(newDate);
    }
    checkinM.hide();
    $('#ArrDt')[0].focus();
}).data('datepicker');


var checkout = $('#ArrDt').datepicker({
    onRender: function (date) {
        return date.valueOf() <= checkinR.date.valueOf() ? 'disabled' : '';
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

