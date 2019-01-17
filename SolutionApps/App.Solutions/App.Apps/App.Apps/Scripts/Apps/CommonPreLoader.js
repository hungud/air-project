/*******************************************************
********************************************************
***Function      : Common PreLoader *********************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 04/08/2015*** ***********************
********************************************************
********************************************************/

$(document).ready(function () { initializePreLoaderWaitPopupBox(); });
function initializePreLoaderWaitPopupBox() {
    var html = '<div id="PreLoaderAndWaiting" style="display: block;"> <div id="PreLoaderAndWaiting_div" class="ui-corner-all"><h2 id="row1" style="clear: both;display: block;">Search for the best value of your money is in progress….</h2><div> <div style="margin-top:10px" id="loader"></div><h2 style="color:blue;font-weight:normal;padding-left: 26px;padding-top: 6px;font-size: 13px;font-weight: bold;">Please wait..</h2></div><h2 id="row2">Best Hotels and Car Rentals are at your finger tips</h2></div> </div>';
    $("body").append(html);
    $("#PreLoaderAndWaiting").hide();
}

function ShowWaitProgress() {
    $("#PreLoaderAndWaiting").show();
    //debugger;
    var caller = HideWaitProgress.caller;
    //if ($('#ProductWidgetBox').css('display') == 'none') {
    //    $('#ProductWidgetBox').css({ 'display': 'block' });
    //    $('#ProductWidgetBox').widget_box('show').addClass('fullscreen');
    //} else {
    //    $('#ProductWidgetBox').css({ 'display': 'none' });
    //}
}

function HideWaitProgress() {
    //alert('hidecalled');
    //alert("caller is " + HideWaitProgress.caller);
   // debugger;
    var caller = HideWaitProgress.caller;
    if (caller != null) {
        $("#PreLoaderAndWaiting").hide();
    }
    //$('#ProductWidgetBox').css({ 'display': 'none' });
}



//$(".basic").click(function () {
//    waitingDialog.show('Loading Something...');
//    setTimeout(function () {
//        waitingDialog.hide();
//    }, 3000);
//});
//$(".custom").click(function () {
//    waitingDialog.show('Loading Something...', {
//        headerText: 'jQueryScript',
//        dialogSize: 'sm',
//        progressType: 'danger'
//    });
//    setTimeout(function () {
//        waitingDialog.hide();
//    }, 3000);
//});
//$(".callback").click(function () {
//    waitingDialog.show('Loading Something...', {
//        progressType: 'success',
//        onHide: function () { alert('Callback!'); }
//    });
//    setTimeout(function () {
//        waitingDialog.hide();
//    }, 3000);
//});


//top.window.moveTo(0, 0);
//// Resize the window to the available space in the browser window
//if (document.all) {
//    top.window.resizeTo(screen.availWidth, screen.availHeight);
//}
//else if (document.layers || document.getElementById) {
//    if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
//        top.window.outerHeight = screen.availHeight;
//        top.window.outerWidth = screen.availWidth;
//    }
//}


/***************************************************************************************
***********************Default Application Preloader************************************
****************************************************************************************/

ShowWaitProgress();
window.onload = HideWaitProgress;
window.onreadystatechange = ShowWaitProgress;
window.onAfterUnload = HideWaitProgress;
window.onunload = HideWaitProgress;

/***************************************************************************************
***********************Default Application Preloader************************************
****************************************************************************************/




//////////////////////////////////DisableRightClick////////////////////////////////////////////

function clickIE()
{ if (document.all) { (message); return false; } }
function clickNS(e) {
    if (document.layers || (document.getElementById && !document.all)) {
        if (e.which == 2 || e.which == 3) { (message); return false; }
    }
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

//DisableRightClick();



/***************************************************************************************
***********************Default Application Preloader************************************
****************************************************************************************/


/***************************************************************************************
***********************Default Application Windows Open*********************************
****************************************************************************************/

function OpenWinWithToolBar(surl) {
    var windowprops = "height=500, left=0, location=no, menubar=no, resizable=yes, scrollbars=yes, status=yes, titlebar=yes, toolbar=yes, top=0, width=780";
    var objwin = window.open(surl, "OPROAPP", windowprops);
    objwin.focus();
}

var GetCurrentPanel;
var GetChldWnd = new Array();
var oPenCount = 0;
function GetOpenPanels(pagename) {
    oPenCount = oPenCount + 1;
    try {
        GetCurrentPanel.Close()
    }
    catch (err) {
    }
    var windowprops = "height=" + screen.height + ", left=0, location=no, channelmode=yes, menubar=no, resizable=yes, scrollbars=yes, status=yes, titlebar=no, toolbar=no, top=0, width=" + screen.width;
    GetCurrentPanel = window.open(pagename, "CRMSVITAL", windowprops);
    GetCurrentPanel.focus();
    GetChldWnd[oPenCount - 1] = GetCurrentPanel;
}

function GetOpenWithHWPanels(page, pageName, width, height) {
    oPenCount = oPenCount + 1;
    try {
        GetCurrentPanel.Close()
    }
    catch (err) {
    }
    GetCurrentPanel = window.open(page, pageName, 'menubar=no,resizable=no,scrollbars=yes,toolbar=no,width=' + width + ',height=' + height + ',top=50,left=50');
    GetChldWnd[oPenCount - 1] = GetCurrentPanel;
}


function OpenGISViewer(page) {
    var wOpen;
    var sOptions;
    sOptions = 'status=no,menubar=no,scrollbars=no,resizable=no,toolbar=no';
    sOptions = sOptions + ',width=' + (screen.availWidth - 10).toString();
    sOptions = sOptions + ',height=' + (screen.availHeight - 122).toString();
    sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';

    top.window.opener = top;
    top.window.open('', '_parent');
    wOpen = top.window.open(page, '', sOptions);
    wOpen.focus();
    wOpen.moveTo(0, 0);
    wOpen.resizeTo(screen.availWidth, screen.availHeight);
    return wOpen;
}
function OpenDashViewer(page) {
    var wOpen;
    var sOptions;
    sOptions = 'status=no,menubar=no,scrollbars=yes,resizable=no,toolbar=no';
    sOptions = sOptions + ',width=' + (screen.availWidth - 10).toString();
    sOptions = sOptions + ',height=' + (screen.availHeight - 122).toString();
    sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';

    top.window.opener = top;
    top.window.open('', '_parent');
    wOpen = top.window.open(page, '', sOptions);
    wOpen.focus();
    wOpen.moveTo(0, 0);
    wOpen.resizeTo(screen.availWidth, screen.availHeight);
    return wOpen;
}

function OpenWindow(pagename) {
    window.open(pagename, '', 'location=no,menubar=no,titlebar=no,scrollbars=yes,resizable=1');
}

/***************************************************************************************
***********************Default Application Windows Open*********************************
****************************************************************************************/
