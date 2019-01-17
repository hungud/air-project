/////////////////////////////////////////Master JQuery Ajax Property//////////////////////////////////////////////////////////////////////////////////////////////
function CallBackSuccess(e, xhr, opts) {
    //alert('Yes');
}
//
function CallBackError(e, xhr, opts) {
    //alert('No');
}


function MasterAppConfigurationsServices(ActionKey, ServicesURL, ConfigParamData, CallBackSuccess, CallBackError) {
    try {
        
        //var headers = { Accept: 'application/vvv.website+json;version=1 ', Authorization: 'Token token=\"FuHCLyY46\"', 'SecurityToken': this.session.content.token, 'ConfigSecurityToken': '450202B8-EAA3-45FF-87E8-EE6CF8146E9C' }
        //var headers = { Accept: 'application/vvv.website+json;version=1 ', Authorization: 'Tokentoken=\"FuHCLyY46\"', 'SecurityToken': '450202B8-EAA3-45FF-87E8-EE6CF8146E9C', 'ConfigSecurityToken': '450202B8-EAA3-45FF-87E8-EE6CF8146E9C' };
        var headers = { 'CORS': 'Access-Control-Allow-Origin', 'token': 'tokenStringHere', 'Accept': 'application/vvv.website+json;version=1 ', 'Authorization': 'Tokentoken=\"FuHCLyY46\"', 'SecurityToken': '450202B8-EAA3-45FF-87E8-EE6CF8146E9C', 'ConfigSecurityToken': '450202B8-EAA3-45FF-87E8-EE6CF8146E9C' };
        ActionKey = ActionKey.toUpperCase();
        switch (ActionKey) {
            case "GETAUTO": 
                $.ajax({
                    url: ServicesURL,
                    type: "GET",
                    async: true,
                    cache: false,
                    crossDomain: true,
                    contentType: "application/json",
                    dataType: "json",
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                        // e.setRequestHeader(headers);
                        //e.setRequestHeader('Token', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('Authorization', 'Bearer  RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('User-Authorization-Token', 'Bearer  RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('Authorization-Token', 'Bearer RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('X-HTTP-Method-Override', 'GET');
                        //e.setRequestHeader('Authorization', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('ConfigSecurityToken', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('SecurityToken', 'Bearer : 450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }
                });
                break;
            case "GET": ShowWaitProgress();
                $.ajax({
                    url: ServicesURL,
                    type: "GET",
                    async: true,
                    cache: false,
                    crossDomain: true,
                    contentType: "application/json",
                    dataType: "json",
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                        // e.setRequestHeader(headers);
                        //e.setRequestHeader('Token', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('Authorization', 'Bearer  RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('User-Authorization-Token', 'Bearer  RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('Authorization-Token', 'Bearer RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        e.setRequestHeader('X-HTTP-Method-Override', 'GET');
                        //e.setRequestHeader('Authorization', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('ConfigSecurityToken', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('SecurityToken', 'Bearer : 450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        //HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        debugger;
                      //  HideWaitProgress();
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }
                });
                break;
            case "POST": 
                $.ajax({
                    url: ServicesURL,
                    type: "POST",
                    async: true,
                    cache: false,
                    crossDomain: true,
                    contentType: "application/json",
                    dataType: "json",
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                        //e.withCredentials = true;
                        //e.setRequestHeader('Token', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('User-Authorization-Token', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('Authorization', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('ConfigSecurityToken', 'Bearer : RAKESHPALRMSI-450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                        //e.setRequestHeader('SecurityToken', 'Bearer : 450202B8-EAA3-45FF-87E8-EE6CF8146E9C');
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                      //  HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        debugger;
                        //HideWaitProgress();
                        if (e.responseText == "Authentication Required") {
                            var urlpart = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=/";
                            var urlpart2 = $.urlParam('Search');
                            var url = urlpart + urlpart2;
                            window.location.href = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=" + CommonConfiguration.AirProjectURL + $.urlParam('Search');
                            return false;
                        }
                        // Handle the beforeSend event
                        CallBackError(e, xhr, opts);
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }
                });
                break;
            case "PUT": ShowWaitProgress();
                $.ajax({
                    url: ServicesURL,
                    type: "PUT",
                    async: false,
                    cache: false,
                    crossDomain: true,
                    contentType: "application/json",
                    dataType: "json",
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        debugger;
                        HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        debugger;
                        HideWaitProgress();
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }
                });
                break;
            case "DELETE": ShowWaitProgress();
                $.ajax({
                    url: ServicesURL,
                    type: "DELETE",
                    async: false,
                    cache: false,
                    crossDomain: true,
                    contentType: "application/json",
                    dataType: "json",
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        HideWaitProgress();
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }
                });
                break;
            case "FILEUPLOAD": ShowWaitProgress();
                $.ajax({
                    type: "POST",
                    url: ServicesURL,
                    contentType: false,
                    processData: false,
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        HideWaitProgress();
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }

                });
                break;
            case "FIEUPLOADS": ShowWaitProgress();
                $.ajax({
                    type: "POST",
                    url: ServicesURL,
                    contentType: "application/json",
                    autoUpload: true,
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        HideWaitProgress();
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }
                });
                break;
            case "MOVEFILE": ShowWaitProgress();
                $.ajax({
                    type: "POST",
                    url: ServicesURL,
                    contentType: 'application/json; charset=utf-8',
                    data: ConfigParamData,
                    beforeSend: function (e, xhr, opts) {
                        //$.blockUI({message: options.AjaxWait.AjaxWaitMessage,css: options.AjaxWait.AjaxWaitMessageCss});
                    },
                    ajaxSend: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    success: function (e, xhr, opts) {
                        HideWaitProgress();
                        //var Q_Data = JSON.parse(e);
                        var Q_Data = e;
                        if (CallBackSuccess) {
                            CallBackSuccess(e, xhr, opts);
                        }
                    },
                    ajaxSuccess: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    error: function (e, xhr, opts) {
                        HideWaitProgress();
                        // Handle the beforeSend event
                        if (CallBackError) {
                            CallBackError(e, xhr, opts);
                        }
                    },
                    ajaxError: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    complete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxComplete: function (e, xhr, opts) {
                        // Handle the beforeSend event
                    },
                    ajaxStop: function () {
                        //$.unblockUI();
                    }

                });
                break;
            case "AJAXCONFIGS": ShowWaitProgress();
                $.ajax(ConfigParamData).done(function (res, xhr, opts) {
                    HideWaitProgress();
                    if (CallBackSuccess) {
                        CallBackSuccess(res, xhr, opts);
                    }
                }).success(function (res, xhr, opts) {
                    HideWaitProgress();
                }).error(function (res, xhr, opts) {
                    HideWaitProgress();
                    if (CallBackError) {
                        CallBackError(res, xhr, opts);
                    }
                }).complete(function (res, xhr, opts) {
                    HideWaitProgress();
                });
                break;
            default:
                if (CallBackError) {
                    CallBackError();
                }
        }
    }
    catch (e) {
        var ex = e;
        ex = e;
    }
}


