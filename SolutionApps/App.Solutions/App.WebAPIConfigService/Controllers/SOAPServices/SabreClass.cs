using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using App.BusinessLayer;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
//using App.ConfigService.Models;
using System.Xml.Serialization;

namespace App.ConfigService.Controllers.SOAPServices.SOAPServices
{
    public class SabreClass
    {
        String sessionconversionid = "";
        String binarysecuritytoken = "";
        private string baseurl = "http://test.nanojot.com/services/Insurance/";

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public FlightPassengerDetails GetBookingDetailsREQRES(App.Common.BookingDetailsRQModal Request)
        {
            string TimeStamp = "";
            FlightPassengerDetails ResponseDetails = new FlightPassengerDetails();
            string sess = SessionCreate(Request.PCC, Request.Username, Request.Password);

            string getDetails = ReadRetrievePNR(Request.PCC, binarysecuritytoken, Request.PNR, "2018-03-07T05:17:44-06:00", "FULL");
            ResponseDetails = readPNRXML(getDetails);
            return ResponseDetails;
            //return new Common.CommonUtility()
            //{
            //    Data = ResponseDetails,
            //};
        }

        public App.Common.CommonUtility AirPassengerCreateCardBookingREQRES(AirPassengerCreateCardModelsList AirPassengerCreateCardOtherList)
        {
            FlightPassengerDetails ResponseDetails = new FlightPassengerDetails();
            //App.BusinessLayer.VMSaveSearch SaveSearchModel = new App.BusinessLayer.VMSaveSearch();

            string ReturnMessage = string.Empty;
            string ErrorStatus = string.Empty;
            string AgencyDetails = string.Empty;
            string PolicyNumber = string.Empty;
            string PURCHASE_DATE = string.Empty;
            //foreach(var item in QuoteResponse.RESPONSE.Products[0].PRICE)
            string Insurance_Price_array = string.Empty;
            try
            {
                List<AirNoofPassengers> AirNoofPassengersDetails = new List<AirNoofPassengers>();
                Dictionary<string, string> PassangerType = new Dictionary<string, string>();
                //string RequestID = Base.ErrorsLog.ErrorsLogInstance.RequestID.ToString();
                //ResponseDetails.RequestID = RequestID;

                PassangerType.Add("ADT", "Adults");
                PassangerType.Add("CNN", "Childrens");
                PassangerType.Add("INF", "Infants");

                foreach (var item in PassangerType)
                {
                    if (!item.Key.Equals("INF"))
                    {
                        AirNoofPassengersDetails.Add(new AirNoofPassengers() { IsInfant = false, PassengerCode = item.Key, NoOfPassengers = AirPassengerCreateCardOtherList.passengersDetailsList.Where(person => person.passengerType.Equals(item.Value)).Count() });
                    }
                    if (item.Key.Equals("INF"))
                    {
                        AirNoofPassengersDetails.Add(new AirNoofPassengers() { IsInfant = true, PassengerCode = item.Key, NoOfPassengers = AirPassengerCreateCardOtherList.passengersDetailsList.Where(person => person.passengerType.Equals(item.Value)).Count() });
                    }
                }
                int totalPassengers = 0;
                foreach (var items in AirPassengerCreateCardOtherList.passengersDetailsList)
                {
                    if (!string.IsNullOrEmpty(items.passengerAddress))
                    {
                        items.passengerAddress = items.passengerAddress.Replace(",", "");
                    }

                    if (items.passengerType == "Adults")
                    {
                        items.passengerType = "ADT";
                        items.IsInfant = "false";
                        totalPassengers++;
                    }
                    else if (items.passengerType == "Childrens")
                    {
                        items.passengerType = "CNN";
                        items.IsInfant = "false";
                        totalPassengers++;
                    }
                    else if (items.passengerType == "Infants")
                    {
                        items.passengerType = "INF";
                        items.IsInfant = "true";
                    }
                }
                foreach (var items in AirPassengerCreateCardOtherList.flightsDetailsList)
                {
                    items.NoofPassengers = totalPassengers.ToString();
                }

                string pcc = AirPassengerCreateCardOtherList._AgencyDetails.PseudoCityCode;

                //Create Session
                string sess = SessionCreate(pcc, AirPassengerCreateCardOtherList._AgencyDetails.UserName, AirPassengerCreateCardOtherList._AgencyDetails.Password);

                string WEBSERVICE_URL = ConfigurationManager.AppSettings["SearchBoxServiceURL"] + "/GetAgencyDetailsByDomain";
                string AgencyAddressLine = AirPassengerCreateCardOtherList._AgencyDetails.Name;
                string AgencyCCEnable = AirPassengerCreateCardOtherList._AgencyDetails.CCEnableAir;
                string AgencyCityName = AirPassengerCreateCardOtherList._AgencyDetails.CityName;
                string AgencyCountryCode = AirPassengerCreateCardOtherList._AgencyDetails.country;
                string AgencyPostalCode = AirPassengerCreateCardOtherList._AgencyDetails.PostalCode;
                string AgencyStateCode = AirPassengerCreateCardOtherList._AgencyDetails.state;
                string AgencyStreetNumber = AirPassengerCreateCardOtherList._AgencyDetails.StreetAddress;
                string AgencyPhoneNumber = AirPassengerCreateCardOtherList._AgencyDetails.PhoneNumber;
                string Origin = AirPassengerCreateCardOtherList._AgencyDetails.Origin;
                string Destination = AirPassengerCreateCardOtherList._AgencyDetails.Destination;
                string _ServiceFee = AirPassengerCreateCardOtherList._AgencyDetails.ServiceFee;


                string AgencyTicketType = string.Empty;
                string FromEmail = AirPassengerCreateCardOtherList._AgencyDetails.FromEmail;
                string ToEmail = AirPassengerCreateCardOtherList._AgencyDetails.ToEmail;
                AgencyDetails = AgencyAddressLine + "<br/> " + AgencyStreetNumber + " " + AgencyCityName + " " + AgencyStateCode + " " + AgencyPostalCode + "<br/> " + FromEmail + "<br/> " + AgencyPhoneNumber;
                AgencyTicketType = ConfigurationManager.AppSettings["AgencyTicketType"];
                string AgencyQueueNo = AirPassengerCreateCardOtherList._AgencyDetails.QueueNo;
                //string addARUNK = airEnhancedBookArunkRQ(pcc, binarysecuritytoken);
                string getbookings = airEnhancedBookMultiSegmentRQ(pcc, binarysecuritytoken, AirPassengerCreateCardOtherList.flightsDetailsList);
                string bookingStatus = string.Empty;
                ErrorStatus = GetApplicationResultBooking(getbookings, out bookingStatus);
                string endTrans = string.Empty;
                String ccValidationerror = string.Empty;
                if (ErrorStatus == "Complete" || string.IsNullOrEmpty(ErrorStatus))
                {
                    string getPrice = "";

                    //Add Price Itinerary to Session and Validate Price 
                    getPrice = airPriceRequest(pcc, binarysecuritytoken, "true", AirNoofPassengersDetails, AirPassengerCreateCardOtherList._AgencyDetails.CurrencyCode);

                    string remark = "";
                    string ServiceFeeRemark = "";
                    string CommissionRemark = "";
                    string CCFEERemark = "";
                    float Commission = AirPassengerCreateCardOtherList._AgencyDetails.Commission;
                    float ServiceFee = Convert.ToSingle(AirPassengerCreateCardOtherList._AgencyDetails.ServiceFee);
                    float CCFEE = Convert.ToSingle(AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CCFee);
                    if (AirPassengerCreateCardOtherList.creditCardOtherDetails.Count > 0)
                    {
                        remark = "CVV " +
                            AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CVVNumber.ToString() + "-" +
                            AirPassengerCreateCardOtherList.creditCardOtherDetails[0].NameOnCard.ToString() + "-" +
                            AirPassengerCreateCardOtherList.creditCardOtherDetails[0].Address.ToString().Replace(",", "") +
                            "-CCFee " + AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CCFee.
                                        Substring(0, AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CCFee.IndexOf('.', 0));


                        remark = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].Address.ToString().Replace(",", ""); // Updating it as discussed with sir to set the remarks section to empty




                        if (Commission > 0)
                        {
                            CommissionRemark = "Commission " + AirPassengerCreateCardOtherList._AgencyDetails.Commission;
                        }
                        if (CCFEE > 0)
                        {
                            CCFEERemark = "CCFEE " + AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CCFee;
                        }
                        ServiceFeeRemark = "ServiceFee " + AirPassengerCreateCardOtherList._AgencyDetails.ServiceFee;
                    }

                    //Add Itinerary to Session
                    string getPassengerRes = addPassengerRequest(pcc, binarysecuritytoken, "false", "true", "true", AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber, "", AgencyTicketType, AgencyQueueNo, "ABD", AirPassengerCreateCardOtherList.passengersDetailsList, remark);

                    string getVerificationCode = "";
                    string getServiceFeeVerificationCode = "";
                    string getCommissionVerificationCode = "";
                    string getCCFeeVerificationCode = "";
                    // Credit Card Details
                    foreach (var CCDItem in AirPassengerCreateCardOtherList.creditCardOtherDetails)
                    {
                        if (ServiceFee > 0)
                        {
                            if (AirPassengerCreateCardOtherList._AgencyDetails.CCEnableAir == "True")
                            {
                                if (CCDItem.AirLineCode != "AI" && CCDItem.AirLineCode != "PK")
                                    ccValidationerror = checkCCVerificationRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, Convert.ToDecimal(CCDItem.PaymentAmount).ToString(), CCDItem.CurrCode);
                            }

                            getVerificationCode = AddRemarkRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, Convert.ToDecimal(CCDItem.PaymentAmount).ToString(), CCDItem.CurrCode, CCDItem.Address, CCDItem.NameOnCard, CCDItem.CCFee, AirPassengerCreateCardOtherList._AgencyDetails.ServiceFee, AirPassengerCreateCardOtherList.SaveRecords.BillingEmail, AirPassengerCreateCardOtherList.SaveRecords.BillingPhone, AirPassengerCreateCardOtherList._policydetails.POLICYCODE, AirPassengerCreateCardOtherList._policydetails.Total_Insurance_Price);

                            //checkCCVerificationRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, CCDItem.CCFee.ToString(), CCDItem.CurrCode);
                            getCCFeeVerificationCode = AddCCFEERemarkRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, CCDItem.CCFee.ToString(), CCDItem.CurrCode, CCDItem.Address, CCDItem.NameOnCard, "AddCCFEERemark", CCFEERemark);
                        }
                        else
                        {
                            if (AirPassengerCreateCardOtherList._AgencyDetails.CCEnableAir == "True")
                            {
                                if (CCDItem.AirLineCode != "AI" && CCDItem.AirLineCode != "PK")
                                    ccValidationerror = checkCCVerificationRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, (Convert.ToDecimal(CCDItem.PaymentAmount) + Convert.ToDecimal(CCDItem.CCFee)).ToString(), CCDItem.CurrCode);
                            }

