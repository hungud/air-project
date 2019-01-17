var AppActionData = 0, SearchSelectionType = 1, BFMXAllFlightSegmentItineraryPricingResult = null;
var myPassengerDetailsListArray = new Array();

var _MS_PER_DAY = 1000 * 60 * 60 * 24;
// a and b are javascript Date objects
function dateDiffInDays(a, b) {
    // Discard the time and time-zone information.
    var utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
    var utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

    return Math.floor((utc2 - utc1) / _MS_PER_DAY);
}
Date.prototype.addDays = function (num) {
    var value = this.valueOf();
    value += 86400000 * num;
    return new Date(value);
}

Date.prototype.addSeconds = function (num) {
    var value = this.valueOf();
    value += 1000 * num;
    return new Date(value);
}

Date.prototype.addMinutes = function (num) {
    var value = this.valueOf();
    value += 60000 * num;
    return new Date(value);
}

Date.prototype.addHours = function (num) {
    var value = this.valueOf();
    value += 3600000 * num;
    return new Date(value);
}

Date.prototype.addMonths = function (num) {
    var value = new Date(this.valueOf());
    var mo = this.getMonth();
    var yr = this.getYear();
    mo = (mo + num) % 12;
    if (0 > mo) {
        yr += (this.getMonth() + num - mo - 12) / 12;
        mo += 12;
    }
    else
        yr += ((this.getMonth() + num - mo) / 12);

    value.setMonth(mo);
    value.setYear(yr);
    return value;
}
/******************************Fulter AirLineName*************************/
Array.prototype.FulterContainsAirLineName = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].FlightSegments[0].OperatingAirline.Code === v) return true;
    }
    return false;
};
Array.prototype.FulterUniqueAirLineName = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.FulterContainsAirLineName(this[i].FlightSegments[0].OperatingAirline.Code)) {
            arr.push(this[i]);
        }
    }
    return arr.sort();
}
/******************************Fulter AirLineName*************************/

/******************************Fulter AirLineName*************************/
Array.prototype.FulterContainsDepartureAirport = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].FlightSegments[0].DepartureAirport.LocationCode === v) return true;
    }
    return false;
};

Array.prototype.FulterUniqueDepartureAirport = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.FulterContainsDepartureAirport(this[i].FlightSegments[0].DepartureAirport.LocationCode)) {
            arr.push(this[i]);
        }
    }
    return arr.sort();
}
/******************************Fulter AirLineName*************************/

/******************************Bind Resut Module Data New*************************/
function FilteringBindDisplayResutData(Searched_Details_Data) {
    try {
        var dynamicDetails = "";
        var myPassenger_DetailsListArray = new Array();
        var AllFlightSegment_Array = new Array();
        var AllFlightSegmentOptions_Array = new Array();
        var FlightSegment_Array = new Array();
        if (Searched_Details_Data != 'undefined' && Searched_Details_Data != null) {
            var SearchedDetailsData = JSON.parse(Searched_Details_Data);
            if (SearchedDetailsData.Links.length > 0) {
                $.each(SearchedDetailsData.OTA_AirLowFareSearchRS.PricedItineraries, function (dt_key, dt_value) {
                    $.each(dt_value, function (dt_PIkey, dt_PIvalue) {
                        $.each(dt_PIvalue.AirItinerary.OriginDestinationOptions, function (AirItineraryODOID, AirItineraryODOValue) {
                            var AirItineraryPricingInfo_Array = new Array(), AirLinesAirPortsPricingDepartureArrival_Array = new Array(), Flight_Segment_ID, Flight_Segment_Direction;;
                            Flight_Segment_ID = GetNewGuid();
                            $.each(dt_PIvalue.AirItineraryPricingInfo, function (dt_AIPInfoID, dt_AIPInfoValue) {
                                AirItineraryPricingInfo_Array = {
                                    FareInfos: dt_AIPInfoValue.FareInfos,
                                    ItinTotalFare: dt_AIPInfoValue.ItinTotalFare,
                                    LastTicketDate: dt_AIPInfoValue.LastTicketDate,
                                    PTC_FareBreakdowns: dt_AIPInfoValue.PTC_FareBreakdowns,
                                    PricingSource: dt_AIPInfoValue.PricingSource,
                                    PricingSubSource: dt_AIPInfoValue.PricingSubSource,
                                    TPA_Extensions: dt_AIPInfoValue.TPA_Extensions
                                };
                            });
                            AllFlightSegmentOptions_Array = new Array();
                            Flight_Segment_Direction = dt_PIvalue.AirItinerary.DirectionInd;
                            $.each(AirItineraryODOValue, function (dt_AIODOkey, dt_AIODOValue) {
                                var AIODOElapsedTime = dt_AIODOValue.ElapsedTime;
                                FlightSegment_Array = new Array();
                                $.each(dt_AIODOValue.FlightSegment, function (dt_FSKey, dt_FSValue) {
                                    FlightSegment_Array.push({
                                        FlightSegmentID: Flight_Segment_ID,
                                        ArrivalAirport: dt_FSValue.ArrivalAirport,
                                        ArrivalDateTime: dt_FSValue.ArrivalDateTime,
                                        ArrivalTimeZone: dt_FSValue.ArrivalTimeZone,
                                        DepartureAirport: dt_FSValue.DepartureAirport,
                                        DepartureDateTime: dt_FSValue.DepartureDateTime,
                                        DepartureTimeZone: dt_FSValue.DepartureTimeZone,
                                        ElapsedTime: dt_FSValue.ElapsedTime,
                                        Equipment: dt_FSValue.Equipment,
                                        FlightNumber: dt_FSValue.FlightNumber,
                                        MarketingAirline: dt_FSValue.MarketingAirline,
                                        MarriageGrp: dt_FSValue.MarriageGrp,
                                        OnTimePerformance: dt_FSValue.OnTimePerformance,
                                        OperatingAirline: dt_FSValue.OperatingAirline,
                                        ResBookDesigCode: dt_FSValue.ResBookDesigCode,
                                        StopAirports: dt_FSValue.StopAirports,
                                        StopQuantity: dt_FSValue.StopQuantity,
                                        TPA_Extensions: dt_FSValue.TPA_Extensions,
                                        AirItineraryPricingInfo: AirItineraryPricingInfo_Array,
                                        TicketingInfo: dt_PIvalue.TicketingInfo,
                                        TPA_Extensions: dt_PIvalue.TPA_Extensions
                                    });
                                });
                                AllFlightSegmentOptions_Array.push({
                                    ElapsedTime: AIODOElapsedTime,
                                    FlightSegments: FlightSegment_Array
                                });
                            });
                            AllFlightSegment_Array.push({
                                FlightSegmentID: Flight_Segment_ID,
                                FlightSegmentDirection: Flight_Segment_Direction,
                                FlightSegments: AllFlightSegmentOptions_Array
                            });
                        });
                    });
                });
                SolutionDataTraveler("SET", "BFMXAllFlightSegmentItineraryPricingResult", AllFlightSegment_Array);
                BindResutModuleDataNew(AllFlightSegment_Array);
            }
        }
        else { HideWaitProgress(); }
    }
    catch (e) {
        var error = e;
        error = e;
        HideWaitProgress();
    }
}


function GetmarkUp() {
    var domainName = localStorage.getItem("domain");
    var servicepath = "http://localhost:7932/searchboxwebservice.asmx/GetMarkupByDomain";
    var markUp = '';
    $.ajax({
        cache: false,
        async: false,
        url: servicepath,
        data: ({ domain: 'nano.com' }),
        type: "POST",
        cors: true,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "xml",
        success: function (xml) {

            $(xml).find('MarkUp').each(function () {
                var publishedFareMarkup = $(this).find('PublishedFareMarkup').text()
                var netfareMarkup = $(this).find('NetfareMarkup').text();
                markUp = netfareMarkup + ',' + publishedFareMarkup;

            });

        },
        error: function (response) {
            // alert(response.responseText);
        },
        failure: function (response) {
            //  alert(response.responseText);
        }
    });
    return markUp;
}



