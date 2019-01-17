/*******************************************************
********************************************************
***Function      : Master App Configration *************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 17/09/2015*** ***********************
********************************************************
********************************************************/


var SecurityTokenGHeaders = {
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT,DELETE',
    'Content-Type': 'application/json',
    'Accept': 'application/json'
};

/*******************************************************
********************************************************
***Function      : Master App Configration *************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 17/09/2015*** ***********************
********************************************************
********************************************************/
//////////////////User Profiles//////////////////////////////////////////////////////////
var sConfigServicePath, sURLSolutionApp, sProjectName, sSolutionAppCulture;
//////////////////User Profiles//////////////////////////////////////////////////////////

//////////////////Q-PRO APP Configuration//////////////////////////////////////////////////////////
var QPROGIS_GISMapService, QPROGIS_DynamicMapService, QPROGIS_FeatureMapService, QPROGIS_ROMapService, QPROGIS_GeometryService, QPROGIS_PrintService;
var sGISAppConfigServiceUrl, sGISAppRO_Code;
//////////////////Q-PRO APP Configuration//////////////////////////////////////////////////////////

//////////////////App DataTraveler Configuration//////////////////////////////////////////////////////////
var QPRO_DataTraveler_ProjectID, QPRO_DataTraveler_ROCode;
//////////////////App DataTraveler Configuration//////////////////////////////////////////////////////////


//////////////////QPROGIS Configuration//////////////////////////////////////////////////////////
function QPRO_Application_URL() {
    var qproApplicationurl;
    if (window.location.port == '' && window.location.hostname != "localhost")
    { qproApplicationurl = sURLSolutionApp; }
    else { qproApplicationurl = ""; }
    return qproApplicationurl;
}
//////////////////QPROGIS Configuration//////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
    //$.holdReady(true);
    ////initializeMasterAppConfigration();
    //$.holdReady(false);
});
function initializeMasterAppConfigration() {
    SolutionUserProfiler();
    SolutionAppProfiler();
}
function SolutionUserProfiler() {
    var SolutionGIS = {
        ActionType: "SolutionUserProfileData"
    };
    MasterAppConfigurationsServices("GET", sURLSolutionApp + "DashBoard/UserProfiler", SolutionGIS, ResultCallBackSuccess, ResultCallBackError);
    function ResultCallBackSuccess(e, xhr, opts) {
        SETSolutionUserProfilerData(e);
    }
    function ResultCallBackError(e, xhr, opts) {
        location.href = sURLSolutionApp;
    }
}

function SETSolutionUserProfilerData(response_data) {
    if (response_data != null) {
        response_data = JSON.parse(response_data);
        if (!response_data.IsAuthenticatedUser && !response_data.IsSucess) {
            location.href = sURLSolutionApp;
        }
        else if (response_data != null) {
            SolutionDataTraveler("SET", "SolutionUserProfileTraveler", response_data);
            var sSolutionUserProfiler = new Array();
            $.each(response_data, function (r_key, r_value) {
                sSolutionUserProfiler.push({ key: r_key, value: r_value });
            });
            if (sSolutionUserProfiler.length > 0) {
                $.localStorage('Solution_User_Profile_Traveler', sSolutionUserProfiler)
                $.sessionStorage('Solution_User_Profile_Traveler', sSolutionUserProfiler)
            }
        }
        else {
            location.href = sURLSolutionApp;
        }
    }
    else {
        location.href = sURLSolutionApp;
    }
}

function SolutionAppProfiler() {
    var SolutionGIS = {
        ActionType: "SolutionUserProfileData"
    };
    MasterAppConfigurationsServices("GET", sConfigServicePath + "api/GISModule", SolutionGIS, ResultCallBackSuccess, ResultCallBackError);
    function ResultCallBackSuccess(e, xhr, opts) {
        SETSolutionAppProfilerData(e.Data);
    }
    function ResultCallBackError(e, xhr, opts) {
        location.href = sURLSolutionApp;
    }
}

function SETSolutionAppProfilerData(response_data) {
    if (response_data != null) {
        if (response_data != null) {
            SolutionDataTraveler("SET", "SolutionAppConfigTraveler", response_data);
        }
        else {
            location.href = sURLSolutionApp;
        }
    }
    else {
        location.href = sURLSolutionApp;
    }
}




/*


SolutionDataTraveler.ROCODE();

SolutionUserProfileTraveler.AgTypeID();
7
SolutionUserProfileTraveler.UserID();
7446
SolutionUserProfileTraveler.UserName();
"Rizwan Malek "
SolutionUserProfileTraveler.GISMapService()
"http://cams/agswaashghal/rest/services/QPRO2/DraftROFeatureService/FeatureServer"
SolutionUserProfileTraveler.PrintService()
"http://cams/agswaashghal/rest/services/Ashghal2/MapServer"


SolutionDataTraveler('SET','ROCODE','343434')
343434
SolutionDataTraveler('GET','ROCODE')
343434
var myArray = new Array();
undefined
myArray.push({name:'McGruff', company:'Police', zip:60652});
1
myArray.push({name:'Jared', company:'Upstatement', zip:63124});
2
SolutionDataTraveler('SET','ROCODE',myArray)
[Object, Object]
SolutionDataTraveler('GET','ROCODE')
[Object, Object]

SolutionDataTraveler('Clear','ROCODE')

SolutionDataTraveler('ClearAll')

*/








//$(document).ready(function () {
//    $.getJSON("data/data.json").done(function (data) {
//        localStorage.setItem("data", JSON.stringify(data));
//        $.each(data.contacts, function (index, value) {
//            $("#contacts ul").append(
//                "<li class='topcoat-list__item'>\
//                    First Name: " + value.first + "<br> Last Name: " + value.last +
//                "</li>"
//            );
//        });
//    }).fail(function () {
//        if (localStorage.length != 0) {
//            var localData = $.parseJSON(localStorage.getItem("data"));
//            $.each(localData.contacts, function (index, value) {
//                $("#contacts ul").append(
//                    "<li class='topcoat-list__item'>\
//                        First Name: " + value.first + "<br> Last Name: " + value.last +
//                   "</li>"
//                );

//            });

//        }

//    });

//});

///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////
