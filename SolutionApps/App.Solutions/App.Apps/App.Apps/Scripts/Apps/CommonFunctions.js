function checkRedirectingDomain() {
    debugger;
    var referrer = "";
    if (document.referrer.length > 0) {
        ref = document.referrer.split('/');
        referrer = ref[2];

        if (validDomains.indexOf(referrer) > -1) {
            var domainStored = localStorage.getItem("domain");
            if (referrer != domainStored)
                return true;
        } 
    }
    return false;
}


function LoadLocalStorageValues() {
     
    var loginStatus = getParameterByName('LoginStatus');
    var loginUID = getParameterByName('LoginUID');
    if (loginStatus != null && loginUID != null && (loginStatus == true || loginStatus == "True")) {
        var date = new Date();
        localStorage.setItem("loginUID", loginUID);
        localStorage.setItem("is_loggedin", loginStatus);
    }
    if (window.location.href.indexOf("/Payments") > -1) {

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
    }

    //  debugger;
    var ischanged = checkRedirectingDomain();
    if (sessionStorage.getItem("UserName") == null || sessionStorage.getItem("UserName") == "" || ischanged) {
        var ref = "sunspotsholidays.com";
        var referrer = "";
        if (document.referrer.length > 0) {
            ref = document.referrer;
        } 
        updateHeaderFooter(ref);
    }
    else {
        var headerUrl = sessionStorage.getItem('HeaderUrl');
        var footerUrl = sessionStorage.getItem('FooterUrl');
        var TermsUrl = sessionStorage.getItem('TermsUrl');
        var PrivacyUrl = sessionStorage.getItem('PrivacyUrl');
        //Update Page's Title For Each Page
        $(document).prop('title', sessionStorage.getItem("Name"));

        //Set Favicon
        $("#favicon").attr("href", sessionStorage.getItem("FaviconUrl"));

        if (headerUrl != null && headerUrl != undefined && headerUrl != '') {

            try {

                if (localStorage.getItem("domain") == 'sunspotsholidays.com') {
                    if (headerUrl != null && headerUrl != undefined && headerUrl != '') {
                        $('#siteFooter').html('');
                        $("#frame1").attr("src", headerUrl);
                        //$("#frame1").attr("src", headerUrl);
                        $("#header").load(headerUrl);

                    }
                }
                else {
                    $("#header").load(headerUrl, function () {
                        //debugger;

                        if (GetQueryStringParameterValues('Search') != null) {
                            if (SolutionDataTraveler("GET", "AirLineList") == null) {

                                $.when(GetAirLineList()).done(function (a1) {
                                    // the code here will be executed when all four ajax requests resolve.
                                    // a1, a2, a3 and a4 are lists of length 3 containing the response text,
                                    // status, and jqXHR object for each of the four ajax calls respectively.
                                    //alert('Passeger list fetched');
                                    AirPortNameCodeCoList = SolutionDataTraveler("GET", "AirPortNameCodeCoList");
                                    AirLineList = SolutionDataTraveler("GET", "AirLineList");
                                    //alert('Loaddata');
                                    OnLoadGetQueryStringData();
                                });
                            }
                            else {
                                OnLoadGetQueryStringData();
                            }
                        }
                        if (localStorage.getItem("is_loggedin") == "True") {
                            console.log($('#btn-lg').text('Logout'));
                            console.log($('#btn-lg').attr('data-url', CommonConfiguration.AuthUrl + 'Account/LogOff'));
                            $('#lnk-register').hide();
                            $('#lnk-forgetpass').hide();
                            $('#lnk-mybookings').show();
                            $('#lnk-myprofile').show();

                        }
                        else {
                            console.log($('#btn-lg').text('Login'));
                            console.log($('#btn-lg').attr('data-url', CommonConfiguration.AuthUrl + 'Account/Login?returnurl=' + CommonConfiguration.AirProjectURL));
                            $('#lnk-mybookings').hide();
                            $('#lnk-myprofile').hide();
                            $('#lnk-register').show();
                            $('#lnk-forgetpass').show();
                        }


                        var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");

                        if (CompanyTypeID == "1") {
                            $('#lnk-register').attr("href", "http://qa.nanojot.com/authentication/Account/Register");
                        }
                        else {
                            $('#lnk-register').attr("href", "http://qa.nanojot.com/authentication/Account/MemberAgencyRegister");
                        }
                    });
                }

            } catch (e) {

            }



            //$('#frame1').attr("src", headerUrl);
            //$("#frame1").css("display", "block");
        }
        if (footerUrl != null && footerUrl != undefined && footerUrl != '') {
            //$('#siteFooter').html('');
            //$(".Footer-iframe").load(footerUrl, function () { });
            //$("#frame").attr("src", footerUrl);
            $('#siteFooter').html('');
            $("#frame").attr("src", footerUrl);
        }
        if (PrivacyUrl != null && PrivacyUrl != undefined && PrivacyUrl != '') {
            $('#privacy-url-link').attr("href", PrivacyUrl);
        }
        if (TermsUrl != null && TermsUrl != undefined && TermsUrl != '') {
            $('#terms-url-link').attr("href", TermsUrl);
        }
        if ((localStorage.getItem('is_loggedin') == null || localStorage.getItem('is_loggedin') == "False" || localStorage.getItem('is_loggedin') == "false") && sessionStorage.getItem('CompanyTypeId') == 1) {

            window.location.href = CommonConfiguration.unauthurl;
        }
        else {
            $(".preloader").delay(200).fadeOut();
        }
    }
    //debugger;

    //debugger;
    //Update Currency on Payments Page
    $('#allfares2_currency').html(sessionStorage.getItem("CurrencyCode"));
    $('#allfares_currency').html(sessionStorage.getItem("CurrencyCode"));

    //Replace Agency Name on Payments page
    $('#terms_agencyname').html(sessionStorage.getItem("Name"));

    if (sessionStorage.getItem("CurrencyCode") == "CAD") {
        $('#canada_env').show();
    }
    //canada_env

}

