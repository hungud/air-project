var SelectedAirLineBokingPayment_Data = '';
var myPassengerDetailsListArray = new Array();
var FilterAllFlightSegment_Array = new Array();
//****************************** Booking Payment start*************************//
$(function ($) {
    try {
        $("#txtPICCNumber").inputmask({ "Card": "XXXX-XXXX-XXXX-XXXXX" });
        ///==================================================//
        ///==================================================//
        $("#txtTDDOB").datepicker({
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_txtTDDOB").click(function () {
            $("#txtTDDOB").datepicker("show");
        });
        ///==================================================//
        $("#txtPIVCNumber").keypress(function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                ConfirmBootBox("Successfully", "Please Enter Number Only.....", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtPIVCNumber").focus();
                return false;
            }
        });
        $("#txtPICCNumber").keypress(function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                ConfirmBootBox("Successfully", "Please Enter Number Only.....", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtPICCNumber").focus();
                return false;
            }
        });
        //$("#txtPICCMonth").keypress(function (e) {
        //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //        ConfirmBootBox("Successfully", "Please Enter Number Only.....", 'App_Warning', initialCallbackYes, initialCallbackNo);
        //        $("#txtPICCMonth").focus();
        //        return false;
        //    }
        //});
        //$("#txtPICCYear").keypress(function (e) {
        //    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //        ConfirmBootBox("Successfully", "Please Enter Number Only.....", 'App_Warning', initialCallbackYes, initialCallbackNo);
        //        $("#txtPICCYear").focus();
        //        return false;
        //    }
        //});
    } catch (e) { var ex = e; }
});

$(document).ready(function (e) {
    var monthShortValues = [{ "Key": "Jan (01)", "Value": "01", }, { "Key": "Feb (02)", "Value": "02", }, { "Key": "Mar (03)", "Value": "03", }, { "Key": "Apr (04)", "Value": "04", }, { "Key": "May (05)", "Value": "05", }, { "Key": "Jun (06)", "Value": "06", }, { "Key": "Jul (07)", "Value": "07", }, { "Key": "Sep (08)", "Value": "08", }, { "Key": "Sep (09)", "Value": "09", }, { "Key": "Oct (10)", "Value": "10", }, { "Key": "Nov (11)", "Value": "11", }, { "Key": "Dec (12)", "Value": "12", }];
    $.each(monthShortValues, function (key, value) {
        var monthdata = value;
        $(".selectPICCMonth").append("<option value=\"" + monthdata.Value + "\">" + monthdata.Key + "</option>");
    });

    //var monthShortValues = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    $('.selectPICCYear').yearselect({ order: 'asc' });
});
function ValidateData() {
    try {
        if (ValidateTravelerDetails()) {
            //======================Payment Info (Secure Payment Transaction)==================
            if ($("#selPICCType").val().trim() == "Select a Payment Card") {
                ConfirmBootBox("Successfully", "Please Enter Payment Method.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selPICCType").focus();
                return false;
            }
            if ($("#txtPICCNumber").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Credit or Debit Card Number.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtPICCNumber").focus();
                return false;
            }
            if ($("#txtPIVCNumber").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Card Verification Number.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtPIVCNumber").focus();
                return false;
            }
            if ($("#txtPICCHName").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Card Holder Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtPICCHName").focus();
                return false;
            }
            if (!ValidateTravelerPassangerAndCreaditCardHolder()) {
                ConfirmBootBox("Successfully", "Card Holder Name Should be one of the Adult Passanger Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtPICCHName").focus();
                return false;
            }
            
            //if ($("#txtPICCMonth").val().trim() == "") {
            //    ConfirmBootBox("Successfully", "Please Enter Expiration Date Month.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtPICCMonth").focus();
            //    return false;
            //}
            //if ($("#txtPICCYear").val().trim() == "") {
            //    ConfirmBootBox("Successfully", "Please Enter Expiration Date Year.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtPICCYear").focus();
            //    return false;
            //}
            

            //======================Payment Info (Secure Payment Transaction)==================


            //======================Billing & Contact Information===============================
            if ($("#selBCICountry").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Country.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selBCICountry").focus();
                return false;
            }           
            if ($("#txtBCICity").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please City.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCICity").focus();
                return false;
            }
            if ($("#txtBCIStreet").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Street.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIStreet").focus();
                return false;
            }
            if ($("#txtBCIZip").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Zip.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIZip").focus();
                return false;
            }
            if ($("#txtBCIBillPhone").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Billing Phone.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIBillPhone").focus();
                return false;
            }
            if ($("#txtBCIBillPhone").val().trim().length != 10) {
                ConfirmBootBox("Successfully", "Please Enter valid Phone.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIBillPhone").focus();
                return false;
            }
            if ($("#txtBCIEmail").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter E-Mail ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIEmail").focus();
                return false;
            }
            if (!ValidateEmail($("#txtBCIEmail").val().trim())) {
                ConfirmBootBox("Successfully", "Please Enter Vailid E-Mail ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIEmail").focus();
                return false;
            }
            if ($("#txtBCIConfEmail").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter  E-Mail ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIConfEmail").focus();
                return false;
            }
            if (!ValidateEmail($("#txtBCIConfEmail").val().trim())) {
                ConfirmBootBox("Successfully", "Please Enter Vailid E-Mail ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIConfEmail").focus();
                return false;
            }
            if ($("#txtBCIConfEmail").val().trim() != $("#txtBCIEmail").val().trim()) {
                ConfirmBootBox("Successfully", "Please Enter  E-Mail not matched ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIConfEmail").focus();
                return false;
            }
            if ($("#txtBCIPhoneZip").val().trim() == "") {
                ConfirmBootBox("Successfully", "Please Enter Phone Zip.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtBCIPhoneZip").focus();
                return false;
            }
            //======================Billing & Contact Information===============================

            //======================Terms & COndition Contact Information===============================
            if (!$("#chkTravelInsYes").prop("checked") && !$("#chkTravelInsNo").prop("checked")) {
                ConfirmBootBox("Successfully", "Please Select Travel Protection Option.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selPICCType").focus();
                return false;
            }
            if (!$("#txtpolicy").prop("checked")) {
                ConfirmBootBox("Successfully", "Please Select Travel Rules and Restrictions.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#txtpolicy").focus();
                return false;
            }
            //======================Terms & COndition Contact Information===============================
            return true;
        }
    }
    catch (Ex) {
    }
}


