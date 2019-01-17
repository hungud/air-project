var SelectedAirLineBokingPayment_Data = '';
var myPassengerDetailsListArray = new Array();
var FilterAllFlightSegment_Array = new Array();
var TotalAmount = '';
var isFlight_AI = false;
var isFlight_PK = false;
var PaymentMethod = "";
var CreditCardNumber = 0;
var CreditCardName = "";
var CreditCardExpDate = "";
var CVV = 0;
var UserLocation = "";
var UserStreetAddress = "";
var UserZip = 0;
var UserPhone = 0;
var UserEmail = "";
var CCFEE = 0.0;
var PublishFareMessage = "";
//****************************** Booking Payment start*************************//

Math.trunc = Math.trunc || function (x) {
    if (isNaN(x)) {
        return NaN;
    }
    if (x > 0) {
        return Math.floor(x);
    }
    return Math.ceil(x);
};

$(document).on('change', '#selPICCType', function () {
    var selValue = $(this).find(":selected").val();
    //alert("selected value-" + selValue);

    if (selValue != "CQ") {
        $('.stat_col_payment').show();
        $('.cc-section').show();
    }
    else {
        $('.stat_col_payment').hide();
        $('.cc-section').hide();
    }

    var cardFee = 0.0;

    if ((isFlight_AI == true || isFlight_PK == true) && selValue == 'MX') {
        cardFee = fpercent(TotalAmount, 4.5);
    }

    if (isFlight_AI == true && (selValue == 'VI' || selValue == 'MC')) {
        cardFee = fpercent(TotalAmount, 3.0);
    }
    //alert('card fee-' + cardFee);
    $('#lblCCFee').text(cardFee.toFixed(2));
    if (cardFee > 0) {
        $('#msgCCFee').html("<span style='color:red;'> (Separate charge of Credit Card fee will appear on the credit card)</span> ");
    }
});

function GetUserContactDetails() {
    debugger;
    /* Check Company Type */
    var servicepath = "http://search.nanojot.com/searchboxwebservice.asmx/GetContactDetailsByUserID";
    var UserID = localStorage.getItem("LoggedinUID");
    $.ajax({
        cache: false,
        url: servicepath,
        data: ({ UserID: parseInt(UserID) }),
        type: "POST",
        async: false,
        cors: true,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "xml",
        success: function (xml) {

            console.log(xml);

            $(xml).find('ContactDetail').each(function () {

                PaymentMethod = $(this).find('PaymentMethod').text();
                CreditCardNumber = $(this).find('CreditCardNumber').text();
                CreditCardName = $(this).find('CreditCardName').text();
                CreditCardExpDate = $(this).find('CreditCardExpDate').text();
                var Exp_arr = "";
                var Exp_m = "";
                var Exp_y = "";
                if (CreditCardExpDate.length > 0) {
                    Exp_arr = CreditCardExpDate.split('-');
                    Exp_m = Exp_arr[0];
                    Exp_y = Exp_arr[1];
                }


                CVV = $(this).find('CVV').text();
                UserLocation = $(this).find('UserLocation').text();
                UserStreetAddress = $(this).find('UserStreetAddress').text();
                UserZip = $(this).find('UserZip').text();
                UserPhone = $(this).find('UserPhone').text();
                UserEmail = $(this).find('UserEmail').text();


                $('#selPICCType').val(PaymentMethod);




                $('#txtPICCNumber').val(CreditCardNumber);
                $('#txtPICCHName').val(CreditCardName);
                $('#selectPICCMonth').val(Exp_m);
                $('#selectPICCYear').val(Exp_y);
                $('#txtPIVCNumber').val(CVV);
                $('#selBCICountry').val(UserLocation);
                $('#txtBCICity').val(UserStreetAddress);
                $('#txtBCIZip').val(UserZip);
                $('#txtBCIBillPhone').val(UserPhone);
                $('#txtBCIEmail').val(UserEmail);


            });
        },
        error: function (response) {
            // alert(response.responseText);
        },
        failure: function (response) {
            //  alert(response.responseText);
        }
    })

    /* Check Company Type */


}



function GetOriginDestination() {
    try {
        function ResultCallBackSuccess(e, xhr, opts) {
            HideWaitProgress();
            debugger;
            var App_Data = JSON.parse(e);
            sessionStorage.setItem("Origin", App_Data.origin);
            sessionStorage.setItem("Destination", App_Data.destination);
            sessionStorage.setItem("SelectionType", App_Data.SelectionName);

            var SDeparture = sessionStorage.getItem("Origin");
            var SArrival = sessionStorage.getItem("Destination");
            var SelectionType = sessionStorage.getItem("SelectionType");

            var Departure_arr = SDeparture.split(";");
            var Arrival_arr = SArrival.split(";");

            var dest_array = new Array();
            var arrival_array = new Array();
            Departure_arr.forEach(function (entry) {
                if (entry.length > 0) {
                    var dest = entry;
                    var start = dest.indexOf("[");
                    var end = dest.indexOf("]");
                    var dest = dest.substring(start + 1, end);
                    dest_array.push(dest);
                }
            });
            Arrival_arr.forEach(function (entry) {
                if (entry.length > 0) {
                    var dest = entry;
                    var start = dest.indexOf("[");
                    var end = dest.indexOf("]");
                    var dest = dest.substring(start + 1, end);
                    arrival_array.push(dest);
                }
            });
            sessionStorage.setItem("departarray", JSON.stringify(dest_array));
            sessionStorage.setItem("arrivalarray", JSON.stringify(arrival_array));
            //SolutionDataTraveler("SET", "BFMXSearchResult", App_Data);
            //console.log("----------------Search Result Data--------------");
            //console.log(App_Data);
            //console.log("----------------Search Result Data--------------");
            //FilteringBindDisplayResutData(App_Data.Result);
            //FilteringBindDisplayResutData(App_Data.Data.Result);

        }
        function ResultCallBackError(e, xhr, opts) {
            //debugger;
            HideWaitProgress();
        }
        var Reqst_Resource = {
            folderName: localStorage.getItem("RequestID"),
            SearchText: "QuoteRQ.log"
        };
        MasterAppConfigurationsServices("GET", CommonConfiguration.WebAPIServicesURL + "API/CommonService/GetQuoteRQRSService", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
        HideWaitProgress();
    } catch (e) {
        var ex = e;
    }
}

function calculateAge(birthday) { // birthday is a date
    debugger;
    var dob = new Date(birthday);
    //var today = new Date();
    var today = new Date(sessionStorage.getItem("LastDepartureDate"));
    var age = (today - dob) / (365.25 * 24 * 60 * 60 * 1000);
    return age.toFixed(5);
}

$(function ($) {

    //Create Local Storage Item For Payment Page Entry
    sessionStorage.setItem("IsFromPaymentPage", "True");
    var CompanyType = sessionStorage.getItem("CompanyTypeId");


    GetOriginDestination();


    if (CompanyType == "1") {
        $('.stat_col_payment').hide();
        $('.cc-section').hide();
    }

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

    } catch (e) { var ex = e; }
});

$(document).ready(function (e) {

    var monthShortValues = [{ "Key": "Jan (01)", "Value": "01", }, { "Key": "Feb (02)", "Value": "02", }, { "Key": "Mar (03)", "Value": "03", }, { "Key": "Apr (04)", "Value": "04", }, { "Key": "May (05)", "Value": "05", }, { "Key": "Jun (06)", "Value": "06", }, { "Key": "Jul (07)", "Value": "07", }, { "Key": "Sep (08)", "Value": "08", }, { "Key": "Sep (09)", "Value": "09", }, { "Key": "Oct (10)", "Value": "10", }, { "Key": "Nov (11)", "Value": "11", }, { "Key": "Dec (12)", "Value": "12", }];
    $.each(monthShortValues, function (key, value) {
        var monthdata = value;
        $(".selectPICCMonth").append("<option value=\"" + monthdata.Value + "\">" + monthdata.Key + "</option>");
    });

    $('.selectPICCYear').yearselect({ order: 'asc' });

});


function filterTime(str) {

    var arr = str.split(':');
    return ("0" + arr[0]).slice(-2) + ":" + ("0" + arr[1]).slice(-2);
    //// ("0" + myNumber).slice(-2);
}


