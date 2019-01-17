var AppData;
function ReicivedPermitDetails(id, pinNo) {
    try {
        ConfirmBootBox("Error", "Successfully: ", 'App_Success', initialCallbackYes, initialCallbackNo);
    } catch (e) {
        console.log(e);
        HideWaitProgress();
    }
}
function OnLoadGetDataList() {
    try {
        function GetBootStrapPaginationTableList(DataTable_Data) {
            try {
                $.each(DataTable_Data, function (dt_key, dt_value) {
                    var DataAction = "<td><div class=' action-buttons myclick '><a href='javascript:void(0)' id=" + dt_value.ApplicationNo +
                        " onclick='ReicivedDetails(this.id ," + dt_value.Pin + ")' title='Project Detail'><i class='ace-icon fa fa-search-plus bigger-120'></i></a> </div></td>";
                    $('#BootStrapTableDataGridRespansive').dataTable().fnAddData([
                                                               nullEmptyToHyphen(dt_value.ApplicationNo),
                                                               nullEmptyToHyphen(dt_value.Pin),
                                                               nullEmptyToHyphen(dt_value.OFirstName),
                                                               nullEmptyToHyphen(dt_value.Area),
                                                               nullEmptyToHyphen(dt_value.Status),
                                                               nullEmptyToHyphen(dt_value.Date),
                                                               DataAction]);
                });
            }
            catch (e) {
                var error = e;
                error = e;
            }
        }
        function ResultCallBackSuccess(e, xhr, opts) {
            var App_Data = JSON.parse(e.Data).PricedItineraries;
            AppData = App_Data;
            var Data_Columns = [{ sTitle: "S.No", sWidth: "65px" }, { sTitle: "FROM", sWidth: "65px" }, { sTitle: "TO", sWidth: "65px" }, { sTitle: "Date", sWidth: "100px" }, { sTitle: "Price", sWidth: "100px" }, { sTitle: "Actions", sWidth: "50px" }];
            MasterRespansiveInitGenericDataTable("#Page-DataTable-result", 'BootStrapTableDataGridRespansive', null, Data_Columns);
            GetBootStrapPaginationTableList(App_Data, 'BootStrapTableDataGridRespansive');
            //SolutionDataTraveler('SET', 'pslistData', App_Data);
        }
        function ResultCallBackError(e, xhr, opts) {
            ConfirmBootBox("Error", "Error: ", 'App_Error', initialCallbackYes, initialCallbackNo);
        }
        //var DHCData = { UserId: sCurrentUserId, RoleId: sCurrentRole, Token: sCurrentToken, RoleId: sCurrentRole, AppNo: sCurrentAppid, Roles: sCurrentRoles };
        var DHCData = {  };
        var servicelocation = (!location.port.trim() ? location.href.replace("AppsTravel", "ConfigService") : location.origin.replace("11011", "11012/"));
        //servicelocation = "http://airservice.nanojot.com/";
        MasterAppConfigurationsServices("POST", servicelocation + "API/APIService", JSON.stringify(DHCData), ResultCallBackSuccess, ResultCallBackError);
    } catch (e) {
        var error = e;
        error = error;
    }
}
$(document).ready(function () {
    //OnLoadGetDataList();
});