function ValidateTravelerDetails() {
    var ValidationStatus = true;
    try {
        //======================Traveler Details==================
        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
            if (parseInt(dt_value.Adults) > 0) {
                for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                    if ($("#txtFirstNameAdults" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Adult First Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtFirstNameAdults" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtMiddleNameAdults" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Adult Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtMiddleNameAdults" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtLastNameAdults" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Adult Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameAdults" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtDOBDateAdults" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Adult DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateAdults" + i + "").focus();
                        return ValidationStatus = false;
                    }
                }
            }
            if (parseInt(dt_value.Childrens) > 0) {
                for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {
                    if ($("#txtFirstNameChildrens" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Children First Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtFirstNameChildrens" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtMiddleNameChildrens" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Children Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtMiddleNameChildrens" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtLastNameChildrens" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Children Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameChildrens" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtDOBDateChildrens" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Children DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateChildrens" + i + "").focus();
                        return ValidationStatus = false;
                    }
                }
            }
            if (parseInt(dt_value.Infants) > 0) {
                for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                    if ($("#txtFirstNameInfants" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Infants First Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtFirstNameInfants" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtMiddleNameInfants" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Infants Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtMiddleNameInfants" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtLastNameInfants" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Infants Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameInfants" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtDOBDateInfants" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Infants DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateInfants" + i + "").focus();
                        return ValidationStatus = false;
                    }
                }
            }
            if (parseInt(dt_value.OnSeat) > 0) {
                for (var i = 0; i < parseInt(dt_value.OnSeat) ; i++) {
                    if ($("#txtFirstNameOnSeat" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter OnSeat First Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtFirstNameOnSeat" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtMiddleNameOnSeat" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter OnSeat Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtMiddleNameOnSeat" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtLastNameOnSeat" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter OnSeat Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameOnSeat" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtDOBDateOnSeat" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter OnSeat DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateOnSeat" + i + "").focus();
                        return ValidationStatus = false;
                    }
                }
            }
            if (parseInt(dt_value.Seniors) > 0) {
                for (var i = 0; i < parseInt(dt_value.Seniors) ; i++) {
                    if ($("#txtFirstNameSeniors" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Seniors First Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtFirstNameSeniors" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtMiddleNameSeniors" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Seniors Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtMiddleNameSeniors" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtLastNameSeniors" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Seniors Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameSeniors" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    if ($("#txtDOBDateSeniors" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Seniors DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateSeniors" + i + "").focus();
                        return ValidationStatus = false;
                    }
                }
            }
        });
        //======================Traveler Details==================
    }
    catch (Ex) {
    }
    return ValidationStatus;
}

function ValidateTravelerPassangerAndCreaditCardHolder() {
    var ValidationStatus = true;
    try {
        //======================Traveler Details==================
        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
            if (parseInt(dt_value.Adults) > 0) {
                for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                    var PassangerName = $("#txtFirstNameAdults" + i + "").val().trim() + $("#txtMiddleNameAdults" + i + "").val().trim() + $("#txtLastNameAdults" + i + "").val().trim();
                    var CardHolderName = $("#txtPICCHName").val().trim().split(" ").join("");
                    if (PassangerName != CardHolderName)
                    {
                        return ValidationStatus = false;
                    }
                }
            }
           
        });
        //======================Traveler Details==================
    }
    catch (Ex) {
    }
    return ValidationStatus;
}

