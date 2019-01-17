// JScript File

/*
///*************************************************
/// Developed By:   RAKESH PAL (SSE)                
/// Company Name:   NIIT Technologies GIS Ltd            
/// Created Date:   Developed on: 18/10/2010           
/// Summary :
///*************************************************
*/



function GetFunctionMonth(strValue) {
    var Month;
    switch (strValue) {
        case "Jan":
            Month = 1;
            break;
        case "Feb":
            Month = 2;
            break;
        case "Mar":
            Month = 3;
            break;
        case "Apr":
            Month = 4;
            break;
        case "May":
            Month = 5;
            break;
        case "Jun":
            Month = 6;
            break;
        case "Jul":
            Month = 7;
            break;
        case "Aug":
            Month = 8;
            break;
        case "Sep":
            Month = 9;
            break;
        case "Oct":
            Month = 10;
            break;
        case "Nov":
            Month = 11;
            break;
        case "Dec":
            Month = 12;
            break;
        default:
            Month = 0;
    }
    return Month;
}






function checkTime(i) {
    return (i < 10) ? "0" + i : i;
}
function GetCurrentTime() {
    var today = new Date(),
        h = checkTime(today.getHours()),
        m = checkTime(today.getMinutes()),
        s = checkTime(today.getSeconds());
    return h + ":" + m + ":" + s;
}
function startTime() {
    //var today = new Date(),
    //    h = checkTime(today.getHours()),
    //    m = checkTime(today.getMinutes()),
    //    s = checkTime(today.getSeconds());
    //document.getElementById('time').innerHTML = h + ":" + m + ":" + s;
    document.getElementById('time').innerHTML = GetCurrentTime();
    t = setTimeout(function () {
        startTime()
    }, 500);
}
//(function () {
//    startTime();
//})();




















/*
1.CheckPin(obj)            --PIN
2.ValidatePan(obj)         --PAN No
3.CheckPhone(obj)          --PHONE No
4.ConvertToUpperCase(value,object) -- ConvertToUpperCase
5.Onlytext()               --Only Text is Allowed
6.TwoDigitRound(obj)        --Rounding 2 decimal
*/
/***********************************************
Code for Date validation starts
***********************************************/
// Declaring valid date character, minimum year and maximum year

var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;
function TwoDigitRound(obj) {
    var str = obj.value;
    var decimalIndex = str.indexOf(".");
    if (decimalIndex >= 0)
        obj.value = Math.round(obj.value * 100) / 100
    if (obj.value == "" || obj.value == ".")
        obj.value = 0;
}

function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function OnlyNumberWithDecimal(ID) {
    var str = ID.value
    if ((window.event.keyCode > 47 && window.event.keyCode <= 57) || window.event.keyCode == 46) {
        if ((window.event.keyCode == 46) && (str.indexOf(".") == str.lastIndexOf("."))) {
            if (str.indexOf(".") == -1) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (window.event.keyCode != 46) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}


function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31;
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30; }
        if (i == 2) { this[i] = 29; }
    }
    return this;
}


// Validate the date entry
function isDate(dtStr) {
    var daysInMonth = DaysArray(12);
    var pos1 = dtStr.indexOf(dtCh);
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1);
    //var strDay=dtStr.substring(0,pos1);
    //var strMonth=dtStr.substring(pos1+1,pos2);
    //	var strMonth=dtStr.substring(0,pos1);
    //	var strDay=dtStr.substring(pos1+1,pos2);
    var strDay = dtStr.substring(0, pos1);
    var strMonth = dtStr.substring(pos1 + 1, pos2);
    var strYear = dtStr.substring(pos2 + 1);

    strYr = strYear;
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1);
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1);
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1);
    }
    month = parseInt(strMonth);
    day = parseInt(strDay);
    year = parseInt(strYr);
    if (pos1 == -1 || pos2 == -1) {
        return false;
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        return false;
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        return false;
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        return false;
    }
    if (isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        return false;
    }
    return true;
}


function CheckDateFormat(sender, args) {
    //var dt=txtControl;
    if (isDate(args) == false) {
        args.IsValid = false;
        //sender.focus();
        return false;
    }
    args.IsValid = true;
    return true;
}

