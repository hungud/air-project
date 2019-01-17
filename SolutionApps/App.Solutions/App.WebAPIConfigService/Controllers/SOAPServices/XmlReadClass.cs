using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace App.ConfigService.Controllers.SOAPServices
{
    public class XmlReadClass
    {

        public List<Model.SOAPData.FlightSegmentData> ReadSearchXML(string getResult)
        {
            List<Model.SOAPData.FlightSegmentData> flightsearch = new List<Model.SOAPData.FlightSegmentData>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(getResult);
            XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName("OriginDestinationOption");
            foreach (XmlNode node in nodeList)
            {
                Model.SOAPData.FlightSegmentData flightDataItem = new Model.SOAPData.FlightSegmentData();
                flightDataItem.ArrivalDateTime = node.ChildNodes[0].Attributes["ArrivalDateTime"].Value;
                flightDataItem.DepartureDateTime = node.ChildNodes[0].Attributes["DepartureDateTime"].Value;
                flightDataItem.FlightNumber = node.ChildNodes[0].Attributes["FlightNumber"].Value;
                flightDataItem.DesignationCode = node.ChildNodes[0]["DestinationLocation"].Attributes["LocationCode"].Value;
                flightDataItem.MarketingAirlineCode = node.ChildNodes[0]["MarketingAirline"].Attributes["Code"].Value;
                flightDataItem.OriginLocationCode = node.ChildNodes[0]["OriginLocation"].Attributes["LocationCode"].Value;
                flightDataItem.AirEquipType = node.ChildNodes[0]["Equipment"].Attributes["AirEquipType"].Value;
                flightDataItem.ResBookDesigCode = "Y";//node.ChildNodes[0]["BookingClassAvail"].Attributes["ResBookDesigCode"].Value;
                flightDataItem.Status = "NN";
                flightDataItem.iseTicket = node.ChildNodes[0].Attributes["eTicket"].Value;
                flightsearch.Add(flightDataItem);
            }

            return flightsearch;
        }

    }
}