var results = [];
function GetSearchAutoCompleteDetails(request, response) {
    try {
        function ResultCallBackSuccess(e, xhr, opts) {
            var App_Data = e.Data;
            response($.map(App_Data, function (item) {
                return {
                    label: item.airlinename,
                    val: item.airlinecode
                }
            }))
        }
        function ResultCallBackError(e, xhr, opts) {
            HideWaitProgress();
        }
        var Reqst_Resource = {
            CommonServiceType: "AirlinesCodeSearch",
            SearchText: request.term
        };
        MasterAppConfigurationsServices("GETAUTO", CommonConfiguration.WebAPIServicesURL + "API/CommonService/GetCommonService", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        HideWaitProgress();
    }
}

function GetOnloadSearchPageAutoCompleteDetails() {
    try {
        ///==================================================//
        ///==================================================//

        ///====================txtOrigin==============================//
        //$("#txtOrigin").autocomplete({
        //    source: function (request, response) {
        //        var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
        //        response($.grep(results, function (item) {
        //            return matcher.test(item);
        //        }));
        //        GetSearchAutoCompleteDetails(request, response);
        //    },
        //    delay: 1,
        //    limit: 5,
        //    minLength: 2
        //});
        ///====================txtDestination==============================//
        //$("#txtDestination").autocomplete({
        //    source: function (request, response) {
        //        var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
        //        response($.grep(results, function (item) {
        //            return matcher.test(item);
        //        }));
        //        GetSearchAutoCompleteDetails(request, response);
        //    },
        //    delay: 1,
        //    limit: 5,
        //    minLength: 2
        //});
        ///====================txtMLOrigin_1==============================//
        $("#txtMLOrigin_1").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLDestination_1==============================//
        $("#txtMLDestination_1").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLOrigin_2==============================//
        $("#txtMLOrigin_2").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLDestination_2==============================//
        $("#txtMLDestination_2").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLOrigin_3==============================//
        $("#txtMLOrigin_3").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLDestination_3==============================//
        $("#txtMLDestination_3").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLOrigin_4==============================//
        $("#txtMLOrigin_4").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLDestination_4==============================//
        $("#txtMLDestination_4").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLOrigin_5==============================//
        $("#txtMLOrigin_5").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///====================txtMLDestination_5==============================//
        $("#txtMLDestination_5").autocomplete({
            source: function (request, response) {
                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                response($.grep(results, function (item) {
                    return matcher.test(item);
                }));
                GetSearchAutoCompleteDetails(request, response);
            },
            delay: 1,
            limit: 5,
            minLength: 2
        });
        ///==================================================//
        ///==================================================//

    } catch (e) { }
}

function GetPaymetsAutoCompleteDetails(request, response) {
    try {
        debugger;
        function ResultCallBackSuccess(e, xhr, opts) {
            debugger;
            var App_Data = e.Data;
            
            response($.map(App_Data, function (item) {
                debugger;
                return {
                    label: item.airlinename,
                    val: item.airlinecode
                }
            }))
        }
        function ResultCallBackError(e, xhr, opts) {
            HideWaitProgress();
        }
        //var Reqst_Resource = {
        //    CommonServiceType: "AirlinesPaymentsCitySearch",
        //    SearchText: request.term
        //};
        var Reqst_Resource = {            
            prefix: request.term
        };
        debugger;
        MasterAppConfigurationsServices("GETAUTO", "http://search.nanojot.com/searchboxwebservice.asmx/Get_Destination_CityNameListJson", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
        //MasterAppConfigurationsServices("GETAUTO", CommonConfiguration.WebAPIServicesURL + "API/CommonService/GetCommonService", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        HideWaitProgress();
    }
}

function GetOnloadPaymetsPageAutoCompleteDetails() {
    try {
        ///==================================================//
        ///==================================================//

        ///====================txtOrigin==============================//
        //$("#selBCICountry").autocomplete({
        //    source: function (request, response) {
        //        var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
        //        response($.grep(results, function (item) {
        //            return matcher.test(item);
        //        }));
        //        GetPaymetsAutoCompleteDetails(request, response);
        //    },
        //    delay: 1,
        //    limit: 5,
        //    minLength: 2
        //});
       
    } catch (e) { }
}

$(function ($) {
    GetOnloadPaymetsPageAutoCompleteDetails();
    GetOnloadSearchPageAutoCompleteDetails();
});



/*jslint  browser: true, white: true, plusplus: true */
/*global $, countries */
var results = [];
$(function () {
    'use strict';


    // Initialize ajax autocomplete:
    //$('#selBCICountry').autocomplete({
    //    source: function (request, response) {
    //        var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
    //        response($.grep(results, function (item) {
    //            return matcher.test(item);
    //        }));
    //        console.log(this.id);

    //        $.ajax({
    //            cache: false,
    //            url: "http://search.nanojot.com/searchboxwebservice.asmx/Get_Destination_CityNameListJson",
    //            data: ({ prefix: request.term }),
    //            type: "POST",
    //            headers: {
    //                'Content-Type': 'application/x-www-form-urlencoded'
    //            },
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "xml",
    //            success: function (xml) {
    //                results = new Array();
    //                parseXmlCountry(xml);
    //                response($.map(results, function (item) {
    //                    return {
    //                        label: item.value,
    //                        val: item.key
    //                    }
    //                }))
    //            },

    //            error: function (response) {
    //                alert(response.responseText);
    //            },
    //            failure: function (response) {
    //                alert(response.responseText);
    //            }
    //        });

    //    },

    //    select: function (e, i) {
    //        $("#hfCustomerId").val(i.item.val);

    //    },
    //    minLength: 3,
    //    delay: 0
    //});

     

});
 

//function parseXmlCountry(xml) {

//    $(xml).find('CityStateCountry').each(function () {
//        //alert('parsexml : ' + $(this).val());
//        if ($(this).val() == false) {
//            //alert('thisval');
//            results.push({
//                value: $(this).find('CityName').text() + ", " + $(this).find('StateName').text() + ", " + $(this).find('CountryName').text(),
//                key: $(this).find('CountryName').text()
//            })
//        }
//    });

//}

 

