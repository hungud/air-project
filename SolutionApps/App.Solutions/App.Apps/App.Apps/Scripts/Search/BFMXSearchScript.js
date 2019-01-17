var AppActionData = 0, SearchSelectionType = 1, BFMXAllFlightSegmentItineraryPricingResult = null;
var myPassengerDetailsListArray = new Array();
var fromFilter = false;
var priceRangeFilter = false;
var AirPortNameCodeCoList, AirLineList;
var Consolidator_Markup_Array = new Array();
var MemberAgency_Markup_Array = new Array();
var AirCode_Array = new Array();
if (AirLineList == null) {
    GetAirLineList();
}

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return "";
    }
    else {
        var res = results[0];
        return res;
    }
    //return results[0] || "";
    //return results[0].replace("?", "") || 0;
}

//Internet Explorer 11 Related Function

Math.trunc = Math.trunc || function (x) {
    if (isNaN(x)) {
        return NaN;
    }
    if (x > 0) {
        return Math.floor(x);
    }
    return Math.ceil(x);
};

//IE 11 function block

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

Array.prototype.FulterContainsAirLineName1 = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].FlightSegments[0].MarketingAirline.Code === v) return true;
    }
    return false;
};

Array.prototype.FulterUniqueAirLineName = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.FulterContainsAirLineName(this[i].FlightSegments[0].OperatingAirline.Code)) {
            arr.push(this[i]);
        }
        if (!arr.FulterContainsAirLineName1(this[i].FlightSegments[0].MarketingAirline.Code)) {
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
                BFMXAllFlightSegmentItineraryPricingResult = AllFlightSegment_Array;
                //
                BindResutModuleDataNew(AllFlightSegment_Array);
            }
        }
        else {
            //
            $("#Result_Div_Panel").removeClass("hidden");
            var dynamicDetails = "<tr><td colspan='7' style='align-items: center; text-align: center;'> No flights are available for the selected criteria, please change your criteria and try again</td></tr>";
            $("#Search-Widgets-Result-Panel").empty().append(dynamicDetails);
            //
            HideWaitProgress();
        }
    }
    catch (e) {
        var error = e;
        error = e;
        //
        HideWaitProgress();
    }
}


function GetmarkUp() {
    var domainName = localStorage.getItem("domain");
    var servicepath = CommonConfiguration.searchBoxService + "/GetMarkupByDomain";
    var markUp = '';

    if (domainName != '' && domainName != null && domainName != undefined) {
        $.ajax({
            cache: false,
            async: false,
            url: servicepath,
            data: ({ domain: domainName }),
            type: "POST",
            cors: true,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            contentType: "application/json; charset=utf-8",
            dataType: "xml",
            success: function (xml) {

                //  console.log("Returned XML");
                //console.log(xml);

                $(xml).find('MarkUp').each(function () {
                    var CompanyTypeID = $(this).find('CompanyTypeID').text();
                    if (CompanyTypeID == "2") {
                        //Member Agency
                        MemberAgency_Markup_Array.push({
                            PublishedFareMarkup: $(this).find('PublishedFareMarkup').text(),
                            NetfareMarkup: $(this).find('NetfareMarkup').text(),
                            PublishedMarkupType: $(this).find('PublishedMarkupType').text(),
                            IATACode: $(this).find('IATACode').text(),
                        });
                    }
                    else {
                        Consolidator_Markup_Array.push({
                            PublishedFareMarkup: $(this).find('PublishedFareMarkup').text(),
                            NetfareMarkup: $(this).find('NetfareMarkup').text(),
                            PublishedMarkupType: $(this).find('PublishedMarkupType').text(),
                            IATACode: $(this).find('IATACode').text(),
                        });
                    }
                });

                //$(xml).find('MarkUp').each(function () {
                //    var publishedFareMarkup = $(this).find('PublishedFareMarkup').text()
                //    var netfareMarkup = $(this).find('NetfareMarkup').text();
                //    var PublishedMarkupType = $(this).find('PublishedMarkupType').text();
                //    markUp = netfareMarkup + ',' + publishedFareMarkup+','+PublishedMarkupType;
                //});

            },
            error: function (response) {
                // alert(response.responseText);
            },
            failure: function (response) {
                //  alert(response.responseText);
            }
        });
    }
    //return Markup_Array;
}



function GetBlockedAirlines() { 
    var CompanyTypeid = sessionStorage.getItem("CompanyTypeId");
    var domainName = localStorage.getItem("domain");
    var servicepath = CommonConfiguration.WebSSOURL + "/api/CommonService/GetBlockedAirlines";

    var blockedAirlines;
    if (domainName != '' && domainName != null && domainName != undefined) {
        $.ajax({
            cache: false,
            async: false,
            url: servicepath,
            data: ({ domain: domainName, CompanyTypeId: CompanyTypeid }),
            type: "GET",
            cors: true,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                blockedAirlines = result;
            },
            error: function (response) {
                // alert(response.responseText);
            },
            failure: function (response) {
                //  alert(response.responseText);
            }
        });
    }
    return blockedAirlines;
}