function updateHeaderFooter(domainName) {
    var servicepath = CommonConfiguration.searchBoxService + "/GetAgencyDetailsByDomain";
    $.ajax({
        cache: false,
        url: servicepath,
        data: ({ domains: domainName + "|" + $(location).attr('hostname') }),
        type: "POST",
        cors: true,
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "xml",
        success: function (xml) {

            console.log(xml);

            $(xml).find('CompanyDetail').each(function () {
                var Id = $(this).find('Id').text();
                var headerUrl = $(this).find('HeaderUrl').text();
                var footerUrl = $(this).find('FooterUrl').text();
                var TermsUrl = $(this).find('TermsUrl').text();
                var PrivacyUrl = $(this).find('PrivacyUrl').text();
                var Name = $(this).find('Name').text();
                var CityName = $(this).find('CityName').text();
                var country = $(this).find('country').text();
                var PostalCode = $(this).find('PostalCode').text();
                var state = $(this).find('state').text();
                var StreetAddress = $(this).find('StreetAddress').text();
                var SmtpEmailID = $(this).find('SmtpEmailID').text();
                var PhoneNumber = $(this).find('PhoneNumber').text();
                var UserName = $(this).find('UserName').text();
                var Password = $(this).find('Password').text();
                var QueueNo = $(this).find('QueueNo').text();
                var PseudoCityCode = $(this).find('PseudoCityCode').text();
                var FromEmail = $(this).find('FromEmail').text();
                var ToEmail = $(this).find('ToEmail').text();
                var Domain = $(this).find('Domain').text();
                var CurrencyCode = $(this).find('CurrencyCode').text();
                var CompanyTypeId = $(this).find('CompanyTypeId').text();
                var FaviconUrl = $(this).find('FaviconUrl').text();
                var Air = $(this).find('Air').text();
                var Hotel = $(this).find('Hotel').text();
                var CarRental = $(this).find('CarRental').text();
                var Insurance = $(this).find('Insurance').text();
                var CCEnableAir = $(this).find('CCEnableAir').text();
                var websiteUrl = $(this).find('WebsiteUrl').text();
                debugger;
                sessionStorage.setItem("Id", Id);
                sessionStorage.setItem("HeaderUrl", headerUrl);
                sessionStorage.setItem("FooterUrl", footerUrl);
                sessionStorage.setItem("TermsUrl", TermsUrl);
                sessionStorage.setItem("PrivacyUrl", PrivacyUrl);
                sessionStorage.setItem("Name", Name);
                sessionStorage.setItem("CityName", CityName);
                sessionStorage.setItem("country", country);
                sessionStorage.setItem("PostalCode", PostalCode);
                sessionStorage.setItem("state", state);
                sessionStorage.setItem("StreetAddress", StreetAddress);
                sessionStorage.setItem("SmtpEmailID", SmtpEmailID);
                sessionStorage.setItem("PhoneNumber", PhoneNumber);
                sessionStorage.setItem("UserName", UserName);
                sessionStorage.setItem("Password", Password);
                sessionStorage.setItem("QueueNo", QueueNo);
                sessionStorage.setItem("PseudoCityCode", PseudoCityCode);
                sessionStorage.setItem("FromEmail", FromEmail);
                sessionStorage.setItem("ToEmail", ToEmail);
                sessionStorage.setItem("Domain", Domain);
                sessionStorage.setItem("CurrencyCode", CurrencyCode);
                sessionStorage.setItem("CompanyTypeId", CompanyTypeId);
                sessionStorage.setItem("FaviconUrl", FaviconUrl);
                sessionStorage.setItem("Air", Air);
                sessionStorage.setItem("Hotel", Hotel);
                sessionStorage.setItem("CarRental", CarRental);
                sessionStorage.setItem("Insurance", Insurance);
                sessionStorage.setItem("CCEnableAir", CCEnableAir);
                localStorage.setItem("domain", websiteUrl);




                localStorage.setItem("currentDomain", $(location).attr('hostname'));

                //Update Page's Title For Each Page
                $(document).prop('title', Name);

                //Set Favicon
                $("#favicon").attr("href", FaviconUrl);

                if (headerUrl != null && headerUrl != undefined && headerUrl != '') {

                    try {

                        if (localStorage.getItem("domain") == 'sunspotsholidays.com') {
                            if (headerUrl != null && headerUrl != undefined && headerUrl != '') {
                                //$('#siteFooter').html('');
                                $("#frame1").attr("src", headerUrl);
                                $("#header").load(headerUrl);

                            }
                        }
                        else {
                            $("#header").load(headerUrl, function () {
                                if (GetQueryStringParameterValues('Search') != null) {
                                    GetAirLineList();
                                    OnLoadGetQueryStringData();
                                }
                                if (localStorage.getItem("is_loggedin") == "True") {
                                    console.log($('#btn-lg').text('Logout'));
                                    console.log($('#btn-lg').attr('data-url', CommonConfiguration.AuthUrl + 'Account/LogOff'));
                                    $('#lnk-register').hide();
                                    $('#lnk-forgetpass').hide();
                                    $('#lnk-mybookings').show();
                                    $('#lnk-myprofile').show();

                                }
                                else {
                                    console.log($('#btn-lg').text('Login'));
                                    console.log($('#btn-lg').attr('data-url', CommonConfiguration.AuthUrl + 'Account/LogOff'));
                                    $('#lnk-mybookings').hide();
                                    $('#lnk-myprofile').hide();
                                    $('#lnk-register').show();
                                    $('#lnk-forgetpass').show();
                                }


                                var CompanyTypeID = sessionStorage.getItem("CompanyTypeId");

                                if (CompanyTypeID == "1") {
                                    $('#lnk-register').attr("href", "http://qa.nanojot.com/authentication/Account/Register");
                                }
                                else {
                                    $('#lnk-register').attr("href", "http://qa.nanojot.com/authentication/Account/MemberAgencyRegister");
                                }
                            });
                        }
                    } catch (e) {

                    }
                }
                if (footerUrl != null && footerUrl != undefined && footerUrl != '') {
                    $('#siteFooter').html('');
                    $("#frame").attr("src", footerUrl);
                }
                if (PrivacyUrl != null && PrivacyUrl != undefined && PrivacyUrl != '') {
                    $('#privacy-url-link').attr("href", PrivacyUrl);
                }
                if (TermsUrl != null && TermsUrl != undefined && TermsUrl != '') {
                    $('#terms-url-link').attr("href", TermsUrl);
                }

                if ((localStorage.getItem('is_loggedin') == null || localStorage.getItem('is_loggedin') == "False" || localStorage.getItem('is_loggedin') == "false") && sessionStorage.getItem('CompanyTypeId') == 1) {
                    window.location.href = CommonConfiguration.unauthurl;
                }
                

                $(".preloader").delay(200).fadeOut();
            });
        },
        error: function (response) {
            // alert(response.responseText);
            $(".preloader").delay(200).fadeOut();
        },
        failure: function (response) {
            //  alert(response.responseText);
            $(".preloader").delay(200).fadeOut();
        }
    })
}