function LoadAirReservationResultData(ResultData) {
    try {
        if (ResultData.Message == "Success" && ResultData.Status == true) {
            SelectedAirLineBokingPayment_Data = SolutionDataTraveler("GET", "SelectedAirLineBokingPayment")[0];
            var Reqst_flightsDetailsList_Resource = SolutionDataTraveler("GET", "AirReservationBookingRequest").flightsDetailsList[0];
            var Reqst_passengersDetail_Resource = SolutionDataTraveler("GET", "AirReservationBookingRequest").passengersDetailsList[0];
            var Reqst_CCD_Resource = SolutionDataTraveler("GET", "AirReservationBookingRequest").creditCardOtherDetails[0];
            $(".AirBokingConfirmation-Panel").removeClass("hidden");
            $(".AirBokingPayment-Panel").addClass("hidden");
            var Result_Data = ResultData.Data;
            /******************************confirmation_title*************************/
            var confirmation_title = "";
            $.each(Result_Data.flights_details, function (FD_Key, FD_Value) {
                confirmation_title = FD_Value.DepartureDate + " <i class='glyphicon glyphicon-play'></i>" + FD_Value.ArrivalDate + "  TRIP TO " + FD_Value.DepartureCityCode + ", " + FD_Value.ArrivalCityCode + "";
            });
            $("#confirmation_title").empty().append(confirmation_title);
            /******************************Fulter AirLineName*************************/

            /******************************Confirmation_PreparedFor*************************/
            var Confirmation_PreparedFor = "";
            Confirmation_PreparedFor += "<div class='col-md-6 '>";
            Confirmation_PreparedFor += "<p class='prep_for'>PREPARED FOR</p>";
            $.each(Result_Data.passengers_details, function (PD_Key, PD_Value) {
                Confirmation_PreparedFor += " <p>" + PD_Value.passengername +" "+ PD_Value.Surname + "</p>";
            });
            Confirmation_PreparedFor += "<p>RESERVATION CODE  :  " + Result_Data.pnrnumber + "<br />AIRLINE RESERVATION CODE  " + Result_Data.pnrnumber + "</p>";
            Confirmation_PreparedFor += "</div>";
            Confirmation_PreparedFor += "<div class='col-md-6 '>";
            Confirmation_PreparedFor += " <p style='padding-top:15px;'>SKYFLIGHT TRAVEL CENTRE<br />" + Reqst_passengersDetail_Resource.PhoneNumber + "<br />" + Reqst_passengersDetail_Resource.PassengerEmail + "";
            Confirmation_PreparedFor += " <br /><h5>DATE & TIME OF BOOKING : " + $.formatDateTime('DD dd M yy g:ii a', new Date(new Date().toJSON())) + "</h5></p>";
            Confirmation_PreparedFor += "</div>";

            $("#Confirmation_PreparedFor").empty().append(Confirmation_PreparedFor);
            /******************************Confirmation_PreparedFor*************************/

            /******************************Confirmation FlightDetails AirLineName*************************/
            var Confirmation_FlightDetails = "";
            $.each(Result_Data.flights_details, function (FD_Key, FD_Value) {
                Confirmation_FlightDetails += "<div class='row'><div class='col-md-12'><div class='col-md-12 orng_title'><p>DEPARTURE: " + FD_Value.DepartureDate + " <i class='glyphicon glyphicon-play'></i> ARRIVAL: " + FD_Value.ArrivalDate + "  <i class='fa fa-plane fa-1'></i><br><span>Please verify flight times prior to departure</span></p></div></div></div>";

                Confirmation_FlightDetails += "<div class='row'><div class='col-md-12'><div class='col-md-12'>";
                Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table table-bordered' style='text-align:inherit; margin-top:15px;'>";
                Confirmation_FlightDetails += "<tr><td rowspan='2'><p>" + Airline_Name(FD_Value.AirlineName) + "</p><br><p class='fl_heading'><strong>" + FD_Value.AirlineName + "  " + FD_Value.FlightNumber + "</strong></p><p>Duration:<br>" + FD_Value.ArrivalDateTime + "</p></td>";
                Confirmation_FlightDetails += "<td><p class='fl_heading'><strong>" + FD_Value.DepartureCityCode + "</strong></p><p><br>" + AirPort_Name(FD_Value.DepartureCityCode) + "</p></td><td><p class='fl_heading'><strong>" + FD_Value.ArrivalCityCode + "</strong></p><p><br>" + AirPort_Name(FD_Value.ArrivalCityCode) + "</p></td><td rowspan='2'><p>Aircraft:<br>" + FD_Value.AircraftType + "<br></p><p>Distance (in Miles): <br>" + FD_Value.DistanceTravel + "<br><br>Stop(s): " + FD_Value.Stops + "</p></td></tr>";
                Confirmation_FlightDetails += "<td rowspan='2'></td></tr><tr><td><p>Departing At:<br>" + FD_Value.DepartureTime + "<br></p><p><br>" + FD_Value.DepartureDate + "<br></p><p><strong>Terminal:</strong><br>" + FD_Value.DepartureTerminal + "</p></td><td><p>Arriving At:<br>" + FD_Value.ArrivalTime + "<br></p><p><br>" + FD_Value.ArrivalDate + "<br></p><p><strong>Terminal:</strong><br>" + FD_Value.ArrivalTerminal + "</p></td></tr>";
                Confirmation_FlightDetails += "</table></div></div></div>";
                if(FD_Value.status == "false")
                    $("#error_title_title").empty().append("Booking is not yet confirm, SkyFlight Customer service team will get back to you once the booking is confirmed in next 24 hours.");
            });

            Confirmation_FlightDetails += "<div class='row'><div class='col-md-12'>";
            Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table table-striped' style='text-align:inherit;'>";
            Confirmation_FlightDetails += "<thead class='thead-inverse'>";
            Confirmation_FlightDetails += "<tr><th>Passenger Name:</th><th>Seats:</th><th>Class:</th><th>Status:</th><th>Meals:</th></tr></thead>";
            $.each(Result_Data.passengers_details, function (FD_Key, FD_Value) {
                Confirmation_FlightDetails += " <tr><td>" + FD_Value.passengername + " " + FD_Value.Surname + "</td><td>Check-In Required</td><td>Economy</td><td>Confirmed</td><td>Meals</td></tr>";
            });
            Confirmation_FlightDetails += "</table></div></div>";
            $("#Confirmation_FlightDetails").empty().append(Confirmation_FlightDetails);
            /******************************Confirmation FlightDetails AirLineName*************************/


            /******************************Payment Detail*************************/

            var Confirmation_PaymentDetail = "";

            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-12 orng_bg top'><strong>Payment Detail:</strong></div>";
            Confirmation_PaymentDetail += "<div class='col-md-6'>Name On The Card:</div>";
            Confirmation_PaymentDetail += "<div class='col-md-6'>" + Result_Data.passengers_details[0].passengername + "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-6'>Credit Card Charged:</div>";

            Confirmation_PaymentDetail += "<div class='col-md-6'>****-****-****-" + Reqst_CCD_Resource.CardNumber.substr(12, 4); + "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-6'>Amount Charged:</div";
            Confirmation_PaymentDetail += "<div class='col-md-6'>" + Result_Data.currCode_details[0].CurrCode + " " + Result_Data.currCode_details[0].PaymentAmount + "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-12 orng_bg'><strong>Billing Detail:</strong></div>";
            Confirmation_PaymentDetail += "<div class='col-md-6'>" + Result_Data.passengers_details[0].passengername + "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-6 pad_bot25'></div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";

            $("#Confirmation_PaymentDetail").empty().append(Confirmation_PaymentDetail);

                        /******************************Payment Detail*************************/
        }
        else if (ResultData.Message == "") {
            $(".AirBokingConfirmation-Panel").removeClass("hidden");
            $(".AirBokingPayment-Panel").addClass("hidden");
            var error_title_title = "";
            error_title_title += "<div class='row'>";
            error_title_title += "<div class='col-md-12'>";

            //error_title_title += "<div class='col-md-12 orng_bg top'><strong>An Error Has Occurred</strong></div>";
            //error_title_title += "<div class='col-md-6'>An unexpected error occurred on our website. The admistrator has been notified</div>";


            error_title_title += "<div class='col-md-12 orng_bg top'><strong>Booking is Unconfirmed</strong></div>";
            error_title_title += "<div class='col-md-6'>We have notified with explicit declaration of unconfirmed booking by email as well</div>";

            error_title_title += "<div class='col-md-6'>" + ResultData.ErrorCode + "</div>";
            error_title_title += "</div>";
            error_title_title += "</div>";
            $("#error_title_title").empty().append(error_title_title);
        }
        else {
            $(".AirBokingPayment-Panel").removeClass("hidden");
            $(".AirBokingConfirmation-Panel").addClass("hidden");
        }
    }
    catch (e) {
        var error = e;
        error = e;
        HideWaitProgress();
    }
}