/////////////////////////////////////////Master JQuery Ajax Property//////////////////////////////////////////////////////////////////////////////////////////////



function GetSabreAuthorizeUserToken() {
    try {
        var TokenSettings = {
            "async": true,
            "crossDomain": true,
            "url": "https://api.test.sabre.com/v2/auth/token",
            "method": "POST",
            "headers": {
                "authorization": "Basic VmpFNmFHOHhNV2x3T1ROa04yOXBNWGszWWpwRVJWWkRSVTVVUlZJNlJWaFU6VjJ0VE4zSTNjRTQ9",
                "content-type": "application/x-www-form-urlencoded",
                "cache-control": "no-cache"
            },
            "processData": false,
            "data": "grant_type=client_credentials"
        }
        $.ajax(TokenSettings).done(function (res, xhr, opts) {
            SolutionDataTraveler("SET", "AuthToken", res["access_token"]);
            SolutionDataTraveler("SET", "AuthTokenExpires_in", res["expires_in"]);
            SolutionDataTraveler("SET", "AuthToken_type", res["token_type"]);
            //var expirationDate = Date.today().setTimeToNow().addSeconds(res["expires_in"]);
            GetReqBFMX();
        }).error(function (res, xhr, opts) {
            console.log(res);
        }).success(function (res, xhr, opts) {
            //var Q_Data = JSON.parse(e);
            var Q_Data = res;
        });

        function ResultCallBackSuccess(res, xhr, opts) {
            var App_Data = res;
            SolutionDataTraveler("SET", "AuthToken", res["access_token"]);
            SolutionDataTraveler("SET", "AuthTokenExpires_in", res["expires_in"]);
            SolutionDataTraveler("SET", "AuthToken_type", res["token_type"]);
            GetReqBFMX();
        }
        function ResultCallBackError(res, xhr, opts) {
            var App_Data = res;
        }
       
        var servicelocation = (!location.port.trim() ? location.href.replace("AppsTravel", "ConfigService") : location.origin.replace("11011", "11012/"));
        //servicelocation = "http://airservice.nanojot.com/";
        MasterAppConfigurationsServices("AJAXCONFIGS", servicelocation + "API/BargainFinderMaxSearch", TokenSettings, ResultCallBackSuccess, ResultCallBackError);

    } catch (e) {
        var error = e;
        error = error;
    }
}



