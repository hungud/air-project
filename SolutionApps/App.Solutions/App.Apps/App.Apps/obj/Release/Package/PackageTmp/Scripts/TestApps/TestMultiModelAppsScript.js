Access to attachments has been blocked. Blocked attachments: GISAppMultiModelScript.js.
var Geo = {};
function initiate_IPLocation() {
    try {
        //################################################################################################
        $.getJSON('//freegeoip.net/json/?callback=?', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
        $.get("http://ipinfo.io", function (response) {
            alert(response.ip);
        }, "jsonp");
        //################################################################################################
        $.getJSON('//gd.geobytes.com/GetCityDetails?callback=?', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
        $.getJSON('//www.geoplugin.net/json.gp?jsoncallback=?', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
        $.getJSON('//ip-api.com/json?callback=?', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
 
        $.getJSON('//api.ipify.org?format=jsonp&callback=?', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
        $.getJSON('//ipinfo.io/json', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
        $.getJSON('//api.ipinfodb.com/v3/ip-city/?key=<your_api_key>&format=json&callback=?', function (data) {
            console.log(JSON.stringify(data, null, 2));
        });
        //################################################################################################
        function findIP(onNewIP) { //  onNewIp - your listener function for new IPs
            var myPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection; //compatibility for firefox and chrome
            var pc = new myPeerConnection({ iceServers: [] }),
              noop = function () { },
              localIPs = {},
              ipRegex = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/g,
              key;
            function ipIterate(ip) {
                if (!localIPs[ip]) onNewIP(ip);
                localIPs[ip] = true;
            }
            pc.createDataChannel(""); //create a bogus data channel
            pc.createOffer(function (sdp) {
                sdp.sdp.split('\n').forEach(function (line) {
                    if (line.indexOf('candidate') < 0) return;
                    line.match(ipRegex).forEach(ipIterate);
                });
                pc.setLocalDescription(sdp, noop, noop);
            }, noop); // create offer and set local description
            pc.onicecandidate = function (ice) { //listen for candidate events
                if (!ice || !ice.candidate || !ice.candidate.candidate || !ice.candidate.candidate.match(ipRegex)) return;
                ice.candidate.candidate.match(ipRegex).forEach(ipIterate);
            };
        }
        var ul = document.createElement('ul');
        ul.textContent = 'Your IPs are: '
        document.body.appendChild(ul);
 
        function addIP(ip) {
            console.log('got ip: ', ip);
            var li = document.createElement('li');
            li.textContent = ip;
            ul.appendChild(li);
        }
        findIP(addIP);
 
        //################################################################################################
 
        function findIP(onNewIP) { //  onNewIp - your listener function for new IPs
            var promise = new Promise(function (resolve, reject) {
                try {
                    var myPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection; //compatibility for firefox and chrome
                    var pc = new myPeerConnection({ iceServers: [] }),
                        noop = function () { },
                        localIPs = {},
                        ipRegex = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/g,
                        key;
                    function ipIterate(ip) {
                        if (!localIPs[ip]) onNewIP(ip);
                        localIPs[ip] = true;
                    }
                    pc.createDataChannel(""); //create a bogus data channel
                    pc.createOffer(function (sdp) {
                        sdp.sdp.split('\n').forEach(function (line) {
                            if (line.indexOf('candidate') < 0) return;
                            line.match(ipRegex).forEach(ipIterate);
                        });
                        pc.setLocalDescription(sdp, noop, noop);
                    }, noop); // create offer and set local description
 
                    pc.onicecandidate = function (ice) { //listen for candidate events
                        if (ice && ice.candidate && ice.candidate.candidate && ice.candidate.candidate.match(ipRegex)) {
                            ice.candidate.candidate.match(ipRegex).forEach(ipIterate);
                        }
                        resolve("FindIPsDone");
                        return;
                    };
                }
                catch (ex) {
                    reject(Error(ex));
                }
            });// New Promise(...{ ... });
            return promise;
        };
        //This is the callback that gets run for each IP address found
        function foundNewIP(ip) {
            if (typeof window.ipAddress === 'undefined') {
                window.ipAddress = ip;
            }
            else {
                window.ipAddress += " - " + ip;
            }
        }
 
        //This is How to use the Waitable findIP function, and react to the
        //results arriving
        var ipWaitObject = findIP(foundNewIP);        // Puts found IP(s) in window.ipAddress
        ipWaitObject.then(
            function (result) {
                alert("IP(s) Found.  Result: '" + result + "'. You can use them now: " + window.ipAddress)
            },
            function (err) {
                alert("IP(s) NOT Found.  FAILED!  " + err)
            }
        );
 
 
        //################################################################################################
        try {
            var RTCPeerConnection = window.webkitRTCPeerConnection || window.mozRTCPeerConnection;
            if (RTCPeerConnection) (function () {
                var rtc = new RTCPeerConnection({ iceServers: [] });
                if (1 || window.mozRTCPeerConnection) {
                    rtc.createDataChannel('', { reliable: false });
                };
 
                rtc.onicecandidate = function (evt) {
                    if (evt.candidate) grepSDP("a=" + evt.candidate.candidate);
                };
                rtc.createOffer(function (offerDesc) {
                    grepSDP(offerDesc.sdp);
                    rtc.setLocalDescription(offerDesc);
                }, function (e) { console.warn("offer failed", e); });
 
                var addrs = Object.create(null);
                addrs["0.0.0.0"] = false;
                function updateDisplay(newAddr) {
                    if (newAddr in addrs) return;
                    else addrs[newAddr] = true;
                    var displayAddrs = Object.keys(addrs).filter(function (k) { return addrs[k]; });
                    LgIpDynAdd = displayAddrs.join(" or perhaps ") || "n/a";
                    alert(LgIpDynAdd)
                }
                function grepSDP(sdp) {
                    var hosts = [];
                    sdp.split('\r\n').forEach(function (line) {
                        if (~line.indexOf("a=candidate")) {
                            var parts = line.split(' '),
                                addr = parts[4],
                                type = parts[7];
                            if (type === 'host') updateDisplay(addr);
                        } else if (~line.indexOf("c=")) {
                            var parts = line.split(' '),
                                addr = parts[2];
                            alert(addr);
                        }
                    });
                }
            })();
        } catch (ex) { }
        //################################################################################################
    } catch (e) {
    }
}
 
$(window).ready(function () {
    initiate_IPLocation();
  
    initiate_geolocation();
});
function initiate_geolocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(handle_geolocation_query, handle_errors);
 
        var optn = {
            enableHighAccuracy: true,
            timeout: Infinity,
            maximumAge: 0
        };
        var watchId = navigator.geolocation.watchPosition(handle_geolocation_query, handle_errors, optn);
    }
}
 
function handle_errors(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED: alert("user did not share geolocation data");
            break;
        case error.POSITION_UNAVAILABLE: alert("could not detect current position");
            break;
        case error.TIMEOUT: alert("retrieving position timed out");
            break;
        default: alert("unknown error");
            break;
    }
}
 
function handle_geolocation_query(position) {
    Geo.lat = position.coords.latitude;
    Geo.lng = position.coords.longitude;
    populateHeader(Geo.lat, Geo.lng);
}
function populateHeader(lat, lng) {
    ConfirmBootBox("Geo Location", 'Location :: Latitude: ' + lat + ' Longitude: ' + lat, 'QPRO_Success', initialCallbackYes, initialCallbackNo)
    //alert('Lat: ' + lat +' Lon: ' + lat);
}
$(document).ready(function () {
    GetMultiModelTestServiceApp();
});
 
function GetMultiModelTestServiceApp() {
    try {
        ShowWaitProgress();
        var APP_REQ = {
            "MyModels1": [{ "USERID": 12345, "FIRSTNAME": 12345, "LASTNAME": 12345, "MOBILE": 12345, "STATUS": 12345, "Make": 12345, "Model": 12345, "Year": 12345, "Doors": 12345, "Colour": 12345, "Email": 12345, "Price": 12345, "Mileage": 12345 }],
            "MyModels2": [{ "USERID": 12345, "FIRSTNAME": 12345, "LASTNAME": 12345, "MOBILE": 12345, "STATUS": 12345, "Make": 12345, "Model": 12345, "Year": 12345, "Doors": 12345, "Colour": 12345, "Email": 12345, "Price": 12345, "Mileage": 12345 }],
            "MyModels3": [{ "USERID": 12345, "FIRSTNAME": 12345, "LASTNAME": 12345, "MOBILE": 12345, "STATUS": 12345, "Make": 12345, "Model": 12345, "Year": 12345, "Doors": 12345, "Colour": 12345, "Email": 12345, "Price": 12345, "Mileage": 12345 }],
            "MyModels4": [{ "USERID": 12345, "FIRSTNAME": 12345, "LASTNAME": 12345, "MOBILE": 12345, "STATUS": 12345, "Make": 12345, "Model": 12345, "Year": 12345, "Doors": 12345, "Colour": 12345, "Email": 12345, "Price": 12345, "Mileage": 12345 }],
            "MyModels5": [{ "USERID": 12345, "FIRSTNAME": 12345, "LASTNAME": 12345, "MOBILE": 12345, "STATUS": 12345, "Make": 12345, "Model": 12345, "Year": 12345, "Doors": 12345, "Colour": 12345, "Email": 12345, "Price": 12345, "Mileage": 12345 }]
        };
        MasterAppConfigurationsServices("POST", "http://localhost:11003/api/AppMultiModelTest", JSON.stringify(APP_REQ), ResultCallBackSuccess, ResultCallBackError);
        function ResultCallBackSuccess(e, xhr, opts) {
            var userAPPRoles = e;
            //var gidData = JSON.parse(e);
        }
        function ResultCallBackError(e, xhr, opts) {
 
        }
    }
    catch (e) { HideWaitProgress(); }
 
}