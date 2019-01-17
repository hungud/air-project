using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Apps.ViewModal
{
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