function DisableRightClick() {
    if (document.layers)
    { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
    else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
    document.oncontextmenu = new Function("return false");
    document.onselectstart = new Function("return false");
    if (window.sidebar) {
        document.onmousedown = disableselect
        document.onclick = reEnable
    }
}

////////////////////DisableRightClick//////////////////////////

function GetNewGuid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toUpperCase();
}

// Application Common Pagnation
var QPRO_App_LastScrollTop = 0;
var ScrollingPaginationCounts = 1;
$(window).scroll(function (event) {
    var QPRO_App_Status = $(this).scrollTop();
    if ($(this).scrollTop() == $(document).height() - $(this).height()) {
        // ajax call get data from server and append to the div
        GetOnScrollLoadAppData();
    }
    if ($(this).scrollTop() + $(this).height() > $(document).height() - 100) {
        //$(window).unbind('scroll');
        //GetOnScrollLoadAppData();
        //$(window).bind('scroll');
    }
    if ($(this).scrollTop() == $(document).height() - $(this).height()) {
        //$(window).unbind('scroll');
        //GetOnScrollLoadAppData();
        //$(window).bind('scroll');
    }
    if (QPRO_App_Status > QPRO_App_LastScrollTop) {
        //alert("DownScroll");
        //GetOnScrollLoadAppData();
    } else {
    }
    QPRO_App_LastScrollTop = QPRO_App_Status;
});