function BindDisplayResutModuleDataNew(FilterAllFlightSegment_Array) {
    try {

        //console.log("--FilterAllFlightSegment_Array--");
        //console.log(FilterAllFlightSegment_Array);
        //console.log("--FilterAllFlightSegment_Array--");

        //var Markup_Array = new Array();
        //var Markup_Array = GetmarkUp();
        GetmarkUp();
        var FareMarkup = 0;




        
        var blockedAirArray = GetBlockedAirlines(); 
        //if (blockedAirlines != null && $.trim(blockedAirlines) != '') {
        //    blockedAirArray = blockedAirlines.split(',');
        //}

        var dynamicDetails = "";
        var DirectFlightCount = 0;
        var NonDirectFlightCount = 0;
        var FlightCount = 0;
        var SubFlightCount = 0;
        if (FilterAllFlightSegment_Array.length > 0) {
            console.log("-----------------Flightsegementarray--------------");
            console.log(FilterAllFlightSegment_Array);
            console.log("-----------------Flightsegementarray--------------");
            var IsDirectFlight = $("#directFlight").prop("checked");
            var SelectionType = $("input[name='radioSelectionName']:checked").val();
            AirCode_Array = new Array();
            AirCode_Array = [];
            var blockedAirlineCount = 0;
            $.each(FilterAllFlightSegment_Array, function (FlightSegment_key, FlightSegment_value) {

                var Flight_Segment_ID = FlightSegment_value.FlightSegmentID;
                var Allkey = FlightSegment_key, FlightNumberStop = '';
                var Hour = 0, Hours = 0, Minute = 0, Minutes = 0, StopoverHours = 0, StopoverMinutes = 0, FinalMinutes = 0;
                var NoOfStops = 0, cntStops = 1;


                if (FlightSegment_value.FlightSegments.length > 0) {
                    var flightSegment;

                    if (priceRangeFilter) { flightSegment = FlightSegment_value.FlightSegments[0].FlightSegments[0]; }
                    else {
                        if (fromFilter) {
                            flightSegment = FlightSegment_value.FlightSegments[0].FlightSegments[0];
                        }
                        else {
                            flightSegment = FlightSegment_value.FlightSegments[0].FlightSegments[0];
                        }
                    }


                    var type = flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerTypeQuantity;
                    var PassengerTypeQuantity = flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerTypeQuantity.Code;

                    var marketingAirlineCode = flightSegment.MarketingAirline.Code;
                    var MarkupType = 0;
                    var PublishedFare = 0;
                    var NetFare = 0;
                    var MA_MarkupType = 0;
                    var MA_PublishedFare = 0;
                    var MA_NetFare = 0;
                    var PublishFareMarkupExist = false;
                    //
                    
                    var CompanyTypeid = sessionStorage.getItem("CompanyTypeId");
                   // if (CompanyTypeid == "2") {
                        //Member Agency
                        //Code for publish fare and net fare markup
                        $.each(MemberAgency_Markup_Array, function (Markup_key, Markup_value) {

                            if (Markup_value.IATACode == marketingAirlineCode) {
                                // 
                                MA_MarkupType = Markup_value.PublishedMarkupType;
                                MA_PublishedFare = Markup_value.PublishedFareMarkup;
                                MA_NetFare = Markup_value.NetfareMarkup;
                                PublishFareMarkupExist = true;
                                return false;

                            }
                        });
                        $.each(Consolidator_Markup_Array, function (Markup_key, Markup_value) {

                            if (Markup_value.IATACode == marketingAirlineCode) {
                                // 
                                MarkupType = Markup_value.PublishedMarkupType;
                                PublishedFare = Markup_value.PublishedFareMarkup;
                                NetFare = Markup_value.NetfareMarkup;
                                PublishFareMarkupExist = true;
                                return false;

                            }
                        });

                        if (PassengerTypeQuantity == "ADT") {
                            //Public Fare
                            if (MA_MarkupType == "value") {
                                MA_PublishedFare = MA_PublishedFare;
                            }
                            else if (MA_MarkupType == "percentage") {
                                MA_PublishedFare = parseFloat(flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount) * MA_PublishedFare / 100;
                            }

                            if (MarkupType == "value") {
                                PublishedFare = PublishedFare;
                            }
                            else if (MarkupType == "percentage") {
                                PublishedFare = parseFloat(flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount) * PublishedFare / 100;
                            }
                        }
                        else {
                            //Net Fare Markup
                            FareMarkup = parseFloat(MA_NetFare) + parseFloat(NetFare);
                        }
                    //}
                    //else {
                    //    //Independent agency or consolidator
                    //    //Code for publish fare and net fare markup
                    //    $.each(Consolidator_Markup_Array, function (Markup_key, Markup_value) {

                    //        if (Markup_value.IATACode == marketingAirlineCode) {
                    //            // 
                    //            MarkupType = Markup_value.PublishedMarkupType;
                    //            PublishedFare = Markup_value.PublishedFareMarkup;
                    //            NetFare = Markup_value.NetfareMarkup;
                    //            PublishFareMarkupExist = true;
                    //            return false;

                    //        }
                    //    });

                    //    if (PassengerTypeQuantity == "ADT") {
                    //        //Public Fare
                    //        if (MarkupType == "value") {
                    //            PublishedFare = PublishedFare;
                    //        }
                    //        else if (MarkupType == "percentage") {
                    //            PublishedFare = parseFloat(flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount) * PublishedFare / 100;
                    //        }
                    //    }
                    //    else {
                    //        //Net Fare Markup
                    //        FareMarkup = parseFloat(NetFare);
                    //    }
                    //    //Independent agency or consolidator
                    //}

                    var total = flightSegment.AirItineraryPricingInfo != undefined ?
                        parseFloat(flightSegment.AirItineraryPricingInfo.ItinTotalFare.EquivFare.Amount) + parseFloat(FareMarkup) : 0;
                    var finalTotal = flightSegment.AirItineraryPricingInfo != undefined ?
                        parseFloat(flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount) + parseFloat(FareMarkup) : 0;
                    //
                    //debugger;
					var  matchAirline= new Array();
					if(blockedAirArray == '')
					  matchAirline= new Array();
				  else{
                     matchAirline = blockedAirArray.filter(function (item) {
                        if (item.IATACode == marketingAirlineCode)
                            return item;
                    });
				  }
                    var isDisplay = true;
                    if (matchAirline.length > 0)
                    {
                        if (PassengerTypeQuantity == "ADT" && matchAirline[0].IsPublicMarkup) {
                            isDisplay = false;
                            blockedAirlineCount += 1;
                        }
                        else if (matchAirline[0].IsNetfareMarkup) {                            
                            isDisplay = false;
                            blockedAirlineCount += 1;
                        }
                    }

                    //var te = blockedAirArray.indexOf(marketingAirlineCode);
                  
                    //if (blockedAirArray.length > 0 && blockedAirArray.indexOf(marketingAirlineCode) > -1) {
                    //    isDisplay = false;
                    //    blockedAirlineCount += 1;
                    //}
                    
                    if (isDisplay) {
                        //alert("DirectFlight-" + IsDirectFlight);
                        //debugger
                        if (IsDirectFlight == true) {
                            var _a = FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length;
                            if (SelectionType == "1") {
                                if ((FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length > 0) && (FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length == 1)) {

                                    var cancellationFee = "", changeFee = "";
                                    var cancellationBefore = 0, cancellationAfter = 0, changeBefore = 0, changeAfter = 0;

                                    $.each(flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerFare.PenaltiesInfo.Penalty, function (key, penalty) {
                                        if (penalty.Type == "Exchange") {
                                            if (penalty.Applicability == "Before") {
                                                cancellationBefore = (penalty.Amount == undefined) ? "Non-Changeable" : parseFloat(penalty.Amount).toFixed(2);
                                            }
                                            if (penalty.Applicability == "After") {
                                                cancellationAfter = (penalty.Amount == undefined) ? "Non-Changeable" : parseFloat(penalty.Amount).toFixed(2);
                                            }

                                        }
                                        if (penalty.Type == "Refund") {
                                            if (penalty.Applicability == "Before") {
                                                if (penalty.Refundable)
                                                    changeBefore = (penalty.Amount == undefined) ? "Non-refundable" : parseFloat(penalty.Amount).toFixed(2);
                                                else
                                                    changeBefore = "No Refund";
                                            }
                                            if (penalty.Applicability == "After") {
                                                if (penalty.Refundable)
                                                    changeAfter = (penalty.Amount == undefined) ? "Non-refundable" : parseFloat(penalty.Amount).toFixed(2);
                                                else
                                                    changeAfter = "No Refund";

                                            }
                                        }
                                    });
                                    cancellationFee = '<div class="customTip">   Change Fee   <span class="tooltiptext"> Before Departure :' + cancellationBefore + '<br>After Departure:' + cancellationAfter + '<br><span style="font-size:10px;">Amount displayed in CAD</span></span></div>'
                                    changeFee = '<div class="customTip">     Cancellation Fee     <span class="tooltiptext"> Before Departure: ' + changeBefore + '<br>After Departure:' + changeAfter + '<br><span style="font-size:10px;">Amount displayed in CAD</span></span></div>'




                                    var baggageAllowance = new Array();
                                    var inUnits = false;
                                    var allowedBaggage;
                                    $.each(flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerFare.TPA_Extensions.BaggageInformationList.BaggageInformation, function (key, val) {
                                        $.each(val.Allowance, function (k, v) {
                                            if (v.Pieces == undefined) {
                                                inUnits = true;
                                                allowedBaggage = (v.Weight == undefined) ? '' : (v.Weight + ' ' + v.Unit);
                                            }
                                            else {
                                                baggageAllowance.push(v.Pieces);
                                            }
                                        });


                                    });
                                    if (!inUnits)
                                        allowedBaggage = Math.min.apply(Math, baggageAllowance);

                                    var baggageAllowance = '<div class="customTip">    Baggage   <span class="tooltiptext"> Quantity : ' + allowedBaggage + '</span></div>'

                                    var miscFields = '<div class="col-sm-12 no-padding" ><div class="col-sm-4 no-padding">' + baggageAllowance + '</div><div class="col-sm-4">' + cancellationFee + '</div><div class="col-sm-4">' + changeFee + '</div></div>'


                                    dynamicDetails += "<div class='table-responsive' style='margin-bottom:30px;'>";
                                    dynamicDetails += "<table class='table without_tab'><thead><tr>";
                                    dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong><span class='total-p'>Final Total Price(incl fee)  :</span>     " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.EquivFare.CurrencyCode + " " + total.toFixed(2) + " + " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + " " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + (FareMarkup > 0 ? "<ul>" + flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + "</ul>" : flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode) + " " + (PublishFareMarkupExist == true ? "<span style='text-decoration:  underline;'>" + finalTotal.toFixed(2) + "</span>" : finalTotal.toFixed(2)) + "</strong><br>" + miscFields + "  </div>";
                                    dynamicDetails += "<div class='col-md-3 rt-align'><button class='btn btn-warning myAirCartSelector btn btn-sm btn-round margin-top'  value='btn-sm' type='button' data-MApublishedfare='" + MA_PublishedFare + "'  data-Cpublishedfare='" + PublishedFare + "' data-param='" + FlightSegment_value.FlightSegmentID + "'>Book Now <i class='fa fa-plane' aria-hidden='true'></i></button>";
                                    dynamicDetails += "</div></th></tr></thead>";
                                    dynamicDetails += "<tbody class='pink-bg'>";
                                    dynamicDetails += " <tr class='txt-yellow'>";
                                    dynamicDetails += " <td colspan='5'><i class='fa fa-check' aria-hidden='true'></i> Best Price Guarantee! </td>";
                                    dynamicDetails += "</tr>";
                                    dynamicDetails += "</tbody>";
                                }
                                else {
                                    return true;
                                }
                            }
                            else {
                                if ((FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length > 0) && (FlightSegment_value.FlightSegments[SubFlightCount + 1].FlightSegments.length > 0) && (FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length == 1) && (FlightSegment_value.FlightSegments[SubFlightCount + 1].FlightSegments.length == 1)) {


                                    var cancellationFee = "", changeFee = "";
                                    var cancellationBefore = 0, cancellationAfter = 0, changeBefore = 0, changeAfter = 0;

                                    $.each(flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerFare.PenaltiesInfo.Penalty, function (key, penalty) {
                                        if (penalty.Type == "Exchange") {
                                            if (penalty.Applicability == "Before") {
                                                cancellationBefore = (penalty.Amount == undefined) ? "Non-Changeable" : parseFloat(penalty.Amount).toFixed(2);
                                            }
                                            if (penalty.Applicability == "After") {
                                                cancellationAfter = (penalty.Amount == undefined) ? "Non-Changeable" : parseFloat(penalty.Amount).toFixed(2);
                                            }

                                        }
                                        if (penalty.Type == "Refund") {
                                            if (penalty.Applicability == "Before") {
                                                if (penalty.Refundable)
                                                    changeBefore = (penalty.Amount == undefined) ? "Non-refundable" : parseFloat(penalty.Amount).toFixed(2);
                                                else
                                                    changeBefore = "No Refund";
                                            }
                                            if (penalty.Applicability == "After") {
                                                if (penalty.Refundable)
                                                    changeAfter = (penalty.Amount == undefined) ? "Non-refundable" : parseFloat(penalty.Amount).toFixed(2);
                                                else
                                                    changeAfter = "No Refund";

                                            }
                                        }
                                    });
                                    cancellationFee = '<div class="customTip">   Change Fee    <span class="tooltiptext"> Before Departure :' + cancellationBefore + '<br>After Departure :' + cancellationAfter + '<br><span style="font-size:10px;">Amount displayed in CAD</span></span></div>'
                                    changeFee = '<div class="customTip">      Cancellation Fee   <span class="tooltiptext"> Before Departure : ' + changeBefore + '<br>After Departure:' + changeAfter + '<br><span style="font-size:10px;">Amount displayed in CAD</span></span></div>'




                                    var baggageAllowance = new Array();
                                    var inUnits = false;
                                    var allowedBaggage;
                                    $.each(flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerFare.TPA_Extensions.BaggageInformationList.BaggageInformation, function (key, val) {
                                        $.each(val.Allowance, function (k, v) {
                                            if (v.Pieces == undefined) {
                                                inUnits = true;
                                                allowedBaggage = (v.Weight == undefined) ? '' : (v.Weight + ' ' + v.Unit);
                                            }
                                            else {
                                                baggageAllowance.push(v.Pieces);
                                            }
                                        });


                                    });
                                    if (!inUnits)
                                        allowedBaggage = Math.min.apply(Math, baggageAllowance);
                                    var baggageAllowance = '<div class="customTip">    Baggage   <span class="tooltiptext"> Quantity : ' + allowedBaggage + '</span></div>'

                                    var miscFields = '<div class="col-sm-12 no-padding" ><div class="col-sm-4 no-padding">' + baggageAllowance + '</div><div class="col-sm-4">' + cancellationFee + '</div><div class="col-sm-4">' + changeFee + '</div></div>'




                                    dynamicDetails += "<div class='table-responsive' style='margin-bottom:30px;'>";
                                    dynamicDetails += "<table class='table without_tab'><thead><tr>";
                                    dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong>  Final Total Price(incl fee)  :     " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.EquivFare.CurrencyCode + " " + total.toFixed(2) + " + " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + " " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + " " + (PublishFareMarkupExist == true ? "<span style='text-decoration:  underline;'>" + finalTotal.toFixed(2) + "</span>" : finalTotal.toFixed(2)) +  miscFields + "</strong> " +" </div>";
                                    dynamicDetails += "<div class='col-md-3 rt-align'><button class='btn btn-warning myAirCartSelector btn btn-sm btn-round margin-top'  value='btn-sm' type='button' data-MApublishedfare='" + MA_PublishedFare + "'  data-Cpublishedfare='" + PublishedFare + "' data-param='" + FlightSegment_value.FlightSegmentID + "'>Book Now <i class='fa fa-plane' aria-hidden='true'></i></button>";
                                    dynamicDetails += "</div></th></tr></thead>";
                                    dynamicDetails += "<tbody class='pink-bg'>";
                                    dynamicDetails += " <tr class='txt-yellow'>";
                                    dynamicDetails += " <td colspan='5'><i class='fa fa-check' aria-hidden='true'></i> Best Price Guarantee! </td>";
                                    dynamicDetails += "</tr>";
                                    dynamicDetails += "</tbody>";
                                }
                                else {
                                    return true;
                                }
                            }

                        }
                        else {
                            var cancellationFee = "", changeFee = "";
                            var cancellationBefore = 0, cancellationAfter = 0, changeBefore = 0, changeAfter = 0;

                            $.each(flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerFare.PenaltiesInfo.Penalty, function (key, penalty) {
                                if (penalty.Type == "Exchange") {
                                    if (penalty.Applicability == "Before") {
                                        cancellationBefore = (penalty.Amount == undefined) ? "Non-Changeable" : parseFloat(penalty.Amount).toFixed(2);
                                    }
                                    if (penalty.Applicability == "After") {
                                        cancellationAfter = (penalty.Amount == undefined) ? "Non-Changeable" : parseFloat(penalty.Amount).toFixed(2);
                                    }

                                }
                                if (penalty.Type == "Refund") {
                                    if (penalty.Applicability == "Before") {
                                        if (penalty.Refundable)
                                            changeBefore = (penalty.Amount == undefined) ? "Non-refundable" : parseFloat(penalty.Amount).toFixed(2);
                                        else
                                            changeBefore = "No Refund";
                                    }
                                    if (penalty.Applicability == "After") {
                                        if (penalty.Refundable)
                                            changeAfter = (penalty.Amount == undefined) ? "Non-refundable" : parseFloat(penalty.Amount).toFixed(2);
                                        else
                                            changeAfter = "No Refund";

                                    }
                                }
                            });
                            cancellationFee = '<div class="customTip">    Change Fee     <span class="tooltiptext"> Before Departure :' + cancellationBefore + '<br>After Departure:' + cancellationAfter + '<br><span style="font-size:10px;">Amount displayed in CAD</span></span></div>'
                            changeFee = '<div class="customTip">   Cancellation Fee    <span class="tooltiptext"> Before Departure: ' + changeBefore + '<br>After Departure:' + changeAfter + '<br><span style="font-size:10px;">Amount displayed in CAD</span></span></div>'



                            var baggageAllowance = new Array();
                            var inUnits = false;
                            var allowedBaggage;
                            $.each(flightSegment.AirItineraryPricingInfo.PTC_FareBreakdowns.PTC_FareBreakdown[0].PassengerFare.TPA_Extensions.BaggageInformationList.BaggageInformation, function (key, val) {
                                $.each(val.Allowance, function (k, v) {
                                    if (v.Pieces == undefined) {
                                        inUnits = true;
                                        allowedBaggage = (v.Weight == undefined) ? '' : (v.Weight + ' ' + v.Unit);
                                    }
                                    else {
                                        baggageAllowance.push(v.Pieces);
                                    }
                                });


                            });
                            if (!inUnits)
                                allowedBaggage = Math.min.apply(Math, baggageAllowance);
                            var baggageAllowance = '<div class="customTip">    Baggage   <span class="tooltiptext"> Quantity : ' + allowedBaggage + '</span></div>'

                            var miscFields = '<a class="additional-info" href="#">Additional Info</a><div class="col-sm-12 show-info no-padding"><div class="col-sm-4 no-padding">' + baggageAllowance + '</div><div class="col-sm-4">' + cancellationFee + '</div><div class="col-sm-4">' + changeFee + '</div></div>'

                            dynamicDetails += "<div class='table-responsive' style='margin-bottom:30px;'>";
                            dynamicDetails += "<table class='table without_tab'><thead><tr>";
                            dynamicDetails += "<th colspan='5'> <div class='col-md-9'><strong>  <span class='total-p'>Final Total Price(incl fee)  :</span>     " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.EquivFare.CurrencyCode + " " + total.toFixed(2) + " + " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].CurrencyCode + " " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax[0].Amount + " (taxes) = " + flightSegment.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode + " " + (PublishFareMarkupExist == true ? "<span style='text-decoration:  underline;'>" + finalTotal.toFixed(2) + "</span>" : finalTotal.toFixed(2))  + miscFields + "</strong></div>";



                            dynamicDetails += "<div class='col-md-3 rt-align'><button class='btn btn-warning myAirCartSelector btn btn-sm btn-round margin-top'  value='btn-sm' type='button' data-MApublishedfare='" + MA_PublishedFare + "'  data-Cpublishedfare='" + PublishedFare + "' data-param='" + FlightSegment_value.FlightSegmentID + "'>Book Now <i class='fa fa-plane' aria-hidden='true'></i></button>";
                            dynamicDetails += "</div></th></tr></thead>";
                            dynamicDetails += "<tbody class='pink-bg'>";
                            dynamicDetails += " <tr class='txt-yellow'>";
                            dynamicDetails += " <td colspan='5'><i class='fa fa-check' aria-hidden='true'></i> Best Price Guarantee! </td>";
                            dynamicDetails += "</tr>";
                            dynamicDetails += "</tbody>";

                        }
                        var flighthours = "";
                        var flightminutes = "";
                        var OriginDestinationElapsedTime = "";

                        //userIsYoungerThan18 ? "Minor" : "Adult"
                        $.each(FlightSegment_value.FlightSegments, function (All_key, All_value) {

                            if ((All_value.FlightSegments.length > 0) && (All_value.FlightSegments.length == 1)) {
                                console.log("----------Direct Flights----------");
                            }
                            else if ((All_value.FlightSegments.length > 0) && (All_value.FlightSegments.length > 1)) {
                                console.log("----------Non Direct Flights----------");
                            }
                            if (IsDirectFlight == true) {

                                var _b = All_value.FlightSegments.length;
                                if (SelectionType == "1") {
                                    if ((All_value.FlightSegments.length > 0) && (All_value.FlightSegments.length == 1)) {
                                        //alert('Direct Flight');[]
                                        DirectFlightCount += 1;
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

                                            //if (fromFilter) {
                                            //    if (!priceRangeFilter) {
                                            //        Segment_value = Segment_value.FlightSegments[0];
                                            //    }
                                            //}

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
                                            AirCode_Array.push(Segment_value.MarketingAirline.Code);
                                            AirCode_Array.push(Segment_value.OperatingAirline.Code);

                                            dynamicDetails += "<tr>";
                                            if (window.location.href.indexOf("/air") > -1) {
                                                dynamicDetails += "<td><img src='/air/Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";
                                            }
                                            else {
                                                dynamicDetails += "<td><img src='/Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";
                                            }


                                            dynamicDetails += "<td><strong1>" + Airline_Name(Segment_value.MarketingAirline.Code) + "</strong1><br> Flight # <strong>" + Segment_value.OperatingAirline.Code + "-" + Segment_value.OperatingAirline.FlightNumber + "</strong><br>  Optd by <strong1>" + Airline_Name(Segment_value.OperatingAirline.Code) + "</strong1></td>";
                                            dynamicDetails += "<td><strong>" + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.DepartureDateTime)) + "<br> " + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.ArrivalDateTime)) + "</strong></td>";
                                            //dynamicDetails += "<td><strong>" + Segment_value.DepartureDateTime + "<br> " + Segment_value.ArrivalDateTime + "</strong></td>";
                                            dynamicDetails += "<td><strong>" + AirPort_Name(Segment_value.DepartureAirport.LocationCode) + "<br> " + AirPort_Name(Segment_value.ArrivalAirport.LocationCode) + "</strong></td>";
                                            //
                                            dynamicDetails += "<td class='rt-align' style='padding:10px;'>" + " Coach <br><div style='width:62px;'>" + flighthours + "H " + flightminutes + "M</div></td>";

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
                                    }
                                    else {
                                        return true;
                                    }
                                }
                                else {
                                    //if ((All_value.FlightSegments.length > 0) && (All_value.FlightSegments.length == 1)) {
                                    if ((FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length > 0) && (FlightSegment_value.FlightSegments[SubFlightCount].FlightSegments.length == 1)) {
                                        //alert('Direct Flight');
                                        DirectFlightCount += 1;
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

                                            //if (fromFilter) {
                                            //    if (!priceRangeFilter) {
                                            //        Segment_value = Segment_value.FlightSegments[0];
                                            //    }
                                            //}

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

                                            AirCode_Array.push(Segment_value.MarketingAirline.Code);
                                            AirCode_Array.push(Segment_value.OperatingAirline.Code);

                                            dynamicDetails += "<tr>";
                                            if (window.location.href.indexOf("/air") > -1) {
                                                dynamicDetails += "<td><img src='/air/Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";
                                            }
                                            else {
                                                dynamicDetails += "<td><img src='/Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";
                                            }
                                            dynamicDetails += "<td><strong1>" + Airline_Name(Segment_value.MarketingAirline.Code) + "</strong1><br> Flight # <strong>" + Segment_value.OperatingAirline.Code + "-" + Segment_value.OperatingAirline.FlightNumber + "</strong><br>  Optd by  <strong1>" + Airline_Name(Segment_value.OperatingAirline.Code) + "</strong1></td>";
                                            dynamicDetails += "<td><strong>" + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.DepartureDateTime)) + "<br> " + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.ArrivalDateTime)) + "</strong></td>";
                                            //dynamicDetails += "<td><strong>" + Segment_value.DepartureDateTime + "<br> " + Segment_value.ArrivalDateTime + "</strong></td>";
                                            dynamicDetails += "<td><strong>" + AirPort_Name(Segment_value.DepartureAirport.LocationCode) + "<br> " + AirPort_Name(Segment_value.ArrivalAirport.LocationCode) + "</strong></td>";
                                            //
                                            dynamicDetails += "<td class='rt-align' style='padding:10px;'>" + " Coach <br><div style='width:62px;'>" + flighthours + "H " + flightminutes + "M</div></td>";

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
                                    }
                                    else {
                                        return true;
                                    }
                                }

                            }
                            else {
                                NonDirectFlightCount += 1;
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

                                    //if (fromFilter) {
                                    //    if (!priceRangeFilter) {
                                    //        Segment_value = Segment_value.FlightSegments[0];
                                    //    }
                                    //}

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

                                    AirCode_Array.push(Segment_value.MarketingAirline.Code);
                                    AirCode_Array.push(Segment_value.OperatingAirline.Code);

                                    dynamicDetails += "<tr>";
                                    if (window.location.href.indexOf("/air") > -1) {
                                        dynamicDetails += "<td><img src='/air/Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";
                                    }
                                    else {
                                        dynamicDetails += "<td><img src='/Content/Images/Airlines_Logo/" + Segment_value.MarketingAirline.Code + ".gif'  alt=''></td>";
                                    }


                                    dynamicDetails += "<td><strong1>" + Airline_Name(Segment_value.MarketingAirline.Code) + "</strong1><br> Flight # <strong>" + Segment_value.OperatingAirline.Code + "-" + Segment_value.OperatingAirline.FlightNumber + "</strong><br>  Optd by <strong1>" + Airline_Name(Segment_value.OperatingAirline.Code) + "</strong1></td>";
                                    dynamicDetails += "<td><strong>" + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.DepartureDateTime)) + "<br> " + $.formatDateTime('g:iia DD dd MM', new Date(Segment_value.ArrivalDateTime)) + "</strong></td>";
                                    //dynamicDetails += "<td><strong>" + Segment_value.DepartureDateTime + "<br> " + Segment_value.ArrivalDateTime + "</strong></td>";
                                    dynamicDetails += "<td><strong>" + AirPort_Name(Segment_value.DepartureAirport.LocationCode) + "<br> " + AirPort_Name(Segment_value.ArrivalAirport.LocationCode) + "</strong></td>";
                                    //
                                    dynamicDetails += "<td class='rt-align' style='padding:10px;'>" + " Coach <br><div style='width:62px;'>" + flighthours + "H " + flightminutes + "M</div></td>";

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
                            }
                            SubFlightCount += 1;
                        });
                        //alert('Finally directflight Count-' + DirectFlightCount)
                        //alert('flight Count-' + FlightCount)
                        if ((IsDirectFlight == false) || (DirectFlightCount > 0)) {
                            dynamicDetails += "</table>";
                            dynamicDetails += "</div>";
                        }
                    }
                }
                FlightCount += 1;
                SubFlightCount = 0;
            });            
            

            //alert(IsDirectFlight);
            //alert(DirectFlightCount);
            if (((IsDirectFlight == true) && (DirectFlightCount == 0)) || (blockedAirlineCount == FilterAllFlightSegment_Array.length)) {
                //alert("Non Direct Flight");

                dynamicDetails += "<tr><td colspan='7' style='align-items: center; text-align: center;'> No Direct flights are available for the selected criteria, please change your criteria and try again</td></tr>";
            }
        }
        else {
            // 
            dynamicDetails += "<tr><td colspan='7' style='align-items: center; text-align: center;'> No flights are available for the selected criteria, please change your criteria and try again</td></tr>";
        }
        $("#Result_Div_Panel").removeClass("hidden");
        //alert(dynamicDetails);
        //console.log(dynamicDetails);
        $("#Search-Widgets-Result-Panel").empty().append(dynamicDetails);
    }
    catch (e) {
        //alert(e);
        var error = e;
        error = e;
        //

        HideWaitProgress();
    }
    SliderActionStatus = true;
}