function GetBlockedAirlines() {
    var domainName = localStorage.getItem("domain");
    var servicepath = "http://localhost:7932/searchboxwebservice.asmx/GetBlockedAirlines";
    var blockedAirlines = '';
    $.ajax({
        cache: false,
        async: false,
        url: servicepath,
        data: ({ domain: 'nano.com' }),
        type: "POST",
        cors: true,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "xml",
        success: function (xml) {
            debugger;
            blockedAirlines = $(xml).find('string').text();
             

        },
        error: function (response) {
            // alert(response.responseText);
        },
        failure: function (response) {
            //  alert(response.responseText);
        }
    });
    return blockedAirlines;
}



function BindDisplayResutModuleDataNew(FilterAllFlightSegment_Array) {
    try {
        var markup = GetmarkUp();
        var netfareMarkup = 0;
        if ($.trim(markup) != '') {
            netfareMarkup = markup.split(",")[0];
        }
        var blockedAirlines = GetBlockedAirlines();
        debugger;
        var dynamicDetails = "";
        if (FilterAllFlightSegment_Array.length > 0) {
            $.each(FilterAllFlightSegment_Array, function (FlightSegment_key, FlightSegment_value) {

                var Flight_Segment_ID = FlightSegment_value.FlightSegmentID;
                var Allkey = FlightSegment_key, FlightNumberStop = '';
                var Hour = 0, Hours = 0, Minute = 0, Minutes = 0, StopoverHours = 0, StopoverMinutes = 0, FinalMinutes = 0;
                var NoOfStops = 0, cntStops = 1;
                var total = parseFloat(FlightSegment_value.FlightSegments[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.EquivFare.Amount) + parseFloat(netfareMarkup);
                var finalTotal = parseFloat(FlightSegment_value.FlightSegments[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount) + parseFloat(netfareMarkup);
                dynamicDetails += "<div class='table-responsive'>";
                dynamicDetails += "<table class='table without_tab'><thead><tr>";
                dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong>  Final Total Price(incl fee)  :     " + FlightSegment_value.FlightSegments[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.EquivFare.CurrencyCode + "$" + total.toFixed(2) + " + " + FlightSegment_value.FlightSegments[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + "$" + FlightSegment_value.FlightSegments[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + FlightSegment_value.FlightSegments[0].FlightSegments[0].AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "$" + finalTotal.toFixed(2) + "</strong><br>  </div>";
                dynamicDetails += "<div class='col-md-3 rt-align'><button class='btn btn-warning myAirCartSelector btn btn-sm btn-round'  value='btn-sm' type='button' data-param='" + FlightSegment_value.FlightSegmentID + "'>Book Now <i class='fa fa-plane' aria-hidden='true'></i></button>";
                dynamicDetails += "</div></th></tr></thead>";
                dynamicDetails += "<tbody class='pink-bg'>";
                dynamicDetails += " <tr class='txt-yellow'>";
                dynamicDetails += " <td colspan='5'><i class='fa fa-check' aria-hidden='true'></i> Best Price Guarantee! </td>";
                dynamicDetails += "</tr>";
                dynamicDetails += "</tbody>";
                var flighthours = "";
                var flightminutes = "";
                var OriginDestinationElapsedTime = "";
                //userIsYoungerThan18 ? "Minor" : "Adult"
                $.each(FlightSegment_value.FlightSegments, function (All_key, All_value) {
                    FlightNumberStop = (All_value.FlightSegments.length == 1) ? "NonStop" : ((All_value.FlightSegments.length == 2) ? "1 Stop" : "2+ Stop");
                    var NoOfStops = 0, cntStops = 1;
                    var flights_StopsTime_List = new Array();
                    var FlitArrivalDateTimeforStopColculation = "";
                    var SegmentsElapsedTime = "";
                    OriginDestinationElapsedTime = All_value.ElapsedTime;
                    NoOfStops = All_value.FlightSegments.length;
                    var timeDifflapse = "";
                    var ArrivalTime = "";
                    var DepTime = "";
                    $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                        flighthours = "";
                        flightminutes = "";
                        if (FlitArrivalDateTimeforStopColculation != "") {
                            ArrivalTime = FlitArrivalDateTimeforStopColculation;
                            DepTime = Segment_value.DepartureDateTime;
                            timeDifflapse = timeDiff(DepTime, ArrivalTime);
                            Hour = (Math.floor(Math.abs(timeDifflapse.minutes) / 60));
                            Minute = (Math.abs(timeDifflapse.minutes) % 60);
                            dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Layover: " + Hour + "H " + Minute + "M </td></tr>";
                        }

                        FlitArrivalDateTimeforStopColculation = Segment_value.ArrivalDateTime;
                        SegmentsElapsedTime = Segment_value.ElapsedTime;
                        flighthours = Math.trunc(SegmentsElapsedTime / 60);
                        flightminutes = SegmentsElapsedTime % 60;

                        //Hour = (Math.floor(Math.abs(timeDiff(Segment_value.DepartureDateTime, Segment_value.ArrivalDateTime).minutes) / 60));
                        //Minute = (Math.abs(timeDiff(Segment_value.DepartureDateTime, Segment_value.ArrivalDateTime).minutes) % 60);

                        dynamicDetails += "<tr>";
                        dynamicDetails += "<td><img src='../Content/Images/Airlines_Logo/" + Segment_value.OperatingAirline.Code + ".gif'  alt=''></td>";
                        dynamicDetails += "<td><strong>" + Airline_Name(Segment_value.OperatingAirline.Code) + "</strong><br>  Flight # <strong>" + Segment_value.OperatingAirline.Code + "-" + Segment_value.OperatingAirline.FlightNumber + "</strong></td>";
                        dynamicDetails += "<td><strong>" + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.DepartureDateTime)) + "<br> " + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.ArrivalDateTime)) + "</strong></td>";
                        dynamicDetails += "<td><strong>" + AirPort_Name(Segment_value.DepartureAirport.LocationCode) + "<br> " + AirPort_Name(Segment_value.ArrivalAirport.LocationCode) + "</strong></td>";
                        dynamicDetails += "<td class='rt-align'>" + " Coach <br> " + flighthours + " Hours " + flightminutes + " Minutes</td>";
                        //dynamicDetails += "<td class='rt-align'><strong>" + FlightNumberStop + " Coach / " + flighthours + " Hours  <br>" + flightminutes + " Minutes</td>";
                        dynamicDetails += "</tr>";
                        if (cntStops < NoOfStops) {

                        }

                        cntStops = cntStops + 1;
                    });
                    FlitArrivalDateTimeforStopColculation = "";
                    var hours = Math.trunc(OriginDestinationElapsedTime / 60);
                    var minutes = OriginDestinationElapsedTime % 60;

                    dynamicDetails += "<tbody class='pink-bg'>";
                    dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + hours + "h " + minutes + "m (" + FlightNumberStop + ")</td></tr>";
                    //dynamicDetails += "<tr><td colspan='5'><i class='fa fa-clock-o' aria-hidden='true'></i> Total Trip Duration:  " + hours + "h " + minutes + "m" + FlightNumberStop + " (Flight: " + hours + "h " + minutes + "m)</td></tr>";
                    dynamicDetails += "</tbody>";

                });
                dynamicDetails += "</table>";
                dynamicDetails += "</div>";
            });
        }
        else {
            dynamicDetails += "<tr><td colspan='7' style='align-items: center; text-align: center;'> No Data found...</td></tr>";
        }
        $("#Result_Div_Panel").removeClass("hidden");
        $("#Search-Widgets-Result-Panel").empty().append(dynamicDetails);
    }
    catch (e) {
        var error = e;
        error = e;
        //HideWaitProgress();
    }
    SliderActionStatus = true;
}

function BindResutModuleDataNew(myResult_Data_Array) {
    try {
        BindDisplayResutModuleDataNew(myResult_Data_Array);
        BindFilterAirlinesModuleResutNew(myResult_Data_Array);
        BindFilterDepartingModuleResutNew(myResult_Data_Array);
    }
    catch (e) {
        var error = e;
        error = e;
        HideWaitProgress();
    }
    HideWaitProgress();
}

