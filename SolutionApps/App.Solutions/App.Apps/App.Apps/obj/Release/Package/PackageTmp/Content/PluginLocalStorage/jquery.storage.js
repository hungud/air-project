/*!
 * jquery.storage.js 0.0.3 - https://github.com/yckart/jquery.storage.js
 * The client-side storage for every browser, on any device.
 *
 * Copyright (c) 2012 Yannick Albert (http://yckart.com)
 * Licensed under the MIT license (http://www.opensource.org/licenses/mit-license.php).
 * 2013/02/10
 **/
;(function($, window, document) {
    'use strict';

    $.map(['localStorage', 'sessionStorage'], function( method ) {
        var defaults = {
            cookiePrefix : 'fallback:' + method + ':',
            cookieOptions : {
                path : '/',
                domain : document.domain,
                expires : ('localStorage' === method) ? { expires: 365 } : undefined
            }
        };

        try {
            $.support[method] = method in window && window[method] !== null;
        } catch (e) {
            $.support[method] = false;
        }

        $[method] = function(key, value) {
            var options = $.extend({}, defaults, $[method].options);

            this.getItem = function( key ) {
                var returns = function(key){
                    return JSON.parse($.support[method] ? window[method].getItem(key) : $.cookie(options.cookiePrefix + key));
                };
                if(typeof key === 'string') return returns(key);

                var arr = [],
                    i = key.length;
                while(i--) arr[i] = returns(key[i]);
                return arr;
            };

            this.setItem = function( key, value ) {
                value = JSON.stringify(value);
                return $.support[method] ? window[method].setItem(key, value) : $.cookie(options.cookiePrefix + key, value, options.cookieOptions);
            };

            this.removeItem = function( key ) {
                return $.support[method] ? window[method].removeItem(key) : $.cookie(options.cookiePrefix + key, null, $.extend(options.cookieOptions, {
                    expires: -1
                }));
            };

            this.clear = function() {
                if($.support[method]) {
                    return window[method].clear();
                } else {
                    var reg = new RegExp('^' + options.cookiePrefix, ''),
                        opts = $.extend(options.cookieOptions, {
                            expires: -1
                        });

                    if(document.cookie && document.cookie !== ''){
                        $.map(document.cookie.split(';'), function( cookie ){
                            if(reg.test(cookie = $.trim(cookie))) {
                                 $.cookie( cookie.substr(0,cookie.indexOf('=')), null, opts);
                            }
                        });
                    }
                }
            };

            if (typeof key !== "undefined") {
                return typeof value !== "undefined" ? ( value === null ? this.removeItem(key) : this.setItem(key, value) ) : this.getItem(key);
            }

            return this;
        };

        $[method].options = defaults;
    });
}(jQuery, window, document));




//Example

///* localStorage
//       ================= */
//var local = $('.local'),
//    set = $('.set'),
//    get = $('.get'),
//    del = $('.remove');

//set.find('input').keyup(function () {
//    $.localStorage(set.find('.key').val(), set.find('.value').val());
//    get.find('input').val(set.find('.key').val());
//    get.find('.result').text('// ' + $.localStorage(set.find('.key').val()))
//});
//get.find('.result').text('// ' + $.localStorage(set.find('.key').val()))

//get.find('input').keyup(function () {
//    get.find('.result').text('// ' + $.localStorage(get.find('input').val()))
//});

//del.find('button').click(function () {
//    var val = $.localStorage(del.find('input').val(), null);

//});

//var resizeInput = function () {
//    var self = $(this),
//        newWidth = self.val().length,
//        minWidth = self.attr('placeholder').length;
//    self.attr('size', newWidth > minWidth ? newWidth : minWidth);
//};

//$('input[type="text"]').on('keydown keyup', resizeInput).each(resizeInput);



///* sessionStorage
//================= */
//var session = $('.session');

//var boxRGB = $.data(this, 'rgb', $('#box').css('backgroundColor').match(/\d+/g)),
//    RGB = $.sessionStorage('rgb') || boxRGB;

//$('#box').css({
//    backgroundColor: 'rgb(' + RGB[0] + ',' + RGB[1] + ',' + RGB[2] + ')'
//});

//session.find('input').each(function (i) {
//    $(this).val(RGB[i]);
//});

//session.find('input').on('change keyup', function () {

//    $.sessionStorage('rgb', [
//        $('#r').val(),
//        $('#g').val(),
//        $('#b').val()
//    ]);

//    RGB = $.sessionStorage('rgb');

//    $('#box').css({
//        backgroundColor: 'rgb(' + RGB[0] + ',' + RGB[1] + ',' + RGB[2] + ')'
//    });
//});


//session.find('button').click(function () {
//    $.sessionStorage('rgb', null);
//    $('#box').css({
//        backgroundColor: 'rgb(' + boxRGB[0] + ',' + boxRGB[1] + ',' + boxRGB[2] + ')'
//    });
//    session.find('input').each(function (i) {
//        $(this).val(boxRGB[i]);
//    });
//});

//// Allow only rgb values as input
//session.find('input').keyup(function (e) {
//    var v = this.value,
//        s = v.slice(0, -1);
//    this.value = v.charAt(0) > 2 ? s : v.charAt(0) > 1 && v.charAt(1) > 5 ? s : v > 255 ? s : v.replace(/\D/g, ''); // /[^0-9\.]/g
//});
