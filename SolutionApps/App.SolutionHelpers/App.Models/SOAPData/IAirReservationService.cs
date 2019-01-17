using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using App.Models;
namespace App.Model.SOAPData
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAirReservationService
    {
        [OperationContract]
        string GetData();


        [OperationContract]
        string GetSabreBooking(List<AirFlightDetails> flights, List<PassengerDetails> passengersList, List<AirNoofPassengers> pQuantity);

        [OperationContract]
        string SessionCreate(string pcc);

        [OperationContract]
        string ChangeAirPCC(string token, string pcc, string newpcc);

        [OperationContract]
        string AirBookMultiSegmentRQ(string pcc, string token, List<AirFlightDetails> flights);

        [OperationContract]
        string AddTravelItinerayRQ(string pcc, string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo, string SText, List<PassengerDetails> passengersList);

        [OperationContract]
        string airBookRequest(string pcc, string token, string dtDepartureTime, string dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code);

        [OperationContract]
        string addTravelItinerayRequest(string pcc, string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string GivenName, string Surname, string Phone1LocationCode, string PNameNumber, string Phone1, string Phone1UseType, string PType, string NameRef, string PassengerEmail, string SText, string EMailType, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo);

        [OperationContract]
        string airPriceRequest(string pcc, string token, string retain, List<AirNoofPassengers> pQuantity);

        [OperationContract]
        string airSeatBookRequest(string token, string nameNumber, string pref, string segNumber);

        [OperationContract]
        string airFlightdetails(string token, string dtDepartureTime, string flightNumber, string destlocationCode, string originLocationCode, string code);

        [OperationContract]
        string CCVerificationRequest(string token, string pcc, string retain, string code, string airlineCode, string ccNumber, string expDate, string amount, string currCode);
        [OperationContract]
        string endTransaction(string pcc, string token, string ind, string receivedFrom);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

}