function BindFilterAirlinesModuleResutNew(myResultDataArray) {
    try {
        var dynamicDetails = "", DataStatus = 0;
        var OriginDestinationElapsedTime = "";
        var AllFlightSegment_Array = new Array();
        var FlightSegment_Array = new Array();
        if (myResultDataArray.length > 0) {
            $.each(myResultDataArray, function (All_key1, All_value1) {
                var Flight_Segment_ID = All_value1.FlightSegmentID;
                var Flight_Segment_Direction = All_value1.FlightSegmentDirection;
                FlightSegment_Array = new Array();
                var TicketingInfo = "";
                var TPA_Extensions = "";
                $.each(All_value1.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                    $.each(FlightSegment_value.FlightSegments, function (dt_FSKey, dt_FSValue) {
                        FlightSegment_Array.push({
                            FlightSegmentID: Flight_Segment_ID,
                            ArrivalAirport: dt_FSValue.ArrivalAirport,
                            ArrivalDateTime: dt_FSValue.ArrivalDateTime,
                            ArrivalTimeZone: dt_FSValue.ArrivalTimeZone,
                            DepartureAirport: dt_FSValue.DepartureAirport,
                            DepartureDateTime: dt_FSValue.DepartureDateTime,
                            DepartureTimeZone: dt_FSValue.DepartureTimeZone,
                            ElapsedTime: dt_FSValue.ElapsedTime,
                            Equipment: dt_FSValue.Equipment,
                            FlightNumber: dt_FSValue.FlightNumber,
                            MarketingAirline: dt_FSValue.MarketingAirline,
                            MarriageGrp: dt_FSValue.MarriageGrp,
                            OnTimePerformance: dt_FSValue.OnTimePerformance,
                            OperatingAirline: dt_FSValue.OperatingAirline,
                            ResBookDesigCode: dt_FSValue.ResBookDesigCode,
                            StopAirports: dt_FSValue.StopAirports,
                            StopQuantity: dt_FSValue.StopQuantity,
                            TPA_Extensions: dt_FSValue.TPA_Extensions,
                            AirItineraryPricingInfo: dt_FSValue.AirItineraryPricingInfo,
                            TicketingInfo: TicketingInfo,
                            TPA_Extensions: TPA_Extensions
                        });
                    });
                });
                AllFlightSegment_Array.push({
                    FlightSegmentID: Flight_Segment_ID,
                    FlightSegmentDirection: Flight_Segment_Direction,
                    FlightSegments: FlightSegment_Array
                });
            });

            var myUniquesResultDataArray = AllFlightSegment_Array.FulterUniqueAirLineName();
            $.each(myUniquesResultDataArray, function (All_key, All_value) {
                DataStatus = 0;
                $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                    if (DataStatus == 0) {
                        DataStatus = (DataStatus + 1);
                        dynamicDetails += "<div class='checkbox row'><div class='col-md-7' style='padding-right:0'><label><input type='checkbox' id=id='" + Segment_value.OperatingAirline.Code + "' name='AirlinesGroup' value='" + Segment_value.OperatingAirline.Code + "' checked=''>" + Airline_Name(Segment_value.OperatingAirline.Code) + " </label></div><div class='col-md-5' style='padding-left:0'></div></div>";
                    }
                });
            });
            $("#Div_Search-Result-Airlines").empty().append(dynamicDetails);
        }
    }
    catch (e) {
        //HideWaitProgress();
    }
}

function BindFilterDepartingModuleResutNew(myResultDataArray) {
    try {
        var dynamicDetails = "";
        var OriginDestinationElapsedTime = "";
        var AllFlightSegment_Array = new Array();
        var FlightSegment_Array = new Array();
        if (myResultDataArray.length > 0) {
            $.each(myResultDataArray, function (All_key1, All_value1) {
                var Flight_Segment_ID = All_value1.FlightSegmentID;
                var Flight_Segment_Direction = All_value1.FlightSegmentDirection;
                FlightSegment_Array = new Array();
                var TicketingInfo = "";
                var TPA_Extensions = "";
                $.each(All_value1.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                    $.each(FlightSegment_value.FlightSegments, function (dt_FSKey, dt_FSValue) {
                        FlightSegment_Array.push({
                            FlightSegmentID: Flight_Segment_ID,
                            ArrivalAirport: dt_FSValue.ArrivalAirport,
                            ArrivalDateTime: dt_FSValue.ArrivalDateTime,
                            ArrivalTimeZone: dt_FSValue.ArrivalTimeZone,
                            DepartureAirport: dt_FSValue.DepartureAirport,
                            DepartureDateTime: dt_FSValue.DepartureDateTime,
                            DepartureTimeZone: dt_FSValue.DepartureTimeZone,
                            ElapsedTime: dt_FSValue.ElapsedTime,
                            Equipment: dt_FSValue.Equipment,
                            FlightNumber: dt_FSValue.FlightNumber,
                            MarketingAirline: dt_FSValue.MarketingAirline,
                            MarriageGrp: dt_FSValue.MarriageGrp,
                            OnTimePerformance: dt_FSValue.OnTimePerformance,
                            OperatingAirline: dt_FSValue.OperatingAirline,
                            ResBookDesigCode: dt_FSValue.ResBookDesigCode,
                            StopAirports: dt_FSValue.StopAirports,
                            StopQuantity: dt_FSValue.StopQuantity,
                            TPA_Extensions: dt_FSValue.TPA_Extensions,
                            AirItineraryPricingInfo: dt_FSValue.AirItineraryPricingInfo,
                            TicketingInfo: TicketingInfo,
                            TPA_Extensions: TPA_Extensions
                        });
                    });
                });
                AllFlightSegment_Array.push({
                    FlightSegmentID: Flight_Segment_ID,
                    FlightSegmentDirection: Flight_Segment_Direction,
                    FlightSegments: FlightSegment_Array
                });
            });

            var myUniquesResultDataArray = AllFlightSegment_Array.FulterUniqueDepartureAirport();
            $.each(myUniquesResultDataArray, function (All_key, All_value) {
                $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                    dynamicDetails += "<div class='checkbox row'><div class='col-md-7' style='padding-right:0'><label><input type='checkbox' id=id='" + Segment_value.DepartureAirport.LocationCode + "' name='AirlinesGroup' value='" + Segment_value.DepartureAirport.LocationCode + "' checked=''>" + Segment_value.DepartureAirport.LocationCode + " </label></div><div class='col-md-5' style='padding-left:0'></div></div>";
                });
            });
            $("#Div_Search-Result-Departing").empty().append(dynamicDetails);
        }
    }
    catch (e) {
        //HideWaitProgress();
    }
}
/******************************Bind Resut Module Data New*************************/
function isValidCode(code, codes) {
    return ($.inArray(code, codes) > -1);
}

/******************************Bind Filter Airlines Module Result*************************/
function GetFilterAirlinesModuleResut() {
    try {
        var OriginDestinationElapsedTime = "";
        ShowWaitProgress();
        var myRefineResultDataArray = new Array(), FlightSegment_Array = new Array();
        var myAirportDataArray = new Array();
        var AllFlightSegmentOptions_Array = new Array();
        $('#Div_Search-Result-Airlines input[type="checkbox"]:checked').each(function () {
            myAirportDataArray.push($(this).val());
        });
        var isValidAirline = 0;
        BFMXAllFlightSegmentItineraryPricingResult = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
        if (BFMXAllFlightSegmentItineraryPricingResult != null) {
            $.each(BFMXAllFlightSegmentItineraryPricingResult, function (dt_key, dt_value) {
                var Flight_Segment_Direction = dt_value.FlightSegmentDirection;
                AllFlightSegmentOptions_Array = new Array();
                $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                    OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;
                    FlightSegment_Array = new Array();
                    isValidAirline = 0;
                    $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {
                        var DAirLine = Segment_value.OperatingAirline.Code;

                        if (isValidCode(DAirLine, myAirportDataArray)) {
                            isValidAirline = 1;
                            FlightSegment_Array.push(Segment_value);
                        }
                        else {
                            isValidAirline = 0; return;
                        }

                    });
                    if (isValidAirline == 1) {
                        AllFlightSegmentOptions_Array.push({
                            ElapsedTime: OriginDestinationElapsedTime,
                            FlightSegments: FlightSegment_Array
                        });
                    }
                    else
                        return;
                });
                if (isValidAirline == 1) {
                    myRefineResultDataArray.push({
                        FlightSegmentID: dt_value.FlightSegmentID,
                        FlightSegmentDirection: Flight_Segment_Direction,
                        FlightSegments: AllFlightSegmentOptions_Array
                    });
                }
            });

            if (myRefineResultDataArray.length > 0) {
                BindDisplayResutModuleDataNew(myRefineResultDataArray);
            }
            else { BindDisplayResutModuleDataNew(BFMXAllFlightSegmentItineraryPricingResult); }

        }
    }
    catch (e) {
        HideWaitProgress();
    }
    HideWaitProgress();
}
/******************************Bind Filter Airlines Module Result*************************/

