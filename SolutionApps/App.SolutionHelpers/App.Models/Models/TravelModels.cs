using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App.Models
{
    public class TravelModels
    {
        public TravelModels()
        {

        }
        public class ServiceProviderInfo
        {
            public ServiceProviderInfo()
            {

            }
            public int SPID { get; set; }
            public int SFID { get; set; }
            public string ServiceAPIType { get; set; }
            public string ServiceFOR { get; set; }
            public string ActionType { get; set; }

        }
        public class APPServiceProviderData
        {
            public APPServiceProviderData()
            {

            }
            public int SPID { get; set; }
            public int SFID { get; set; }
            public string ServiceProviderName { get; set; }
            public string ServiceProviderURL { get; set; }
            public string ServiceAPIType { get; set; }
            public string ServiceFOR { get; set; }
            public string FunctionGroup { get; set; }
            public string FunctionName { get; set; }
            public string FunctionURL { get; set; }
        }
        public class APPServiceProvider
        {
            public APPServiceProvider()
            {

            }
            public int SPID { get; set; }
            public string ServiceProviderName { get; set; }
            public string ServiceProviderURL { get; set; }
            public string CreatDT { get; set; }
            public string UpdateDT { get; set; }

        }
        public class APPServiceProviderService
        {
            public APPServiceProviderService()
            {

            }
            public int SPSID { get; set; }
            public int SPID { get; set; }
            public string ServiceAPIType { get; set; }
            public string ServiceFOR { get; set; }
            public string CreatDT { get; set; }
            public string UpdateDT { get; set; }
        }
        public class APPServiceFunctions
        {
            public APPServiceFunctions()
            {

            }
            public int SFID { get; set; }
            public int SPSID { get; set; }
            public string FunctionGroup { get; set; }
            public string FunctionName { get; set; }
            public string FunctionURL { get; set; }
            public string CreatDT { get; set; }
            public string UpdateDT { get; set; }
        }
        public class APPServiceParamers
        {
            public APPServiceParamers()
            {

            }
            public int ID { get; set; }
            public int SFID { get; set; }
            public string ParameterName { get; set; }
            public string ParameterType { get; set; }
            public string ParameterDesc { get; set; }
            public string CreatDT { get; set; }
            public string UpdateDT { get; set; }
        }
    }

    namespace SabreModels
    {

        public class SabreReqstResourceModels
        {
            public SabreReqstResourceModels()
            {
            }
            public string origin { get; set; }
            public string destination { get; set; }
            public string departuredate { get; set; }
            public string SelectionName { get; set; }
            public string Airline { get; set; }
            public string AirClass { get; set; }
            public string noOfAdults { get; set; }
            public string noOfChildrens { get; set; }
            public string noOfInfants { get; set; }
           
            public string returndate { get; set; }
            public string onlineitinerariesonly { get; set; }
            public string limit { get; set; }
            public string offset { get; set; }
            public string eticketsonly { get; set; }
            public string sortby { get; set; }
            public string order { get; set; }
            public string sortby2 { get; set; }
            public string order2 { get; set; }
            public string pointofsalecountry { get; set; }
            public string passengercount { get; set; }
            public string lengthofstay { get; set; }
            public string minfare { get; set; }
            public string maxfare { get; set; }
            public string PCC { get; set; }
            public string ClientID { get; set; }
            public string ClientSecret { get; set; }
            public int CompanyTypeId { get; set; }
            public string BFMXRQ_RequestContent { get; set; }
            public string IsAuthenticated { get; set; }
            public string IsDirectFlight { get; set; }
        }
        public class BookingFlightPassengerDetails
        {
            public List<AirFlightDetails> Air_Flights_Details { get; set; }
            public List<AirPassengerDetails> Air_Passengers_Details { get; set; }
            public List<AirOtherDetails> Air_Others_Details { get; set; }
        }
        public class FlightPassengerDetails
        {
            public List<PassengerDetails> Passenger_Details { get; set; }
            public List<FlightDetails> Flight_Details { get; set; }
            public List<AirFlightDetails> Air_Flights_Details { get; set; }
            public List<AirPassengerDetails> Air_Passengers_Details { get; set; }
            public List<AirOtherDetails> Air_Others_Details { get; set; }
        }

        public class PassengerDetails
        {
            public string passengername { get; set; }
            public string passengerAddress { get; set; }
            public string ReservationCode { get; set; }
            public string airlineName { get; set; }
            public string email { get; set; }
            public string DepartureCityCode { get; set; }
            public string DepartureTime { get; set; }
            public string DepartureTerminal { get; set; }
        }
        public class FlightDetails
        {
            public string DepartureDate { get; set; }
            public string ArrivalDate { get; set; }
            public string AirlineName { get; set; }
            public string FlightNumber { get; set; }
            public string DepartureCityCode { get; set; }
            public string DepartureTime { get; set; }
            public string DepartureTerminal { get; set; }
            public string ArrivalCityCode { get; set; }
            public string ArrivalTime { get; set; }
            public string ArrivalTerminal { get; set; }
            public string DistanceTravel { get; set; }
            public string AircraftType { get; set; }
        }
        public class AirFlightDetails
        {
            public AirFlightDetails()
            {

            }
            public string DepartureDateTime { get; set; }
            public string ArrivalDateTime { get; set; }
            public string FlightNumber { get; set; }
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
        }

        public class AirPassengerDetails
        {
            public AirPassengerDetails()
            {

            }

            public string PhoneLocationCode { get; set; }
            public string PassengerNameNumber { get; set; }
            public string PhoneNumber { get; set; }
            public string PhoneUseType { get; set; }
            public string PassengerEmail { get; set; }
            public string PassengerNameRef { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string PassengerType { get; set; }
        }
        public class AirNoofPassengers
        {
            public string PassengerCode { get; set; }
            public int NoOfPassengers { get; set; }
        }

        public class AirOtherDetails
        {
            public AirOtherDetails()
            {

            }
            #region SabreSearchMetaData
            public string sbrAirCartSelector { get; set; }
            public string sbrPricingInfoSequenceNumber { get; set; }
            public string sbrAirLineCode { get; set; }
            public string sbrAirLineNumber { get; set; }
            public string sbrAirLineName { get; set; }
            public string sbrDepart { get; set; }
            public string sbrArrive { get; set; }
            public string sbrDepartTime { get; set; }
            public string sbrArriveTime { get; set; }
            public string sbrDuration { get; set; }
            public string sbrCurrencyCode { get; set; }
            public string sbrTotalFare { get; set; }
            public string sbrDepartureAirport { get; set; }
            public string sbrArrivalAirport { get; set; }
            public string sbrResBookDesigCode { get; set; }
            public string sbrAirEquipType { get; set; }
            public string sbrMarketingAirline { get; set; }
            public string sbreTicket { get; set; }

            #endregion SabreSearchMetaData




            #region FlightBookingEnhanceBookRQ
            public string token { get; set; }
            public string dtDepartureTime { get; set; }
            public string dtArrivalTime { get; set; }
            public string flightNumber { get; set; }
            public string destlocationCode { get; set; }
            public string AirEquipType { get; set; }
            public string originLocationCode { get; set; }
            public string resBookDesigCode { get; set; }
            public string status { get; set; }
            public string code { get; set; }
            public string retain { get; set; }
            string p_code { get; set; }
            public string quantity { get; set; }

            #endregion FlightBookingEnhanceBookRQ


            #region FlightBookingAddTravelItinerayRequest
            public string pcc { get; set; }
            public string AgencyAddressLine { get; set; }
            public string AgencyCityName { get; set; }
            public string AgencyCountryCode { get; set; }
            public string AgencyPostalCode { get; set; }
            public string AgencyStateCode { get; set; }
            public string AgencyStreetNumber { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string Phone1LocationCode { get; set; }
            public string PNameNumber { get; set; }
            public string Phone1 { get; set; }
            public string Phone1UseType { get; set; }
            public string PType { get; set; }
            public string NameRef { get; set; }
            public string PassengerEmail { get; set; }
            public string SText { get; set; }
            public string EMailType { get; set; }
            public string AgencyTicketTime { get; set; }
            public string AgencyTicketType { get; set; }
            public string AgencyQueueNo { get; set; }

            #endregion FlightBookingAddTravelItinerayRequest



            #region FlightBookingAddPassengerRequest

            public string isfalse { get; set; }
            public string ignoreAfter { get; set; }
            public string redispResv { get; set; }
            public string usCC { get; set; }
            public string issueCountry { get; set; }
            public string nationality { get; set; }
            public string id { get; set; }
            public string segmentNo { get; set; }
            public string docExpDate { get; set; }
            public string docNumber { get; set; }
            public string docType { get; set; }
            public string p_dob { get; set; }
            public string p_gender { get; set; }
            public string middleName { get; set; }
            public string pref { get; set; }
            public string segment { get; set; }
            public string segNumber { get; set; }

            #endregion FlightBookingAddPassengerRequest


            #region FlightBookingCheckCCVerificationRequest

            public string airlineCode { get; set; }
            public string ccNumber { get; set; }
            public string expDate { get; set; }
            public string amount { get; set; }
            public string currCode { get; set; }

            #endregion FlightBookingCheckCCVerificationRequest

        }
        public class SabreFlightBookingPayentRQ
        {
            public SabreFlightBookingPayentRQ()
            {
            }
            #region SabreSearchMetaData
            public string sbrAirCartSelector { get; set; }
            public string sbrPricingInfoSequenceNumber { get; set; }
            public string sbrAirLineCode { get; set; }
            public string sbrAirLineNumber { get; set; }
            public string sbrAirLineName { get; set; }
            public string sbrDepart { get; set; }
            public string sbrArrive { get; set; }
            public string sbrDepartTime { get; set; }
            public string sbrArriveTime { get; set; }
            public string sbrDuration { get; set; }
            public string sbrCurrencyCode { get; set; }
            public string sbrTotalFare { get; set; }
            public string sbrDepartureAirport { get; set; }
            public string sbrArrivalAirport { get; set; }
            public string sbrResBookDesigCode { get; set; }
            public string sbrAirEquipType { get; set; }
            public string sbrMarketingAirline { get; set; }
            public string sbreTicket { get; set; }

            #endregion SabreSearchMetaData




            #region FlightBookingEnhanceBookRQ
            public string token { get; set; }
            public string dtDepartureTime { get; set; }
            public string dtArrivalTime { get; set; }
            public string flightNumber { get; set; }
            public string destlocationCode { get; set; }
            public string AirEquipType { get; set; }
            public string originLocationCode { get; set; }
            public string resBookDesigCode { get; set; }
            public string status { get; set; }
            public string code { get; set; }
            public string retain { get; set; }
            string p_code { get; set; }
            public string quantity { get; set; }

            #endregion FlightBookingEnhanceBookRQ


            #region FlightBookingAddTravelItinerayRequest
            public string pcc { get; set; }
            public string AgencyAddressLine { get; set; }
            public string AgencyCityName { get; set; }
            public string AgencyCountryCode { get; set; }
            public string AgencyPostalCode { get; set; }
            public string AgencyStateCode { get; set; }
            public string AgencyStreetNumber { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string Phone1LocationCode { get; set; }
            public string PNameNumber { get; set; }
            public string Phone1 { get; set; }
            public string Phone1UseType { get; set; }
            public string PType { get; set; }
            public string NameRef { get; set; }
            public string PassengerEmail { get; set; }
            public string SText { get; set; }
            public string EMailType { get; set; }
            public string AgencyTicketTime { get; set; }
            public string AgencyTicketType { get; set; }
            public string AgencyQueueNo { get; set; }

            #endregion FlightBookingAddTravelItinerayRequest



            #region FlightBookingAddPassengerRequest

            public string isfalse { get; set; }
            public string ignoreAfter { get; set; }
            public string redispResv { get; set; }
            public string usCC { get; set; }
            public string issueCountry { get; set; }
            public string nationality { get; set; }
            public string id { get; set; }
            public string segmentNo { get; set; }
            public string docExpDate { get; set; }
            public string docNumber { get; set; }
            public string docType { get; set; }
            public string p_dob { get; set; }
            public string p_gender { get; set; }
            public string middleName { get; set; }
            public string pref { get; set; }
            public string segment { get; set; }
            public string segNumber { get; set; }

            #endregion FlightBookingAddPassengerRequest


            #region FlightBookingCheckCCVerificationRequest

            public string airlineCode { get; set; }
            public string ccNumber { get; set; }
            public string expDate { get; set; }
            public string amount { get; set; }
            public string currCode { get; set; }

            #endregion FlightBookingCheckCCVerificationRequest

        }
      
        public class SabreFlightBookingEnhanceBookRQ
        {
            string token { get; set; }
            string dtDepartureTime { get; set; }
            string dtArrivalTime { get; set; }
            string flightNumber { get; set; }
            string destlocationCode { get; set; }
            string AirEquipType { get; set; }
            string originLocationCode { get; set; }
            string resBookDesigCode { get; set; }
            string status { get; set; }
            string code { get; set; }
            string retain { get; set; }
            string p_code { get; set; }
            string quantity { get; set; }
        }
        public class SabreFlightBookingAddTravelItinerayRequest
        {
            string pcc { get; set; }
            string token { get; set; }
            string AgencyAddressLine { get; set; }
            string AgencyCityName { get; set; }
            string AgencyCountryCode { get; set; }
            string AgencyPostalCode { get; set; }
            string AgencyStateCode { get; set; }
            string AgencyStreetNumber { get; set; }
            string GivenName { get; set; }
            string Surname { get; set; }
            string Phone1LocationCode { get; set; }
            string PNameNumber { get; set; }
            string Phone1 { get; set; }
            string Phone1UseType { get; set; }
            string PType { get; set; }
            string NameRef { get; set; }
            string PassengerEmail { get; set; }
            string SText { get; set; }
            string EMailType { get; set; }
            string AgencyTicketTime { get; set; }
            string AgencyTicketType { get; set; }
            string AgencyQueueNo { get; set; }
        }
        public class SabreFlightBookingAddPassengerRequest
        {
            string token { get; set; }
            string isfalse { get; set; }
            string ignoreAfter { get; set; }
            string redispResv { get; set; }
            string usCC { get; set; }
            string issueCountry { get; set; }
            string nationality { get; set; }
            string id { get; set; }
            string segmentNo { get; set; }
            string docExpDate { get; set; }
            string docNumber { get; set; }
            string docType { get; set; }
            string p_dob { get; set; }
            string p_gender { get; set; }
            string AgencyAddressLine { get; set; }
            string AgencyCityName { get; set; }
            string AgencyCountryCode { get; set; }
            string AgencyPostalCode { get; set; }
            string AgencyStateCode { get; set; }
            string AgencyStreetNumber { get; set; }
            string GivenName { get; set; }
            string middleName { get; set; }
            string Surname { get; set; }
            string PNameNumber { get; set; }
            string Phone1LocationCode { get; set; }
            string Phone1 { get; set; }
            string Phone1UseType { get; set; }
            string PType { get; set; }
            string PassengerEmail { get; set; }
            string SText { get; set; }
            string EMailType { get; set; }
            string AgencyTicketTime { get; set; }
            string AgencyTicketType { get; set; }
            string AgencyQueueNo { get; set; }
            string pref { get; set; }
            string segment { get; set; }
            string segNumber { get; set; }
        }
        public class SabreFlightBookingCheckCCVerificationRequest
        {
            string token { get; set; }
            string pcc { get; set; }
            string retain { get; set; }
            string code { get; set; }
            string airlineCode { get; set; }
            string ccNumber { get; set; }
            string expDate { get; set; }
            string amount { get; set; }
            string currCode { get; set; }
        }

    }
}
