var to, width, height, position, autohide, opacity, time;

function qpro_notify_setDefaultValues() {
    width = 500;
    //width = '100%'; //400
    height = 45; //60
    position = "right";
    autohide = true;
    msg = "";
    opacity = 1; // Default opacity (Only Chrome, Firefox and Safari)
    multiline = false;
    fade = false;
    bgcolor = "#444";
    color = "#EEE";
    time = 5000;
}
function notif(config) {
    qpro_notify_setDefaultValues();
    if (config.position) {
        if (config.position == "center" ||
            config.position == "left" ||
            config.position == "right") {
            position = config.position; // Take the position
        }
    }
    if (config.width) {
        if (config.width > 0) {
            width = config.width; // Take the width in pixels
        } else if (config.width === "all") {
            width = screen.width - 60; // Margin difference
        }
    }
    if (config.fade) { fade = config.fade; }
    if (config.multiline) { multiline = config.multiline; }
    if (config.height) {
        if (config.height < 100 && config.height > 0) {
            height = config.height; // Take the height in pixels
        }
    }
    if (typeof config.autohide !== "undefined")
        autohide = config.autohide;
    var div = "<div id='ui_qpro_notify'><p>" + ((config.msg) ? config.msg : "") + "</p></div>";
    $("#ui_qpro_notify").remove();// Preventive remove
    clearInterval(to); // Preventive clearInterval
    $("body").append(div);
    if (multiline) {
        $("#ui_qpro_notify").css("padding", 15);
    } else {
        $("#ui_qpro_notify").css("height", height);
        //$("#ui_qpro_notify p").css("line-height", height + "px");
        $("#ui_qpro_notify p").css("line-height:auto");
    }
    $("#ui_qpro_notify").css("width", width);
    switch (position) {
        case "center":
            $("#ui_qpro_notify").css("top", parseInt(0 - (height + 10)));
            break;
        case "right":
            $("#ui_qpro_notify").css("right", parseInt(0 - (width + 10)));
            break;
        case "left":
            $("#ui_qpro_notify").css("left", parseInt(0 - (width + 10)));
            break;
        default:
            $("#ui_qpro_notify").css("right", parseInt(0 - (width + 10)));
            break;
    }
    if (config.opacity) { $("#ui_qpro_notify").css("opacity", config.opacity); }
    switch (config.type) {
        case "Notify_Error":
            $("#ui_qpro_notify").addClass("Notify_Error");
            break;
        case "Notify_Success":
            $("#ui_qpro_notify").addClass("Notify_Success");
            break;
        case "Notify_Info":
            $("#ui_qpro_notify").addClass("Notify_Info");
            break;
        case "Notify_Warning":
            $("#ui_qpro_notify").addClass("Notify_Warning");
            break;
        default:
            $("#ui_qpro_notify").addClass("default");
            break;
    }
    // Override color if given
    if (config.bgcolor) {
        $("#ui_qpro_notify").css("background-color", config.bgcolor);
    }
    if (config.color) {
        $("#ui_qpro_notify").css("color", config.color);
    }
    switch (position) {
        case "left":
            $("#ui_qpro_notify").css("left", parseInt(0 - (width * 2)));
            break;
        case "right":
            $("#ui_qpro_notify").css("right", parseInt(0 - (width * 2)));
            break;
        case "center":
            var mid = window.innerWidth / 2;
            $("#ui_qpro_notify").css("left", mid - parseInt(width / 2));
            break;
        default:
            var mid = window.innerWidth / 2;
            $("#ui_qpro_notify").css("left", mid - parseInt(width / 2));
            break;
    }
    switch (position) {
        case "center":
            $("#ui_qpro_notify").animate({ top: 0 });
            break;
        case "right":
            $("#ui_qpro_notify").animate({ right: 10 });
            break;
        case "left":
            $("#ui_qpro_notify").animate({ left: 10 });
            break;
        default:
            $("#ui_qpro_notify").animate({ right: 10 });
            break;
    }
    $("#ui_qpro_notify").click(function () {
        qpro_notify_dismiss();
    });
    if (autohide) {
        if (config.timeout) {
            if (!isNaN(config.timeout)) {
                time = config.timeout;
            }
        }
        to = setTimeout(function () { qpro_notify_dismiss(); }, time);
    }
}

function qpro_notify_dismiss() {
    clearInterval(to);
    if (!fade) {
        if (position == "center") {
            $("#ui_qpro_notify").animate({
                top: parseInt(height - (height / 2))
            }, 100, function () {
                $("#ui_qpro_notify").animate({
                    top: parseInt(0 - (height * 2))
                }, 100, function () {
                    $("#ui_qpro_notify").remove();
                });
            });
        } else if (position == "right") {
            $("#ui_qpro_notify").animate({
                right: parseFloat(width - (width * 0.9))
            }, 100, function () {
                $("#ui_qpro_notify").animate({
                    right: parseInt(0 - (width * 2))
                }, 100, function () {
                    $("#ui_qpro_notify").remove();
                });
            });
        } else if (position == "left") {
            $("#ui_qpro_notify").animate({
                left: parseFloat(width - (width * 0.9))
            }, 100, function () {
                $("#ui_qpro_notify").animate({
                    left: parseInt(0 - (width * 2))
                }, 100, function () {
                    $("#ui_qpro_notify").remove();
                });
            });
        }
    } else {
        $("#ui_qpro_notify").fadeOut("slow", function () {
            $("#ui_qpro_notify").remove();
        });
    }
    qpro_notify_setDefaultValues();
}
