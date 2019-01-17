using register_functionlity.DB.Data;
using register_functionlity.DB.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Service
{
    public class BookingsService
    {
        public List<BookingsModel> GetBookings(long Userid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AirDb"].ToString();
            //string connectionString = @"Data Source=192.168.1.6;Initial Catalog=airDB;User ID=heemanshu;Password=$eP@1717";
            BookingsModel Booking = new BookingsModel();
            List<BookingsModel> Bookings = new List<BookingsModel>();
            using (SqlConnection connection =
              new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("select Locator,Departure,Destination,ReservationDate,DepartureDate,BillingEmail,BillingPhone,Id from MyBookings where UserId=" + Userid + "  order by ReservationDate desc", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        Booking = new BookingsModel();
                        //{
                        Booking.Locator = reader[0].ToString();
                        Booking.Departure = reader[1].ToString();
                        Booking.Destination = reader[2].ToString();
                        Booking.ReservationDate = Convert.ToDateTime(reader[3].ToString());
                        var depDate = reader[4].ToString();
                        if (!String.IsNullOrEmpty(depDate) && depDate.Contains(";"))
                        {
                            var splitArr = depDate.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            depDate = splitArr.Length > 0 ? splitArr[0] : depDate;
                        }
                        Booking.DepartureDate = Convert.ToDateTime(depDate);
                        Booking.BillingEmail = reader[5].ToString();
                        Booking.BillingPhone = reader[6].ToString();
                        Booking.Id = Convert.ToInt64(reader[7]);
                        //};
                        Bookings.Add(Booking);
                    }
                    reader.Close();
                    //return Bookings.Count.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                    //return ex.Message;
                }
                return Bookings;
            }
            //using (var context = new AirAdminDBEntities())
            //{
            //    var entityList = context.BookingReports.Join(context.CompanyDetails, b => b.DefaultCompanyId, co => co.Id, (b, co) =>
            //               new BookingsModel
            //               {
            //                   Amount = b.Amount,
            //                   DefaultCompanyId = b.DefaultCompanyId,
            //                   Departure = b.Departure,
            //                   DepartureDate = b.DepartureDate,
            //                   Destination = b.Destination,
            //                   Locator = b.Locator,
            //                   ReservationDate = b.ReservationDate,
            //                   Title = b.Title,
            //                   Id = b.Id,
            //                   ParentId = co.ParentId == null ? 0 : co.ParentId.Value,
            //                   BillingEmail = b.BillingEmail,
            //                   BillingPhone = b.BillingPhone
            //               });


            //    var list = entityList.Where(k => k.ParentId == companyId).ToList();

            //    return list;

            //}
        }

        public BookingsModel GetBookingDetail(long bookingId)
        {
            using (var context = new AirAdminDBEntities())
            {
                var user = context.BookingReports.Where(m => m.Id == bookingId).FirstOrDefault();
                var model = new BookingsModel();
                if (user != null)
                {
                    model.Id = user.Id;
                    model.Locator = user.Locator;
                    model.Destination = user.Destination;
                    model.Departure = user.Departure;
                    model.ReservationDate = user.ReservationDate;
                    model.DepartureDate = user.DepartureDate;
                    model.Amount = user.Amount;
                    model.BillingEmail = model.BillingEmail;
                    model.BillingPhone = model.BillingPhone;
                }
                return model;
            }
        }

        public VMBookingDetails GetBookingDetailsByID(int bookingid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AirDb"].ToString();
            //string connectionString = @"Data Source=192.168.1.6;Initial Catalog=AirDB;User ID=heemanshu;Password=$eP@1717";
            VMBookingDetails Booking = new VMBookingDetails();

            using (SqlConnection connection =
              new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("select * from VwGetBookingDetailsByID where id=" + bookingid, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        Booking = new VMBookingDetails
                        {
                            Id = Convert.ToInt32(reader[0].ToString()),
                            TripType = Convert.ToInt32(reader[1].ToString()),
                            Departure = reader[2].ToString(),
                            Destination = reader[3].ToString(),
                            DepartureDate = reader[4].ToString(),
                            ReturnDate = reader[5].ToString(),
                            CompanyId = Convert.ToInt32(reader[6].ToString()),
                            ReservationDate = reader[7].ToString(),
                            AgencyStreetAddress = reader[8].ToString(),
                            AgencyName = reader[9].ToString(),
                            country = reader[11].ToString(),
                            state = reader[12].ToString(),
                            PostalCode = reader[13].ToString(),
                            PhoneNumber = reader[14].ToString(),
                            PCC = reader[15].ToString(),
                            PNR = reader[16].ToString(),
                            DefaultCompanyId = reader[17].ToString(),
                            PaymentMethod= reader[18].ToString()
                        };

                    }
                    reader.Close();
                    //return Bookings.Count.ToString();
                }
                catch (Exception ex)
                {
                    //return ex.Message;
                }
                return Booking;
            }
        }

        public SuppierDetail GetSupplierDetails(string defaultCompanyId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CommonDb"].ToString();
            //string connectionString = @"Data Source=192.168.1.6;Initial Catalog=AirDB;User ID=heemanshu;Password=$eP@1717";
            SuppierDetail supplier = new SuppierDetail();

            using (SqlConnection connection =
              new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("dbo.USP_GEtSpplierbyCompanyId", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@defaultcompanyid", defaultCompanyId));
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        supplier = new SuppierDetail
                        {
                            UserName = reader[0].ToString(),
                            Password = reader[1].ToString(),
                            PCC = reader[2].ToString()
                        };

                    }
                    reader.Close();
                    //return Bookings.Count.ToString();
                }
                catch (Exception ex)
                {
                    //return ex.Message;
                }
                return supplier;
            }
        }

    }
}