function BindResutModuleDataNew(myResult_Data_Array) {
    try {
        //   
        BindDisplayResutModuleDataNew(myResult_Data_Array);
        BindFilterAirlinesModuleResutNew(myResult_Data_Array);
        BindFilterDepartingModuleResutNew(myResult_Data_Array);
    }
    catch (e) {
        var error = e;
        error = e;
        HideWaitProgress();
    }
    //
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

            //console.log("--------Airline Name-------------");
            //console.log(AllFlightSegment_Array.FulterUniqueAirLineName());
            //console.log("--------Airline Name-------------");

            var myUniquesResultDataArray = AllFlightSegment_Array.FulterUniqueAirLineName();
            var newArray = [];
            $.each(myUniquesResultDataArray, function (All_key, All_value) {
                $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
                    newArray.push(Segment_value.OperatingAirline.Code);
                    newArray.push(Segment_value.MarketingAirline.Code);
                });
            });

            console.log("AirCode_Array");
            console.log(AirCode_Array);
            console.log("AirCode_Array");

            console.log("newArray");
            console.log(newArray);
            console.log("newArray");

            var uniqueArray = AirCode_Array.filter(function (item, pos) {
                return AirCode_Array.indexOf(item) == pos;
            });

            console.log("uniqueArray");
            console.log(uniqueArray);
            console.log("uniqueArray");

            $.each(uniqueArray, function (k, v) {
                var Airline = Airline_Name(v);
                var AirlineToolTip = Airline;
                //
                if (Airline.length > 15) {
                    Airline = Airline.substring(0, 12);
                    Airline = Airline.split(" ");
                    if (Airline.length > 1) {

                        Airline = Airline[0] + " " + Airline[1] + "...";
                    }
                    else {
                        Airline = Airline[0] + "...";
                    }

                }
                if (Airline.length > 0) {
                    dynamicDetails += "<div class='checkbox row'><div class='col-md-7' style='padding-right:0'><label><input type='checkbox' id='" + v + "' name='AirlinesGroup' value='" + v + "' checked=''><span title='" + AirlineToolTip + "'>" + Airline + "</span> </label></div><div class='col-md-5' style='padding-left:0'></div></div>";
                }
                //   console.log("details-");
                // console.log(dynamicDetails);
            });
            //$.each(myUniquesResultDataArray, function (All_key, All_value) {
            //    DataStatus = 0;
            //    $.each(All_value.FlightSegments, function (Segment_key, Segment_value) {
            //        if (DataStatus == 0) {
            //            DataStatus = (DataStatus + 1);
            //            dynamicDetails += "<div class='checkbox row'><div class='col-md-7' style='padding-right:0'><label><input type='checkbox' id=id='" + Segment_value.OperatingAirline.Code + "' name='AirlinesGroup' value='" + Segment_value.OperatingAirline.Code + "' checked=''>" + Airline_Name(Segment_value.OperatingAirline.Code) + " </label></div><div class='col-md-5' style='padding-left:0'></div></div>";
            //        }
            //    });
            //});
            $("#Div_Search-Result-Airlines").empty().append(dynamicDetails);
        }
    }
    catch (e) {
        // HideWaitProgress();
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
        // HideWaitProgress();
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
                var toAdd = true;
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
                            isValidAirline = 0;
                            toAdd = false;
                            //return;
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
                if (toAdd) {
                    //if (isValidAirline == 1) {
                    myRefineResultDataArray.push({
                        FlightSegmentID: dt_value.FlightSegmentID,
                        FlightSegmentDirection: Flight_Segment_Direction,
                        FlightSegments: AllFlightSegmentOptions_Array
                    });
                    // }
                }
            });
            fromFilter = false;
            //   if (myRefineResultDataArray.length > 0) {
            BindDisplayResutModuleDataNew(myRefineResultDataArray);
            BindFilterAirlinesModuleResutNew(myRefineResultDataArray);
            // }
            // else { BindDisplayResutModuleDataNew(BFMXAllFlightSegmentItineraryPricingResult); }

        }
    }
    catch (e) {
        HideWaitProgress();
    }
    //
    //HideWaitProgress();
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
                var segmentValue;
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
                    if (FlightSegment_value != undefined) {

                        var arr = {
                            FlightSegmentID: dt_value.FlightSegmentID,
                            FlightSegmentDirection: dt_value.FlightSegmentDirection,
                            FlightSegments: FlightSegment_value
                        };

                        myRefineResultDataArray.push(arr);
                    }
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
    //
    HideWaitProgress();
}

