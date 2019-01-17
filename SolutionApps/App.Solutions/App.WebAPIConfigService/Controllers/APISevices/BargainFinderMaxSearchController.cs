using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Text;
namespace App.WebAPIConfigService.Controllers
{
    public class BargainFinderMaxSearchController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBFMXSearch")]
        public HttpResponseMessage GetDashboardData(App.Models.Models UserInfo)
        {
        
            string jsonData = string.Empty;
            App.Common.CommonUtility Utility = new App.Common.CommonHttpClientWebClientHttpWebRequestCommunication().GetAsync("", "");
            jsonData = new JavaScriptSerializer().Serialize(Utility);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ActionName("PostBFMXSearch")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> PostDashboardData(App.Models.SabreModels.SabreReqstResourceModels ReqstResource_BFMXRQ_RequestContent)
         {
            HttpResponseMessage response = new HttpResponseMessage();

            if ((ReqstResource_BFMXRQ_RequestContent.CompanyTypeId == 1) && (ReqstResource_BFMXRQ_RequestContent.IsAuthenticated=="False")) {
                response.Content = new StringContent("Authentication Required");
                return response;
            }
            
            string jsonData = string.Empty, ServiceURL = string.Empty;
            try
            {
                Base.ErrorsLog.ErrorsLogInstance.RequestID = Base.ErrorsLog.RandomString();
                if (ReqstResource_BFMXRQ_RequestContent != null)
                {
                    //jsonData = System.Configuration.ConfigurationManager.AppSettings["PseudoCityCode"].ToString();
                    jsonData = ReqstResource_BFMXRQ_RequestContent.PCC;
                    ServiceURL = System.Configuration.ConfigurationManager.AppSettings["SareServiceURL"].ToString();
                    ReqstResource_BFMXRQ_RequestContent.BFMXRQ_RequestContent = ReqstResource_BFMXRQ_RequestContent.BFMXRQ_RequestContent.Replace("#####", jsonData);
                    App.Common.CommonUtility Utility = new App.Common.CommonHttpClientWebClientHttpWebRequestCommunication().PostAsync(ServiceURL+ "?mode=live&limit=50&offset=1", ReqstResource_BFMXRQ_RequestContent.BFMXRQ_RequestContent, Base.ErrorsLog.ErrorsLogInstance.RequestID,ReqstResource_BFMXRQ_RequestContent.ClientID, ReqstResource_BFMXRQ_RequestContent.ClientSecret);


                    jsonData = new JavaScriptSerializer().Serialize(Utility);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    string RequestID = Utility.Data1;
                    /* Save Searched Data in Database */
                    App.BusinessLayer.BusinessLayer save_data = new App.BusinessLayer.BusinessLayer();
                    await save_data.TrackSearchRecords(ReqstResource_BFMXRQ_RequestContent, RequestID);
                }
                else
                {
                    response = this.Request.CreateResponse(HttpStatusCode.NotFound, jsonData);
                }
                
            }
            catch (Exception ex)
            {
                Base.ErrorsLog.ErrorsLogInstance.REQRESException(ex);
            }
            finally {
                Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageREST(new JavaScriptSerializer().Serialize(ReqstResource_BFMXRQ_RequestContent).ToString(),"QuoteRQ");
                Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageREST(jsonData.ToString(), "QuoteRS");
            }

            

            return response;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("PutBFMXSearch")]
        public HttpResponseMessage PutDashboardData(App.Models.Models UserInfo)
        {
            string jsonData = string.Empty;
            if (UserInfo != null)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonHttpClientWebClientHttpWebRequestCommunication().PutAsync("", "");
                jsonData = new JavaScriptSerializer().Serialize(Utility);
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, jsonData);
            }
        }
        [HttpDelete]
        [ActionName("DeleteBFMXSearch")]
        public HttpResponseMessage DeleteDashboardData(App.Models.Models UserInfo)
        {
            string jsonData = string.Empty;
            if (UserInfo != null)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonHttpClientWebClientHttpWebRequestCommunication().DeleteAsync("", "");
                jsonData = new JavaScriptSerializer().Serialize(Utility);
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, jsonData);
            }
        }
    }
}