function AirBookingPaymentForm() {
    try {
        var FilterAllFlightSegmentArray = SolutionDataTraveler("GET", "SelectedAirLineBokingPayment");
        if (FilterAllFlightSegmentArray != 'undefined' && FilterAllFlightSegmentArray != null) {

            var AirItineraryPricingInformation = '', AirLineCode = '', AirLineName = '';
            FilterAllFlightSegment_Array = new Array();
            $.each(FilterAllFlightSegmentArray, function (All_key, All_value) {
                $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                    FilterAllFlightSegment_Array.push(Segment_value);
                });
            });

            ////FlightSegments Details Management
            var flights_Details_List = new Array();
            $.each(FilterAllFlightSegment_Array, function (All_key, All_value) {
                AirItineraryPricingInformation = All_value.AirItineraryPricingInfo.ItinTotalFare.TotalFare;
                AirLineCode = All_value.FlightNumber;
                AirLineName = All_value.FlightNumber;
                flights_Details_List.push({
                    DepartureDate: All_value.DepartureDateTime.slice(0, -3),
                    ArrivalDate: All_value.ArrivalDateTime.slice(0, -3),
                    AirlineName: All_value.ArrivalAirport.LocationCode,
                    FlightNumber: All_value.FlightNumber,
                    DepartureCityCode: All_value.DepartureAirport.LocationCode,
                    DepartureTime: All_value.DepartureDateTime.slice(0, -3),
                    DepartureTerminal: All_value.DepartureAirport.LocationCode,
                    ArrivalCityCode: All_value.ArrivalAirport.LocationCode,
                    ArrivalTime: All_value.ArrivalDateTime.slice(0, -3),
                    ArrivalTerminal: All_value.ArrivalAirport.LocationCode,
                    DistanceTravel: "0",
                    AircraftType: All_value.Equipment[0].AirEquipType,
                    DepartureDateTime: All_value.DepartureDateTime.slice(0, -3),
                    ArrivalDateTime: All_value.ArrivalDateTime.slice(0, -3),
                    BookingClass: "0",
                    FlightTime: All_value.DepartureDateTime.slice(0, -3),
                    DirectionInd: "0",
                    DepAirportLocationCode: All_value.DepartureAirport.LocationCode,
                    OperatingAirlineCode: All_value.OperatingAirline.Code,
                    ArrAirportLocationCode: All_value.ArrivalAirport.LocationCode,
                    Equipment: All_value.Equipment[0].AirEquipType,
                    MarketingAirline: All_value.MarketingAirline.Code,
                    NoofPassengers: "1",
                    resBookDesigCode: All_value.ResBookDesigCode,
                    status: "NN"
                });
            });
            ////FlightSegments Details Management
            var numOfPassCnt = 0;
            var passengers_Details_List = new Array(), PassengerNameNumberCounting = 0;
            myPassengerDetailsListArray = SolutionDataTraveler("GET", "GetPassengerDetailsList");
            $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
                for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                    PassengerNameNumberCounting = (PassengerNameNumberCounting + 1);
                    passengers_Details_List.push({
                        passengerType: "Adults",
                        passengername: $("#txtFirstNameAdults" + i + "").val(),
                        passengerAddress: $("#txtBCIStreet").val() + $("#selBCICountry").val(),
                        ReservationCode: $("#txtBCIZip").val(),
                        airlineName: AirLineName,
                        email: $("#txtBCIEmail").val(),
                        DepartureCityCode: $("#txtPICCNumber").val(),
                        DepartureTime: $("#txtPICCNumber").val(),
                        DepartureTerminal: $("#txtPICCNumber").val(),
                        IsInfant: "false",
                        DateofBirth: $("#txtDOBDateAdults" + i + "").val(),
                        PhoneLocationCode: $("#txtBCIPhoneZip").val(),
                        PassengerNameNumber: PassengerNameNumberCounting + '.1',
                        PhoneNumber: $("#txtBCIBillPhone").val(),
                        PhoneUseType: "H",
                        PassengerEmail: $("#txtBCIEmail").val(),
                        PassengerNameRef: $("#txtFirstNameAdults" + i + "").val(),
                        Gender: $("#selTDGenderAdults" + i + "").val(),
                        GivenName: $("#txtFirstNameAdults" + i + "").val(),
                        Surname: $("#txtLastNameAdults" + i + "").val()
                    });
                }
                for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {
                    PassengerNameNumberCounting = (PassengerNameNumberCounting + 1);
                    passengers_Details_List.push({
                        passengerType: "Childrens",
                        passengername: $("#txtFirstNameChildrens" + i + "").val(),
                        passengerAddress: $("#txtBCIStreet").val(),
                        ReservationCode: $("#txtBCIZip").val(),
                        airlineName: AirLineName,
                        email: $("#txtBCIEmail").val(),
                        DepartureCityCode: $("#txtPICCNumber").val(),
                        DepartureTime: $("#txtPICCNumber").val(),
                        DepartureTerminal: $("#txtPICCNumber").val(),
                        DateofBirth: $("#txtDOBDateChildrens" + i + "").val(),
                        PhoneLocationCode: $("#txtBCIPhoneZip").val(),
                        PassengerNameNumber: PassengerNameNumberCounting + '.1',
                        PhoneNumber: $("#txtBCIBillPhone").val(),
                        PhoneUseType: "H",
                        PassengerEmail: $("#txtBCIEmail").val(),
                        PassengerNameRef: $("#txtFirstNameChildrens" + i + "").val(),
                        Gender: $("#selTDGenderAdults" + i + "").val(),
                        GivenName: $("#txtFirstNameChildrens" + i + "").val(),
                        Surname: $("#txtLastNameChildrens" + i + "").val()
                    });
                }
                for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                    PassengerNameNumberCounting = (PassengerNameNumberCounting + 1);
                    passengers_Details_List.push({
                        passengerType: "Infants",
                        passengername: $("#txtFirstNameInfants" + i + "").val(),
                        passengerAddress: $("#txtBCIStreet").val(),
                        ReservationCode: $("#txtBCIZip").val(),
                        airlineName: AirLineName,
                        email: $("#txtBCIEmail").val(),
                        DepartureCityCode: $("#txtPICCNumber").val(),
                        DepartureTime: $("#txtPICCNumber").val(),
                        DepartureTerminal: $("#txtPICCNumber").val(),
                        DateofBirth: $("#txtDOBDateInfants" + i + "").val(),
                        PhoneLocationCode: $("#txtBCIPhoneZip").val(),
                        PassengerNameNumber: PassengerNameNumberCounting + '.1',
                        PhoneNumber: $("#txtBCIBillPhone").val(),
                        PhoneUseType: "H",
                        PassengerEmail: $("#txtBCIEmail").val(),
                        PassengerNameRef: $("#txtFirstNameInfants" + i + "").val(),
                        GivenName: $("#txtFirstNameInfants" + i + "").val(),
                        Gender: $("#selTDGenderAdults" + i + "").val(),
                        Surname: $("#txtLastNameInfants" + i + "").val()
                    });
                }

            });

            ////FlightSegments Details Management
            var credit_CardOther_Details_List = new Array();
            credit_CardOther_Details_List.push({
                CardType: $("#selPICCType").val().trim(), CardNumber: $("#txtPICCNumber").val().trim(),
                CVVNumber: $("#txtPIVCNumber").val().trim(),
                //CardExpiryDate: $("#txtPICCYear").val().trim() + "-" + $("#txtPICCMonth").val().trim(),
                CardExpiryDate: $("#selectPICCYear").val().trim() + "-" + $("#selectPICCMonth").val().trim(),
                NameOnCard: $("#txtPICCHName").val().trim(), AirLineCode: FilterAllFlightSegment_Array[0].MarketingAirline.Code,
                CurrCode: AirItineraryPricingInformation.CurrencyCode, PaymentAmount: AirItineraryPricingInformation.Amount
            });
            var APP_REQ = {
                "flightsDetailsList": flights_Details_List,
                "passengersDetailsList": passengers_Details_List,
                "creditCardOtherDetails": credit_CardOther_Details_List
            };

            function ResultCallBackSuccess(e, xhr, opts) {
                var App_Data = e;
                SolutionDataTraveler("SET", "AirReservationBookingResult", App_Data);
                LoadAirReservationResultData(App_Data)
            }
            function ResultCallBackError(e, xhr, opts) {
            }
            SolutionDataTraveler("SET", "AirReservationBookingRequest", APP_REQ);
            MasterAppConfigurationsServices("POST", CommonConfiguration.WebAPIServicesURL + "API/AirReservationBooking/PostAirBooking", JSON.stringify(APP_REQ), ResultCallBackSuccess, ResultCallBackError);
        }
    }
    catch (e) { HideWaitProgress(); }
}

