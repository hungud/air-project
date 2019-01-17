using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Text;
using App.ConfigService.Controllers.SOAPServices.SOAPServices;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Security;

namespace App.WebAPIConfigService.Controllers
{
    public class AirReservationBookingController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAirBooking")]
        public HttpResponseMessage GetAirBookingREQRES(Model.SOAPData.FlightBookingPayentRQ Flight_Booking_REQ)
        {
            string jsonData = string.Empty;
           
            Common.CommonUtility Utility = new Common.CommonUtility();
            jsonData = new JavaScriptSerializer().Serialize(Utility);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return response;
        }
        [HttpPost]
        [ActionName("GetInsuranceQuote")]
        public HttpResponseMessage GetInsuranceQuoteREQRES(ReqQuote2 request)
        {
            string jsonData = string.Empty;

            ReqQuote2 Quoterequest = new ReqQuote2();
            SabreClass obj_class = new SabreClass();

            //request.DATEDEP = request.DATEDEP;
            //request.DATERET = request.DATERET;
            //request.PROVINCE = request.PROVINCE;
            //request.NBPAX = request.NBPAX;

            //foreach (var items in request.PAXES)
            //{
            //    request.PAXES.Add(new PAX
            //    {
            //        BIRTHDATE = items.BIRTHDATE.ToString(),
            //        TRIPCOST = items.TRIPCOST,
            //        AMTAFTER = "",
            //        TAX = items.TAX,
            //        PRICE = items.PRICE,
            //    });
            //}
           

            /////////////////////////////////////////////////////////
            var Quoteresponse = obj_class.InsuranceQuote2(request);
            
            Quoteresponse.Wait();
            var result = Quoteresponse.Result;

            InsuranceQuoteResponse QuoteResponse = null;

            XmlSerializer serializer = new XmlSerializer(typeof(InsuranceQuoteResponse));
            //result = result.Replace("'\'", "");
            //StreamReader reader = new StreamReader(result);
            
            result = result.Substring(1, result.Length - 2);
            result = result.Replace("\\", @"");
            XmlReader xmlReader = XmlReader.Create(new StringReader(result));



            QuoteResponse = (InsuranceQuoteResponse)serializer.Deserialize(xmlReader);
            //reader.Close();

            jsonData = new JavaScriptSerializer().Serialize(QuoteResponse);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return response;
        }

       
        [HttpPost]
        [ActionName("PostCCAuthorizeREQRES")]
        public HttpResponseMessage CCAuthorizeREQRES(App.ConfigService.Controllers.SOAPServices.SOAPServices.CCAuthorizeModelsList Booking_Flight_Passenger_REQ)
        {
            string jsonData = string.Empty;
            if (Booking_Flight_Passenger_REQ != null)
            {
                Common.CCAuthorizeUtility Utility = new App.ConfigService.Controllers.SOAPServices.SOAPServices.SabreClass().CCAuthorizeREQRES(Booking_Flight_Passenger_REQ);

                string AirLineReservationCode = Utility.Locator;

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
        /// <summary>
        /// For RetrievePNR
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ActionName("GetBookingDetails")]
        public HttpResponseMessage GetBookingDetailsREQRES(string PNR, string PCC,string username,string password)
        {
            if (string.IsNullOrEmpty(PNR) || PNR.Length == 0 )
            {
                return null;
            }

            App.Common.BookingDetailsRQModal RQ = new App.Common.BookingDetailsRQModal();
            RQ.PNR = PNR;
            RQ.PCC = PCC;
            RQ.Username = username;
            RQ.Password = password;
            string jsonData = string.Empty;
            FlightPassengerDetails Utility = new App.ConfigService.Controllers.SOAPServices.SOAPServices.SabreClass().GetBookingDetailsREQRES(RQ);
            //return Utility;
            jsonData = new JavaScriptSerializer().Serialize(Utility);
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return response;
            //return Request.CreateResponse(HttpStatusCode.OK, Utility);
        }

        [HttpPost]
        [ActionName("PostAirBooking")]
        public HttpResponseMessage PutAirBookingREQRES(App.ConfigService.Controllers.SOAPServices.SOAPServices.AirPassengerCreateCardModelsList Booking_Flight_Passenger_REQ)
        {
            string jsonData = string.Empty;
            if (Booking_Flight_Passenger_REQ != null)
            {
                App.BusinessLayer.BusinessLayer save_data = new App.BusinessLayer.BusinessLayer();
                //App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                var initsaber = new App.ConfigService.Controllers.SOAPServices.SOAPServices.SabreClass();
                Common.CommonUtility Utility = initsaber.AirPassengerCreateCardBookingREQRES(Booking_Flight_Passenger_REQ);

                //if((Utility.Message== "Success") && (Utility.ErrorCode== "Complete"))
                //{
                //    //Save Booking To Database 
                //    App.BusinessLayer.BusinessLayer Obj = new App.BusinessLayer.BusinessLayer();
                //    App.BusinessLayer.VMSaveSearch model = new App.BusinessLayer.VMSaveSearch();
                //    Obj.SaveBooking(model);

                //}


                //App.Base.Common.CommonMailManager.SendMailManager(Utility);

                string RequestID = Utility.Data1;
                string pnrnumber = Utility.Data.pnrnumber;
                /* Save Searched Data in Database */
                
                Booking_Flight_Passenger_REQ.SaveRecords.Locator = pnrnumber;
                save_data.TrackReservationRecords(Booking_Flight_Passenger_REQ.SaveRecords);
                
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
        /// <summary>
        /// For Air Booking REQ and RES
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        /// 

        [HttpPut]
        [ActionName("PutAirBooking")]
        public HttpResponseMessage PostDashboardData(Models.SabreModels.SabreFlightBookingPayentRQ Flight_Booking_REQ)
        {
            string jsonData = string.Empty;
            if (Flight_Booking_REQ != null)
            {
                Common.CommonUtility Utility = new App.ConfigService.Controllers.SOAPServices.SabreClassVersion1.SabreClass().AirReservationBookingREQRES(Flight_Booking_REQ);
                //Common.CommonUtility Utility = new Common.CommonUtility();
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
        /// <summary>
        /// For Air Cancel Request
        /// </summary>
        /// <param name="Flight_Booking_REQ"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteAirBooking")]
        public HttpResponseMessage DeleteAirBookingREQRES(App.Model.SOAPData.FlightBookingPayentRQ Flight_Booking_REQ)
        {
            string jsonData = string.Empty;
            if (Flight_Booking_REQ != null)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
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