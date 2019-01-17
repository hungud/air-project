$(function ($) {
    try {
        ///==================================================//
        ///==================================================//
        $("#txtdepartureDate").datepicker({
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate").click(function () {
            $("#txtdepartureDate").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtreturnDate").datepicker({
            startDate: $("#txtreturnDate").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_returnDate").click(function () {
            $("#txtreturnDate").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtdepartureDate_1").datepicker({
            startDate: $("#txtdepartureDate_1").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate_1").click(function () {
            $("#txtdepartureDate_1").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtdepartureDate_2").datepicker({
            startDate: $("#txtdepartureDate_2").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate_2").click(function () {
            $("#txtdepartureDate_2").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtdepartureDate_3").datepicker({
            startDate: $("#txtdepartureDate_3").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate_3").click(function () {
            $("#txtdepartureDate_3").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtdepartureDate_4").datepicker({
            startDate: $("#txtdepartureDate_4").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate_4").click(function () {
            $("#txtdepartureDate_4").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtdepartureDate_5").datepicker({
            startDate: $("#txtdepartureDate_5").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate_5").click(function () {
            $("#txtdepartureDate_5").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txt_applicationNumber,#txt_pinNumber,#txtQID").keyup(function (e) {
            $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                return false;
            }
        });
        ///==================================================//
        ///==================================================//
        $("#txtOrigin").autocomplete({
            source: function (request, response) {
                GetAutoCompleteDetails(request, response);
            },
            delay: 500,
            limit: 5,
            minLength: 2
        });
        $("#txtDestination").autocomplete({
            source: function (request, response) {
                GetAutoCompleteDetails(request, response);
            },
            delay: 500,
            limit: 5,
            minLength: 2
        });
        ///==================================================//
        ///==================================================//
    } catch (e) { }
});


var AppData;
function ReicivedPermitDetails(id, pinNo) {
    try {
        ConfirmBootBox("Error", "Successfully: ", 'App_Success', initialCallbackYes, initialCallbackNo);
    } catch (e) {
        console.log(e);
        HideWaitProgress();
    }
}