                            getVerificationCode = AddRemarkRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, (Convert.ToDecimal(CCDItem.PaymentAmount) + Convert.ToDecimal(CCDItem.CCFee)).ToString(), CCDItem.CurrCode, CCDItem.Address, CCDItem.NameOnCard, CCDItem.CCFee, AirPassengerCreateCardOtherList._AgencyDetails.ServiceFee, AirPassengerCreateCardOtherList.SaveRecords.BillingEmail, AirPassengerCreateCardOtherList.SaveRecords.BillingPhone, AirPassengerCreateCardOtherList._policydetails.POLICYCODE, AirPassengerCreateCardOtherList._policydetails.Total_Insurance_Price);


                        }
                        //Service Fee Charge Request

                        if (ServiceFee > 0)
                        {
                            //checkCCVerificationRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, ServiceFee.ToString(), CCDItem.CurrCode);
                            getServiceFeeVerificationCode = AddServiceCommissionRemarkRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, ServiceFee.ToString(), CCDItem.CurrCode, CCDItem.Address, CCDItem.NameOnCard, "AddServiceRemark", ServiceFeeRemark);
                        }
                        if (Commission > 0)
                        {
                            //checkCCVerificationRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, Commission.ToString(), CCDItem.CurrCode);
                            getCommissionVerificationCode = AddServiceCommissionRemarkRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, Commission.ToString(), CCDItem.CurrCode, CCDItem.Address, CCDItem.NameOnCard, "AddCommissionRemark", CommissionRemark);
                        }

                    }



                    //End Tansaction 
                    endTrans = endTransaction(pcc, binarysecuritytoken, "true", "Online");
                    ErrorStatus = GetApplicationResult(endTrans);
                }
                if (ErrorStatus == "VERIFY ORDER OF ITINERARY SEGMENTS - MODIFY OR END TRANSACTIONERR.SWS.HOST.ERROR_IN_RESPONSE")
                {
                    string addARUNK = airEnhancedBookArunkRQ(pcc, binarysecuritytoken);
                    endTrans = endTransaction(pcc, binarysecuritytoken, "true", "Online");
                    ErrorStatus = GetApplicationResult(endTrans);
                }
                if (ErrorStatus == "Complete")
                {
                    //Get PNR
                    string getResult = GetResponseDocument(endTrans);
                    if (!string.IsNullOrEmpty(getResult))
                    {
                        string TimeStamp = getResult.Split(' ')[0].ToString();
                        string PNRID = getResult.Split(' ')[1].ToString();
                        string getDetails = ReadRetrievePNR(pcc, binarysecuritytoken, PNRID, TimeStamp, "FULL");
                        ResponseDetails = readPNRXML(getDetails);
                        ReturnMessage = "Success";
                        string emailid = "";
                        string ErrorMessage = "";
                        if (ccValidationerror.StartsWith("OK"))
                        {
                            //payment success
                            ErrorMessage = "PaymentSuccess";
                            //var insurance_response = InsuranceBook2();

                            //get insurance quote

                            Book2 request = new Book2();
                            request.LANGUAGE = "EN";
                            request.SEARCHTYPE = "FO";
                            //2018-12-10T00:30:00
                            request.DATEDEP = AirPassengerCreateCardOtherList._policydetails.DATEDEP.Split('T')[0].Split('-')[0].ToString() + AirPassengerCreateCardOtherList._policydetails.DATEDEP.Split('T')[0].Split('-')[1].ToString() + AirPassengerCreateCardOtherList._policydetails.DATEDEP.Split('T')[0].Split('-')[2].ToString();
                            request.DATERET = AirPassengerCreateCardOtherList._policydetails.DATERET.Split('T')[0].Split('-')[0].ToString() + AirPassengerCreateCardOtherList._policydetails.DATERET.Split('T')[0].Split('-')[1].ToString() + AirPassengerCreateCardOtherList._policydetails.DATERET.Split('T')[0].Split('-')[2].ToString();
                            request.GATEWAY = AirPassengerCreateCardOtherList._policydetails.GATEWAY.Substring(AirPassengerCreateCardOtherList._policydetails.GATEWAY.IndexOf('[') + 1, 3);
                            request.DESTINATION = AirPassengerCreateCardOtherList._policydetails.DESTINATION.Substring(AirPassengerCreateCardOtherList._policydetails.DESTINATION.IndexOf('[') + 1, 3);
                            request.POLICYCODE = AirPassengerCreateCardOtherList._policydetails.POLICYCODE;
                            request.PHONE = AirPassengerCreateCardOtherList._policydetails.PHONE;
                            request.EMAIL = AirPassengerCreateCardOtherList._policydetails.EMAIL;
                            request.ADDRESS = AirPassengerCreateCardOtherList._policydetails.ADDRESS;
                            request.CITY = AirPassengerCreateCardOtherList._policydetails.CITY;
                            request.PROVINCE = AirPassengerCreateCardOtherList._policydetails.PROVINCE.Trim();
                            request.POSTALCODE = AirPassengerCreateCardOtherList._policydetails.POSTALCODE;
                            request.COUNTRY = AirPassengerCreateCardOtherList._policydetails.COUNTRY;
                            request.DAYSPERTRIP = "";
                            request.COMMENTS = "";
                            int NBPAX = 0;

                            foreach (var items in AirPassengerCreateCardOtherList.passengersDetailsList)
                            {
                                NBPAX += 1;
                                if (items.passengerType == "Adults")
                                {

                                    request.TRAVELERS.Add(new Traveler
                                    {
                                        FIRSTNAME = items.passengername,
                                        LASTNAME = "",
                                        BIRTHDATE = Convert.ToDateTime(items.DateofBirth).ToString("yyyyMMdd"),
                                        TRIPCOST = items.Tripcost,
                                        AMTAFTER = "",
                                        REFNUM = "",
                                        INFANT = "ONSEAT"
                                    });
                                }
                                else
                                {
                                    request.TRAVELERS.Add(new Traveler
                                    {
                                        FIRSTNAME = items.passengername,
                                        LASTNAME = "",
                                        BIRTHDATE = Convert.ToDateTime(items.DateofBirth).ToString("yyyyMMdd"),
                                        TRIPCOST = items.Tripcost,
                                        AMTAFTER = "",
                                        REFNUM = "",
                                    });
                                }
                                request.PAYMENTS.Add(new Payment
                                {
                                    CCNAME = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].NameOnCard,
                                    CCAMOUNT = items.InsurancePrice,
                                    CCTYPE = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CardType,
                                    CCNUMBER = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CardNumber,
                                    CCEXPMONTH = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CardExpiryDate.Split('-')[1],
                                    CCEXPYEAR = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CardExpiryDate.Split('-')[0].Substring(AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CardExpiryDate.Split('-')[1].Length, 2),
                                });
                            }

                            request.NBPAX = NBPAX.ToString();
                            var response = InsuranceBook2(request);
                            response.Wait();
                            var result = response.Result;
                            result = result.Substring(1, result.Length - 2);
                            result = result.Replace("\\", @"");
                            InsuranceQuoteResponse QuoteResponse = null;
                            XmlReader xmlReader = XmlReader.Create(new StringReader(result));
                            XmlSerializer serializer = new XmlSerializer(typeof(InsuranceQuoteResponse));


                            //StreamReader reader = new StreamReader(result);
                            QuoteResponse = (InsuranceQuoteResponse)serializer.Deserialize(xmlReader);
                            //reader.Close();
                            PolicyNumber = QuoteResponse.RESPONSE.POLICY_NUMBER;
                            PURCHASE_DATE = QuoteResponse.RESPONSE.PURCHASE_DATE;
                            //foreach(var item in QuoteResponse.RESPONSE.Products[0].PRICE)
                            Insurance_Price_array = "";
                            for (int i = 0; i < QuoteResponse.RESPONSE.Products.Count; i++)
                            {
                                Insurance_Price_array += QuoteResponse.RESPONSE.Products[0].PRICE + ";";
                            }
                            //get insurance quote


                        }
                        else
                        {
                            //payment failed
                            ErrorMessage = "PaymentFailure";

                        }


                        string body = CreateBookingConfirmationPageSuccess(AirPassengerCreateCardOtherList.flightsDetailsList, AirPassengerCreateCardOtherList.passengersDetailsList, PNRID, "Confirmed", DateTime.Now.ToString("yyyy-dd-MM"), AgencyAddressLine + " " + AgencyStreetNumber + " " + AgencyCityName + " " + AgencyStateCode + " " + AgencyPostalCode, _ServiceFee, AirPassengerCreateCardOtherList.creditCardOtherDetails, "", "", "", "", out emailid, ErrorMessage, AgencyAddressLine, PolicyNumber, PURCHASE_DATE, Insurance_Price_array, AgencyCCEnable, Origin, Destination, AirPassengerCreateCardOtherList.SaveRecords.selectionkey, AirPassengerCreateCardOtherList.SaveRecords.RequestID);
                        ErrorStatus = ccValidationerror;
                        SendHtmlFormattedEmail(emailid.Split(',')[0].ToString(), "Flight Confirmation - " + PNRID + "", body, FromEmail, ToEmail);

                    }
                }
                else
                {
                    string emailid = "";
                    string body = CreateBookingConfirmationPageFailure(AirPassengerCreateCardOtherList.flightsDetailsList, AirPassengerCreateCardOtherList.passengersDetailsList, "PENDING", "PENDING", DateTime.Now.ToString("yyyy-dd-MM"), "", _ServiceFee, AirPassengerCreateCardOtherList.creditCardOtherDetails, "", "", "", "", out emailid, AgencyAddressLine, AgencyPhoneNumber, FromEmail, Origin, Destination, AirPassengerCreateCardOtherList.SaveRecords.selectionkey, AirPassengerCreateCardOtherList.SaveRecords.RequestID);
                    SendHtmlFormattedEmail(emailid.Split(',')[0].ToString(), "Booking Pending", body, FromEmail, ToEmail);
                    StringBuilder bldr = new StringBuilder();

                    bldr.Append("<html>");
                    bldr.Append("<body>");
                    bldr.Append("<table>");
                    bldr.Append("<tr>");
                    bldr.Append("<td>");
                    bldr.Append("Reservation has been failed for the RequestID :" + Base.ErrorsLog.ErrorsLogInstance.RequestID + ". Please take necessary action.");
                    bldr.Append("</td>");
                    bldr.Append("</tr>");
                    bldr.Append("<tr>");
                    bldr.Append("<td>");
                    bldr.Append(endTrans);
                    bldr.Append("</td>");
                    bldr.Append("</tr>");
                    bldr.Append("</table>");
                    bldr.Append("</body>");
                    bldr.Append("</html>");
                    body = bldr.ToString();
                    SendHtmlFormattedEmail("qa@nanojot.com", "Error in Booking", body, FromEmail, "");
                }
                SessionClose(binarysecuritytoken, pcc);


            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            if (AirPassengerCreateCardOtherList.creditCardOtherDetails.Count > 0)
            {
                return new Common.CommonUtility()
                {
                    Data = ResponseDetails,
                    Message = ReturnMessage,
                    AgencyDetails = AgencyDetails,
                    Status = true,
                    ErrorCode = ErrorStatus,
                    CCFEE = AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CCFee,
                    PolicyNumber = PolicyNumber,
                    PurchaseDate = PURCHASE_DATE,
                    Price = Insurance_Price_array,

                };
            }
            else
            {
                return new Common.CommonUtility()
                {
                    Data = ResponseDetails,
                    Message = ReturnMessage,
                    AgencyDetails = AgencyDetails,
                    Status = true,
                    ErrorCode = ErrorStatus,
                    CCFEE = "0.00",
                    PolicyNumber = PolicyNumber,
                    PurchaseDate = PURCHASE_DATE,
                    Price = Insurance_Price_array,

                };
            }
        }

        public App.Common.CCAuthorizeUtility CCAuthorizeREQRES(CCAuthorizeModelsList AirPassengerCreateCardOtherList)
        {
            FlightPassengerDetails ResponseDetails = new FlightPassengerDetails();
            string Locator = null;
            //App.BusinessLayer.VMSaveSearch SaveSearchModel = new App.BusinessLayer.VMSaveSearch();
            string CCValidationStatus = "Failed";
            string ccValidationerror = string.Empty;
            string ReturnMessage = string.Empty;
            string ErrorStatus = string.Empty;
            string AgencyDetails = string.Empty;
            try
            {
                List<AirNoofPassengers> AirNoofPassengersDetails = new List<AirNoofPassengers>();
                //string RequestID = Base.ErrorsLog.ErrorsLogInstance.RequestID.ToString();
                //ResponseDetails.RequestID = RequestID;

                //Add Passenger Count For Request
                AirNoofPassengersDetails.Add(new AirNoofPassengers() { IsInfant = false, PassengerCode = "ADT", NoOfPassengers = 1 });
                int totalPassengers = 1;

                List<PassengerDetails> passengersDetailsList = new List<PassengerDetails>();
                PassengerDetails add_passenger = new PassengerDetails();
                add_passenger.passengerType = "ADT";
                add_passenger.passengerAddress = "Jersey City,United States";
                add_passenger.ReservationCode = "142144";
                add_passenger.airlineName = "1245";
                add_passenger.email = "qa@nanojot.com";
                add_passenger.DepartureCityCode = "4592030000312745";
                add_passenger.DepartureTime = "4592030000312745";
                add_passenger.DepartureTerminal = "4592030000312745";
                add_passenger.IsInfant = "false";
                add_passenger.DateofBirth = "1975-03-20";
                add_passenger.PhoneLocationCode = "0024278145";
                add_passenger.PassengerNameNumber = "1.1";
                add_passenger.PhoneNumber = "0024278145";
                add_passenger.PhoneUseType = "H";
                add_passenger.PassengerEmail = "qa@nanojot.com";
                add_passenger.PassengerNameRef = "ADT000";
                add_passenger.Gender = "M";
                add_passenger.GivenName = "jatin MR";
                add_passenger.Surname = "shah";
                passengersDetailsList.Add(add_passenger);

                List<FlightDetails> flightsDetailsList = new List<FlightDetails>();
                FlightDetails add_flightdetails = new FlightDetails();
                add_flightdetails.DepartureDate = "2018-07-07T21:40";
                add_flightdetails.ArrivalDate = "";
                add_flightdetails.AirlineName = "DEL";
                add_flightdetails.FlightNumber = "173";
                add_flightdetails.DepartureCityCode = "BLR";
                add_flightdetails.DepartureTime = "2018-07-07T21:40:00";
                add_flightdetails.DepartureTerminal = "BLR";
                add_flightdetails.ArrivalCityCode = "DEL";
                add_flightdetails.ArrivalTime = "2018-07-08T00:25";
                add_flightdetails.ArrivalTerminal = "";
                add_flightdetails.DistanceTravel = "0";
                add_flightdetails.AircraftType = "32B";
                add_flightdetails.DepartureDateTime = "2018-07-07T21:40:00";
                add_flightdetails.ArrivalDateTime = "2018-07-08T00:25";
                add_flightdetails.BookingClass = "0";
                add_flightdetails.FlightTime = "2018-07-07T21:40";
                add_flightdetails.DirectionInd = "0";
                add_flightdetails.DepAirportLocationCode = "BLR";
                add_flightdetails.OperatingAirlineCode = "AI";
                add_flightdetails.ArrAirportLocationCode = "DEL";
                add_flightdetails.Equipment = "32B";
                add_flightdetails.MarketingAirline = "AI";
                add_flightdetails.NoofPassengers = "1";
                add_flightdetails.resBookDesigCode = "S";
                add_flightdetails.status = "YK";
                flightsDetailsList.Add(add_flightdetails);

                AgencyDetails _AgencyDetails = new AgencyDetails();
                _AgencyDetails.Name = "Flypapa";
                _AgencyDetails.CityName = "";
                _AgencyDetails.country = "";
                _AgencyDetails.PostalCode = "07306";
                _AgencyDetails.state = "";
                _AgencyDetails.StreetAddress = "3000 JFK Blvd Suite 313C";
                _AgencyDetails.SmtpEmailID = "info@flypapa.com";
                _AgencyDetails.HeaderUrl = "http://bookings.flypapa.com/Asset/Flypapa/header.html";
                _AgencyDetails.FooterUrl = "http://bookings.flypapa.com/Asset/Flypapa/footer.html";
                _AgencyDetails.PrivacyUrl = "http://bookings.flypapa.com/ClientPages/Flypapa/privacy.html";
                _AgencyDetails.TermsUrl = "http://bookings.flypapa.com/ClientPages/Flypapa/Terms.html";
                _AgencyDetails.PhoneNumber = "02013370067";
                _AgencyDetails.UserName = "831673";
                _AgencyDetails.Password = "WS935917";
                _AgencyDetails.QueueNo = "55";
                _AgencyDetails.PseudoCityCode = "5IDI";
                _AgencyDetails.FromEmail = "info@flypapa.com";
                _AgencyDetails.ToEmail = "info@flypapa.com";
                _AgencyDetails.Domain = "flypapa.com";

                foreach (var items in flightsDetailsList)
                {
                    items.NoofPassengers = totalPassengers.ToString();
                }

                string pcc = _AgencyDetails.PseudoCityCode;

                //Create Session
                string sess = SessionCreate(pcc, _AgencyDetails.UserName, _AgencyDetails.Password);

                string WEBSERVICE_URL = ConfigurationManager.AppSettings["SearchBoxServiceURL"] + "/GetAgencyDetailsByDomain";
                string AgencyAddressLine = _AgencyDetails.Name;
                string AgencyCityName = _AgencyDetails.CityName;
                string AgencyCountryCode = _AgencyDetails.country;
                string AgencyPostalCode = _AgencyDetails.PostalCode;
                string AgencyStateCode = _AgencyDetails.state;
                string AgencyStreetNumber = _AgencyDetails.StreetAddress;
                string AgencyPhoneNumber = _AgencyDetails.PhoneNumber;
                string AgencyTicketType = string.Empty;
                string FromEmail = _AgencyDetails.FromEmail;
                string ToEmail = _AgencyDetails.ToEmail;
                AgencyDetails = AgencyAddressLine + "<br/> " + AgencyStreetNumber + " " + AgencyCityName + " " + AgencyStateCode + " " + AgencyPostalCode + "<br/> " + FromEmail + "<br/> " + AgencyPhoneNumber;
                AgencyTicketType = ConfigurationManager.AppSettings["AgencyTicketType"];
                string AgencyQueueNo = _AgencyDetails.QueueNo;

                string getbookings = airShortSellRQ(binarysecuritytoken, flightsDetailsList);

                string bookingStatus = string.Empty;
                ErrorStatus = GetApplicationResultBooking(getbookings, out bookingStatus);
                string endTrans = string.Empty;
                //String ccValidationerror = string.Empty;
                if (ErrorStatus == "Complete" || string.IsNullOrEmpty(ErrorStatus))
                {
                    string getPrice = "";

                    //Add Price Itinerary to Session and Validate Price 
                    getPrice = airPriceRequest(pcc, binarysecuritytoken, "true", AirNoofPassengersDetails, _AgencyDetails.CurrencyCode);

                    string remark = "";
                    if (AirPassengerCreateCardOtherList.creditCardOtherDetails.Count > 0)
                        remark = "CVV " + AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CVVNumber.ToString() + "- Name of CC " + AirPassengerCreateCardOtherList.creditCardOtherDetails[0].NameOnCard.ToString() + "- " + AirPassengerCreateCardOtherList.creditCardOtherDetails[0].Address.ToString().Replace(",", "") + "- CC Fee " + AirPassengerCreateCardOtherList.creditCardOtherDetails[0].CCFee;

                    //Add Itinerary to Session
                    string getPassengerRes = addPassengerRequest(pcc, binarysecuritytoken, "false", "true", "true", AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber, "", AgencyTicketType, AgencyQueueNo, "ABD", passengersDetailsList, remark);

                    string getVerificationCode = "";

                    // Credit Card Details
                    foreach (var CCDItem in AirPassengerCreateCardOtherList.creditCardOtherDetails)
                    {
                        //if (CCDItem.AirLineCode != "AI" && CCDItem.AirLineCode != "PK")
                        ccValidationerror = checkCCVerificationRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, "AI", CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, (Convert.ToDecimal(CCDItem.PaymentAmount) + Convert.ToDecimal(CCDItem.CCFee)).ToString(), CCDItem.CurrCode);


                        CCValidationStatus = GetCCAuthorizeResult(ccValidationerror);

                        getVerificationCode = AddRemarkccRequest(binarysecuritytoken, pcc, "true", CCDItem.CardType, CCDItem.AirLineCode, CCDItem.CardNumber, CCDItem.CardExpiryDate, CCDItem.CVVNumber, (Convert.ToDecimal(CCDItem.PaymentAmount) + Convert.ToDecimal(CCDItem.CCFee)).ToString(), CCDItem.CurrCode, CCDItem.Address, CCDItem.NameOnCard);
                    }

                    //End Tansaction 
                    endTrans = endTransaction(pcc, binarysecuritytoken, "true", "Online");
                    ErrorStatus = GetApplicationResult(endTrans);
                }
                if (ErrorStatus == "VERIFY ORDER OF ITINERARY SEGMENTS - MODIFY OR END TRANSACTIONERR.SWS.HOST.ERROR_IN_RESPONSE")
                {
                    string addARUNK = airEnhancedBookArunkRQ(pcc, binarysecuritytoken);
                    endTrans = endTransaction(pcc, binarysecuritytoken, "true", "Online");
                    ErrorStatus = GetApplicationResult(endTrans);
                }
                if (CCValidationStatus == "Complete")
                {
                    //Get PNR
                    string getResult = GetResponseDocument(endTrans);
                    if (!string.IsNullOrEmpty(getResult))
                    {
                        string TimeStamp = getResult.Split(' ')[0].ToString();
                        string PNRID = getResult.Split(' ')[1].ToString();
                        string getDetails = ReadRetrievePNR(pcc, binarysecuritytoken, PNRID, TimeStamp, "FULL");
                        ResponseDetails = readPNRXML(getDetails);
                        ReturnMessage = "Success";
                        string emailid = "";
                        string ErrorMessage = "";
                        if (ccValidationerror.StartsWith("OK"))
                        {
                            //payment success
                            ErrorMessage = "PaymentSuccess";
                        }
                        else
                        {
                            //payment failed
                            ErrorMessage = "PaymentFailure";

                        }


                        //string body = CreateBookingConfirmationPageSuccess(AirPassengerCreateCardOtherList.flightsDetailsList, AirPassengerCreateCardOtherList.passengersDetailsList, PNRID, "Confirmed", DateTime.Now.ToString("yyyy-dd-MM"), AgencyAddressLine + " " + AgencyStreetNumber + " " + AgencyCityName + " " + AgencyStateCode + " " + AgencyPostalCode, "", "", "", "", out emailid, ErrorMessage, AgencyAddressLine);
                        //ErrorStatus = ccValidationerror;
                        //SendHtmlFormattedEmail(emailid.Split(',')[0].ToString(), "Flight Confirmation - " + PNRID + "", body, FromEmail, ToEmail);

                    }
                }
                else
                {
                    Locator = null;
                    //string emailid = "";
                    //string body = CreateBookingConfirmationPageFailure(AirPassengerCreateCardOtherList.flightsDetailsList, AirPassengerCreateCardOtherList.passengersDetailsList, "PENDING", "PENDING", DateTime.Now.ToString("yyyy-dd-MM"), "", "", "", "", "", out emailid, AgencyAddressLine, AgencyPhoneNumber, FromEmail);
                    //SendHtmlFormattedEmail(emailid.Split(',')[0].ToString(), "Booking Pending", body, FromEmail, ToEmail);
                    //StringBuilder bldr = new StringBuilder();

                    //bldr.Append("<html>");
                    //bldr.Append("<body>");
                    //bldr.Append("<table>");
                    //bldr.Append("<tr>");
                    //bldr.Append("<td>");
                    //bldr.Append("Reservation has been failed for the RequestID :" + Base.ErrorsLog.ErrorsLogInstance.RequestID + ". Please take necessary action.");
                    //bldr.Append("</td>");
                    //bldr.Append("</tr>");
                    //bldr.Append("<tr>");
                    //bldr.Append("<td>");
                    //bldr.Append(endTrans);
                    //bldr.Append("</td>");
                    //bldr.Append("</tr>");
                    //bldr.Append("</table>");
                    //bldr.Append("</body>");
                    //bldr.Append("</html>");
                    //body = bldr.ToString();
                    //SendHtmlFormattedEmail("qa@nanojot.com", "Error in Booking", body, FromEmail, "");
                }
                SessionClose(binarysecuritytoken, pcc);


            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }

            Locator = ResponseDetails.pnrnumber;
            return new Common.CCAuthorizeUtility()
            {
                Locator = Locator,
                ResponseStatus = CCValidationStatus,
                ResponseMessage = ccValidationerror

            };
        }

        public string GetCCAuthorizeResult(string result)
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.LoadXml(result);
            string res = doc1.SelectSingleNode("//*[local-name()='Body']/*").FirstChild.Attributes[0].Value;
            string ret = string.Empty;
            ret = res;
            if (res != "Complete")
            {
                ret = doc1.SelectSingleNode("//*[local-name()='Body']/*").FirstChild.InnerText;
            }
            return ret;
        }
        //getBookRequest_ShortSellRQ   
        public string airShortSellRQ(string token, List<FlightDetails> flights)
        {
            string result = "";
            result = executeWebRequest(getBookRequest_ShortSellRQ(token, flights), "EnhancedBookMultiSegment");
            return result;
        }
        private String getBookRequest_ShortSellRQ(string token, List<FlightDetails> flights)
        {
            string flightsegments = ShortSellflightDetails(flights);
            //string dtDepartureTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code
            StringBuilder bldr = new StringBuilder();
            string pcc = "6DTH";
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">ShortSellLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>ShortSellLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Book Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("   <sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("       <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("   </sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <ShortSellRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.0\">");
            bldr.Append("      <OriginDestinationInformation>");
            bldr.Append(flightsegments);
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     </ShortSellRQ>");
            bldr.Append("     </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private string ShortSellflightDetails(List<FlightDetails> flights)
        {
            StringBuilder bldr = new StringBuilder();
            int count = 1;
            foreach (var flight in flights)
            {
                if (count == 1)
                {
                    //if (count == 3)
                    //{
                    //    string addarunk=airEnhancedBookArunkRQ(pcc, binarysecuritytoken);
                    //}
                    bldr.Append("   <FlightSegment DepartureDateTime=\"" + ShortSellDateFormat(flight.DepartureDateTime) + "\" FlightNumber=\"" + flight.FlightNumber + "\" NumberInParty=\"" + flight.NoofPassengers + "\" ResBookDesigCode=\"" + flight.resBookDesigCode + "\" Status=\"" + "YK" + "\">");
                    bldr.Append("      <DestinationLocation   LocationCode=\"" + flight.ArrAirportLocationCode + "\"/>");
                    bldr.Append("      <MarketingAirline Code=\"" + flight.MarketingAirline + "\" FlightNumber=\"" + flight.FlightNumber + "\"/>");
                    bldr.Append("      <OriginLocation   LocationCode=\"" + flight.DepAirportLocationCode + "\"/>");
                    bldr.Append("     </FlightSegment>");
                    count += 1;
                }
            }
            return bldr.ToString();
        }

        public string ShortSellDateFormat(string dateTime)
        {
            DateTime parsedDate = new DateTime(Convert.ToInt32(dateTime.Split('T')[0].ToString().Split('-')[0].ToString()), Convert.ToInt32(dateTime.Split('T')[0].ToString().Split('-')[1].ToString()), Convert.ToInt32(dateTime.Split('T')[0].ToString().Split('-')[2].ToString()), Convert.ToInt32(dateTime.Split('T')[1].ToString().Split(':')[0].ToString()), Convert.ToInt32(dateTime.Split('T')[1].ToString().Split(':')[1].ToString()), 0);
            return String.Format("{0:MM-dd}", parsedDate);

        }
        #region Get Insurance Quote
        public async Task<string> InsuranceQuote2(ReqQuote2 prequest)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                List<Quote2> lstrequest = new List<Quote2>();
                Quote2 request = new Quote2();
                request.NBPAX = prequest.NBPAX;
                request.PAXES = prequest.PAXES;
                request.PROVINCE = prequest.PROVINCE.Trim();

                //2018-11-01T01:20:00

                request.DATEDEP = prequest.DATEDEP.Split('T')[0].Split('-')[0].ToString() + prequest.DATEDEP.Split('T')[0].Split('-')[1].ToString() + prequest.DATEDEP.Split('T')[0].Split('-')[2].ToString();
                request.DATERET = prequest.DATERET.Split('T')[0].Split('-')[0].ToString() + prequest.DATERET.Split('T')[0].Split('-')[1].ToString() + prequest.DATERET.Split('T')[0].Split('-')[2].ToString();

                request.LANGUAGE = "EN";
                request.SEARCHTYPE = "FO";
                request.DAYSPERTRIP = "";
                request.FAMILYRATE = "";
                request.PRODUCTS.Add(new PRODUCT
                {
                    CODE = "TSVGIN"
                });

                lstrequest.Add(request);
                string requesquote2 = new JavaScriptSerializer().Serialize(lstrequest);

                string uri = "api/Insurance/GetQuote2?quote2request=" + requesquote2;
                HttpResponseMessage Res = await client.GetAsync(uri).ConfigureAwait(false);

                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                    return response;
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(response);
                    XmlDocument filteredDocument = null;
                    XmlNode filteredResponse = xDoc.SelectSingleNode("//*[local-name()='Body']/*");
                    filteredDocument = new XmlDocument();
                    filteredDocument.LoadXml(filteredResponse.OuterXml);

                    foreach (XmlNode node in filteredDocument.DocumentElement.ChildNodes)
                    {

                    }
                }
            }



            return null;
        }
        #endregion

        public async Task<string> InsuranceBook2(Book2 request)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource  using HttpClient  
                List<Book2> lstrequest = new List<Book2>();
                //Book2 request = new Book2();

                //optional request.AGENTCODE = "MyAgent";
                //request.LANGUAGE = "EN";
                //request.SEARCHTYPE = "FO";
                //request.DATEDEP = "20180505";
                //request.DATERET = "20180707";
                //optional request.DAYSPERTRIP = "30";
                //optional request.FAMILYRATE = "NO";
                //request.GATEWAY = "YYZ";
                //request.DESTINATION = "CCC";
                //request.POLICYCODE = "TSVGIN";
                //request.PHONE = "8360037112";
                //request.EMAIL = "jonnys@nanojot.com";
                //request.ADDRESS = "My Address";
                //request.CITY = "KAPUSKASING";
                //request.PROVINCE = "ON";
                //request.POSTALCODE = "P5N2M7";
                //request.COUNTRY = "Canada";
                //request.COMMENTS = "";
                //request.BENEFICIARY = "Mrs Kamla Rani Sood";
                //request.RELATIONSHIP = "Wife";
                //request.PAYMENTS.Add(new Payment
                //{
                //    CCAMOUNT = "100.00",
                //    CCTYPE = "VI",
                //    CCNUMBER = "4444333322221111",
                //    CCEXPMONTH = "04",
                //    CCEXPYEAR = "19",
                //});
                //request.PAYMENTS.Add(new Payment
                //{
                //    CCAMOUNT = "120.00",
                //    CCTYPE = "VI",
                //    CCNUMBER = "4444333322220000",
                //    CCEXPMONTH = "05",
                //    CCEXPYEAR = "21",
                //});
                //request.NBPAX = "2";
                //request.TRAVELERS.Add(new Traveler
                //{
                //    FIRSTNAME = "Rajeev Kumar",
                //    LASTNAME = "Sood",
                //    BIRTHDATE = "19690525",
                //    TRIPCOST = "1560.00",
                //    AMTAFTER = "1860.00",
                //    REFNUM = "",
                //    INFANT = "ONSEAT"
                //});
                //request.TRAVELERS.Add(new Traveler
                //{
                //    FIRSTNAME = "Kavita Kumari",
                //    LASTNAME = "Sood",
                //    BIRTHDATE = "19720315",
                //    TRIPCOST = "1260.00",
                //    AMTAFTER = "1000.00",
                //    REFNUM = "",
                //});
                request.FAMILYRATE = "NO";
                lstrequest.Add(request);
                string book2request = new JavaScriptSerializer().Serialize(lstrequest);

                string uri = "api/Insurance/Book2Request?book2request=" + book2request;
                HttpResponseMessage Res = await client.GetAsync(uri).ConfigureAwait(false);

                if (Res.IsSuccessStatusCode)
                {
                    response = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }
        #region "Process Request"

        public string CreateBookingConfirmationPageFailure(List<FlightDetails> flights, List<PassengerDetails> passengers, string ConfirmationNumber, string BookingStatus, string ConfirmationDate, string TravelProvidedBy, string ServiceFEE, List<CreateCardAndOtherDetails> CardDetails, string Departingfrom, string Destination1, string DepartingDate, string ReturningDate, out string emailid, string AgencyAddressline, string AgencyContactnumber, string AgencyEmail, string Origin, string Destination, int selectionId,string requestId)
        {
            emailid = "";
            StringBuilder bldr = new StringBuilder();
            string flightSegmentDetails = "";
            string flightSegment = CreateflightDetails(flights, Origin, Destination, out flightSegmentDetails);
            string PaymentDetails = CreateCardDetails(CardDetails, ServiceFEE);
            bldr.Append("<html>");
            bldr.Append("<body>");
            bldr.Append("<table>");
            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Thank you for making your travel arrangements with " + AgencyAddressline + ". <br>" +
              "  Your reservation details are listed below for your review. <br>Please ensure that your flight information dates and times are correct.Please verify that all passenger names are exactly as they appear on your government - issued photo ID / Passport(s)." +
                "<br>PLEASE PRINT OR SAVE THIS PAGE AND KEEP IT AS YOUR RECORD OF YOUR REQUEST / PURCHASE.");
            bldr.Append("</td>");

            bldr.Append("</tr>");
            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append(" Important Information about Ticket Delivery: " +
              "  <br>Your electronic tickets(e - tickets) and supporting information will be sent to you by e - mail(to the address you have provided on your booking form) approximately 48 hours after your booking has been processed." +
                "<br>* *Please ensure to check your spam filters / junk mail folders associated with your email account as e - tickets or other " + AgencyAddressline + " correspondence may have been accidentally filtered in these boxes by your email provider * *" +
                "<br>For ticket inquiries, please e - mail us at " + AgencyEmail);
            bldr.Append("</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td>   ********YOUR BOOKING IS NOT YET CONFIRMED************* </td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td>   Your booking is not yet confirmed and is pending verification. <br>Please note that rates and availability are subject to change and will not be confirmed until payment is processed.If you have any questions please contact us at " + AgencyContactnumber + ". </td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Booking Details </td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("<table>");

            bldr.Append("<tr>");
            bldr.Append("<td> Booking Confirmation Number     : </td> ");
            bldr.Append("<td> " + ConfirmationNumber + " </td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Booking Status          : </td>");
            bldr.Append("<td> " + BookingStatus + "</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Confirmation Date       : </td>");
            bldr.Append("<td> " + ConfirmationDate + "</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Travel Provided By      :  </td>");
            bldr.Append("<td> " + TravelProvidedBy + "</td>");
            bldr.Append("</tr>");

            App.BusinessLayer.BusinessLayer appCommon = new BusinessLayer.BusinessLayer();
            string departureFrom = "", departureDate = "";
            string arrival = "", arrivalDate = "";


            if (selectionId == 3)
            {
                var request = appCommon.GetRequestById(requestId);
                if (request != null)
                {
                    var departureArray = request.Departure.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var destinationArray = request.Destination.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var departureDateArray = request.DepartureDate.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    int index = 0;
                    foreach (var item in departureArray)
                    {
                        bldr.Append("<tr>");
                        bldr.Append("<td> Departing from          :  </td>");
                        //bldr.Append("<td> " + flightSegmentDetails.Split(';')[0].ToString() + "</td>");
                        bldr.Append("<td> " + appCommon.GetAirlineCommonService(item, "Airport") + "</td>");
                        bldr.Append("</tr>");
                        if (index <= departureDateArray.Count - 1)
                        {
                            bldr.Append("<tr>");
                            bldr.Append("<td> Departing Date           :  </td>");
                            //bldr.Append("<td> " + flightSegmentDetails.Split(';')[2].ToString() + "</td>");
                            bldr.Append("<td> " + departureDateArray[index] + "</td>");
                            bldr.Append("</tr>");
                        }
                        index++;

                    }
                    foreach (var item in destinationArray)
                    {
                        bldr.Append("<tr>");
                        bldr.Append("<td> Destination             :  </td>");
                        //bldr.Append("<td> " + flightSegmentDetails.Split(';')[1].ToString() + "</td>");
                        bldr.Append("<td> " + appCommon.GetAirlineCommonService(item, "Airport") + "</td>");
                        bldr.Append("</tr>");
                    }
                }
            }

            else
            {

                if (flights.Count > 0)
                {

                    var lastFlight = flights.Count() - 1;
                    departureFrom = appCommon.GetAirlineCommonService(flights[0].DepAirportLocationCode, "Airport");
                    departureDate = DateFormat(flights[0].DepartureDateTime);
                    arrival = appCommon.GetAirlineCommonService(flights[lastFlight].ArrAirportLocationCode, "Airport");
                    arrivalDate = DateFormat(flights[lastFlight].ArrivalDateTime);
                }

                bldr.Append("<tr>");
                bldr.Append("<td> Departing from          :  </td>");
                //bldr.Append("<td> " + flightSegmentDetails.Split(';')[0].ToString() + "</td>");
                bldr.Append("<td> " + departureFrom + "</td>");
                bldr.Append("</tr>");

                bldr.Append("<tr>");
                bldr.Append("<td> Destination             :  </td>");
                // bldr.Append("<td> " + flightSegmentDetails.Split(';')[1].ToString() + "</td>");
                bldr.Append("<td> " + Destination + "</td>");
                bldr.Append("</tr>");

                bldr.Append("<tr>");
                bldr.Append("<td> Departing Date           :  </td>");
                //bldr.Append("<td> " + flightSegmentDetails.Split(';')[2].ToString() + "</td>");
                bldr.Append("<td> " + departureDate + "</td>");
                bldr.Append("</tr>");

                if (selectionId != 1)
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td> Returning Date           :  </td>");
                    //bldr.Append("<td> " + flightSegmentDetails.Split(';')[3].ToString() + "</td>");
                    bldr.Append("<td> " + arrivalDate + "</td>");
                    bldr.Append("</tr>");
                }
            }
            bldr.Append("</table>");
            bldr.Append("</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("<table>");

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Itinerary Details:");
            bldr.Append("</td>");
            bldr.Append("<td></td>");
            bldr.Append("</tr>");

            bldr.Append(flightSegment);
            bldr.Append("</td>");
            bldr.Append("</table>");

            bldr.Append("<table>");
            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Payment Details:");
            bldr.Append("</td>");
            bldr.Append("<td></td>");
            bldr.Append("</tr>");
            bldr.Append(PaymentDetails);
            bldr.Append("</td>");
            bldr.Append("</table>");

            bldr.Append("<table>");
            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Passengers Detail:");
            bldr.Append("</td>");
            bldr.Append("<td></td>");
            bldr.Append("</tr>");
            bldr.Append(CreatePassengerDetails(passengers, out emailid));

            bldr.Append("</td>");
            bldr.Append("</table>");

            bldr.Append("</td>");
            bldr.Append("</tr>");
            bldr.Append("</table>");

            bldr.Append("</body>");
            bldr.Append("</html>");

            return bldr.ToString();
        }

        public string CreateBookingConfirmationPageSuccess(List<FlightDetails> flights, List<PassengerDetails> passengers, string ConfirmationNumber, string BookingStatus, string ConfirmationDate, string TravelProvidedBy, string ServiceFEE, List<CreateCardAndOtherDetails> CardDetails, string Departingfrom, string Destination1, string DepartingDate, string ReturningDate, out string emailid, string ErrorMessage, string AgencyAddressline, string PolicyNumber, string PURCHASE_DATE, string Insurance_Price_array, string AgencyCCEnable, string Origin, string Destination, int selectionid, string requestId)
        {
            emailid = "";
            StringBuilder bldr = new StringBuilder();
            string flightSegmentDetails = "";
            string flightSegment = CreateflightDetails(flights, Origin, Destination, out flightSegmentDetails);
            string passDetails = CreatePassengerDetails(passengers, out emailid);
            string PaymentDetails = CreateCardDetails(CardDetails, ServiceFEE);
            string InsuranceDetails = "";
            if (PURCHASE_DATE.Length > 0)
            {
                InsuranceDetails = CreateInsuranceDetails(PolicyNumber, PURCHASE_DATE, Insurance_Price_array);
            }
            bldr.Append("<html>");
            bldr.Append("<body>");
            App.BusinessLayer.BusinessLayer appCommon = new BusinessLayer.BusinessLayer();
            bldr.Append("<table>");


            if (AgencyCCEnable == "True")
            {
                if (ErrorMessage == "PaymentFailure")
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td style='color:red;'>");

                    bldr.Append("Payment for the following booking was not successful. Selected fare is not guaranteed until paid in full. <br>  Please contact the agency and provide an alternate payment information to guarantee the selected fares.<br/>");
                    bldr.Append("</td>");
                    bldr.Append("</tr>");
                }
            }
            else if (AgencyCCEnable == "False")
            {
                bldr.Append("<tr>");
                bldr.Append("<td style='color:red;'>");

                bldr.Append("Thank you for booking with us, we are processing your booking.<br/>");
                bldr.Append("</td>");
                bldr.Append("</tr>");
            }

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Thank you for making your travel arrangements with " + AgencyAddressline + ". <br>" +
              "  Your reservation details are listed below for your review. <br>Please validate the correctness of your flight dates and times. <br> Please verify that all passenger names are exactly as they appear on your government - issued photo ID / Passport(s). <br>" +
                "PLEASE PRINT AND SAVE THIS PAGE AS YOUR RECORD OF REQUEST/PURCHASE <br>");
            bldr.Append("</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Booking Details </td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("<table>");

            bldr.Append("<tr>");
            bldr.Append("<td> Booking Confirmation Number     : </td> ");
            bldr.Append("<td> " + ConfirmationNumber + " </td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Booking Status          : </td>");
            bldr.Append("<td> " + BookingStatus + "</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Confirmation Date       : </td>");
            bldr.Append("<td> " + ConfirmationDate + "</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Travel Provided By      :  </td>");
            bldr.Append("<td> " + TravelProvidedBy + "</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td> Eamil ID & Phone No:  </td>");
            bldr.Append("<td> " + emailid + "</td>");
            bldr.Append("</tr>");





            string departureFrom = "", departureDate = "";
            string arrival = "", arrivalDate = "";


            if (selectionid == 3)
            {
                var request = appCommon.GetRequestById(requestId);
                if (request != null)
                {
                    var departureArray = request.Departure.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var destinationArray = request.Destination.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var departureDateArray = request.DepartureDate.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    int index = 0;
                    foreach (var item in departureArray)
                    {
                        bldr.Append("<tr>");
                        bldr.Append("<td> Departing from          :  </td>");
                        //bldr.Append("<td> " + flightSegmentDetails.Split(';')[0].ToString() + "</td>");
                        bldr.Append("<td> " + appCommon.GetAirlineCommonService(item, "Airport") + "</td>");
                        bldr.Append("</tr>");
                        if(index <= departureDateArray.Count-1)
                        {
                            bldr.Append("<tr>");
                            bldr.Append("<td> Departing Date           :  </td>");
                            //bldr.Append("<td> " + flightSegmentDetails.Split(';')[2].ToString() + "</td>");
                            bldr.Append("<td> " + departureDateArray[index] + "</td>");
                            bldr.Append("</tr>");
                        }                      
                        index++;

                    }
                    foreach (var item in destinationArray)
                    {
                        bldr.Append("<tr>");
                        bldr.Append("<td> Destination             :  </td>");
                        //bldr.Append("<td> " + flightSegmentDetails.Split(';')[1].ToString() + "</td>");
                        bldr.Append("<td> " + appCommon.GetAirlineCommonService(item, "Airport") + "</td>");
                        bldr.Append("</tr>");
                    }
                }
            }
            else
            {
                if (flights.Count > 0)
                {
                    //App.BusinessLayer.BusinessLayer appCommon = new BusinessLayer.BusinessLayer();
                    var lastFlight = flights.Count() - 1;
                    departureFrom = appCommon.GetAirlineCommonService(flights[0].DepAirportLocationCode, "Airport");
                    departureDate = DateFormat(flights[0].DepartureDateTime);
                    arrival = appCommon.GetAirlineCommonService(flights[lastFlight].ArrAirportLocationCode, "Airport");
                    arrivalDate = DateFormat(flights[lastFlight].ArrivalDateTime);
                }
                bldr.Append("<tr>");
                bldr.Append("<td> Departing from          :  </td>");
                //bldr.Append("<td> " + flightSegmentDetails.Split(';')[0].ToString() + "</td>");
                bldr.Append("<td> " + departureFrom + "</td>");
                bldr.Append("</tr>");

                bldr.Append("<tr>");
                bldr.Append("<td> Destination             :  </td>");
                //bldr.Append("<td> " + flightSegmentDetails.Split(';')[1].ToString() + "</td>");
                bldr.Append("<td> " + Destination + "</td>");
                bldr.Append("</tr>");

                bldr.Append("<tr>");
                bldr.Append("<td> Departing Date           :  </td>");
                //bldr.Append("<td> " + flightSegmentDetails.Split(';')[2].ToString() + "</td>");
                bldr.Append("<td> " + departureDate + "</td>");
                bldr.Append("</tr>");

                if (selectionid != 1)
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td> Returning Date           :  </td>");
                    //bldr.Append("<td> " + flightSegmentDetails.Split(';')[3].ToString() + "</td>");
                    bldr.Append("<td> " + arrivalDate + "</td>");
                    bldr.Append("</tr>");
                }
            }


            bldr.Append("</table>");
            bldr.Append("</td>");
            bldr.Append("</tr>");

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("<table>");

            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Itinerary Details");
            bldr.Append("</td>");
            bldr.Append("<td></td>");
            bldr.Append("</tr>");

            bldr.Append(flightSegment);
            bldr.Append("</td>");
            bldr.Append("</table>");


            bldr.Append("<table>");
            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Payment Details");
            bldr.Append("</td>");
            bldr.Append("<td></td>");
            bldr.Append("</tr>");
            bldr.Append(PaymentDetails);
            bldr.Append("</td>");
            bldr.Append("</table>");

            if (InsuranceDetails != "")
            {
                bldr.Append("<table>");
                bldr.Append("<tr>");
                bldr.Append("<td>");
                bldr.Append("Insurance Details");
                bldr.Append("</td>");
                bldr.Append("<td></td>");
                bldr.Append("</tr>");
                bldr.Append(InsuranceDetails);
                bldr.Append("</td>");
                bldr.Append("</table>");
            }
            bldr.Append("<table>");
            bldr.Append("<tr>");
            bldr.Append("<td>");
            bldr.Append("Passengers Detail");
            bldr.Append("</td>");
            bldr.Append("<td></td>");
            bldr.Append("</tr>");
            bldr.Append(passDetails);
            bldr.Append("</td>");
            bldr.Append("</table>");

            bldr.Append("<tr>");
            bldr.Append("<td width='100%'>");
            bldr.Append("RequestID : " + Base.ErrorsLog.ErrorsLogInstance.RequestID);
            bldr.Append("</td>");
            bldr.Append("</tr>");

            bldr.Append("</td>");
            bldr.Append("</tr>");
            bldr.Append("</table>");

            bldr.Append("</body>");
            bldr.Append("</html>");
            return bldr.ToString();
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body, string FromEmail, string ToEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPHostName"]);
                //SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["Host"]"webmail.skylinkgroup.com");
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(FromEmail);

                mail.To.Add(new MailAddress(recepientEmail));
                //mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailCC"]));
                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["recepientEmailCC"]))

                //need to uncomment when done with testing
                //if (ToEmail.Length > 0)
                //    mail.CC.Add(new MailAddress(ToEmail));

                mail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailBCC"]));
                mail.Subject = subject;
                mail.Body = body;

                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServerPort"]);
                //SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@skyflight.ca", "");
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);

                SmtpServer.EnableSsl = false;


                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

                //throw;
            }
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient("localhost");
            //    mail.IsBodyHtml = true;
            //    mail.From = new MailAddress("donotreply@localhost.com");
            //    mail.To.Add(new MailAddress(recepientEmail));
            //    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["recepientEmailCC"]))
            //        mail.CC.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailCC"]));

            //    mail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailBCC"]));
            //    mail.Subject = subject;
            //    mail.Body = body;

            //    SmtpServer.Port = 25;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("donotreply@localhost.com", "");
            //    SmtpServer.EnableSsl = true;

            //    SmtpServer.Send(mail);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}



            //    //mailMessage.From = new MailAddress("donotreply@localhost.com");
            //    //mailMessage.Subject = subject;
            //    //mailMessage.Body = body;
            //    //mailMessage.IsBodyHtml = true;
            //    //mailMessage.To.Add(new MailAddress(recepientEmail));

            //    //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["recepientEmailCC"]))
            //    //    mailMessage.CC.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailCC"]));

            //    //mailMessage.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailBCC"]));
            //    //SmtpClient smtp = new SmtpClient();
            //    //smtp.Host = "localhost";
            //    //smtp.EnableSsl = true;
            //    //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            //    ////smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            //    //smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            //    //NetworkCred.UserName = "donotreply@localhost.com";
            //    //NetworkCred.Password = "";
            //    ////smtp.UseDefaultCredentials = true;
            //    ////smtp.EnableSsl = true;
            //    ////smtp.Credentials = NetworkCred;
            //    //smtp.Port = 25;
            //    //smtp.Send(mailMessage);
            //}
        }

        private string CreateflightDetails(List<FlightDetails> flights, string Origin, string Destination, out string flight_details)
        {

            StringBuilder bldr = new StringBuilder();
            flight_details = "";
            string Departure = "";
            string Arrival = "";
            string DepartureDate = "";
            string ArrivalDate = "";
            App.BusinessLayer.BusinessLayer appCommon = new BusinessLayer.BusinessLayer();
            string depCode = "";
            int cnt = flights.Count;
            int total = 1;
            foreach (var flight in flights)
            {

                if (string.IsNullOrEmpty(Departure))
                {
                    depCode = flight.DepAirportLocationCode;
                    Departure = appCommon.GetAirlineCommonService(Origin, "Airport");
                    DepartureDate = DateFormat(flight.DepartureDateTime) + " (Local Time)";
                }
                if (total == cnt)
                    Arrival = appCommon.GetAirlineCommonService(Destination, "Airport");

                //Common.CommonUtility Utility = appCommon.GetCommonService("AirlinesListDetails", "SearchText");
                string Flight_Airline = appCommon.GetAirlineCommonService(flight.OperatingAirlineCode, "Airline");
                string Marketing_Airline = appCommon.GetAirlineCommonService(flight.MarketingAirline, "Airline");
                ArrivalDate = DateFormat(flight.ArrivalDateTime) + " (Local Time)";
                bldr.Append(" <tr>");
                bldr.Append(" <td>Flight No                   : </td>");
                bldr.Append(" <td>" + flight.FlightNumber + " </td>");
                bldr.Append("  </tr>");

                bldr.Append(" <tr>");
                bldr.Append(" <td>Airline Name                   : </td>");
                bldr.Append(" <td>" + Marketing_Airline + " ( " + Flight_Airline + " ) </td>");
                bldr.Append("  </tr>");


                bldr.Append(" <tr>");
                bldr.Append(" <td>Departure                                      : </td>");
                bldr.Append(" <td>" + appCommon.GetAirlineCommonService(flight.DepAirportLocationCode, "Airport") + " " + DateFormat(flight.DepartureDateTime) + "  (Local Time)" + "  </td>");
                bldr.Append(" </tr>");

                bldr.Append(" <tr>");
                bldr.Append(" <td>Arrival                                        : </td>");
                bldr.Append(" <td>" + appCommon.GetAirlineCommonService(flight.ArrAirportLocationCode, "Airport") + " " + DateFormat(flight.ArrivalDateTime) + "  (Local Time)" + " </td>");
                bldr.Append(" </tr>");
                total++;
            }
            flight_details = Departure + ";" + Arrival + ";" + DepartureDate + ";" + ArrivalDate;
            return bldr.ToString();
        }

        public string DateFormat(string dateTime)
        {
            DateTime parsedDate = new DateTime(Convert.ToInt32(dateTime.Split('T')[0].ToString().Split('-')[0].ToString()), Convert.ToInt32(dateTime.Split('T')[0].ToString().Split('-')[1].ToString()), Convert.ToInt32(dateTime.Split('T')[0].ToString().Split('-')[2].ToString()), Convert.ToInt32(dateTime.Split('T')[1].ToString().Split(':')[0].ToString()), Convert.ToInt32(dateTime.Split('T')[1].ToString().Split(':')[1].ToString()), 0);
            return String.Format("{0:dddd, MMMM d yyyy h:mm tt}", parsedDate);

        }
        private string CreateInsuranceDetails(string PolicyNumber, string PURCHASE_DATE, string Insurance_Price_array)
        {
            StringBuilder bldr = new StringBuilder();
            int cnt = 0;
            foreach (var item in Insurance_Price_array)
            {
                bldr.Append("<tr>");
                bldr.Append("<td> Passenger # " + cnt + 1 + "  : </td>");
                bldr.Append("<td>" + item + " </td>");
                bldr.Append("</tr>");

                cnt = cnt + 1;

            }
            return bldr.ToString();
        }
        private string CreatePassengerDetails(List<PassengerDetails> passengers, out string emailid)
        {
            StringBuilder bldr = new StringBuilder();
            int cnt = 0;
            emailid = "";
            foreach (var passenger in passengers)
            {
                bldr.Append("<tr>");
                bldr.Append("<td> Passenger # " + cnt + 1 + "  : </td>");
                //bldr.Append("<td>" + passenger.Surname + " " + passenger.GivenName + " </td>");
                bldr.Append("<td>" + passenger.passengername + " </td>");
                bldr.Append("</tr>");

                if (cnt == 0)
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td>Daytime Phone          : </td>");
                    bldr.Append("<td>" + passenger.PhoneNumber + " </td>");
                    bldr.Append("</tr>");

                    bldr.Append("<tr>");
                    bldr.Append("<td> E-Mail                 : </td>");
                    bldr.Append("<td>" + passenger.PassengerEmail + " </td>");
                    bldr.Append("</tr>");

                    bldr.Append("<tr>");
                    bldr.Append("<td> Address                 : </td>");
                    bldr.Append("<td>" + passenger.passengerAddress + " , " + passenger.ReservationCode + " </td>");
                    bldr.Append("</tr>");

                    if (string.IsNullOrEmpty(emailid))
                        emailid = passenger.PassengerEmail + ", " + passenger.PhoneNumber;
                }
                cnt = cnt + 1;

            }
            return bldr.ToString();
        }

        private string CreateCardDetails(List<CreateCardAndOtherDetails> CardDetails, string servicefee)
        {
            StringBuilder bldr = new StringBuilder();
            int cnt = 0;
            foreach (var card in CardDetails)
            {
                float SFEE = Convert.ToSingle(servicefee);
                string CardHolder = card.NameOnCard;
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                CardHolder = textInfo.ToTitleCase(CardHolder.ToLower());
                if (card.CardType != "CQ")
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td> Name On The Card : </td>");
                    bldr.Append("<td>" + CardHolder + " </td>");
                    bldr.Append("</tr>");
                }
                if (card.CardType != "CQ")
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td> Credit Card Charged : </td>");

                    bldr.Append("<td> ****-****-****-" + (string.IsNullOrEmpty(card.CardNumber) ? "" : card.CardNumber.Substring(12, 4)) + " </td>");
                    bldr.Append("</tr>");
                }
                bldr.Append("<tr>");
                bldr.Append("<td> Amount Charged : </td>");
                bldr.Append("<td> CAD:" + card.PaymentAmount + " </td>");
                bldr.Append("</tr>");

                if (Convert.ToSingle(card.CCFee) > 0)
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td> Credit Card Fee : </td>");
                    bldr.Append("<td  >" + card.CCFee + (SFEE > 0 ? "<span style='color:red'> ( Separate charge of Credit Card fee will appear on the credit card ) " : "") + "</span> </td>");
                    bldr.Append("</tr>");
                }

                if (SFEE > 0)
                {
                    bldr.Append("<tr>");
                    bldr.Append("<td> Service Fee : </td>");
                    bldr.Append("<td>" + servicefee + "<span style='color:red'> (Separate charge of service fee will appear on the credit card) </span></td>");
                    bldr.Append("</tr>");
                }
            }
            return bldr.ToString();
        }
        public string executeWebRequest(string requestXml, string eventName)
        {
            Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(requestXml, eventName + "RQ");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(requestXml);
            String xml = doc.OuterXml;
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }
            try
            {
                Stream stResponse = req.GetResponse().GetResponseStream();
                using (StreamReader responseReader = new StreamReader(stResponse))
                {
                    result = responseReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(pageContent, eventName + "RS");
                return pageContent;
            }
            Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(result, eventName + "RS");
            return result;
        }

        public string executeCCWebRequest(string requestXml, string eventName)
        {
            Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(requestXml, eventName + "RQ");
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(requestXml);

            String xml = doc.OuterXml;

            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";

            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }
            try
            {
                Stream stResponse = req.GetResponse().GetResponseStream();
                using (StreamReader responseReader = new StreamReader(stResponse))
                {
                    result = responseReader.ReadToEnd();

                    result = replaceMessage(result);
                    Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(result, eventName + "RS");
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(result);

                    return doc1.SelectSingleNode("//Body").InnerText;
                    //binarysecuritytoken = doc1.SelectSingleNode("//BinarySecurityToken").InnerText;
                    //return binarysecuritytoken;
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(pageContent, eventName + "RS");
                return pageContent;
            }
        }
        /// <returns>ResultCode, 1 if success.</returns>

        public string SessionCreate(string pcc, string username, string password)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(getSessionRequest(pcc, username, password));
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = true;
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(doc.OuterXml);
                }
            }
            Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(doc.ToString(), "CreateToken-" + "RQ");
            try
            {
                using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    result = responseReader.ReadToEnd();
                    result = replaceMessage(result);
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(result);
                    sessionconversionid = doc1.SelectSingleNode("//ConversationId").InnerText;
                    binarysecuritytoken = doc1.SelectSingleNode("//BinarySecurityToken").InnerText;
                    return binarysecuritytoken;
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(result, "CreateToken-" + "RS");
                return pageContent;
            }
        }

        public string GetApplicationResult(string result)
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.LoadXml(result);
            string res = doc1.SelectSingleNode("//*[local-name()='Body']/*").FirstChild.Attributes[0].Value;
            string ret = string.Empty;
            ret = res;
            if (res != "Complete")
            {
                ret = doc1.SelectSingleNode("//*[local-name()='Body']/*").FirstChild.InnerText;
            }
            return ret;
        }

        public string GetApplicationResultBooking(string result, out string status)
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.LoadXml(result);
            XmlNode res = doc1.SelectSingleNode("//*[local-name()='Body']/*");
            string ret = string.Empty;

            XmlDocument filteredDocument = new XmlDocument();
            filteredDocument.LoadXml(res.OuterXml);
            status = "";
            foreach (XmlNode node in filteredDocument.DocumentElement.ChildNodes)
            {
                if (node.Name == "ApplicationResults")
                {
                    ret = node.Attributes["status"].InnerText;
                }
                if (node.Name == "OTA_AirBookRS")
                {
                    foreach (XmlNode cnodes in node)
                    {
                        if (cnodes.Name == "OriginDestinationOption")
                        {
                            foreach (XmlNode flight in cnodes)
                            {
                                if (flight.Name == "FlightSegment")
                                {
                                    status = flight.Attributes["Status"].InnerText;
                                }
                            }
                        }
                    }
                }
            }

            if (ret != "Complete")
            {
                ret = doc1.SelectSingleNode("//*[local-name()='Body']/*").FirstChild.InnerText;
            }
            return ret;
        }

        private String replaceMessage(String msg)
        {

            String message = msg.Replace(" xmlns:soap-env=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            message = message.Replace(" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\"", "");
            message = message.Replace(" xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\"", "");

            message = message.Replace(" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\"", "");
            message = message.Replace(" xmlns:swse=\"http://wse.sabre.com/eventing\"", "");
            message = message.Replace(" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"", "");
            message = message.Replace(" xmlns:wse=\"http://schemas.xmlsoap.org/ws/2004/08/eventing\"", "");
            message = message.Replace(" eb:version=\"1.0\" soap-env:mustUnderstand=\"1\"", "");
            message = message.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>", "");
            message = message.Replace("<? xml version=\"1.0\" encoding=\"UTF-8\" ?>", "");

            message = message.Replace("wse:", "");

            message = message.Replace("wsse:", "");
            message = message.Replace("swse:", "");
            message = message.Replace("eb:", "");
            message = message.Replace("wsa:", "");
            message = message.Replace("sCCC.", "");
            message = message.Replace("stl:", "");
            message = message.Replace("soap-env:", "");

            return message;

        }

        public string SessionClose(string token, string pcc)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(getSessionCloseRequest(token, pcc));

            String xml = doc.OuterXml;

            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";

            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }
            try
            {
                using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    result = responseReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                return pageContent;
            }
            return result;
        }

        public string ReadRetrievePNR(string pcc, string token, string Id, string tStamp, string subArea)
        {

            XmlDocument doc = new XmlDocument();
            string result = "";
            return result = executeWebRequest(getTravelItineraryReadRequest(pcc, token, Id, tStamp, subArea), "RetrievePNR");
        }

        public string checkCCVerificationRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode)
        {
            string result = "";
            result = executeCCWebRequest(getCCVerificationRequest(token, pcc, retain, code, airlineCode, ccNumber, expDate, secCode, amount, currCode), "CCVerification");
            return result;
        }


        public string AddRemarkccRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name)
        {
            string result = "";
            result = executeCCWebRequest(getAirAddccRemarkRequest(token, pcc, retain, code, airlineCode, ccNumber, expDate, secCode, amount, currCode, Address, Name), "AddRemark");
            return result;
        }
        public string AddRemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name, string CCFee, string ServiceFee, string BillingEmail, string BillingPhone, string PolicyCode, string TotalInsurancePrice)
        {
            string result = "";
            result = executeCCWebRequest(getAirAddRemarkRequest(token, pcc, retain, code, airlineCode, ccNumber, expDate, secCode, amount, currCode, Address, Name, CCFee, ServiceFee, BillingEmail, BillingPhone, PolicyCode, TotalInsurancePrice), "AddRemark");
            return result;
        }
        public string AddServiceCommissionRemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name, string type, string message)
        {
            string result = "";
            result = executeCCWebRequest(getAirAddServiceCommissionRemarkRequest(token, pcc, retain, code, airlineCode, ccNumber, expDate, secCode, amount, currCode, Address, Name, message), type);
            return result;
        }

        public string AddCCFEERemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name, string type, string message)
        {
            string result = "";
            result = executeCCWebRequest(getAirAddCCFEECommissionRemarkRequest(token, pcc, retain, code, airlineCode, ccNumber, expDate, secCode, amount, currCode, Address, Name, message), type);
            return result;
        }
        public string airChangePCC(string token, string pcc, string newpcc)
        {
            return executeWebRequest(getChangeAAARequest(token, pcc, newpcc), "ChangePCC");
        }

        public string airEnhancedBookMultiSegmentRQ(string pcc, string token, List<FlightDetails> flights)
        {
            string result = "";
            result = executeWebRequest(getMultiSegmentEnhancedBookingRequest(pcc, token, flights), "EnhancedBookMultiSegment");
            return result;
        }
        public string airEnhancedBookArunkRQ(string pcc, string token)
        {
            string result = "";
            result = executeWebRequest(getArunkEnhancedBookingRequest(pcc, token), "EnhancedBookArunk");
            return result;
        }

        public string airPriceRequest(string pcc, string token, string retain, List<AirNoofPassengers> pQuantity, string CurrencyCode)
        {
            string result = "";
            result = executeWebRequest(getAirPriceRequest(pcc, token, retain, pQuantity, CurrencyCode), "AirPrice");
            return result;
        }

        public string addPassengerRequest(string pcc, string token, string ignoreAfter, string redispResv, string usCC, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string SText, List<PassengerDetails> passengersList, string remark)
        {
            string result = "";
            result = executeWebRequest(getPassengerInfoRequest(pcc, token, ignoreAfter, redispResv, usCC, AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber, AgencyTicketTime, AgencyTicketType, AgencyQueueNo, SText, passengersList, remark), "PassengerDetails");
            return result;
        }

        public string endTransaction(string pcc, string token, string ind, string receivedFrom)
        {
            return executeWebRequest(getEndTransactionRequest(pcc, token, ind, receivedFrom), "EndTransaction");
        }

        #endregion

        #region "Create Request XML"

        public FlightPassengerDetails readPNRXML(string xmlFileName)
        {
            FlightPassengerDetails Flight_Passenger_Details = new FlightPassengerDetails();

            bool isTrue = true; ;
            List<PassengerDetails> passengers = new List<PassengerDetails>();
            PassengerDetails passenger = new PassengerDetails();
            List<FlightDetails> flights = new List<FlightDetails>();
            FlightDetails flight = new FlightDetails();
            CreateCardAndOtherDetails payment = new SOAPServices.CreateCardAndOtherDetails();
            XmlDocument xDoc = new XmlDocument();
            string pnrNumber = string.Empty;
            string bookingDate = string.Empty;
            string AirLineReservationCode = string.Empty;
            xDoc.LoadXml(xmlFileName);
            XmlDocument filteredDocument = null;
            XmlNode filteredResponse = xDoc.SelectSingleNode("//*[local-name()='Body']/*");
            filteredDocument = new XmlDocument();
            filteredDocument.LoadXml(filteredResponse.OuterXml);
            bool ispassAdd = false;
            foreach (XmlNode node in filteredDocument.DocumentElement.ChildNodes)
            {
                if (node.Name == "TravelItinerary")
                {
                    foreach (XmlNode mainNode in node.ChildNodes)
                    {
                        if (mainNode.Name == "CustomerInfo")
                        {
                            foreach (XmlNode custNode in mainNode.ChildNodes)
                            {

                                ispassAdd = false;
                                if (custNode.Name == "ContactNumbers")
                                {
                                    foreach (XmlNode contactNode in custNode.ChildNodes)
                                    {
                                        if (contactNode.Name == "ContactNumber")
                                        {
                                            passenger.PhoneLocationCode = contactNode.Attributes["LocationCode"].InnerText;
                                            passenger.PhoneNumber = contactNode.Attributes["Phone"].InnerText;
                                        }
                                    }
                                }
                                if (custNode.Name == "Address")
                                {
                                    foreach (XmlNode addressNode in custNode.ChildNodes)
                                    {
                                        if (addressNode.Name == "AddressLine")
                                        {
                                            passenger.passengerAddress = passenger.passengerAddress + "</br>" + addressNode.InnerText;
                                        }
                                    }
                                }
                                if (custNode.Name == "PersonName")
                                {
                                    ispassAdd = true;
                                    foreach (XmlNode personNode in custNode.ChildNodes)
                                    {
                                        if (personNode.Name == "GivenName")
                                        {
                                            var passengername = string.Empty;
                                            if (!string.IsNullOrEmpty(personNode.InnerText))
                                            {
                                                var splitBy = new char[] { ' ' };
                                                var nameArr = personNode.InnerText.Split(splitBy, StringSplitOptions.RemoveEmptyEntries);

                                                if (nameArr.Length == 3)
                                                {
                                                    passengername = nameArr[2] + " " + nameArr[0] + " " + nameArr[1];
                                                }
                                                else if (nameArr.Length == 2)
                                                {
                                                    passengername = nameArr[1] + " " + nameArr[0];
                                                }
                                                else
                                                {
                                                    for (int i = nameArr.Length - 1; i >= 0; i--)
                                                    {
                                                        passengername += nameArr[i] + " ";
                                                    }
                                                }
                                            }
                                            passenger.passengername = passengername;
                                        }
                                        else if (personNode.Name == "Surname")
                                        {
                                            passenger.Surname = personNode.InnerText;
                                        }
                                        else if (personNode.Name == "Email")
                                        {
                                            passenger.email = personNode.InnerText;
                                        }
                                    }
                                }
                                if (ispassAdd)
                                {
                                    passengers.Add(passenger);
                                    passenger = new PassengerDetails();
                                }
                                if (custNode.Name == "PaymentInfo")
                                {
                                    foreach (XmlNode itienPaymentNode in custNode.ChildNodes)
                                    {
                                        if (itienPaymentNode.Name == "Payment")
                                        {
                                            foreach (XmlNode itienCCNode in itienPaymentNode.ChildNodes)
                                            {
                                                if (itienCCNode.Name == "CC_Info")
                                                {
                                                    foreach (XmlNode itienPaymentcardNode in itienCCNode.ChildNodes)
                                                    {
                                                        if (itienPaymentcardNode.Name == "CardHolderInfo")
                                                        {
                                                            payment.NameOnCard = itienPaymentcardNode.Attributes["Name"].InnerText;
                                                            foreach (XmlNode itienPaymentAddressNode in itienCCNode.ChildNodes)
                                                            {
                                                                if (itienPaymentAddressNode.Name == "Address")
                                                                {
                                                                    foreach (XmlNode addressNode in itienPaymentAddressNode.ChildNodes)
                                                                    {
                                                                        if (addressNode.Name == "AddressLine")
                                                                        {
                                                                            payment.Address = payment.Address + addressNode.InnerText;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (mainNode.Name == "AccountingInfo")
                        {
                            foreach (XmlNode itienAccountNode in mainNode.ChildNodes)
                            {
                                if (itienAccountNode.Name == "BaseFare")
                                {
                                    payment.PaymentAmount = itienAccountNode.Attributes["Amount"].InnerText;
                                }
                                else if (itienAccountNode.Name == "PaymentInfo")
                                {
                                    foreach (XmlNode itienPaymentNode in itienAccountNode.ChildNodes)
                                    {
                                        if (itienPaymentNode.Name == "Payment")
                                        {
                                            foreach (XmlNode itienCCNode in itienPaymentNode.ChildNodes)
                                            {
                                                if (itienCCNode.Name == "CC_Info")
                                                {
                                                    foreach (XmlNode itienPaymentcardNode in itienCCNode.ChildNodes)
                                                    {
                                                        if (itienPaymentcardNode.Name == "PaymentCard")
                                                        {
                                                            payment.CardNumber = itienAccountNode.Attributes["Number"].InnerText;
                                                            payment.CurrCode = itienAccountNode.Attributes["Code"].InnerText;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (mainNode.Name == "ItineraryInfo")
                        {
                            foreach (XmlNode itienNode in mainNode.ChildNodes)
                            {
                                if (itienNode.Name == "ReservationItems")
                                {
                                    foreach (XmlNode resItemNode in itienNode.ChildNodes)
                                    {
                                        if (resItemNode.Name == "Item")
                                        {
                                            foreach (XmlNode flightItemNode in resItemNode.ChildNodes)
                                            {
                                                flight = new FlightDetails();
                                                if (flightItemNode.Name == "FlightSegment")
                                                {
                                                    foreach (XmlNode flightNode in flightItemNode.ChildNodes)
                                                    {
                                                        if (flightItemNode.Attributes["Status"].InnerText != "SS")
                                                            isTrue = false;

                                                        flight.status = isTrue.ToString();
                                                        flight.DepartureDate = flightItemNode.Attributes["DepartureDateTime"].InnerText.Split('T')[0].ToString();
                                                        flight.DepartureTime = flightItemNode.Attributes["DepartureDateTime"].InnerText.Split('T')[1].ToString();
                                                        flight.DepartureDateTime = flightItemNode.Attributes["DepartureDateTime"].InnerText.ToString();

                                                        int year = Convert.ToInt32(flight.DepartureDate.Split('-')[0].ToString());
                                                        int month = Convert.ToInt32(flight.DepartureDate.Split('-')[1].ToString());
                                                        int day = Convert.ToInt32(flight.DepartureDate.Split('-')[2].ToString());
                                                        int hour = Convert.ToInt32(flight.DepartureTime.Split(':')[0].ToString());
                                                        int min = Convert.ToInt32(flight.DepartureTime.Split(':')[1].ToString());
                                                        DateTime a = new DateTime(year, month, day, hour, min, 00);
                                                        flight.DepartureDate = String.Format("{0:ddd, MMM dd yyyy}", a);
                                                        flight.ArrivalDate = flightItemNode.Attributes["ArrivalDateTime"].InnerText.Split('T')[0].ToString();
                                                        flight.ArrivalTime = flightItemNode.Attributes["ArrivalDateTime"].InnerText.Split('T')[1].ToString();
                                                        month = Convert.ToInt32(flight.ArrivalDate.Split('-')[0].ToString());
                                                        day = Convert.ToInt32(flight.ArrivalDate.Split('-')[1].ToString());
                                                        hour = Convert.ToInt32(flight.ArrivalTime.Split(':')[0].ToString());
                                                        min = Convert.ToInt32(flight.ArrivalTime.Split(':')[1].ToString());
                                                        DateTime b = new DateTime(year, month, day, hour, min, 00);

                                                        flight.ArrivalDateTime1 = year.ToString() + "-" + flightItemNode.Attributes["ArrivalDateTime"].InnerText.ToString();

                                                        flight.ArrivalDate = String.Format("{0:ddd, MMM dd yyyy}", b);
                                                        flight.FlightNumber = flightItemNode.Attributes["FlightNumber"].InnerText;
                                                        flight.ElapsedTime = flightItemNode.Attributes["ElapsedTime"].InnerText;
                                                        flight.DistanceTravel = flightItemNode.Attributes["AirMilesFlown"].InnerText;
                                                        flight.Stops = flightItemNode.Attributes["StopQuantity"].InnerText;
                                                        flight.DayOfWeekInd = flightItemNode.Attributes["DayOfWeekInd"].InnerText;

                                                        var diff = b.Subtract(a);
                                                        var res = String.Format("{0}:{1}:{2}", diff.Hours, diff.Minutes, diff.Seconds);
                                                        flight.ArrivalDateTime = res;

                                                        if (flightNode.Name == "SupplierRef")
                                                        {
                                                            if (flightNode.Attributes["ID"] != null)
                                                            {
                                                                AirLineReservationCode = flightNode.Attributes["ID"].InnerText;
                                                            }
                                                        }
                                                        if (flightNode.Name == "DestinationLocation")
                                                        {
                                                            if (flightNode.Attributes["LocationCode"] != null)
                                                            {
                                                                flight.ArrivalCityCode = flightNode.Attributes["LocationCode"].InnerText;
                                                            }
                                                            if (flightNode.Attributes["Terminal"] != null)
                                                            {
                                                                flight.ArrivalTerminal = flightNode.Attributes["Terminal"].InnerText;
                                                            }
                                                        }
                                                        else if (flightNode.Name == "Equipment")
                                                        {
                                                            if (flightNode.Attributes["AirEquipType"] != null)
                                                            {
                                                                flight.AircraftType = flightNode.Attributes["AirEquipType"].InnerText;
                                                            }
                                                        }
                                                        else if (flightNode.Name == "OperatingAirline")
                                                        {
                                                            flight.AirlineName = flightNode.Attributes["Code"].InnerText;
                                                        }
                                                        else if (flightNode.Name == "MarketingAirline")
                                                        {
                                                            flight.MarketingAirline = flightNode.Attributes["Code"].InnerText;
                                                        }
                                                        else if (flightNode.Name == "OriginLocation")
                                                        {
                                                            if (flightNode.Attributes["LocationCode"] != null)
                                                            {
                                                                flight.DepartureCityCode = flightNode.Attributes["LocationCode"].InnerText;
                                                            }
                                                            if (flightNode.Attributes["Terminal"] != null)
                                                            {
                                                                flight.DepartureTerminal = flightNode.Attributes["Terminal"].InnerText;
                                                            }
                                                        }
                                                    }
                                                }
                                                flights.Add(flight);
                                            }
                                        }
                                    }
                                }
                                else if (itienNode.Name == "ItineraryPricing")
                                {
                                    foreach (XmlNode priceNode in itienNode.ChildNodes)
                                    {
                                        if (priceNode.Name == "PriceQuoteTotals")
                                        {
                                            foreach (XmlNode priceamtNode in priceNode.ChildNodes)
                                            {
                                                if (priceamtNode.Name == "TotalFare")
                                                {

                                                    payment.PaymentAmount = priceamtNode.Attributes["Amount"].InnerText;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (mainNode.Name == "ItineraryRef")
                        {
                            pnrNumber = mainNode.Attributes["ID"].InnerText;
                            foreach (XmlNode item1 in mainNode.ChildNodes)
                            {
                                if (item1.Name == "Source")
                                {
                                    bookingDate = item1.Attributes["CreateDateTime"].InnerText;
                                }
                            }

                        }
                        else if (mainNode.Name == "RemarkInfo")
                        {
                            foreach (XmlNode item1 in mainNode.ChildNodes)
                            {
                                if (item1.Name == "Remark")
                                {
                                    if (item1.Attributes["RPH"].Value == "001")
                                    {
                                        payment.Address = item1.InnerText;
                                    }
                                    if (item1.Attributes["RPH"].Value == "003")
                                    {
                                        payment.NameOnCard = item1.InnerText;
                                    }
                                    if (item1.Attributes["RPH"].Value == "004")
                                    {
                                        payment.CardNumber = item1.InnerText;
                                    }
                                    if (item1.Attributes["RPH"].Value == "005")
                                    {
                                        payment.CardExpiryDate = item1.InnerText;
                                    }
                                    if (item1.Attributes["RPH"].Value == "006")
                                    {
                                        // payment.PaymentAmount = item1.InnerText;
                                    }
                                    if (item1.Attributes["RPH"].Value == "007")
                                    {
                                        //Ph number
                                    }
                                }
                            }

                        }
                    }
                }
            }
            Flight_Passenger_Details.passengers_details = passengers;
            Flight_Passenger_Details.BookingDate = bookingDate;
            Flight_Passenger_Details.flights_details = flights;
            Flight_Passenger_Details.pnrnumber = pnrNumber;
            Flight_Passenger_Details.AirLineReservationCode = AirLineReservationCode;
            Flight_Passenger_Details.currCode_details = payment;
            return Flight_Passenger_Details;
        }

        private String getSessionRequest(string pcc, string _Username, string _Password)
        {

            //String username = ConfigurationManager.AppSettings["username"].ToString();
            //String password = ConfigurationManager.AppSettings["password"].ToString();

            String username = _Username;
            String password = _Password;

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:mes=\"http://www.ebxml.org/namespaces/messageHeader\" xmlns:ns=\"http://www.opentravel.org/OTA/2002/11\">");
            bldr.Append(" <soapenv:Header>");
            bldr.Append("     <sec:Security  xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("        <sec:UsernameToken>");
            bldr.Append("           <sec:Username>" + username + "</sec:Username>");
            bldr.Append("           <sec:Password>" + password + "</sec:Password>");
            bldr.Append("           <Organization>" + pcc + "</Organization>");
            bldr.Append("           <Domain>AA</Domain>");
            bldr.Append("         </sec:UsernameToken>");
            bldr.Append("     </sec:Security>");
            bldr.Append("      <mes:MessageHeader  mes:mustUnderstand=\"1\" mes:id=\"1000\" mes:version=\"3.6.0\">");
            bldr.Append("        <mes:From>");
            bldr.Append("          <mes:PartyId mes:type=\"urn: x12.org:IO5:01\">999999</mes:PartyId>");
            bldr.Append("        </mes:From>");
            bldr.Append("       <mes:To>");
            bldr.Append("           <mes:PartyId mes:type=\"urn: x12.org:IO5:01\">123123</mes:PartyId>");
            bldr.Append("         </mes:To>");
            bldr.Append("         <mes:CPAId>" + pcc + "</mes:CPAId>");
            bldr.Append("        <mes:ConversationId>1111111111111111111</mes:ConversationId>");
            bldr.Append("        <mes:Service mes:type=\"OTA\">SessionCreateRQ</mes:Service>");
            bldr.Append("        <mes:Action>SessionCreateRQ</mes:Action>");
            bldr.Append("         <mes:MessageData>");
            bldr.Append("           <mes:MessageId>1000</mes:MessageId>");
            bldr.Append("            <mes:Timestamp>2016-05-11T19:20:10Z</mes:Timestamp>");
            bldr.Append("            <mes:TimeToLive>2016-05-11T19:20:10Z</mes:TimeToLive>");
            bldr.Append("         </mes:MessageData>");
            bldr.Append("     </mes:MessageHeader>");
            bldr.Append("   </soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("      <ns:SessionCreateRQ>");
            bldr.Append("         <ns:POS>");
            bldr.Append("            <ns:Source PseudoCityCode=\"" + pcc + "\"/>");
            bldr.Append("         </ns:POS>");
            bldr.Append("      </ns:SessionCreateRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getChangeAAARequest(string token, string pcc, string newpcc)
        {

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">ContextChangeLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>ContextChangeLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("    <ContextChangeRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.3\">");
            bldr.Append("         <ChangeAAA  PseudoCityCode=\"" + newpcc + "\"/>");
            bldr.Append("    </ContextChangeRQ >");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        //MultiSegmentBookings
        private String getMultiSegmentEnhancedBookingRequest(string pcc, string token, List<FlightDetails> flights)
        {
            string flightsegments = flightDetails(flights, pcc);
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">EnhancedAirBookRQ</eb:Service>");
            bldr.Append("       <eb:Action>EnhancedAirBookRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Book Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("   <sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("       <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("   </sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <EnhancedAirBookRQ  xmlns=\"http://services.sabre.com/sp/eab/v3_6\"  version=\"3.6.0\" IgnoreOnError=\"true\" HaltOnError=\"false\" >");
            bldr.Append("     <OTA_AirBookRQ>");
            bldr.Append("   <RetryRebook Option=\"true\"/>");
            bldr.Append("   <HaltOnStatus Code=\"NO\"/>");
            bldr.Append("   <HaltOnStatus Code=\"NN\"/>");
            bldr.Append("   <HaltOnStatus Code=\"UC\"/>");
            bldr.Append("   <HaltOnStatus Code=\"US\"/>");
            bldr.Append("   <HaltOnStatus Code=\"WL\"/>");
            bldr.Append("   <HaltOnStatus Code=\"HL\"/>");
            bldr.Append("   <HaltOnStatus Code=\"UN\"/>");
            bldr.Append("   <HaltOnStatus Code=\"PN\"/>");
            bldr.Append("   <HaltOnStatus Code=\"LL\"/>");
            bldr.Append("      <OriginDestinationInformation>");
            bldr.Append(flightsegments);
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     <RedisplayReservation NumAttempts=\"2\" WaitInterval=\"5000\"/>");
            bldr.Append("     </OTA_AirBookRQ>");
            bldr.Append(" <PostProcessing IgnoreAfter =\"false\">");
            bldr.Append("   <RedisplayReservation WaitInterval =\"5000\"/>");
            bldr.Append(" </PostProcessing>");
            bldr.Append("    </EnhancedAirBookRQ>");
            bldr.Append("   </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private String getArunkEnhancedBookingRequest(string pcc, string token)
        {
            StringBuilder bldr = new StringBuilder();

            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">ARUNK_LLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>ARUNK_LLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Book Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("   <sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("       <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("   </sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            //<ARUNK_RQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.2\" />
            bldr.Append("    <ARUNK_RQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.2\" />");
            bldr.Append("   </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private string flightDetails(List<FlightDetails> flights, string pcc)
        {
            StringBuilder bldr = new StringBuilder();
            int count = 1;
            foreach (var flight in flights)
            {
                //if (count == 3)
                //{
                //    string addarunk=airEnhancedBookArunkRQ(pcc, binarysecuritytoken);
                //}
                bldr.Append("   <FlightSegment DepartureDateTime=\"" + flight.DepartureDateTime + "\" ArrivalDateTime=\"" + flight.ArrivalDateTime + "\" FlightNumber=\"" + flight.FlightNumber + "\" NumberInParty=\"" + flight.NoofPassengers + "\" ResBookDesigCode=\"" + flight.resBookDesigCode + "\" Status=\"" + flight.status + "\">");
                bldr.Append("      <DestinationLocation   LocationCode=\"" + flight.ArrAirportLocationCode + "\"/>");
                bldr.Append("      <Equipment AirEquipType=\"" + flight.Equipment + "\"/>");
                bldr.Append("      <MarketingAirline Code=\"" + flight.MarketingAirline + "\" FlightNumber=\"" + flight.FlightNumber + "\"/>");
                bldr.Append("      <OperatingAirline Code=\"" + flight.OperatingAirlineCode + "\"/>");
                bldr.Append("      <OriginLocation   LocationCode=\"" + flight.DepAirportLocationCode + "\"/>");
                bldr.Append("     </FlightSegment>");
                count += 1;
            }
            return bldr.ToString();
        }

        private string passengerDetails(List<PassengerDetails> passengers)
        {
            StringBuilder bldr = new StringBuilder();
            int cnt = 0;

            foreach (var passenger in passengers)
            {
                if (cnt == 0)
                {
                    bldr.Append("<ContactNumbers>");
                    bldr.Append("<ContactNumber  NameNumber=\"" + passenger.PassengerNameNumber + "\" Phone=\"" + passenger.PhoneNumber + "\" PhoneUseType=\"" + passenger.PhoneUseType + "\"/>");
                    bldr.Append("<ContactNumber PhoneUseType=\"A\" Phone=\"18773897388\" />");
                    bldr.Append("</ContactNumbers>");
                    bldr.Append("<CustomerIdentifier>IBM301</CustomerIdentifier>");
                    // var passengerEmail = string.IsNullOrEmpty(passenger.PassengerEmail) ? string.Empty : passenger.PassengerEmail.Replace("@", "ad");
                    bldr.Append("<Email Address=\"" + passenger.PassengerEmail + "\" NameNumber=\"" + passenger.PassengerNameNumber + "\" />");
                    bldr.Append("<Email Address=\"reservations@skyflight.ca\" />");
                }
                cnt = cnt + 1;
                bldr.Append("          <PersonName Infant=\"" + passenger.IsInfant + "\" NameNumber=\"" + passenger.PassengerNameNumber + "\"   PassengerType =\"" + passenger.passengerType + "\">");
                bldr.Append("               <GivenName>" + passenger.GivenName + "</GivenName>");
                bldr.Append("               <Surname>" + passenger.Surname + "</Surname>");
                bldr.Append("          </PersonName>");

            }
            return bldr.ToString();
        }

        private String getAirPriceRequest(string pcc, string token, string retain, List<AirNoofPassengers> pQuantity, string CurrencyCode)
        {
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">OTA_AirPriceLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>OTA_AirPriceLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Price Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <OTA_AirPriceRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.14.0\">");
            bldr.Append("       <PriceRequestInformation Retain=\"" + retain + "\">");
            bldr.Append("          <OptionalQualifiers>");
            bldr.Append("              <PricingQualifiers CurrencyCode=\"" + CurrencyCode + "\"> ");
            foreach (var pass in pQuantity)
            {
                if (pass.NoOfPassengers > 0)
                {
                    bldr.Append("     <PassengerType  Code=\"" + pass.PassengerCode + "\" Quantity=\"" + pass.NoOfPassengers + "\"/>");
                }
            }
            bldr.Append("             </PricingQualifiers>");
            bldr.Append("          </OptionalQualifiers>");
            bldr.Append("       </PriceRequestInformation>");
            bldr.Append("    </OTA_AirPriceRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private string setPriceQuoteInfo(List<PassengerDetails> passengers)
        {
            StringBuilder bldr = new StringBuilder();
            bldr.Append(" <PriceQuoteInfo>");
            bool isChild = false;
            foreach (var pass in passengers)
            {
                string record = null;
                if (pass.passengerType == "ADT")
                {
                    record = "1";
                }
                if (pass.passengerType == "CNN")
                {
                    record = "2";
                    isChild = true;
                }
                if (pass.passengerType == "INF")
                {
                    if (!isChild)
                        record = "2";
                    else
                        record = "3";
                }
                bldr.Append(" <Link NameNumber=\"" + pass.PassengerNameNumber + "\" Record=\"" + record + "\"/>");
            }
            bldr.Append("</PriceQuoteInfo>");

            return bldr.ToString();
        }

        private string secureFlightInfo(List<PassengerDetails> passengers)
        {
            string segNumber = "A";
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<SpecialServiceInfo>");
            foreach (var pass in passengers)
            {
                if (pass.IsInfant == "true" && (pass.passengerType == "INF"))
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<SecureFlight  SegmentNumber=\"" + segNumber + "\">");
                    bldr.Append("   <PersonName DateOfBirth=\"" + dt.ToString("yyyy-MM-dd").ToUpper() + "\" Gender=\"" + pass.Gender + "\" NameNumber=\"1.1\" >");
                    bldr.Append("        <GivenName>" + pass.GivenName + "</GivenName>");
                    bldr.Append("        <Surname>" + pass.Surname + "</Surname>");
                    bldr.Append("   </PersonName>");
                    //bldr.Append("   <VendorPrefs>");
                    //bldr.Append("       <Airline Hosted=\"false\"/>");
                    //bldr.Append("   </VendorPrefs>");
                    bldr.Append("</SecureFlight>");
                }
                else if (pass.IsInfant == "true" && (pass.passengerType == "INS"))
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<SecureFlight  SegmentNumber=\"" + segNumber + "\" >");
                    bldr.Append("   <PersonName DateOfBirth=\"" + dt.ToString("yyyy-MM-dd").ToUpper() + "\" Gender=\"" + pass.Gender + "\" NameNumber=\"" + pass.PassengerNameNumber + "\" >");
                    bldr.Append("        <GivenName>" + pass.GivenName + "</GivenName>");
                    bldr.Append("        <Surname>" + pass.Surname + "</Surname>");
                    bldr.Append("   </PersonName>");
                    //bldr.Append("   <VendorPrefs>");
                    //bldr.Append("       <Airline Hosted=\"false\"/>");
                    //bldr.Append("   </VendorPrefs>");
                    bldr.Append("</SecureFlight>");
                }
                else if (pass.passengerType == "CNN")
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<SecureFlight  SegmentNumber=\"" + segNumber + "\" >");
                    bldr.Append("   <PersonName DateOfBirth=\"" + dt.ToString("yyyy-MM-dd").ToUpper() + "\" Gender=\"" + pass.Gender + "\" NameNumber=\"" + pass.PassengerNameNumber + "\" >");
                    bldr.Append("        <GivenName>" + pass.GivenName + "</GivenName>");
                    bldr.Append("        <Surname>" + pass.Surname + "</Surname>");
                    bldr.Append("   </PersonName>");
                    //bldr.Append("   <VendorPrefs>");
                    //bldr.Append("       <Airline Hosted=\"false\"/>");
                    //bldr.Append("   </VendorPrefs>");
                    bldr.Append("</SecureFlight>");
                }
                else
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<SecureFlight  SegmentNumber=\"" + segNumber + "\">");
                    bldr.Append("   <PersonName DateOfBirth=\"" + dt.ToString("yyyy-MM-dd").ToUpper() + "\" Gender=\"" + pass.Gender + "\" NameNumber=\"" + pass.PassengerNameNumber + "\" >");
                    bldr.Append("        <GivenName>" + pass.GivenName + "</GivenName>");
                    bldr.Append("        <Surname>" + pass.Surname + "</Surname>");
                    bldr.Append("   </PersonName>");
                    //bldr.Append("   <VendorPrefs>");
                    //bldr.Append("       <Airline Hosted=\"false\"/>");
                    //bldr.Append("   </VendorPrefs>");
                    bldr.Append("</SecureFlight>");
                }
            }
            foreach (var pass in passengers)
            {
                if (pass.IsInfant == "true" && (pass.passengerType == "INF"))
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<Service SegmentNumber=\"" + segNumber + "\" SSR_Code=\"INFT\">");
                    bldr.Append("   <PersonName NameNumber=\"1.1\"/>");
                    bldr.Append("   <Text>" + pass.Surname + "/" + pass.GivenName + "/" + dt.ToString("ddMMMyy").ToUpper() + "</Text>");
                    bldr.Append("   <VendorPrefs>");
                    bldr.Append("       <Airline Hosted=\"false\"/>");
                    bldr.Append("   </VendorPrefs>");
                    bldr.Append("</Service>");
                }
                else if (pass.IsInfant == "true" && (pass.passengerType == "INS"))
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<Service SegmentNumber=\"" + segNumber + "\" SSR_Code=\"INFT\">");
                    bldr.Append("   <PersonName NameNumber=\"" + pass.PassengerNameNumber + "\"/>");
                    bldr.Append("   <Text>" + pass.Surname + "/" + pass.GivenName + "/" + dt.ToString("ddMMMyy").ToUpper() + "</Text>");
                    bldr.Append("   <VendorPrefs>");
                    bldr.Append("       <Airline Hosted=\"false\"/>");
                    bldr.Append("   </VendorPrefs>");
                    bldr.Append("</Service>");
                }
                else if (pass.passengerType == "CNN")
                {
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(pass.DateofBirth);
                    bldr.Append("<Service SSR_Code=\"CHLD\">");
                    bldr.Append("   <PersonName NameNumber=\"" + pass.PassengerNameNumber + "\"/>");
                    bldr.Append("   <Text>" + dt.ToString("ddMMMyy").ToUpper() + "</Text>");
                    bldr.Append("   <VendorPrefs>");
                    bldr.Append("       <Airline Hosted=\"false\"/>");
                    bldr.Append("   </VendorPrefs>");
                    bldr.Append("</Service>");
                }

            }
            bldr.Append("</SpecialServiceInfo>");
            return bldr.ToString();
        }

        private String getPassengerInfoRequest(string pcc, string token, string ignoreAfter, string redispResv, string usCC, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string SText, List<PassengerDetails> passengers, string CCAddressLine)
        {
            string passenger = passengerDetails(passengers);
            string secureFlightInf = secureFlightInfo(passengers);
            string setPriceQuoteInf = setPriceQuoteInfo(passengers);
            StringBuilder bldr = new StringBuilder();

            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">PassengerDetailsRQ</eb:Service>");
            bldr.Append("       <eb:Action>PassengerDetailsRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Travel Itinerary Info Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <PassengerDetailsRQ  xmlns=\"http://services.sabre.com/sp/pd/v3_3\" version=\"3.3.0\" IgnoreOnError=\"false\" HaltOnError=\"false\">");
            bldr.Append("       <PostProcessing IgnoreAfter=\"" + ignoreAfter + "\" RedisplayReservation=\"" + redispResv + "\" UnmaskCreditCard=\"" + usCC + "\"/>");
            //bldr.Append("       <PreProcessing IgnoreBefore=\"" + isfalse + "\">");
            //bldr.Append("           <UniqueID ID=\""+ id + "\"/>");
            //bldr.Append("       </PreProcessing>");
            bldr.Append(setPriceQuoteInf);
            bldr.Append("        <SpecialReqDetails>");
            bldr.Append("           <AddRemarkRQ>");
            bldr.Append("               <RemarkInfo>");
            bldr.Append("                   <Remark Code=\"H\" SegmentNumber=\"1\" Type=\"General\">");
            bldr.Append("                       <Text>" + CCAddressLine + "</Text>");
            bldr.Append("                   </Remark>");
            bldr.Append("               </RemarkInfo>");
            bldr.Append("           </AddRemarkRQ>");
            bldr.Append("          <SpecialServiceRQ>");
            bldr.Append(secureFlightInf);
            bldr.Append("          </SpecialServiceRQ>");
            bldr.Append("     </SpecialReqDetails>");
            bldr.Append("    <TravelItineraryAddInfoRQ>");
            //bldr.Append("       <ContactNumber PhoneUseType=\"A\" Phone=\"1-877-389-7388\" />");
            bldr.Append("       <AgencyInfo>");
            bldr.Append("          <Address>");
            bldr.Append("               <AddressLine>" + AgencyAddressLine + "</AddressLine>");
            bldr.Append("               <CityName>" + AgencyCityName + "</CityName>");
            bldr.Append("               <CountryCode>" + AgencyCountryCode + "</CountryCode>");
            bldr.Append("               <PostalCode>" + AgencyPostalCode + "</PostalCode>");
            bldr.Append("               <StateCountyProv StateCode=\"" + AgencyStateCode + "\"/>");
            bldr.Append("               <StreetNmbr>" + AgencyStreetNumber + "</StreetNmbr>");
            bldr.Append("          </Address>");
            bldr.Append("          <Ticketing  TicketType=\"" + AgencyTicketType + "\" PseudoCityCode=\"" + pcc + "\" QueueNumber=\"" + AgencyQueueNo + "\" ShortText=\"" + SText + "\"/>");
            bldr.Append("       </AgencyInfo>");
            bldr.Append("       <CustomerInfo>");
            bldr.Append(passenger);
            bldr.Append("       </CustomerInfo>");
            bldr.Append("    </TravelItineraryAddInfoRQ>");
            bldr.Append("    </PassengerDetailsRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getCCVerificationRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode)
        {
            if (!string.IsNullOrEmpty(amount))
                amount = Math.Round(Convert.ToDecimal(amount), 2).ToString();

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">CreditVerificationLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>CreditVerificationLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Price Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <CreditVerificationRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.2.0\">");
            bldr.Append("       <Credit AutoStore=\"true\" DisplayReference=\"true\">");
            bldr.Append("          <CC_Info>");
            bldr.Append("               <PaymentCard AirlineCode=\"" + airlineCode + "\" Code=\"" + code + "\" CardSecurityCode=\"" + secCode + "\" ExpireDate=\"" + expDate + "\" Number=\"" + ccNumber + "\"/>");
            bldr.Append("          </CC_Info>");
            bldr.Append("          <ItinTotalFare>");
            bldr.Append("               <TotalFare  Amount=\"" + amount + "\" CurrencyCode=\"" + currCode + "\"/>");
            bldr.Append("          </ItinTotalFare>");
            bldr.Append("       </Credit>");
            bldr.Append("    </CreditVerificationRQ >");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }
        private String getAirAddRemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name, string CCFee, string ServiceFee, string BillingEmail, string BillingPhone, string PolicyCode, string TotalInsurancePrice)
        {

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">AddRemarkLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>AddRemarkLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("    <AddRemarkRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.1\">");
            bldr.Append("       <RemarkInfo>");
            bldr.Append("           <FOP_Remark>");
            bldr.Append("               <CC_Info Suppress=\"false\">");
            bldr.Append("                   <PaymentCard AirlineCode=\"" + airlineCode + "\" Code=\"" + code + "\" CardSecurityCode=\"" + secCode + "\" ExpireDate=\"" + expDate + "\" Number=\"" + ccNumber + "\"/>");
            bldr.Append("               </CC_Info>");
            bldr.Append("            </FOP_Remark>");
            bldr.Append("           <Remark Type=\"Client Address\">");
            bldr.Append("               <Text>" + Address + "</Text>");
            bldr.Append("           </Remark>");

            bldr.Append("           <Remark Type=\"General\">");
            //bldr.Append("               <Text>" + Name+","+ccNumber+","+expDate+","+secCode+","+BillingPhone+","+BillingEmail+","+ServiceFee+","+CCFee+","+TotalInsurancePrice + "</Text>");
            //bldr.Append("               <Text>" + Name + "," + ccNumber + "," + expDate + "," + secCode + "," + BillingPhone + "," + BillingEmail + "</Text>");
            bldr.Append("               <Text>" + Name + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            //bldr.Append("               <Text>" + Name+","+ccNumber+","+expDate+","+secCode+","+BillingPhone+","+BillingEmail+","+ServiceFee+","+CCFee+","+TotalInsurancePrice + "</Text>");
            //bldr.Append("               <Text>" + Name + "," + ccNumber + "," + expDate + "," + secCode + "," + BillingPhone + "," + BillingEmail + "</Text>");
            bldr.Append("               <Text>" + ccNumber + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>" + expDate.Replace('-', '/') + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>" + secCode + "</Text>");
            bldr.Append("           </Remark>");

            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>" + BillingPhone + "</Text>");
            bldr.Append("           </Remark>");

            if (Convert.ToSingle(CCFee) > 0)
            {
                bldr.Append("           <Remark Type=\"General\">");
                bldr.Append("               <Text>" + CCFee + "</Text>");
                bldr.Append("           </Remark>");

            }
            if (Convert.ToSingle(ServiceFee) > 0)
            {
                bldr.Append("           <Remark Type=\"General\">");
                bldr.Append("               <Text>" + ServiceFee + "</Text>");
                bldr.Append("           </Remark>");
            }
            if (!(System.String.IsNullOrEmpty(TotalInsurancePrice)))
            {
                if (Convert.ToSingle(TotalInsurancePrice) > 0)
                {
                    bldr.Append("           <Remark Type=\"General\">");
                    bldr.Append("               <Text>" + TotalInsurancePrice + "</Text>");
                    bldr.Append("           </Remark>");

                }
            }

            bldr.Append("           <Remark Type=\"General\">");
            var email = string.IsNullOrEmpty(BillingEmail) ? "" : BillingEmail.Replace("@", "ad");
            bldr.Append("               <Text>" + email + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>Total Amount Charged " + amount + "</Text>");
            bldr.Append("           </Remark>");

            //bldr.Append("           <Remark Type=\"CCNumber\">");
            //bldr.Append("               <Text>" + ccNumber + "</Text>");
            //bldr.Append("           </Remark>");
            //bldr.Append("           <Remark Type=\"ExpDate\">");
            //bldr.Append("               <Text>" + expDate + "</Text>");
            //bldr.Append("           </Remark>");
            //bldr.Append("           <Remark Type=\"CVV\">");
            //bldr.Append("               <Text>" + secCode+ "</Text>");
            //bldr.Append("           </Remark>");
            //bldr.Append("           <Remark Type=\"ServiceFee\">");
            //bldr.Append("               <Text>" + ServiceFee + "</Text>");
            //bldr.Append("           </Remark>");
            //bldr.Append("           <Remark Type=\"BillingEmail\">");
            //bldr.Append("               <Text>" + BillingEmail + "</Text>");
            //bldr.Append("           </Remark>");
            //bldr.Append("           <Remark Type=\"BillingPhone\">");
            //bldr.Append("               <Text>" + BillingPhone + "</Text>");
            //bldr.Append("           </Remark>");
            //bldr.Append("           <Remark Type=\"CCFee\">");
            //bldr.Append("               <Text>" + CCFee + "</Text>");
            //bldr.Append("           </Remark>");
            //if (PolicyCode.Length > 0)
            //{
            //    bldr.Append("           <Remark Type=\"PolicyCode\">");
            //    bldr.Append("               <Text>" + PolicyCode + "</Text>");
            //    bldr.Append("           </Remark>");
            //    bldr.Append("           <Remark Type=\"TotalInsurancePrice\">");
            //    bldr.Append("               <Text>" + TotalInsurancePrice + "</Text>");
            //    bldr.Append("           </Remark>");
            //}
            bldr.Append("       </RemarkInfo>");
            bldr.Append("    </AddRemarkRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
            //string CCFee,string ServiceFee,string BillingEmail,string BillingPhone,string PolicyCode,string TotalInsurancePrice
        }
        private String getAirAddccRemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name)
        {

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">AddRemarkLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>AddRemarkLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("    <AddRemarkRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.1\">");
            bldr.Append("       <RemarkInfo>");
            bldr.Append("           <FOP_Remark>");
            bldr.Append("               <CC_Info Suppress=\"true\">");
            bldr.Append("                   <PaymentCard AirlineCode=\"" + airlineCode + "\" Code=\"" + code + "\" CardSecurityCode=\"" + secCode + "\" ExpireDate=\"" + expDate + "\" Number=\"" + ccNumber + "\"/>");
            bldr.Append("               </CC_Info>");
            bldr.Append("            </FOP_Remark>");
            bldr.Append("           <Remark Type=\"Client Address\">");
            bldr.Append("               <Text>" + Address + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>" + Name + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("       </RemarkInfo>");
            bldr.Append("    </AddRemarkRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private String getAirAddServiceCommissionRemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name, string message)
        {

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">AddRemarkLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>AddRemarkLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("    <AddRemarkRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.1\">");
            bldr.Append("       <RemarkInfo>");
            bldr.Append("           <FOP_Remark>");
            bldr.Append("               <CC_Info Suppress=\"true\">");
            bldr.Append("                   <PaymentCard AirlineCode=\"" + airlineCode + "\" Code=\"" + code + "\" CardSecurityCode=\"" + secCode + "\" ExpireDate=\"" + expDate + "\" Number=\"" + ccNumber + "\"/>");
            bldr.Append("               </CC_Info>");
            bldr.Append("            </FOP_Remark>");
            bldr.Append("           <Remark Type=\"Client Address\">");
            bldr.Append("               <Text>" + Address + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>" + message + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("       </RemarkInfo>");
            bldr.Append("    </AddRemarkRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private String getAirAddCCFEECommissionRemarkRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string secCode, string amount, string currCode, string Address, string Name, string message)
        {

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">AddRemarkLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>AddRemarkLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("    <AddRemarkRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.1\">");
            bldr.Append("       <RemarkInfo>");
            bldr.Append("           <FOP_Remark>");
            bldr.Append("               <CC_Info Suppress=\"true\">");
            bldr.Append("                   <PaymentCard AirlineCode=\"" + airlineCode + "\" Code=\"" + code + "\" CardSecurityCode=\"" + secCode + "\" ExpireDate=\"" + expDate + "\" Number=\"" + ccNumber + "\"/>");
            bldr.Append("               </CC_Info>");
            bldr.Append("            </FOP_Remark>");
            bldr.Append("           <Remark Type=\"Client Address\">");
            bldr.Append("               <Text>" + Address + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("           <Remark Type=\"General\">");
            bldr.Append("               <Text>" + message + "</Text>");
            bldr.Append("           </Remark>");
            bldr.Append("       </RemarkInfo>");
            bldr.Append("    </AddRemarkRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private String getEndTransactionRequest(string pcc, string token, string ind, string receivedFrom)
        {
            StringBuilder bldr = new StringBuilder();

            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">EndTransactionLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>EndTransactionLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Price Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <EndTransactionRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.5\">");
            bldr.Append("       <EndTransaction  Ind=\"" + ind + "\"/>");
            bldr.Append("       <Source ReceivedFrom=\"" + receivedFrom + "\"/>");
            bldr.Append("    </EndTransactionRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getCancelAirIteRequest(string token, string ind)
        {
            StringBuilder bldr = new StringBuilder();
            string pcc = "6DTH";
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">OTA_CancelLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>OTA_CancelLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Cancel Air Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <OTA_CancelRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.1\">");
            bldr.Append("       <Segment  Type=\"" + ind + "\"/>");
            bldr.Append("    </OTA_CancelRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getTravelItineraryReadRequest(string pcc, string token, string Id, string tStamp, string subArea)
        {
            StringBuilder bldr = new StringBuilder();

            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">TravelItineraryReadRQ</eb:Service>");
            bldr.Append("       <eb:Action>TravelItineraryReadRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Price Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <TravelItineraryReadRQ  xmlns=\"http://services.sabre.com/res/tir/v3_6\" TimeStamp=\"" + tStamp + "\"  Version=\"3.6.0\">");
            bldr.Append("       <MessagingDetails>");
            bldr.Append("           <SubjectAreas>");
            bldr.Append("               <SubjectArea>" + subArea + "</SubjectArea>");
            bldr.Append("           </SubjectAreas>");
            bldr.Append("       </MessagingDetails>");
            bldr.Append("       <UniqueID  ID=\"" + Id + "\"/>");
            bldr.Append("       <EchoToken/>");
            bldr.Append("    </TravelItineraryReadRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getSessionCloseRequest(string token, string pcc)
        {

            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();

            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:mes=\"http://www.ebxml.org/namespaces/messageHeader\" xmlns:ns=\"http://www.opentravel.org/OTA/2002/11\">");
            bldr.Append(" <soapenv:Header>");
            bldr.Append("     <sec:Security  xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("        <sec:UsernameToken>");
            bldr.Append("           <sec:Username>" + username + "</sec:Username>");
            bldr.Append("           <sec:Password>" + password + "</sec:Password>");
            bldr.Append("           <Organization>" + pcc + "</Organization>");
            bldr.Append("           <Domain>Default</Domain>");
            bldr.Append("         </sec:UsernameToken>");
            bldr.Append("       <sec:BinarySecurityToken valueType=\"String\" EncodingType=\"sec:Base64Binary\">" + token + "</sec:BinarySecurityToken>");
            bldr.Append("     </sec:Security>");
            bldr.Append("      <mes:MessageHeader  mes:mustUnderstand=\"1\" mes:id=\"1000\" mes:version=\"3.6.0\">");
            bldr.Append("        <mes:From>");
            bldr.Append("          <mes:PartyId mes:type=\"urn: x12.org:IO5:01\">999999</mes:PartyId>");
            bldr.Append("        </mes:From>");
            bldr.Append("       <mes:To>");
            bldr.Append("           <mes:PartyId mes:type=\"urn: x12.org:IO5:01\">123123</mes:PartyId>");
            bldr.Append("         </mes:To>");
            bldr.Append("         <mes:CPAId>" + pcc + "</mes:CPAId>");
            bldr.Append("        <mes:ConversationId>1111111111111111111</mes:ConversationId>");
            bldr.Append("        <mes:Service mes:type=\"OTA\">eb:SessionCloseRQ </mes:Service>");
            bldr.Append("        <mes:Action>eb:SessionCloseRQ </mes:Action>");
            bldr.Append("         <mes:MessageData>");
            bldr.Append("           <mes:MessageId>1000</mes:MessageId>");
            bldr.Append("            <mes:Timestamp>2016-05-11T19:20:10Z</mes:Timestamp>");
            bldr.Append("            <mes:TimeToLive>2016-05-11T19:20:10Z</mes:TimeToLive>");
            bldr.Append("         </mes:MessageData>");
            bldr.Append("     </mes:MessageHeader>");
            bldr.Append("   </soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("      <SessionCloseRQ>");
            bldr.Append("         <POS>");
            bldr.Append("            <Source PseudoCityCode=\"" + pcc + "\"/>");
            bldr.Append("         </POS>");
            bldr.Append("      </SessionCloseRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");

            return bldr.ToString();
        }

        private string GetResponseDocument(string result)
        {
            XmlDocument responseXmlDocument = new XmlDocument();
            XmlDocument filteredDocument = null;
            string val = string.Empty;
            try
            {
                responseXmlDocument.LoadXml(result);
                XmlNode filteredResponse = responseXmlDocument.SelectSingleNode("//*[local-name()='Body']/*");

                filteredDocument = new XmlDocument();
                filteredDocument.LoadXml(filteredResponse.OuterXml);
                string pnr = "";
                foreach (XmlNode node in filteredDocument.DocumentElement.ChildNodes)
                {
                    if (node.Name == "ItineraryRef")
                    {
                        pnr = node.Attributes[0].Value;
                    }
                }

                if (filteredDocument.FirstChild.FirstChild.Attributes[0].Value == "Complete")
                {
                    val = filteredDocument.FirstChild.FirstChild.FirstChild.Attributes[0].Value;
                    val = val + " " + pnr;
                }
            }
            catch (Exception ex)
            {

            }
            return val;
        }

        #endregion "Create Request XML"
    }
    //public class TrackBookingRecords
    //{
    //    public string RequestID { get; set; }
    //    public string Locator { get; set; }
    //    public string UserId { get; set; }
    //    public string DefaultCompanyId { get; set; }
    //    public string BillingEmail { get; set; }
    //    public string BillingPhone { get; set; }
    //    public string BillingName { get; set; }
    //    public string PaymentMethod { get; set; }
    //}
    public class AirPassengerCreateCardModelsList
    {
        public AirPassengerCreateCardModelsList()
        {
        }
        public string requestID { get; set; }
        public List<FlightDetails> flightsDetailsList { get; set; }
        public List<PassengerDetails> passengersDetailsList { get; set; }
        public List<CreateCardAndOtherDetails> creditCardOtherDetails { get; set; }
        public AgencyDetails _AgencyDetails { get; set; }
        public TrackBookingRecords SaveRecords { get; set; }
        public PolicyDetails _policydetails { get; set; }


    }
    public class PolicyDetails
    {
        public string DATEDEP { get; set; }
        public string DATERET { get; set; }
        public string PROVINCE { get; set; }
        public string GATEWAY { get; set; }
        public string DESTINATION { get; set; }
        public string POLICYCODE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string POSTALCODE { get; set; }
        public string COUNTRY { get; set; }
        public string Total_Insurance_Price { get; set; }
    }
    public class AgencyDetails
    {

        public string Name { get; set; }
        public string CityName { get; set; }
        public string country { get; set; }
        public string PostalCode { get; set; }
        public string state { get; set; }
        public string StreetAddress { get; set; }
        public string SmtpEmailID { get; set; }
        public string HeaderUrl { get; set; }
        public string FooterUrl { get; set; }
        public string PrivacyUrl { get; set; }
        public string TermsUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueNo { get; set; }
        public string PseudoCityCode { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Domain { get; set; }
        public string CurrencyCode { get; set; }
        public string Insurance { get; set; }
        public string ServiceFee { get; set; }
        public float cpublishedfare { get; set; }
        public float mapublishedfare { get; set; }
        public string CompanyTypeID { get; set; }
        public float Commission { get; set; }
        public string CCEnableAir { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class FlightDetails
    {
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string ElapsedTime { get; set; }
        public string Sequence { get; set; }
        public string DepartureCityCode { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalCityCode { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalTerminal { get; set; }
        public string DistanceTravel { get; set; }
        public string AircraftType { get; set; }
        public string DepartureDateTime { get; set; }
        public string ArrivalDateTime { get; set; }
        public string ArrivalDateTime1 { get; set; }
        public string BookingClass { get; set; }
        public string FlightTime { get; set; }
        public string DirectionInd { get; set; }
        public string DepAirportLocationCode { get; set; }
        public string OperatingAirlineCode { get; set; }
        public string ArrAirportLocationCode { get; set; }
        public string Equipment { get; set; }
        public string MarketingAirline { get; set; }
        public string NoofPassengers { get; set; }
        public string resBookDesigCode { get; set; }
        public string status { get; set; }
        public string Stops { get; set; }
        public string DayOfWeekInd { get; set; }

    }

    public class FlightPassengerDetails
    {
        public FlightPassengerDetails()
        {

        }
        public List<FlightDetails> flights_details { get; set; }
        public List<PassengerDetails> passengers_details { get; set; }
        public string pnrnumber { get; set; }
        public string AirLineReservationCode { get; set; }
        public CreateCardAndOtherDetails currCode_details { get; set; }
        public string ErrorCode { get; set; }

        public string RequestID { get; set; }
        public string BookingDate { get; set; }

    }

    public class CreateCardAndOtherDetails
    {

        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CVVNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public string NameOnCard { get; set; }
        public string AirLineCode { get; set; }
        public string CurrCode { get; set; }
        public string PaymentAmount { get; set; }
        public string Address { get; set; }
        public string CCFee { get; set; }
        public string Province { get; set; }

    }

    public class AirNoofPassengers
    {
        public string PassengerCode { get; set; }
        public int NoOfPassengers { get; set; }
        public bool IsInfant { get; set; }
    }

    public class PassengerDetails
    {

        public string passengerType { get; set; }
        public string passengername { get; set; }
        public string passengerAddress { get; set; }
        public string ReservationCode { get; set; }
        public string airlineName { get; set; }
        public string email { get; set; }
        public string DepartureCityCode { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureTerminal { get; set; }
        public string IsInfant { get; set; }
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneLocationCode { get; set; }
        public string PassengerNameNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneUseType { get; set; }
        public string PassengerEmail { get; set; }
        public string PassengerNameRef { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Tripcost { get; set; }
        public string Tax { get; set; }
        public string Price { get; set; }
        public string InsurancePrice { get; set; }

    }

    [XmlRoot(ElementName = "PAX")]
    public class RPAX
    {
        [XmlElement(ElementName = "BIRTHDATE")]
        public string BIRTHDATE { get; set; }
        [XmlElement(ElementName = "TRIPCOST")]
        public string TRIPCOST { get; set; }
        [XmlElement(ElementName = "AMTAFTER")]
        public string AMTAFTER { get; set; }
        [XmlElement(ElementName = "PRICE")]
        public string PRICE { get; set; }
        [XmlElement(ElementName = "TAX")]
        public string TAX { get; set; }
    }

    [XmlRoot(ElementName = "PAXES")]
    public class PAXES
    {
        [XmlElement(ElementName = "PAX")]
        public List<RPAX> PAX { get; set; }
    }

    [XmlRoot(ElementName = "PRODUCT")]
    public class RPRODUCT
    {
        [XmlAttribute(AttributeName = "CODE")]
        public string CODE { get; set; }
    }

    [XmlRoot(ElementName = "PRODUCTS")]
    public class PRODUCTS
    {
        [XmlElement(ElementName = "PRODUCT")]
        public RPRODUCT PRODUCT { get; set; }
    }

    [XmlRoot(ElementName = "REQUEST")]
    public class REQUEST
    {
        [XmlElement(ElementName = "SERVICEKEY")]
        public string SERVICEKEY { get; set; }
        [XmlElement(ElementName = "BRANCHCODE")]
        public string BRANCHCODE { get; set; }
        [XmlElement(ElementName = "LANGUAGE")]
        public string LANGUAGE { get; set; }
        [XmlElement(ElementName = "SEARCHTYPE")]
        public string SEARCHTYPE { get; set; }
        [XmlElement(ElementName = "DATEDEP")]
        public string DATEDEP { get; set; }
        [XmlElement(ElementName = "DATERET")]
        public string DATERET { get; set; }
        [XmlElement(ElementName = "PROVINCE")]
        public string PROVINCE { get; set; }
        [XmlElement(ElementName = "DAYSPERTRIP")]
        public string DAYSPERTRIP { get; set; }
        [XmlElement(ElementName = "FAMILYRATE")]
        public string FAMILYRATE { get; set; }
        [XmlElement(ElementName = "NBPAX")]
        public string NBPAX { get; set; }
        [XmlElement(ElementName = "PAXES")]
        public PAXES PAXES { get; set; }
        [XmlElement(ElementName = "PRODUCTS")]
        public PRODUCTS PRODUCTS { get; set; }
    }

    [XmlRoot(ElementName = "Product")]
    public class RProduct
    {
        [XmlElement(ElementName = "CODE")]
        public string CODE { get; set; }
        [XmlElement(ElementName = "NAME")]
        public string NAME { get; set; }
        [XmlElement(ElementName = "DESCURL")]
        public string DESCURL { get; set; }
        [XmlElement(ElementName = "PRICE")]
        public string PRICE { get; set; }
        [XmlElement(ElementName = "TAX")]
        public string TAX { get; set; }
        [XmlElement(ElementName = "TOTAL")]
        public string TOTAL { get; set; }
        [XmlElement(ElementName = "PERDAY")]
        public string PERDAY { get; set; }
        [XmlElement(ElementName = "FAMILYRATE")]
        public string FAMILYRATE { get; set; }
        [XmlElement(ElementName = "PAXES")]
        public PAXES PAXES { get; set; }
    }

    [XmlRoot(ElementName = "Products")]
    public class Products
    {
        [XmlElement(ElementName = "Product")]
        public RProduct Product { get; set; }
    }

    [XmlRoot(ElementName = "RESPONSE")]
    public class RRESPONSE
    {
        [XmlElement(ElementName = "HEADER")]
        public string HEADER { get; set; }
        [XmlElement(ElementName = "TEXT")]
        public string TEXT { get; set; }
        [XmlElement(ElementName = "Products")]
        public Products Products { get; set; }
    }

    [XmlRoot(ElementName = "XML")]
    public class InsuranceQuoteResponse
    {
        [XmlElement(ElementName = "REQUEST")]
        public REQUEST REQUEST { get; set; }
        [XmlElement(ElementName = "RESPONSE")]
        public RESPONSE RESPONSE { get; set; }
    }

    public class Book2
    {
        public string SERVICEKEY { get; set; }
        public string BRANCHCODE { get; set; }
        public string LANGUAGE { get; set; }
        public string SEARCHTYPE { get; set; }
        public string DATEDEP { get; set; }
        public string DATERET { get; set; }
        public string PROVINCE { get; set; }
        public string DAYSPERTRIP { get; set; }
        public string FAMILYRATE { get; set; }
        public string NBPAX { get; set; }
        public string AGENTCODE { get; set; }
        public string GATEWAY { get; set; }
        public string DESTINATION { get; set; }
        public string POLICYCODE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public string POSTALCODE { get; set; }
        public string COUNTRY { get; set; }
        public string COMMENTS { get; set; }
        public string BENEFICIARY { get; set; }
        public string RELATIONSHIP { get; set; }
        public List<Payment> PAYMENTS { get; set; } = new List<Payment>();
        public List<Traveler> TRAVELERS { get; set; } = new List<Traveler>();


    }
    public class Payment
    {
        public string CCAMOUNT { get; set; }
        public string CCTYPE { get; set; }
        public string CCNUMBER { get; set; }
        public string CCEXPMONTH { get; set; }
        public string CCEXPYEAR { get; set; }
        public string CCNAME { get; set; }

    }
    public class Traveler
    {
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string BIRTHDATE { get; set; }
        public string TRIPCOST { get; set; }
        public string AMTAFTER { get; set; }
        public string REFNUM { get; set; }
        public string INFANT { get; set; }
    }
    public class ReqQuote2
    {
        public string DATEDEP { get; set; }
        public string DATERET { get; set; }
        public string PROVINCE { get; set; }
        public string NBPAX { get; set; }
        public List<PAX> PAXES { get; set; }
    }
    public class Quote2
    {
        public string SERVICEKEY { get; set; }
        public string BRANCHCODE { get; set; }
        public string LANGUAGE { get; set; }
        public string SEARCHTYPE { get; set; }
        public string DATEDEP { get; set; }
        public string DATERET { get; set; }
        public string PROVINCE { get; set; }
        public string DAYSPERTRIP { get; set; }
        public string FAMILYRATE { get; set; }
        public string NBPAX { get; set; }
        public List<PAX> PAXES { get; set; } = new List<PAX>();
        public List<PRODUCT> PRODUCTS { get; set; } = new List<PRODUCT>();

    }
    public class InsuranceBookRqst
    {

    }
    //[Serializable]
    public class PAX
    {
        public string BIRTHDATE { get; set; }
        public string TRIPCOST { get; set; }
        public string AMTAFTER { get; set; }
        public string PRICE { get; set; }
        public string TAX { get; set; }
    }
    public class PRODUCT
    {
        public string CODE { get; set; }

    }
    public class RESPONSE
    {
        public string HEADER { get; set; }
        public string TEXT { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public string PURCHASE_DATE { get; set; }
        public string POLICY_NUMBER { get; set; }
    }

    public class Product
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string DESCURL { get; set; }
        public string PRICE { get; set; }
        public string TAX { get; set; }
        public string TOTAL { get; set; }
        public string PERDAY { get; set; }
        public string FAMILYRATE { get; set; }
        public List<PAX> PAXES { get; set; } = new List<PAX>();

    }

    public class CCAuthorizeModelsList
    {
        public CCAuthorizeModelsList()
        {
        }
        public List<CreateCardAndOtherDetails> creditCardOtherDetails { get; set; }
    }


}