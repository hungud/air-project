/*******************************************************
********************************************************
***Function      : Master App Configration *************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 17/09/2015*** ***********************
********************************************************
********************************************************/


SolutionUserProfileTraveler = {
    UserName: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'UserName');
    },
    UserID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'UserID');
    },
    RoleID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'RoleID');
    },
    EmailID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'EmailID');
    },
    FirstName: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'FirstName');
    },
    LastName: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'LastName');
    },
    Roles: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'Roles');
    },
    Token: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'Token');
    },
    DeptID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'DeptID');
    },
    AgTypeID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'AG_Type_ID');
    },
    AgencyID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'AgencyID');
    },
    CommetteeID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'CommetteeID');
    },
    AppID: function () {
        return fatchLocalStorageData('Solution_User_Profile_Traveler', 'APPID');
    }
}



SolutionConfigTraveler = {
    MapService: function () {
        return SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["DynamicMapService"];
    },
    DynamicService: function () {
        return SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["DynamicMapService"];
    },
    FeatureService: function () {
        return SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["FeatureMapService"];
    },
    GeometryService: function () {
        return SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["GeometryService"];
    },
    PrintService: function () {
        return SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["PrintService"];
    },
    TablePageSize: function () {
        return parseInt(SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["TablePageSize"]);
    },
    TableEmptyMassege: function () {
        return (SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["TableEmptyMassege"]);
    },
    AppService: function () {
        return (SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["AppService"]);
    },
    ConfigService: function () {
        return (SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["ConfigService"]);
    },
    SSOSTSService: function () {
        return (SolutionDataTraveler("GET", "SolutionAppConfigTraveler")["SSOSTSService"]);
    },


}

SolutionAppDataTraveler = {
    ROCODE: function () {
        return SolutionDataTraveler('GET', 'ROCODE');
    },
    PROJECTID: function () {
        return SolutionDataTraveler('GET', 'PROJECTID');
    },
    AGENCYTYPEID: function () {
        return SolutionDataTraveler('GET', 'AGENCYTYPEID');
    }
}



SolutionGISModuleTraveler = {
    ROCODE: function () {
        return SolutionDataTraveler('GET', 'GISMODULEROCODE');
    },
    PROJECTID: function () {
        return SolutionDataTraveler('GET', 'GISMODULEPROJECTID');
    },
    PIN: function () {
        return SolutionDataTraveler('GET', 'GISMODULECURRENTPIN');
    }
}

//SolutionGISModuleTraveler.PIN();




function fatchLocalStorageData(selectionType, selection) {
    try {
        var returndata = '';
        $.each($.localStorage(selectionType), function (r_value, r_key) {
            if (r_key.key == selection) {
                return returndata = r_key.value;
            }
        });
        return returndata;
    } catch (e) {
        //location.href = sURLSolutionApp;
    }
}


function SolutionDataTraveler(ActionType, StorageID, StorageData) {
    try {
        var appDataTraveler = '', appDataTraveler_StorageID = '', appDataTravelerID = 'SolutionData_App_Data_Traveler';
        appDataTraveler_StorageID = appDataTravelerID + '|_|' + StorageID;
        if (ActionType == 'SET') {
            appDataTraveler = $.localStorage(appDataTraveler_StorageID, StorageData);
        }
        else if (ActionType == 'GET') {
            appDataTraveler = $.localStorage(appDataTraveler_StorageID)
        }
        else if (ActionType == 'Clear') {
            var SolutionData_Profiler = new Array();
            SolutionData_Profiler = $.localStorage.getAll();
            $.each($.localStorage.getAll(), function (r_value, r_key) {
                if (r_key.key.split('|_|')[1] == StorageID) {
                    $.localStorage.deleteItem(r_key.key); //$.localStorage.getAll();//$.localStorage.deleteItem(;ID')
                }
            });
            appDataTraveler = 'Clear';
        }
        else if (ActionType == 'ClearAll') {
            var SolutionData_Profiler = new Array();
            SolutionData_Profiler = $.localStorage.getAll();
            $.each($.localStorage.getAll(), function (r_value, r_key) {
                if (r_key.key.split('|_|')[0] == appDataTravelerID) {
                    $.localStorage.deleteItem(r_key.key); //$.localStorage.getAll();//$.localStorage.deleteItem(;ID')
                }
            });
            appDataTraveler = 'ClearAll';
        }
        else {
            appDataTraveler = '';
        }
        return appDataTraveler;
    } catch (e) {
        //location.href = sURLSolutionApp;
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