/******************************Bind Filter Airports Module Result*************************/
function GetFilterAirportsModuleResut() {
    try {
        var OriginDestinationElapsedTime = "";
        ShowWaitProgress();
        var myRefineResultDataArray = new Array(), FlightSegment_Array = new Array();
        var myAirportDataArray = new Array();
        var AllFlightSegmentOptions_Array = new Array();
        $('#Div_Search-Result-Departing input[type="checkbox"]:checked').each(function () {
            myAirportDataArray.push($(this).val());
        });
        var isValidAirline = 0;
        BFMXAllFlightSegmentItineraryPricingResult = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
        if (BFMXAllFlightSegmentItineraryPricingResult != null) {
            $.each(BFMXAllFlightSegmentItineraryPricingResult, function (dt_key, dt_value) {

                AllFlightSegmentOptions_Array = new Array();
                var SAirport = "";
                isValidAirline = 0;
                $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                    FlightSegment_Array = new Array();
                    OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;

                    $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {

                        var DAirLine = Segment_value.DepartureAirport.LocationCode;

                        if (isValidCode(DAirLine, myAirportDataArray)) {
                            isValidAirline = 1;
                            FlightSegment_Array.push(Segment_value);
                        }
                        else {
                            isValidAirline = 0; return;
                        }
                    });
                    if (isValidAirline == 1) {
                        AllFlightSegmentOptions_Array.push({
                            ElapsedTime: OriginDestinationElapsedTime,
                            FlightSegments: FlightSegment_Array
                        });
                    }
                    else
                        return;
                });
                if (isValidAirline == 1) {
                    myRefineResultDataArray.push({
                        FlightSegmentID: dt_value.FlightSegmentID,
                        FlightSegmentDirection: dt_value.FlightSegmentDirection,
                        FlightSegments: FlightSegment_value
                    });
                }
            });
            if (myRefineResultDataArray.length > 0) {
                BindDisplayResutModuleDataNew(myRefineResultDataArray);
            }
            else { BindDisplayResutModuleDataNew(myRefineResultDataArray); }


        }
    }
    catch (e) {
        HideWaitProgress();
    }
    HideWaitProgress();
}
/******************************Bind Filter Airports Module Result*************************/

/******************************Range Slider*************************/
var SliderActionStatus = false;
function GetRefineSearchType(RefineType, SearchValue) {
    try {
        var OriginDestinationElapsedTime = "";
        ShowWaitProgress();
        if (SliderActionStatus) {
            SliderActionStatus = false;
            var myRefineResultDataArray = new Array(), FlightSegment_Array = new Array(), AllFlightSegmentOptions_Array = new Array();
            BFMXAllFlightSegmentItineraryPricingResult = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
            switch (RefineType) {
                case "#Price-range-slider":
                    if (BFMXAllFlightSegmentItineraryPricingResult != null) {
                        var MinNewPriceRange = $("#Price-range-slider").slider('value').split(";")[0];
                        var MaxNewPriceRange = $("#Price-range-slider").slider('value').split(";")[1];
                        $.each(BFMXAllFlightSegmentItineraryPricingResult, function (dt_key, dt_value) {
                            AllFlightSegmentOptions_Array = new Array();
                            $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                                OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;
                                FlightSegment_Array = new Array();
                                $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {
                                    MaxPriceRange = $("#Price-range-slider").slider('value').split(";")[1];
                                    MinPriceRange = $("#Price-range-slider").slider('value').split(";")[0];
                                    var TotalFare = Segment_value.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount;
                                    if (TotalFare < parseFloat(MaxPriceRange) && TotalFare > parseFloat(MinPriceRange)) {
                                        FlightSegment_Array.push(Segment_value);
                                    }
                                });
                                AllFlightSegmentOptions_Array.push({
                                    ElapsedTime: OriginDestinationElapsedTime,
                                    FlightSegments: FlightSegment_Array
                                });
                            });
                            if (FlightSegment_Array.length > 0) {
                                myRefineResultDataArray.push({
                                    FlightSegmentID: dt_value.FlightSegmentID,
                                    FlightSegments: FlightSegment_Array
                                });
                            }
                        });
                        if (AllFlightSegmentOptions_Array.length > 0) {
                            BindDisplayResutModuleDataNew(AllFlightSegmentOptions_Array);
                        }
                        else { BindDisplayResutModuleDataNew(AllFlightSegmentOptions_Array); }
                    }
                    break;
                case "#Departure-range-slider":
                    if (BFMXAllFlightSegmentItineraryPricingResult != null) {
                        $.each(BFMXAllFlightSegmentItineraryPricingResult, function (dt_key, dt_value) {
                            AllFlightSegmentOptions_Array = new Array();
                            $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                                FlightSegment_Array = new Array();
                                $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {
                                    var OriginDestinationElapsedTime = Segment_value.ElapsedTime;
                                    var dtvalueFlightSegments = Segment_value;

                                    var MinDepartureSelection = $("#Departure-range-slider").slider('value').split(";")[0];
                                    var MaxDepartureSelection = $("#Departure-range-slider").slider('value').split(";")[1];
                                    var Departure = dtvalueFlightSegments.DepartureDateTime.split("T")[1].split(":")[0];
                                    Departure = parseInt($.formatDateTime('hh', new Date(dtvalueFlightSegments.DepartureDateTime)));
                                    if ((Departure < parseInt(MaxDepartureSelection)) && (Departure > parseInt(MinDepartureSelection))) {
                                        FlightSegment_Array.push(FlightSegment_value);
                                    }
                                });
                                AllFlightSegmentOptions_Array.push({
                                    ElapsedTime: OriginDestinationElapsedTime,
                                    FlightSegments: FlightSegment_Array
                                });
                            });
                            if (FlightSegment_Array.length > 0) {
                                myRefineResultDataArray.push({
                                    FlightSegmentID: dt_value.FlightSegmentID,
                                    FlightSegments: AllFlightSegmentOptions_Array
                                });
                            }
                        });
                        BindDisplayResutModuleDataNew(myRefineResultDataArray);
                    }
                    break;
                case "#Arrive-range-slider":
                    if (BFMXAllFlightSegmentItineraryPricingResult != null) {
                        $.each(BFMXAllFlightSegmentItineraryPricingResult, function (dt_key, dt_value) {
                            AllFlightSegmentOptions_Array = new Array();
                            $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                                FlightSegment_Array = new Array();
                                $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {
                                    var OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;
                                    var dtvalueFlightSegments = FlightSegment_value.FlightSegments[FlightSegment_value.FlightSegments.length - 1];
                                    var MaxDepartureSelection = $("#Arrive-range-slider").slider('value').split(";")[1];
                                    var MinDepartureSelection = $("#Arrive-range-slider").slider('value').split(";")[0];

                                    var Departure = dtvalueFlightSegments.ArrivalDateTime.split("T")[1].split(":")[0];
                                    Departure = parseInt($.formatDateTime('hh', new Date(dtvalueFlightSegments.ArrivalDateTime)));
                                    if (Departure < parseInt(MaxDepartureSelection) && Departure > parseInt(MinDepartureSelection)) {
                                        FlightSegment_Array.push(FlightSegment_value);
                                    }
                                });
                                AllFlightSegmentOptions_Array.push({
                                    ElapsedTime: OriginDestinationElapsedTime,
                                    FlightSegments: FlightSegment_Array
                                });
                            });
                            if (FlightSegment_Array.length > 0) {
                                myRefineResultDataArray.push({
                                    FlightSegmentID: dt_value.FlightSegmentID,
                                    FlightSegments: AllFlightSegmentOptions_Array
                                });
                            }
                        });
                        BindDisplayResutModuleDataNew(myRefineResultDataArray);
                    }
                    break;
            }
        }
        HideWaitProgress();
    } catch (e) {
        var ex = e;
    }
}
/******************************End Range Slider*************************/

