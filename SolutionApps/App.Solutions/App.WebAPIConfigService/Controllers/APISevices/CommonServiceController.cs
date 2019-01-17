using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.Http.Cors;
using register_functionlity.DB.Service;
using register_functionlity.DB.Model;

namespace App.WebAPIConfigService.Controllers
{
    public class CommonServiceController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetCommonService")]
        public HttpResponseMessage GetCommonService(string CommonServiceType, string SearchText)
        {
            string jsonData = string.Empty;
            if (!string.IsNullOrEmpty(CommonServiceType) && !string.IsNullOrEmpty(SearchText))
            {
                try
                {
                    Common.CommonUtility Utility = new App.BusinessLayer.BusinessLayer().GetCommonService(CommonServiceType, SearchText);
                    jsonData = new JavaScriptSerializer().Serialize(Utility);
                    HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    return response;
                }
                catch (Exception ex)
                {
                    App.Base.ErrorsLog.ErrorsLogInstance.ManageException(ex);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonData);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, jsonData);
            }
        }

        [HttpGet]
        [ActionName("GetMarkupByDomain")]
        public App.BusinessLayer.MarkUp GetMarkupByDomain(string domain)
        {
            App.BusinessLayer.MarkUp jsonData = new BusinessLayer.MarkUp();

            try
            {
                jsonData = new App.BusinessLayer.BusinessLayer().GetMarkupByDomain(domain);

            }
            catch (Exception ex)
            {
                App.Base.ErrorsLog.ErrorsLogInstance.ManageException(ex);
            }
            return jsonData;
        }

        [HttpGet]
        [ActionName("GetBlockedAirlines")]
        public List<BlockAirlineModel> GetBlockedAirlines(string domain, int CompanyTypeId)
        {
            try
            {
                return new MISCServices().GetBlockedAirlines(domain, CompanyTypeId);
            }
            catch (Exception ex)
            {
                App.Base.ErrorsLog.ErrorsLogInstance.ManageException(ex);
            }
            return new List<BlockAirlineModel>();
        }

        [HttpGet]
        [ActionName("GetQuoteRQRSService")]
        public HttpResponseMessage GetQuoteRQRSService(string folderName, string SearchText)
        {
            string jsonData = string.Empty;
            if (!string.IsNullOrEmpty(folderName) && !string.IsNullOrEmpty(SearchText))
            {
                try
                {
                    string Utility = new App.BusinessLayer.BusinessLayer().GetQuoteSONFile(folderName, SearchText);
                    if (SearchText == "QuoteRQ.log")
                        jsonData = new JavaScriptSerializer().Serialize(Utility);
                    else
                        jsonData = Utility;

                    HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    return response;
                }
                catch (Exception ex)
                {
                    App.Base.ErrorsLog.ErrorsLogInstance.ManageException(ex);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, jsonData);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, jsonData);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("PostCommonService")]
        public HttpResponseMessage PostCommonService(App.Models.SabreModels.SabreReqstResourceModels ReqstResource)
        {
            string jsonData = string.Empty;
            if (ReqstResource != null)
            {
                Common.CommonUtility Utility = new Common.CommonUtility();
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



        [HttpGet]
        [ActionName("GetpaymentOptionsByTypeId")]
        public List<App.Models.PaymentModal> GetpaymentOptions(int CompanyTypeId, Int64 CompanyId)
        {
            List<App.Models.PaymentModal> paymentOptions = new List<App.Models.PaymentModal>();
            try
            {
                using (register_functionlity.DB.Data.AirAdminDBEntities db = new register_functionlity.DB.Data.AirAdminDBEntities())
                {
                    paymentOptions.Add(
                          new App.Models.PaymentModal
                          {
                              Id = 0,
                              CardType = "VISA",
                              CardValue = "VI"
                          });
                    paymentOptions.Add(
                          new App.Models.PaymentModal
                          {
                              Id = 1,
                              CardType = "MASTER CARD",
                              CardValue = "CA"
                          });
                    paymentOptions.Add(
                       new App.Models.PaymentModal
                       {
                           Id = 2,
                           CardType = "AMERICAN EXPRESS",
                           CardValue = "AX"
                       });

                    if (CompanyTypeId == 2)
                    {
                        var companyDetail = db.CompanyDetails.Where(k => k.Id == CompanyId).FirstOrDefault();
                        if (companyDetail != null)
                        {
                            CompanyId = companyDetail.ParentId == null ? CompanyId : companyDetail.ParentId.Value;
                        }
                    }
                    else
                    {
                        paymentOptions.Add(
                       new App.Models.PaymentModal
                       {
                           Id = 0,
                           CardType = "Cheque",
                           CardValue = "CQ"
                       });
                    }

                    //        var list = (from n in db.PaymentOptions
                    //                    join m in db.DomainPaymentOptionMappings
                    //on n.Id equals m.PaymentOptionId
                    //                    select new { n.CardFee, n.CardType, n.IsActive, n.Logo, m.CompanyId, n.Id })
                    //                    .Where(x => x.CompanyId == CompanyId && x.IsActive == true).ToList();
                    //        foreach (var item in list)
                    //        {
                    //            paymentOptions.Add(
                    //                new App.Models.PaymentModal
                    //                {
                    //                    Id = item.Id,
                    //                    CardType = item.CardType
                    //                });
                    //        }

                }
            }
            catch (Exception ex)
            {
                App.Base.ErrorsLog.ErrorsLogInstance.ManageException(ex);
            }
            return paymentOptions;
        }


    }
}