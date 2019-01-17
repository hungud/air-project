jQuery(document).ready(function () {
    alert('');
    //getUserAppRoles();

});
var userAPPRoles = [];

function getUserAppRoles() {
    try {
        jQuery.ajax({
            url: "http://localhost:54841/api/DashboardApi",
            type: "GET",
            contentType: "application/json",
            success: function (data) {
                userAPPRoles = data;
            },
            error: function (jqXHR, textStatus, err) {
                hideLoading();
                //jQuery('#product').text('Error: ' + err);
            }
        });
        $.ajax({
            url: "http://localhost:54841/api/DashboardApi",
            type: "GET",
            contentType: "application/json",
            success: function (data) {
                userAPPRoles = data;
            },
            error: function (jqXHR, textStatus, err) {
                hideLoading();
                //jQuery('#product').text('Error: ' + err);
            }
        });
    }
    catch (e) { }
}