$(function ($) {
    if (SolutionDataTraveler("GET", "AirLineList") == null) {
        GetAirLineList();
    }

    $('#Search-Widgets-Result-Panel').on('click', '.myAirCartSelector', function (e) {
        OnLoadGetPassengerDetailsList();
        var myPaymentDataArray = new Array();
        var AllFlightSegmentOptions_Array = new Array();
        var AirFlightSegmentID = $(this).attr('data-param');
        var AllFlightSegmentItineraryPricing = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
        var OriginDestinationElapsedTime = "";
        $.each(AllFlightSegmentItineraryPricing, function (dt_key, dt_value) {
            $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;
                if (dt_value.FlightSegmentID == AirFlightSegmentID) {
                    myPaymentDataArray.push(FlightSegment_value);
                }
            });
        });

        SolutionDataTraveler("Clear", "AirReservationBookingResult");
        SolutionDataTraveler("SET", "SelectedAirLineBokingPayment", myPaymentDataArray);
        Airline_Boking_Process($(this).attr('data-param'));
    });
});

var AirPortNameCodeCoList, AirLineList;
function Airline_Name(airline_code) {
    var currentdate = '';
    $.each(AirLineList, function (dt_key, dt_value) {
        if (dt_value.airlinecode == airline_code) {
            currentdate = dt_value.airlinename;
        }
    });
    return currentdate;
}