//****************************** Booking Payment end*************************/
$(document).ready(function () {
    var ResultData = SolutionDataTraveler("GET", "AirReservationBookingResult");
    if (ResultData != null) {
        if (ResultData.Message == "Success" && ResultData.Status == true)
            //if (SolutionDataTraveler("GET", "AirReservationBookingResult").Message == "Success")
        {
            $(".AirBokingConfirmation-Panel").removeClass("hidden");
            $(".AirBokingPayment-Panel").addClass("hidden");
            LoadAirReservationResultData(ResultData);
        }
        else {
            $(".AirBokingPayment-Panel").removeClass("hidden");
            $(".AirBokingConfirmation-Panel").addClass("hidden");
        }
    }
    else {
        $(".AirBokingPayment-Panel").removeClass("hidden");
        $(".AirBokingConfirmation-Panel").addClass("hidden");
    }
    $(".btn-success-booking").on('click', function () {
        ShowWaitProgress();
        if (ValidateData()) {
            AirBookingPaymentForm();
        }
        else { HideWaitProgress(); }
    });

    OnLoadCreateVailidatePassengerDetailsDynamically();
    BindDisplayResutModuleDataNew();
});


var timeDiff = function (date1, date2) {
    var a = new Date(date1).getTime(),
        b = new Date(date2).getTime(),
        diff = {};
    diff.milliseconds = a > b ? a % b : b % a;
    diff.seconds = diff.milliseconds / 1000;
    diff.minutes = diff.seconds / 60;
    diff.hours = diff.minutes / 60;
    diff.days = diff.hours / 24;
    diff.weeks = diff.days / 7;
    return diff;
}