function GetOnScrollLoadAppData(initialODPaginationCallBackYes) {
    if (PaginationLoadTableData) {
        PaginationLoadTableData()
        //ScrollingPaginationCounts++;
    }
    return false;
}
function initialPaginationCallBackYes() {
}
function PaginationLoadTableData() {
    //$.ajax({
    //    url: "<?php bloginfo('wpurl') ?>/wp-admin/admin-ajax.php",
    //    type: 'POST',
    //    data: "action=infinite_scroll&page_no=" + pageNumber + '&loop_file=loop',
    //    success: function (html) {
    //        $("#content").append(html);   // This will be the div where our content will be loaded
    //    }
    //});
    //alert('Page Number : ' + ScrollingPaginationCounts);
    return false;
}

// DataTable Controls Windows Resize Helper.
//$(document).ready(function () {
//    function WindowsResize() {
//        if ($.fn.DataTable.tables != null)
//            $($.fn.dataTable.tables(true)).DataTable().columns.adjust().responsive.recalc();
//    }
//    $(window).resize(function () {
//        WindowsResize();
//    });
//});
// Help Text Tooltip for Controls
$(document).ready(function () {
    //var inputs = document.getElementsByClassName("helpText");
    var inputs = $(".helpText");
    for (var i = 0; i < inputs.length; i++) {
        // test to see if the hint span exists first
        if (inputs[i].parentNode.getElementsByTagName("div")[0]) {
            // the span exists!  on focus, show the hint
            inputs[i].onfocus = function () {
                this.parentNode.getElementsByTagName("div")[0].style.display = "inline";
            }
            // when the cursor moves away from the field, hide the hint
            inputs[i].onblur = function () {
                this.parentNode.getElementsByTagName("div")[0].style.display = "none";
            }
        }
    }
    // repeat the same tests as above for selects
    //var selects = document.getElementsByTagName("select");
    var selects = $("select");
    for (var k = 0; k < selects.length; k++) {
        if (selects[k].parentNode.getElementsByTagName("div")[0]) {
            selects[k].onfocus = function () {
                this.parentNode.getElementsByTagName("div")[0].style.display = "inline";
            }
            selects[k].onblur = function () {
                this.parentNode.getElementsByTagName("div")[0].style.display = "none";
            }
        }
    }
});