function ValidateData() {
    var re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
    try {
        if (ValidateTravelerDetails()) {
            //======================Payment Info (Secure Payment Transaction)==================
            var CompanyType = sessionStorage.getItem("CompanyTypeId");
            if ($("#selPICCType").val().trim() == "Select a Payment Card") {
                ConfirmBootBox("Successfully", "Please Enter Payment Method.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selPICCType").focus();
                return false;
            }
            if ($("#selPICCType").val().trim() != "CQ") {

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
                else {
                    var Input = $("#txtPICCHName").val().trim();
                    var isSplChar = re.test(Input);
                    if (isSplChar) {
                        ConfirmBootBox("Successfully", "Card Holder Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtPICCHName").focus();
                        return false;
                    }
                }
                if (!ValidateTravelerPassangerAndCreaditCardHolder()) {
                    ConfirmBootBox("Successfully", "Card Holder Name Should be one of the Adult Passanger Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtPICCHName").focus();
                    return false;
                }
            }
            var val_len = $('#selBCICountry').val().split(',').length;
            if (val_len != 3) {
                ConfirmBootBox("Successfully", "Please Select Valid City.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selBCICountry").focus();
                return false;
            }

            //======================Payment Info (Secure Payment Transaction)==================


            //======================Billing & Contact Information===============================
            //if ($("#selBCICountry").val().trim() == "") {
            //    ConfirmBootBox("Successfully", "Please Enter Country.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#selBCICountry").focus();
            //    return false;
            //}
            //if ($("#txtBCICity").val().trim() == "") {
            //    ConfirmBootBox("Successfully", "Please Enter Street.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtBCICity").focus();
            //    return false;
            //}
            //if ($("#txtBCIStreet").val().trim() == "") {
            //    ConfirmBootBox("Successfully", "Please Enter Street.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtBCIStreet").focus();
            //    return false;
            //}
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
            //if ($("#txtBCIConfEmail").val().trim() == "") {
            //    ConfirmBootBox("Successfully", "Please Enter  E-Mail ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtBCIConfEmail").focus();
            //    return false;
            //}
            //if (!ValidateEmail($("#txtBCIConfEmail").val().trim())) {
            //    ConfirmBootBox("Successfully", "Please Enter Vailid E-Mail ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtBCIConfEmail").focus();
            //    return false;
            //}
            //if ($("#txtBCIConfEmail").val().trim() != $("#txtBCIEmail").val().trim()) {
            //    ConfirmBootBox("Successfully", "Please Enter  E-Mail not matched ID.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            //    $("#txtBCIConfEmail").focus();
            //    return false;
            //}
            //======================Billing & Contact Information===============================

            //======================Terms & COndition Contact Information===============================
            var Insurance_option = sessionStorage.getItem("Insurance");
            if (Insurance_option == "False") {
                if (!$("input[name='radioNameTravelIns']:checked").val()) {
                    ConfirmBootBox("Successfully", "Please Select Travel Protection Option.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#radioNameTravelIns").focus();
                    return false;

                }
            }
            else {
                if (!$("input[name='radioNameTravelIns1']:checked").val()) {
                    ConfirmBootBox("Successfully", "Please Select Travel Insurance Option.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#radioNameTravelIns").focus();
                    return false;

                }
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

function ValidateInsuranceData() {
    try {
        if (ValidateTravelerDetails()) {
            //======================Payment Info (Secure Payment Transaction)==================
            var CompanyType = sessionStorage.getItem("CompanyTypeId");
            if ($("#selPICCType").val().trim() == "Select a Payment Card") {
                ConfirmBootBox("Successfully", "Please Enter Payment Method.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selPICCType").focus();
                return false;
            }
            if ($("#selPICCType").val().trim() != "CQ") {

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
            }
            var val_len = $('#selBCICountry').val().split(',').length;
            if (val_len != 3) {
                ConfirmBootBox("Successfully", "Please Select Valid City.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                $("#selBCICountry").focus();
                return false;
            }

            //======================Payment Info (Secure Payment Transaction)==================


            //======================Billing & Contact Information===============================

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
            return true;
        }
    }
    catch (Ex) {
    }
}

function ValidateTravelerDetails() {
    var ValidationStatus = true;
    var re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
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
                    else {
                        var Input = $("#txtFirstNameAdults" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Adult First Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtFirstNameAdults" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtMiddleNameAdults" + i + "").val().trim() == "") {
                        //    ConfirmBootBox("Successfully", "Please Enter Adult Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        //    $("#txtMiddleNameAdults" + i + "").focus();
                        //    return ValidationStatus = false;
                    }
                    else {
                        var Input = $("#txtMiddleNameAdults" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Adult Middle Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtMiddleNameAdults" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }

                    if ($("#txtLastNameAdults" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Adult Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameAdults" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    else {
                        var Input = $("#txtLastNameAdults" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Adult Last Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtLastNameAdults" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtDOBDateAdults" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Adult DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateAdults" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    else {
                        var birth = new Date($("#txtDOBDateAdults" + i + "").val());

                        var Age = calculateAge(birth);
                        if (Age < 2) {
                            ConfirmBootBox("Successfully", "According to Date Of Birth passenger is not Adult please select passenger type Infant and search again.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtDOBDateInfants" + i + "").focus();
                            return ValidationStatus = false;
                        }
                        else if (Age < 12) {
                            ConfirmBootBox("Successfully", "According to Date Of Birth passenger is not Adult please select passenger type Children and search again.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtDOBDateInfants" + i + "").focus();
                            return ValidationStatus = false;
                        }
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
                    else {
                        var Input = $("#txtFirstNameChildrens" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Children First Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtFirstNameChildrens" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtMiddleNameChildrens" + i + "").val().trim() == "") {
                        //    ConfirmBootBox("Successfully", "Please Enter Children Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        //    $("#txtMiddleNameChildrens" + i + "").focus();
                        //    return ValidationStatus = false;
                    }
                    else {
                        var Input = $("#txtMiddleNameChildrens" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Children Middle Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtMiddleNameChildrens" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtLastNameChildrens" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Children Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameChildrens" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    else {
                        var Input = $("#txtLastNameChildrens" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Children Last Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtLastNameChildrens" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtDOBDateChildrens" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Children DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateChildrens" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    else {
                        var birth = new Date($("#txtDOBDateChildrens" + i + "").val());

                        var Age = calculateAge(birth);
                        if (Age < 2) {
                            ConfirmBootBox("Successfully", "According to Date Of Birth passenger is no more Child please select passenger type Infant and search again.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtDOBDateChildrens" + i + "").focus();
                            return ValidationStatus = false;
                        }
                        else if (Age > 12) {
                            ConfirmBootBox("Successfully", "According to Date Of Birth passenger is no more Child please select passenger type Adult and search again.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtDOBDateChildrens" + i + "").focus();
                            return ValidationStatus = false;
                        }
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
                    else {
                        var Input = $("#txtFirstNameInfants" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Infant First Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtFirstNameInfants" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtMiddleNameInfants" + i + "").val().trim() == "") {
                        //    ConfirmBootBox("Successfully", "Please Enter Infants Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        //    $("#txtMiddleNameInfants" + i + "").focus();
                        //    return ValidationStatus = false;
                    }
                    else {
                        var Input = $("#txtMiddleNameInfants" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Infant Middle Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtMiddleNameInfants" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtLastNameInfants" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Infants Last Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtLastNameInfants" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    else {
                        var Input = $("#txtLastNameInfants" + i + "").val().trim();
                        var isSplChar = re.test(Input);
                        if (isSplChar) {
                            ConfirmBootBox("Successfully", "Infant Last Name Cannot Have Special Characters or Symbols", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtLastNameInfants" + i + "").focus();
                            return ValidationStatus = false;
                        }
                    }
                    if ($("#txtDOBDateInfants" + i + "").val().trim() == "") {
                        ConfirmBootBox("Successfully", "Please Enter Infants DOB.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                        $("#txtDOBDateInfants" + i + "").focus();
                        return ValidationStatus = false;
                    }
                    else {
                        var birth = new Date($("#txtDOBDateInfants" + i + "").val());

                        var Age = calculateAge(birth);
                        if (Age > 2) {
                            ConfirmBootBox("Successfully", "According to Date Of Birth passenger is no more Infant please select passenger type Child and search again.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                            $("#txtDOBDateInfants" + i + "").focus();
                            return ValidationStatus = false;
                        }
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
                    //if ($("#txtMiddleNameOnSeat" + i + "").val().trim() == "") {
                    //    ConfirmBootBox("Successfully", "Please Enter OnSeat Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    //    $("#txtMiddleNameOnSeat" + i + "").focus();
                    //    return ValidationStatus = false;
                    //}
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
                    //if ($("#txtMiddleNameSeniors" + i + "").val().trim() == "") {
                    //    ConfirmBootBox("Successfully", "Please Enter Seniors Middle Name.", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    //    $("#txtMiddleNameSeniors" + i + "").focus();
                    //    return ValidationStatus = false;
                    //}
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

function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    var x = [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-');
    return x;
}


function convertDateInfant(inputFormat) {

    var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");

    var d = new Date(inputFormat);
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear().toString().substr(-2);
    //document.write(curr_date + "-" + m_names[curr_month]
    //+ "-" + curr_year);
    var x = curr_date + m_names[curr_month] + curr_year;
    return x;
}
function ValidateTravelerPassangerAndCreaditCardHolder() {
    var ValidationStatus = true;
    try {
        //======================Traveler Details==================
        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
            if (parseInt(dt_value.Adults) > 0) {
                for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                    //var PassangerName = $("#txtFirstNameAdults" + i + "").val().trim() + $("#txtMiddleNameAdults" + i + "").val().trim() + $("#txtLastNameAdults" + i + "").val().trim();
                    var PassangerName = $("#txtFirstNameAdults" + i + "").val().trim() + $("#txtMiddleNameAdults" + i + "").val().trim() + $("#txtLastNameAdults" + i + "").val().trim();
                    var CardHolderName = $("#txtPICCHName").val().trim().split(" ").join("");
                    if (PassangerName.toLowerCase() != CardHolderName.toLowerCase()) {
                        ValidationStatus = false;
                    }
                    else {
                        ValidationStatus = true;
                        return ValidationStatus;
                    }
                }
            }

        });
        return ValidationStatus;
        //======================Traveler Details==================
    }
    catch (Ex) {
    }
    return ValidationStatus;
}

function tConvert(time) {
    // Check correct time format and split into components
    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
        time = time.slice(1);  // Remove full string match value
        time[5] = +time[0] < 12 ? 'AM' : 'PM'; // Set AM/PM
        time[0] = +time[0] % 12 || 12; // Adjust hours
    }
    return time.join(''); // return adjusted time or original string
}

function LoadAirReservationResultData(ResultData) {
    try {
        if (ResultData.Message == "Success" && ResultData.Status == true) {

            var PurchasedDate = sessionStorage.getItem("PurchasedDate");
            var Price = sessionStorage.getItem("Price");
            var PolicyNumber = sessionStorage.getItem("PolicyNumber");

            //Remove Session Storage That shows i came from payment page as search is successfull
            sessionStorage.removeItem("IsFromPaymentPage");

            //console.log("--------------------Dt-----------------");
            //console.log(ResultData);
            //payment successful
            SelectedAirLineBokingPayment_Data = SolutionDataTraveler("GET", "SelectedAirLineBokingPayment")[0];
            var Reqst_flightsDetailsList_Resource = SolutionDataTraveler("GET", "AirReservationBookingRequest").flightsDetailsList[0];
            var Reqst_passengersDetail_Resource = SolutionDataTraveler("GET", "AirReservationBookingRequest").passengersDetailsList[0];
            var Reqst_CCD_Resource = SolutionDataTraveler("GET", "AirReservationBookingRequest").creditCardOtherDetails[0];

            $(".AirBokingConfirmation-Panel").removeClass("hidden");
            $(".AirBokingPayment-Panel").addClass("hidden");


            //console.log("--------------------Dr-----------------");
            //console.log(SelectedAirLineBokingPayment_Data);
            //console.log("--------------------Dt-----------------");
            //console.log(ResultData.Data);

            //console.log("-------------------------------------");
            //console.log(Reqst_passengersDetail_Resource);
            //console.log("-------------------------------------");
            //console.log(Reqst_flightsDetailsList_Resource);
            //console.log("-------------------------------------");


            var Result_Data = ResultData.Data;
            debugger
            /******************************confirmation_title*************************/
            var confirmation_title = "";
            //dest_array = JSON.parse(sessionStorage.getItem("departarray"));
            //arrival_array = JSON.parse(sessionStorage.getItem("arrivalarray"));
            //var city = 0;
            //var AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
            //var LastDepartureDate = "";
            var flights_len = Result_Data.flights_details.length;
            $.each(Result_Data.flights_details, function (FD_Key, FD_Value) {



                debugger;

                console.log(Result_Data.flights_details);
                console.log("----------------------");
                var SelectionName = localStorage.getItem("SelectionName");
                if (SelectionName == "2") {
                    //Round Trip
                    confirmation_title = "<p style='padding:5px;'>Departure: " + Result_Data.flights_details[0].DepartureDate + "<i class='glyphicon glyphicon-play'></i> Return: " + FD_Value.ArrivalDate + "</p><p style='padding:5px;'>" + localStorage.getItem("Origin") + "<i class='glyphicon glyphicon-play'></i>" + localStorage.getItem("Destination") + "</p>";
                }
                else {
                    confirmation_title = "<p style='padding:5px;'>Departure: " + Result_Data.flights_details[0].DepartureDate + "</p>" + "<p style='padding:5px;'>" + localStorage.getItem("Origin") + "<i class='glyphicon glyphicon-play'></i>" + localStorage.getItem("Destination") + "</p>";
                }

            });
            $("#confirmation_title").empty().append(confirmation_title);
            /******************************Fulter AirLineName*************************/

            /******************************Confirmation_PreparedFor*************************/
            var Confirmation_PreparedFor = "";
            Confirmation_PreparedFor += "<div class='col-md-6 '>";
            Confirmation_PreparedFor += "<p class='prep_for'>PREPARED FOR</p>";
            $.each(Result_Data.passengers_details, function (PD_Key, PD_Value) {
                Confirmation_PreparedFor += " <p>" + PD_Value.passengername + " " + PD_Value.Surname + "</p>";
            });
            Confirmation_PreparedFor += "<p>Booking Confirmation Code  :  " + Result_Data.pnrnumber + "<br />Airline Reservation Code  " + Result_Data.AirLineReservationCode + "</p>";
            Confirmation_PreparedFor += "</div>";
            Confirmation_PreparedFor += "<div class='col-md-6 '>";
            //Confirmation_PreparedFor += " <p style='padding-top:15px;'>SKYFLIGHT TRAVEL CENTRE<br />" + Result_Data.passengers_details[0].passengerAddress + "<br>" + Reqst_passengersDetail_Resource.PhoneNumber + "<br />" + Reqst_passengersDetail_Resource.PassengerEmail + "";
            Confirmation_PreparedFor += " <p style='padding-top:15px;'>" + ResultData.AgencyDetails;
            Confirmation_PreparedFor += " <br /><h5>DATE & TIME OF BOOKING : " + $.formatDateTime('DD dd M yy g:ii a', new Date(new Date().toJSON())) + "</h5></p>";
            Confirmation_PreparedFor += "</div>";

            $("#Confirmation_PreparedFor").empty().append(Confirmation_PreparedFor);
            /******************************Confirmation_PreparedFor*************************/

            /******************************Confirmation FlightDetails AirLineName*************************/
            var Confirmation_FlightDetails = "";
            var FlitArrivalDateTimeforStopColculation = "";
            var count = 1;
            var sequence = "";
            var consumed_minutes = 0;
            var Stop = 0;
            var len = Result_Data.flights_details.length;

            dest_array = JSON.parse(sessionStorage.getItem("departarray"));
            arrival_array = JSON.parse(sessionStorage.getItem("arrivalarray"));
            var CurrentNode = "";
            var SelectionType = sessionStorage.getItem("SelectionType");
            var city = 0;
            var AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
            debugger;
            $.each(Result_Data.flights_details, function (FD_Key, FD_Value) {
                debugger;

                if (FD_Value.DepartureCityCode == null) {
                    return true;
                }
                var Departure = dest_array[city];
                var Arrival = arrival_array[city];
                var ChkDepArray = new Array();
                var ChkArrivArray = new Array();

                //for departure check its for city
                if (AirPortNameCodeCoList != undefined) {
                    $.each(AirPortNameCodeCoList, function (dt_key, dt_value) {
                        var a = dt_value.airportname.indexOf("All Airports");
                        if ((dt_value.airlinecode == Departure) && (dt_value.airportname.indexOf("All Airports") > -1)) {
                            //Airline Code is a city
                            var city = dt_value.city;

                            //Get Current City All Airports
                            $.each(AirPortNameCodeCoList, function (dt_city_key, dt_city_value) {
                                if (dt_city_value.city == city) {
                                    ChkDepArray.push(dt_city_value.iata);
                                }
                            });
                            //Out From Loop

                        }

                    });
                    if (ChkDepArray.length == 0) {
                        ChkDepArray.push(Departure);
                    }
                }

                //for Arrival check its for city
                if (AirPortNameCodeCoList != undefined) {
                    $.each(AirPortNameCodeCoList, function (dt_key, dt_value) {
                        var a = dt_value.airportname.indexOf("All Airports");
                        if ((dt_value.airlinecode == Arrival) && (dt_value.airportname.indexOf("All Airports") > -1)) {

                            //Airline Code is a city
                            var city = dt_value.city;

                            //Get Current City All Airports
                            $.each(AirPortNameCodeCoList, function (dt_city_key, dt_city_value) {
                                if (dt_city_value.city == city) {
                                    ChkArrivArray.push(dt_city_value.iata);
                                }
                            });
                            //Out From Loop
                        }

                    });
                    if (ChkArrivArray.length == 0) {
                        ChkArrivArray.push(Arrival);
                    }
                }

                debugger;
                if ((ChkDepArray.indexOf(FD_Value.DepartureCityCode) == -1) && (ChkArrivArray.indexOf(FD_Value.DepartureCityCode) == -1)) {
                    //Layover
                    var timeDifflapse = "";
                    var ArrivalTime = "";
                    var DepTime = "";
                    var Minute = "";
                    if (FlitArrivalDateTimeforStopColculation != "") {
                        ArrivalTime = FlitArrivalDateTimeforStopColculation;
                        DepTime = FD_Value.DepartureDateTime;
                        timeDifflapse = timeDiff(DepTime, ArrivalTime);
                        Hour = (Math.floor(Math.abs(timeDifflapse.minutes) / 60));
                        Minute = (Math.abs(timeDifflapse.minutes) % 60);

                        consumed_minutes += parseInt(parseInt((Hour * 60)) + parseInt(Minute));

                        Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table without_tab' style='text-align:center; margin-top:15px;'>";
                        Confirmation_FlightDetails += "<tr><td colspan='4'><i class='fa fa-clock-o' aria-hidden='true'></i> Layover: " + Hour + "H " + Minute + "M </td></tr>";
                        Confirmation_FlightDetails += "</table>";

                        Stop += 1;
                    }
                }
                debugger;
                if (ChkArrivArray.indexOf(FD_Value.DepartureCityCode) > -1) {
                    //Total Trip Duration
                    var FlightNumberStop = (Stop == 0) ? "NonStop" : ((Stop == 1) ? "1 Stop" : "2+ Stop");

                    var vflighthours = Math.floor(Math.abs(consumed_minutes) / 60);
                    var vflightminutes = Math.abs(consumed_minutes) % 60;
                    Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table without_tab' style='text-align:center; margin-top:15px;'>";
                    Confirmation_FlightDetails += "<tr><td colspan='4'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + vflighthours + "H " + vflightminutes + "M (" + FlightNumberStop + ")</td></tr>";
                    Confirmation_FlightDetails += "</table>";
                    sequence = FD_Value.Sequence;
                    consumed_minutes = 0;
                    Stop = 0;
                }
                debugger;
                var vSegmentsElapsedTime = FD_Value.ElapsedTime.split('.');
                var vflighthours = vSegmentsElapsedTime[0];
                var vflightminutes = vSegmentsElapsedTime[1];
                //Flight Details
                consumed_minutes += parseInt(parseInt((vflighthours * 60)) + parseInt(vflightminutes));
                FlitArrivalDateTimeforStopColculation = FD_Value.ArrivalDateTime1;
                //FLIGHT DETAILS
                Confirmation_FlightDetails += "<div class='row'><div class='col-md-12'><div class='col-md-12 orng_title'><p>DEPARTURE: " + FD_Value.DepartureDate + " <i class='glyphicon glyphicon-play'></i> ARRIVAL: " + FD_Value.ArrivalDate + "  <i class='fa fa-plane fa-1'></i><br><span>Please verify flight times prior to departure</span></p></div></div></div>";

                Confirmation_FlightDetails += "<div class='row'><div class='col-md-12'><div class='col-md-12 no-padding'>";
                Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table without_tab' style='text-align:inherit; margin-top:15px;'>";

                Confirmation_FlightDetails += "<tr><td rowspan='2'><p><img src='Content/Images/Airlines_Logo/" + FD_Value.MarketingAirline + ".gif'  alt=''></p><p>" + (FD_Value.AirlineName == null ? Airline_Name(FD_Value.MarketingAirline) + "Operated By " + Airline_Name(FD_Value.MarketingAirline) : Airline_Name(FD_Value.MarketingAirline) + "Operated By " + Airline_Name(FD_Value.AirlineName)) + "</p> <p class='fl_heading'><strong> Flight # " + (FD_Value.AirlineName == null ? FD_Value.MarketingAirline : FD_Value.AirlineName) + "  " + FD_Value.FlightNumber.replace(/^0+/, '') + "</strong></p></td>";

                Confirmation_FlightDetails += "<td> " + AirPort_Name(FD_Value.DepartureCityCode) + "</p><p>Departing: " + FD_Value.DepartureDate +
                    " At " + tConvert(FD_Value.DepartureTime) + "</p><p><strong>Terminal:</strong>" +
                    (FD_Value.DepartureTerminal == null ? 'Not Available' : FD_Value.DepartureTerminal.replace("TERMINAL", "")) +
                    "</p></td><td><p>" + AirPort_Name(FD_Value.ArrivalCityCode) + "</p><p>Arriving: " + FD_Value.ArrivalDate + " At " +
                    tConvert(FD_Value.ArrivalTime) + "</p><p><strong>Terminal:</strong>" +
                    (FD_Value.ArrivalTerminal == null ? 'Not Available' : FD_Value.ArrivalTerminal.replace("TERMINAL", "")) +
                    "</p></td><td rowspan='2'><p>Aircraft:" + FD_Value.AircraftType +
                    "</p><p>Duration:" +
                    vflighthours + " H " + vflightminutes + " M" + "</p></td></tr>";
                debugger;
                if ((SelectionType == "1") || (SelectionType == "3")) {
                    //OneWay & Multiway
                    if (ChkArrivArray.indexOf(FD_Value.ArrivalCityCode) > -1) {
                        //Total Trip Duration
                        var FlightNumberStop = (Stop == 0) ? "NonStop" : ((Stop == 1) ? "1 Stop" : "2+ Stop");

                        var vflighthours = Math.floor(Math.abs(consumed_minutes) / 60);
                        var vflightminutes = Math.abs(consumed_minutes) % 60;
                        Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table without_tab' style='text-align:center; margin-top:15px;'>";
                        Confirmation_FlightDetails += "<tr><td colspan='4'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + vflighthours + "H " + vflightminutes + "M (" + FlightNumberStop + ")</td></tr>";
                        Confirmation_FlightDetails += "</table>";
                        sequence = FD_Value.Sequence;
                        consumed_minutes = 0;
                        city += 1;
                    }
                }
                else if (SelectionType == "2") {

                    if (ChkDepArray.indexOf(FD_Value.ArrivalCityCode) > -1) {
                        //Total Trip Duration
                        var FlightNumberStop = (Stop == 0) ? "NonStop" : ((Stop == 1) ? "1 Stop" : "2+ Stop");

                        var vflighthours = Math.floor(Math.abs(consumed_minutes) / 60);
                        var vflightminutes = Math.abs(consumed_minutes) % 60;
                        Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table without_tab' style='text-align:center; margin-top:15px;'>";
                        Confirmation_FlightDetails += "<tr><td colspan='4'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + vflighthours + "H " + vflightminutes + "M (" + FlightNumberStop + ")</td></tr>";
                        Confirmation_FlightDetails += "</table>";
                        sequence = FD_Value.Sequence;
                        consumed_minutes = 0;
                    }
                }

                debugger;

                Confirmation_FlightDetails += "</table></div></div></div>";
                if (FD_Value.status == "false")
                    $("#error_title_title").empty().append("Booking is not yet confirm, SkyFlight Customer service team will get back to you once the booking is confirmed in next 24 hours.");
            });



            Confirmation_FlightDetails += "<div class='row'><div class='col-md-12'>";
            Confirmation_FlightDetails += "<table width='100%' border='0' cellspacing='0' cellpadding='0' class='table table-striped' style='text-align:inherit;'>";
            Confirmation_FlightDetails += "<thead class='thead-inverse'>";
            Confirmation_FlightDetails += "<tr><th>Passenger Name</th><th>Seats</th><th>Class</th><th>Status</th><th>Meals</th></tr></thead>";
            $.each(Result_Data.passengers_details, function (FD_Key, FD_Value) {
                // alert(FD_Value.passengername);
                Confirmation_FlightDetails += " <tr><td>" + FD_Value.passengername + " " + FD_Value.Surname + "</td><td>Check-In Required</td><td>Economy</td><td>Confirmed</td><td>Meals</td></tr>";
            });
            Confirmation_FlightDetails += "</table></div></div>";
            $("#Confirmation_FlightDetails").empty().append(Confirmation_FlightDetails);
            /******************************Confirmation FlightDetails AirLineName*************************/

            /******************************Payment Detail*************************/

            var Confirmation_PaymentDetail = "";
            debugger;
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-12 orng_bg top'><strong>Payment Detail:</strong></div>";
            Confirmation_PaymentDetail += "<div class='col-md-4'>Name On The Card:</div>";
            Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'>" + Reqst_CCD_Resource.NameOnCard + "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-4'>Credit Card Charged:</div>";

            Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'>****-****-****-" + Reqst_CCD_Resource.CardNumber.substr(12, 4); + "</div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";
            // Confirmation_PaymentDetail += "<div class='row'>";

            var MAPublishedFare = parseFloat(sessionStorage.getItem('mapublishedfare'));
            var CPublishedFare = parseFloat(sessionStorage.getItem('cpublishedfare'));
            var Total = 0;
            var CCFEECharges = parseFloat(CCFEE).toFixed(2);

            Total = (parseFloat(Result_Data.currCode_details.PaymentAmount));

            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-4'>Amount Charged:</div>";
            Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'> " + sessionStorage.getItem('CurrencyCode') + " " + Total + "</div>";
            Confirmation_PaymentDetail += "</div>";

            //if (CCFEECharges>0) {
            if (CCFEECharges > 0) {
                Confirmation_PaymentDetail += "<div class='col-md-12'>";
                Confirmation_PaymentDetail += "<div class='col-md-4'>CCFEE:</div>";

                if ((MAPublishedFare > 0) || (CPublishedFare > 0)) {
                    Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'> " + sessionStorage.getItem('CurrencyCode') + " " + CCFEECharges + "<span style='color:red;'> (Separate charge of Credit Card fee will appear on the credit card)</span> </div>";
                }
                else {
                    Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'> " + sessionStorage.getItem('CurrencyCode') + " " + CCFEECharges + " </div>";
                }

                Confirmation_PaymentDetail += "</div>";
            }
            //}

            var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");

            if (CompanyTypeID == "2") {
                //member agency
                var MAPublishedFare = parseFloat(sessionStorage.getItem('mapublishedfare'));
                var CPublishedFare = parseFloat(sessionStorage.getItem('cpublishedfare'));
                var TotalPublishedFare = MAPublishedFare + CPublishedFare;
                if ((MAPublishedFare > 0) && (CPublishedFare > 0)) {
                    Confirmation_PaymentDetail += "<div class='col-md-12'>";
                    Confirmation_PaymentDetail += "<div class='col-md-4'>Service Fee:</div>";
                    Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'> " + sessionStorage.getItem('CurrencyCode') + " " + TotalPublishedFare.toFixed(2) + "<span style='color:red;'> (Separate charge of service fee will appear on the credit card)</span> </div>";
                    Confirmation_PaymentDetail += "</div>";
                }
                if ((MAPublishedFare > 0) && (CPublishedFare <= 0)) {

                    Confirmation_PaymentDetail += "<div class='col-md-12'>";
                    Confirmation_PaymentDetail += "<div class='col-md-4'>Service Fee:</div>";
                    Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'> " + sessionStorage.getItem('CurrencyCode') + " " + MAPublishedFare.toFixed(2) + "<span style='color:red;'> (Separate charge of service fee will appear on the credit card)</span> </div>";
                    Confirmation_PaymentDetail += "</div>";
                }
            }
            else {
                //Independent or consolidator
                PublishFareMessage = "Separate charge of service fee will appear on the credit card";
                var PublishedFare = parseFloat(sessionStorage.getItem('mapublishedfare'));
                if (PublishedFare > 0) {
                    Confirmation_PaymentDetail += "<div class='col-md-12'>";
                    Confirmation_PaymentDetail += "<div class='col-md-4'>Service Fee:</div>";
                    Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'> " + sessionStorage.getItem('CurrencyCode') + " " + sessionStorage.getItem('mapublishedfare') + "<span style='color:red;'> (Separate charge of service fee will appear on the credit card)</span> </div>";
                    Confirmation_PaymentDetail += "</div>";
                }
            }


            debugger;

            if (PolicyNumber != "") {

                var Price_array = Price.split(';');

                Confirmation_PaymentDetail += "<div class='row'>";
                Confirmation_PaymentDetail += "<div class='col-md-12'>";
                Confirmation_PaymentDetail += "<div class='col-md-12 orng_bg top'><strong>Insurance Detail:</strong></div>";

                var i;
                for (i = 0; i < Price_array.length; i++) {
                    if (parseFloat(Price_array[i]) > 0) {
                        Confirmation_PaymentDetail += "<div class='col-md-4'>Passenger " + (i + 1) + ":</div>";
                        Confirmation_PaymentDetail += "<div class='col-md-6 no-padding'>" + Price_array[i] + "</div>";
                    }
                }

                Confirmation_PaymentDetail += "</div>";
                Confirmation_PaymentDetail += "</div>";

            }

            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-12 orng_bg'><strong>Billing Detail:</strong></div>";
            Confirmation_PaymentDetail += "<div class='col-md-6'>" + Reqst_passengersDetail_Resource.passengername + " " + Reqst_passengersDetail_Resource.Surname + "<br>" + Reqst_passengersDetail_Resource.passengerAddress + "<br>" + Reqst_passengersDetail_Resource.email + "<br>" + Reqst_passengersDetail_Resource.PhoneNumber + "</div>";

            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "<div class='row'>";
            Confirmation_PaymentDetail += "<div class='col-md-12'>";
            Confirmation_PaymentDetail += "<div class='col-md-6 pad_bot25'></div>";
            Confirmation_PaymentDetail += "</div>";
            Confirmation_PaymentDetail += "</div>";

            $("#Confirmation_PaymentDetail").empty().append(Confirmation_PaymentDetail);
            $(".AirBokingConfirmation-Panel").addClass("visible");
            $('html, body').animate({ scrollTop: 0 }, 500);
            //$("AirBokingPayment-Panel").hide();
            //$("AirBokingConfirmation-Panel").show();
            /******************************Payment Detail*************************/

            var n = ResultData.ErrorCode.startsWith("OK");
            if (n == true) {

                var Hotel = sessionStorage.getItem("Hotel");
                var CarRental = sessionStorage.getItem("CarRental");

                if ((Hotel == "True") && (CarRental == "True")) {
                    $('#footer-buttons').removeClass("hidden");
                    $('#btn-hotel').removeClass("hidden");
                    $('#btn-car').removeClass("hidden");
                }
                else if ((Hotel == "True") && (CarRental == "False")) {
                    $('#footer-buttons').removeClass("hidden");
                    $('#btn-hotel').removeClass("hidden");
                    $('#btn-car').addClass("hidden");
                }
                else if ((Hotel == "False") && (CarRental == "True")) {
                    $('#footer-buttons').removeClass("hidden");
                    $('#btn-hotel').addClass("hidden");
                    $('#btn-car').removeClass("hidden");
                }
                else if ((Hotel == "False") && (CarRental == "False")) {
                    $('#footer-buttons').addClass("hidden");
                }

                $('html, body').animate({ scrollTop: 0 }, 500);
            }
            else {
                // Booking Successfull And Payment Unsuccessful
                $(".AirBokingConfirmation-Panel").removeClass("hidden");
                $(".AirBokingPayment-Panel").addClass("hidden");
                $(".AirBokingConfirmation-Panel").addClass("visible");
                $('#footer-buttons').addClass("hidden");
                var error_title_title = "";
                if (PublishFareMessage != "") {
                    error_title_title += "<div class='row'>";
                    error_title_title += "<div class='col-md-12' style='color:red'>" + PublishFareMessage + "</div>";
                    error_title_title += "</div>";
                }
                error_title_title += "<div class='row'>";
                error_title_title += "<div class='col-md-12' style='color:red'>Payment for the following booking was not successful. Selected Fare is not guaranteed until paid in full. Please contact the agency and provide an alternate payment information to guarantee the selected Fare.</div>";
                error_title_title += "</div>";
                $("#error_title_title").empty().append(error_title_title);
                $('html, body').animate({ scrollTop: 0 }, 500);
            }


        }
        else if (ResultData.Message == "") {

            //Booking Failure
            $(".AirBokingConfirmation-Panel").removeClass("hidden");
            $(".AirBokingPayment-Panel").addClass("hidden");
            //$(".AirBokingConfirmation-Panel").addClass("visible");
            var error_title_title = "";
            error_title_title += "<div class='row'>";
            error_title_title += "<div class='col-md-12'>";
            debugger;
            if (ResultData.ErrorCode == "INVLD ACT CODE SEGMENT 01") {
                error_title_title += "<div class='col-md-12 orng_bg top'><strong>Seats for the selected fare are gone, please click the button below 'Select Another Fare' and select another fare from the quote page</strong></div>";
            }
            else {
                error_title_title += "<div class='col-md-12 orng_bg top'><strong>Selected fare has already been sold, please click the button below 'Select Another Fare' and select another fare from the quote page</strong></div>";
            }

            error_title_title += "<div class='col-md-6'><a href='http://qa.nanojot.com/air/' class='btn btn-primary'>Select Another Fare</a></div>";

            error_title_title += "</div>";
            error_title_title += "</div>";
            $("#error_title_title").empty().append(error_title_title);
            $('#footer-buttons').addClass("hidden");
            $('#confirmation-section').addClass("hidden");
            $('#confirmationprepared-section').addClass("hidden");
            $('html, body').animate({ scrollTop: 0 }, 500);
        }
        else {
            $(".AirBokingPayment-Panel").removeClass("hidden");
            $(".AirBokingConfirmation-Panel").addClass("hidden");
        }
        $('html, body').animate({ scrollTop: 0 }, 500);
        HideWaitProgress();
    }
    catch (e) {
        //$(".AirBokingPayment-Panel").removeClass("hidden");
        //$(".AirBokingConfirmation-Panel").addClass("hidden");


        var error = e;
        debugger;
        error = e;
        HideWaitProgress();
    }
}

function AirBookingPaymentForm() {
    try {
        debugger;
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
                    DepartureDate: All_value.DepartureDateTime,
                    ArrivalDate: All_value.ArrivalDateTime.slice,
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
            debugger;
            ////FlightSegments Details Management
            var numOfPassCnt = 0;
            var passengers_Details_List = new Array(), PassengerNameNumberCounting = 0;
            myPassengerDetailsListArray = SolutionDataTraveler("GET", "GetPassengerDetailsList");
            $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
                for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                    PassengerNameNumberCounting = (PassengerNameNumberCounting + 1);

                    //var givenname = $("#txtFirstNameAdults" + i + "").val() + " " + ($("#txtMiddleNameAdults" + i + "").val() == "" ? "" : $("#txtMiddleNameAdults" + i + "").val() + " ")+" "+$("#SelTDTitleAdults" + i + "").val().replace(".", "").toUpperCase();
                    var givenname = $("#txtFirstNameAdults" + i + "").val() + " " + ($("#txtMiddleNameAdults" + i + "").val() == "" ? "" : $("#txtMiddleNameAdults" + i + "").val() + " ") + " " + $("#SelTDTitleAdults" + i + "").val().replace(".", "").toUpperCase();
                    var PassengerName = $("#SelTDTitleAdults" + i + "").val().replace(".", "").toUpperCase() + " " + $("#txtFirstNameAdults" + i + "").val() + " " + $("#txtMiddleNameAdults" + i + "").val();
                    //alert(PassengerName);

                    passengers_Details_List.push({
                        passengerType: "Adults",
                        //passengername: $("#SelTDTitleAdults" + i + "").val() + " " + $("#txtFirstNameAdults" + i + "").val() + " " + $("#txtMiddleNameAdults" + i + "").val(),
                        passengername: PassengerName,
                        passengerAddress: $("#txtBCICity").val() + " , " + $("#selBCICountry").val(),
                        ReservationCode: $("#txtBCIZip").val(),
                        airlineName: AirLineName,
                        email: $("#txtBCIEmail").val(),
                        DepartureCityCode: $("#txtPICCNumber").val(),
                        DepartureTime: $("#txtPICCNumber").val(),
                        DepartureTerminal: $("#txtPICCNumber").val(),
                        IsInfant: "false",
                        DateofBirth: convertDate($("#txtDOBDateAdults" + i + "").val()),
                        PhoneLocationCode: $("#txtBCIBillPhone").val(),
                        PassengerNameNumber: PassengerNameNumberCounting + '.1',
                        PhoneNumber: $("#txtBCIBillPhone").val(),
                        PhoneUseType: "H",
                        PassengerEmail: $("#txtBCIEmail").val(),
                        PassengerNameRef: "ADT00" + i,
                        Gender: $("#selTDGenderAdults" + i + "").val(),
                        //GivenName: $("#txtFirstNameAdults" + i + "").val(),
                        GivenName: givenname,
                        //GivenName: $("#SelTDTitleAdults" + i + "").val().toUpperCase() + " " + $("#txtFirstNameAdults" + i + "").val() + " " + $("#txtMiddleNameAdults" + i + "").val(),
                        Surname: $("#txtLastNameAdults" + i + "").val(),
                        InsurancePrice: $('#passenger' + (PassengerNameNumberCounting - 1)).val()
                    });
                }
                for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {

                    PassengerNameNumberCounting = (PassengerNameNumberCounting + 1);

                    //var givenname = $("#SelTDTitleChildrens" + i + "").val().replace(".", "").toUpperCase() + " " + $("#txtFirstNameChildrens" + i + "").val() + " " + $("#txtMiddleNameChildrens" + i + "").val();
                    //var PassengerName = $("#SelTDTitleChildrens" + i + "").val() + " " + $("#txtFirstNameChildrens" + i + "").val() + " " + $("#txtMiddleNameChildrens" + i + "").val();

                    var givenname = $("#txtFirstNameChildrens" + i + "").val() + " " + ($("#txtMiddleNameChildrens" + i + "").val() == "" ? "" : $("#txtMiddleNameChildrens" + i + "").val() + " ") + " " + $("#SelTDTitleChildrens" + i + "").val().replace(".", "").toUpperCase();
                    var PassengerName = $("#SelTDTitleChildrens" + i + "").val().replace(".", "").toUpperCase() + " " + $("#txtFirstNameChildrens" + i + "").val() + " " + $("#txtMiddleNameChildrens" + i + "").val();
                    passengers_Details_List.push({
                        passengerType: "Childrens",
                        passengername: PassengerName,
                        passengerAddress: $("#txtBCICity").val() + " , " + $('#selBCICountry').val(),
                        ReservationCode: $("#txtBCIZip").val(),
                        airlineName: AirLineName,
                        email: $("#txtBCIEmail").val(),
                        DepartureCityCode: $("#txtPICCNumber").val(),
                        DepartureTime: $("#txtPICCNumber").val(),
                        DepartureTerminal: $("#txtPICCNumber").val(),
                        DateofBirth: convertDate($("#txtDOBDateChildrens" + i + "").val()),
                        PhoneLocationCode: $("#txtBCIBillPhone").val(),
                        PassengerNameNumber: PassengerNameNumberCounting + '.1',
                        PhoneNumber: $("#txtBCIBillPhone").val(),
                        PhoneUseType: "H",
                        PassengerEmail: $("#txtBCIEmail").val(),
                        PassengerNameRef: "CHD00" + i,
                        Gender: $("#selTDGenderChildrens" + i + "").val(),
                        //Gender: $("#SelTDTitleChildrens" + i + "").val() + " " + $("#txtFirstNameChildrens" + i + "").val() + " " + $("#txtMiddleNameChildrens" + i + "").val(),
                        GivenName: givenname,
                        Surname: $("#txtLastNameChildrens" + i + "").val(),
                        InsurancePrice: $('#passenger' + (PassengerNameNumberCounting - 1)).val()
                    });
                }
                for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                    PassengerNameNumberCounting = (PassengerNameNumberCounting + 1);
                    //var givenname = $("#SelTDTitleInfants" + i + "").val().replace(".", "").toUpperCase() + " " + $("#txtFirstNameInfants" + i + "").val() + " " + $("#txtMiddleNameInfants" + i + "").val();
                    //var PassengerName = $("#SelTDTitleInfants" + i + "").val() + " " + $("#txtFirstNameInfants" + i + "").val() + " " + $("#txtMiddleNameInfants" + i + "").val();

                    var givenname = $("#txtFirstNameInfants" + i + "").val() + " " + ($("#txtMiddleNameInfants" + i + "").val() == "" ? "" : $("#txtMiddleNameInfants" + i + "").val() + " ") + " " + $("#SelTDTitleInfants" + i + "").val().replace(".", "").toUpperCase();
                    var PassengerName = $("#SelTDTitleInfants" + i + "").val().replace(".", "").toUpperCase() + " " + $("#txtFirstNameInfants" + i + "").val() + " " + $("#txtMiddleNameInfants" + i + "").val();

                    passengers_Details_List.push({
                        passengerType: "Infants",
                        passengername: PassengerName,
                        passengerAddress: $("#txtBCICity").val() + " , " + $('#selBCICountry').val(),
                        ReservationCode: $("#txtBCIZip").val(),
                        airlineName: AirLineName,
                        email: $("#txtBCIEmail").val(),
                        DepartureCityCode: $("#txtPICCNumber").val(),
                        DepartureTime: $("#txtPICCNumber").val(),
                        DepartureTerminal: $("#txtPICCNumber").val(),
                        DateofBirth: convertDateInfant($("#txtDOBDateInfants" + i + "").val()),
                        PhoneLocationCode: $("#txtBCIBillPhone").val(),
                        PassengerNameNumber: PassengerNameNumberCounting + '.1',
                        PhoneNumber: $("#txtBCIBillPhone").val(),
                        PhoneUseType: "H",
                        PassengerEmail: $("#txtBCIEmail").val(),
                        PassengerNameRef: "INF00" + i,
                        GivenName: givenname,
                        //GivenName: $("#SelTDTitleInfants" + i + "").val() + " " + $("#txtFirstNameInfants" + i + "").val() + " " + $("#txtMiddleNameInfants" + i + "").val(),
                        Gender: $("#selTDGenderInfants" + i + "").val(),
                        Surname: $("#txtLastNameInfants" + i + "").val(),
                        InsurancePrice: $('#passenger' + (PassengerNameNumberCounting - 1)).val()
                    });
                }
            });

            ////FlightSegments Details Management
            var credit_CardOther_Details_List = new Array();
            credit_CardOther_Details_List.push({
                CardType: $("#selPICCType").val().trim(),
                CardNumber: $("#txtPICCNumber").val().trim(),
                CVVNumber: $("#txtPIVCNumber").val().trim(),
                Address: $("#txtBCICity").val().trim() + "  " + $("#selBCICountry").val().trim() + "  " + $("#txtBCIZip").val().trim(),
                CardExpiryDate: $("#selectPICCYear").val().trim() + "-" + $("#selectPICCMonth").val().trim(),
                NameOnCard: $("#txtPICCHName").val().trim(),
                AirLineCode: FilterAllFlightSegment_Array[0].MarketingAirline.Code,
                CurrCode: AirItineraryPricingInformation.CurrencyCode,
                PaymentAmount: parseFloat(AirItineraryPricingInformation.Amount).toFixed(2),
                CCFee: $("#lblCCFee").text()
            });
            //debugger;
            var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");
            var MAPublishedFare = parseFloat(sessionStorage.getItem('mapublishedfare'));
            var CPublishedFare = parseFloat(sessionStorage.getItem('cpublishedfare'));
            var ServiceFee = 0;
            var Commission = -1;
            if (CompanyTypeID == "2") {
                //Member Agency
                if ((MAPublishedFare > 0) && (CPublishedFare > 0)) {
                    ServiceFee = MAPublishedFare + CPublishedFare;
                    Commission = MAPublishedFare;
                }
                else if ((MAPublishedFare > 0) && (CPublishedFare <= 0)) {
                    ServiceFee = MAPublishedFare;
                    Commission = CPublishedFare + MAPublishedFare;
                }
            }
            else {
                //Consolidator
                if (CPublishedFare > 0) {
                    ServiceFee = CPublishedFare;
                }
            }

            var CCEnableAir = sessionStorage.getItem("CCEnableAir");
            var AgencyDetails = {
                Name: sessionStorage.getItem("Name"),
                CityName: sessionStorage.getItem("CityName"),
                country: sessionStorage.getItem("country"),
                PostalCode: sessionStorage.getItem("PostalCode"),
                state: sessionStorage.getItem("state"),
                StreetAddress: sessionStorage.getItem("StreetAddress"),
                SmtpEmailID: sessionStorage.getItem("SmtpEmailID"),
                HeaderUrl: sessionStorage.getItem("HeaderUrl"),
                FooterUrl: sessionStorage.getItem("FooterUrl"),
                PrivacyUrl: sessionStorage.getItem("PrivacyUrl"),
                TermsUrl: sessionStorage.getItem("TermsUrl"),
                PhoneNumber: sessionStorage.getItem("PhoneNumber"),
                UserName: sessionStorage.getItem("UserName"),
                Password: sessionStorage.getItem("Password"),
                QueueNo: sessionStorage.getItem("QueueNo"),
                PseudoCityCode: sessionStorage.getItem("PseudoCityCode"),
                FromEmail: sessionStorage.getItem("FromEmail"),
                ToEmail: sessionStorage.getItem("ToEmail"),
                Domain: sessionStorage.getItem("Domain"),
                CurrencyCode: sessionStorage.getItem("CurrencyCode"),
                ServiceFee: ServiceFee.toFixed(2),
                cpublishedfare: CPublishedFare,
                mapublishedfare: MAPublishedFare,
                CompanyTypeID: CompanyTypeID,
                Commission: Commission,
                CCEnableAir: CCEnableAir
            };
            var SaveRecords = {
                RequestID: localStorage.getItem("RequestID"),
                Locator: "",
                UserId: localStorage.getItem("LoggedinUID") == null ? "" : localStorage.getItem("LoggedinUID"),
                DefaultCompanyId: sessionStorage.getItem("Id"),
                BillingEmail: $('#txtBCIEmail').val(),
                BillingPhone: $('#txtBCIBillPhone').val(),
                BillingName: $('#txtPICCHName').val(),
                PaymentMethod: $('#selPICCType').val(),
            };
            var DateDep = sessionStorage.getItem("Origin_Final_Date");
            var DateRet = sessionStorage.getItem("Destination_Final_Date");
            //POLICYCODE : "TSVGIN",
            var GateWay = "";
            var Destination = "";

            var policy_code = "";
            if ($('#add-to-itinerary').prop('checked') == true) {
                policy_code = $('#hdpolicycode').val().trim();
            }

            var PolicyDetails = {
                DATEDEP: DateDep,
                DATERET: DateRet,
                PROVINCE: $("#selBCICountry").val().trim().split(',')[1],
                GATEWAY: localStorage.getItem("Origin"),
                DESTINATION: localStorage.getItem("Destination"),
                POLICYCODE: policy_code,
                PHONE: $('#txtBCIBillPhone').val().trim(),
                EMAIL: $('#txtBCIEmail').val().trim(),
                ADDRESS: $("#txtBCICity").val().trim(),
                CITY: $("#selBCICountry").val().trim().split(',')[0],
                POSTALCODE: $("#txtBCIZip").val().trim(),
                COUNTRY: $("#selBCICountry").val().trim().split(',')[2]
            }
            
            var APP_REQ = {
                "flightsDetailsList": flights_Details_List,
                "passengersDetailsList": passengers_Details_List,
                "creditCardOtherDetails": credit_CardOther_Details_List,
                "_AgencyDetails": AgencyDetails,
                "SaveRecords": SaveRecords,
                "_policydetails": PolicyDetails
            };




            //console.log("Data--------------------------------------");
            //console.log(APP_REQ);
            //console.log("Data--------------------------------------");
            function ResultCallBackSuccess(e, xhr, opts) {
                var App_Data = e;
                console.log("CCFEE-" + e.CCFEE);
                CCFEE = e.CCFEE;
                var PurchasedDate = e.PurchaseDate;
                var Price = e.Price;
                var PolicyNumber = e.PolicyNumber;
                sessionStorage.setItem("PurchasedDate", PurchasedDate);
                sessionStorage.setItem("Price", Price);
                sessionStorage.setItem("PolicyNumber", PolicyNumber);

                SolutionDataTraveler("SET", "AirReservationBookingResult", App_Data);
                $(".AirBokingConfirmation-Panel").removeClass("hidden");
                $(".AirBokingPayment-Panel").addClass("hidden");
                console.log(App_Data);
                LoadAirReservationResultData(App_Data);
            }
            function ResultCallBackError(e, xhr, opts) {
            }
            SolutionDataTraveler("SET", "AirReservationBookingRequest", APP_REQ);
            debugger;
            MasterAppConfigurationsServices("POST", CommonConfiguration.WebAPIServicesURL + "API/AirReservationBooking/PostAirBooking", JSON.stringify(APP_REQ), ResultCallBackSuccess, ResultCallBackError);
        }
    }
    catch (e) { HideWaitProgress(); }
}

//****************************** Booking Payment end*************************/


$(document).ready(function () {

    $.when(GetUserContactDetails()).done(function (a1) {
        var ResultData = SolutionDataTraveler("GET", "AirReservationBookingResult");
        console.log("-------Atload------");
        console.log(ResultData);
        if (ResultData != null) {
            if (ResultData.Message == "Success" && ResultData.Status == true) {
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
            var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");
            if (CompanyTypeID == "2") {
                //member agency
                var MAPublishedFare = parseFloat(sessionStorage.getItem('mapublishedfare'));
                var CPublishedFare = parseFloat(sessionStorage.getItem('cpublishedfare'));
                var TotalPublishedFare = MAPublishedFare + CPublishedFare;
                if ((MAPublishedFare > 0) && (CPublishedFare > 0)) {
                    $('#lblServiceFee').html(TotalPublishedFare.toFixed(2));
                    $('#msgServiceFee').html("<span style='color:red;'> (Separate charge of service fee will appear on the credit card)</span> ");
                }
                if ((MAPublishedFare > 0) && (CPublishedFare <= 0)) {
                    $('#lblServiceFee').html(MAPublishedFare.toFixed(2));
                    $('#msgServiceFee').html("<span style='color:red;'> (Separate charge of service fee will appear on the credit card)</span>");
                }
            }
            else {
                //Independent or consolidator
                var PublishedFare = parseFloat(sessionStorage.getItem('cpublishedfare'));
                if (PublishedFare > 0) {
                    $('#lblServiceFee').html(PublishedFare.toFixed(2));
                    $('#msgServiceFee').html("<span style='color:red;'> (Separate charge of service fee will appear on the credit card)</span>");
                }
            }
        }

    });

    $('#add-to-itinerary').click(function () {
        if ($(this).prop('checked')) {
            $('#msg-includeinsurance').show();
        } else {
            $('#msg-includeinsurance').hide();
        }
    });
    var Insurance_option = sessionStorage.getItem("Insurance");
    if (Insurance_option == "True") {
        $('#insurance-options').show();
        $('#no-insurance-options').hide();
    }
    else {
        $('#insurance-options').hide();
        $('#no-insurance-options').show();
    }
    $(".btn-success-booking").on('click', function () {
        ShowWaitProgress();
        $('#row1').html('Reservation of the selected fare is in progress');
        if (ValidateData()) {
            AirBookingPaymentForm();

        }
        else { HideWaitProgress(); }
    });

    //insurance block
    /*$("#section-insurance-details").on('click', function () {
        $('#insurance-details').toggle();
        if ($('#insurance-span-icon').text() == "+") {
            $('#insurance-span-icon').text("-");
        }
        else {
            $('#insurance-span-icon').text("+");
        }
    });*/
    //radiocancelinsurance
    $("#radiocancelinsurance").on('click', function () {
        $("#section-insurance-details").hide();
        //$('#msg-includeinsurance').hide();
    });
    $("#radiobookinsurance").on('click', function () {
        ShowWaitProgress();

        if (ValidateInsuranceData()) {

            $('#row1').html('Searching for your best Insurance Plan');
            $('.gisPreloader').css('top', '29px;')

            var Pax_List = new Array();
            var Pass_Cnt = $('#passengercnt').val();

            for (var i = 0; i < parseInt(Pass_Cnt) ; i++) {

                var type = $('#type' + i).text().trim();
                var typeid = "";
                if (type == "Adult") {
                    typeid = "Adults";
                }
                else if (type == "Child") {
                    typeid = "Childrens";
                }
                else if (type == "Infant") {
                    typeid = "Infants";
                }

                var date = new Date($('#txtDOBDate' + typeid + '0').val());

                var year = date.getFullYear().toString();
                var month = "";
                var day = "";
                if (date.getMonth() < 10) {
                    month = "0" + (date.getMonth() + 1).toString();
                }
                else {
                    month = (date.getMonth() + 1).toString();
                }
                if (date.getDate() < 10) {
                    day = "0" + (date.getDate() + 1).toString();
                }
                else {
                    day = (date.getDate() + 1).toString();
                }

                //var day = date.getDate().toString();

                var yyyymmdd = year + month + day;

                Pax_List.push({
                    BIRTHDATE: yyyymmdd,
                    TRIPCOST: $('#tripcost' + i).text().trim(),
                    AMTAFTER: "",
                    PRICE: $('#price' + i).text().trim(),
                    TAX: $('#tax' + i).text().trim(),
                });
            }

            var DateDep = sessionStorage.getItem("Origin_Final_Date");
            var DateRet = sessionStorage.getItem("Destination_Final_Date");
            debugger;
            var APP_REQ = {
                "DATEDEP": DateDep,
                "DATERET": DateRet,
                "PROVINCE": $('#selBCICountry').val().split(',')[1],
                "NBPAX": $('#passengercnt').val(),
                "PAXES": Pax_List,
            };
            function ResultCallBackSuccess(e, xhr, opts) {

                var App_Data = e;
                console.log(e);
                console.log(App_Data.RESPONSE.Products[0].CODE);
                $('#hdpolicycode').val(App_Data.RESPONSE.Products[0].CODE);
                var html = "";

                html += "<p>Plan: " + App_Data.RESPONSE.Products[0].NAME + " ( <a target='_blank' href='" + App_Data.RESPONSE.Products[0].DESCURL + "'>Plan Details </a>)</p>";
                var cnt = 1;
                for (var j = 0; j < App_Data.RESPONSE.Products[0].PAXES.length; j++) {
                    if (j == 0) {
                        html += "<p>Adults Insurance Price " + ": " + sessionStorage.getItem('CurrencyCode') + " " + App_Data.RESPONSE.Products[0].PAXES[j].PRICE + "</p>";
                        $('#passenger' + j).val(App_Data.RESPONSE.Products[0].PAXES[j].PRICE);
                    }
                    else if (j == 1) {
                        html += "<p>Childrens Insurance Price " + ": " + sessionStorage.getItem('CurrencyCode') + " " + App_Data.RESPONSE.Products[0].PAXES[j].PRICE + "</p>";
                        $('#passenger' + j).val(App_Data.RESPONSE.Products[0].PAXES[j].PRICE);
                    }
                    else if (j == 2) {
                        html += "<p>Infants Insurance Plan " + ": " + sessionStorage.getItem('CurrencyCode') + " " + App_Data.RESPONSE.Products[0].PAXES[j].PRICE + "</p>";
                        $('#passenger' + j).val(App_Data.RESPONSE.Products[0].PAXES[j].PRICE);
                    }

                }

                $('#section-insurance-details').show();
                //$('#msg-includeinsurance').show();
                $('#insurance-details').html(html);
                HideWaitProgress();
                debugger;

            }
            function ResultCallBackError(e, xhr, opts) {
            }
            debugger;
            MasterAppConfigurationsServices("POST", CommonConfiguration.WebAPIServicesURL + "API/AirReservationBooking/GetInsuranceQuote", JSON.stringify(APP_REQ), ResultCallBackSuccess, ResultCallBackError);

        }
        else {
            $(this).prop("checked", false);
            HideWaitProgress();
        }
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

function fpercent(amount, percent) {
    return amount * percent / 100;
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
        var count = 0;
        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {

            if (parseInt(dt_value.Adults) > 0) {
                AppPassengerDetailsData += "<div class='col-md-12'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Adults</div></div>";
            }
            AppPassengerDetailsData += "<input type='hidden' id='passenger" + count + "' />";
            count += 1;
            for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                AppPassengerDetailsData += "<div class='col-md-12 table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: 0px; margin-left: 10px;'>" + (i + 1) + ":<span class='stat_col'>*</span></label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' data-i='" + i + "' id='SelTDTitleAdults" + i + "' style='width:100%; height: 27px; margin-top: 0px;'><option>Mr.</option><option>Ms.</option><option>Miss.</option><option>Mstr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off' id='txtFirstNameAdults" + i + "' name='txtFirstNameAdults" + i + "' placeholder='First Name'  class=' form-control' style='width: 96%;float:left; height: 27px; margin-top: 0px;'  /><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtMiddleNameAdults" + i + "' name='txtMiddleNameAdults" + i + "' placeholder='Middle Name'  class='form-control' style='width: 100%; height: 27px; margin-top: 0px;'  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtLastNameAdults" + i + "' name='txtLastNameAdults" + i + "' placeholder='Last Name'  class='  form-control' style='width: 96%;float:left; height: 27px; margin-top: 0px;'  /><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-11 col-sm-11' style='float:left;'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateAdults" + i + "' readonly='readonly' type='text'  placeholder='Date of Birth' name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height: 27px; margin-top: 0px;'  /><span class='input-group-addon'  data-i='" + i + "' id='col_txtDOBDateAdults" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control'  data-i='" + i + "' id='selTDGenderAdults" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.Childrens) > 0) {
                AppPassengerDetailsData += "<div class='col-md-12'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Children</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {
                AppPassengerDetailsData += "<div class='col-md-12 table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: 0px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' data-i='" + i + "' id='SelTDTitleChildrens" + i + "' style='width:100%; height:27px; margin-top: 0px; '><option>Mr.</option><option>Ms.</option><option>Miss.</option><option>Mstr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtFirstNameChildrens" + i + "' name='txtFirstNameChildrens" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 96%;float:left; height:27px; margin-top: 0px; '  /><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtMiddleNameChildrens" + i + "' name='txtMiddleNameChildrens" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtLastNameChildrens" + i + "' name='txtLastNameChildrens" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 96%;float:left; height:27px; margin-top: 0px; '  /><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-11 col-sm-11' style='float:left;'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateChildrens" + i + "' readonly='readonly' type='text'  placeholder='Date of Birth'  name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:96%; height:27px; margin-top: 0px; '  /><span class='input-group-addon'  data-i='" + i + "' id='col_txtDOBDateChildrens" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control'  data-i='" + i + "' id='selTDGenderChildrens" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.Infants) > 0) {
                AppPassengerDetailsData += "<div class='col-md-12'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Infants</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                AppPassengerDetailsData += "<div class='col-md-12 table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: 0px;  margin-top: -1px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' data-i='" + i + "' id='SelTDTitleInfants" + i + "' style='width:100%; height:27px; margin-top: 0px; ; '><option>Inf.</option><option>Mr.</option><option>Ms.</option><option>Miss.</option><option>Mstr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtFirstNameInfants" + i + "' name='txtFirstNameInfants" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 96%;float:left; height:27px; margin-top: 0px; '  /><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtMiddleNameInfants" + i + "' name='txtMiddleNameInfants" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtLastNameInfants" + i + "' name='txtLastNameInfants" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 96%;float:left; height:27px; margin-top: 0px; '  /><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-11 col-sm-11' style='float:left;'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateInfants" + i + "' readonly='readonly' type='text'  placeholder='Date of Birth'  name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px; margin-top: 0px; '  /><span class='input-group-addon'  data-i='" + i + "' id='col_txtDOBDateInfants" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div><span style='float:left;' class='stat_col'>*</span></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' data-i='" + i + "'  id='selTDGenderInfants" + i + "' style='width:100%; height:27px'><option value='MI'>Male</option><option  value='FI'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.OnSeat) > 0) {
                AppPassengerDetailsData += "<div class='col-md-12'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>OnSeat</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.OnSeat) ; i++) {
                AppPassengerDetailsData += "<div class='col-md-12 table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: 0px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' data-i='" + i + "'  id='SelTDTitleOnSeat" + i + "' style='width:100%; height:27px; margin-top: 0px;  '><option>Mr.</option><option>Ms.</option><option>Miss.</option><option>Mstr.</option><option>Dr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtFirstNameOnSeat" + i + "' name='txtFirstNameOnSeat" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtMiddleNameOnSeat" + i + "' name='txtMiddleNameOnSeat" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtLastNameOnSeat" + i + "' name='txtLastNameOnSeat" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateOnSeat" + i + "' type='text'  readonly='readonly' placeholder='Date of Birth'  name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px; margin-top: 0px; '  /><span class='input-group-addon'  data-i='" + i + "'  id='col_txtDOBDateOnSeat" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderOnSeat" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
            if (parseInt(dt_value.Seniors) > 0) {
                AppPassengerDetailsData += "<div class='col-md-12'><div class='form-group col-md-1' style='width:100%; height:27px;  margin-left: 11px;'>Seniors</div></div>";
            }
            for (var i = 0; i < parseInt(dt_value.Seniors) ; i++) {
                AppPassengerDetailsData += "<div class='col-md-12 table-responsive'>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><label class='form-control' style='width:100%; height:27px; margin-top: 0px; margin-left: 10px;'>" + (i + 1) + ":</label></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' data-i='" + i + "'  id='SelTDTitleSeniors" + i + "' style='width:100%; height:27px; margin-top: 0px;  '><option>Mr.</option><option>Ms.</option><option>Miss.</option><option>Mstr.</option><option>Dr.</option></select></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtFirstNameSeniors" + i + "' name='txtFirstNameSeniors" + i + "' placeholder='First Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtMiddleNameSeniors" + i + "' name='txtMiddleNameSeniors" + i + "' placeholder='Middle Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><input type='text' autocomplete='off'  id='txtLastNameSeniors" + i + "' name='txtLastNameSeniors" + i + "' placeholder='Last Name'  class='col-xs-10 col-sm-5' style='width: 100%; height:27px; margin-top: 0px; '  /></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-2'><div class='input-group col-xs-12 col-sm-12'><input class='form-control  date-picker  Restrictinput ' id='txtDOBDateSeniors" + i + "' type='text'   readonly='readonly' placeholder='Date of Birth'  name='date-range-picker' data-date-format='yyyy-mm-dd' style='width:100%; height:27px; margin-top: 0px; '  /><span class='input-group-addon'  data-i='" + i + "'  id='col_txtDOBDateSeniors" + i + "'><i class='fa fa-calendar bigger-80'></i></span></div></div>";
                AppPassengerDetailsData += "<div class='form-group col-md-1'><select class='form-control' id='selTDGenderSeniors" + i + "' style='width:100%; height:27px'><option value='M'>Male</option><option  value='F'>Female</option></select></div>";
                AppPassengerDetailsData += "<div class='space-8'></div></div>";
            }
        });
        $("#BookingPassangerDetails").empty().append(AppPassengerDetailsData);

        debugger;

        /* Set Date Range For Pssenger's Date Of Birth 8 */
        var DobEndDate = new Date();
        DobEndDate = DobEndDate.getFullYear();

        var AdultDOBStartDate = new Date();
        AdultDOBStartDate = AdultDOBStartDate.getFullYear() - 100;
        //var AdultRange = AdultDOBStartDate + ':' + DobEndDate;

        var ChildrenDOBStartDate = new Date();
        ChildrenDOBStartDate = ChildrenDOBStartDate.getFullYear() - 18;

        var InfantDOBStartDate = new Date();
        InfantDOBStartDate = InfantDOBStartDate.getFullYear() - 2;

        /* Set Date Range For Pssenger's Date Of Birth 8 */


        $.each(myPassengerDetailsListArray, function (dt_key, dt_value) {
            for (var i = 0; i < parseInt(dt_value.Adults) ; i++) {
                $("#txtDOBDateAdults" + i + "").datepicker({
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    yearRange: AdultDOBStartDate + ':' + DobEndDate,
                    changeMonth: true,
                    changeYear: true
                    //todayHighlight: true
                });
                //$(document).on("click", "#col_txtDOBDateAdults" + i , function(){

                $("#col_txtDOBDateAdults" + i).click(function () {
                    $("#txtDOBDateAdults" + $(this).attr('data-i') + "").datepicker("show");
                });
                $('#SelTDTitleAdults' + i).on('change', function () {
                    var val = $(this).find('option:selected').text();
                    //Mr. Ms. Miss. Mstr.
                    if ((val == "Mr.") || (val == "Mstr.")) {
                        $('#selTDGenderAdults' + $(this).attr('data-i')).val("M");
                    }
                    else if ((val == "Ms.") || (val == "Miss.")) {
                        $('#selTDGenderAdults' + $(this).attr('data-i')).val("F");
                    }
                });

                $('#selTDGenderAdults' + i).on('change', function () {
                    var val = $(this).val();
                    //Mr. Ms. Miss. Mstr.
                    if (val == "M") {
                        $('#SelTDTitleAdults' + $(this).attr('data-i')).val("Mr.");
                    }
                    else if (val == "F") {
                        $('#SelTDTitleAdults' + $(this).attr('data-i')).val("Ms.");
                    }
                });

                $('#txtFirstNameAdults' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
                $('#txtMiddleNameAdults' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
                $('#txtLastNameAdults' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
            }
            for (var i = 0; i < parseInt(dt_value.Childrens) ; i++) {
                $("#txtDOBDateChildrens" + i + "").datepicker({
                    yearRange: ChildrenDOBStartDate + ':' + DobEndDate,
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    changeMonth: true,
                    changeYear: true
                    //todayHighlight: true
                });
                $("#col_txtDOBDateChildrens" + i + "").click(function () {
                    $("#txtDOBDateChildrens" + $(this).attr('data-i') + "").datepicker("show");
                });
                $('#SelTDTitleChildrens' + i).on('change', function () {
                    var val = $(this).find('option:selected').text();
                    //Mr. Ms. Miss. Mstr.
                    if ((val == "Mr.") || (val == "Mstr.")) {
                        $('#selTDGenderChildrens' + $(this).attr('data-i')).val("M");
                    }
                    else if ((val == "Ms.") || (val == "Miss.")) {
                        $('#selTDGenderChildrens' + $(this).attr('data-i')).val("F");
                    }
                });
                $('#selTDGenderChildrens' + i).on('change', function () {
                    var val = $(this).val();
                    //Mr. Ms. Miss. Mstr.
                    if (val == "M") {
                        $('#SelTDTitleChildrens' + $(this).attr('data-i')).val("Mstr.");
                    }
                    else if (val == "F") {
                        $('#SelTDTitleChildrens' + $(this).attr('data-i')).val("Miss.");
                    }
                });

                $('#txtFirstNameChildrens' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
                $('#txtMiddleNameChildrens' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
                $('#txtLastNameChildrens' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
            }
            for (var i = 0; i < parseInt(dt_value.Infants) ; i++) {
                $("#txtDOBDateInfants" + i + "").datepicker({
                    yearRange: InfantDOBStartDate + ':' + DobEndDate,
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    changeMonth: true,
                    changeYear: true
                    //todayHighlight: true
                });
                $("#col_txtDOBDateInfants" + i + "").click(function () {
                    $("#txtDOBDateInfants" + $(this).attr('data-i') + "").datepicker("show");
                });
                $('#SelTDTitleInfants' + i).on('change', function () {
                    var val = $(this).find('option:selected').text();
                    //Mr. Ms. Miss. Mstr.
                    if ((val == "Mr.") || (val == "Mstr.")) {
                        $('#selTDGenderInfants' + $(this).attr('data-i')).val("MI");
                    }
                    else if ((val == "Ms.") || (val == "Miss.")) {
                        $('#selTDGenderInfants' + $(this).attr('data-i')).val("FI");
                    }
                });

                $('#selTDGenderInfants' + i).on('change', function () {
                    var val = $(this).val();
                    //Mr. Ms. Miss. Mstr.
                    if (val == "M") {
                        $('#SelTDTitleInfants' + $(this).attr('data-i')).val("Mstr.");
                    }
                    else if (val == "F") {
                        $('#SelTDTitleInfants' + $(this).attr('data-i')).val("Miss.");
                    }
                });

                $('#txtFirstNameInfants' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
                $('#txtMiddleNameInfants' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
                $('#txtLastNameInfants' + i).on('blur', function () {
                    $(this).val($(this).val().toUpperCase());
                });
            }
            for (var i = 0; i < parseInt(dt_value.OnSeat) ; i++) {
                $("#txtDOBDateOnSeat" + i + "").datepicker({
                    yearRange: AdultDOBStartDate + ':' + DobEndDate,
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    changeMonth: true,
                    changeYear: true
                    //todayHighlight: true
                });
                $("#col_txtDOBDateOnSeat" + i + "").click(function () {
                    $("#txtDOBDateOnSeat" + $(this).attr('data-i') + "").datepicker("show");
                });
            }
            for (var i = 0; i < parseInt(dt_value.Seniors) ; i++) {
                $("#txtDOBDateSeniors" + i + "").datepicker({
                    yearRange: AdultDOBStartDate + ':' + DobEndDate,
                    autoclose: true,
                    format: 'yyyy-mm-dd',
                    changeMonth: true,
                    changeYear: true
                    //todayHighlight: true
                });
                $("#col_txtDOBDateSeniors" + i + "").click(function () {
                    $("#txtDOBDateSeniors" + $(this).attr('data-i') + "").datepicker("show");
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
                var FightsArray = new Array();
                FightsArray.push({
                    OriginDestinationFlight: SelectedAirLineAllFlightSegment_Data
                });

                ////////////////////////////////FlightSegments////////////////////////////////////////////////////////////////////////////////////////
                var FlightSegmentCounter = 0;
                var LastDepDate = "";
                $.each(FightsArray, function (All_key, All_value) {
                    var OriginDestLength = All_value.OriginDestinationFlight.length;
                    var FlightSegments = All_value.OriginDestinationFlight[OriginDestLength - 1].FlightSegments.length;
                    debugger;

                    sessionStorage.setItem("Origin_Final_Date", All_value.OriginDestinationFlight[0].FlightSegments[0].DepartureDateTime);
                    sessionStorage.setItem("Destination_Final_Date", All_value.OriginDestinationFlight[OriginDestLength - 1].FlightSegments[FlightSegments - 1].ArrivalDateTime);

                    LastDepDate = All_value.OriginDestinationFlight[0].FlightSegments[FlightSegments - 1].DepartureDateTime;
                    sessionStorage.setItem("LastDepartureDate", LastDepDate);
                    debugger;
                    var Flight_Segment_ID = All_value.OriginDestinationFlight[0].FlightSegments[0].FlightSegmentID;
                    var Allkey = All_key, FlightNumberStop = '';
                    var Hour = 0, Hours = 0, Minute = 0, Minutes = 0, StopoverHours = 0, StopoverMinutes = 0, FinalMinutes = 0;
                    var NoOfStops = 0, cntStops = 1;
                    TotalAmount = All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount;
                    dynamicDetails += "<div class='table-responsive'>";
                    dynamicDetails += "<table class='table without_tab'><thead style='display:none;'><tr>";
                    dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong> Fares:  " + All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount + " + " + All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "$" + All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + All_value.OriginDestinationFlight[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount + "</strong><br> <small class='gray'>Final Total Price(incl fee)</small> </div>";
                    dynamicDetails += "<div class='col-md-3 rt-align'>";
                    dynamicDetails += "</div></th></tr></thead>";
                    dynamicDetails += "<tbody class='pink-bg'>";
                    dynamicDetails += " <tr class='txt-yellow'>";
                    dynamicDetails += " <td colspan='5'><i class='fa fa-check' aria-hidden='true'></i> Best Price Guarantee! </td>";
                    dynamicDetails += "</tr>";
                    dynamicDetails += "</tbody>";
                    //userIsYoungerThan18 ? "Minor" : "Adult"

                    var flights_StopsTime_List = new Array();
                    var FlitArrivalDateTimeforStopColculation = "";
                    var SegmentsElapsedTime = "";
                    debugger;
                    $.each(All_value.OriginDestinationFlight, function (FlightSegment_key, FlightSegment_value) {
                        NoOfStops = FlightSegment_value.FlightSegments.length;
                        var segment_len = FlightSegment_value.FlightSegments.length;
                        cntStops = 1;
                        FlitArrivalDateTimeforStopColculation = "";
                        FlightNumberStop = (FlightSegment_value.FlightSegments.length == 1) ? "NonStop" : ((FlightSegment_value.FlightSegments.length == 2) ? "1 Stop" : "2+ Stop");
                        $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {

                            var timeDifflapse = "";
                            var ArrivalTime = "";
                            var DepTime = "";
                            if (FlitArrivalDateTimeforStopColculation != "") {
                                ArrivalTime = FlitArrivalDateTimeforStopColculation;
                                DepTime = Segment_value.DepartureDateTime;
                                timeDifflapse = timeDiff(DepTime, ArrivalTime);
                                Hour = (Math.floor(Math.abs(timeDifflapse.minutes) / 60));
                                Minute = (Math.abs(timeDifflapse.minutes) % 60);
                                dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Layover: " + Hour + "H " + Minute + "M </td></tr>";
                            }
                            FlitArrivalDateTimeforStopColculation = Segment_value.ArrivalDateTime;
                            var vSegmentsElapsedTime = Segment_value.ElapsedTime;
                            var vflighthours = Math.trunc(vSegmentsElapsedTime / 60);
                            var vflightminutes = vSegmentsElapsedTime % 60;

                            if (Segment_value.OperatingAirline.Code == "AI")
                                isFlight_AI = true;

                            if (Segment_value.OperatingAirline.Code == "PK")
                                isFlight_PK = true;

                            dynamicDetails += "<tr>";
                            dynamicDetails += "<td><img src='Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";

                            dynamicDetails += "<td><strong>" + Airline_Name(Segment_value.MarketingAirline.Code) + "</strong><br> Operated by <strong>" + Airline_Name(Segment_value.OperatingAirline.Code) + "</strong><br>  Flight # <strong>" + Segment_value.OperatingAirline.Code + "-" + Segment_value.OperatingAirline.FlightNumber + "</strong></td>";
                            dynamicDetails += "<td><strong>" + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.DepartureDateTime)) + "<br> " + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.ArrivalDateTime)) + "</strong></td>";
                            dynamicDetails += "<td><strong>" + AirPort_Name(Segment_value.DepartureAirport.LocationCode) + "<br> " + AirPort_Name(Segment_value.ArrivalAirport.LocationCode) + "</strong></td>";
                            //dynamicDetails += "<td class='rt-align'><strong>" + FlightNumberStop + "</strong> <br> Coach / " + vflighthours + " Hours  <br>" + vflightminutes + " Minutes</td>";
                            dynamicDetails += "<td class='rt-align'>" + " Coach <br> " + vflighthours + " H " + vflightminutes + " M</td>";
                            dynamicDetails += "</tr>";


                            cntStops = cntStops + 1;
                        });
                        SegmentsElapsedTime = FlightSegment_value.ElapsedTime;
                        var flighthours = Math.trunc(SegmentsElapsedTime / 60);
                        var flightminutes = SegmentsElapsedTime % 60;
                        dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + flighthours + "H " + flightminutes + "M (" + FlightNumberStop + ")</td></tr>";

                    });
                    dynamicDetails += "</table>";
                    dynamicDetails += "</div>";
                });
                $("#Search-FlightSegments-Result-Panel").empty().append(dynamicDetails);
                ////////////////////////////////FlightSegments////////////////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////FlightTaxes///////////////////////////////////////////////////////////////////////////////////////////
                dynamicDetails = "";
                var AirItinerary = 0;
                $.each(SelectedAirLineAllFlightSegment_Data, function (All_key, All_value) {
                    var Flight_Segment_ID = All_value.FlightSegments[0].FlightSegmentID;
                    var FlightNumberStop = '';
                    dynamicDetails += "<div class='table-responsive' style=''>";
                    dynamicDetails += "<table class='table without_tab'><thead style='border-top:1px;'><tr>";
                    dynamicDetails += "<th>Passanger Type</th>";
                    dynamicDetails += "<th style='width:20%'>Number of Passanger</th>";
                    dynamicDetails += "<th class='text-right'>Base Fare</th>";
                    dynamicDetails += "<th class='text-right'>Taxes &amp; Fees</th>";
                    dynamicDetails += "<th class='text-right'>Total Price</th>";
                    dynamicDetails += "</tr></thead>";

                    dynamicDetails += "<tbody class='pink-bg_1 text-left'>";
                    FlightNumberStop = (All_value.FlightSegments.length == 1) ? "NonStop" : ((All_value.FlightSegments.length == 2) ? "1 Stop" : "2+ Stop");
                    $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                        if (AirItinerary == 0) {
                            AirItinerary = (AirItinerary + 1);
                            var AirItineraryPricingInformation = Segment_value.AirItineraryPricingInfo;
                            var total = 0;
                            var currencyCode = "";
                            $('#passengercnt').val(AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown.length);
                            for (var i = 0; i < AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown.length; i++) {

                                if (AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Code == 'ADT')
                                    dynamicDetails += "<tr><td id='type" + i + "'> Adult</td>";
                                else if (AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Code == 'C07')
                                    dynamicDetails += "<tr><td id='type" + i + "'> Child</td>";
                                else if (AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Code == 'INF')
                                    dynamicDetails += "<tr><td id='type" + i + "'> Infant</td>";

                                dynamicDetails += "<td>" + AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Quantity + "</td>";
                                dynamicDetails += "<td id='tripcost" + i + "' class='text-right'>" + parseFloat(AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Quantity * AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerFare.EquivFare.Amount).toFixed(2) + "</td>";
                                dynamicDetails += "<td id='tax" + i + "' class='text-right'>" + parseFloat(AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Quantity * AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerFare.Taxes.TotalTax.Amount).toFixed(2) + "</td>";
                                dynamicDetails += "<td id='price" + i + "' class='text-right'>" + parseFloat(AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Quantity * AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerFare.TotalFare.Amount).toFixed(2) + "</td></tr>";
                                total = parseFloat(total) + parseFloat(AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerTypeQuantity.Quantity * AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerFare.TotalFare.Amount);
                                currencyCode = AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[i].PassengerFare.EquivFare.CurrencyCode;
                            }
                            dynamicDetails += "<tr><td colspan='3'></td><td><b>Grand Total</b></td><td class='text-right'>" + total.toFixed(2) + " " + currencyCode + "</td></tr>";
                            dynamicDetails += "<tr><td colspan='5'>Please note: All fares are quoted in " + sessionStorage.getItem('CurrencyCode') + ". Some airlines may charge baggage fees.</td></tr>";



                            //dynamicDetails += "<tr><td>&nbsp;</td><td>" + AirItineraryPricingInformation.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerTypeQuantity.Quantity + " Adult</td><td>&nbsp;</td></tr>";
                            //dynamicDetails += "<tr><td>Base Fare:</td><td>" + AirItineraryPricingInformation.ItinTotalFare.BaseFare.Amount + " " + AirItineraryPricingInformation.ItinTotalFare.TotalFare.CurrencyCode + "</td><td>&nbsp;</td></tr>";
                            //dynamicDetails += "<tr><td>Taxes &amp; Fees:</td><td>" + AirItineraryPricingInformation.ItinTotalFare.Taxes.Tax[0].Amount + " " + AirItineraryPricingInformation.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "</td><td>&nbsp;</td></tr>";
                            //dynamicDetails += "<tr><td>Total Price:</td><td>" + AirItineraryPricingInformation.ItinTotalFare.TotalFare.Amount + " " + AirItineraryPricingInformation.ItinTotalFare.TotalFare.CurrencyCode + "</td><td>&nbsp;</td></tr>";
                            //dynamicDetails += "<tr><td colspan='3'>Please note: All fares are quoted in CAD. Some airlines may charge baggage fees.</td></tr>";
                        }
                    });
                    dynamicDetails += "</tbody>";
                    dynamicDetails += "</table>";
                    dynamicDetails += "</div>";
                    return false;
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

$(function () {

    $("select#selPICCType").change();
});