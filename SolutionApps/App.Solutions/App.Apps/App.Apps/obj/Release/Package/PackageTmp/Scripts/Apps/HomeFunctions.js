/*******************************************************
********************************************************
***Function      : Common Function *********************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 04/09/2015*** ***********************
********************************************************
********************************************************/

//<div id="winSize"></div>
//<script>
//    var WindowsSize=function(){
//        var h=$(window).height(),
//            w=$(window).width();
//        $("#winSize").html("<p>Width: "+w+"<br>Height: "+h+"</p>");
//    };
//$(document).ready(function(){WindowsSize();}); 
//$(window).resize(function(){WindowsSize();}); 
//</script>

//try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
$(document).ready(function () {
    $(window).resize(function (event) {
        $('#menu-toggler').show();
        MenusActionsStatus(event);
    });
    $(window).load(function (event) {
        $('#menu-toggler').show();
        MenusActionsStatus(event);
    });
    $("#menu-toggler").click(function (event) {
        if ($(window).width() >= 975) {
            MenusActions(event);
        }
    });
    function MenusActionsStatus(ActionEvent) {
        try {
            $('.SliderMenuLeftPanel').css({ 'display': 'block' });
        }
        catch (e) {
        }
    }
    function MenusActions(ActionEvent) {
        try {
            if ($('.SliderMenuLeftPanel').css('display') == 'none') {
                $('.SliderMenuLeftPanel').css({ 'display': 'block' });
            } else {
                $('.SliderMenuLeftPanel').css({ 'display': 'none' });
            }
        }
        catch (e) {
        }
    }

    $("#widgetboxclose").click(function (event) {
        $('#ProductWidgetBox').css({ 'display': 'none' });
        $("#widgetBoxData").empty();
        return false;
    });
    //$(".ProductShowDetail").click(function (event) {
    //    ConfirmBootBox("Header Message", "Product Detail : " + event, 'QPRO_Success', initialCallbackYes, initialCallbackNo);
    //    WidgetBoxActions(event);
    //    return false;
    //});
    //$(".ProductAddToKart").click(function (event) {
    //    ConfirmBootBox("Header Message", "Product Detail : " + event, 'QPRO_Success', initialCallbackYes, initialCallbackNo);
    //    WidgetBoxActions(event);
    //    return false;
    //});

});
$(window).on("resize", function () {
    if ($(window).width() > 991 && $('#sidebar').css('display') == 'none') {
        $('#sidebar').css({ 'display': 'block' });
    }
    else { $('#sidebar').css({ 'display': 'block' }) }
});

//GetSetProductShowDetail('AJSKUQHHAJHSHAUS')
//GetSetProductAddToKart('ANSMAHSJAGSJGAJ')


function GetSetProductShowDetail(ActionEvent) {
    try {
        WidgetBoxActions(ActionEvent);
    }
    catch (e) {
    }
    return false;
}
function GetSetProductAddToKart(ActionEvent) {
    try {
        WidgetBoxActions(ActionEvent);
    }
    catch (e) {
    }
    return false;
}

function WidgetBoxActions(ActionEvent) {
    try {
        if ($('#ProductWidgetBox').css('display') == 'none') {
            $('#ProductWidgetBox').css({ 'display': 'block' });
            $('#ProductWidgetBox').widget_box('show').addClass('fullscreen');
            //var htmlData = ActionEvent + ' ::::: <p class="alert alert-info">Nunc aliquam enim ut arcu aliquet adipiscing. Fusce dignissim volutpat justo non consectetur. Nulla fringilla eleifend consectetur.</p><p class="alert alert-success">Raw denim you probably have not heard of them jean shorts Austin.</p>';
            var htmlData = ActionEvent + ' ::::: ';
            $("#widgetBoxData").empty().html(htmlData);
        } else {
            $('#ProductWidgetBox').css({ 'display': 'none' });
        }
        //ConfirmBootBox("Header Message", "Product Detail : " + ActionEvent, 'QPRO_Success', initialCallbackYes, initialCallbackNo);
        //var a = $("#ProductWidgetBox").addClass('fullscreen').css({ 'display': 'block' });
        //$('#my-widget').widget_box('toggle');
        //$('#ProductWidgetBox').widget_box('show').addClass('fullscreen');
        //$('#ProductWidgetBox').widget_box('close');
        //$('#my-widget').widget_box('reload');
        UserProductActions(ActionEvent);
    }
    catch (e) {
    }
    return false;
}


function UserProductActions(ActionProductcodeEvent) {
    try {
        var myDataArray = new Array();
        myDataArray.push({ Productcode: ActionProductcodeEvent, ProductQuantity: 1 });

        //var myArray = new Array();
        //undefined
        //myArray.push({ name: 'McGruff', company: 'Police', zip: 60652 });
        //1
        //myArray.push({ name: 'Jared', company: 'Upstatement', zip: 63124 });
        //2
        SolutionDataTraveler('SET', 'ProductsSelected', myDataArray);
    }
    catch (e) {
    }
    return false;
}