function FilterSearch_AllFilterCombined(MinPriceRange, MaxPriceRange, MinDepartureSelection, MaxDepartureSelection, MinArrivalSelection, MaxArrivalSelection) {
    //
    ShowWaitProgress();
    var myRefineResultDataArray = new Array(), FlightSegment_Array = new Array(), AllFlightSegmentOptions_Array = new Array();
    var toAdd = true;
    var myAirportDataArray = new Array();
    $('#Div_Search-Result-Airlines input[type="checkbox"]:checked').each(function () {
        myAirportDataArray.push($(this).val());
    });
    var isValidAirline = 0;

    BFMXAllFlightSegmentItineraryPricingResult = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
    if (BFMXAllFlightSegmentItineraryPricingResult != null) {
        var MinNewPriceRange = MinPriceRange;
        var MaxNewPriceRange = MaxPriceRange;
        var MaxArrivalSelection = MaxArrivalSelection;
        var MinArrivalSelection = MinArrivalSelection;
        var MinDepartureSelection = MinDepartureSelection;
        var MaxDepartureSelection = MaxDepartureSelection;

        $.each(BFMXAllFlightSegmentItineraryPricingResult, function (dt_key, dt_value) {

            AllFlightSegmentOptions_Array = new Array();
            var Flight_Segment_Direction = dt_value.FlightSegmentDirection;
            var toAdd = true;

            $.each(dt_value.FlightSegments, function (FlightSegment_key, FlightSegment_value) {
                var OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;
                FlightSegment_Array = new Array();
                isValidAirline = 0;
                //var _segmentValue = FlightSegment_value.FlightSegments[0];
                //var OriginDestinationElapsedTime = _segmentValue.ElapsedTime;
                var dtvalueFlightSegments = FlightSegment_value.FlightSegments[0];

                var Departure = dtvalueFlightSegments.DepartureDateTime.split("T")[1].split(":")[0];
                Departure = parseInt($.formatDateTime('hh', new Date(dtvalueFlightSegments.DepartureDateTime)));

                //var OriginDestinationElapsedTime = FlightSegment_value.ElapsedTime;
                var dtvalueFlightSegments_Arrival = FlightSegment_value.FlightSegments[FlightSegment_value.FlightSegments.length - 1];
                var Arrival = dtvalueFlightSegments_Arrival.ArrivalDateTime.split("T")[1].split(":")[0];
                Arrival = parseInt($.formatDateTime('hh', new Date(dtvalueFlightSegments_Arrival.ArrivalDateTime)));

                $.each(FlightSegment_value.FlightSegments, function (Segment_key, Segment_value) {

                    var TotalFare = Segment_value.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount;
                    var DAirLine = Segment_value.OperatingAirline.Code;

                    if ((TotalFare < parseFloat(MaxPriceRange)) && (TotalFare > parseFloat(MinPriceRange)) && (Departure <= parseInt(MaxDepartureSelection)) && (Departure >= parseInt(MinDepartureSelection)) && (Arrival <= parseInt(MaxArrivalSelection)) && (Arrival >= parseInt(MinArrivalSelection)) && (isValidCode(DAirLine, myAirportDataArray))) {
                        isValidAirline = 1;
                        FlightSegment_Array.push(Segment_value);
                    }
                    else {
                        isValidAirline = 0;
                        toAdd = false;
                    }

                });
                AllFlightSegmentOptions_Array.push({
                    ElapsedTime: OriginDestinationElapsedTime,
                    FlightSegments: FlightSegment_Array
                });
            });

            if (toAdd) {
                if (FlightSegment_Array.length > 0) {
                    myRefineResultDataArray.push({
                        FlightSegmentID: dt_value.FlightSegmentID,
                        FlightSegmentDirection: Flight_Segment_Direction,
                        FlightSegments: AllFlightSegmentOptions_Array
                    });
                }
            }
        });
        priceRangeFilter = true;
        fromFilter = false;
        if (myRefineResultDataArray.length > 0) {
            BindDisplayResutModuleDataNew(myRefineResultDataArray);
        }
        else { BindDisplayResutModuleDataNew(myRefineResultDataArray); }
    }
    //
    setTimeout(
  function () {
      //do something special
      HideWaitProgress();
  }, 1000);

}
/******************************Bind Filter Airports Module Result*************************/

/******************************Range Slider*************************/
var SliderActionStatus = false;
function GetRefineSearchType(RefineType, SearchValue) {
    try {
        var OriginDestinationElapsedTime = "";
        //alert(SliderActionStatus);
        if (SliderActionStatus) {

            ShowWaitProgress();
            SliderActionStatus = false;
            var myRefineResultDataArray = new Array(), FlightSegment_Array = new Array(), AllFlightSegmentOptions_Array = new Array();
            var MinNewPriceRange = $("#Price-range-slider").slider('value').split(";")[0];
            var MaxNewPriceRange = $("#Price-range-slider").slider('value').split(";")[1];
            var MinDepartureSelection = $("#Departure-range-slider").slider('value').split(";")[0];
            var MaxDepartureSelection = $("#Departure-range-slider").slider('value').split(";")[1];
            var MaxArrivalSelection = $("#Arrive-range-slider").slider('value').split(";")[1];
            var MinArrivalSelection = $("#Arrive-range-slider").slider('value').split(";")[0];
            BFMXAllFlightSegmentItineraryPricingResult = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
            FilterSearch_AllFilterCombined(MinNewPriceRange, MaxNewPriceRange, MinDepartureSelection, MaxDepartureSelection, MinArrivalSelection, MaxArrivalSelection);
        }
        else {
            // 
            HideWaitProgress();
        }
        //        
        //HideWaitProgress();
    } catch (e) {
        var ex = e;
    }
}
/******************************End Range Slider*************************/

$(function ($) {


    $('#Search-Widgets-Result-Panel').on('click', '.myAirCartSelector', function (e) {
        OnLoadGetPassengerDetailsList();
        localStorage.setItem("Origin", $('#txtOrigin').val());
        localStorage.setItem("Destination", $('#txtDestination').val());
        localStorage.setItem("SelectionName", $("input[name='radioSelectionName']:checked").val());
        //alert($("input[name='radioSelectionName']:checked").val());
        var myPaymentDataArray = new Array();
        var AllFlightSegmentOptions_Array = new Array();
        var AirFlightSegmentID = $(this).attr('data-param');
        AirCode_Array = [];
        //var AllFlightSegmentItineraryPricing = SolutionDataTraveler("GET", "BFMXAllFlightSegmentItineraryPricingResult");
        var AllFlightSegmentItineraryPricing = BFMXAllFlightSegmentItineraryPricingResult;
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
        //debugger;
        //MApublishedfare        
        var CPublishedFare = $(this).attr('data-Cpublishedfare');
        var MAPublishedFare = $(this).attr('data-MApublishedfare');
        sessionStorage.setItem("mapublishedfare", MAPublishedFare);
        sessionStorage.setItem("cpublishedfare", CPublishedFare);

        Airline_Boking_Process($(this).attr('data-param'));
    });
	
	
	 $('#Search-Widgets-Result-Panel').on('click', '.additional-info', function (e) { 
	 if($(window).width() <= 768){
			$(".show-info").toggle("fast");
			e.preventDefault();
			} 
	}); 
	
});



function Airline_Name(airline_code) {
    // console.log("Airline code-" + airline_code);
    var currentdate = '';
    //console.log("Airline list");
    // console.log(AirLineList);
    if (AirLineList == undefined) {
        AirLineList = SolutionDataTraveler("GET", "AirLineList");
    }
    if (AirLineList != undefined) {
        //console.log("Has Airline List");
        //
        $.each(AirLineList, function (dt_key, dt_value) {
            if (dt_value.airlinecode == airline_code) {
                currentdate = dt_value.airlinename;
                // console.log("Airline name-" + currentdate);
            }
        });
    }

    return currentdate;
}