function userDetails(userID, tagID) {
    try {
        if (tagID != "0") {
            var prmdata = "";
            function ResultCallBackSuccess(e, xhr, opts) {
                var TData = JSON.parse(e);
                if (TData.length > 0) {
                    jQuery("#" + tagID).attr("data-content", "<div style='word-break: break-all;'><b>User Name:</b>" + TData[0].FIRST_NAME + " </div><div style='word-break: break-all;'><b>Ph No:</b>" + TData[0].MOBILE + "</div><div style='word-break: break-all;'><b>Email:</b>" + TData[0].EMAIL + "</div>");
                }
                else {
                    ConfirmBootBox("Header Message", $("#error").val(), 'App_Error', initialCallbackYes, initialCallbackNo)
                }
                $('#' + tagID).popover('show');
                jQuery("#" + tagID).attr("id", "0");
                //$("#0").trigger("click");
                HideWaitProgress();
            }
            function ResultCallBackError(e, xhr, opts) {
                ConfirmBootBox("Header Message", $("#error").val(), 'App_Error', initialCallbackYes, initialCallbackNo);
                HideWaitProgress();
            }
            MasterAppConfigurationsServices("GET", sConfigServicePath + "api/userdetail/" + userID, prmdata, ResultCallBackSuccess, ResultCallBackError);
        }
    }
    catch (e) { HideWaitProgress(); }
}


///function to make bask button dynamic////
function BackToPrevious() {
    window.location = QPRODataTraveler('GET', 'BACK_URL');
}

$(document).ready(function () {
    var abn = $(".ul.mav > li").find('a');
    $('ul.mav > li').find('a').each(function () {
        //alert(this.href);
        if ((this.href).toLowerCase() == (window.location.href).toLowerCase()) {
            if (($(this).parent().parent().parent('li')).length != 0) {
                $(this).parent('li').addClass('active')
                $(this).parent().parent().parent('li').addClass('active open');
                return false;
            }
            $(this).parent('li').addClass('active');
            return false;
        }
    });
});


function GetQueryStringParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

/////////////////////////////////////////Master User Login Validation//////////////////////////////////////////////////////////////////////////////////////////////


function ValidateLoginUser(QPROID) {
    var ErrorMessage = "The user name or password not provided.";
    if ($('#txtEmailID').val() == '' && $('#txtPassword').val() == '') {
        ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
        return false;
    }
    else if ($('#txtEmailID').val() == '') {
        ErrorMessage = "The User Name not provided.";
        ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
        return false;
    }
    else if ($('#txtPassword').val() == '') {
        ErrorMessage = "The Password not provided.";
        ConfirmBootBox("Login Error", ErrorMessage, 'App_Error', initialCallbackYes, initialCallbackNo);
        return false;
    }
    return true;
}
/////////////////////////////////////////Master User Login Validation//////////////////////////////////////////////////////////////////////////////////////////////



