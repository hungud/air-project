/*******************************************************
********************************************************
***Function      : Common DialogBox *********************
***Developed By  : Rakesh Pal **************************
***Developed ON  : 04/09/2015*** ***********************
********************************************************
********************************************************/


//
$(document).ready(function () { initializeConfirmPopupBox(); });
function initializeConfirmPopupBox() {
    var html = '<div id="CommonDialogBox"><div id="dialog-confirm" class="custom-dialog-box" title="Empty the recycle bin?"><p style="min-height: 75px; color:black;" id="confirmPopupText">Dialog Box</p> <hr /><div style="float: right;"> <button id="btnConfirm">OK</button> <button id="btnCancel">Cancel</button></div></div></div>';
    $("body").append(html);
    $("#CommonDialogBox").hide();

}
//
function initConfirmPopup() {
    $("#dialog-confirm").dialog({ autoOpen: false, resizable: false, height: 140, modal: true });
}
//
function ConfirmBoxPopUp(title, text, CallbackYes, CallbackNo, AlertConfirm) {
    try {
        AlertConfirm = typeof AlertConfirm !== 'undefined' ? jQuery('#btnCancel').hide() : jQuery('#btnCancel').show();
        $('#dialog-confirm').dialog('option', 'title', title);
        $("#confirmPopupText").html(text);
        $("#dialog-confirm").dialog("open");
        $('#btnConfirm').off('click');
        $('#btnCancel').off('click');
        $('#btnConfirm').click(function () {
            $("#dialog-confirm").dialog('close');
            if (CallbackYes) {
                CallbackYes();
            }
        });
        jQuery('#btnCancel').click(function () {
            $("#dialog-confirm").dialog('close');
            if (CallbackNo) {
                CallbackNo();
            }
        });
    }
    catch (e) {
        var ex = e;
        ex = e;
    }
}
//
function initialCallbackYes() {
    //alert('Yes');
}
//
function initialCallbackNo() {
    //alert('No');
}
//
function ConfirmBootBoxPopUp(title, text, CallbackYes, CallbackNo, AlertConfirm) {
    try {
        AlertConfirm = typeof AlertConfirm !== 'undefined' ? jQuery('#btnCancel').hide() : jQuery('#btnCancel').show();
        $('#dialog-confirm').dialog('option', 'title', title);
        $("#confirmPopupText").html(text);
        $("#dialog-confirm").dialog("open");
        $('#btnConfirm').off('click');
        $('#btnCancel').off('click');
        bootbox.dialog({
            message: text,
            buttons: {
                "success": { "label": "OK", "className": "btn-sm btn-primary" },
                button: { "label": "Cancel", "className": "btn-sm btn-danger" }
            }
        });

        $('.btn-primary').click(function () {
            $("#dialog-confirm").dialog('close');
            if (CallbackYes) {
                CallbackYes();
            }
        });
        jQuery('.btn-danger').click(function () {
            $("#dialog-confirm").dialog('close');
            if (CallbackNo) {
                CallbackNo();
            }
        });
    }
    catch (e) {
        var ex = e;
        ex = e;
    }
}
//
function ConfirmBootBox(Title, Message, ActionKeyWord, CallbackYes, CallbackNo) {
    try {
        if (ActionKeyWord != '') {
            switch (ActionKeyWord) {
                case "Info":
                    bootbox.alert(Message, function (result) {
                        if (CallbackYes) {
                            CallbackYes();
                        }
                    });
                    break;
                case "Confirm":
                    bootbox.confirm(Title, function (result) {
                        var action = result;
                        if (result) {
                            if (CallbackYes) {
                                CallbackYes();
                            }
                        }
                        else {
                            if (CallbackNo) {
                                CallbackNo();
                            }
                        }
                    });
                    break;
                case "Prompt":
                    bootbox.prompt(Title, function (result) {
                        if (result === null) {
                            ConfirmBootBox("Message", "Cancel or Dismissed", 'Info', initialCallbackYes, initialCallbackNo);
                        } else {
                            ConfirmBootBox("Message", result, 'Info', initialCallbackYes, initialCallbackNo);
                        }
                    });
                    break;
                case "CustomDialog":
                    bootbox.dialog({
                        message: Message,
                        title: Title,
                        buttons: {
                            success: {
                                label: "Success!",
                                className: "btn-success",
                                callback: function (result) {
                                    if (CallbackYes) {
                                        CallbackYes();
                                    }
                                }
                            },
                            danger: {
                                label: "Danger!",
                                className: "btn-danger",
                                callback: function (result) {
                                    if (CallbackNo) {
                                        CallbackNo();
                                    }
                                }
                            },
                            main: {
                                label: "Click ME!",
                                className: "btn-primary",
                                callback: function (result) {
                                    if (CallbackNo) {
                                        CallbackNo();
                                    }
                                }
                            }
                        }
                    });
                    break;
                case "AlertOKCancel":
                    bootbox.dialog({
                        title: Title,
                        message: Message,
                        buttons: {
                            success: {
                                label: "OK",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackYes) {
                                        CallbackYes();
                                    }
                                }
                            },
                            main: {
                                label: "Cancel",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackNo) {
                                        CallbackNo();
                                    }
                                }
                            }
                        }
                    });
                    break;
                case "AlertOK":
                    bootbox.dialog({
                        title: Title,
                        message: Message,
                        buttons: {
                            success: {
                                label: "OK",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackYes) {
                                        CallbackYes();
                                    }
                                }
                            }
                        }
                    });
                    break;
                case "AlertCancel":
                    bootbox.dialog({
                        title: Title,
                        message: Message,
                        buttons: {
                            main: {
                                label: "Cancel",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackNo) {
                                        CallbackNo();
                                    }
                                }
                            }
                        }
                    });
                    break;
                case "Custom1":
                    bootbox.dialog({
                        title: Title,
                        message: Message,
                        buttons: {
                            success: {
                                label: "Save",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackYes) {
                                        CallbackYes();
                                    }
                                    //ConfirmBootBox("Message", "Cancel or Dismissed", 'Info', initialCallbackYes, initialCallbackNo);
                                }
                            }
                        }
                    }
                     );
                    break;
                case "Custom2":
                    bootbox.dialog({
                        title: Title,
                        message: Message,
                        buttons: {
                            success: {
                                label: "Save",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackYes) {
                                        CallbackYes();
                                    }
                                    //ConfirmBootBox("Message", "Cancel or Dismissed", 'Info', initialCallbackYes, initialCallbackNo);
                                }
                            }
                        }
                    }
                     );
                    break;
                case "Custom3":
                    bootbox.dialog({
                        title: Title,
                        message: Message,
                        buttons: {
                            success: {
                                label: "Save",
                                className: "btn btn-pink btn-xs btn-round",
                                callback: function (result) {
                                    if (CallbackYes) {
                                        CallbackYes();
                                    }
                                    //ConfirmBootBox("Message", "Cancel or Dismissed", 'Info', initialCallbackYes, initialCallbackNo);
                                }
                            }
                        }
                    }
                     );
                    break;
                case "CustomForms":
                    //var bootloginform = "<form id='loginForm' method='post' class='form-horizontal' style='isplay: none;'><div class='form-group'><label class='col-xs-3 control-label'>Username</label><div class='col-xs-5'><input type='text' class='form-control' name='username' /></div></div><div class='form-group'><label class='col-xs-3 control-label'>Password</label><div class='col-xs-5'><input type='password' class='form-control' name='password' /></div></div><div class='form-group'><div class='col-xs-5 col-xs-offset-3'><button type='submit' class='btn btn-default'>Login</button></div></div></form>";
                    //$("body").append(bootloginform);
                    //bootbox.dialog({
                    //title: 'Login',
                    //message: $('#loginForm'),
                    //show: false // We will show it manually later
                    //    })
                    //    .on('shown.bs.modal', function() {
                    //        $('#loginForm').show();                             // Show the login form
                    //        //$('#loginForm').show().formValidation('resetForm', true); // Reset form
                    //    })
                    //    .on('hide.bs.modal', function(e) {
                    //        // Bootbox will remove the modal (including the body which contains the login form)
                    //        // after hiding the modal
                    //        // Therefor, we need to backup the form
                    //        $('#loginForm').hide().appendTo('body');
                    //    })
                    //    .modal('show');
                    //$("body").remove('#loginForm');
                    break;
                case "App_Success":
                    notif({
                        msg: Message,
                        type: "Notify_Success",
                        position: "center"
                    });
                    break;
                case "App_Error":
                    notif({
                        msg: Message,
                        type: "Notify_Error",
                        position: "center"
                    });
                    break;
                case "App_Warning":
                    notif({
                        msg: Message,
                        type: "Notify_Warning",
                        position: "center"
                    });
                    break;
                case "App_Info":
                    notif({
                        msg: Message,
                        type: "Notify_Info",
                        position: "center"
                    });
                    break;
                default:
                    bootbox.alert("Hello world!", function (result) {
                        if (CallbackYes) {
                            CallbackYes();
                        }
                    });
            }
        }
    }
    catch (e) {
        var ex = e;
        ex = e;
    }
}

