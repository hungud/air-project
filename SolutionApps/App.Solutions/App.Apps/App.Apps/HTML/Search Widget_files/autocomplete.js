/*jslint  browser: true, white: true, plusplus: true */
/*global $, countries */
var results=[];
$(function () {
    'use strict';
	
	
    // Initialize ajax autocomplete:
    $('#OBdep1, #OBarr1, #OBdep2, #OBarr2, #OBdep3, #OBarr3, #OBdep4, #OBarr4, #hotelDestination, #pickUp, #dropOff,#txtDestination1,#txtOrigin').autocomplete({
       
	   source: function (request, response) {
		   var matcher = new RegExp( "^" + $.ui.autocomplete.escapeRegex( request.term ), "i" );
            response( $.grep( results, function( item ){
                return matcher.test(item);
            }) );
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
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });

        },
			
        select: function (e, i) {
			$("#hfCustomerId").val(i.item.val);
		    
        }, 
        minLength: 3,
		delay: 0
    });
	

    
});


function parseXml(xml) {
   
    $(xml).find('CityStateCountry').each(function () {
        //alert('parsexml : ' + $(this).val());
        if ($(this).val()==false) {
            //alert('thisval');
            results.push({
                value: $(this).find('CityName').text(),
                key: $(this).find('StateName').text()
            })
        }
    });
	
}