function GetResultAction(FlightNumberID) {
    try {
        ConfirmBootBox("Successfully", "Successfully FlightNumber ID : " + FlightNumberID, 'App_Success', initialCallbackYes, initialCallbackNo);
    } catch (e) {
        var error = e;
        error = e;
    }
}
function ModuleLoadJSONData(JSONData) {
    try {
        function GetBindAllContractorUserList(DataTable_Data, DataTable_Result_Contant) {
            var myResultDataArray = new Array();

            try {
                $.each(DataTable_Data.PricedItineraries, function (dt_key, dt_value) {
                    $.each(dt_value.AirItineraryPricingInfo, function (dt_AIkey, dt_AIvalue) {
                        var SSDataAction = "";
                        if (dt_AIvalue.PTC_FareBreakdown) {
                            $.each(dt_AIvalue, function (dt_AAIkey, dt_AAIvalue) {
                                myResultDataArray.push({
                                    ArrivalAirportCode: dt_AAIvalue.FareBasisCodes.FareBasisCode[0].ArrivalAirportCode, DepartureAirportCode: dt_AAIvalue.FareBasisCodes.FareBasisCode[0].DepartureAirportCode,
                                    BaseFare: dt_AAIvalue.PassengerFare.BaseFare.Amount, Taxes: (dt_AAIvalue.PassengerFare.TotalFare.Amount - dt_AAIvalue.PassengerFare.BaseFare.Amount), TotalFare: dt_AAIvalue.PassengerFare.TotalFare.Amount
                                });
                            });
                        }
                    });
                });
                $.each(myResultDataArray, function (dt_key, dt_value) {
                    var DataAction = "<td><div class=' action-buttons myclick '><a href='javascript:void(0)' id=" + dt_value.FlightNumber + " onclick='GetResultAction(" + dt_value.FlightNumber + ")' title='Edit Detail'><i class='ace-icon fa fa-play bigger-120'></i></a>";
                    $("#" + DataTable_Result_Contant + "").dataTable().fnAddData([
                                 nullEmptyToHyphen(dt_value.DepartureAirportCode),
                                  nullEmptyToHyphen(dt_value.ArrivalAirportCode),
                                 nullEmptyToHyphen(dt_value.BaseFare),
                                 nullEmptyToHyphen(dt_value.Taxes),
                                 nullEmptyToHyphen(dt_value.TotalFare),
                                DataAction]);
                });
            }
            catch (e) {
                var error = e;
                error = e;
            }
        }
        function GetBindAllContractorUserList1(DataTable_Data, DataTable_Result_Contant) {
            var myResultArray = new Array();
            try {
                $.each(DataTable_Data.PricedItineraries, function (dt_key, dt_value) {
                    $.each(dt_value.AirItinerary.OriginDestinationOptions, function (dt_skey, dt_svalue) {
                        var SSDataAction = "";
                        $.each(dt_svalue, function (dt_sskey, dt_ssvalue) {
                            var SSSSDataAction = dt_ssvalue.FlightSegment[0].FlightNumber;
                            $.each(dt_ssvalue.FlightSegment, function (dt_ssskey, dt_sssvalue) {
                                myResultArray.push({
                                    ArrivalAirport: dt_sssvalue.ArrivalAirport.LocationCode,
                                    ArrivalDateTime: dt_sssvalue.ArrivalDateTime,
                                    DepartureAirport: dt_sssvalue.DepartureAirport.LocationCode,
                                    DepartureDateTime: dt_sssvalue.DepartureDateTime,
                                    FlightNumber: dt_sssvalue.FlightNumber,
                                    TPA_Extensions: dt_sssvalue.TPA_Extensions.eTicket.Ind,
                                    OperatingAirline: dt_sssvalue.OperatingAirline.Code,
                                    OperatingAirlineFlightNumber: dt_sssvalue.OperatingAirline.FlightNumber
                                });
                            });
                        });
                    });
                });
                $.each(myResultArray, function (dt_key, dt_value) {
                    var DataAction = "<td><div class=' action-buttons myclick '><a href='javascript:void(0)' id=" + dt_value.FlightNumber + " onclick='GetResultAction(" + dt_value.FlightNumber + ")' title='Edit Detail'><i class='ace-icon fa fa-play bigger-120'></i></a>";
                    $("#" + DataTable_Result_Contant + "").dataTable().fnAddData([
                                 nullEmptyToHyphen(dt_value.ArrivalAirport),
                                 nullEmptyToHyphen(dt_value.ArrivalDateTime),
                                 nullEmptyToHyphen(dt_value.DepartureAirport),
                                 nullEmptyToHyphen(dt_value.DepartureDateTime),
                                 nullEmptyToHyphen(dt_value.FlightNumber),
                                 nullEmptyToHyphen(dt_value.TPA_Extensions),
                                 nullEmptyToHyphen(dt_value.OperatingAirline),
                                 nullEmptyToHyphen(dt_value.OperatingAirlineFlightNumber),
                                DataAction]);
                });
            }
            catch (e) {
                var error = e;
                error = e;
            }
        }
        function GetOnLoadInitializeAllContractorUserList(App_DataTable) {
            try {
                var Data_Column = [{ sTitle: "ArrivalAirport", sWidth: "15px" }, { sTitle: "ArrivalDateTime", sWidth: "30px" }, { sTitle: "DepartureAirport", sWidth: "25px" }, { sTitle: "DepartureAirport", sWidth: "30px" }, { sTitle: "FlightNumber", sWidth: "20px" }, { sTitle: "TPA_Extensions", sWidth: "20px" }, { sTitle: "OperatingAirline", sWidth: "20px" }, { sTitle: "OAirlineNumnbe", sWidth: "20px" }, { sTitle: "Action", sWidth: "10px" }];
                var Data_Columns = [{ sTitle: "Origin", sWidth: "25px" }, { sTitle: "Destination", sWidth: "25px" }, { sTitle: "BaseFare", sWidth: "100px" }, { sTitle: "Taxes", sWidth: "100px" }, { sTitle: "TotalFare", sWidth: "100px" }, { sTitle: "Action", sWidth: "10px" }];
                MasterRespansiveInitGenericDataTable("#Page-josn-result1", 'BootStrapTableRespansive-result', App_DataTable, Data_Columns);
                GetBindAllContractorUserList(App_DataTable, 'BootStrapTableRespansive-result');

                MasterRespansiveInitGenericDataTable("#Page-josn-result2", 'BootStrapTableRespansive-result1', App_DataTable, Data_Column);
                GetBindAllContractorUserList1(App_DataTable, 'BootStrapTableRespansive-result1');
                HideWaitProgress();

            } catch (e) {
                var error = e;
                error = error;
                HideWaitProgress();
            }
        }
        GetOnLoadInitializeAllContractorUserList(JSON.parse(JSONData));
    }
    catch (e) { }
}
function GetAutoCompleteDetails(request, response) {
    try {
        var availableTags = ["ActionScript", "AppleScript", "Asp", "BASIC", "C", "C++", "Clojure", "COBOL", "ColdFusion", "Erlang", "Fortran", "Groovy", "Haskell", "Java", "JavaScript", "Lisp", "Perl", "PHP", "Python", "Ruby", "Scala", "Scheme"];
        ConfirmBootBox("Successfully", "Successfully request  : " + request + " : Successfully request  : " + response, 'App_Success', initialCallbackYes, initialCallbackNo);
        response($.map(availableTags, function (item) {
            return { value: item, data: item };
            //if (item.contains(request)) {
            //    return { value: item, data: item };
            //}
            //if(item.Search(request))
            //{
            //    return {item }
            //}
            //items = [];
            //map = {};
            //$.each(data.d, function (i, item) {
            //    var id = item.split('-')[1];
            //    var name = item.split('-')[0];
            //    map[name] = { id: id, name: name };
            //    items.push(name);
            //});
            //response(items);
            $(".dropdown-menu").css("height", "auto");
        }));
        //$.ajax({
        //    url: '@Url.Content("~/Employee/SearchEmployee")/',
        //    type: 'POST',
        //    contentType: 'application/json',
        //    dataType: "json",
        //    data: JSON.stringify({
        //        employerId: 1,
        //        searchStr: me.val()
        //    }),
        //    success: function (data) {
        //        if (data.success) {
        //            response($.map(data.data, function (item) {
        //                return {
        //                    label: "(" + item.EmployeeNumber + ") " +
        //                                 item.FirstName + " " +
        //                                 item.MothersLast + ", " +
        //                                 item.FathersLast,
        //                    employeeId: item.EmployeeId
        //                }
        //            }));
        //        }
        //    }
        //});

    } catch (e) {
        HideWaitProgress();
    }
}
function ReicivedDetails(id, ProductID) {
    try {
        ConfirmBootBox("Successfully", "Successfully Fatched ID : " + ProductID, 'App_Success', initialCallbackYes, initialCallbackNo);
    } catch (e) {
        HideWaitProgress();
    }
}
function OnLoadGetDataList() {
    try {
        $("#Page-josn-result").empty();
        function GetJSONList(DataTable_Data) {
            SolutionDataTraveler("SET", "InstaFlightsSearchResult", JSON.parse(DataTable_Data));
            MasterRespansiveJSONDataManagement("#Page-josn-result", JSON.parse(DataTable_Data));
        }


        function ResultCallBackSuccess(e, xhr, opts) {
            HideWaitProgress();
            $(".SearchJSONResult").show();
            var App_Data = e.Data;
            GetJSONList(App_Data.Data);
            ModuleLoadJSONData(App_Data.Data);
        }
        function ResultCallBackError(e, xhr, opts) {
            HideWaitProgress();
            ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
        }
        var ReqURL = "";
        var Reqst_Resource = {
            origin: $('#txtOrigin').val(),
            destination: $('#txtDestination').val(),
            departuredate: $('#txtdepartureDate').val(),
            returndate: $('#txtreturnDate').val(),
            onlineitinerariesonly: 'Y',
            limit: 150,
            offset: 1,
            eticketsonly: 'Y',
            sortby: 'totalfare',
            order: 'asc',
            sortby2: 'departuretime',
            order2: 'asc',
            pointofsalecountry: 'US',
            passengercount: 1,
        };
        var servicelocation = (!location.port.trim() ? location.href.replace("AppsTravel", "ConfigService") : location.origin.replace("11011", "11012/"));
        //servicelocation = "http://airservice.nanojot.com/";
        MasterAppConfigurationsServices("POST", servicelocation + "API/InstaFlightsSearch", JSON.stringify(Reqst_Resource), ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var error = e;
        error = error;
    }
}
$(document).ready(function () {
    //OnLoadGetDataList();
});

$(document).ready(function () {
    $(".SearchJSONResult").hide();
    $(".btn-success").on('click', function () {
        ShowWaitProgress();
        if (ValidateData()) {
            OnLoadGetDataList();
        }
    });
});
function ValidateData() {
    if ($('#txtOrigin').val().trim() == "") {
        ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
        $("#txtOrigin").focus();
        HideWaitProgress();
        return false;
    }
    if ($('#txtDestination').val().trim() == "") {
        ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
        $("#txtDestination").focus();
        HideWaitProgress();
        return false;
    }
    if ($('#txtdepartureDate').val().trim() == "") {
        ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
        $("#txtdepartureDate").focus();
        HideWaitProgress();
        return false;
    }
    if ($('#txtreturnDate').val().trim() == "") {
        ConfirmBootBox("Message", "Please Enter Return Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
        $("#txtreturnDate").focus();
        HideWaitProgress();
        return false;
    }
    return true;
}



$(document).ready(function () {
    $("input[name='radioSelectionName']").on("click", function (event) {
        ConfirmBootBox("Error", $(this).val() + " : Successfully: ", 'App_Success', initialCallbackYes, initialCallbackNo);
        switch ($(this).val()) {
            case "1":
                $('#txtreturnDate').attr('readonly', true);
                $(".myrow_1").hide();
                break;
            case "2":
                $('#txtreturnDate').attr('readonly', false);
                $(".myrow_2").hide();
                break;
            case "3":
                $('#txtreturnDate').attr('readonly', false);
                $(".myrow_3").show();
                break;
        }
    });
    $("input[id='txtreturnDate']").on("click", function (event) {
        $('#txtreturnDate').attr('readonly', false);
    });
    var arr = [1, 2, 3, 4, 5];
    $.each(arr, function (index, value) {
         $(".myrow_" + value).hide();
    });
    
    var max_fields = 5; //maximum input boxes allowed
    var add_button = $(".add_more_field_button"); //Add button ID
    var x = 1; //initlal text box count
    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            $(".myrow_" + x).show();
        }
    });
});