function AirPort_Name(airPort_code) {
    var currentdate = '';
    $.each(AirPortNameCodeCoList, function (dt_key, dt_value) {
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
    if ((airline_code.lastIndexOf('[') > 0) && (airline_code.lastIndexOf(']') > 0)) {
        return (airline_code.substr(airline_code.lastIndexOf('['), airline_code.lastIndexOf(']')).replace('[', '').replace(']', ''));
    }

    else { return airline_code; }
}

function GetAirLineList() {
    try {
        function ResultCallBackSuccess(e, xhr, opts) {
            HideWaitProgress();
            var App_Data = e.Data;
            var App_Data1 = e.Data1;
            SolutionDataTraveler("SET", "AirLineList", App_Data);
            SolutionDataTraveler("SET", "AirPortNameCodeCoList", App_Data1);
            AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
            AirLineList = SolutionDataTraveler("GET", "AirLineList");
        }
        function ResultCallBackError(e, xhr, opts) {
            HideWaitProgress();
        }
        var Reqst_Resource = {
            CommonServiceType: "AirlinesListDetails",
            SearchText: "SearchText"
        };
        MasterAppConfigurationsServices("GET", CommonConfiguration.WebAPIServicesURL + "API/CommonService/GetCommonService", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var ex = e;
    }
}

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
    //var difference= b-a;
    //var lapse=new Date(difference);  

    return diff;
}

function Airline_Boking_Process(airline_code) {
    try {
        location.href = 'Payments';
    } catch (e) {
        var error = e;
        error = e;
    }
}

$(function ($) {
    try {
        ///==================Clear LS=========================///
        ///==================================================///
        SolutionDataTraveler("Clear", "BFMXSearchResult");
        SolutionDataTraveler("Clear", "SearchResult");
        SolutionDataTraveler("Clear", "SelectedAirLineBokingPayment");
        SolutionDataTraveler("Clear", "AirReservationBookingResult");
        SolutionDataTraveler("Clear", "BFMXAllFlightSegmentItineraryPricingResult");

        ///==================================================///
        ///==================Clear LS=========================///

        ///==================================================//
        ///==================================================//        

        //$("#txtOriginDate").datepicker({
        //    startDate: new Date(),
        //    autoclose: true,
        //    format: 'yyyy-mm-dd',
        //    todayHighlight: true
        //});
        $("#col_departureDate").click(function () {
            $("#txtOriginDate").datepicker("show");
        });
        ///==================================================/ /
        ///==================================================/ /
        var dateToday = new Date();
        //$("#txtDestinationDate").datepicker({
        //    startDate: new Date($("#txtOriginDate").val()),
        //    //startDate: '+10y',
        //    //startDate: '+10m',            
        //    //startDate: GetNextStartData($("#txtOriginDate").val()),
        //    minDate: new Date($('#txtOriginDate').val()),
        //    //startDate : new Date('2017-11-01'),
        //    //startDate: new Date(),
        //    autoclose: true,
        //    format: 'yyyy-mm-dd',

        //    //todayHighlight: true
        //});
        $("#col_returnDate").click(function () {
            $("#txtDestinationDate").datepicker("show");
        });

        ///==================================================//
        ///==================================================//
        $("#txtMLDate_1").datepicker({
            startDate: new Date(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_txtMLDate_1").click(function () {
            $("#txtMLDate_1").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtMLDate_2").datepicker({
            startDate: new Date(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_txtMLDate_2").click(function () {
            $("#txtMLDate_1").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtMLDate_3").datepicker({
            startDate: new Date(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_txtMLDate_3").click(function () {
            $("#txtMLDate_3").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtMLDate_4").datepicker({
            startDate: new Date(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_txtMLDate_4").click(function () {
            $("#txtMLDate_4").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txtMLDate_5").datepicker({
            startDate: new Date(),
            autoclose: true,
            format: 'yyyy-mm-dd',
            todayHighlight: true
        });
        $("#col_txtMLDate_5").click(function () {
            $("#txtMLDate_5").datepicker("show");
        });
        ///==================================================//
        ///==================================================//
        $("#txt_applicationNumber,#txt_pinNumber,#txtQID").keyup(function (e) {
            $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
            if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                return false;
            }
        });

    } catch (e) { }
});

function GetNextStartData(CurrentDate) {
    var NextDate = 00;
    try {
        if (CurrentDate != "") {
            NextDate = $.formatDateTime('yyyy-mm-dd', new Date().addDays(dateDiffInDays(new Date(), new Date(CurrentDate))));
            //NextDate = "+" + (Math.floor(Math.abs(timeDiff(new Date(), new Date(CurrentDate)).days))) + "d";
        }
        else { NextDate = $.formatDateTime('yy-mm-dd', new Date()); }
    } catch (e) {
        var ex = e;
    }
    return NextDate;
}

function GenarateAirSearchRequest() {
    var dynamicAirSearchRequestData = "";

    try {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        var Air_Origin_Destination_Information = Array();
        var Air_TravelerAvail_Information = Array();
        var Air_CabinPref_Information = Array();
        var AirSearchType = AppActionData;
        var Air_Origin_Destination_Information_List = function (RPH_Number, Departure_DateTime, Origin_Location, Destination_Location) {
            this.RPH = RPH_Number;
            this.DepartureDateTime = Departure_DateTime;
            this.OriginLocation = Origin_Location;
            this.DestinationLocation = Destination_Location;
        };
        if (SearchSelectionType == 1) {
            var AODInformation1 = new Air_Origin_Destination_Information_List(1, $('#txtOriginDate_1').val() + "T00:00:00", { LocationCode: $('#txtOrigin_1').val() }, { LocationCode: $('#txtDestination_1').val() });
            Air_Origin_Destination_Information.push(AODInformation1);
        }
        else if (SearchSelectionType == 2) {
            var AODInformation1 = new Air_Origin_Destination_Information_List(1, $('#txtOriginDate_1').val() + "T00:00:00", { LocationCode: $('#txtOrigin_1').val() }, { LocationCode: $('#txtDestination_1').val() });
            Air_Origin_Destination_Information.push(AODInformation1);
            var AODInformation2 = new Air_Origin_Destination_Information_List(2, $('#txtDestinationDate_1').val() + "T00:00:00", { LocationCode: $('#txtDestination_1').val() }, { LocationCode: $('#txtOrigin_1').val() });
            Air_Origin_Destination_Information.push(AODInformation2);
        }
        else if (SearchSelectionType == 3) {
            for (var i = 1; i <= AirSearchType; i++) {
                var AODInformation1 = new Air_Origin_Destination_Information_List(i, $('#txtdepartureDate_' + i).val() + "T00:00:00", { LocationCode: $('#txtOrigin_' + i).val() }, { LocationCode: $('#txtDestination_' + i).val() });
                Air_Origin_Destination_Information.push(AODInformation1);
            }
        }
        var Air_TravelerAvail_Information_List = function (code_Number, passenger_Quantity, passenger_Changeable) {
            this.PassengerTypeQuantity = [{
                Code: code_Number,
                Quantity: passenger_Quantity,
                Changeable: passenger_Changeable
            }];
        };
        var ATAInformation1 = new Air_TravelerAvail_Information_List("ADT", 1, true);
        Air_TravelerAvail_Information.push(ATAInformation1);

        var Air_TravelerAvail_Information_List = function (cabin_Type, preferLevel) {
            this.Cabin = cabin_Type;
            this.PreferLevel = preferLevel;
        };
        var ACIInformation1 = new Air_TravelerAvail_Information_List("Y", "Only");
        Air_CabinPref_Information.push(ACIInformation1);

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        var dynamicJsonData = {
            DirectFlightsOnly: false,
            AvailableFlightsOnly: true,
            OTA_AirLowFareSearchRQ: {
                OriginDestinationInformation: Air_Origin_Destination_Information,
                TPA_Extensions: {
                    IntelliSellTransaction: { RequestType: { Name: '50ITINS' } }
                },
                TravelerInfoSummary: {
                    AirTravelerAvail: Air_TravelerAvail_Information,
                    PriceRequestInformation: {
                        CurrencyCode: 'CAD'
                    }
                },
                TravelPreferences: {
                    ValidInterlineTicket: true,
                    CabinPref: Air_TravelerAvail_Information,
                    TPA_Extensions: {}
                },
                POS: {
                    Source: [{
                        RequestorID: { Type: '0.AAA.X', ID: 'REQ.ID', CompanyName: { Code: 'TN' } },
                        PseudoCityCode: '6DTH'
                    }]
                }
            }
        };
        dynamicData = dynamicJsonData;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////\
        dynamicAirSearchRequestData = JSON.stringify(dynamicJsonData);
        HideWaitProgress();
        return dynamicAirSearchRequestData;
    } catch (e) {
        var ex = e;
    }
}

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

function ReicivedDetails(id, ProductID) {
    try {
        ConfirmBootBox("Successfully", "Successfully Fatched ID : " + ProductID, 'App_Success', initialCallbackYes, initialCallbackNo);
    } catch (e) {
        HideWaitProgress();
    }
}

function lzw_decode(s) {
    var dict = {};
    var data = (s + "").split("");
    var currChar = data[0];
    var oldPhrase = currChar;
    var out = [currChar];
    var code = 256;
    var phrase;
    for (var i = 1; i < data.length; i++) {
        var currCode = data[i].charCodeAt(0);
        if (currCode < 256) {
            phrase = data[i];
        }
        else {
            phrase = dict[currCode] ? dict[currCode] : (oldPhrase + currChar);
        }
        out.push(phrase);
        currChar = phrase.charAt(0);
        dict[code] = oldPhrase + currChar;
        code++;
        oldPhrase = phrase;
    }
    return out.join("");
}

/******************************Start Search Result*************************/
function QueryStringDataSearchCondition(SearchType, SearchContentData) {
    try {
        if (SearchType != "") {
            switch (SearchType) {
                case "1":
                    $("input[name='radioSelectionName']").filter("[value='" + SearchType + "']").attr("checked", true);
                    $('#txtOrigin').val(SearchContentData.origin.toUpperCase());
                    $("#txtDestination").val(SearchContentData.destination.toUpperCase());
                    $("#txtOriginDate").val(SearchContentData.departuredate.toUpperCase());
                    break;
                case "2":
                    $("input[name='radioSelectionName']").filter("[value='" + SearchType + "']").attr("checked", true);
                    $('#txtOrigin').val(SearchContentData.origin.toUpperCase());
                    $("#txtDestination").val(SearchContentData.destination.toUpperCase());
                    $("#txtOriginDate").val(SearchContentData.departuredate.toUpperCase());
                    $("#txtDestinationDate").val(SearchContentData.returndate.toUpperCase());
                    break;
                case "3":
                    $("input[name='radioSelectionName']").filter("[value='" + SearchType + "']").attr("checked", true);
                    var arr = [1, 2, 3, 4, 5];
                    $.each(arr, function (index, value) {
                        AppActionData++;
                        $(".myrow_" + value).show();
                    });
                    $('#txtMLOrigin_1').val(SearchContentData.origin1.toUpperCase());
                    $("#txtMLDestination_1").val(SearchContentData.destination1.toUpperCase());
                    $("#txtMLDate_1").val(SearchContentData.departuredate1.toUpperCase());

                    $('#txtMLOrigin_2').val(SearchContentData.origin2.toUpperCase());
                    $("#txtMLDestination_2").val(SearchContentData.destination2.toUpperCase());
                    $("#txtMLDate_2").val(SearchContentData.departuredate2.toUpperCase());

                    $('#txtMLOrigin_3').val(SearchContentData.origin3.toUpperCase());
                    $("#txtMLDestination_3").val(SearchContentData.destination3.toUpperCase());
                    $("#txtMLDate_3").val(SearchContentData.departuredate3.toUpperCase());

                    $('#txtMLOrigin_4').val(SearchContentData.origin4.toUpperCase());
                    $("#txtMLDestination_4").val(SearchContentData.destination4.toUpperCase());
                    $("#txtMLDate_4").val(SearchContentData.departuredate4.toUpperCase());

                    $('#txtMLOrigin_5').val(SearchContentData.origin4.toUpperCase());
                    $("#txtMLDestination_5").val(SearchContentData.destination4.toUpperCase());
                    $("#txtMLDate_5").val(SearchContentData.departuredate4.toUpperCase());

                    break;
            }
        }

    } catch (e) {
        var error = e;
        error = error;
    }
}

function OnLoadGetQueryStringData() {
    try {
        // debugger;
        if (GetQueryStringParameterValues('Search') != null) {

            ShowWaitProgress();
            var Request_ContentData = JSON.parse(unescape(GetQueryStringParameterValues('Search')));
            SearchCondition((Request_ContentData.SearchType == "O") ? "1" : (Request_ContentData.SearchType == "R") ? "2" : (Request_ContentData.SearchType == "M") ? "3" : "");
            QueryStringDataSearchCondition(((Request_ContentData.SearchType == "O") ? "1" : (Request_ContentData.SearchType == "R") ? "2" : (Request_ContentData.SearchType == "M") ? "3" : ""), Request_ContentData);

            var RequestContentData = '';
            switch (Request_ContentData.SearchType) {
                case "O":
                    RequestContentData = JSON.stringify({
                        "OTA_AirLowFareSearchRQ": {
                            "OriginDestinationInformation": [
                              {
                                  "RPH": "1",
                                  "DepartureDateTime": Request_ContentData.departuredate + "T" + GetCurrentTime(),
                                  "OriginLocation": {
                                      "LocationCode": Request_ContentData.origin.toUpperCase()
                                  },
                                  "DestinationLocation": {
                                      "LocationCode": Request_ContentData.destination.toUpperCase()
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
                                            "Quantity": parseInt(Request_ContentData.numadults)
                                        }
                                      ]
                                  }
                                ],
                                "PriceRequestInformation": {
                                    "CurrencyCode": "CAD"
                                }
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
                                      "PseudoCityCode": "#####"
                                  }
                                ]
                            }
                        }
                    });
                    break;
                case "R":
                    RequestContentData = JSON.stringify({
                        "OTA_AirLowFareSearchRQ": {
                            "OriginDestinationInformation": [
                               {
                                   "RPH": "1",
                                   "DepartureDateTime": Request_ContentData.departuredate + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.origin.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.destination.toUpperCase()
                                   }
                               }, {
                                   "RPH": "2",
                                   "DepartureDateTime": Request_ContentData.returndate + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.destination.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.origin.toUpperCase()
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
                                ],
                                "PriceRequestInformation": {
                                    "CurrencyCode": "CAD"
                                }
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
                                      "PseudoCityCode": "#####"
                                  }
                                ]
                            }
                        }
                    });
                    break;
                case "M":
                    RequestContentData = JSON.stringify({
                        "OTA_AirLowFareSearchRQ": {
                            "OriginDestinationInformation": [
                               {
                                   "RPH": "1",
                                   "DepartureDateTime": Request_ContentData.departuredate1 + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.origin1.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.destination1.toUpperCase()
                                   }
                               }, {
                                   "RPH": "2",
                                   "DepartureDateTime": Request_ContentData.departuredate2 + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.origin2.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.destination2.toUpperCase()
                                   }
                               }, {
                                   "RPH": "3",
                                   "DepartureDateTime": Request_ContentData.departuredate3 + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.origin3.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.destination3.toUpperCase()
                                   }
                               }, {
                                   "RPH": "4",
                                   "DepartureDateTime": Request_ContentData.departuredate4 + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.origin4.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.destination4.toUpperCase()
                                   }
                               }, {
                                   "RPH": "5",
                                   "DepartureDateTime": Request_ContentData.departuredate5 + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": Request_ContentData.origin5.toUpperCase()
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": Request_ContentData.destination5.toUpperCase()
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
                                ],
                                "PriceRequestInformation": {
                                    "CurrencyCode": "CAD"
                                }
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
                                      "PseudoCityCode": "#####"
                                  }
                                ]
                            }
                        }
                    });
                    break;
            }
            function GetJSON_Viewer(input) {
                try {
                    input = eval('(' + input + ')');
                    var options = {
                        collapsed: false,
                        withQuotes: false
                    };
                    $('#Search-josn-Result-renderer').jsonViewer(input, options);
                }
                catch (error) {
                    return alert("Cannot eval JSON: " + error);
                }
            }
            function GetJSONList(DataTable_Data) {
                MasterRespansiveJSONDataManagement(".Search-josn-Result-Panel", JSON.parse(DataTable_Data));
            }
            function ResultCallBackSuccess(e, xhr, opts) {
                if (e.Data != null) {
                    ShowWaitProgress();
                    var App_Data = e.Data;
                    SolutionDataTraveler("SET", "BFMXSearchResult", App_Data);
                    FilteringBindDisplayResutData(App_Data.Result);
                }
            }
            function ResultCallBackError(e, xhr, opts) {
                HideWaitProgress();
                ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
            }
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
                pointofsalecountry: 'CA',
                passengercount: 1,
                lengthofstay: 1,
                minfare: 1,
                maxfare: 1,
                BFMXRQ_RequestContent: RequestContentData
            };
            MasterAppConfigurationsServices("POST", CommonConfiguration.WebAPIServicesURL + "API/BargainFinderMaxSearch/PostBFMXSearch", JSON.stringify(Reqst_Resource), ResultCallBackSuccess, ResultCallBackError);
        }
    } catch (e) {
        var error = e;
        error = error;
        HideWaitProgress();
    }
}