/////////////////////////////////////////Master Task Bar Legend//////////////////////////////////////////////////////////////////////////////////////////////
function MasterTaskbarLegendPanelOff() {
    StaticTaskbarLegendPanelOn.setAttribute('class', 'visible');
    StaticTaskbarLegendPanelOff.setAttribute('class', 'hidden');
    jQuery('#StaticTaskbarLegendPanelOn').css({
        'display': 'block',
        'width': '100%'
    });
}
function MasterTaskbarLegendPanelOn() {
    jQuery('#StaticTaskbarLegendPanelOff').css({
        'display': 'block',
        'width': '15px'
    });
    StaticTaskbarLegendPanelOn.setAttribute('class', 'hidden');
    StaticTaskbarLegendPanelOff.setAttribute('class', 'visible');
}
function MasterTaskbarLegendPanelClose() { jQuery(".StaticTaskbarLegendPanel-Container").hide(); }
/////////////////////////////////////////Master Task Bar Legend//////////////////////////////////////////////////////////////////////////////////////////////



/////////////////////////////////////////Master Date Time Formating//////////////////////////////////////////////////////////////////////////////////////////////

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    //return (dt.getMonth() + 1) + "-" + dt.getDate() + "-" + dt.getFullYear();
    return dt.getDate() + "-" + (dt.getMonth() + 1) + "-" + dt.getFullYear();
}
/////////////////////////////////////////Master Date Time Formating//////////////////////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////Master Modal Container//////////////////////////////////////////////////////////////////////////////////////////////
function SolutionModalContainerEvent(ModalContainerPanel, ModalContainerPanelTile, ModalContainer, ModalContainerData, ActionSender) {
    if (ActionSender != '') {
        $(ModalContainerPanelTile).empty().append(ActionSender);
    }
    ModalContainerData = ModalContainerData.height($(ModalContainerPanel).height() - 35).width($(ModalContainerPanel).width())
    $(ModalContainer).empty().append(ModalContainerData);
    $(ModalContainerPanel).toggle();
    $(ModalContainerPanel).show();
    $(ModalContainerPanel).draggable({ containment: "#AppMainPage", handle: 'h3', cursor: "move" });
    $(ModalContainerPanel).resizable();
}




function LocationDetailsContainerModal(btnName, ActionSender) {
    if (ActionSender != '') {
        $("#ModelContentDetailsContainerTitle").empty().append(ActionSender);
    }
    //$("#ModelContentDetailsContainer").toggle();
    $("#ModelContentDetailsContainer").show();
    //$("#ModelContentDetailsContainer").hide();
    $('#ModelContentDetailsContainer').draggable({ containment: "#AppMainPage", handle: 'h3', cursor: "move" });
    $("#ModelContentDetailsContainer").resizable();
}
function SolutionModalContainerForImg(ModalContainerData, ActionSender) {
    if (ActionSender != '') {
        $("#ModelContentDetailsContainerTitle").empty().append(ActionSender);
    }
    ModalContainerData = ModalContainerData.height($("#ModelContentDetailsContainer").height() - 35).width($("#ModelContentDetailsContainer").width())
    $("#ModelContentData").empty().append(ModalContainerData);
    $("#ModelContentDetailsContainer").toggle();
    $("#ModelContentDetailsContainer").show();
    $('#ModelContentDetailsContainer').draggable({ containment: "#AppMainPage", handle: 'h3', cursor: "move" });
    $("#ModelContentDetailsContainer").resizable();
}


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)", "i"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(document).ready(function () {
    $("#ModelContentDetailsContainer").resize(function (event) {
        var $elem = $(this);
        $elem = $elem.find('#ModelContentData').find('img');
        if ($elem.length > 0) {
            SolutionModalContainerForImg($elem, '');
        }
    });

    $(".closeModelContentDetailsContainer").on('click', function () {
        $("#ModelContentDetailsContainer").hide();
    });
    $(".closeAdminAddModelContentDetailsContainer").on('click', function () {
        $("#AdminAddModelContentDetailsContainer").hide();
    });
    $(".closeAdminEditModelContentDetailsContainer").on('click', function () {
        $("#AdminEditModelContentDetailsContainer").hide();
    });

});
/////////////////////////////////////////Master Modal Container//////////////////////////////////////////////////////////////////////////////////////////////




