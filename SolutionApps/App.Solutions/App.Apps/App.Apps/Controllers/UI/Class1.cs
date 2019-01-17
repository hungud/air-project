using App.Apps.ViewModal;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace App.Apps.Controllers.UI
{
    public class Class1
    {
        private string FormatDate(string datetime)
        {
            if (string.IsNullOrEmpty(datetime))
                return string.Empty;

            DateTime bookingDate = default(DateTime);
            DateTime.TryParse(datetime, out bookingDate);
            return bookingDate.ToString("dddd, dd MMMM yyyy hh:mm:ss");
        }
        public async Task<ActionResult> GetBookingDetails(int Id)
        {
            string APIUrl = ConfigurationManager.AppSettings["AirServiceUrl"].ToString();
            string siteLayout = ConfigurationManager.AppSettings["mainSite"].ToString();

            Common.BookingDetailsRQModal RequestObject = new Common.BookingDetailsRQModal();

            var htmlDoc = new HtmlDocument();


            VMBookingDetails data = new BookingsService().GetBookingDetailsByID(Id);
            //RequestObject.PCC = "6DTH";
            RequestObject.PNR = data.PNR;

            Common.CommonUtility Utility_resp = new Common.CommonUtility();
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(APIUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                //set Accept headers
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml,application/json");
                //set User agent
                //client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; EN; rv:11.0) like Gecko");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                //StringContent content = new StringContent(JsonConvert.SerializeObject(RequestObject), Encoding.UTF8, "application/json");
                string queryString = APIUrl + "/api/CommonService/GetCommonService?CommonServiceType=AirlinesListDetails&SearchText=SearchText";
                HttpResponseMessage response = await client.GetAsync(queryString);
                //response.Wait();
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Utility_resp = JsonConvert.DeserializeObject<Common.CommonUtility>(EmpResponse);
                }
            }




            if (RequestObject.PNR == null)
            {
                ViewBag.Response = "<div class='col-md-12' id='error_content' style='color:red;text-align:center;'><h2>The reservation has been cancelled, Please contact Customer service</h2></div></div></div>";
                return View();
            }

            //HttpResponseMessage response=aGetBookingDetails(RequestObject).Result;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(APIUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                FlightPassengerDetails resp = new FlightPassengerDetails();

                //StringContent content = new StringContent(JsonConvert.SerializeObject(RequestObject), Encoding.UTF8, "application/json");
                string queryString = APIUrl + "/api/AirReservationBooking/GetBookingDetails?PNR=" + RequestObject.PNR + "&PCC=" + RequestObject.PCC;
                HttpResponseMessage response = await client.GetAsync(queryString);
                //response.Wait();
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = response.Content.ReadAsStringAsync().Result;



                    //Deserializing the response recieved from web api and storing into the Employee list  
                    resp = JsonConvert.DeserializeObject<FlightPassengerDetails>(EmpResponse);


                    if (resp.flights_details.Count == 0)
                    {
                        ViewBag.Response = "<div class='col-md-12' id='error_content' style='color:red;text-align:center;'><h2>The reservation has been cancelled, Please contact customer service</h2></div></div></div>";

                        return View();
                    }

                    //if (resp.RequestID == null)
                    //{
                    //    ViewBag.Response = "<div class='col-md-12' id='error_content' style='color:red;text-align:center;'><h2>The reservation has been cancelled, Please contact customer service</h2></div></div></div>";

                    //    return View();
                    //}



                    // var html = siteLayout +"/layout.html";
                    var html = siteLayout + "/layout.html";
                    HtmlWeb web = new HtmlWeb();

                    htmlDoc = web.Load(html);


                    var header_passenger_section = htmlDoc.GetElementbyId("html_header_Passenger_section");
                    //Prepared For
                    var name = string.Empty;
                    if (resp.passengers_details.Count > 0 && !string.IsNullOrEmpty(resp.passengers_details[0].passengername))
                    {
                        var splitName = resp.passengers_details[0].passengername.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (splitName.Length == 3)
                        {
                            name = resp.passengers_details[0].Surname + " " + splitName[1] + " " + splitName[2] + " " + splitName[0];
                        }
                        else if (splitName.Length == 2)
                        {
                            name = resp.passengers_details[0].Surname + " " + splitName[1] + " " + splitName[0];
                        }
                        else
                        {
                            name = (resp.passengers_details[0].passengername + " " + resp.passengers_details[0].Surname);
                        }
                    }


                    header_passenger_section.SelectSingleNode("//p[@id='html_preparedfor']").InnerHtml = resp.passengers_details.Count > 0 ? name : string.Empty;
                    header_passenger_section.SelectSingleNode("//span[@id='html_bookingcode']").InnerHtml = string.IsNullOrEmpty(resp.pnrnumber) ? string.Empty : resp.pnrnumber;
                    var reservatinCode = string.IsNullOrEmpty(resp.AirLineReservationCode) ? "" : (resp.AirLineReservationCode.Split('*').Length > 1 ? resp.AirLineReservationCode.Split('*')[1] : "");
                    header_passenger_section.SelectSingleNode("//span[@id='html_reservationcode']").InnerHtml = ":" + reservatinCode;
                    //header_passenger_section.SelectSingleNode("//span[@id='html_companyname']").InnerHtml = "SkyFlight Travel Center";
                    header_passenger_section.SelectSingleNode("//span[@id='html_companyaddress']").InnerHtml = resp.passengers_details.Count > 0 ? (resp.passengers_details[0].passengerAddress) : string.Empty;
                    header_passenger_section.SelectSingleNode("//span[@id='html_companymail']").InnerHtml = resp.passengers_details.Count > 0 ? (resp.passengers_details[0].email) : string.Empty;
                    header_passenger_section.SelectSingleNode("//span[@id='html_companyphone']").InnerHtml = resp.passengers_details.Count > 0 ? (resp.passengers_details[0].PhoneNumber) : string.Empty;
                    header_passenger_section.SelectSingleNode("//span[@id='html_bookingtime']").InnerHtml = FormatDate(resp.BookingDate);


                    var FlightNode = htmlDoc.GetElementbyId("Confirmation_FlightDetails");

                    var PaymentDetailsNode = htmlDoc.GetElementbyId("Confirmation_PaymentDetail");

                    var InnerFlightNodes = htmlDoc.GetElementbyId("html_flight_node");


                    var Confirmation_Title_Node = htmlDoc.GetElementbyId("confirmation_title");

                    //"11/1/2018 12:00:00 AM"
                    string _DepartureDate = data.DepartureDate.Split(' ')[0].ToString();
                    int year = Convert.ToInt32(_DepartureDate.Split('/')[2].ToString());
                    int month = Convert.ToInt32(_DepartureDate.Split('/')[0].ToString());
                    int day = Convert.ToInt32(_DepartureDate.Split('/')[1].ToString());
                    int hour = Convert.ToInt32(data.DepartureDate.Split(' ')[1].ToString().Split(':')[0].ToString());
                    int min = Convert.ToInt32(data.DepartureDate.Split(' ')[1].ToString().Split(':')[1].ToString());
                    DateTime a = new DateTime(year, month, day, hour, min, 00);
                    _DepartureDate = String.Format("{0:ddd, MMM dd yyyy}", a);

                    string _ReturnDate = data.ReturnDate.Split(' ')[0].ToString();
                    int ryear = Convert.ToInt32(_ReturnDate.Split('/')[2].ToString());
                    int rmonth = Convert.ToInt32(_ReturnDate.Split('/')[0].ToString());
                    int rday = Convert.ToInt32(_ReturnDate.Split('/')[1].ToString());
                    int rhour = Convert.ToInt32(data.ReturnDate.Split(' ')[1].ToString().Split(':')[0].ToString());
                    int rmin = Convert.ToInt32(data.ReturnDate.Split(' ')[1].ToString().Split(':')[1].ToString());
                    DateTime ra = new DateTime(ryear, rmonth, rday, rhour, rmin, 00);
                    _ReturnDate = String.Format("{0:ddd, MMM dd yyyy}", ra);

                    string _headerDeparture = "";
                    string _headerDestination = "";

                    foreach (var item in Utility_resp.Data1)
                    {
                        if (item.airlinecode == data.Departure)
                        {
                            _headerDeparture = item.airlinename;
                        }
                        if (item.airlinecode == data.Destination)
                        {
                            _headerDestination = item.airlinename;
                        }
                    }

                    if (data.TripType == 2)
                    {
                        //Round Trip

                        Confirmation_Title_Node.SelectSingleNode("//span[@id='html_dep_date']").InnerHtml = "Departure: " + _DepartureDate;
                        Confirmation_Title_Node.SelectSingleNode("//span[@id='html_return_date']").InnerHtml = "Return: " + _ReturnDate;
                        Confirmation_Title_Node.SelectSingleNode("//span[@id='html_dep_city']").InnerHtml = _headerDeparture;
                        Confirmation_Title_Node.SelectSingleNode("//span[@id='html_return_city']").InnerHtml = _headerDestination;

                    }
                    else
                    {
                        Confirmation_Title_Node.SelectSingleNode("//span[@id='html_dep_date']").InnerHtml = "Departure: " + _DepartureDate;
                        //Confirmation_Title_Node.SelectSingleNode("//span[@id='html_return_date']").InnerHtml = resp.pnrnumber;

                        Confirmation_Title_Node.SelectSingleNode("//span[@id='html_return_date']").InnerHtml = data.Destination;
                    }

                    try
                    {
                        //Flight Loop
                        string FlitArrivalDateTimeforStopColculation = "";
                        int Stop = 0;
                        int consumed_minutes = 0;
                        int len = resp.flights_details.Count;
                        string sequence = "";
                        int dvcount = 0;
                        HtmlNodeCollection allnodes;

                        foreach (var item in resp.flights_details)
                        {

                            dvcount += 1;
                            List<string> ChkDepArray = new List<string>();
                            List<string> ChkArrivArray = new List<string>();
                            var htmlbody = InnerFlightNodes;
                            string _nodehtml = InnerFlightNodes.InnerHtml;


                            HtmlNode IterativeNode = HtmlNode.CreateNode("<div id='html_flight_node'></div>");

                            IterativeNode.CopyFrom(htmlbody);
                            //var iterativeBody = IterativeNode.SelectSingleNode("//span[@id='html_flights_dep_date']");

                            //Fill Departure Array List
                            string str_marketingairline = "";
                            string str_airline = "";
                            string Dep_Airport = "";
                            string Arr_Airport = "";
                            foreach (var d in Utility_resp.Data1)
                            {
                                if (d.airlinecode == item.DepartureCityCode)
                                {
                                    Dep_Airport = d.airlinename;
                                }
                                if (d.airlinecode == item.ArrivalCityCode)
                                {
                                    Arr_Airport = d.airlinename;
                                }
                            }

                            foreach (var d in Utility_resp.Data)
                            {
                                if (d.airlinecode == item.MarketingAirline)
                                {
                                    str_marketingairline = d.airlinename;
                                }
                                if (d.airlinecode == item.AirlineName)
                                {
                                    str_airline = d.airlinename;
                                }
                            }
                            try
                            {
                                foreach (var d in Utility_resp.Data1)
                                {
                                    string _airlinecode = d.airlinecode;
                                    string _airport = d.airportname;
                                    if ((_airlinecode == data.Departure) && (_airport.IndexOf("All Airports") > -1))
                                    {
                                        var city = d.city;

                                        foreach (var d1 in Utility_resp.Data1)
                                        {
                                            if (d1.city == city)
                                            {
                                                string iata = d1.iata;
                                                ChkDepArray.Add(iata);
                                            }
                                        }
                                    }



                                }
                            }
                            catch (Exception ex)
                            {


                            }

                            if (ChkDepArray.Count == 0)
                            {
                                ChkDepArray.Add(data.Departure);
                            }

                            //Fill Arrival Array List
                            foreach (var d in Utility_resp.Data1)
                            {
                                string a1 = d.airlinecode;
                                string b = d.airportname;
                                if ((a1 == data.Destination) && (b.IndexOf("All Airports") > -1))
                                {
                                    var city = d.city;

                                    foreach (var d1 in Utility_resp.Data1)
                                    {
                                        if (d1.city == city)
                                        {
                                            string iata = d1.iata;
                                            ChkArrivArray.Add(iata);
                                        }
                                    }
                                }
                            }
                            //foreach (var d in Utility_resp.Data1)
                            //{
                            //    string a = d.airlinecode;
                            //    string b = d.airportname;
                            //    if ((a == data.Destination) && (b.IndexOf("All Airports") > -1))
                            //    {
                            //        var city = d.city;

                            //        foreach (var d1 in Utility_resp.Data)
                            //        {
                            //            if (d1.city == city)
                            //            {
                            //                string iata = d1.iata;
                            //                ChkArrivArray.Add(iata);
                            //            }
                            //        }
                            //    }
                            //}
                            if (ChkArrivArray.Count == 0)
                            {
                                ChkArrivArray.Add(data.Destination);
                            }

                            //var LayoverNode = IterativeNode.SelectSingleNode("//table[@id='layover-section']");
                            bool is_layover = false;
                            //Segments Differenciation Logic
                            if ((ChkDepArray.IndexOf(item.DepartureCityCode) == -1) && (ChkArrivArray.IndexOf(item.DepartureCityCode) == -1))
                            {
                                is_layover = true;
                                //Layover
                                //Iterative Layover Section Start//
                                var timeDifflapse = "";
                                var ArrivalTime = "";
                                var DepTime = "";
                                int Minute;
                                int Hour;

                                if (FlitArrivalDateTimeforStopColculation != "")
                                {
                                    ArrivalTime = FlitArrivalDateTimeforStopColculation;
                                    DepTime = item.DepartureDateTime;
                                    TimeSpan duration = DateTime.Parse(DepTime).Subtract(DateTime.Parse(ArrivalTime));
                                    Hour = duration.Hours;
                                    Minute = duration.Minutes;
                                    //timeDifflapse = timeDiff(DepTime, ArrivalTime);
                                    //Hour = (Math.floor(Math.abs(timeDifflapse.minutes) / 60));
                                    //Minute = (Math.abs(timeDifflapse.minutes) % 60);


                                    consumed_minutes += (Hour * 60) + Minute;


                                    //LayoverNode.Attributes["id"].Value = "layover-section" + dvcount.ToString();
                                    IterativeNode.SelectSingleNode("//span[@id='html_layover']").InnerHtml = " Layover: " + Hour + "H " + Minute + "M";


                                    Stop += 1;
                                }


                                //Iterative Layover Section End //
                            }
                            else
                            {

                                try
                                {

                                }
                                catch (Exception ex)
                                {


                                }

                            }
                            bool is_totalduration = false;
                            if (ChkArrivArray.IndexOf(item.DepartureCityCode) > -1)
                            {
                                is_totalduration = true;
                                //Total Duration
                                //Total Trip Section Start//
                                var FlightNumberStop = (Stop == 0) ? "NonStop" : ((Stop == 1) ? "1 Stop" : "2+ Stop");

                                int _vflighthours = consumed_minutes / 60;
                                var _vflightminutes = consumed_minutes % 60;

                                sequence = item.Sequence;
                                consumed_minutes = 0;
                                Stop = 0;

                                //var TripNode = IterativeNode.SelectSingleNode("//table[@id='totaltrip-section']");
                                //TripNode.Attributes["id"].Value = "totaltrip-section" + dvcount.ToString();
                                IterativeNode.SelectSingleNode("//span[@id='html_totaltrip_span1']").InnerHtml = "Total Trip Duration:  " + _vflighthours + "H " + _vflightminutes + "M (" + FlightNumberStop + ")";
                                //Iterative Layover Section End //

                            }




                            //Iterative Dates Section Start // 
                            IterativeNode.SelectSingleNode("//span[@id='html_flights_dep_date']").InnerHtml = FormatDate(item.DepartureDateTime);
                            IterativeNode.SelectSingleNode("//span[@id='html_flight_arrs_date']").InnerHtml = FormatDate(item.ArrivalDateTime1);
                            //Iterative Dates Section End //

                            //Iterative Flights Details Section Start //
                            IterativeNode.SelectSingleNode("//img[@id='flight-logo']").Attributes["src"].Value = "/air/Content/Images/Airlines_Logo/" + item.MarketingAirline + ".gif";

                            //(FD_Value.AirlineName == null ? Airline_Name(FD_Value.MarketingAirline) + "Operated By " + Airline_Name(FD_Value.MarketingAirline) : Airline_Name(FD_Value.MarketingAirline) + "Operated By " + Airline_Name(FD_Value.AirlineName))


                            IterativeNode.SelectSingleNode("//p[@id='marketing-airline']").InnerHtml = (item.AirlineName == null ? str_marketingairline + "Operated By " + str_marketingairline : str_marketingairline + "Operated By " + str_airline);

                            //(FD_Value.AirlineName == null ? FD_Value.MarketingAirline : FD_Value.AirlineName) + "  " + FD_Value.FlightNumber.replace(/^0+/, '') 
                            IterativeNode.SelectSingleNode("//strong[@id='flight-number']").InnerHtml = "Flight #" + " " + item.FlightNumber.Replace("/^0+/", "");

                            IterativeNode.SelectSingleNode("//p[@id='des-airline']").InnerHtml = Dep_Airport;
                            IterativeNode.SelectSingleNode("//p[@id='dep-date']").InnerHtml = FormatDate(item.DepartureDateTime);
                            IterativeNode.SelectSingleNode("//strong[@id='dep-terminal']").InnerHtml = "Terminal: " + (item.DepartureTerminal == null ? "Not Available" : item.DepartureTerminal.Replace("Terminal", ""));
                            IterativeNode.SelectSingleNode("//p[@id='arr-airline']").InnerHtml = Arr_Airport;
                            IterativeNode.SelectSingleNode("//p[@id='arr-date']").InnerHtml = FormatDate(item.ArrivalDateTime1);
                            IterativeNode.SelectSingleNode("//strong[@id='arr-terminal']").InnerHtml = "Terminal: " + (item.ArrivalTerminal == null ? "Not Available" : item.ArrivalTerminal.Replace("Terminal", ""));
                            IterativeNode.SelectSingleNode("//p[@id='aircraft']").InnerHtml = "Aircraft:" + item.AircraftType;

                            string[] vSegmentsElapsedTime = item.ElapsedTime.Split('.');
                            int vflighthours = Convert.ToInt32(vSegmentsElapsedTime[0]);
                            int vflightminutes = Convert.ToInt32(vSegmentsElapsedTime[1]);
                            consumed_minutes += (vflighthours * 60) + (vflightminutes);
                            FlitArrivalDateTimeforStopColculation = item.ArrivalDateTime1;
                            IterativeNode.SelectSingleNode("//p[@id='duration']").InnerHtml = "Duration:" + vflighthours + "H " + vflightminutes + "M";

                            //Iterative Flights Details Section End //

                            // header_passenger_section.SelectSingleNode("//span[@id='html_bookingtime']").InnerHtml = "Monday 05 Mar 2018 4:53 PM";

                            bool is_totalduration1 = false;
                            if ((data.TripType == 1) || (data.TripType == 3))
                            {
                                //OneWay & Multiway
                                if (ChkArrivArray.IndexOf(item.ArrivalCityCode) > -1)
                                {
                                    is_totalduration1 = true;
                                    //Total Duration
                                    //Total Trip Section Start//
                                    var FlightNumberStop = (Stop == 0) ? "NonStop" : ((Stop == 1) ? "1 Stop" : "2+ Stop");

                                    var _vflighthours = consumed_minutes / 60;
                                    var _vflightminutes = consumed_minutes % 60;
                                    sequence = item.Sequence;
                                    consumed_minutes = 0;
                                    //city += 1;

                                    // var TripNode = IterativeNode.SelectSingleNode("//table[@id='totaltrip-section"+ "']");
                                    //TripNode.Attributes["id"].Value = "totaltrip-section" + dvcount.ToString();
                                    IterativeNode.SelectSingleNode("//span[@id='html_totaltrip_span']").InnerHtml = "Total Trip Duration:  " + _vflighthours + "H " + _vflightminutes + " M (" + FlightNumberStop + ")";
                                    //Iterative Layover Section End //

                                }
                            }
                            else if (data.TripType == 2)
                            {
                                if (ChkDepArray.IndexOf(item.ArrivalCityCode) > -1)
                                {
                                    is_totalduration1 = true;
                                    //Total Duration
                                    //Total Trip Section Start//
                                    var FlightNumberStop = (Stop == 0) ? "NonStop" : ((Stop == 1) ? "1 Stop" : "2+ Stop");

                                    var _vflighthours = consumed_minutes / 60;
                                    var _vflightminutes = consumed_minutes % 60;
                                    sequence = item.Sequence;
                                    consumed_minutes = 0;
                                    //var TripNode = IterativeNode.SelectSingleNode("//table[@id='totaltrip-section" + "']");
                                    //TripNode.Attributes["id"].Value = "totaltrip-section" + dvcount.ToString();
                                    IterativeNode.SelectSingleNode("//span[@id='html_totaltrip_span']").InnerHtml = "Total Trip Duration:  " + _vflighthours + "H " + _vflightminutes + "M (" + FlightNumberStop + ")";

                                    //IterativeNode.SelectSingleNode("//span[@id='html_totaltrip_span']").InnerHtml = "Total Trip Duration:  " + _vflighthours + "H " + _vflightminutes + "M (" + FlightNumberStop + ")";
                                    //Iterative Layover Section End //

                                }
                            }
                            if (is_layover == false)
                            {
                                var node = IterativeNode.SelectSingleNode("//table[@id='layover-section']");
                                ////IterativeNode.ChildNodes.Remove(node);
                                foreach (HtmlNode n in node.SelectNodes("*"))
                                {
                                    n.Remove();
                                }
                                var innerText = node.InnerText.Trim();

                                //IterativeNode.ChildNodes.Remove(node);
                            }
                            if (is_totalduration1 == false)
                            {
                                var node = IterativeNode.SelectSingleNode("//table[@id='totaltrip-section']");
                                //IterativeNode.ChildNodes.Remove(node);
                                node.Remove();
                                foreach (HtmlNode n in node.SelectNodes("*"))
                                {
                                    n.Remove();
                                }
                                var innerText = node.InnerText.Trim();

                            }
                            if (is_totalduration == false)
                            {
                                var node1 = IterativeNode.SelectSingleNode("//table[@id='totaltrip-section1']");
                                //IterativeNode.ChildNodes.Remove(node);
                                node1.Remove();
                                foreach (HtmlNode n in node1.SelectNodes("*"))
                                {
                                    n.Remove();
                                }
                                var innerText1 = node1.InnerText.Trim();
                            }
                            //HtmlNode newPara = HtmlNode.CreateNode(IterativeNode.InnerHtml);
                            FlightNode.ChildNodes.Append(IterativeNode);
                            try
                            {
                                FlightNode.ChildNodes.Remove(InnerFlightNodes);
                            }
                            catch (Exception ex)
                            {


                            }

                            //try
                            //{
                            //    //Remove Template Node

                            //}
                            //catch (Exception ex)
                            //{

                            //    throw;
                            //}

                        }


                        //html_totaltrip_span


                        //Passenger detail
                        //HtmlNode IterativeNode = HtmlNode.CreateNode("<div id='html_flight_node'></div>");
                        //IterativeNode.CopyFrom(htmlbody);
                        var passenger_section = htmlDoc.GetElementbyId("html_Confirmation_passengerDetails");
                        var inital_passenger_body = passenger_section.SelectSingleNode("//tbody[@id='html_Confirmation_passengerDetails']");
                        var initial_passenger_tr = inital_passenger_body.SelectSingleNode("//tr[@id='html_tr_passenger']");

                        foreach (var item in resp.passengers_details)
                        {
                            HtmlNode Iterative_passenger_Node = HtmlNode.CreateNode("<tbody id='html_Confirmation_passengerDetails'>");
                            Iterative_passenger_Node.CopyFrom(inital_passenger_body);


                            var name1 = string.Empty;
                            if (!string.IsNullOrEmpty(item.passengername))
                            {
                                var splitName = item.passengername.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (splitName.Length == 3)
                                {
                                    name1 = item.Surname + " " + splitName[1] + " " + splitName[2] + " " + splitName[0];
                                }
                                else if (splitName.Length == 2)
                                {
                                    name1 = item.Surname + " " + splitName[1] + " " + splitName[0];
                                }
                                else
                                {
                                    name1 = (resp.passengers_details[0].passengername + " " + item.Surname);
                                }
                            }



                            Iterative_passenger_Node.SelectSingleNode("//td[@id='html_td_passenger']").InnerHtml = name1;
                            passenger_section.ChildNodes.Append(Iterative_passenger_Node);
                            try
                            {
                                passenger_section.ChildNodes.Remove(initial_passenger_tr);
                            }
                            catch (Exception ex)
                            {


                            }
                        }
                        //Add Passenger Amount  
                        PaymentDetailsNode.SelectSingleNode("//div[@id='html_amtcharged']").InnerHtml = "CAD" + resp.currCode_details != null ? (resp.currCode_details.PaymentAmount == null ? string.Empty : resp.currCode_details.PaymentAmount) : string.Empty;
                        PaymentDetailsNode.SelectSingleNode("//div[@id='html_ccname']").InnerHtml = resp.currCode_details != null ? (resp.currCode_details.NameOnCard == null ? string.Empty : resp.currCode_details.NameOnCard) : string.Empty;
                        PaymentDetailsNode.SelectSingleNode("//span[@id='html_ccnumber']").InnerHtml = resp.currCode_details != null ? (resp.currCode_details.CardNumber == null ? string.Empty : resp.currCode_details.CardNumber.Substring(12, 4)) : string.Empty;
                        //html_ccnumber ;
                        if (Convert.ToDouble(resp.currCode_details.CCFee) > 0)
                        {

                            PaymentDetailsNode.SelectSingleNode("//div[@id='html_ccfee']").InnerHtml = resp.currCode_details != null ? (resp.currCode_details.CCFee == null ? string.Empty : resp.currCode_details.CCFee) : string.Empty;

                        }
                        else
                        {
                            var paymentdetails_section = htmlDoc.GetElementbyId("section_paymentdetails");
                            var ccfee_node = passenger_section.SelectSingleNode("//div[@id='ccfee-node']");
                            paymentdetails_section.ChildNodes.Remove(ccfee_node);
                        }
                        if (resp.currCode_details.ServiceFee != null && Convert.ToDouble(resp.currCode_details.ServiceFee) > 0)
                        {

                            PaymentDetailsNode.SelectSingleNode("//div[@id='html_servicefee']").InnerHtml = resp.currCode_details.ServiceFee;
                        }
                        else
                        {
                            var paymentdetails_section = htmlDoc.GetElementbyId("section_paymentdetails");
                            var servicefee_node = passenger_section.SelectSingleNode("//div[@id='servicefee-node']");
                            paymentdetails_section.ChildNodes.Remove(servicefee_node);

                        }


                        //Add Billing Details For CreditCard
                        PaymentDetailsNode.SelectSingleNode("//div[@id='html_billingdetails']").InnerHtml = resp.currCode_details.NameOnCard + " " + "<br>" + resp.currCode_details.Address + "<br>" + resp.currCode_details.BillingEmail + "<br>" + resp.currCode_details.BillingPhone;

                    }
                    catch (Exception ex)
                    {

                        string error_message = ex.Message.ToString();
                    }

                }
                //var result1 = response;
                //var result = JsonConvert.DeserializeObject<Common.CommonUtility>(response.Result.ToString());
                //return response;
                ViewBag.Response = htmlDoc.DocumentNode.InnerHtml;
                return View();
            }

            //return View();
        }


    }
    public class VMBookingDetails
    {
        public int Id { get; set; }
        public int TripType { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public int CompanyId { get; set; }
        public string ReservationDate { get; set; }
        public string AgencyStreetAddress { get; set; }
        public string AgencyName { get; set; }
        public string CityName { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PCC { get; set; }
        public string PNR { get; set; }
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
        public string BookingDate { get; set; }
        public string RequestID { get; set; }
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
        public string ServiceFee { get; set; }
        public string Province { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }

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
}