
$(function ($) {
    try {
        $("#txtdepartureDate").datepicker({
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_departureDate").click(function () {
            $("#txtdepartureDate").datepicker("show");
        });
        $("#txtreturnDate").datepicker({
            startDate: $("#txtreturnDate").val(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_returnDate").click(function () {
            $("#txtreturnDate").datepicker("show");
        });


        //GetApplicationnumber();
        $("#txt_applicationNumber,#txt_pinNumber,#txtQID").keyup(function (e) {
            $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                return false;
            }
        });

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

    } catch (e) { }
});

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
            SolutionDataTraveler("SET", "LoadPriceCalenderResult", DataTable_Data);
            MasterRespansiveJSONDataManagement("#Page-josn-result", DataTable_Data);
        }
        function ResultCallBackSuccess(e, xhr, opts) {
            HideWaitProgress();
            $(".SearchJSONResult").show();
            var App_Data = e.Data;
            GetJSONList(App_Data.Data);
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
            lengthofstay: $('#lengthOfStay').val(),
            minfare: $('#txtminFare').val(),
            maxfare: $('#txtmaxFare').val(),
            pointofsalecountry: $('#pointOfSaleCountry').val(),
        };
        var servicelocation = (!location.port.trim() ? location.href.replace("AppsTravel", "ConfigService") : location.origin.replace("11011", "11010/"));
        //servicelocation = "http://airservice.nanojot.com/";
        MasterAppConfigurationsServices("POST", servicelocation + "API/LeadPriceCalendar", JSON.stringify(Reqst_Resource), ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var error = e;
        error = error;
    }
}
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
    
    return true;
}
