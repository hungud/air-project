using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Model.SOAPData
{


    public class FlightBookingPayentRQ
    {
        public FlightBookingPayentRQ()
        {
        }
    }
    public class FlightBookingEnhanceBookRQ
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
    public class FlightBookingAddTravelItinerayRequest
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
    public class FlightBookingAddPassengerRequest
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
    public class FlightBookingCheckCCVerificationRequest
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


    #region AirBookingMultiModelsList

    public class AirPassengerCreateCardModelsList
    {
        public AirPassengerCreateCardModelsList()
        {
        }
        public List<AirFlightDetails> flightDetails { get; set; }
        public List<PassengerDetails> passengerDetails { get; set; }
        public List<CreateCardAndOtherDetails> createCardOtherDetails { get; set; }
       

    }

    public class AirNoofPassengers
    {
        public string PassengerCode { get; set; }
        public int NoOfPassengers { get; set; }

    }
    public class CreateCardAndOtherDetails
    {
        public CreateCardAndOtherDetails()
        {

        }

        public string airlineCode { get; set; }
        public string ccNumber { get; set; }
        public string expDate { get; set; }
        public string amount { get; set; }
        public string currCode { get; set; }

    }
    public class AirFlightDetails
    {
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
    public class PassengerDetails
    {
        public string PhoneLocationCode { get; set; }
        public string PassengerNameNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneUseType { get; set; }
        public string PassengerEmail { get; set; }
        public string PassengerNameRef { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }
    public class FlightSegmentData
    {
        /// <summary>
        /// Gets or sets the departure date time.
        /// </summary>
        /// <value>
        /// The departure date time.
        /// </value>
        public string DepartureDateTime { get; set; }

        public string ArrivalDateTime { get; set; }
        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }

        public string NumberInParty { get; set; }

        public string AirEquipType { get; set; }

        public string ResBookDesigCode { get; set; }

        public string Status { get; set; }

        public string iseTicket { get; set; }

        /// <summary>
        /// Gets or sets the marketing airline code.
        /// </summary>
        /// <value>
        /// The marketing airline code.
        /// </value>
        public string MarketingAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the destination location (airport) code.
        /// </summary>
        /// <value>
        /// The destination location code.
        /// </value>
        public string DestinationLocationCode { get; set; }

        /// <summary>
        /// Gets or sets the origin location (airport) code.
        /// </summary>
        /// <value>
        /// The origin location code.
        /// </value>
        public string OriginLocationCode { get; set; }

        /// <summary>
        /// Gets or sets the designation code (<c>ResBookDesigCode</c>).
        /// </summary>
        /// <value>
        /// The designation code.
        /// </value>
        public string DesignationCode { get; set; }

    }
    #endregion AirBookingMultiModelsList


}