/*******************************************************
********************************************************
***Function      : Common Configuration *********************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 04/06/2017*** ***********************
********************************************************
********************************************************/
var CommonConfiguration = {
    WebAPIServicesURL: "http://localhost:11000/",
    WebApplicationURL: "http://localhost:11011/",
    WebSSOURL: "http://localhost:11000/",
    //AirProjectURL: "http://qa.nanojot.com/air/",
    AuthUrl: "http://localhost:62468/",
    AirProjectURL: "http://localhost:11011/",
    HomePage: "HomePage",
    LogOutPage: "LogOutPage",
    searchBoxService: "http://qa.nanojot.com/services/searchbox/searchboxwebservice.asmx",
    unauthurl: "http://localhost:62468/Account/Login?returnUrl=http://localhost:11011/"
}

//var CommonConfiguration = {
//    WebAPIServicesURL: "http://qa.nanojot.com/services/Air/",
//    WebApplicationURL: "http://qa.nanojot.com/services/Air/",
//    WebSSOURL: "http://qa.nanojot.com/services/Air/",
//    AirProjectURL: "http://qa.nanojot.com/air/",
//    AuthUrl: "http://qa.nanojot.com/authentication/",
//    HomePage: "HomePage",
//    LogOutPage: "LogOutPage",
//    searchBoxService: "http://qa.nanojot.com/services/searchbox/searchboxwebservice.asmx",
//    //unauthurl: "http://qa.nanojot.com/authentication//Account/Login?returnUrl=http://qa.nanojot.com/air/"
//}

//var CommonConfiguration = {
////http://localhost:7932/searchboxwebservice.asmx
//    searchBoxService: "http://qa.nanojot.com//services/searchbox/searchboxwebservice.asmx",
//    //WebAPIServicesURL: "http://localhost:11000/",
//    WebApplicationURL: "http://airservice.nanojot.com/",
//    WebSSOURL: "http://airservice.nanojot.com/",
//    HomePage: "HomePage",
//    LogOutPage: "LogOutPage"
//}

//////////////////User Profiles//////////////////////////////////////////////////////////
var sConfigServicePath, sURLSolutionApp, sProjectName, sSolutionAppCulture,TotalPassagerMaxCount = 7, PassagerMaxCount = 7;
//////////////////User Profiles//////////////////////////////////////////////////////////
function AdultInfantValidation() {
    try {
        if ($('.noOfAdults').val() < $(".noOfInfants").val()) {
            ConfirmBootBox("Successfully", "Number of infants cannot be more than number of adults, incase you want to accompany more infants, please call agency.", 'App_Warning', initialCallbackYes, initialCallbackNo);
            return false;
        }
        return true;
    }
    catch (e) {
        var error = e;
        error = e;
    }
}
/*******************************************************
********************************************************
***Function      : Common Configuration *********************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 04/06/2017*** ***********************
********************************************************
********************************************************/

