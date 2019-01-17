/*jslint  browser: true, white: true, plusplus: true */
/*global $, countries */
var results=[];
$(function () {
    'use strict';
	
	
    // Initialize ajax autocomplete:
    $('#txtDestination,#txtOrigin,#txtMLOrigin_1,#txtMLDestination_1,#txtMLOrigin_2,#txtMLDestination_2,#txtMLOrigin_3,#txtMLDestination_3,#txtMLOrigin_4,#txtMLDestination_4,#txtMLOrigin_5,#txtMLDestination_5').autocomplete({
        source: function (request, response) {
            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
            response($.grep(results, function (item) {
                return matcher.test(item);
            }));
            console.log(this.id);

            $.ajax({
                cache: false,
                // url: "http://qa.nanojot.com/searchboxwebservice.asmx/GetCityNameListJson",
                //url: "http://air.nanojot.com/searchboxwebservice/searchboxwebservice.asmx/Get_AE_CityNameListJson",
                url: "http://search.nanojot.com/searchboxwebservice.asmx/Get_AE_CityNameListJson",
                data: ({ prefix: request.term }),
                type: "POST",
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                contentType: "application/json; charset=utf-8",
                dataType: "xml",
                success: function (xml) {
                    results = new Array();
                    parseXml(xml);
                    response($.map(results, function (item) {
                        return {
                            label: item.value,
                            val: item.key
                        }
                    }))
                },

                error: function (response) {
                    //alert(response.responseText);
                },
                failure: function (response) {
                    //alert(response.responseText);
                }
            });

        },

        select: function (e, i) {
            $("#hfCustomerId").val(i.item.val);

        },
        minLength: 3,
        delay: 0
    });


    $.ajax({
        cache: false,
        // url: "http://qa.nanojot.com/searchboxwebservice.asmx/GetCityNameListJson",
        //url: "http://air.nanojot.com/searchboxwebservice/searchboxwebservice.asmx/Get_AE_CityNameListJson",
        url: "http://search.nanojot.com/searchboxwebservice.asmx/GetAllAirlines",        
        type: "POST",
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "xml",
        success: function (xml) {
            results = new Array();
            parseXml1(xml);
        },

        error: function (response) {
            //alert(response.responseText);
        },
        failure: function (response) {
           // alert(response.responseText);
        }
    });



});


function parseXml(xml) {

    $(xml).find('CityStateCountry').each(function () {
        //alert('parsexml : ' + $(this).val());
        if ($(this).val() == false) {
            //alert('thisval');
            results.push({
                value: $(this).find('CityName').text(),
                key: $(this).find('StateName').text()
            })
        }
    });

}


function parseXml1(xml) {

    $(xml).find('Airline').each(function () {
        var airLinename = $(this).find('airlinename').text().capitalize();
        $("#ddlAirline").append($("<option></option>").val($(this).find('iata').text()).html(airLinename));
    });

}

String.prototype.capitalize = function () {

    var str = this.toLowerCase();
    var splitStr = str.split(" ");
    var capitalize = "";
    for (var i = 0; i < splitStr.length; i++) {
        capitalize += splitStr[i].charAt(0).toUpperCase() + splitStr[i].slice(1) + " ";
    }
    return capitalize.trim();    
}