function GetReqBFMX() {
    try {
        var data = JSON.stringify({
            "OTA_AirLowFareSearchRQ": {
                "OriginDestinationInformation": [
                  {
                      "RPH": "1",
                      "DepartureDateTime": "2016-11-25T00:00:00",
                      "OriginLocation": {
                          "LocationCode": "LAX"
                      },
                      "DestinationLocation": {
                          "LocationCode": "JFK"
                      }
                  }
                ],
                "TPA_Extensions": {
                    "IntelliSellTransaction": {
                        "RequestType": {
                            "Name": "50ITINS"
                        }
                    }
                },
                "TravelerInfoSummary": {
                    "AirTravelerAvail": [
                      {
                          "PassengerTypeQuantity": [
                            {
                                "Code": "ADT",
                                "Quantity": 1
                            }
                          ]
                      }
                    ]
                },
                "TravelPreferences": {
                    "TPA_Extensions": {}
                },
                "POS": {
                    "Source": [
                      {
                          "RequestorID": {
                              "Type": "0.AAA.X",
                              "ID": "REQ.ID",
                              "CompanyName": {
                                  "Code": "TN"
                              }
                          },
                          "PseudoCityCode": "6DTH"
                      }
                    ]
                }
            }
        });

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": "https://api.test.sabre.com/v1.9.0/shop/flights?mode=live",
            "method": "POST",
            "headers": {
                "authorization": "Bearer " + SolutionDataTraveler("GET", "AuthToken"),//T1RLAQLip8YH/p1slHKf1M/Nw2iIow3aTBBvcSiktcwQs5uqUpzLyKdjAADAPKA/eib7NIf66Cgql1IxweexaC4gbqb05sp/UlyuBOs3hk3sgFg2FnqRR7w2Fj0d3SkdxesaRj/ui2RNP4R9Xp8pNuAJu1yFw62ePROlbrs3mYLnUh3tCWXje8t56NFNUabuZ2uUdJIg9Qk6wzX4qQ48J2Zvp4sP7kqtcxV384dYjx4Pl+lgio+izuzsXWfJn8ej0K9Jv2KjAxMCVzyCJnc3qd2w0UtcuQ2khzfxNzcbDSH1BxJw/v0W0sDUbeAS
                "content-type": "application/json",
                "cache-control": "no-cache"
            },
            "data": "{\r\n  \"OTA_AirLowFareSearchRQ\": {\r\n    \"OriginDestinationInformation\": [{\r\n      \"RPH\": \"1\",\r\n      \"DepartureDateTime\": \"2016-11-25T00:00:00\",\r\n      \"OriginLocation\": {\r\n        \"LocationCode\": \"LAX\"\r\n      },\r\n      \"DestinationLocation\": {\r\n        \"LocationCode\": \"JFK\"\r\n      }\r\n    }],\r\n    \"TPA_Extensions\": {\r\n      \"IntelliSellTransaction\": {\r\n        \"RequestType\": {\r\n          \"Name\": \"50ITINS\"\r\n        }\r\n      }\r\n    },\r\n    \"TravelerInfoSummary\": {\r\n      \"AirTravelerAvail\": [{\r\n        \"PassengerTypeQuantity\": [{\r\n          \"Code\": \"ADT\",\r\n          \"Quantity\": 1\r\n        }]\r\n      }]\r\n    },\r\n    \"TravelPreferences\": {\r\n      \"TPA_Extensions\": {\r\n      }\r\n    },\r\n    \"POS\": {\r\n      \"Source\": [{\r\n        \"RequestorID\": {\r\n          \"Type\": \"0.AAA.X\",\r\n          \"ID\": \"REQ.ID\",\r\n          \"CompanyName\": {\r\n            \"Code\": \"TN\"\r\n          }\r\n        },\r\n        \"PseudoCityCode\": \"6DTH\"\r\n      }]\r\n    }\r\n  }\r\n}\r\n"
        }
        function ResultCallBackSuccess(res, xhr, opts) {
            var App_Data = res;
            SolutionDataTraveler("SET", "BFMXSearchResult", res);
        }
        function ResultCallBackError(res, xhr, opts) {
            var App_Data = res;
        }
        var servicelocation = (!location.port.trim() ? location.href.replace("AppsTravel", "ConfigService") : location.origin.replace("11011", "11012/"));
        MasterAppConfigurationsServices("AJAXCONFIGS", servicelocation + "API/BargainFinderMaxSearch", settings, ResultCallBackSuccess, ResultCallBackError);

    } catch (e) {
        var error = e;
        error = error;
    }
}