$(document).ready(function () {
    $('.MasterLeftResponsiveMenus').on('click', function (e) {
        e.preventDefault();
        if (!$("#sidebar").hasClass("menu-min")) {
            $("#sidebar").addClass("menu-min");
        }
        else { $("#sidebar").removeClass("menu-min"); }
    });
    var Status = GetQueryStringParameterValues('GISStatus') ? 1 : 0;
    if (Status == 1) {
        $('.tabbable').tabs({ active: 3 });
    }
    $(function () {
        $(".tabbable").tabs({
            beforeActivate: function (event, ui) {
                if (ui.newPanel.selector == "#map_tab") {
                    if (Status == 0) {
                        location.href = location.href + '?GISStatus=1';
                    }
                    //if (SolutionDataTraveler("GET", "SolutionGISModuleEnable") == 0) {
                    //    alert("Hi  map_tab Please load MAP");
                    //    SolutionDataTraveler("SET", "SolutionGISModuleEnable", 1);
                    //    //location.reload(true);
                    //    location.href = location.href + '?GISStatus=1';
                    //}
                }
            }
        });
    });


    //$(function () {
    //    $(".tabbableInspectionItems").tabs({
    //        beforeActivate: function (event, ui) {
    //            alert(ui.olPanel.selector);
    //            alert(ui.newPanel.selector);
    //        }
    //    });
    //});


});



$(document).ready(function () {
    $("#menu-toggler").click(function (event) {
        MenusActions(event);
    });
    function MenusActions(ActionEvent) {
        try {
            //if ($('.responsiveMenus').css('display') == 'none') {
            //    alert('Hi IF');
            //    $('.responsiveMenus').css({ 'display': 'block' });
            //} else {
            //    alert('Hi Else');
            //    $('.responsiveMenus').css({
            //        'display': 'none'
            //    });
            //}
        }
        catch (e) {
        }
    }
});

function CommonslimScroll(ControllName, ControllHeight) {
    //$('.CommonslimScroll').slimScroll({
    $(ControllName).slimScroll({
        height: ControllHeight,
        alwaysVisible: true,
        railVisible: true,
        position: 'right'
    });
}

function nullEmptyToHyphen(str) {
    if (str == null || str == "") {
        str = "-";
    }
    return str;
}

/////////////////////////////////////////Master DataTable Property Legend//////////////////////////////////////////////////////////////////////////////////////////////
function TablePagination(DataTableData) {
    if (DataTableData != null) {
        return (DataTableData.length > SolutionConfigTraveler.TablePageSize()) ? true : false;
    }
    return true;
}

/////////////////////////////////////////Master DataTable Property Legend//////////////////////////////////////////////////////////////////////////////////////////////


$(function () {

    function TabMenuActionManagement(index, TabAction_Result_Container, DataTable_Result_Contant) {
        switch (index) {
            case 0:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#ChartDetails').html());
                break;
            case 1:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#txtDetails').html());
                break;
            case 2:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#ChartDetails').html());
                break;
            case 3:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#txtDetails').html());
                break;
            case 4:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#ChartDetails').html());
                break;
            case 5:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#txtDetails').html());
                break;
            case 6:
                MasterRespansiveTabActionManagement(index, $('#TabAction-Result'), $('#ChartDetails').html());
                break;
        }
    }

    $("div.bhoechie-tab-menu>div.list-group>a").click(function (e) {
        e.preventDefault();
        $(this).siblings('a.active').removeClass("active");
        $(this).addClass("active");
        var index = $(this).index();
        //index = 0;
        $("div.bhoechie-tab>div.bhoechie-tab-content").removeClass("active");
        $("div.bhoechie-tab>div.bhoechie-tab-content").eq(0).addClass("active");
        //$("div.bhoechie-tab>div.bhoechie-tab-content").eq(index).addClass("active");
        TabMenuActionManagement(index);
    });

    CommonslimScroll('.bhoechie-tab-scroll', '370px');

});

