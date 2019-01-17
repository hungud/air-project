using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace App.ConfigService.Controllers.SOAPServices.SabreClassVersion1
{
    public class SabreClass
    {
        String sessionconversionid = "";
        String binarysecuritytoken = "";
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

     
        public App.Common.CommonUtility AirReservationBookingREQRES(Models.SabreModels.SabreFlightBookingPayentRQ Flight_Booking_REQ)
        {

            List<App.Model.SOAPData.FlightSegmentData> flightsearch = new List<App.Model.SOAPData.FlightSegmentData>();
            string getResult = string.Empty;
            string sess = SessionCreate(ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString());
            string changepcc = airChangePCC(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString(), ConfigurationManager.AppSettings["PseudoCityCode"].ToString());

            string AgencyAddressLine = "WATERDOWN TRAVEL AGENCY";
            string AgencyCityName = "Brampton";
            string AgencyCountryCode = "CAN";
            string AgencyPostalCode = "L6Y1N7";
            string AgencyStateCode = "ONT";
            string AgencyStreetNumber = "Suite 209 Main Street South";
            string GivenName = Flight_Booking_REQ.GivenName;
            string Surname = Flight_Booking_REQ.Surname;
            string Phone1LocationCode = Flight_Booking_REQ.Phone1LocationCode; //DEL
            string PNameNumber = Flight_Booking_REQ.PNameNumber; //910-555-1212
            string Phone1 = Flight_Booking_REQ.Phone1;
            string Phone1UseType = Flight_Booking_REQ.Phone1UseType; //"H"
            string PType = Flight_Booking_REQ.PType; //"ADT"
            string NameRef = Flight_Booking_REQ.NameRef; //"ABC123"
            string PassengerEmail = Flight_Booking_REQ.PassengerEmail; //"ksandeepchaudhary@gmail.com" 
            string SText = Flight_Booking_REQ.SText; //"Test"
            string EMailType = Flight_Booking_REQ.EMailType; //"CC" 
            string AgencyTicketTime = Flight_Booking_REQ.AgencyTicketTime;
            string AgencyTicketType = "7TAW";
            string AgencyQueueNo = "55";

            // Credit Card Details
            string retain = Flight_Booking_REQ.retain;
            string code = Flight_Booking_REQ.code;
            string airlineCode = Flight_Booking_REQ.airlineCode;
            string ccNumber = Flight_Booking_REQ.ccNumber;
            string expDate = Flight_Booking_REQ.expDate;
            string amount = Flight_Booking_REQ.amount;
            string currCode = Flight_Booking_REQ.sbrCurrencyCode;

           // getResult = airAvailRequest(binarysecuritytoken, "YYZ", "DEL", DateTime.Now.AddDays(15).ToString("yyyy-MM-ddTHH:mm"));
           // XmlReadClass getResultReader = new XmlReadClass();
           // flightsearch = getResultReader.ReadSearchXML(getResult);

           // //XmlReadClass getResultReader = new XmlReadClass();
           // //flightsearch = getResultReader.ReadSearchXML(getResult);
           // string getboookings = string.Empty;
           // ///*******Air booking Segment***
           // foreach (var data in flightsearch)
           // {
           //     if (data.iseTicket == "true")
           //     {
           //         getboookings = airEnhanceBookRQ(binarysecuritytoken, data.DepartureDateTime, data.ArrivalDateTime,
           //             data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode,
           //             data.Status, data.MarketingAirlineCode, "true", "ADT", "1");
                    
           //         break;
           //     }
           //}

            string getbookings = airEnhanceBookRQ(binarysecuritytoken, Flight_Booking_REQ.dtDepartureTime,
                        Flight_Booking_REQ.dtArrivalTime, Flight_Booking_REQ.sbrAirLineNumber, Flight_Booking_REQ.sbrArrivalAirport,
                        Flight_Booking_REQ.sbrAirEquipType, Flight_Booking_REQ.sbrDepartureAirport, Flight_Booking_REQ.sbrResBookDesigCode,
                        Flight_Booking_REQ.status, Flight_Booking_REQ.sbrAirLineCode, "true", Flight_Booking_REQ.PType, "1");

            ///****Passenger Details****//////
            string getresult = addTravelItinerayRequest(ConfigurationManager.AppSettings["PseudoCityCode"].ToString(), binarysecuritytoken,
                AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber,
                GivenName, Surname, Phone1LocationCode, "1.1", PNameNumber, Phone1UseType, PType , NameRef, PassengerEmail, SText, EMailType, AgencyTicketTime, AgencyTicketType, AgencyQueueNo);

            string getPrice = airPriceRequest(binarysecuritytoken, "true", "ADT", "1");

            ///****Credit Card Details****//////
            string getVerificationCode = checkCCVerificationRequest(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString(), "true", code, airlineCode, ccNumber, expDate, amount, currCode);

            string endTrans = endTransaction(ConfigurationManager.AppSettings["PseudoCityCode"].ToString(), binarysecuritytoken, "true", "SWS TEST");
            string GetPNRTrans = GetResponseDocument(endTrans);

            SessionClose(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString());
            getResult = GetPNRTrans;

            return new Common.CommonUtility()
            {
                Data = AirReservationRetrievePNR(getResult).Data,
                Message = "Success",
                Status = true,
                ErrorCode = "00",
            };
        }

        public App.Common.CommonUtility AirReservationRetrievePNR(string Flight_ReservationPNR_REQ)
        {
            if (!string.IsNullOrEmpty(Flight_ReservationPNR_REQ))
            {
                string TimeStamp = Flight_ReservationPNR_REQ.Split(' ')[0].ToString();
                string PNRID = Flight_ReservationPNR_REQ.Split(' ')[1].ToString();
                List<App.Model.SOAPData.FlightSegmentData> flightsearch = new List<App.Model.SOAPData.FlightSegmentData>();
                string sess = SessionCreate(ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString());
                string getDetails = RetrievePNR(binarysecuritytoken, PNRID, TimeStamp, "FULL");
                SessionClose(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString());
                Models.SabreModels.FlightPassengerDetails PNRFlightPassengerDetails = readPNRXML(getDetails);
                return new Common.CommonUtility()
                {
                    Data = PNRFlightPassengerDetails,
                    Message = "Success",
                    Status = true,
                    ErrorCode = "00",
                };
            }
            else
            {
                return new Common.CommonUtility()
                {
                    Data = null,
                    Message = "Fail",
                    Status = false,
                    ErrorCode = "00",
                };
            }
        }

        public string AirBookingREQRES(App.Model.SOAPData.FlightBookingPayentRQ Flight_Booking_REQ)
        {
            List<App.Model.SOAPData.FlightSegmentData> flightsearch = new List<App.Model.SOAPData.FlightSegmentData>();
            string getResult = string.Empty;
            string sess = SessionCreate("6DTH");

            string changepcc = airChangePCC(binarysecuritytoken, "6DTH", "6DTH");

            DateTime dtDepDate = DateTime.Now.AddDays(15);
            string stDate = dtDepDate.ToString("MM-ddTHH:mm");

            DateTime dtDepDate2 = DateTime.Now.AddDays(10);
            string stDate2 = dtDepDate2.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime dtDepDate3 = DateTime.Now.AddDays(13);
            string stDate3 = dtDepDate3.ToString("yyyy-MM-ddTHH:mm:ss");
            //string result = airBFMRequest(binarysecuritytoken, "1", stDate2, "YYZ", "NYC", "2", "2016-07-29T08:00:00", "NYC", "DFW", "Y", "Preferred", "Return", "1", "ADT", "1");
            getResult = airAvailRequest(binarysecuritytoken, "YYZ", "DEL", stDate);

            XmlReadClass getResultReader = new XmlReadClass();
            flightsearch = getResultReader.ReadSearchXML(getResult);

            ///*******Air booking Segment***
            foreach (var data in flightsearch)
            {
                if (data.iseTicket == "true")
                {
                    string getbookings = airEnhanceBookRQ(binarysecuritytoken, data.DepartureDateTime, data.ArrivalDateTime, data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode, data.Status, data.MarketingAirlineCode, "true", "ADT", "1");
                    break;
                }
                //string getbookings = airBookRequest(binarysecuritytoken, data.DepartureDateTime, data.ArrivalDateTime, data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode, data.Status, data.MarketingAirlineCode);
                //string getbookings =  airShortSell_BookRequest(binarysecuritytoken, data.DepartureDateTime, data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode, data.Status, data.MarketingAirlineCode);

            }

            ///****Passenger Details****//////

            string getresult = addTravelItinerayRequest("", binarysecuritytoken, "WATERDOWN TRAVEL AGENCY", "Brampton", "CAN", "L6Y1N7", "ONT", "Suite 209 Main Street South", "Sandeep", "Kumar", "DEL", "1.1", "910-555-1212", "H", "ADT", "", "ksandeepchaudhary@gmail.com", "TEST", "CC", "08-01T06:00", "7TAW", "55");
            string getPrice = airPriceRequest(binarysecuritytoken, "true", "ADT", "1");

            //string getresultw = addPassengerRequest(binarysecuritytoken, "true", "true", "true", "true", "India", "INDIAN", "", "2", "2018-05-26", "1234567890", "P", "1980-12-02", "M", "WATERDOWN TRAVEL AGENCY", "Brampton", "CAN", "L6Y 1N7", "ONT", "Suite 209-499 Main Street South", "Sandeep", "", "Kumar", "1.1", "DEL", "911-555-1212", "H", "ADT", "ksandeepchaudhary@gmail.com", "hello", "CC", "07-21T06:00", "7TAW", "57", "AN", "2", "A");
            ///****Passenger Details****//////
            //string getVerificationCode = checkCCVerificationRequest(binarysecuritytoken, "6DTH", "true", code, AirlineCode, ccNumber, expDate, amount, currCode);

            //string bookSeat = airSeatBookRequest(binarysecuritytoken, "1.1", "AN", "1");
            string endTrans = endTransaction("", binarysecuritytoken, "true", "SWS TEST");
            SessionClose(binarysecuritytoken, "6DTH");
            return getResult;
        }

        public string MainClass()
        {
            List<App.Model.SOAPData.FlightSegmentData> flightsearch = new List<App.Model.SOAPData.FlightSegmentData>();
            string getResult = string.Empty;
            string sess = SessionCreate("6DTH");

            string changepcc = airChangePCC(binarysecuritytoken, "6DTH", "6DTH");

            DateTime dtDepDate = DateTime.Now.AddDays(15);
            string stDate = dtDepDate.ToString("MM-ddTHH:mm");

            DateTime dtDepDate2 = DateTime.Now.AddDays(10);
            string stDate2 = dtDepDate2.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime dtDepDate3 = DateTime.Now.AddDays(13);
            string stDate3 = dtDepDate3.ToString("yyyy-MM-ddTHH:mm:ss");
            //string result = airBFMRequest(binarysecuritytoken, "1", stDate2, "YYZ", "NYC", "2", "2016-07-29T08:00:00", "NYC", "DFW", "Y", "Preferred", "Return", "1", "ADT", "1");
            getResult = airAvailRequest(binarysecuritytoken, "YYZ", "DEL", stDate);

            XmlReadClass getResultReader = new XmlReadClass();
            flightsearch = getResultReader.ReadSearchXML(getResult);

            ///*******Air booking Segment***
            foreach (var data in flightsearch)
            {
                if (data.iseTicket == "true")
                {
                    string getbookings = airEnhanceBookRQ(binarysecuritytoken, data.DepartureDateTime, data.ArrivalDateTime, data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode, data.Status, data.MarketingAirlineCode, "true", "ADT", "1");
                    break;
                }
                //string getbookings = airBookRequest(binarysecuritytoken, data.DepartureDateTime, data.ArrivalDateTime, data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode, data.Status, data.MarketingAirlineCode);
                //string getbookings =  airShortSell_BookRequest(binarysecuritytoken, data.DepartureDateTime, data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode, data.Status, data.MarketingAirlineCode);

            }

            ///****Passenger Details****//////

            string getresult = addTravelItinerayRequest("", binarysecuritytoken, "WATERDOWN TRAVEL AGENCY", "Brampton", "CAN", "L6Y1N7", "ONT", "Suite 209 Main Street South", "Sandeep", "Kumar", "DEL", "1.1", "910-555-1212", "H", "ADT", "", "ksandeepchaudhary@gmail.com", "TEST", "CC", "08-01T06:00", "7TAW", "55");
            string getPrice = airPriceRequest(binarysecuritytoken, "true", "ADT", "1");

            //string getresultw = addPassengerRequest(binarysecuritytoken, "true", "true", "true", "true", "India", "INDIAN", "", "2", "2018-05-26", "1234567890", "P", "1980-12-02", "M", "WATERDOWN TRAVEL AGENCY", "Brampton", "CAN", "L6Y 1N7", "ONT", "Suite 209-499 Main Street South", "Sandeep", "", "Kumar", "1.1", "DEL", "911-555-1212", "H", "ADT", "ksandeepchaudhary@gmail.com", "hello", "CC", "07-21T06:00", "7TAW", "57", "AN", "2", "A");
            ///****Passenger Details****//////
            //string getVerificationCode = checkCCVerificationRequest(binarysecuritytoken, "6DTH", "true", code, AirlineCode, ccNumber, expDate, amount, currCode);

            //string bookSeat = airSeatBookRequest(binarysecuritytoken, "1.1", "AN", "1");
            string endTrans = endTransaction("", binarysecuritytoken, "true", "SWS TEST");
            SessionClose(binarysecuritytoken, "6DTH");
            return getResult;
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

            message = message.Replace("wse:", "");

            message = message.Replace("wsse:", "");
            message = message.Replace("swse:", "");
            message = message.Replace("eb:", "");
            message = message.Replace("wsa:", "");
            message = message.Replace("sCCC.", "");
            message = message.Replace("soap-env:", "");

            return message;

        }

        /// <returns>ResultCode, 1 if success.</returns>
        private void QueueRead()
        {

            String xml = "";//textRequest.Text;
            XmlDocument doc = new XmlDocument();
            doc.Load("D:/Works/SABRE/docs/queueread.xml");

            xml = doc.OuterXml;
            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);

            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            String url = ConfigurationManager.AppSettings["webserviceurl"].ToString();

            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);

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

            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();

                result = replaceMessage(result);

                //textResult.Text = result;

            }

        }

        /// <returns>ResultCode, 1 if success.</returns>
        private void QueueCount()
        {

            String xml = "";// textRequest.Text;

            XmlDocument doc = new XmlDocument();
            doc.Load("D:/Works/SABRE/docs/queuecount.xml");

            xml = doc.OuterXml;
            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);

            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            String url = ConfigurationManager.AppSettings["webserviceurl"].ToString();

            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);

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

            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();

                result = replaceMessage(result);

                //textResult.Text = result;

            }

        }

        /// <returns>ResultCode, 1 if success.</returns>
        private void RetrieveCurrency()
        {

            String xml = "";// textRequest.Text;
            XmlDocument doc = new XmlDocument();
            doc.Load("D:/Works/SABRE/docs/currency.xml");
            xml = doc.OuterXml;

            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);

            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            String url = ConfigurationManager.AppSettings["webserviceurl"].ToString();

            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);

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

            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();

                result = replaceMessage(result);

                //textResult.Text = result;
            }
        }


        public string postXMLData()
        {
            //SessionCreate();
            string requestXml = @"C:\Users\sandeep.chaudhary\Desktop\AAI-N1Key\SessionRQ.xml";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            try
            {
                request.Headers.Add("SOAPAction", "SessionCreateRQ");
                request.ContentType = "text/xml; encoding='utf-8'";
                request.Accept = "gzip,deflate";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Proxy = WebRequest.GetSystemWebProxy();
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                return pageContent;
            }
            return null;
        }

        public string RetrievePNR(string token, string Id, string tStamp, string subArea)
        {
            //String xml = textRequest.Text;
            XmlDocument doc = new XmlDocument();
            string result = "";
            return result = executeWebRequest(getTravelItineraryReadRequest(token, Id, tStamp, subArea));
        }




        #region MultiCity Search
        public App.Common.CommonUtility AirBookingEnginREQRES(Models.SabreModels.BookingFlightPassengerDetails FlightsPassengersCCDetails)
        {
            return new Common.CommonUtility()
            {
                Data = null,
                Message = "Success",
                Status = true,
                ErrorCode = "00",
            };
        }


        public App.Common.CommonUtility AirBookingEnginREQRES(List<App.Models.SabreModels.AirFlightDetails> FlightsDetails, List<App.Models.SabreModels.AirPassengerDetails> PassengersDetails, List<App.Models.SabreModels.AirOtherDetails> OthersDetails)
        {

            List<App.Model.SOAPData.FlightSegmentData> flightsearch = new List<App.Model.SOAPData.FlightSegmentData>();
            string getResult = string.Empty;
            string sess = SessionCreate(ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString());
            string changepcc = airChangePCC(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString(), ConfigurationManager.AppSettings["PseudoCityCode"].ToString());

            string AgencyAddressLine = "WATERDOWN TRAVEL AGENCY";
            string AgencyCityName = "Brampton";
            string AgencyCountryCode = "CAN";
            string AgencyPostalCode = "L6Y1N7";
            string AgencyStateCode = "ONT";
            string AgencyStreetNumber = "Suite 209 Main Street South";
           
            string AgencyTicketType = "7TAW";
            string AgencyQueueNo = "55";

            // Credit Card Details
            string retain = string.Empty;
            string code = string.Empty;
            string airlineCode = string.Empty;
            string ccNumber = string.Empty;
            string expDate = string.Empty;
            string amount = string.Empty;
            string currCode = string.Empty;
            string AgencyTicketTime = string.Empty;
            string SText = string.Empty;
            //string retain = Flight_Booking_REQ.retain;
            //string code = Flight_Booking_REQ.code;
            //string airlineCode = Flight_Booking_REQ.airlineCode;
            //string ccNumber = Flight_Booking_REQ.ccNumber;
            //string expDate = Flight_Booking_REQ.expDate;
            //string amount = Flight_Booking_REQ.amount;
            //string currCode = Flight_Booking_REQ.sbrCurrencyCode;

            // getResult = airAvailRequest(binarysecuritytoken, "YYZ", "DEL", DateTime.Now.AddDays(15).ToString("yyyy-MM-ddTHH:mm"));
            // XmlReadClass getResultReader = new XmlReadClass();
            // flightsearch = getResultReader.ReadSearchXML(getResult);

            // //XmlReadClass getResultReader = new XmlReadClass();
            // //flightsearch = getResultReader.ReadSearchXML(getResult);
            // string getboookings = string.Empty;
            // ///*******Air booking Segment***
            // foreach (var data in flightsearch)
            // {
            //     if (data.iseTicket == "true")
            //     {
            //         getboookings = airEnhanceBookRQ(binarysecuritytoken, data.DepartureDateTime, data.ArrivalDateTime,
            //             data.FlightNumber, data.DesignationCode, data.AirEquipType, data.OriginLocationCode, data.ResBookDesigCode,
            //             data.Status, data.MarketingAirlineCode, "true", "ADT", "1");

            //         break;
            //     }
            //}

            string getbookings = airEnhancedBookMultiSegmentRQ(binarysecuritytoken, FlightsDetails, "true", "ADT", "1");

            ///****Passenger Details****//////
            string getresult = addTravelMultiItinerayRequest(ConfigurationManager.AppSettings["PseudoCityCode"].ToString(), binarysecuritytoken,
                AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber,
                AgencyTicketTime, AgencyTicketType, AgencyQueueNo, SText, PassengersDetails);


            List<App.Models.SabreModels.AirNoofPassengers> NoofPassengers = new List<Models.SabreModels.AirNoofPassengers>();

            string getPrice = airPriceRequest(binarysecuritytoken, "true", NoofPassengers);

            ///****Credit Card Details****//////
            string getVerificationCode = checkCCVerificationRequest(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString(), "true", code, airlineCode, ccNumber, expDate, amount, currCode);

            string endTrans = endTransaction(ConfigurationManager.AppSettings["PseudoCityCode"].ToString(), binarysecuritytoken, "true", "SWS TEST");
            string GetPNRTrans = GetResponseDocument(endTrans);

            SessionClose(binarysecuritytoken, ConfigurationManager.AppSettings["PseudoCityCode_1"].ToString());
            getResult = GetPNRTrans;

            return new Common.CommonUtility()
            {
                Data = null,
                Message = "Success",
                Status = true,
                ErrorCode = "00",
            };
        }


        public string airEnhancedBookMultiSegmentRQ(string token, List<App.Models.SabreModels.AirFlightDetails> flights, string retain, string p_code, string quantity)
        {
            string result = "";
            result = executeWebRequest(getMultiSegmentEnhancedBookingRequest("", token, flights, retain, p_code, quantity));
            return result;
        }

        public string addTravelMultiItinerayRequest(string pcc, string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string SText, List<App.Models.SabreModels.AirPassengerDetails> passengersList)
        {
            string result = "";
            result = executeWebRequest(getTravelItineraryAddPassengersRequest(pcc, token, AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber, AgencyTicketTime, AgencyTicketType, AgencyQueueNo, SText, passengersList));
            return result;
        }

        private String getMultiSegmentEnhancedBookingRequest(string pcc, string token, List<App.Models.SabreModels.AirFlightDetails> flights, string retain, string p_code, string quantity)
        {
            string flightsegments = flightDetails(flights);
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
            bldr.Append("    <EnhancedAirBookRQ  xmlns=\"http://services.sabre.com/sp/eab/v3_6\"  version=\"3.6.0\" IgnoreOnError=\"true\" HaltOnError=\"true\" >");
            bldr.Append("     <OTA_AirBookRQ>");
            bldr.Append("       <HaltOnStatus Code=\"NO\"/>");
            bldr.Append("       <HaltOnStatus Code=\"NN\"/>");
            bldr.Append("       <HaltOnStatus Code=\"UC\"/>");
            bldr.Append("       <HaltOnStatus Code=\"US\"/>");
            bldr.Append("      <OriginDestinationInformation>");
            //bldr.Append("          <FlightSegment  DepartureDateTime =\"" + dtDepartureTime + "\" ArrivalDateTime=\"" + dtArrivalTime + "\" FlightNumber=\"" + flightNumber + "\" NumberInParty=\"1\" ResBookDesigCode=\"" + resBookDesigCode + "\" Status=\"" + status + "\">");
            //bldr.Append("           <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            //bldr.Append("           <Equipment  AirEquipType=\"" + AirEquipType + "\"/>");
            //bldr.Append("           <MarketingAirline  Code=\"" + code + "\" FlightNumber=\"" + flightNumber + "\"/>");
            //bldr.Append("           <OperatingAirline  Code=\"" + code + "\"/>");
            //bldr.Append("           <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            //bldr.Append("           </FlightSegment>");
            bldr.Append(flightsegments);
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     <RedisplayReservation NumAttempts=\"2\" WaitInterval=\"5000\"/>");
            bldr.Append("     </OTA_AirBookRQ>");
            bldr.Append("    </EnhancedAirBookRQ>");
            bldr.Append("   </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getTravelItineraryAddPassengersRequest(string pcc, string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string SText, List<App.Models.SabreModels.AirPassengerDetails> passengers)
        {
            string passenger = passengerDetails(passengers);
            StringBuilder bldr = new StringBuilder();
            //string pcc = "6DTH";
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">TravelItineraryAddInfoLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>TravelItineraryAddInfoLLSRQ</eb:Action>");
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
            bldr.Append("    <TravelItineraryAddInfoRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.0\">");
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
            //bldr.Append("          <Ticketing TicketTimeLimit=\"" + AgencyTicketTime + "\" TicketType=\"" + AgencyTicketType + "\" PseudoCityCode=\"" + pcc + "\" QueueNumber=\"" + AgencyQueueNo + "\" ShortText=\"" + SText + "\"/>");
            bldr.Append("       </AgencyInfo>");
            bldr.Append("       <CustomerInfo>");
            bldr.Append(passenger);
            bldr.Append("       </CustomerInfo>");
            bldr.Append("    </TravelItineraryAddInfoRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }
        private string flightDetails(List<App.Models.SabreModels.AirFlightDetails> flights)
        {
            StringBuilder bldr = new StringBuilder();
            foreach (var flight in flights)
            {

                //bldr.Append("<FlightSegmentoption>");
                bldr.Append("   <FlightSegment DepartureDateTime=\"" + flight.DepartureDateTime + "\" ArrivalDateTime=\"" + flight.ArrivalDateTime + "\" FlightNumber=\"" + flight.FlightNumber + "\" BookingClass=\"" + flight.BookingClass + "\" NumberInParty=\"" + flight.NoofPassengers + "\" DirectionInd=\"" + flight.DirectionInd + "\" ResBookDesigCode=\"" + flight.resBookDesigCode + "\" Status=\"" + flight.status + "\">");
                bldr.Append("      <DepartureAirport LocationCode=\"" + flight.DepAirportLocationCode + "\"/>");
                bldr.Append("       <ArrivalAirport LocationCode=\"" + flight.ArrAirportLocationCode + "\"/>");
                bldr.Append("       <OperatingAirline Code=\"" + flight.OperatingAirlineCode + "\"/>");
                bldr.Append("       <Equipment AirEquipType=\"" + flight.Equipment + "\"/>");
                bldr.Append("       <MarketingAirline Code=\"" + flight.MarketingAirline + "\"/>");
                bldr.Append("     </FlightSegment>");
                //bldr.Append("</FlightSegmentoption>");
            }
            return bldr.ToString();
        }

        private string passengerDetails(List<App.Models.SabreModels.AirPassengerDetails> passengers)
        {
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<ContactNumbers>");
            foreach (var passenger in passengers)
            {
                bldr.Append("<ContactNumber LocationCode=\"" + passenger.PhoneLocationCode + "\" NameNumber=\"" + passenger.PassengerNameNumber + "\" Phone=\"" + passenger.PhoneNumber + "\" PhoneUseType=\"" + passenger.PhoneUseType + "\"/>");
            }
            bldr.Append("</ContactNumbers>");
            foreach (var passenger in passengers)
            {
                bldr.Append("          <Email Address=\"" + passenger.PassengerEmail + "\" NameNumber=\"" + passenger.PassengerNameNumber + "\" />");
                bldr.Append("          <PersonName NameNumber=\"" + passenger.PassengerNameNumber + "\"  PassengerType=\"" + passenger.PassengerNameRef + "\">");
                bldr.Append("               <GivenName>" + passenger.GivenName + "</GivenName>");
                bldr.Append("               <Surname>" + passenger.Surname + "</Surname>");
                bldr.Append("          </PersonName>");
            }
            return bldr.ToString();
        }



        #endregion MultiCity Search



        #region "Process Request"

        public string executeWebRequest(string requestXml)
        {
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
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                return pageContent;
            }
            return result;
        }

        public string executeCCWebRequest(string requestXml)
        {
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
                return pageContent;
            }
        }
        /// <returns>ResultCode, 1 if success.</returns>
        public string SessionCreate(string pcc)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(getSessionRequest(pcc));
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
                    stmw.Write(doc.OuterXml);
                }
            }
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
                return pageContent;
            }
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


        public string checkCCVerificationRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string amount, string currCode)
        {
            string result = "";
            result = executeCCWebRequest(getCCVerificationRequest(token, pcc, retain, code, airlineCode, ccNumber, expDate, amount, currCode));
            return result;
        }

        public string airChangePCC(string token, string pcc, string newpcc)
        {
            return executeWebRequest(getChangeAAARequest(token, pcc, newpcc));
        }

        public string airBFMRequest(string token, string rphOne, string rphOne_depDateTime, string rphOne_OriginLocCode, string rphOne_DesLocCode, string rphTwo, string rphTwo_depDateTime, string rphTwo_OriginLocCode, string rphTwo_DesLocCode, string cabin, string prefLevel, string tripType, string seatRequest, string pCode, string pQuantity)
        {
            return executeWebRequest(getBargainRequest(token, rphOne, rphOne_depDateTime, rphOne_OriginLocCode, rphOne_DesLocCode, rphTwo, rphTwo_depDateTime, rphTwo_OriginLocCode, rphTwo_DesLocCode, cabin, prefLevel, tripType, seatRequest, pCode, pQuantity));
        }

        public string airAvailRequest(string token, string departureCode, string originCode, string depDate)
        {
            string result = "";
            result = executeWebRequest(getAirAvailRequest(token, depDate, departureCode, originCode));
            return result;
        }

        public string airEnhanceBookRQ(string token, string dtDepartureTime, string dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code, string retain, string p_code, string quantity)
        {
            string result = "";
            result = executeWebRequest(getEnhancedBookingRequest("", token, dtDepartureTime, dtArrivalTime, flightNumber, destlocationCode, AirEquipType, originLocationCode, resBookDesigCode, status, code, retain, p_code, quantity));
            return result;

        }

        public string airBookRequest(string pcc, string token, string dtDepartureTime, string dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code)
        {
            string result = "";
            dtDepartureTime = DateTime.Now.Year.ToString() + "-" + dtDepartureTime.ToString();
            dtArrivalTime = DateTime.Now.Year.ToString() + "-" + dtArrivalTime.ToString();
            return result = executeWebRequest(getBookingRequest(pcc, token, dtDepartureTime, dtArrivalTime, flightNumber, destlocationCode, AirEquipType, originLocationCode, resBookDesigCode, status, code));

        }

        public string airShortSell_BookRequest(string token, string dtDepartureTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code)
        {
            string result = "";
            dtDepartureTime = DateTime.Now.Year.ToString() + "-" + dtDepartureTime.ToString();
            return result = executeWebRequest(getBookRequest_ShortSellRQ(token, dtDepartureTime, flightNumber, destlocationCode, AirEquipType, originLocationCode, resBookDesigCode, status, code));
        }

        public string addTravelItinerayRequest(string pcc, string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string GivenName, string Surname, string Phone1LocationCode, string PNameNumber, string Phone1, string Phone1UseType, string PType, string NameRef, string PassengerEmail, string SText, string EMailType, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo)
        {
            string result = "";
            result = executeWebRequest(getTravelItineraryAddInfoRequest(pcc, token, AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber, GivenName, Surname, Phone1LocationCode, PNameNumber, Phone1, Phone1UseType, PType, NameRef, PassengerEmail, SText, EMailType, AgencyTicketTime, AgencyTicketType, AgencyQueueNo));
            return result;
        }

        public string addPassengerRequest(string token, string isfalse, string ignoreAfter, string redispResv, string usCC, string issueCountry, string nationality, string id, string segmentNo, string docExpDate, string docNumber, string docType, string p_dob, string p_gender, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string GivenName, string middleName, string Surname, string PNameNumber, string Phone1LocationCode, string Phone1, string Phone1UseType, string PType, string PassengerEmail, string SText, string EMailType, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string pref, string segment, string segNumber)
        {
            string result = "";
            result = executeWebRequest(getPassengerInfoRequest(token, isfalse, ignoreAfter, redispResv, usCC, issueCountry, nationality, id, segmentNo, docExpDate, docNumber, docType, p_dob, p_gender, AgencyAddressLine, AgencyCityName, AgencyCountryCode, AgencyPostalCode, AgencyStateCode, AgencyStreetNumber, GivenName, middleName, Surname, PNameNumber, Phone1LocationCode, Phone1, Phone1UseType, PType, PassengerEmail, SText, EMailType, AgencyTicketTime, AgencyTicketType, AgencyQueueNo, pref, segment, segNumber));
            return result;
        }

        public string airPriceRequest(string token, string retain, string code, string quantity)
        {
            string result = "";

            result = executeWebRequest(getAirPriceRequest("", token, retain, code, quantity));

            return result;
        }
        public string airPriceRequest(string token, string retain, List<App.Models.SabreModels.AirNoofPassengers> airPassenger)
        {
            string result = "";

            result = executeWebRequest(getAirPriceRequest("", token, retain, airPassenger));

            return result;
        }

        public string airSeatBookRequest(string token, string nameNumber, string pref, string segNumber)
        {
            string result = "";
            result = executeWebRequest(getAirSeatRequest(token, nameNumber, pref, segNumber));
            return result;
        }

        public string airFlightdetails(string token, string dtDepartureTime, string flightNumber, string destlocationCode, string originLocationCode, string code)
        {
            string result = "";
            result = executeWebRequest(getFlightDetailsRequest(token, dtDepartureTime, flightNumber, destlocationCode, originLocationCode, code));

            return result;
        }

        //public string RetrievePNR(string token, string Id, string tStamp, string subArea)
        //{
        //    //String xml = textRequest.Text;
        //    XmlDocument doc = new XmlDocument();
        //    string result = "";
        //    return result = executeWebRequest(getTravelItineraryReadRequest(token, Id, tStamp, subArea));
        //}

        public string airCancelRequest(string token, string ind)
        {
            string result = "";

            result = executeWebRequest(getCancelAirIteRequest(token, ind));

            return result;
        }

        public string addSpecialRequest(string token, string code, string personName, string text)
        {
            return executeWebRequest(getAddSpecialRequest(token, code, personName, text));
        }

        public string endTransaction(string pcc, string token, string ind, string receivedFrom)
        {
            string result = "";
            return executeWebRequest(getEndTransactionRequest(pcc, token, ind, receivedFrom));

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

                if (filteredDocument.FirstChild.FirstChild.Attributes[0].Value == "Complete")
                {
                    val = filteredDocument.FirstChild.FirstChild.FirstChild.Attributes[0].Value;
                    string pnr = filteredDocument.InnerText.Split(' ')[2].ToString();
                    val = val + " " + pnr;
                }

            }
            catch (Exception ex)
            {

            }

            return val;
        }



        #endregion

        #region "Create Request XML"


        public Models.SabreModels.FlightPassengerDetails readPNRXML(string xmlFileName)
        {
            Models.SabreModels.FlightPassengerDetails Flight_Passenger_Details = new Models.SabreModels.FlightPassengerDetails();

            List <Models.SabreModels.PassengerDetails> passengers = new List<Models.SabreModels.PassengerDetails>();
            Models.SabreModels.PassengerDetails passenger = new Models.SabreModels.PassengerDetails();
            List<Models.SabreModels.FlightDetails> flights = new List<Models.SabreModels.FlightDetails>();
            Models.SabreModels.FlightDetails flight = new Models.SabreModels.FlightDetails();
            XmlDocument xDoc = new XmlDocument();
            string pnrNumber = string.Empty;
            xDoc.LoadXml(xmlFileName);
            XmlDocument filteredDocument = null;
            XmlNode filteredResponse = xDoc.SelectSingleNode("//*[local-name()='Body']/*");
            filteredDocument = new XmlDocument();
            filteredDocument.LoadXml(filteredResponse.OuterXml);
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
                                if (custNode.Name == "PersonName")
                                {
                                    foreach (XmlNode personNode in custNode.ChildNodes)
                                    {
                                        if (personNode.Name == "GivenName")
                                        {
                                            passenger.passengername = personNode.InnerText;
                                        }
                                        else if (personNode.Name == "Surname")
                                        {
                                            passenger.passengername = passenger.passengername + personNode.InnerText;
                                        }
                                        else if (personNode.Name == "Email")
                                        {
                                            passenger.email = personNode.InnerText;
                                        }
                                        if (!passengers.Contains(passenger))
                                        {
                                            passengers.Add(passenger);
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
                                                flight = new Models.SabreModels.FlightDetails();
                                                if (flightItemNode.Name == "FlightSegment")
                                                {

                                                    foreach (XmlNode flightNode in flightItemNode.ChildNodes)
                                                    {
                                                        flight.ArrivalDate = flightItemNode.Attributes["ArrivalDateTime"].InnerText;
                                                        flight.DepartureDate = flightItemNode.Attributes["DepartureDateTime"].InnerText;
                                                        flight.FlightNumber = flightItemNode.Attributes["FlightNumber"].InnerText;
                                                        flight.DistanceTravel = flightItemNode.Attributes["AirMilesFlown"].InnerText;
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
                                                        else if (flightNode.Name == "MarketingAirline")
                                                        {
                                                            flight.AirlineName = flightNode.Attributes["Code"].InnerText;
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
                            }

                        }
                        else if (mainNode.Name == "ItineraryRef")
                        {
                            pnrNumber = mainNode.Attributes["ID"].InnerText;
                        }

                    }

                }
            }
            Flight_Passenger_Details.Passenger_Details = passengers;
            Flight_Passenger_Details.Flight_Details = flights;

            return Flight_Passenger_Details;
        }
        private String getSessionRequest(string pcc)
        {

            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            //String pcc = "6DTH";

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

        private String getAirAvailRequest(string token, string dtDepartureTime, string destlocationCode, string originLocationCode)
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">OTA_AirAvailService</eb:Service>");
            bldr.Append("       <eb:Action>OTA_AirAvailLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <OTA_AirAvailRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.2.0\">");
            bldr.Append("       <OriginDestinationInformation>");
            bldr.Append("          <FlightSegment  DepartureDateTime=\"" + dtDepartureTime + "\">");
            bldr.Append("               <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            bldr.Append("               <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            bldr.Append("          </FlightSegment>");
            bldr.Append("       </OriginDestinationInformation>");
            bldr.Append("    </OTA_AirAvailRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getAirPriceRequest(string pcc, string token, string retain, string code, string quantity)
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
            bldr.Append("    <OTA_AirPriceRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.11.0\">");
            bldr.Append("       <PriceRequestInformation Retain=\"" + retain + "\">");
            bldr.Append("          <OptionalQualifiers>");
            bldr.Append("              <PricingQualifiers>");
            bldr.Append("               <PassengerType  Code=\"" + code + "\" Quantity=\"" + quantity + "\"/>");
            bldr.Append("             </PricingQualifiers>");
            bldr.Append("          </OptionalQualifiers>");
            bldr.Append("       </PriceRequestInformation>");
            bldr.Append("    </OTA_AirPriceRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }
        private String getAirPriceRequest(string pcc, string token, string retain, List<App.Models.SabreModels.AirNoofPassengers> pQuantity)
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
            bldr.Append("    <OTA_AirPriceRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.11.0\">");
            bldr.Append("       <PriceRequestInformation Retain=\"" + retain + "\">");
            bldr.Append("          <OptionalQualifiers>");
            bldr.Append("              <PricingQualifiers>");
            foreach (var pass in pQuantity)
            {
                bldr.Append("               <PassengerType  Code=\"" + pass.PassengerCode + "\" Quantity=\"" + pass.NoOfPassengers + "\"/>");
            }
            bldr.Append("             </PricingQualifiers>");
            bldr.Append("          </OptionalQualifiers>");
            bldr.Append("       </PriceRequestInformation>");
            bldr.Append("    </OTA_AirPriceRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getAirSeatRequest(string token, string nameNumber, string pref, string segNumber)
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">AirSeatLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>AirSeatLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Seat Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("<sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("</sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("    <AirSeatRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.0\">");
            bldr.Append("       <Seats>");
            bldr.Append("           <Seat  NameNumber=\"" + nameNumber + "\" Preference=\"" + pref + "\" SegmentNumber=\"" + segNumber + "\"/>");
            bldr.Append("       </Seats>");
            bldr.Append("    </AirSeatRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getBookingRequest(string pcc, string token, string dtDepartureTime, string dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code)
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">OTA_AirBookLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>OTA_AirBookLLSRQ</eb:Action>");
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
            bldr.Append("    <OTA_AirBookRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.0\">");
            bldr.Append("      <OriginDestinationInformation>");
            bldr.Append("          <FlightSegment  DepartureDateTime =\"" + dtDepartureTime + "\" ArrivalDateTime=\"" + dtArrivalTime + "\" FlightNumber=\"" + flightNumber + "\" NumberInParty=\"2\" ResBookDesigCode=\"" + resBookDesigCode + "\" Status=\"" + status + "\">");
            bldr.Append("          <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            bldr.Append("          <Equipment  AirEquipType=\"" + AirEquipType + "\"/>");
            bldr.Append("          <MarketingAirline  Code=\"" + code + "\" FlightNumber=\"" + flightNumber + "\"/>");
            bldr.Append("          <OperatingAirline  Code=\"" + code + "\"/>");
            bldr.Append("          <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            bldr.Append("          </FlightSegment>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     </OTA_AirBookRQ>");
            bldr.Append("   </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getBookingReturnRequest(string token, DateTime dtDepartureTime, DateTime dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code, DateTime dtDepartureTime_return, DateTime dtArrivalTime_return, string flightNumber_return, string destlocationCode_return, string AirEquipType_return, string originLocationCode_return)
        {
            StringBuilder bldr = new StringBuilder();
            string pcc = "6DTH";
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\" xmlns:ns1=\"http://www.opentravel.org/OTA/2002/11\">");
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
            bldr.Append("       <eb:Service eb:type=\"OTA\">Air Shopping Service</eb:Service>");
            bldr.Append("       <eb:Action>OTA_AirBookLLSRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>OTA_AirBookLLSRQ Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append("     <sec:Security  xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("       <sec:BinarySecurityToken valueType=\"String\" EncodingType=\"sec:Base64Binary\">" + token + "</sec:BinarySecurityToken>");
            bldr.Append("    </sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("      <OTA_AirBookRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.3.0\">");
            bldr.Append("      <OriginDestinationInformation>");
            bldr.Append("          <FlightSegment  DepartureDateTime=\"" + dtDepartureTime + "\" ArrivalDateTime=\"" + dtArrivalTime + "\" FlightNumber=\"" + flightNumber + "\" NumberInParty=\"2\" ResBookDesigCode=\"" + resBookDesigCode + "\" Status=\"" + status + "\">");
            bldr.Append("           <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            bldr.Append("           <Equipment  AirEquipType=\"" + AirEquipType + "\"/>");
            bldr.Append("           <MarketingAirline  Code=\"" + code + "\" FlightNumber=\"" + flightNumber + "\"/>");
            bldr.Append("           <OperatingAirline  Code=\"" + code + "\"/>");
            bldr.Append("           <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            bldr.Append("          </FlightSegment>");
            bldr.Append("          <FlightSegment  DepartureDateTime=\"" + dtDepartureTime_return + "\" ArrivalDateTime=\"" + dtArrivalTime_return + "\" FlightNumber=\"" + flightNumber_return + "\" NumberInParty=\"2\" ResBookDesigCode=\"" + resBookDesigCode + "\" Status=\"" + status + "\">");
            bldr.Append("           <DestinationLocation LocationCode=\"" + destlocationCode_return + "\"/>");
            bldr.Append("           <Equipment  AirEquipType=\"" + AirEquipType_return + "\"/>");
            bldr.Append("           <MarketingAirline  Code=\"" + code + "\" FlightNumber=\"" + flightNumber_return + "\"/>");
            bldr.Append("           <OperatingAirline  Code=\"" + code + "\"/>");
            bldr.Append("           <OriginLocation LocationCode=\"" + originLocationCode_return + "\"/>");
            bldr.Append("          </FlightSegment>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("      </OTA_AirBookRQ >");
            bldr.Append("     </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getEnhancedBookingRequest(string pcc, string token, string dtDepartureTime, string dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code, string retain, string p_code, string quantity)
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
            bldr.Append("    <EnhancedAirBookRQ  xmlns=\"http://services.sabre.com/sp/eab/v3_6\"  version=\"3.6.0\" IgnoreOnError=\"true\" HaltOnError=\"true\" >");
            bldr.Append("     <OTA_AirBookRQ>");
            bldr.Append("       <HaltOnStatus Code=\"NO\"/>");
            bldr.Append("       <HaltOnStatus Code=\"NN\"/>");
            bldr.Append("       <HaltOnStatus Code=\"UC\"/>");
            bldr.Append("       <HaltOnStatus Code=\"US\"/>");
            bldr.Append("      <OriginDestinationInformation>");
            bldr.Append("          <FlightSegment  DepartureDateTime =\"" + dtDepartureTime + "\" ArrivalDateTime=\"" + dtArrivalTime + "\" FlightNumber=\"" + flightNumber + "\" NumberInParty=\"1\" ResBookDesigCode=\"" + resBookDesigCode + "\" Status=\"" + status + "\">");
            bldr.Append("           <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            bldr.Append("           <Equipment  AirEquipType=\"" + AirEquipType + "\"/>");
            bldr.Append("           <MarketingAirline  Code=\"" + code + "\" FlightNumber=\"" + flightNumber + "\"/>");
            bldr.Append("           <OperatingAirline  Code=\"" + code + "\"/>");
            bldr.Append("           <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            bldr.Append("           </FlightSegment>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     <RedisplayReservation NumAttempts=\"2\" WaitInterval=\"5000\"/>");
            bldr.Append("     </OTA_AirBookRQ>");
            bldr.Append("    </EnhancedAirBookRQ>");
            bldr.Append("   </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getFlightDetailsRequest(string token, string dtDepartureTime, string flightNumber, string destlocationCode, string originLocationCode, string code)
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">VerifyFlightDetailsRQ</eb:Service>");
            bldr.Append("       <eb:Action>VerifyFlightDetailsRQ</eb:Action>");
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
            bldr.Append("    <VerifyFlightDetailsRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.0.0\">");
            bldr.Append("      <OriginDestinationInformation>");
            bldr.Append("          <FlightSegment  DepartureDateTime =\"" + dtDepartureTime + "\" FlightNumber=\"" + flightNumber + "\">");
            bldr.Append("           <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            bldr.Append("           <MarketingAirline  Code=\"" + code + "\"/>");
            bldr.Append("           <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            bldr.Append("          </FlightSegment>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     </VerifyFlightDetailsRQ>");
            bldr.Append("   </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getTravelItineraryAddInfoRequest(string pcc, string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string GivenName, string Surname, string Phone1LocationCode, string PNameNumber, string Phone1, string Phone1UseType, string PType, string NameRef, string PassengerEmail, string SText, string EMailType, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo)
        {
            StringBuilder bldr = new StringBuilder();
            //string pcc = "6DTH";
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">TravelItineraryAddInfoLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>TravelItineraryAddInfoLLSRQ</eb:Action>");
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
            bldr.Append("    <TravelItineraryAddInfoRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.0\">");
            bldr.Append("       <AgencyInfo>");
            bldr.Append("          <Address>");
            bldr.Append("               <AddressLine>" + AgencyAddressLine + "</AddressLine>");
            bldr.Append("               <CityName>" + AgencyCityName + "</CityName>");
            bldr.Append("               <CountryCode>" + AgencyCountryCode + "</CountryCode>");
            bldr.Append("               <PostalCode>" + AgencyPostalCode + "</PostalCode>");
            bldr.Append("               <StateCountyProv StateCode=\"" + AgencyStateCode + "\"/>");
            bldr.Append("               <StreetNmbr>" + AgencyStreetNumber + "</StreetNmbr>");
            bldr.Append("          </Address>");
            //bldr.Append("          <Ticketing TicketTimeLimit=\"" + AgencyTicketTime + "\" TicketType=\"" + AgencyTicketType + "\" PseudoCityCode=\"" + pcc + "\" QueueNumber=\"" + AgencyQueueNo + "\" ShortText=\"" + SText + "\"/>");
            bldr.Append("          <Ticketing TicketType=\"" + AgencyTicketType + "\" PseudoCityCode=\"" + pcc + "\" QueueNumber=\"" + AgencyQueueNo + "\" ShortText=\"" + SText + "\"/>");
            bldr.Append("       </AgencyInfo>");
            bldr.Append("       <CustomerInfo>");
            bldr.Append("          <ContactNumbers>");
            bldr.Append("               <ContactNumber LocationCode=\"" + Phone1LocationCode + "\" NameNumber=\"" + PNameNumber + "\" Phone=\"" + Phone1 + "\" PhoneUseType=\"" + Phone1UseType + "\"/>");
            bldr.Append("          </ContactNumbers>");
            bldr.Append("          <Email Address=\"" + PassengerEmail + "\" NameNumber=\"" + PNameNumber + "\" />");
            bldr.Append("          <PersonName NameNumber=\"" + PNameNumber + "\"  PassengerType=\"" + NameRef + "\">");
            bldr.Append("               <GivenName>" + GivenName + "</GivenName>");
            bldr.Append("               <Surname>" + Surname + "</Surname>");
            bldr.Append("          </PersonName>");
            bldr.Append("       </CustomerInfo>");
            bldr.Append("    </TravelItineraryAddInfoRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getTravelItineraryAddInfantRequest(string token, string GivenName, string Surname, string PType)
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">TravelItineraryAddInfoRQ</eb:Service>");
            bldr.Append("       <eb:Action>TravelItineraryAddInfoRQ</eb:Action>");
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
            bldr.Append("    <TravelItineraryAddInfoRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.0\">");
            //bldr.Append("       <AgencyInfo>");
            //bldr.Append("          <Address>");
            //bldr.Append("               <AddressLine>" + AgencyAddressLine + "</AddressLine>");
            //bldr.Append("               <CityName>" + AgencyCityName + "</CityName>");
            //bldr.Append("               <CountryCode>" + AgencyCountryCode + "</CountryCode>");
            //bldr.Append("               <PostalCode>" + AgencyPostalCode + "</PostalCode>");
            //bldr.Append("               <StateCountyProv StateCode=\"" + AgencyStateCode + "\"/>");
            //bldr.Append("               <StreetNmbr>" + AgencyStreetNumber + "</StreetNmbr>");
            //bldr.Append("          </Address>");
            //bldr.Append("          <Ticketing TicketTimeLimit=\"" + AgencyTicketTime + "\" TicketType=\"" + AgencyTicketType + "\" PseudoCityCode=\"" + pcc + "\" QueueNumber=\"" + AgencyQueueNo + "\" ShortText=\"" + SText + "\"/>");
            //bldr.Append("       </AgencyInfo>");
            bldr.Append("       <CustomerInfo>");
            bldr.Append("          <PersonName Infant=\"true\" >");
            bldr.Append("               <GivenName>" + GivenName + "</GivenName>");
            bldr.Append("               <Surname>" + Surname + "</Surname>");
            bldr.Append("          </PersonName>");
            //bldr.Append("          <ContactNumbers>");
            //bldr.Append("          <ContactNumber LocationCode=\"" + Phone1LocationCode + "\" NameNumber=\"" + PNameNumber + "\" Phone=\"" + Phone1 + "\" PhoneUseType=\"" + Phone1UseType + "\"/>");
            //bldr.Append("          </ContactNumbers>");
            //bldr.Append("          <Email Address=\"" + PassengerEmail + "\" NameNumber=\"" + PNameNumber + "\" ShortText=\"" + SText + "\" Type=\"" + EMailType + "\"/>");
            bldr.Append("       </CustomerInfo>");
            bldr.Append("    </TravelItineraryAddInfoRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getPassengerInfoRequest(string token, string isfalse, string ignoreAfter, string redispResv, string usCC, string issueCountry, string nationality, string id, string segmentNo, string docExpDate, string docNumber, string docType, string p_dob, string p_gender, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string GivenName, string middleName, string Surname, string PNameNumber, string Phone1LocationCode, string Phone1, string Phone1UseType, string PType, string PassengerEmail, string SText, string EMailType, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string pref, string segment, string segNumber)
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
            bldr.Append("          <PreProcessing IgnoreBefore=\"" + isfalse + "\">");
            bldr.Append("             <UniqueID ID=\"" + id + "\"/>");
            bldr.Append("           </PreProcessing>");
            bldr.Append("        <SpecialReqDetails>");
            bldr.Append("           <AirSeatRQ>");
            bldr.Append("               <Seats>");
            bldr.Append("                   <Seat BoardingPass=\"true\" ChangeOfGauge=\"true\" NameNumber=\"" + PNameNumber + "\" Preference=\"" + pref + "\" SegmentNumber=\"" + segment + "\"/>");
            bldr.Append("               </Seats>");
            bldr.Append("           </AirSeatRQ>");
            bldr.Append("          <SpecialServiceRQ>");
            bldr.Append("            <SpecialServiceInfo>");
            bldr.Append("             <AdvancePassenger SegmentNumber=\"" + segNumber + "\">");
            //bldr.Append("               <Document ExpirationDate=\"" + docExpDate + "\" Number=\"" + docNumber + "\" Type=\"" + docType + "\">");
            //bldr.Append("                   <IssueCountry>" + issueCountry + "</IssueCountry>");
            //bldr.Append("                   <NationalityCountry>" + nationality + "</NationalityCountry>");
            //bldr.Append("               </Document>");
            //bldr.Append("               <PersonName DateOfBirth=\"" + p_dob + "\" Gender=\"" + p_gender + "\" NameNumber=\"" + PNameNumber + "\" DocumentHolder=\"" + isfalse + "\">");
            //bldr.Append("                   <GivenName>"+ GivenName+"</GivenName>");
            //bldr.Append("                   <MiddleName>"+ middleName +"</MiddleName>");
            //bldr.Append("                   <Surname>"+ Surname+"</Surname>");
            //bldr.Append("               </PersonName>");
            bldr.Append("               <VendorPrefs>");
            bldr.Append("                   <Airline Hosted=\"false\"/>");
            bldr.Append("               </VendorPrefs>");
            bldr.Append("          </AdvancePassenger>");
            bldr.Append("        </SpecialServiceInfo>");
            bldr.Append("       </SpecialServiceRQ>");
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
            bldr.Append("          <Ticketing TicketTimeLimit=\"" + AgencyTicketTime + "\" TicketType=\"" + AgencyTicketType + "\" PseudoCityCode=\"" + pcc + "\" QueueNumber=\"" + AgencyQueueNo + "\" ShortText=\"" + SText + "\"/>");
            bldr.Append("       </AgencyInfo>");
            bldr.Append("       <CustomerInfo>");
            bldr.Append("          <ContactNumbers>");
            bldr.Append("               <ContactNumber  InsertAfter=\"0\" NameNumber=\"" + PNameNumber + "\"  Phone=\"" + Phone1 + "\"  PhoneUseType=\"H\"/>");
            bldr.Append("               <ContactNumber PhoneUseType=\"A\" Phone=\"1-877-389-7388\" />");
            bldr.Append("          </ContactNumbers>");
            bldr.Append("          <Email Address=\"" + PassengerEmail + "\" NameNumber=\"" + PNameNumber + "\" ShortText=\"" + SText + "\" Type=\"" + EMailType + "\"/>");
            bldr.Append("          <Email Address=\"reservations@skyflight.ca\" />");
            //<Email Address="reservations@skyflight.ca"/>
            bldr.Append("          <PersonName NameNumber=\"" + PNameNumber + "\" PassengerType=\"" + PType + "\">");
            bldr.Append("               <GivenName>" + GivenName + "</GivenName>");
            bldr.Append("               <Surname>" + Surname + "</Surname>");
            bldr.Append("          </PersonName>");
            bldr.Append("       </CustomerInfo>");
            bldr.Append("    </TravelItineraryAddInfoRQ>");
            bldr.Append("    </PassengerDetailsRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getBargainRequest(string token, string rphOne, string rphOne_depDateTime, string rphOne_OriginLocCode, string rphOne_DesLocCode, string rphTwo, string rphTwo_depDateTime, string rphTwo_OriginLocCode, string rphTwo_DesLocCode, string cabin, string prefLevel, string tripType, string seatRequest, string pCode, string pQuantity)
        {
            string one = "1";
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">BargainFinderMaxRQ</eb:Service>");
            bldr.Append("       <eb:Action>BargainFinderMaxRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Air Availibility Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");
            bldr.Append(" <sec:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append(" </sec:Security>");
            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("      <OTA_AirLowFareSearchRQ  xmlns=\"http://www.opentravel.org/OTA/2003/05\"  Version=\"1.9.5\">");
            bldr.Append("          <POS>");
            bldr.Append("              <Source PseudoCityCode=\"" + pcc + "\">");
            bldr.Append("                  <RequestorID ID=\"" + one + "\" Type=\"" + one + "\">");
            bldr.Append("                      <CompanyName Code=\"TN\"/>");
            bldr.Append("                  </RequestorID>");
            bldr.Append("              </Source>");
            bldr.Append("          </POS>");
            bldr.Append("      <OriginDestinationInformation RPH=\"" + rphOne + "\">");
            bldr.Append("          <DepartureDateTime>" + rphOne_depDateTime + "</DepartureDateTime>");
            bldr.Append("          <OriginLocation LocationCode=\"" + rphOne_OriginLocCode + "\"/>");
            bldr.Append("          <DestinationLocation LocationCode=\"" + rphOne_DesLocCode + "\"/>");
            bldr.Append("          <TPA_Extensions>");
            bldr.Append("              <SegmentType Code=\"O\"/>");
            bldr.Append("          </TPA_Extensions>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("      <OriginDestinationInformation RPH=\"" + rphTwo + "\">");
            bldr.Append("          <DepartureDateTime>" + rphTwo_depDateTime + "</DepartureDateTime>");
            bldr.Append("          <OriginLocation LocationCode=\"" + rphTwo_OriginLocCode + "\"/>");
            bldr.Append("          <DestinationLocation LocationCode=\"" + rphTwo_DesLocCode + "\"/>");
            bldr.Append("          <TPA_Extensions>");
            bldr.Append("              <SegmentType Code=\"O\"/>");
            bldr.Append("          </TPA_Extensions>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("      <TravelPreferences ValidInterlineTicket=\"true\">");
            bldr.Append("          <CabinPref Cabin=\"" + cabin + "\" PreferLevel=\"" + prefLevel + "\"/>");
            bldr.Append("          <TPA_Extensions>");
            bldr.Append("              <TripType Value=\"" + tripType + "\"/>");
            bldr.Append("          </TPA_Extensions>");
            bldr.Append("      </TravelPreferences>");
            bldr.Append("      <TravelerInfoSummary>");
            bldr.Append("          <SeatsRequested>" + seatRequest + "</SeatsRequested>");
            bldr.Append("          <AirTravelerAvail>");
            bldr.Append("              <PassengerTypeQuantity Code=\"" + pCode + "\" Quantity=\"" + pQuantity + "\"/>");
            bldr.Append("          </AirTravelerAvail>");
            bldr.Append("      </TravelerInfoSummary>");
            bldr.Append("      <TPA_Extensions>");
            bldr.Append("          <IntelliSellTransaction>");
            bldr.Append("              <RequestType Name=\"50ITINS\"/>");
            bldr.Append("          </IntelliSellTransaction>");
            bldr.Append("      </TPA_Extensions>");
            bldr.Append("      </OTA_AirLowFareSearchRQ>");
            bldr.Append("     </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getBookRequest_ShortSellRQ(string token, string dtDepartureTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code)
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
            bldr.Append("          <FlightSegment  DepartureDateTime =\"" + dtDepartureTime + "\"  FlightNumber=\"" + flightNumber + "\" NumberInParty=\"1\" ResBookDesigCode=\"" + resBookDesigCode + "\" Status=\"" + status + "\">");
            bldr.Append("          <DestinationLocation LocationCode=\"" + destlocationCode + "\"/>");
            bldr.Append("          <MarketingAirline  Code=\"" + code + "\" FlightNumber=\"" + flightNumber + "\"/>");
            bldr.Append("          <OriginLocation LocationCode=\"" + originLocationCode + "\"/>");
            bldr.Append("          </FlightSegment>");
            bldr.Append("      </OriginDestinationInformation>");
            bldr.Append("     </ShortSellRQ>");
            bldr.Append("     </soapenv:Body>");
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

        private String getCCVerificationRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string amount, string currCode)
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
            bldr.Append("    <CreditVerificationRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.1.0\">");
            bldr.Append("       <Credit>");
            bldr.Append("          <CC_Info>");
            bldr.Append("               <PaymentCard AirlineCode=\"" + airlineCode + "\" Code=\"" + code + "\" ExpireDate=\"" + expDate + "\" Number=\"" + ccNumber + "\"/>");
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



        /////


        private String getTravelItineraryReadRequest(string token, string Id, string tStamp, string subArea)
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


        private String getAddSpecialRequest(string token, string code, string personName, string text)
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
            bldr.Append("       <eb:Service eb:type=\"sabreXML\">SpecialServiceLLSRQ</eb:Service>");
            bldr.Append("       <eb:Action>SpecialServiceLLSRQ</eb:Action>");
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
            bldr.Append("    <SpecialServiceRQ  xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Version=\"2.2.0\">");
            bldr.Append("       <SpecialServiceInfo>");
            bldr.Append("           <Service SSR_Code=\"" + code + "\">");
            bldr.Append("               <PersonName ID=\"" + personName + "\"/>");
            bldr.Append("               <Text>" + text + "<Text>");
            bldr.Append("           </Service>");
            bldr.Append("       <SpecialServiceInfo/>");
            bldr.Append("    </SpecialServiceRQ>");
            bldr.Append("</soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private String getEndTransactionRequest(string token, string ind, string receivedFrom)
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



        #endregion "Create Request XML"
    }
}
