
$(document).ready(function () {
    $('.advance-btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('#advance').toggleClass('visible');
        $(document).one('click', function closeAdvance(e) {
            if ($('#advance').has(e.target).length === 0) {
                $('#advance').removeClass('visible');
            } else {
                $(document).one('click', closeAdvance);
            }
        });
    });
    /************* Date Picker **************/
    $('.datetime').datetimepicker();
    $('.date').datetimepicker({
        timepicker: false,
        format: 'Y/m/d',
        formatDate: 'Y/m/d'
    });
    /************* End Date Picker **************/
    $('.submenu-btn').click(function () {
        $(this).find('.sub-menu').slideToggle();
    });
    $('.addAdults').click(function add() {
        var $rooms = $(".noOfAdults");
        var a = $rooms.val();
        if (a < 9) {
            a++;
            $(".subAdults").prop("disabled", !a);
            $rooms.val(a);
        }
        AdultInfantValidation();
    });
    $(".subAdults").prop("disabled", !$(".noOfAdults").val());
    $('.subAdults').click(function subst() {
        var $rooms = $(".noOfAdults");
        var b = $rooms.val();
        if (b > 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subAdults").prop("disabled", true);
        }
        AdultInfantValidation();
    });
    $('.addSeniors').click(function add() {
        var $rooms = $(".noOfSeniors");
        var a = $rooms.val();
        if (a < 9) {
            a++;
            $(".subSeniors").prop("disabled", !a);
            $rooms.val(a);
        }
    });
    $(".subSeniors").prop("disabled", !$(".noOfSeniors").val());
    $('.subSeniors').click(function subst() {
        var $rooms = $(".noOfSeniors");
        var b = $rooms.val();
        if (b >= 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subSeniors").prop("disabled", true);
        }
    });
    $('.addChildrens').click(function add() {
        var $rooms = $(".noOfChildrens");
        var $adultrooms = $(".noOfAdults");
        var a = $rooms.val();
        var b = $adultrooms.val();
        if (a < b) {
            a++;
            $(".subChildrens").prop("disabled", !a);
            $rooms.val(a);
        }
    });
    $(".subChildrens").prop("disabled", !$(".noOfChildrens").val());
    $('.subChildrens').click(function subst() {
        var $rooms = $(".noOfChildrens");
        var b = $rooms.val();
        if (b >= 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subChildrens").prop("disabled", true);
        }
    });
    $('.addInfants').click(function add() {
        var $rooms = $(".noOfInfants");
        var $adultrooms = $(".noOfAdults");
        var b = $adultrooms.val();
        var a = $rooms.val();
        if (a < b) {
            a++;
            $(".subInfants").prop("disabled", !a);
            $rooms.val(a);
        }
        AdultInfantValidation();
    });
    $(".subInfants").prop("disabled", !$(".noOfInfants").val());
    $('.subInfants').click(function subst() {
        var $rooms = $(".noOfInfants");
        var b = $rooms.val();
        if (b >= 1) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subInfants").prop("disabled", true);
        }
        AdultInfantValidation();
    });
    $('.addOnSeats').click(function add() {
        var $rooms = $(".noOfOnSeats");
        var a = $rooms.val();
        a++;
        $(".subOnSeats").prop("disabled", !a);
        $rooms.val(a);
    });
    $(".subOnSeats").prop("disabled", !$(".noOfOnSeats").val());
    $('.subOnSeats').click(function subst() {
        var $rooms = $(".noOfOnSeats");
        var b = $rooms.val();
        if (b >= 9) {
            b--;
            $rooms.val(b);
        }
        else {
            $(".subOnSeats").prop("disabled", true);
        }
    });
    
    $("#Price-range-slider").slider({
        from: 0, to: 10000,
        step: 5, round: 55,
        format: { format: '##.00', locale: 'de' },
        dimension: '&nbsp;$',
        skin: "round",
        skin: "round_plastic",
        onstatechange: function (value) {
        //onslidestop: function (value) {
            //ShowWaitProgress();
            //console.log('It Works!')
            GetRefineSearchType("#Price-range-slider", value)
        }
    });
    $("#Departure-range-slider").slider({
        from: 0, to: 24,
        step: 1, round: 1,
        format: { format: '##.00', locale: 'de' },
        dimension: '&nbsp;',
        skin: "round",
        skin: "round_plastic",
        onstatechange: function (value) {
            //ShowWaitProgress();
            //console.log('It Works!')
            GetRefineSearchType("#Departure-range-slider", value)
        }
    });
    $("#Arrive-range-slider").slider({
        from: 0, to: 24,
        step: 1, round: 1,
        format: { format: '##.00', locale: 'de' },
        dimension: '&nbsp;',
        skin: "round",
        skin: "round_plastic",
        onstatechange: function (value) {
            //ShowWaitProgress();
            //console.log('It Works!')
            GetRefineSearchType("#Arrive-range-slider", value)
        }
    });
    /******************************End Range Slider*************************/
    $(function () {
        $("#tabs").tabs();
    });
    $('#tabs ul li a').click(function () {
        $('#tabs ul li a').removeClass("active");
        $(this).addClass("active");
    });
});