function Airline_Name(airline_code) {
    var currentdate = '';
    $.each(SolutionDataTraveler("GET", "AirLineList"), function (dt_key, dt_value) {
        if (dt_value.airlinecode == airline_code) {
            currentdate = dt_value.airlinename;
        }
    });
    return currentdate;
}
function AirPort_Name(airPort_code) {
    var currentdate = '';
    $.each(SolutionDataTraveler("GET", "AirPortNameCodeCoList"), function (dt_key, dt_value) {
        if (dt_value.airlinecode == airPort_code) {
            currentdate = dt_value.airlinename;
        }
    });
    return currentdate;
}
function GetAirline_Code(airline_code) {
    if ((airline_code.lastIndexOf('(') > 0) && (airline_code.lastIndexOf(')') > 0)) {
        return (airline_code.substr(airline_code.lastIndexOf('('), airline_code.lastIndexOf(')')).replace('(', '').replace(')', ''));
    }
    else { return airline_code; }
}

function OnLoadCreateVailidatePassengerDetailsDynamically() {
    try {

        myPassengerDetailsListArray = SolutionDataTraveler("GET", "GetPassengerDetailsList");
        var AppPassengerDetailsData = '';
        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
            if (parseInt(dt_value.Adults) > 0) {
                AppPassengerDetailsData += "<div class='row'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Adults</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                AppPassengerDetailsData += "<div class='row table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: -1px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='SelTDTitleAdults" + i + "' style='width:100%; height:27px; '><option>Mr.</option><option>Ms.</option><option>Miss.</option><option>Mstr.</option><option>Dr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtFirstNameAdults" + i + "' name='txtFirstNameAdults" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtMiddleNameAdults" + i + "' name='txtMiddleNameAdults" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtLastNameAdults" + i + "' name='txtLastNameAdults" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateAdults" + i + "' type='text' name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px'  /><span class='input-group-addon' id='col_txtDOBDateAdults" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderAdults" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.Childrens) > 0) {
                AppPassengerDetailsData += "<div class='row'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Childrens</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {
                AppPassengerDetailsData += "<div class='row table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: -1px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='SelTDTitleChildrens" + i + "' style='width:100%; height:27px; '><option>Mast.</option><option>Miss.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtFirstNameChildrens" + i + "' name='txtFirstNameChildrens" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtMiddleNameChildrens" + i + "' name='txtMiddleNameChildrens" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtLastNameChildrens" + i + "' name='txtLastNameChildrens" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateChildrens" + i + "' type='text' name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px'  /><span class='input-group-addon' id='col_txtDOBDateChildrens" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderChildrens" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.Infants) > 0) {
                AppPassengerDetailsData += "<div class='row'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Infants</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                AppPassengerDetailsData += "<div class='row table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: -1px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='SelTDTitleInfants" + i + "' style='width:100%; height:27px; '><option>Inf.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtFirstNameInfants" + i + "' name='txtFirstNameInfants" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtMiddleNameInfants" + i + "' name='txtMiddleNameInfants" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtLastNameInfants" + i + "' name='txtLastNameInfants" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateInfants" + i + "' type='text' name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px'  /><span class='input-group-addon' id='col_txtDOBDateInfants" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderInfants" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.OnSeat) > 0) {
                AppPassengerDetailsData += "<div class='row'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>OnSeat</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.OnSeat) ; i++) {
                AppPassengerDetailsData += "<div class='row table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: -1px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='SelTDTitleOnSeat" + i + "' style='width:100%; height:27px; '><option>Mr.</option><option>Mrs.</option><option>Dr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtFirstNameOnSeat" + i + "' name='txtFirstNameOnSeat" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtMiddleNameOnSeat" + i + "' name='txtMiddleNameOnSeat" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtLastNameOnSeat" + i + "' name='txtLastNameOnSeat" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateOnSeat" + i + "' type='text' name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px'  /><span class='input-group-addon' id='col_txtDOBDateOnSeat" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderOnSeat" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.Seniors) > 0) {
                AppPassengerDetailsData += "<div class='row'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Seniors</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Seniors) ; i++) {
                AppPassengerDetailsData += "<div class='row table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: -1px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='SelTDTitleSeniors" + i + "' style='width:100%; height:27px; '><option>Mr.</option><option>Mrs.</option><option>Dr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtFirstNameSeniors" + i + "' name='txtFirstNameSeniors" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtMiddleNameSeniors" + i + "' name='txtMiddleNameSeniors" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' id='txtLastNameSeniors" + i + "' name='txtLastNameSeniors" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateSeniors" + i + "' type='text' name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px'  /><span class='input-group-addon' id='col_txtDOBDateSeniors" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderSeniors" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
        });
        $("#BookingPassangerDetails").empty().append(AppPassengerDetailsData);

        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
            for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                $("#txtDOBDateAdults" + i + "").datepicker({
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    todayHighlight: true
                });
                $("#col_txtDOBDateAdults" + i + "").click(function () {
                    $("#txtDOBDateAdults" + i + "").datepicker("show");
                });
            }
            for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {
                $("#txtDOBDateChildrens" + i + "").datepicker({
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    todayHighlight: true
                });
                $("#col_txtDOBDateChildrens" + i + "").click(function () {
                    $("#txtDOBDateChildrens" + i + "").datepicker("show");
                });
            }
            for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                $("#txtDOBDateInfants" + i + "").datepicker({
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    todayHighlight: true
                });
                $("#col_txtDOBDateInfants" + i + "").click(function () {
                    $("#txtDOBDateInfants" + i + "").datepicker("show");
                });
            }
            for (var i = 0; i < parseInt(dt_value.OnSeat) ; i++) {
                $("#txtDOBDateOnSeat" + i + "").datepicker({
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    todayHighlight: true
                });
                $("#col_txtDOBDateOnSeat" + i + "").click(function () {
                    $("#txtDOBDateOnSeat" + i + "").datepicker("show");
                });
            }
            for (var i = 0; i < parseInt(dt_value.Seniors) ; i++) {
                $("#txtDOBDateSeniors" + i + "").datepicker({
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    todayHighlight: true
                });
                $("#col_txtDOBDateSeniors" + i + "").click(function () {
                    $("#txtDOBDateSeniors" + i + "").datepicker("show");
                });
            }
        });
    }
    catch (e) { HideWaitProgress(); var ex = e; }

}