$(document).ready(function () {
    $('#date-range0').dateRangePicker({ autoClose: true });
    $('#txtreturnDate').attr('readonly', true);
    AppActionData = 1;

    SearchCondition("2");
    $("input[name='radioSelectionName']").on("click", function (event) {
        SearchCondition($(this).val());
    });
    $("input[id='txtreturnDate']").on("click", function (event) {
        $('#txtreturnDate').attr('readonly', false);
    });
    var arr = [1, 2, 3, 4, 5];
    $.each(arr, function (index, value) {
        $(".myrow_" + value).hide();
    });
    var remove_button = $(".remove_field"); //Fields wrapper
    var max_fields = 5; //maximum input boxes allowed
    var add_button = $(".add_more_field_button"); //Add button ID
    var x = 1; //initlal text box count

    $(add_button).click(function (e) { //on add input button click
        e.preventDefault();
        if (x < max_fields) { //max input box allowed
            x++; //text box increment
            AppActionData++;
            $(".myrow_" + x).show();
        }
    });
    $(remove_button).click(function (e) { //on add input button click
        e.preventDefault();
        $(this).parent('div').parent('div').parent('div').parent('div').parent('div').hide(); x--; AppActionData--;
    })

    OnLoadGetQueryStringData();

});
/******************************End Search Result*************************/
function OnLoadGetDataList() {

    try {
        var RequestContentData = '';
        switch (SearchSelectionType) {
            case 1:
                RequestContentData = JSON.stringify({
                    "OTA_AirLowFareSearchRQ": {
                        "OriginDestinationInformation": [
                          {
                              "RPH": "1",
                              "DepartureDateTime": $('#txtOriginDate').val() + "T" + GetCurrentTime(),
                              "OriginLocation": {
                                  "LocationCode": GetAirline_Code($('#txtOrigin').val().toUpperCase())
                              },
                              "DestinationLocation": {
                                  "LocationCode": GetAirline_Code($('#txtDestination').val().toUpperCase())
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
                                        "Quantity": parseInt(myPassengerDetailsListArray[0].Adults)
                                    }
                                  ]
                              }
                            ],
                            "PriceRequestInformation": {
                                "CurrencyCode": "CAD"
                            }
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
                                  "PseudoCityCode": "#####"
                              }
                            ]
                        }
                    }
                });
                break;
            case 2:
                var orginCOde = GetAirline_Code($('#txtOrigin').val().toUpperCase());
                var detinationCode = GetAirline_Code($('#txtDestination').val().toUpperCase());
                // debugger;
                RequestContentData = JSON.stringify({
                    "OTA_AirLowFareSearchRQ": {
                        "OriginDestinationInformation": [
                          {
                              "RPH": "1",
                              "DepartureDateTime": $('#txtOriginDate').val() + "T" + GetCurrentTime(),
                              "OriginLocation": {
                                  "LocationCode": GetAirline_Code($('#txtOrigin').val().toUpperCase())
                              },
                              "DestinationLocation": {
                                  "LocationCode": GetAirline_Code($('#txtDestination').val().toUpperCase())
                              }
                          }, {
                              "RPH": "2",
                              "DepartureDateTime": $('#txtDestinationDate').val() + "T" + GetCurrentTime(),
                              "OriginLocation": {
                                  "LocationCode": GetAirline_Code($('#txtDestination').val().toUpperCase())
                              },
                              "DestinationLocation": {
                                  "LocationCode": GetAirline_Code($('#txtOrigin').val().toUpperCase())
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
                                        "Quantity": parseInt(myPassengerDetailsListArray[0].Adults)
                                    }
                                  ]
                              }
                            ],
                            "PriceRequestInformation": {
                                "CurrencyCode": "CAD"
                            }
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
                                  "PseudoCityCode": "#####"
                              }
                            ]
                        }
                    }
                });
                break;
            case 3:
                switch (AppActionData) {
                    case 1:
                        RequestContentData = JSON.stringify({
                            "OTA_AirLowFareSearchRQ": {
                                "OriginDestinationInformation": [
                                  {
                                      "RPH": "1",
                                      "DepartureDateTime": $('#txtMLDate_1').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_1').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_1').val().toUpperCase())
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
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": "CAD"
                                    }
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
                                          "PseudoCityCode": "#####"
                                      }
                                    ]
                                }
                            }
                        });
                        break;
                    case 2:
                        RequestContentData = JSON.stringify({
                            "OTA_AirLowFareSearchRQ": {
                                "OriginDestinationInformation": [
                                  {
                                      "RPH": "1",
                                      "DepartureDateTime": $('#txtMLDate_1').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_1').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_1').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "2",
                                      "DepartureDateTime": $('#txtMLDate_2').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_2').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_2').val().toUpperCase())
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults)
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": "CAD"
                                    }
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
                                          "PseudoCityCode": "#####"
                                      }
                                    ]
                                }
                            }
                        });
                        break;
                    case 3:
                        RequestContentData = JSON.stringify({
                            "OTA_AirLowFareSearchRQ": {
                                "OriginDestinationInformation": [
                                  {
                                      "RPH": "1",
                                      "DepartureDateTime": $('#txtMLDate_1').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_1').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_1').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "2",
                                      "DepartureDateTime": $('#txtMLDate_2').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_2').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_2').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "3",
                                      "DepartureDateTime": $('#txtMLDate_3').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_3').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_3').val().toUpperCase())
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults)
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": "CAD"
                                    }
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
                                          "PseudoCityCode": "#####"
                                      }
                                    ]
                                }
                            }
                        });
                        break;
                    case 4:
                        RequestContentData = JSON.stringify({
                            "OTA_AirLowFareSearchRQ": {
                                "OriginDestinationInformation": [
                                  {
                                      "RPH": "1",
                                      "DepartureDateTime": $('#txtMLDate_1').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_1').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_1').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "2",
                                      "DepartureDateTime": $('#txtMLDate_2').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_2').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_2').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "3",
                                      "DepartureDateTime": $('#txtMLDate_3').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_3').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_3').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "4",
                                      "DepartureDateTime": $('#txtMLDate_4').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_4').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_4').val().toUpperCase())
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults)
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": "CAD"
                                    }
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
                                          "PseudoCityCode": "#####"
                                      }
                                    ]
                                }
                            }
                        });
                        break;
                    case 5:
                        RequestContentData = JSON.stringify({
                            "OTA_AirLowFareSearchRQ": {
                                "OriginDestinationInformation": [
                                  {
                                      "RPH": "1",
                                      "DepartureDateTime": $('#txtMLDate_1').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_1').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_1').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "2",
                                      "DepartureDateTime": $('#txtMLDate_2').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_2').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_2').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "3",
                                      "DepartureDateTime": $('#txtMLDate_3').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_3').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_3').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "4",
                                      "DepartureDateTime": $('#txtMLDate_4').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_4').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_4').val().toUpperCase())
                                      }
                                  },
                                  {
                                      "RPH": "5",
                                      "DepartureDateTime": $('#txtMLDate_5').val() + "T" + GetCurrentTime(),
                                      "OriginLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLOrigin_5').val().toUpperCase())
                                      },
                                      "DestinationLocation": {
                                          "LocationCode": GetAirline_Code($('#txtMLDestination_5').val().toUpperCase())
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults)
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": "CAD"
                                    }
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
                                          "PseudoCityCode": "#####"
                                      }
                                    ]
                                }
                            }
                        });
                        break;
                }
                break;
        }
        function GetJSON_Viewer(input) {
            try {
                input = eval('(' + input + ')');
                var options = {
                    collapsed: false,
                    withQuotes: false
                };
                $('#Search-josn-Result-renderer').jsonViewer(input, options);
            }
            catch (error) {
                return alert("Cannot eval JSON: " + error);
            }
        }
        function GetJSONList(DataTable_Data) {
            MasterRespansiveJSONDataManagement(".Search-josn-Result-Panel", JSON.parse(DataTable_Data));
        }
        function ResultCallBackSuccess(e, xhr, opts) {
            if (e.Data != null) {
                ShowWaitProgress();
                var App_Data = e.Data;
                SolutionDataTraveler("SET", "BFMXSearchResult", App_Data);
                FilteringBindDisplayResutData(App_Data.Result);
            }
        }
        function ResultCallBackError(e, xhr, opts) {
            HideWaitProgress();
            ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
        }
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
            pointofsalecountry: 'CA',
            passengercount: 1,
            lengthofstay: 1,
            minfare: 1,
            maxfare: 1,
            BFMXRQ_RequestContent: RequestContentData
        };
        MasterAppConfigurationsServices("POST", CommonConfiguration.WebAPIServicesURL + "API/BargainFinderMaxSearch/PostBFMXSearch", JSON.stringify(Reqst_Resource), ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var error = e;
        error = error;
    }
}

