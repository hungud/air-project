using System;
using System.Collections.Generic;
using App.Models;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace App.DataLayer.APIData
{
    public class ServiceProviderDataLayer
    {
        public ServiceProviderDataLayer()
        {
            //ServiceProviderDetails
        }

        public App.Common.CommonUtility GetServiceProviderDetails(App.Models.TravelModels.ServiceProviderInfo ReqInfo)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                if (ReqInfo != null)
                {
                    List<App.Models.TravelModels.APPServiceProviderData> ServiceProvider_Data = new List<App.Models.TravelModels.APPServiceProviderData>();
                    DataSet dsBankDetails = new DataSet();
                    Database db = DatabaseFactory.CreateDatabase();
                    using (DbCommand cmd = db.GetStoredProcCommand("SP_ServiceProviderDetails"))
                    {
                        db.AddInParameter(cmd, "@ServiceAPIType", DbType.String, ReqInfo.ServiceAPIType);
                        db.AddInParameter(cmd, "@ServiceFOR", DbType.String, ReqInfo.ServiceFOR);
                        db.AddInParameter(cmd, "@ActionType", DbType.String, ReqInfo.ActionType);
                        dsBankDetails = db.ExecuteDataSet(cmd);
                    }
                    if (dsBankDetails != null)
                    {
                        foreach (DataRow item in dsBankDetails.Tables[0].Rows)
                        {
                            ServiceProvider_Data.Add(new App.Models.TravelModels.APPServiceProviderData()
                            {
                                SPID = App.Common.AppConvert.ToInt32(item["SPID"]),
                                SFID = App.Common.AppConvert.ToInt32(item["SFID"]),
                                ServiceProviderName = item["ServiceProviderName"].ToString(),
                                ServiceProviderURL = item["ServiceProviderURL"].ToString(),
                                ServiceAPIType = item["ServiceAPIType"].ToString(),
                                ServiceFOR = item["ServiceFOR"].ToString(),
                                FunctionGroup = item["FunctionGroup"].ToString(),
                                FunctionName = item["FunctionName"].ToString(),
                                FunctionURL = item["FunctionURL"].ToString()
                            });
                        }
                    }
                    Utility = new Common.CommonUtility() { Data = ServiceProvider_Data, Status = false, Message = "Invalid Credential.", ErrorCode = "113" };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.AuthenticateUser", ex);
            }
            return Utility;
        }
    }
}
