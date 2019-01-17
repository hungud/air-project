$(window).load(function () {

	$(".preloader").delay(200).fadeOut();
});

var nowTemp = new Date();
    var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);
     
    var checkin = $('#DepDt').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      if (ev.date.valueOf() > checkout.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkout.setValue(newDate);
      }
      checkin.hide();
      $('#ArrDt')[0].focus();
    }).data('datepicker');
    var checkout = $('#ArrDt').datepicker({
      onRender: function(date) {
        return date.valueOf() <= checkin.date.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      checkout.hide();
    }).data('datepicker');
	
	var checkin2 = $('#DepDt2').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      checkin2.hide();
    }).data('datepicker');
	
	var checkin3 = $('#DepDt3').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      checkin3.hide();
    }).data('datepicker');
	
	var checkin4 = $('#DepDt4').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      checkin4.hide();
    }).data('datepicker');
	
	var checkin5 = $('#DepDt5').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      checkin5.hide();
    }).data('datepicker');

	var checkinHotel = $('#checkIn').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      if (ev.date.valueOf() > checkoutHotel.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkoutHotel.setValue(newDate);
      }
      checkinHotel.hide();
      $('#checkOut')[0].focus();
    }).data('datepicker');
    var checkoutHotel = $('#checkOut').datepicker({
      onRender: function(date) {
        return date.valueOf() <= checkinHotel.date.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      checkoutHotel.hide();
    }).data('datepicker');

	var pickUp = $('#Pick-upDate').datepicker({
      onRender: function(date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      if (ev.date.valueOf() > dropOff.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        dropOff.setValue(newDate);
      }
      pickUp.hide();
      $('#Drop-OffDate')[0].focus();
    }).data('datepicker');
    var dropOff = $('#Drop-OffDate').datepicker({
      onRender: function(date) {
        return date.valueOf() <= pickUp.date.valueOf() ? 'disabled' : '';
      }
    }).on('changeDate', function(ev) {
      dropOff.hide();
    }).data('datepicker');
	
	var dob = $('#dob1').datepicker({
    }).on('changeDate', function(ev) {
      dob.hide();
    }).data('datepicker');
	var dob2 = $('#dob2').datepicker({
    }).on('changeDate', function(ev) {
      dob2.hide();
    }).data('datepicker');
	var dob3 = $('#dob3').datepicker({
    }).on('changeDate', function(ev) {
      dob3.hide();
    }).data('datepicker');
	var dob4 = $('#dob4').datepicker({
    }).on('changeDate', function(ev) {
      dob4.hide();
    }).data('datepicker');

$(document).ready(function() {
	var FlightCount = 2;
	$("#search__flights").show();
	$('.search__tab-list').click(function () {
		$('.search__tab-list').removeClass("active");
		$(this).addClass("active");
		$(".search__widget").hide();
		var activeTab = $(this).find('a').attr("href");
		$(activeTab).fadeIn();
		return false;
	});
	$('.search__subtab-list').click(function () {
		$('.search__subtab-list').removeClass("active");
		$(this).addClass("active");
		return false;
	});
	
	$("#search__flights-round-trip").click(function () {
		if ($(this).hasClass("active")) {
			$(".round-trip").removeClass("col-md-4");
			$(".round-trip").addClass("col-md-2");
			$(".one-way").show();
			$(".multiple-city").hide();
			$(".add-delete-flight").hide();
		} 
		
		if ($(window).width()>767 && $(window).width()<992) {
			$(".round-trip").removeClass("col-sm-4");
			$(".round-trip").addClass("col-sm-2");
		}
	});
	$("#search__flights-one-way").click(function () {
		if ($(this).hasClass("active")) {
			$(".round-trip").removeClass("col-md-2");
			$(".round-trip").addClass("col-md-4");
			$(".one-way").hide();
			$(".multiple-city").hide();
			$(".add-delete-flight").hide();
		} 
		if ($(window).width()>767 && $(window).width()<992) {
			$(".round-trip").removeClass("col-sm-2");
			$(".round-trip").addClass("col-sm-4");
		}
	});
	$("#search__flights-multiple-cities").click(function () {
		if ($(this).hasClass("active")) {
			$(".round-trip").removeClass("col-md-2");
			$(".round-trip").addClass("col-md-4");
			$(".one-way").hide();
			$("#multiple-city02").fadeIn();
			$(".add-delete-flight").fadeIn();
			var FlightCount = 2;
		} 
		if ($(window).width()>767 && $(window).width()<992) {
			$(".round-trip").removeClass("col-sm-2");
			$(".round-trip").addClass("col-sm-4");
		}
	});
	$(".search__add-flight").click(function () {
		if (FlightCount==2) {
			$("#multiple-city03").fadeIn();
			FlightCount = FlightCount+1;
			$(".search__delete-flight").show();
		} 
		else if (FlightCount==3) {
			$("#multiple-city04").fadeIn();
			FlightCount = FlightCount+1;
		} 
		else {
			$("#multiple-city05").fadeIn();
			FlightCount = FlightCount+1;
			$(this).hide();
		} 
	});
	$(".search__delete-flight").click(function () {
		if (FlightCount==5) {
			$("#multiple-city05").hide();
			FlightCount = FlightCount-1;
			
			$(".search__add-flight").show();
		} 
		else if (FlightCount==4) {
			$("#multiple-city04").hide();
			FlightCount = FlightCount-1;
		} 
		else {
			$("#multiple-city03").hide();
			FlightCount = FlightCount-1;
			$(this).hide();
		} 
	});
	$("#room1").show();
	var roomCount = 1;
	$('.search__add-room').click(function () {
		if(roomCount == 1){
			$("#room2").fadeIn();
			roomCount = roomCount+1;
		}
		else if(roomCount == 2){
			$("#room3").fadeIn();
			roomCount = roomCount+1;
		}
		else if(roomCount == 3){
			$("#room4").fadeIn();
			roomCount = roomCount+1;
		}
		else {
			$("#room5").fadeIn();
			roomCount = roomCount+1;
		}
		return false;
	});
	$('.search__delete-room').click(function () {
		if(roomCount == 5){
			$("#room5").hide();
			roomCount = roomCount-1;
		}
		else if(roomCount == 4){
			$("#room4").hide();
			roomCount = roomCount-1;
		}
		else if(roomCount == 3){
			$("#room3").hide();
			roomCount = roomCount-1;
		}
		else {
			$("#room2").hide();
			roomCount = roomCount-1;
		}
		return false;
	});
	$(".search__cars-drop-off").hide();
	$("#drop-off-same-location").click(function () {
		if ($(this).is(":checked")) {
			$(".search__cars-pick-up").removeClass("col-md-8");
			$(".search__cars-pick-up").addClass("col-md-4");
			$(".search__cars-pick-up").removeClass("col-sm-8");
			$(".search__cars-pick-up").addClass("col-sm-4");
			$(".search__cars-drop-off").show();
		} else {
			$(".search__cars-pick-up").removeClass("col-md-4");
			$(".search__cars-pick-up").addClass("col-md-8");
			
			$(".search__cars-pick-up").removeClass("col-sm-4");
			$(".search__cars-pick-up").addClass("col-sm-8");
			$(".search__cars-drop-off").hide();
		}
	});

	$(function () {
		$(window).scroll(function () {
			jQuery("#DepDt, #ArrDt, #DepDt2, #DepDt3, #DepDt4, #DepDt5, #checkIn, #checkOut, #Pick-upDate, #Drop-OffDate").blur(); 
		});
	});
	
	
	
	$(window).click(function() {
		$('.tooltip--info').removeClass("active");
	});
	$('.tooltip--info').click(function(event){
		event.stopPropagation();
        if ($(this).hasClass("active")){
            $(this).removeClass("active");
        }else{
            $(this).addClass("active");
        }
    });
	
	$('[data-toggle="tooltip"]').tooltip();
	
	/* ---------- Start Scroll Top ---------- */
	$(".scroll-up").hide();
	var win_height = jQuery(window).height();
	$(window).scroll(function () {
			if ($(this).scrollTop() > win_height) {
					$('.scroll-up').fadeIn('medium');
			} else {
					$('.scroll-up').fadeOut('medium');
			}
	});
	$('.scroll-up').click(function () {
		$('body,html').animate({
			scrollTop: 0
		}, 800);
		return false;
	});
	/* ---------- End Scroll Top ---------- */
});
$(window).resize( function(){
	
});