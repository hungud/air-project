using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLayer.APIData
{
    public class ServiceProviderBusinessLayer
    {
        public ServiceProviderBusinessLayer()
        {

        }

        public App.Common.CommonUtility GetServiceProviderDetails(App.Models.TravelModels.ServiceProviderInfo ReqInfo)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                if (ReqInfo != null)
                {
                    Utility = new App.DataLayer.APIData.ServiceProviderDataLayer().GetServiceProviderDetails(ReqInfo);
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