/***********************************************
Code for Date validation ends
***********************************************/
/***********************************************
Code for checkAlphaNumeric (Starts with Character) validation starts
***********************************************/
function CheckAlphaNumeric(obj) {
    var OUserName = obj;

    if (OUserName.value.length < 1) {
        if (window.event.keyCode >= 65 && window.event.keyCode <= 90) {
            return true;
        }
        else if (window.event.keyCode >= 97 && window.event.keyCode <= 122) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        //small alphabets check
        if (window.event.keyCode < 97 || window.event.keyCode > 122) {
            //Caps Alphabets check
            if (window.event.keyCode < 65 || window.event.keyCode > 90) {
                //Integer Checks
                if (window.event.keyCode < 48 || window.event.keyCode > 57) {
                    //'_' and 'space' check
                    if (window.event.keyCode == 95 || window.event.keyCode == 32) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }

    }
}


/***********************************************
Code for checkAlphaNumeric (Starts with Character) validation ends
***********************************************/

/***********************************************
Code for AllowAlphaNumericExceptAngleBraces except Angle Braces(Starts with Character) validation starts
***********************************************/
function AllowAlphaNumericExceptAngleBraces(obj) {
    var OUserName = obj;

    if (OUserName.value.length < 1) {

        if (window.event.keyCode >= 65 && window.event.keyCode <= 90) {
            return true;
        }
        else if (window.event.keyCode >= 97 && window.event.keyCode <= 122) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        //small alphabets check
        if (window.event.keyCode < 97 || window.event.keyCode > 122) {
            //Caps Alphabets check
            if (window.event.keyCode < 65 || window.event.keyCode > 90) {
                //Integer Checks
                if ((window.event.keyCode < 48 || window.event.keyCode > 57)) {
                    //60 for < & 62 for >
                    if (window.event.keyCode != 60 && window.event.keyCode != 62)//window.event.keyCode ==95 || window.event.keyCode ==32 )
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }

    }
}


/***********************************************
Code for AllowAlphaNumericExceptAngleBraces except Angle Braces (Starts with Character) validation ends
***********************************************/

/***********************************************
Code for minimum username/password length validation starts
To be used with Custom Validator.
***********************************************/
function CheckUserNameLength(obj) {
    if (obj.length < 4) {
        return false;
    }
    else {
        return true;
    }
}

function CheckPasswordLength(obj) {
    if (obj.length < 4) {
        return false;
    }
    else {
        return true;
    }
}

/***********************************************
Code for minimum username/password length validation ends
***********************************************/

/***********************************************
Code for highlighting gridview rows on mouse hover starts
***********************************************/

var oldgridSelectedColor;
function setMouseOverColor(element) {
    oldgridSelectedColor = element.style.backgroundColor;
    element.style.backgroundColor = 'lightyellow';
    element.style.cursor = 'hand';
    element.style.textDecoration = 'underline';
}

function setMouseOutColor(element) {
    element.style.backgroundColor = oldgridSelectedColor;
    element.style.textDecoration = 'none';
}

/***********************************************
Code for highlighting gridview rows on mouse hover ends
***********************************************/

/***********************************************
Code for RequiredField Validation starts
***********************************************/
function RequiredField(Control, Message) {
    var obj = Control;
    if (obj.value.length == 0) {
        obj.focus();
        return false;
    }
    else {
        return true;
    }
}
/***********************************************
Code for RequiredField Validation ends
***********************************************/

/***********************************************
Code for minimum username/password length validation starts
***********************************************/
function MinTextBoxLength(Control, Message) {
    var obj = Control;
    if (obj.value.length < 3) {
        obj.focus();
        return false;
    }
    else {
        return true;
    }
}
/***********************************************
Code for minimum username/password length validation ends
***********************************************/

/***********************************************
Code for Multiple Email validation by seperated ';' starts
***********************************************/
function ValidateEmail(Control) {
    var _doc = Control.split(";");
    var doc = '';
    for (var i = 0; i < _doc.length; i++) {
        doc = trim(_doc[i]);
        if (doc != "") {
            var str = doc;
            var filter = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
            if (filter.test(str))
            { }
            else {
                return false;
            }
        }
    }
    return true;
}
/***********************************************
Code for Email validation ends
***********************************************/

/***********************************************
Code for Web Address validation starts
***********************************************/
function ValidateWeb(Control) {
    doc = Control;

    if (doc.value != "") {
        var str = doc.value;
        var filter = /^www\.[a-z]+\.(com)|(org)|(edu)|(net)$/;
        if (filter.test(str))
            return true;
        else {
            return false;
        }
    }

    return true;
}
/***********************************************
Code for Web Address validation ends
***********************************************/

/***********************************************
Code for Text Box Length 250 validation starts
***********************************************/
function MaxLength250(textbox) {
    var value = textbox.value.length;
    if (value < 250)
        return true;
    else
        return false;
}

/***********************************************
Code for Text Box Length 250 validation end
***********************************************/
/***********************************************
Code for Text Box Length 250(Drag Drop) validation starts
***********************************************/
function MaxLength250RemainingLost(obj) {
    if (obj.value.length > 250) {
        obj.value = obj.value.substring(0, 250)
    }

}
/*****************************************************
Code for Text Box Length 250(Drag Drop) validation end
******************************************************/
/***********************************************
Code for Date validation starts
***********************************************/
function Checkdate() {
    var checkValid = /^((((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9]))))[\-\/\s]?\d{2}(([02468][048])|([13579][26])))|(((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))[\-\/\s]?\d{2}(([02468][1235679])|([13579][01345789]))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$/;
    if (document.getElementById("txtInteractindate").value.match(checkValid)) {
        return (true);
    }
    else {
        document.getElementById("txtInteractindate").value = "";
        return (false);
    }
}
/***********************************************
Code for Date validation end
***********************************************/

/***********************************************
Code for RequiredField Validation and message will be shown in a control (ctrl) starts
***********************************************/
function RequiredFieldonLabel(Control, Message, ctrl) {
    var obj = Control;
    if (obj.value.length == 0) {
        obj.focus();
        return false;
    }
    else {
        return true;
    }
}
/***********************************************
Code for RequiredField Validation and message will be shown in a control (ctrl) end
***********************************************/
/***********************************************
Code for Combo selection Validation and message will be shown in a control (ctrl) starts
***********************************************/
function RequiredComboselection(Control, Message) {
    var obj = Control;
    if (obj.selectedIndex == 0) {
        obj.focus();
        return false;
    }
    else {
        return true;
    }
}


/***********************************************
Code for RequiredField Validation and message will be shown in a control (ctrl) end
***********************************************/
/***********************************************
Code for Correct Email ID Validation and message will be shown in a control (ctrl) starts
***********************************************/
function RequiredEmailIDLabel(Control, Message, ctrl) {
    var obj = Control;
    if (!ValidateEmail(Control)) {
        obj.focus();
        return false;
    }
    else {
        return true;
    }
}





/***********************************************
Code for RequiredField Validation and message will be shown in a control (ctrl) end
***********************************************/
/***********************************************
Code for IsInteger Validation and message will be shown in a control (ctrl) starts
***********************************************/
function RequiredIntegerValueLabel(ID) {
    return OnlyNumberWithDecimal(ID);
}

/***********************************************
Code for Combo selection Validation and message will be shown in a control (ctrl) end
***********************************************/
/***********************************************
Code to allow 0,1,2,3,4,5,6,7,8,9 only starts
***********************************************/
function OnlyNumeric() {
    if ((window.event.keyCode > 47 && window.event.keyCode <= 57)) {
        return true;
    }
    else {
        return false;
    }
}
/***********************************************
Code to allow 0,1,2,3,4,5,6,7,8,9 only end
***********************************************/
function Checkdate(Control) {
    var checkValid = /^((((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9]))))[\-\/\s]?\d{2}(([02468][048])|([13579][26])))|(((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))[\-\/\s]?\d{2}(([02468][1235679])|([13579][01345789]))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$/;
    if (Control.value.match(checkValid)) {
        return (true);
    }
    else {
        Control.value = "";
        return (false);
    }
}
//----------------------------------------------
function CheckPin(obj) {
    if (obj.value.length <= 6) {
        if (window.event.keyCode > 47 && window.event.keyCode <= 57) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function ValidatePan(obj) {
    if (obj.length <= 9) {
        if (obj.length <= 4) {
            if (window.event.keyCode >= 65 && window.event.keyCode <= 90) {
                return true;
            }
            else if (window.event.keyCode >= 97 && window.event.keyCode <= 122) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (obj.length <= 8) {
            if ((window.event.keyCode < 48) || (window.event.keyCode > 57)) {
                return false;
            }
        }
        else if (obj.length > 8) {
            if (window.event.keyCode >= 65 && window.event.keyCode <= 90) {
                return true;
            }
            else if (window.event.keyCode >= 97 && window.event.keyCode <= 122) {
                return true;
            }
            else {
                return false;
            }
        }
    }
    else {
        return false;
    }
}

function CheckPhone(obj) {
    //40= ( , 41= ),     44= ,   45 = -
    if (window.event.keyCode == 40 || window.event.keyCode == 41) {
        return true;
    }
    if (window.event.keyCode == 44 || window.event.keyCode == 45) {

        if (obj.value.length == 0) {
            return false;
        }
        else {
            return true;
        }
    }
    else if (window.event.keyCode > 47 && window.event.keyCode <= 57) {
        return true;
    }
    else {
        return false;
    }
}

function OnlyText() {
    if (window.event.keyCode >= 65 && window.event.keyCode <= 90 || window.event.keyCode == 32) {
        return true;
    }
    else if (window.event.keyCode >= 97 && window.event.keyCode <= 122 || window.event.keyCode == 32) {
        return true;
    }
    else {
        return false;
    }
}

function ConvertToUpperCase(value, object) {
    var textbox = object;
    var newvalue = value.toUpperCase();
    textbox.value = newvalue;
}


//This will show the confirm message to delete.        
function ConfirmDelete() {
    if (confirm("Are you sure, you want to delete.") == true)
        return true;
    else
        return false;
}
//------------------------

function MaxLength(textbox, valuelength) {
    var value = textbox.value.length;
    if (value < valuelength)
        return true;
    else
        return false;
}



function MaxLengthRemainingLost(textbox, valuelength) {
    if (textbox.value.length > valuelength) {
        textbox.value = textbox.value.substring(0, valuelength)
    }
}


function ltrim(str) {
    for (var k = 0; k < str.length && isWhitespace(str.charAt(k)) ; k++);
    return str.substring(k, str.length);
}

function rtrim(str) {
    for (var j = str.length - 1; j >= 0 && isWhitespace(str.charAt(j)) ; j--);
    return str.substring(0, j + 1);
}

function trim(str) {
    return ltrim(rtrim(str));
}

function isWhitespace(charToCheck) {
    var whitespaceChars = " \t\n\r\f";
    return (whitespaceChars.indexOf(charToCheck) != -1);
}