function OnLoadGetPassengerDetailsList() {
    try {
        myPassengerDetailsListArray = new Array();
        myPassengerDetailsListArray.push({
            Class: $('.AirClass').val().trim(),
            Adults: $('.noOfAdults').val().trim(),
            Seniors: $('.noOfSeniors').val().trim(),
            Childrens: $('.noOfChildrens').val().trim(),
            Infants: $('.noOfInfants').val().trim(),
            OnSeat: $('.noOfOnSeats').val().trim()
        });
        SolutionDataTraveler("SET", "GetPassengerDetailsList", myPassengerDetailsListArray);
    } catch (e) {
        var error = e;
        error = error;
    }
}

$(document).ready(function () {
    OnLoadGetPassengerDetailsList();
    $("#Result_Div_Panel").addClass("hidden");
    $(".SearchJSONResult").hide();
    $(".btn-success-Search").on('click', function () {
        ShowWaitProgress();
        if (ValidateData()) {
            OnLoadGetPassengerDetailsList();
            if (SolutionDataTraveler("GET", "AirLineList") == null) {
                GetAirLineList();
            }
            AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
            AirLineList = SolutionDataTraveler("GET", "AirLineList");
            OnLoadGetDataList();
        }
        else { HideWaitProgress(); }

    });
    $(".josn-Result-Viewer").on('click', function () {
        if ($(".Search-josn-Result-renderer").hasClass("hidden")) {
            $(".Search-josn-Result-renderer").removeClass("hidden");
        }
        else {
            $(".Search-josn-Result-renderer").addClass("hidden");
        }
    });
    $("#Div_Search-Result-Departing").on('click', function () {
        GetFilterAirportsModuleResut();
    });
    $("#Div_Search-Result-Airlines").on('click', function () {
        GetFilterAirlinesModuleResut();
    });
});

function ValidateData() {
    var TotalPassagerCount = (parseInt($('.noOfAdults').val()) + parseInt($(".noOfChildrens").val()));
    if (SearchSelectionType == 1) {
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
        if ($('#txtOriginDate').val().trim() == "") {
            ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
            $("#txtdepartureDate").focus();
            HideWaitProgress();
            return false;
        }
    }
    if (SearchSelectionType == 2) {
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
        if ($('#txtOriginDate').val().trim() == "") {
            ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
            $("#txtdepartureDate").focus();
            HideWaitProgress();
            return false;
        }
        if ($('#txtDestinationDate').val().trim() == "") {
            ConfirmBootBox("Message", "Please Enter Return Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
            $("#txtreturnDate").focus();
            HideWaitProgress();
            return false;
        }
    }
    if (SearchSelectionType == 3) {
        switch (AppActionData) {
            case 1:
                if ($('#txtMLOrigin_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_1").focus();
                    HideWaitProgress();
                    return false;
                }
                break;
            case 2:
                if ($('#txtMLOrigin_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_1").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_2").focus();
                    HideWaitProgress();
                    return false;
                }

                break;
            case 3:
                if ($('#txtMLOrigin_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_1").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_2").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_3").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_3").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_3").focus();
                    HideWaitProgress();
                    return false;
                }
                break;
            case 4:
                if ($('#txtMLOrigin_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_1").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_2").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_3").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_3").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_3").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_4').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_4").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_4').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_4").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_4').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_4").focus();
                    HideWaitProgress();
                    return false;
                }
                break;
            case 5:
                if ($('#txtMLOrigin_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_1").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_1').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_1").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_2").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_2').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_2").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_3").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_3").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_3').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_3").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_4').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_4").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_4').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_4").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_4').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_4").focus();
                    HideWaitProgress();
                    return false;
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                if ($('#txtMLOrigin_5').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLOrigin_5").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDestination_5').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDestination_5").focus();
                    HideWaitProgress();
                    return false;
                }
                if ($('#txtMLDate_5').val().trim() == "") {
                    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                    $("#txtMLDate_5").focus();
                    HideWaitProgress();
                    return false;
                }
                break;
        }
    }
    if (TotalPassagerMaxCount < TotalPassagerCount) {
        ConfirmBootBox("Successfully", "Total Passanger should not more than 7.", 'App_Warning', initialCallbackYes, initialCallbackNo);
        HideWaitProgress();
        return false;
    }
    if (!AdultInfantValidation()) {
        HideWaitProgress();
        return false;
    }
    return true;
}

function SearchCondition(SearchType) {
    try {
        if (SearchType != "") {
            switch (SearchType) {
                case "1":
                    $('#txtreturnDate').attr('readonly', false);
                    $("#multicitySearch").addClass("hidden");
                    $("#btnmulticitySearch").addClass("hidden");
                    $("#OneWayReturnWaySearch").removeClass("hidden");
                    $(".myrow_1").hide();
                    SearchSelectionType = 1;
                    for (var i = 0; i < AppActionData; i++) {
                        $(".remove_field").click();
                    }
                    break;
                case "2":
                    $('#txtreturnDate').attr('readonly', true);
                    $("#multicitySearch").addClass("hidden");
                    $("#btnmulticitySearch").addClass("hidden");
                    $("#OneWayReturnWaySearch").removeClass("hidden");
                    $(".myrow_1").hide();
                    SearchSelectionType = 2;
                    for (var i = 0; i < AppActionData; i++) {
                        $(".remove_field").click();
                    }
                    break;
                case "3":
                    $('#txtreturnDate').attr('readonly', false);
                    $("#multicitySearch").removeClass("hidden");
                    $("#btnmulticitySearch").removeClass("hidden");
                    $("#OneWayReturnWaySearch").addClass("hidden");
                    $(".myrow_1").show();
                    SearchSelectionType = 3;
                    AppActionData = 1;
                    break;
            }
        }

    } catch (e) {
        var error = e;
        error = error;
    }
}
