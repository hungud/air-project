using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace App.Model.WCFData
{
    [ServiceContract]
    public interface IAirService
    {

        #region SabreService

        [OperationContract]
        [WebInvoke(UriTemplate = "GetData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        string GetData();


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SessionCreate/{pcc}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SessionCreate(string pcc);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "airBookRequest/{token},{dtDepartureTime},{dtArrivalTime},{flightNumber},{destlocationCode},{AirEquipType},{originLocationCode},{resBookDesigCode},{status},{code}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string airBookRequest(string token, string dtDepartureTime, string dtArrivalTime, string flightNumber, string destlocationCode, string AirEquipType, string originLocationCode, string resBookDesigCode, string status, string code);

        [WebInvoke(Method = "POST", UriTemplate = "addTravelItinerayRequest/{token},{AgencyAddressLine},{AgencyCityName},{AgencyCountryCode},{AgencyPostalCode},{AgencyStateCode},{AgencyStreetNumber},{GivenName},{Surname},{Phone1LocationCode},{PNameNumber},{Phone1},{Phone1UseType},{PType},{NameRef},{PassengerEmail},{SText},{EMailType},{AgencyTicketTime},{AgencyTicketType},{AgencyQueueNo}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string addTravelItinerayRequest(string token, string AgencyAddressLine, string AgencyCityName, string AgencyCountryCode, string AgencyPostalCode, string AgencyStateCode, string AgencyStreetNumber, string GivenName, string Surname, string Phone1LocationCode, string PNameNumber, string Phone1, string Phone1UseType, string PType, string NameRef, string PassengerEmail, string SText, string EMailType, string AgencyTicketTime, string AgencyTicketType, string AgencyQueueNo);

        [WebInvoke(Method = "POST", UriTemplate = "airPriceRequest/{token},{retain},{code},{quantity}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airPriceRequest(string token, string retain, string code, string quantity);

        [WebInvoke(Method = "POST", UriTemplate = "airSeatBookRequest/{token},{nameNumber},{pref},{segNumber}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airSeatBookRequest(string token, string nameNumber, string pref, string segNumber);

        [WebInvoke(Method = "POST", UriTemplate = "airFlightdetails/{token},{dtDepartureTime},{flightNumber},{destlocationCode},{originLocationCode},{code}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airFlightdetails(string token, string dtDepartureTime, string flightNumber, string destlocationCode, string originLocationCode, string code);

        [WebInvoke(Method = "POST", UriTemplate = "endTransaction/{token},{ind},{receivedFrom}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string endTransaction(string token, string ind, string receivedFrom);

        #endregion SabreService


        #region SabreSOAP_Itinerary
        [WebInvoke(Method = "POST", UriTemplate = "RetrievePNR/{token},{Id},{tStamp},{subArea}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string RetrievePNR(string token, string Id, string tStamp, string subArea);

        [WebInvoke(Method = "POST", UriTemplate = "addSpecialRequest/{token},{code},{personName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string addSpecialRequest(string token, string code, string personName, string text);

        [WebInvoke(Method = "POST", UriTemplate = "airCancelRequest/{token},{ind}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airCancelRequest(string token, string ind);
        #endregion SabreSOAP_Itinerary


        #region AirQuoteService
        [WebInvoke(Method = "POST", UriTemplate = "airBFMRequestSingle/{token},{rphOne},{rphOne_depDateTime},{rphOne_OriginLocCode},{rphOne_DesLocCode},{cabin},{prefLevel},{tripType},{seatRequest},{pCode},{pQuantity}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airBFMRequestSingle(string token, string rphOne, string rphOne_depDateTime, string rphOne_OriginLocCode, string rphOne_DesLocCode, string cabin, string prefLevel, string tripType, string seatRequest, string pCode, string pQuantity);

        [WebInvoke(Method = "POST", UriTemplate = "airBFMRequestMultiCity/{token},{rphOne},{rphOne_depDateTime},{rphOne_OriginLocCode},{rphOne_DesLocCode},{rphTwo},{rphTwo_depDateTime},{rphTwo_OriginLocCode},{rphTwo_DesLocCode},{cabin},{prefLevel},{tripType},{seatRequest},{pCode},{pQuantity}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airBFMRequestMultiCity(string token, string rphOne, string rphOne_depDateTime, string rphOne_OriginLocCode, string rphOne_DesLocCode, string rphTwo, string rphTwo_depDateTime, string rphTwo_OriginLocCode, string rphTwo_DesLocCode, string cabin, string prefLevel, string tripType, string seatRequest, string pCode, string pQuantity);

        [WebInvoke(Method = "POST", UriTemplate = "airBFMRequestComplex/{token},{rphOne},{rphOne_depDateTime},{rphOne_OriginLocCode},{rphOne_DesLocCode},{rphTwo},{rphTwo_depDateTime},{rphTwo_OriginLocCode},{rphTwo_DesLocCode},{rphThree},{rphThree_depDateTime},{rphThree_OriginLocCode},{rphThree_DesLocCode},{rphFour},{rphFour_depDateTime},{rphFour_OriginLocCode},{rphFour_DesLocCode},{cabin},{prefLevel},{tripType},{seatRequest},{pCode},{pQuantity}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string airBFMRequestComplex(string token, string rphOne, string rphOne_depDateTime, string rphOne_OriginLocCode, string rphOne_DesLocCode, string rphTwo, string rphTwo_depDateTime, string rphTwo_OriginLocCode, string rphTwo_DesLocCode, string rphThree, string rphThree_depDateTime, string rphThree_OriginLocCode, string rphThree_DesLocCode, string rphFour, string rphFour_depDateTime, string rphFour_OriginLocCode, string rphFour_DesLocCode, string cabin, string isSmoking, string prefLevel, string tripType, string XoFareValue, string seatRequest, string pCode, string pQuantity);

        #endregion AirQuoteService
    }
}
