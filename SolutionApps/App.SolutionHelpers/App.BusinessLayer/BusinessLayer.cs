using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DataLayer;
using App.Models;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace App.BusinessLayer
{
    public class BusinessLayer
    {
        public BusinessLayer()
        {
        }
        ~BusinessLayer()
        {
        }


        #region MultiModelsList
        public App.Common.CommonUtility GetMultiModelInformation(App.Models.MultiModelsList ReqInfo)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                Utility = new App.DataLayer.DataLayer().GetMultiModelInformation(ReqInfo);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.GetMultiModelInformation", ex);
            }
            return Utility;
        }
        #endregion MultiModelsList

        //#region Save Booking In Database
        //public async Task SaveBooking(VMSaveSearch model)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["SQLSource0"].ConnectionString;
        //    await Task.Run(() =>
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("insert into BookingReports values(@Title,@Locator,@ReservationDate,@DepartureDate,@Departure,@Destination,@Amount,@DefaultCompanyId,@BillingEmail,@BillingPhone,@UserId)", connection);
        //            cmd.Parameters.AddWithValue("@Title", model.Title);
        //            cmd.Parameters.AddWithValue("@Locator", model.Locator);
        //            cmd.Parameters.AddWithValue("@UserId", model.UserId);
        //            cmd.Parameters.AddWithValue("@Destination", model.Destination);
        //            cmd.Parameters.AddWithValue("@DepartureDate", model.DepartureDate);
        //            cmd.Parameters.AddWithValue("@Departure", model.Departure);
        //            cmd.Parameters.AddWithValue("@DefaultCompanyId", model.DefaultCompanyId);
        //            cmd.Parameters.AddWithValue("@BillingPhone", model.BillingPhone);
        //            cmd.Parameters.AddWithValue("@BillingEmail", model.BillingEmail);
        //            cmd.Parameters.AddWithValue("@Amount", model.Amount);
        //            cmd.Parameters.AddWithValue("@ReservationDate", model.ReservationDate);
        //            cmd.ExecuteNonQuery();
        //        }
        //    });
        //}
        //#endregion

        public MarkUp GetMarkupByDomain(string domain)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSource0"].ConnectionString;
            MarkUp markup = new MarkUp();
            // Provide the query string with a parameter placeholder.
            string queryString = "select m.PublishedFareMarkup,m.NetfareMarkup from CommonDB.dbo.CompanyDetails cd inner join dbo.Markups m on cd.Id = m.CompanyID  where WebsiteURL='" + domain + "'";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        markup = new MarkUp
                        {
                            PublishedFareMarkup = reader[0].ToString(),
                            NetfareMarkup = reader[1].ToString()
                        };
                    }
                    reader.Close();
                }
                catch
                {
                }
                return markup;
            }

        }

        public async Task TrackSearchRecords(App.Models.SabreModels.SabreReqstResourceModels ReqstResource_BFMXRQ_RequestContent, string RequestID)
        {
            await Task.Run(() =>
            {

                string connectionString = ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
                string queryString = "insert into RequestAir values(@RequestID,@Departure,@Destination,@DepartureDate,@ReturnDate,@Airline,@Class,@Adults,@Children,@Infants,@DirectFlights,@TripType,@CompanyId)";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    try
                    {
                        connection.Open();
                        //str.Substring(str.IndexOf("(")+1, str.IndexOf(")")-str.IndexOf("(")-1)
                        string origin = ReqstResource_BFMXRQ_RequestContent.origin;
                        string destination = ReqstResource_BFMXRQ_RequestContent.destination;
                        command.Parameters.AddWithValue("@RequestID", RequestID);

                        if (ReqstResource_BFMXRQ_RequestContent.SelectionName == "3")
                        {
                            if (!string.IsNullOrEmpty(origin))
                            {
                                var originArr = origin.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                if (originArr.Length > 0)
                                {
                                    string strOrigin = string.Empty;
                                    foreach (var item in originArr)
                                    {
                                        strOrigin += item.Substring(item.IndexOf("[") + 1, item.IndexOf("]") - item.IndexOf("[") - 1) + "-";
                                    }
                                    command.Parameters.AddWithValue("@Departure", strOrigin);
                                }
                            }

                            if (!string.IsNullOrEmpty(destination))
                            {
                                var destinationArr = destination.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                if (destinationArr.Length > 0)
                                {
                                    string strDestination = string.Empty;
                                    foreach (var item in destinationArr)
                                    {
                                        strDestination += item.Substring(item.IndexOf("[") + 1, item.IndexOf("]") - item.IndexOf("[") - 1) + "-";
                                    }
                                    command.Parameters.AddWithValue("@Destination", strDestination);
                                }
                            }


                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Departure", origin.Substring(origin.IndexOf("[") + 1, origin.IndexOf("]") - origin.IndexOf("[") - 1));
                            command.Parameters.AddWithValue("@Destination", destination.Substring(destination.IndexOf("[") + 1, destination.IndexOf("]") - destination.IndexOf("[") - 1));
                        }


                        command.Parameters.AddWithValue("@DepartureDate", ReqstResource_BFMXRQ_RequestContent.departuredate);
                        //if(ReqstResource_BFMXRQ_RequestContent.returndate != "")
                        //{
                        //    command.Parameters.AddWithValue("@ReturnDate", ReqstResource_BFMXRQ_RequestContent.returndate);
                        //}
                        //else
                        //{
                        //    command.Parameters.Add("@ReturnDate", SqlDbType.DateTime).Value = DBNull.Value;
                        //    //command.Parameters.AddWithValue("@ReturnDate",to);
                        //}

                        command.Parameters.AddWithValue("@ReturnDate", ReqstResource_BFMXRQ_RequestContent.returndate == "" ? "" : ReqstResource_BFMXRQ_RequestContent.returndate);
                        command.Parameters.AddWithValue("@Airline", ReqstResource_BFMXRQ_RequestContent.Airline == null ? "All" : ReqstResource_BFMXRQ_RequestContent.Airline);
                        command.Parameters.AddWithValue("@Class", ReqstResource_BFMXRQ_RequestContent.AirClass);
                        command.Parameters.AddWithValue("@Adults", ReqstResource_BFMXRQ_RequestContent.noOfAdults);
                        command.Parameters.AddWithValue("@Children", ReqstResource_BFMXRQ_RequestContent.noOfChildrens);
                        command.Parameters.AddWithValue("@Infants", ReqstResource_BFMXRQ_RequestContent.noOfInfants);
                        command.Parameters.AddWithValue("@DirectFlights", ReqstResource_BFMXRQ_RequestContent.IsDirectFlight);
                        command.Parameters.AddWithValue("@TripType", ReqstResource_BFMXRQ_RequestContent.SelectionName);
                        command.Parameters.AddWithValue("@CompanyId", ReqstResource_BFMXRQ_RequestContent.CompanyTypeId);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            });
        }

        public async void TrackReservationRecords(TrackBookingRecords modal)
        {
            await Task.Run(() =>
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
                string queryString = "insert into BookingReports values(@RequestID,@Locator,@UserId,@DefaultCompanyId,@BillingEmail,@BillingPhone,@BillingName,@PaymentMethod,@ReservationDate,@PCC,@Amount)";

                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@RequestID", modal.RequestID);
                        command.Parameters.AddWithValue("@Locator", string.IsNullOrEmpty(modal.Locator) ? "" : modal.Locator);
                        command.Parameters.AddWithValue("@UserId", modal.UserId);
                        command.Parameters.AddWithValue("@DefaultCompanyId", modal.DefaultCompanyId);
                        command.Parameters.AddWithValue("@BillingEmail", modal.BillingEmail);
                        command.Parameters.AddWithValue("@BillingPhone", modal.BillingPhone);
                        command.Parameters.AddWithValue("@BillingName", modal.BillingName);
                        command.Parameters.AddWithValue("@PaymentMethod", modal.PaymentMethod);
                        command.Parameters.AddWithValue("@ReservationDate", DateTime.Now);
                        command.Parameters.AddWithValue("@PCC", "");
                        command.Parameters.AddWithValue("@Amount", modal.Amount);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            });

        }

        public string GetBlockedAirlines(string domain)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSource0"].ConnectionString;
            string blockedAirline = string.Empty;
            // Provide the query string with a parameter placeholder.
            string queryString = "select blockedAirline.IATACode from CommonDB.dbo.CompanyDetails cd inner join CommonDB.dbo.AirlineRestrictionAir blockedAirline on cd.Id = blockedAirline.DefaultCompanyId  where WebsiteURL='" + domain + "'";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        blockedAirline += reader[0].ToString() + ",";
                    }

                    if (!string.IsNullOrEmpty(blockedAirline))
                    {
                        blockedAirline = String.Join(",",
                                            blockedAirline.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                );
                    }

                    reader.Close();
                }
                catch
                {
                }
                return blockedAirline;
            }

        }


        public RequestAir GetRequestById(string requestId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
            RequestAir request = new RequestAir();
            // Provide the query string with a parameter placeholder.
            string queryString = "SELECT [Id],[RequestID],[Departure],[Destination],[DepartureDate],[ReturnDate],[Airline],[Class],[Adults],[Children],[Infants],[DirectFlights],[TripType],[CompanyId] FROM [QA_AirDB].[dbo].[RequestAir]  where requestid='" + requestId + "'";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        request.Id= reader[0] as int? ?? default(int);
                        request.RequestID = reader[1] as string;
                        request.Departure = reader[2] as string;
                        request.Destination = reader[3] as string;
                        request.DepartureDate= reader[4] as string;
                    } 

                    reader.Close();
                }
                catch
                {
                }
                return request;
            }

        }

        public string GetMarkUpServiceCommonService()
        {

            String line = "";
            try
            {

                line = new App.DataLayer.DataLayer().GetMarkUpService();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.GetCommonService", ex);
            }
            return line;
        }
        public string GetAirlineRestrictionAirService()
        {

            String line = "";
            try
            {

                line = new App.DataLayer.DataLayer().GetAirlineRestrictionAirService();

            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.GetCommonService", ex);
            }
            return line;
        }
        public string GetQuoteSONFile(string folderName, string SearchText)
        {
            //GetMarkUpServiceCommonService();
            //GetAirlineRestrictionAirService();
            var resp = new HttpResponseMessage();
            String line = "";
            string path = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\ApiLogFiles\UserLogFiles\");
            HttpRequestMessage req = new HttpRequestMessage();
            string finalPath = path + @"\" + folderName;
            if (Directory.Exists(finalPath))
            {
                try
                {   // Open the text file using a stream reader.
                    string fileName = finalPath + @"\" + SearchText;
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        // Read the stream to a string, and write the string to the console.
                        line = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

            return line;
        }

        public string GetAirlineCommonService(string Code, string SearchType)
        {
            string Utility = "";
            try
            {
                if (!string.IsNullOrEmpty(Code))
                {
                    Utility = new App.DataLayer.DataLayer().GetAirport_AirlineService(Code, SearchType);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.GetCommonService", ex);
            }
            return Utility;
        }
        public App.Common.CommonUtility GetCommonService(string CommonServiceType, string SearchText)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                if (!string.IsNullOrEmpty(CommonServiceType))
                {
                    Utility = new App.DataLayer.DataLayer().GetCommonService(CommonServiceType, SearchText);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.GetCommonService", ex);
            }
            return Utility;
        }
        public App.Common.CommonUtility GetAdventureWorksProductionProduct(App.Models.ProductionProduct Product_ID)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                if (!string.IsNullOrEmpty(Product_ID.ProductID))
                {
                    Utility = new App.DataLayer.DataLayer().GetAdventureWorksProductionProduct(Product_ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.GetAdventureWorksProductionProduct", ex);
            }
            return Utility;
        }
        public App.Common.CommonUtility SetAirPortData(List<App.Models.AirportData> AirPort_Product)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                if (AirPort_Product.Count > 0)
                {
                    Utility = new App.DataLayer.DataLayer().SetAirPortData(AirPort_Product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.AuthenticateUser", ex);
            }
            return Utility;
        }

        #region Application Test Methods AsynchronousService
        public App.Common.CommonUtility GetData(App.Models.LoginInformation Login_Information)
        {
            try
            {
                return new App.DataLayer.DataLayer().GetData(Login_Information);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : GetData ", ex); }
        }
        public App.Common.CommonUtility PostData(App.Models.LoginInformation Login_Information)
        {
            try
            {
                return new App.DataLayer.DataLayer().PostData(Login_Information);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : PostData ", ex); }
        }
        public App.Common.CommonUtility PutData(App.Models.LoginInformation Login_Information)
        {
            try
            {
                return new App.DataLayer.DataLayer().PutData(Login_Information);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : PutData ", ex); }
        }
        public App.Common.CommonUtility DeleteData(App.Models.LoginInformation Login_Information)
        {
            try
            {
                return new App.DataLayer.DataLayer().DeleteData(Login_Information);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : DeleteData ", ex); }
        }
        #endregion Application Test Methods AsynchronousService


    }

    public class MarkUp
    {
        public string PublishedFareMarkup { get; set; }
        public string NetfareMarkup { get; set; }
    }
    //public class VMSaveSearch
    //{
    //    public string Title { get; set; }
    //    public string Locator { get; set; }
    //    public DateTime ReservationDate { get; set; }
    //    public DateTime DepartureDate { get; set; }
    //    public string Departure { get; set; }
    //    public string Destination { get; set; }
    //    public float Amount { get; set; }
    //    public int DefaultCompanyId { get; set; }
    //    public string BillingEmail { get; set; }
    //    public string BillingPhone { get; set; }
    //    public Int64 UserId { get; set; }
    //}
    public class TrackBookingRecords
    {
        public string RequestID { get; set; }
        public string Locator { get; set; }
        public string UserId { get; set; }
        public string DefaultCompanyId { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }
        public string BillingName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public int selectionkey { get; set; }
    }

    public class RequestAir
    {
        public int Id { get; set; }
        public string RequestID { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public string Airline { get; set; }
        public string Class { get; set; }
        public string Adults { get; set; }
        public string Children { get; set; }
        public string Infants { get; set; }
        public string DirectFlights { get; set; }
        public string TripType { get; set; }
        public string CompanyId { get; set; }
    }

}
