using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Model
{
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

        public string DefaultCompanyId { get; set; }
        public string PaymentMethod { get; set; }
    }


    public class SuppierDetail
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string PCC { get; set; }

    }

}