function AirPort_Name(airPort_code) {
    var currentdate = '';
    if (AirPortNameCodeCoList == undefined) {
        AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
    }
    if (AirPortNameCodeCoList != undefined) {
        $.each(AirPortNameCodeCoList, function (dt_key, dt_value) {
            if (dt_value.airlinecode == airPort_code) {
                currentdate = dt_value.airlinename;
            }
        });
    }
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
            // HideWaitProgress();
            var App_Data = e.Data;
            var App_Data1 = e.Data1;
            SolutionDataTraveler("SET", "AirLineList", App_Data);
            SolutionDataTraveler("SET", "AirPortNameCodeCoList", App_Data1);
            AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
            AirLineList = SolutionDataTraveler("GET", "AirLineList");
            //console.log("Setting Airline List");
            //console.log(AirLineList);
            //console.log("AIrline List assigned");
        }
        function ResultCallBackError(e, xhr, opts) {
            //
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


function GetQuoteRSJSONfromOldSession() {
    try {
        function ResultCallBackSuccess(e, xhr, opts) {
            // HideWaitProgress();
            var App_Data = e;
            SolutionDataTraveler("SET", "BFMXSearchResult", App_Data);
            //console.log("----------------Search Result Data--------------");
            //console.log(App_Data);
            //console.log("----------------Search Result Data--------------");
            //FilteringBindDisplayResutData(App_Data.Result);
            FilteringBindDisplayResutData(App_Data.Data.Result);

        }
        function ResultCallBackError(e, xhr, opts) {
            //
            HideWaitProgress();
        }
        var Reqst_Resource = {
            folderName: localStorage.getItem("RequestID"),
            SearchText: "QuoteRS.log"
        };
        MasterAppConfigurationsServices("GET", CommonConfiguration.WebAPIServicesURL + "API/CommonService/GetQuoteRQRSService", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var ex = e;
    }
}

function GetQuoteRQJSONfromOldSession() {

    try {
        function ResultCallBackSuccess(e, xhr, opts) {
            // HideWaitProgress();
            //  
            var App_Data = e;
            //console.log("------------ee----------------");
            //console.log(e);
            //console.log("------------ee----------------");
            if (App_Data != 'undefined' && App_Data != null) {
                var SearchedDetailsData = JSON.parse(App_Data);
                ShowWaitProgress();
                //console.log("----------------------------");
                //alert(SearchedDetailsData);
                //console.log(SearchedDetailsData);

                var SelectionName = SearchedDetailsData.SelectionName;
                //alert("selectionname-" + SelectionName);
                if (SelectionName == 3) {
                   // $('#directflight-section').hide();
                    var origin = SearchedDetailsData.origin.split(";");
                    $.each(origin, function (i) {
                        var count = parseInt(i) + parseInt(1);
                        //console.log("val " + count + "- " + origin[i]);
                        $('#txtMLOrigin_' + count).val(origin[i]);
                        //alert('"' + origin[i].length + '"');
                        if (origin[i].length == "0") {
                            //  alert("in zero");
                            $('.myrow_' + count).hide();

                        }
                        else {
                            //    alert("Not zero .myrow_" + count);
                            $('.myrow_' + count).css("display", "block");
                        }
                    });

                    var destination = SearchedDetailsData.destination.split(";");
                    $.each(destination, function (i) {
                        var count = parseInt(i) + parseInt(1);
                        //  console.log("val " + count + "- " + destination[i]);
                        $('#txtMLDestination_' + count).val(destination[i]);
                    });
                    debugger;
                    var departuredate = SearchedDetailsData.departuredate.split(";");
                    $.each(departuredate, function (i) {
                        var count = parseInt(i) + parseInt(1);
                        //    console.log("val " + count + "- " + departuredate[i]);
                        $('#txtMLDate_' + count).val(departuredate[i]);

                    });
                }
                else if ((SelectionName == 2) || (SelectionName == 1)) {

                    $('#txtOrigin').val(SearchedDetailsData.origin);
                    $('#txtDestination').val(SearchedDetailsData.destination);
                    //  console.log("----------------------------");
                    $('#txtOriginDate').val(SearchedDetailsData.departuredate);
                    $('#txtDestinationDate').val(SearchedDetailsData.returndate);
                    if (SelectionName == 2) {
                        $('#date-range0').val(SearchedDetailsData.departuredate + " To " + SearchedDetailsData.returndate);
                    }
                    else {
                        $('#date-range1').val(SearchedDetailsData.departuredate);
                    }


                }

                $('#ddlAirline').val(SearchedDetailsData.Airline),
                $('#AirClass').val(SearchedDetailsData.AirClass),
                $('#noOfAdults').val(SearchedDetailsData.noOfAdults),
                $('#noOfChildrens').val(SearchedDetailsData.noOfChildrens),
                $('#noOfInfants').val(SearchedDetailsData.noOfInfants),

                $('[name="radioSelectionName"]').removeAttr('checked');
                //$("input[name=radioSelectionName][value='" + SelectionName + "']").attr('checked', 'checked');
                $("input[name=radioSelectionName][value=" + SelectionName + "]").prop('checked', true).trigger("click");



            }

            var today = new Date();
            var Christmas = new Date(localStorage.getItem("RequestID_CurrentTimeStamp"));
            var diff = (today - Christmas) / 1000;
            diff /= 60;
            var diffMins = Math.abs(Math.round(diff));

            //console.log(diffMins);


            if (diffMins < 2) {
                //alert("Less Than 2 min");
                GetQuoteRSJSONfromOldSession();
                //console.log("--------Time Under equal 2 min---------");
            }
            else {
                //Function To Reload Result
                //alert("greater Than 2 min");
                //console.log("--------more than 2 min API Call To Supplier API---------");
                $(".btn-success-Search").click();
            }



        }
        function ResultCallBackError(e, xhr, opts) {
            //
            HideWaitProgress();
        }
        var Reqst_Resource = {
            folderName: localStorage.getItem("RequestID"),
            SearchText: "QuoteRQ.log"
        };
        MasterAppConfigurationsServices("GET", CommonConfiguration.WebAPIServicesURL + "API/CommonService/GetQuoteRQRSService", Reqst_Resource, ResultCallBackSuccess, ResultCallBackError);
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
        var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");
        if ((CompanyTypeID == "2") || (CompanyTypeID == "4")) {
            var isloggedin = localStorage.getItem('is_loggedin');
            if ((isloggedin == "False") || (isloggedin == null)) {
                location.href = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=" + CommonConfiguration.AirProjectURL + "Payments";
            }
            else if (isloggedin == "True") {
                location.href = CommonConfiguration.AirProjectURL + "Payments";
            }
        }
        else {
            location.href = CommonConfiguration.AirProjectURL + "Payments";
        }
    } catch (e) {
        var error = e;
        error = e;
    }
}

$(function ($) {
    try {
        //alert(sessionStorage.getItem("IsFromPaymentPage"));
        if (GetQueryStringParameterValues('Search') == null) {
            if (sessionStorage.getItem("IsFromPaymentPage") == "True") {
                //ShowWaitProgress();
                //  
                //alert('Old Results');
                GetQuoteRQJSONfromOldSession();
            }
        }

        /*        if (localStorage.getItem("RequestID") === null) {
                }
                else {
        
                    //
                    if (sessionStorage.getItem("IsFromPaymentPage") == "True") {
                        ShowWaitProgress();
                      //  
                        GetQuoteRQJSONfromOldSession();
                    }
                }
                */
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
        //$("#txtMLDate_1").datepicker({
        //    startDate: new Date(),
        //    autoclose: true,
        //    format: 'yyyy-mm-dd',
        //    todayHighlight: true
        //});
        debugger;
        //var maxDate = new Date();
        //var minDate = new Date();
        //window.setInterval(function () {
        //    debugger;
        //    var txtMLDate_1 = $("#txtMLDate_1").val();
        //    var txtMLDate_2 = $("#txtMLDate_2").val();
        //    var txtMLDate_3 = $("#txtMLDate_3").val();
        //    var txtMLDate_4 = $("#txtMLDate_4").val();
        //    var dates = [];
        //    dates.push(new Date(txtMLDate_1))
        //    dates.push(new Date(txtMLDate_2))
        //    dates.push(new Date(txtMLDate_3))
        //    dates.push(new Date(txtMLDate_4))
        //    maxDate = new Date(Math.max.apply(null, dates));
        //    minDate = new Date(Math.min.apply(null, dates));


        //    if (maxDate.toString() == "Invalid Date") {
        //        maxDate = new Date();
        //    }

        //    if (minDate.toString() == "Invalid Date") {
        //        minDate = new Date();
        //    }
        //    $('#txtMLDate_1').data('daterangepicker').setStartDate('03/01/2014');
        //    $('#txtMLDate_1').data('daterangepicker').setEndDate('03/31/2014');
        //}, 500);

        $("#txtMLDate_1").datepicker({
            autoClose: true,
            minDate: 0,
            onSelect: function (dateText, inst) {
                $("#txtMLDate_2").datepicker("option", "minDate", new Date(dateText));
            },
            dateFormat: 'yy-mm-dd'
                //singleDate: true,
                //startDate:new Date(),
                //minDate: minDate,
                //maxDate: maxDate,
            });


            $("#col_txtMLDate_1").click(function () {
                //$("#txtMLDate_1").datepicker("show");
            });
            ///==================================================//
            ///==================================================//
        $("#txtMLDate_2").datepicker({
            autoClose: true,
            minDate: 0,
            onSelect: function (dateText, inst) {
                $("#txtMLDate_1").datepicker("option", "maxDate", new Date(dateText));
                $("#txtMLDate_3").datepicker("option", "minDate", new Date(dateText));
            },
            dateFormat: 'yy-mm-dd'
                //singleDate: true,
                //startDate: new Date(),
                //minDate: minDate,
                //maxDate: maxDate,
            });
            $("#col_txtMLDate_2").click(function () {
                //$("#txtMLDate_1").datepicker("show");
            });
            ///==================================================//
            ///==================================================//
        $("#txtMLDate_3").datepicker({
            autoClose: true,
            minDate: 0,
            onSelect: function (dateText, inst) {
                $("#txtMLDate_2").datepicker("option", "maxDate", new Date(dateText));
                $("#txtMLDate_4").datepicker("option", "minDate", new Date(dateText));
            },
            dateFormat: 'yy-mm-dd'
                //singleDate: true,
                //startDate: new Date(),
                //minDate: minDate,
                //maxDate: maxDate,
            });
            $("#col_txtMLDate_3").click(function () {
                // $("#txtMLDate_3").datepicker("show");
            });
            ///==================================================//
            ///==================================================//
        $("#txtMLDate_4").datepicker({
            autoClose: true,
            minDate: 0,
            onSelect: function (dateText, inst) {
                $("#txtMLDate_3").datepicker("option", "maxDate", new Date(dateText));
            },
            dateFormat: 'yy-mm-dd'
                //singleDate: true,
                //startDate: new Date(),
                //minDate: minDate,
                //maxDate: maxDate,
            });
            $("#col_txtMLDate_4").click(function () {
                // $("#txtMLDate_4").datepicker("show");
            });
       
        ///==================================================//
        ///==================================================//
        //$("#txtMLDate_5").dateRangePicker({
        //    autoClose: true,
        //    singleDate: true,
        //    startDate: new Date()
        //});
        //$("#col_txtMLDate_5").click(function () {
        //    //  $("#txtMLDate_5").datepicker("show");
        //});
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
                        CurrencyCode: sessionStorage.getItem("CurrencyCode")
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
                        PseudoCityCode: sessionStorage.getItem("PseudoCityCode")
                    }]
                }
            }
        };
        dynamicData = dynamicJsonData;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////\
        dynamicAirSearchRequestData = JSON.stringify(dynamicJsonData);
        //
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
        //
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
        //
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
    //
    //console.log('data--');
    //console.log(SearchContentData);
    //alert(SearchContentData.directOnly.toUpperCase());
    //console.log(SearchContentData.directOnly.toUpperCase());
    //
    //alert(SearchContentData.Airline.toUpperCase());
    $('#ddlAirline').val(SearchContentData.Airline);
    $("#AirClass").val(SearchContentData.FareType.toUpperCase());
    $("#noOfAdults").val(SearchContentData.numadults.toUpperCase());
    $("#noOfInfants").val(SearchContentData.numinfants.toUpperCase());
    $("#noOfChildrens").val(SearchContentData.nbchilds.toUpperCase());

    //
    var v = SearchContentData.directOnly.toUpperCase();
    //console.log("----------" + SearchContentData.directOnly.toUpperCase() + "-----------");
    //
    if (v == "ON") {
        console.log($('#directFlight').prop('checked', true));
    }
    else {
        console.log($('#directFlight').prop('checked', false));
    }

    try {
        if (SearchType != "") {
            switch (SearchType) {
                case "1":
                    $("input[name='radioSelectionName']").filter("[value='" + SearchType + "']").attr("checked", true);
                    $('#txtOrigin').val(SearchContentData.origin.toUpperCase());
                    $("#txtDestination").val(SearchContentData.destination.toUpperCase());
                    $("#txtOriginDate").val(SearchContentData.departuredate.toUpperCase());
                    $('#date-range1').val(SearchContentData.departuredate.toUpperCase());



                    break;
                case "2":
                    $("input[name='radioSelectionName']").filter("[value='" + SearchType + "']").attr("checked", true);
                    $('#txtOrigin').val(SearchContentData.origin.toUpperCase());
                    $("#txtDestination").val(SearchContentData.destination.toUpperCase());
                    $("#txtOriginDate").val(SearchContentData.departuredate.toUpperCase());
                    $("#txtDestinationDate").val(SearchContentData.returndate.toUpperCase());
                    $('#date-range0').val(SearchContentData.departuredate.toUpperCase() + " To " + SearchContentData.returndate.toUpperCase());


                    break;
                case "3":
                    $("input[name='radioSelectionName']").filter("[value='" + SearchType + "']").attr("checked", true);
                    var arr = [1, 2, 3, 4, 5];
                    var data = [SearchContentData.origin1, SearchContentData.origin2, SearchContentData.origin3, SearchContentData.origin4, SearchContentData.origin5];
                    var count = 0;
                    //alert(AppActionData);
                    console.log(data);
                    $.each(arr, function (index, value) {
                        if (typeof data[count] === "undefined") { }
                        else {
                            if (data[count].length > 0) {
                                //alert(data[count]);
                                //alert("-"+AppActionData);
                                AppActionData++;
                                $(".myrow_" + value).show();
                            }
                        }
                        count = count + 1;
                    });
                    //alert(AppActionData);
                   // $('#directflight-section').hide();
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

                    //$('#txtMLOrigin_5').val(SearchContentData.origin4.toUpperCase());
                    //$("#txtMLDestination_5").val(SearchContentData.destination4.toUpperCase());
                    //$("#txtMLDate_5").val(SearchContentData.departuredate4.toUpperCase());


                    break;
            }
        }
        //  
    } catch (e) {
        var error = e;
        error = error;
    }
}