/////////////////////////////////////////Master BootStrap Respansive DataTable Property Initialization//////////////////////////////////////////////////////////////////////////////////////////////

function CallBackTableListDataBind(Respansive_Table_Data) {
    //alert('Yes');
}

function MasterRespansiveInitializeDataTable(DataTable_Result_Container, Boot_Table_Data, Boot_table_Colums) {
    try {
        var htmlStr = '<table id=\"BootStrapTableDataGridRespansive\" class="table display  dt-responsive table-bordered"  style=\"width:99%;\"></table>';
        $("" + DataTable_Result_Container + "").empty().html(htmlStr);
        $('#BootStrapTableDataGridRespansive').DataTable({
            ordering: false,
            lengthChange: false,
            bScrollCollapse: true,
            bFilter: true,
            searching: false,
            iDisplayLength: 10,
            paging: ((Boot_Table_Data.length > 10) ? true : false),
            info: false,
            bPaginate: false,
            oLanguage: { sEmptyTable: "Data Not Founds!", sSearch: "Search On Table:" },
            responsive: true,
            aoColumns: Boot_table_Colums,
            select: true
        });
    }
    catch (e) {
    }
}

function MasterRespansiveInitGenericDataTable(DataTable_Result_Container, DataTable_Result_Contant, Boot_Table_Data, Boot_table_Colums) {
    try {
        var htmlStr = "<table id=\"" + DataTable_Result_Contant + "\" class=\"table display  dt-responsive table-bordered\"  style=\"width:99%;\"></table>";
        $("" + DataTable_Result_Container + "").empty().html(htmlStr);
        $("#" + DataTable_Result_Contant + "").DataTable({
            ordering: false,
            lengthChange: false,
            bScrollCollapse: true,
            bFilter: true,
            searching: false,
            iDisplayLength:10,
            paging: ((Boot_Table_Data.length > 10) ? true : false),
            info: false,
            bPaginate: false,
            oLanguage: { sEmptyTable: "Data Not Founds!", sSearch: "Search On Table:" },
            responsive: true,
            aoColumns: Boot_table_Colums,
            select: true
        });
    }
    catch (e) {
    }
}

function MasterRespansiveInitializeDataTableVer1(DataTable_Result_Container, DataTable_Result_Row, Boot_Table_Data, Boot_table_Colums) {
    try {
        var htmlStr = '<table id=\"' + DataTable_Result_Row + '\" class="table display  dt-responsive table-bordered"  style=\"width:99%;\"></table>';
        $("" + DataTable_Result_Container + "").empty().html(htmlStr);
        $("#" + DataTable_Result_Row + "").DataTable({
            ordering: false,
            lengthChange: false,
            bScrollCollapse: true,
            bFilter: true,
            searching: false,
            iDisplayLength: 10,
            paging: ((Boot_Table_Data.length > 10) ? true : false),
            info: false,
            bPaginate: false,
            oLanguage: { sEmptyTable: "Data Not Founds!", sSearch: "Search On Table:" },
            responsive: true,
            aoColumns: Boot_table_Colums,
            select: true
        });
    }
    catch (e) {
    }
}

function MasterRespansiveTabActionManagement(index, TabAction_Result_Container, DataTable_Result_Contant) {
    $(TabAction_Result_Container).empty().html(DataTable_Result_Contant);
}

function MasterRespansiveJSONDataManagement(TabAction_Result_Container, DataTable_Result_Contant) {
    try {
        $(TabAction_Result_Container).empty().html(JSON.stringify(DataTable_Result_Contant));
        CommonslimScroll('.JSONResultContentScroll', '250px');
    }
    catch (e) {
        var error = e;
    }
}
/////////////////////////////////////////Master BootStrap Respansive DataTable Property Initialization//////////////////////////////////////////////////////////////////////////////////////////////