function BindDisplayResutModuleDataNew() {
    try {
        var SelectedAirLineAllFlightSegment_Data = SolutionDataTraveler("GET", "SelectedAirLineBokingPayment");
        if (SelectedAirLineAllFlightSegment_Data != 'undefined' && SelectedAirLineAllFlightSegment_Data != null) {
            var dynamicDetails = "";
            if (SelectedAirLineAllFlightSegment_Data.length > 0) {
                ////////////////////////////////FlightSegments////////////////////////////////////////////////////////////////////////////////////////
                $.each(SelectedAirLineAllFlightSegment_Data, function (All_key, All_value) {
                    var Flight_Segment_ID = All_value.FlightSegmentID;
                    var Allkey = All_key, FlightNumberStop = '';
                    var Hour = 0, Hours = 0, Minute = 0, Minutes = 0, StopoverHours = 0, StopoverMinutes = 0, FinalMinutes = 0;
                    var NoOfStops = 0, cntStops = 1;
                    dynamicDetails += "<div class='table-responsive'>";
                    dynamicDetails += "<table class='table without_tab'><thead><tr>";
                    dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong> " + (Allkey + 1) + " :     " + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount + " + " + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "$" + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount + "</strong><br> <small class='gray'>Final Total Price(incl fee)</small> </div>";
                    //dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong> " + (Allkey + 1) + " :     " + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.BaseFare.CurrencyCode + "$" + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount + " + " + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "$" + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + All_value.FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount + "</strong><br> <small class='gray'>Final Total Price(incl fee)</small> </div>";
                    //dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong> " + (Allkey + 1) + " :     " + All_value.FlightSegments[1].AirItineraryPricingInfo.ItinTotalFare.BaseFare.CurrencyCode + "$" + All_value.FlightSegments[1].AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount + " + " + All_value.FlightSegments[1].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "$" + All_value.FlightSegments[1].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + All_value.FlightSegments[1].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + All_value.FlightSegments[1].AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount + "</strong><br> <small class='gray'>Final Total Price(incl fee)</small> </div>";
                    dynamicDetails += "<div class='col-md-3 rt-align'><button class='btn btn-warning myAirCartSelector btn btn-sm btn-round'  value='btn-sm' type='button' data-param='" + All_value.FlightSegmentID + "'>Select Book Now <i class='fa fa-plane' aria-hidden='true'></i></button>";
                    dynamicDetails += "</div></th></tr></thead>";
                    dynamicDetails += "<tbody class='pink-bg'>";
                    dynamicDetails += " <tr class='txt-yellow'>";
                    dynamicDetails += " <td colspan='5'><i class='fa fa-check' aria-hidden='true'></i> Best Price Guarantee! </td>";
                    dynamicDetails += "</tr>";
                    FlightNumberStop = (All_value.FlightSegments.length == 1) ? "NonStop" : ((All_value.FlightSegments.length == 2) ? "1 Stop" : "2+ Stop");
                    //userIsYoungerThan18 ? "Minor" : "Adult"

                    var flights_StopsTime_List = new Array();
                    var FlitArrivalDateTimeforStopColculation;
                    $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {

                        if (typeof (FlitArrivalDateTimeforStopColculation) != "undefined") {
                            flights_StopsTime_List.push({ StopTime: (timeDiff(FlitArrivalDateTimeforStopColculation, Segment_value.DepartureDateTime)) });
                            var StopoverHour = (Math.floor(Math.abs(timeDiff(FlitArrivalDateTimeforStopColculation, Segment_value.DepartureDateTime).minutes) / 60));
                            var StopoverMinute = (Math.abs(timeDiff(FlitArrivalDateTimeforStopColculation, Segment_value.DepartureDateTime).minutes) % 60);
                            StopoverHours = (parseInt(StopoverHours) + parseInt(StopoverHour));
                            StopoverMinutes = (parseInt(StopoverMinutes) + parseInt(StopoverMinute));
                            dynamicDetails += "<tr>";
                            //dynamicDetails += "<td class='rt-align' colspan='5'>Flight Stopover : <strong>" + timeDiff(FlitArrivalDateTimeforStopColculation, Segment_value.DepartureDateTime).hours.toPrecision(3) + " Hours </strong></td>";
                            dynamicDetails += "<td class='rt-align' colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Flight Layover : <strong>" + StopoverHour + " Hours " + StopoverMinute + " Minute </strong></td>";
                            dynamicDetails += "</tr>";
                        }
                        FlitArrivalDateTimeforStopColculation = Segment_value.ArrivalDateTime;

                        //Hour = String(timeDiff(Segment_value.DepartureDateTime, Segment_value.ArrivalDateTime).hours).split('.')[0];
                        //Minute = String(timeDiff(Segment_value.DepartureDateTime, Segment_value.ArrivalDateTime).hours).split('.')[1];
                        Hour = (Math.floor(Math.abs(timeDiff(Segment_value.DepartureDateTime, Segment_value.ArrivalDateTime).minutes) / 60));
                        Minute = (Math.abs(timeDiff(Segment_value.DepartureDateTime, Segment_value.ArrivalDateTime).minutes) % 60);
                        Hours = (parseInt(Hours) + parseInt(Hour));
                        Minutes = (parseInt(Minutes) + parseInt(Minute));
                        dynamicDetails += "<tr>";
                        dynamicDetails += "<td><img src='../Content/Images/Airlines_Logo/" + Segment_value.OperatingAirline.Code + ".gif'  alt=''></td>";
                        dynamicDetails += "<td><strong>" + Airline_Name(Segment_value.OperatingAirline.Code) + "</strong><br>  Flight # <strong>" + Segment_value.OperatingAirline.Code + "-" + Segment_value.OperatingAirline.FlightNumber + "</strong></td>";
                        dynamicDetails += "<td><strong>" + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.DepartureDateTime)) + "<br> " + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.ArrivalDateTime)) + "</strong></td>";
                        dynamicDetails += "<td><strong>" + AirPort_Name(Segment_value.DepartureAirport.LocationCode) + "<br> " + AirPort_Name(Segment_value.ArrivalAirport.LocationCode) + "</strong></td>";
                        dynamicDetails += "<td class='rt-align'><strong>" + FlightNumberStop + "</strong> <br> Coach / " + Hour + " Hours  <br>" + Minute + " Minutes</td>";
                        dynamicDetails += "</tr>";
                        //dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Layover: " + Hour + "H " + Minute + "M </td></tr>";
                        if (cntStops < NoOfStops) {
                            dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Layover: " + Hour + "H " + Minute + "M </td></tr>";
                        }
                        dynamicDetails += "</tbody>";
                        cntStops = cntStops + 1;
                    });
                    //FinalMinutes = ((Hours * 60) + Minutes);
                    FinalMinutes = (((Hours * 60) + Minutes) + ((StopoverHours * 60) + StopoverMinutes));
                    FinalHours = (FinalMinutes / 60);
                    dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + (Math.floor(Math.abs(FinalMinutes) / 60)) + "h " + (Math.abs(FinalMinutes) % 60) + "m (Flight: " + (Math.floor(Math.abs(FinalMinutes) / 60)) + "h " + (Math.abs(FinalMinutes) % 60) + "m)</td></tr>";
                    dynamicDetails += "</table>";
                    dynamicDetails += "</div>";
                });
                $("#Search-FlightSegments-Result-Panel").empty().append(dynamicDetails);
                ////////////////////////////////FlightSegments////////////////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////FlightTaxes///////////////////////////////////////////////////////////////////////////////////////////
                dynamicDetails = "";
                $.each(SelectedAirLineAllFlightSegment_Data, function (All_key, All_value) {
                    var Flight_Segment_ID = All_value.FlightSegmentID;
                    var FlightNumberStop = '', AirItinerary = 0;
                    dynamicDetails += "<div class='table-responsive'>";
                    dynamicDetails += "<table class='table without_tab'><thead><tr>";
                    dynamicDetails += "<th colspan='5'> <div class='col-md-9'>All Taxes and Total Price Details</div>";
                    dynamicDetails += "</div></th></tr></thead>";
                    dynamicDetails += "<tbody class='pink-bg text-left'>";
                    FlightNumberStop = (All_value.FlightSegments.length == 1) ? "NonStop" : ((All_value.FlightSegments.length == 2) ? "1 Stop" : "2+ Stop");
                    $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                        if (AirItinerary == 0) {
                            AirItinerary = (AirItinerary + 1);
                            var AirItineraryPricingInformation = Segment_value.AirItineraryPricingInfo;
                            dynamicDetails += "<tr><td>&nbsp;</td><td>1 Adult</td><td>&nbsp;</td></tr>";
                            //dynamicDetails += "<tr><td>Air Transportation Charges:</td><td>" + AirItineraryPricingInformation.ItinTotalFare.BaseFare.Amount + " " + AirItineraryPricingInformation.ItinTotalFare.BaseFare.CurrencyCode + "</td><td>&nbsp;</td></tr>";
                            dynamicDetails += "<tr><td>Air Transportation Charges:</td><td>" + AirItineraryPricingInformation.ItinTotalFare.BaseFare.Amount + " " + AirItineraryPricingInformation.ItinTotalFare.TotalFare.CurrencyCode + "</td><td>&nbsp;</td></tr>";
                            dynamicDetails += "<tr><td>Taxes &amp; Fees:</td><td>" + AirItineraryPricingInformation.ItinTotalFare.Taxes.Tax[0].Amount + " " + AirItineraryPricingInformation.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "</td><td>&nbsp;</td></tr>";
                            dynamicDetails += "<tr><td>Trip Protection Insurance: </td><td>Declined</td><td>&nbsp;</td></tr>";
                            dynamicDetails += "<tr><td>Total Price:</td><td>&nbsp;</td><td>" + AirItineraryPricingInformation.ItinTotalFare.TotalFare.Amount + " " + AirItineraryPricingInformation.ItinTotalFare.TotalFare.CurrencyCode + "</td></tr>";
                            dynamicDetails += "<tr><td colspan='3'>Please note: All fares are quoted in CAD. Some airlines may charge baggage fees.</td></tr>";
                        }
                    });
                    dynamicDetails += "</tbody>";
                    dynamicDetails += "</table>";
                    dynamicDetails += "</div>";
                });
                $("#Search-FlightTaxes-Result-Panel").empty().append(dynamicDetails);
                ////////////////////////////////FlightTaxes///////////////////////////////////////////////////////////////////////////////////////////
            }
        }
    }
    catch (e) {
        var error = e;
        error = e;
        HideWaitProgress();
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////
