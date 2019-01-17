function ReicivedDetails(id, ProductID) {
    try {
        ConfirmBootBox("Successfully", "Successfully Fatched ID : " + ProductID, 'App_Success', initialCallbackYes, initialCallbackNo);
    } catch (e) {
        HideWaitProgress();
    }
}
function OnLoadGetDataList() {
    try {
        function GetJSONList(DataTable_Data) {
            MasterRespansiveJSONDataManagement("#Page-josn-result", DataTable_Data);
        }
        function GetBootStrapPaginationTableList(DataTable_Data) {
            try {
                $.each(DataTable_Data, function (dt_key, dt_value) {
                    var DataAction = "<td><div class=' action-buttons myclick '><a href='javascript:void(0)' id=" + dt_value.ProductID +
                        " onclick='ReicivedDetails(this.id ," + dt_value.ProductID + ")' title='Project Detail'><i class='ace-icon fa fa-search-plus bigger-120'></i></a> </div></td>";
                    $('#BootStrapTableDataGridRespansive').dataTable().fnAddData([
                                                               nullEmptyToHyphen(dt_value.ProductID),
                                                               nullEmptyToHyphen(dt_value.Name),
                                                               nullEmptyToHyphen(dt_value.ProductNumber),
                                                               nullEmptyToHyphen(dt_value.MakeFlag),
                                                               nullEmptyToHyphen(dt_value.Color),
                                                               nullEmptyToHyphen(dt_value.WeightUnitMeasureCode),
                                                               nullEmptyToHyphen(dt_value.Weight),
                                                               DataAction]);
                });

            }
            catch (e) {
                var error = e;
                error = e;
            }
        }
        function ResultCallBackSuccess(e, xhr, opts) {
            $(".SearchJSONResult").removeClass("hide");
            $(".SearchResult").removeClass("hide");
            var App_Data = e.Data;
            var AppData = JSON.parse(e.Data1);
            var Data_Columns = [{ sTitle: "ProductID", sWidth: "65px" }, { sTitle: "Name", sWidth: "65px" }, { sTitle: "ProductNumber", sWidth: "65px" }, { sTitle: "MakeFlag", sWidth: "100px" },
                { sTitle: "Color", sWidth: "100px" }, { sTitle: "WeightUnitMeasureCode", sWidth: "50px" }, { sTitle: "Weight", sWidth: "50px" }, { sTitle: "Actions", sWidth: "50px" }];
            MasterRespansiveInitGenericDataTable("#Page-DataTable-result", 'BootStrapTableDataGridRespansive', App_Data, Data_Columns);
            GetBootStrapPaginationTableList(App_Data, 'BootStrapTableDataGridRespansive');
            GetJSONList(App_Data);
        }
        function ResultCallBackError(e, xhr, opts) {
            ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
        }
        var ReqURL = "";
        //var DHCData = { UserId: sCurrentUserId, RoleId: sCurrentRole, Token: sCurrentToken, RoleId: sCurrentRole, AppNo: sCurrentAppid, Roles: sCurrentRoles };
        var DHCData = { ProductID: $('#ProductID').val() };
        var servicelocation = (!location.port.trim() ? location.href.replace("AppsTravel", "ConfigService") : location.origin.replace("11011", "11012/"));
        //servicelocation = "http://airservice.nanojot.com/";
        MasterAppConfigurationsServices("POST", servicelocation + "API/AdventureWorksProductionProduct", JSON.stringify(DHCData), ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var error = e;
        error = error;
    }
}
$(document).ready(function () {
    //$(".SearchJSONResult").addClass("hide");
    //$(".SearchResult").addClass("hide");   
    $(".btn-success").on('click', function () {
        ShowWaitProgress();
        if (ValidateData()) {
            OnLoadGetDataList();
        }
    });
});

function ValidateData() {
    if ($('#ProductID').val().trim() == "") {
        ConfirmBootBox("Message", "Please Enter Product ID", 'App_Warning', initialCallbackYes, initialCallbackNo);
        $("#ProductID").focus();
        HideWaitProgress();
        return false;
    }
    return true;
}