function OnLoadGetQueryStringData() {

    var isloggedin = localStorage.getItem('is_loggedin');
    var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");
    if ((isloggedin == "True") || (CompanyTypeID == "2") || (CompanyTypeID == "4")) {
        // 
        ShowWaitProgress();
    }
    else {
        HideWaitProgress();
    }
    try {
        if (GetQueryStringParameterValues('Search') != null) {

            var Request_ContentData = JSON.parse(unescape(GetQueryStringParameterValues('Search')));

            SearchCondition((Request_ContentData.SearchType == "O") ? "1" : (Request_ContentData.SearchType == "R") ? "2" : (Request_ContentData.SearchType == "M") ? "3" : "");
            QueryStringDataSearchCondition(((Request_ContentData.SearchType == "O") ? "1" : (Request_ContentData.SearchType == "R") ? "2" : (Request_ContentData.SearchType == "M") ? "3" : ""), Request_ContentData);
            //    
            var RequestContentData = '';


            var _adult = parseInt(Request_ContentData.numadults);
            var _children = parseInt(Request_ContentData.nbchilds);
            var _infant = parseInt(Request_ContentData.numinfants);

            var airLineCode = Request_ContentData.Airline;
            var cabinCode = Request_ContentData.FareType;
            var directFlight = false;
            if (Request_ContentData.directOnly == "ON") {
                directFlight = true;
            }

            var TravelPreferences = { "VendorPref": [{ "Code": airLineCode, "PreferLevel": "Preferred" }], "CabinPref": [{ "Cabin": cabinCode, "PreferLevel": "Only" }] };
            if ((airLineCode == 'All') || (airLineCode == null)) {
                TravelPreferences = { "CabinPref": [{ "Cabin": cabinCode, "PreferLevel": "Only" }] };
            }

            var travelAvail;
            if (_adult > 0 && _children > 0 && _infant > 0) {

                travelAvail = [
                                            {
                                                "PassengerTypeQuantity": [
                                                {
                                                    "Code": "ADT",
                                                    "Quantity": parseInt(_adult),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                },
                                                {
                                                    "Code": "C07",
                                                    "Quantity": parseInt(_children),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                },
                                                {
                                                    "Code": "INF",
                                                    "Quantity": parseInt(_infant),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                }
                                                ]
                                            }
                ];
            }
            else if (_adult > 0 && _children > 0 && _infant == 0) {

                travelAvail = [
                                            {
                                                "PassengerTypeQuantity": [
                                                {
                                                    "Code": "ADT",
                                                    "Quantity": parseInt(_adult),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                },
                                                {
                                                    "Code": "C07",
                                                    "Quantity": parseInt(_children),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                }
                                                ]
                                            }
                ];
            }
            else if (_adult > 0 && _infant > 0 && _children == 0) {

                travelAvail = [
                                            {
                                                "PassengerTypeQuantity": [
                                                {
                                                    "Code": "ADT",
                                                    "Quantity": parseInt(_adult),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                },
                                                {
                                                    "Code": "INF",
                                                    "Quantity": parseInt(_infant),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                }
                                                ]
                                            }
                ];
            } else if (_adult > 0 && _infant == 0 && _children == 0) {

                travelAvail = [
                                            {
                                                "PassengerTypeQuantity": [
                                                {
                                                    "Code": "ADT",
                                                    "Quantity": parseInt(_adult),
                                                    "TPA_Extensions": {
                                                        "VoluntaryChanges": {
                                                            "Match": "Info"

                                                        }
                                                    }
                                                }
                                                ]
                                            }
                ];
            }
            //alert(Request_ContentData.SearchType);
            switch (Request_ContentData.SearchType) {
                case "O":
                    RequestContentData = JSON.stringify({
                        "OTA_AirLowFareSearchRQ": {
                            "OriginDestinationInformation": [
                              {
                                  "RPH": "1",
                                  "DepartureDateTime": Request_ContentData.departuredate + "T" + GetCurrentTime(),
                                  "OriginLocation": {
                                      "LocationCode": GetAirline_Code(Request_ContentData.origin.toUpperCase())
                                  },
                                  "DestinationLocation": {
                                      "LocationCode": GetAirline_Code(Request_ContentData.destination.toUpperCase())
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
                                "AirTravelerAvail": travelAvail,
                                "PriceRequestInformation": {
                                    "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                }
                            },
                            "TravelPreferences": TravelPreferences,
                            "DirectFlightsOnly": directFlight,
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
                    //GetAirline_Code($('#txtOrigin').val().toUpperCase());
                    RequestContentData = JSON.stringify({
                        "OTA_AirLowFareSearchRQ": {
                            "OriginDestinationInformation": [
                               {
                                   "RPH": "1",
                                   "DepartureDateTime": Request_ContentData.departuredate + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": GetAirline_Code(Request_ContentData.origin.toUpperCase())
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": GetAirline_Code(Request_ContentData.destination.toUpperCase())
                                   }
                               }, {
                                   "RPH": "2",
                                   "DepartureDateTime": Request_ContentData.returndate + "T" + GetCurrentTime(),
                                   "OriginLocation": {
                                       "LocationCode": GetAirline_Code(Request_ContentData.destination.toUpperCase())
                                   },
                                   "DestinationLocation": {
                                       "LocationCode": GetAirline_Code(Request_ContentData.origin.toUpperCase())
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
                                "AirTravelerAvail": travelAvail,
                                "PriceRequestInformation": {
                                    "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                }
                            },
                            "TravelPreferences": TravelPreferences,
                            "DirectFlightsOnly": directFlight,
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

                    var all_section = "";
                    var Section1 = "";
                    var Section2 = "";
                    var Section3 = "";
                    var Section4 = "";
                    var custom_arr1 = [];

                    if (Request_ContentData.origin1.length > 0) {

                        Section1 = {
                            "RPH": "1",
                            "DepartureDateTime": Request_ContentData.departuredate1 + "T" + GetCurrentTime(),
                            "OriginLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.origin1.toUpperCase())
                            },
                            "DestinationLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.destination1.toUpperCase())
                            }
                        };
                        custom_arr1.push(Section1);

                        all_section += Section1;
                    }
                    if (Request_ContentData.origin2.length > 0) {
                        Section2 = {
                            "RPH": "2",
                            "DepartureDateTime": Request_ContentData.departuredate2 + "T" + GetCurrentTime(),
                            "OriginLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.origin2.toUpperCase())
                            },
                            "DestinationLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.destination2.toUpperCase())
                            }
                        };
                        custom_arr1.push(Section2);
                        all_section += "," + Section2;
                    }
                    if (Request_ContentData.origin3.length > 0) {

                        Section3 = {
                            "RPH": "3",
                            "DepartureDateTime": Request_ContentData.departuredate3 + "T" + GetCurrentTime(),
                            "OriginLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.origin3.toUpperCase())
                            },
                            "DestinationLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.destination3.toUpperCase())
                            }
                        };
                        custom_arr1.push(Section3);
                        all_section += "," + Section3;
                    }
                    if (Request_ContentData.origin4.length > 0) {

                        Section4 = {
                            "RPH": "4",
                            "DepartureDateTime": Request_ContentData.departuredate4 + "T" + GetCurrentTime(),
                            "OriginLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.origin4.toUpperCase())
                            },
                            "DestinationLocation": {
                                "LocationCode": GetAirline_Code(Request_ContentData.destination4.toUpperCase())
                            }
                        };
                        custom_arr1.push(Section4);
                        all_section += "," + Section4;
                    }
                    //jsonArray1.concat(jsonArray2);
                    //var a = (Section1 != "" ? a.concat(Section1) : "") + (Section2 != "" ? Section2 : "") + (Section3 != "" ? Section3 : "") + (Section4 != "" ? a.concat(Section4) : "");
                    //var a = JSON.stringify(Section1, Section2);
                    //
                    RequestContentData = JSON.stringify({
                        "OTA_AirLowFareSearchRQ": {
                            "OriginDestinationInformation":
                                custom_arr1
                            //(Section1 != "" ? Section1 + "," : "")(Section2 != "" ? Section2 + "," : "")(Section3 != "" ? Section3 + "," : "")(Section4 != "" ? Section4 + "," : "")
                            //,{
                            //    "RPH": "2",
                            //    "DepartureDateTime": Request_ContentData.departuredate2 + "T" + GetCurrentTime(),
                            //    "OriginLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.origin2.toUpperCase())
                            //    },
                            //    "DestinationLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.destination2.toUpperCase())
                            //    }
                            //}, {
                            //    "RPH": "3",
                            //    "DepartureDateTime": Request_ContentData.departuredate3 + "T" + GetCurrentTime(),
                            //    "OriginLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.origin3.toUpperCase())
                            //    },
                            //    "DestinationLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.destination3.toUpperCase())
                            //    }
                            //}, {
                            //    "RPH": "4",
                            //    "DepartureDateTime": Request_ContentData.departuredate4 + "T" + GetCurrentTime(),
                            //    "OriginLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.origin4.toUpperCase())
                            //    },
                            //    "DestinationLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.destination4.toUpperCase())
                            //    }
                            //},
                            //{
                            //    "RPH": "5",
                            //    "DepartureDateTime": Request_ContentData.departuredate5 + "T" + GetCurrentTime(),
                            //    "OriginLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.origin5.toUpperCase())
                            //    },
                            //    "DestinationLocation": {
                            //        "LocationCode": GetAirline_Code(Request_ContentData.destination5.toUpperCase())
                            //    }
                            //}
                            ,
                            "TPA_Extensions": {
                                "IntelliSellTransaction": {
                                    "RequestType": {
                                        "Name": "50ITINS"
                                    }
                                }
                            },
                            "TravelerInfoSummary": {
                                "AirTravelerAvail": travelAvail,
                                "PriceRequestInformation": {
                                    "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                }
                            },
                            "TravelPreferences": TravelPreferences,
                            "DirectFlightsOnly": directFlight,
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
                //
                if (e.Data != null) {
                    //  
                    ShowWaitProgress();
                    var App_Data = e.Data;
                    //console.log("Call 1");
                    //console.log("---------Response-----------")
                    //console.log(e);
                    //console.log("---------Response-----------")

                    localStorage.setItem("RequestID", e.Data1);
                    //alert(App_Data.RequestID);

                    //console.log("---------------------------");

                    //console.log(App_Data);

                    //console.log("---------------------------");

                    var RequestID_CurrentTimeStamp = new Date();
                    localStorage.setItem("RequestID_CurrentTimeStamp", RequestID_CurrentTimeStamp)

                    SolutionDataTraveler("SET", "BFMXSearchResult", App_Data);
                    //   

                    FilteringBindDisplayResutData(App_Data.Result);
                }
            }
            function ResultCallBackError(e, xhr, opts) {
                // 
                HideWaitProgress();
                // 
                if (e.responseText == "Authentication Required") {
                    sessionStorage.setItem("IsFromPaymentPage", "False");
                    //alert(sessionStorage.getItem("IsFromPaymentPage") );
                    var urlpart = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=/";
                    var urlpart2 = $.urlParam('Search');
                    var url = urlpart + urlpart2;

                    window.location.href = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=" + CommonConfiguration.AirProjectURL + $.urlParam('Search');
                    return false;
                }
                else {
                    ShowWaitProgress();
                }

                ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
            }
            //
            var Selection_Name = $("input[name='radioSelectionName']:checked").val();
            var Origin = "";
            var Destination = "";
            var Return_Date = "";
            var Departure_Date = "";

            //alert(Selection_Name);
            if (Selection_Name == "3") {

                //If Selected Trip Type is MULTICITY
                $("input[name='txtmulticityOrigin[]']").each(function () {
                    var val = $(this).val();
                    if (val != 'undefined' && val != null && val != "") {
                        Origin += $(this).val() + ";";
                    }
                });
                $("input[name='txtmulticityDestination[]']").each(function () {
                    var val = $(this).val();
                    if (val != 'undefined' && val != null && val != "") {
                        Destination += $(this).val() + ";";
                    }
                });
                $("input[name='date-range-picker[]']").each(function (index) {
                    debugger;
                    var val = $(this).val();
                    if (val != 'undefined' && val != null && val != "") {
                        Departure_Date += $(this).val() + ";";
                    }
                });
            }
            else if ((Selection_Name == "2") || (Selection_Name == "1")) {
                Origin = $('#txtOrigin').val();
                Destination = $('#txtDestination').val();
                Departure_Date = $('#txtOriginDate').val();
                Return_Date = $('#txtDestinationDate').val();

            }
            //            alert("Call 2" + Selection_Name);
            //
            var ClientID = "V1:" + sessionStorage.getItem("UserName") + ":" + sessionStorage.getItem("PseudoCityCode") + ":" + sessionStorage.getItem("Domain");
            //alert(ClientID);
            //alert(ClientID);
            //alert(sessionStorage.getItem("Password"));
            var Reqst_Resource = {
                origin: Origin,
                destination: Destination,
                departuredate: Departure_Date,
                returndate: Return_Date,
                SelectionName: Selection_Name,
                Airline: ($('#ddlAirline').val() == "All" ? null : $('#ddlAirline').val()),
                AirClass: $('#AirClass').val(),
                noOfAdults: $('#noOfAdults').val(),
                noOfChildrens: $('#noOfChildrens').val(),
                noOfInfants: $('#noOfInfants').val(),
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
                PCC: sessionStorage.getItem("PseudoCityCode"),
                ClientID: ClientID,
                ClientSecret: sessionStorage.getItem("Password"),
                CompanyTypeId: sessionStorage.getItem("CompanyTypeId"),
                IsAuthenticated: localStorage.getItem("is_loggedin"),
                IsDirectFlight: $("#directFlight").prop("checked"),
                BFMXRQ_RequestContent: RequestContentData
            };
            //alert(Reqst_Resource);
            //console.log("--------------Requested Data-----------------");
            //console.log(Reqst_Resource.BFMXRQ_RequestContent);
            //console.log("--------------Requested Data-----------------");
            //alert("Make Rquest ClientID");
            MasterAppConfigurationsServices("POST", CommonConfiguration.WebAPIServicesURL + "API/BargainFinderMaxSearch/PostBFMXSearch", JSON.stringify(Reqst_Resource), ResultCallBackSuccess, ResultCallBackError);
        }
    } catch (e) {
        var error = e;
        error = error;
        //
        HideWaitProgress();
    }
}
function convertDate(inputFormat) {
    function pad(s) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat);
    var x = [pad(d.getFullYear()), pad(d.getMonth() + 1), d.getDate()].join('-');
    return x;
}

function ChangeDateFormat(inputdate) {
    var MyDate = new Date(inputdate);
    var MyDateString;



    MyDateString = MyDate.getFullYear() + '-'
             + ('0' + (MyDate.getMonth() + 1)).slice(-2) + '-'
             + ('0' + MyDate.getDate()).slice(-2);
    return MyDateString;
}
$(document).ready(function () {

    //alert('Search Script Document Ready');

    $('#date-range0').dateRangePicker(
        { autoClose: true, startDate: new Date(), stickyMonths: true })
            .bind('datepicker-first-date-selected', function (event, obj) {
                $(".normal-top").html("Select Return Date ");
                var date = ChangeDateFormat(obj.date1);
                $("#txtOriginDate").val(date);
            }).bind('datepicker-change', function (event, obj) {
                var date = ChangeDateFormat(obj.date2);
                $("#txtDestinationDate").val(date);

            }).bind('datepicker-opened', function () {
                $(".default-top").html("Select Departure Date ")
                $(".normal-top").html("Select Departure Date ")
            });

    $('#date-range1').dateRangePicker(
    { autoClose: true, singleDate: true, startDate: new Date() }).bind('datepicker-change', function (event, obj) {
        var date = ChangeDateFormat(obj.date1);
        $("#txtOriginDate").val(date);

    }).bind('datepicker-opened', function () {
        $(".default-top").html("Select Departure Date ")
        $(".normal-top").html("Select Departure Date ")
    });

    $('#txtreturnDate').attr('readonly', true);
    AppActionData = 0;

    SearchCondition("2");
    $("input[name='radioSelectionName']").on("click", function (event) {
        SearchCondition($(this).val());
    });
    $("input[id='txtreturnDate']").on("click", function (event) {
        $('#txtreturnDate').attr('readonly', false);
    });
    var arr = [1, 2, 3, 4];
    $.each(arr, function (index, value) {
        $(".myrow_" + value).hide();
    });
    var remove_button = $(".remove_field"); //Fields wrapper
    var max_fields = 4; //maximum input boxes allowed
    var add_button = $(".add_more_field_button"); //Add button ID
    var x = 1; //initlal text box count

    //$(add_button).click(function (e) { //on add input button click
    //    e.preventDefault();
    //    if (x < max_fields) { //max input box allowed
    //        x++; //text box increment
    //        AppActionData = x;
    //        $(".myrow_" + x).show();
    //    }
    //});
    $(add_button).click(function (e) { //on add input button click
        debugger;
        e.preventDefault();
        if ($("#txtMLOrigin_" + x).val() == "" || $("#txtMLDestination_" + x).val() == "" || $("#txtMLDate_" + x).val() == "") {
            ConfirmBootBox("Message", "Please Enter City " + x +" Info", 'App_Warning', initialCallbackYes, initialCallbackNo);

        }
        else {
            if (x < max_fields) { //max input box allowed
                x++; //text box increment
                AppActionData++;
                $(".myrow_" + x).show();

                var minDate = new Date($("#txtMLDate_" + parseInt(x - 1)).val());
                var maxDate = new Date($("#txtMLDate_" + parseInt(x + 1)).val());

                $("#txtMLDate_" + x).datepicker("option", "minDate", new Date(minDate));
                $("#txtMLDate_" + x).datepicker("option", "maxDate", new Date(maxDate));

                //$("#txtMLDate_" + x).dateRangePicker({
                //    minDate: new Date(),
                //    maxDate: new Date()
                //});
            }
        }
    });
    $(remove_button).click(function (e) { //on add input button click
        e.preventDefault();
        $(this).parent('div').parent('div').parent('div').parent('div').parent('div').hide(); x--; AppActionData--;
    })
    //alert('Search Script Document Ready 1');
    //ShowWaitProgress();
    //OnLoadGetQueryStringData();

});
/******************************End Search Result*************************/
function OnLoadGetDataList() {

    try {
        var RequestContentData = '';
        var airLineCode = ($("#ddlAirline").val() == "All" ? null : $('#ddlAirline').val());
        var cabinCode = $("#AirClass").val();
        var directFlight = $("#directFlight").is(":checked");
        //alert(directFlight);
        var TravelPreferences = { "VendorPref": [{ "Code": airLineCode, "PreferLevel": "Preferred" }], "CabinPref": [{ "Cabin": cabinCode, "PreferLevel": "Only" }] };
        //alert(airLineCode);
        if ((airLineCode == 'All') || (airLineCode == null)) {
            TravelPreferences = { "CabinPref": [{ "Cabin": cabinCode, "PreferLevel": "Only" }] };
        }

        var _adult = parseInt(myPassengerDetailsListArray[0].Adults);
        var _children = parseInt(myPassengerDetailsListArray[0].Childrens);
        var _infant = parseInt(myPassengerDetailsListArray[0].Infants);
        var travelAvail;
        if (_adult > 0 && _children > 0 && _infant > 0) {

            travelAvail = [
                                        {
                                            "PassengerTypeQuantity": [
                                            {
                                                "Code": "ADT",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            },
                                            {
                                                "Code": "C07",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Childrens),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            },
                                            {
                                                "Code": "INF",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Infants),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                            ]
                                        }
            ];
        }
        else if (_adult > 0 && _children > 0 && _infant == 0) {

            travelAvail = [
                                        {
                                            "PassengerTypeQuantity": [
                                            {
                                                "Code": "ADT",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            },
                                            {
                                                "Code": "C07",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Childrens),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                            ]
                                        }
            ];
        }
        else if (_adult > 0 && _infant > 0 && _children == 0) {

            travelAvail = [
                                        {
                                            "PassengerTypeQuantity": [
                                            {
                                                "Code": "ADT",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            },
                                            {
                                                "Code": "INF",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Infants),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                            ]
                                        }
            ];
        } else if (_adult > 0 && _infant == 0 && _children == 0) {

            travelAvail = [
                                        {
                                            "PassengerTypeQuantity": [
                                            {
                                                "Code": "ADT",
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                            ]
                                        }
            ];
        }


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
                            "AirTravelerAvail": travelAvail,
                            "PriceRequestInformation": {
                                "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                            }
                        },
                        //"TravelPreferences": {
                        //    "VendorPref": [{
                        //        "Code": airLineCode,
                        //        "PreferLevel": "Preferred"
                        //    }],
                        //    "CabinPref": [{
                        //        "Cabin": cabinCode,
                        //        "PreferLevel": "Only"
                        //    }]
                        //}
                        "TravelPreferences": TravelPreferences
                        //"TravelPreferences": {
                        //    "TPA_Extensions": {}
                        //}
,
                        "DirectFlightsOnly": directFlight,
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
                var origin_date = $('#txtOriginDate').val() + "T" + GetCurrentTime();
                var destination_date = $('#txtDestinationDate').val() + "T" + GetCurrentTime();
                RequestContentData = JSON.stringify({
                    "OTA_AirLowFareSearchRQ": {
                        "OriginDestinationInformation": [
                           {
                               "RPH": "1",
                               "DepartureDateTime": origin_date,
                               "OriginLocation": {
                                   "LocationCode": orginCOde
                               },
                               "DestinationLocation": {
                                   "LocationCode": detinationCode
                               }
                           }, {
                               "RPH": "2",
                               "DepartureDateTime": destination_date,
                               "OriginLocation": {
                                   "LocationCode": detinationCode
                               },
                               "DestinationLocation": {
                                   "LocationCode": orginCOde
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
                            "AirTravelerAvail": travelAvail,
                            "PriceRequestInformation": {
                                "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                            }
                        },
                        "TravelPreferences": TravelPreferences,
                        "DirectFlightsOnly": directFlight,
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
                    case 0:
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
                                                "Quantity": 1,
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                    }
                                },
                                "TravelPreferences": TravelPreferences,
                                "DirectFlightsOnly": directFlight,
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                    }
                                },
                                "TravelPreferences": TravelPreferences,
                                "DirectFlightsOnly": directFlight,
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                    }
                                },
                                "TravelPreferences": TravelPreferences,
                                "DirectFlightsOnly": directFlight,
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                    }
                                },
                                "TravelPreferences": TravelPreferences,
                                "DirectFlightsOnly": directFlight,
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
                                                "Quantity": parseInt(myPassengerDetailsListArray[0].Adults),
                                                "TPA_Extensions": {
                                                    "VoluntaryChanges": {
                                                        "Match": "Info"

                                                    }
                                                }
                                            }
                                          ]
                                      }
                                    ],
                                    "PriceRequestInformation": {
                                        "CurrencyCode": sessionStorage.getItem("CurrencyCode")
                                    }
                                },
                                "TravelPreferences": TravelPreferences,
                                "DirectFlightsOnly": directFlight,
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

                //console.log("Call2");
                localStorage.setItem("RequestID", e.Data1);
                //alert(e.Data1);

                //console.log("---------------------------");

                //console.log(App_Data);

                //console.log("---------------------------");
                //console.log(App_Data.Result);
                //console.log("---------------------------");
                var RequestID_CurrentTimeStamp = new Date();
                localStorage.setItem("RequestID_CurrentTimeStamp", RequestID_CurrentTimeStamp);

                FilteringBindDisplayResutData(App_Data.Result);
            }
        }
        function ResultCallBackError(e, xhr, opts) {

            HideWaitProgress();
            //
            if (e.responseText == "Authentication Required") {
                sessionStorage.setItem("IsFromPaymentPage", "False");
                //alert(sessionStorage.getItem("IsFromPaymentPage") );
                var urlpart = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=/";
                var urlpart2 = $.urlParam('Search');
                var url = urlpart + urlpart2;

                window.location.href = CommonConfiguration.AuthUrl + "/Account/Login?returnUrl=" + CommonConfiguration.AirProjectURL + $.urlParam('Search');
                return false;
            }
            else {
                ShowWaitProgress();
            }
            ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
        }


        //

        var Selection_Name = $("input[name='radioSelectionName']:checked").val();
        var Origin = "";
        var Destination = "";
        var Return_Date = "";
        var Departure_Date = "";

        //alert(Selection_Name);
        if (Selection_Name == "3") {

            //If Selected Trip Type is MULTICITY
            $("input[name='txtmulticityOrigin[]']").each(function () {
                var val = $(this).val();
                if (val != 'undefined' && val != null && val != "") {
                    Origin += $(this).val() + ";";
                }
            });
            $("input[name='txtmulticityDestination[]']").each(function () {
                var val = $(this).val();
                if (val != 'undefined' && val != null && val != "") {
                    Destination += $(this).val() + ";";
                }
            });
            $("input[name='date-range-picker[]']").each(function () {
                var val = $(this).val();
                if (val != 'undefined' && val != null && val != "") {
                    Departure_Date += $(this).val() + ";";
                }
            });
        }
        else if ((Selection_Name == "2") || (Selection_Name == "1")) {
            Origin = $('#txtOrigin').val();
            Destination = $('#txtDestination').val();
            Departure_Date = $('#txtOriginDate').val();
            Return_Date = $('#txtDestinationDate').val();

        }

        //alert("Call 2" + Selection_Name);
        var ClientID = "V1:" + sessionStorage.getItem("UserName") + ":" + sessionStorage.getItem("PseudoCityCode") + ":" + sessionStorage.getItem("Domain");
        //   alert(ClientID);
        //alert(sessionStorage.getItem("Password"));
        var Reqst_Resource = {
            origin: Origin,
            destination: Destination,
            departuredate: Departure_Date,
            returndate: Return_Date,
            SelectionName: Selection_Name,
            Airline: ($('#ddlAirline').val() == "All" ? "" : $('#ddlAirline').val()),
            AirClass: $('#AirClass').val(),
            noOfAdults: $('#noOfAdults').val(),
            noOfChildrens: $('#noOfChildrens').val(),
            noOfInfants: $('#noOfInfants').val(),
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
            PCC: sessionStorage.getItem("PseudoCityCode"),
            ClientID: ClientID,
            ClientSecret: sessionStorage.getItem("Password"),
            CompanyTypeId: sessionStorage.getItem("CompanyTypeId"),
            IsAuthenticated: localStorage.getItem("is_loggedin"),
            IsDirectFlight: $("#directFlight").prop("checked"),
            BFMXRQ_RequestContent: RequestContentData
        };

        //console.log("--------------Requested Data-----------------");
        //console.log(Reqst_Resource.BFMXRQ_RequestContent);
        //console.log("--------------Requested Data-----------------");
        var obj = delete JSON.parse(RequestContentData).OTA_AirLowFareSearchRQ.TravelPreferences["VendorPref"];



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
            Class: $('.AirClass').val() != null ? $('.AirClass').val().trim() : "",
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
        debugger;
        ShowWaitProgress();
        if (ValidateData()) {
            //alert('validated');
            OnLoadGetPassengerDetailsList();
            if (SolutionDataTraveler("GET", "AirLineList") == null) {
                //alert("null");
                $.when(GetAirLineList()).done(function (a1) {
                    // the code here will be executed when all four ajax requests resolve.
                    // a1, a2, a3 and a4 are lists of length 3 containing the response text,
                    // status, and jqXHR object for each of the four ajax calls respectively.
                    //alert('Passeger list fetched');
                    AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
                    AirLineList = SolutionDataTraveler("GET", "AirLineList");
                    //debugger;
                    //alert('Loaddata');
                    OnLoadGetDataList();
                });
            }
            else {
                //alert("not");
                OnLoadGetDataList();
            }

        }
        else {
            //
            //
            HideWaitProgress();
        }

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
        //GetFilterAirlinesModuleResut();
        var MinNewPriceRange = $("#Price-range-slider").slider('value').split(";")[0];
        var MaxNewPriceRange = $("#Price-range-slider").slider('value').split(";")[1];
        var MinDepartureSelection = $("#Departure-range-slider").slider('value').split(";")[0];
        var MaxDepartureSelection = $("#Departure-range-slider").slider('value').split(";")[1];
        var MaxArrivalSelection = $("#Arrive-range-slider").slider('value').split(";")[1];
        var MinArrivalSelection = $("#Arrive-range-slider").slider('value').split(";")[0];

        FilterSearch_AllFilterCombined(MinNewPriceRange, MaxNewPriceRange, MinDepartureSelection, MaxDepartureSelection, MinArrivalSelection, MaxArrivalSelection);
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
        //alert(AppActionData);
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
                //if ($('#txtMLOrigin_5').val().trim() == "") {
                //    ConfirmBootBox("Message", "Please Enter Origin", 'App_Warning', initialCallbackYes, initialCallbackNo);
                //    $("#txtMLOrigin_5").focus();
                //     HideWaitProgress();
                //    return false;
                //}
                //if ($('#txtMLDestination_5').val().trim() == "") {
                //    ConfirmBootBox("Message", "Please Enter Destination", 'App_Warning', initialCallbackYes, initialCallbackNo);
                //    $("#txtMLDestination_5").focus();
                //     HideWaitProgress();
                //    return false;
                //}
                //if ($('#txtMLDate_5').val().trim() == "") {
                //    ConfirmBootBox("Message", "Please Enter Departure Date", 'App_Warning', initialCallbackYes, initialCallbackNo);
                //    $("#txtMLDate_5").focus();
                //     HideWaitProgress();
                //    return false;
                //}
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
                    $("#date-range0").css("display", "none");
                    $("#date-range1").css("display", "block");
                    $("#lblroundTrip").html("Departure Date");

                    $(".myrow_1").hide();
                    SearchSelectionType = 1;
                    // $('#date-range0').dateRangePicker().destroy();
                    //$('#date-range0').dateRangePicker({ autoClose: true,singleDate : true, });
                    for (var i = 0; i < AppActionData; i++) {
                        $(".remove_field").click();
                    }
                    break;
                case "2":
                    $("#date-range0").css("display", "block");
                    $("#date-range1").css("display", "none");
                    $('#txtreturnDate').attr('readonly', true);
                    $("#multicitySearch").addClass("hidden");
                    $("#btnmulticitySearch").addClass("hidden");
                    $("#OneWayReturnWaySearch").removeClass("hidden");
                    $("#lblroundTrip").html("Departure Date  Return Date");

                    $(".myrow_1").hide();
                    SearchSelectionType = 2;
                    for (var i = 0; i < AppActionData; i++) {
                        $(".remove_field").click();
                    }
                    break;
                case "3":
                    //$('#directflight-section').hide();
                    $('#txtreturnDate').attr('readonly', false);
                    $("#multicitySearch").removeClass("hidden");
                    $("#btnmulticitySearch").removeClass("hidden");
                    $("#OneWayReturnWaySearch").addClass("hidden");
                   // $("#directflight-section").css("display", "block")
                    $(".myrow_1").show();
                    SearchSelectionType = 3;
                    AppActionData = 0;
                    break;
            }
        }

    } catch (e) {
        var error = e;
        error = error;
    }
} 
