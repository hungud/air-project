using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace App.ConfigService.Controllers.SOAPServices
{
    public class CreateDynamicXML
    {

        public void OTA_AirAvailRQ()
        {
            List<Author> list = CreateAuthorList();
            XElement xmlfromlist = new XElement("OTA_AirAvailRQ",
                                                 from a in list
                                                 select
                                                     new XElement("OriginDestinationInformation",
                                                     new XElement("FlightSegment "),
                                                     new XAttribute("DepartureDateTime", a.NumberofArticles),
                                                     new XAttribute("ArrivalDateTime", a.NumberofArticles),
                                                     new XAttribute("FlightNumber", a.NumberofArticles),
                                                     new XAttribute("NumberInParty", a.NumberofArticles),
                                                     new XAttribute("ResBookDesigCode", a.NumberofArticles),
                                                     new XAttribute("Status", a.NumberofArticles),
                                                     new XElement("DestinationLocation", a.NumberofArticles),
                                                     new XAttribute("LocationCode ", a.NumberofArticles),
                                                     new XElement("Equipment", a.NumberofArticles),
                                                     new XAttribute("AirEquipType", a.NumberofArticles),
                                                      new XElement("MarketingAirline", a.NumberofArticles),
                                                     new XAttribute("Code ", a.NumberofArticles),
                                                     new XAttribute("FlightNumber ", a.NumberofArticles),
                                                     new XElement("OperatingAirline ", a.NumberofArticles),
                                                     new XAttribute("Code", a.NumberofArticles),
                                                     new XElement("OriginLocation", a.NumberofArticles),
                                                     new XAttribute("LocationCode", a.NumberofArticles)
                                                     ));
        }

        static List<Author> CreateAuthorList()
        {
            List<Author> list = new List<Author>();
            //{
            //    new Author(){Name="Dhananjay Kumar",NumberofArticles= 60},
            //    new Author (){Name =" Rekha Singh ", NumberofArticles =5},
            //    new Author () {Name = " Deepti maya patra",NumberofArticles =55},
            //    new Author (){Name=" Mahesh Chand",NumberofArticles = 700},
            //    new Author (){Name =" Mike Gold",NumberofArticles = 300},
            //    new Author(){Name ="Praveen Masood",NumberofArticles = 200},
            //    new Author (){Name ="Shiv Prasad Koirala",NumberofArticles=100},
            //    new Author (){Name =" Mamata M ",NumberofArticles =50},
            //    new Author (){Name=" Puren Mehara",NumberofArticles =50}
            //};
            Author Item = new Author();

            return list;
        }

    }

    class Author
    {
        public string Name { get; set; }
        public int NumberofArticles { get; set; }
    }
}