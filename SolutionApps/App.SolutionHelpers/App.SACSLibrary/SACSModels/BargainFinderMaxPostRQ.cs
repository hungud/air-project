using System;
using SACS.Library.Activities.InputData;

namespace SACS.Models
{
    /// <summary>
    /// The POST request from the BargainFinderMax form.
    /// </summary>
    public class BargainFinderMaxPostRQ : IBargainFinderMaxData
    {
        public DateTime DepartureDate { get; set; }

        public string OriginAirportCode { get; set; }

        public string DestinationAirportCode { get; set; }

        public string RPH { get; set; }

        public string RequestorID { get; set; }

        public string RequestorType { get; set; }

        public string RequestorCompanyCode { get; set; }

        public string PassengerTypeCode { get; set; }

        public int NumberOfPassengers { get; set; }

        public string RequestType { get; set; }
    }
}




/*


{
 "OTA_AirLowFareSearchRQ": {
     "Target": "Production",
       "POS": {
            "Source": [{
                "PseudoCityCode":"F9CE",
                "RequestorID": {
                    "Type": "1",
                  "ID": "1",
                    "CompanyName": {
                        
                  }
             }
         }]
        },
        "OriginDestinationInformation": [{
          "RPH": "1",
           "DepartureDateTime": "2016-10-07T11:00:00",
           "OriginLocation": {
             "LocationCode": "DFW"
         },
            "DestinationLocation": {
                "LocationCode": "CDG"
         },
            "TPA_Extensions": {
             "SegmentType": {
                    "Code": "O"
               }
         }
     },
        {
         "RPH": "2",
           "DepartureDateTime": "2016-10-08T11:00:00",
           "OriginLocation": {
             "LocationCode": "CDG"
         },
            "DestinationLocation": {
                "LocationCode": "DFW"
         },
            "TPA_Extensions": {
             "SegmentType": {
                    "Code": "O"
               }
         }
     }],
       "TravelPreferences": {
          "ValidInterlineTicket": true,
           "CabinPref": [{
             "Cabin": "Y",
             "PreferLevel": "Preferred"
            }],
           "TPA_Extensions": {
             "TripType": {
                   "Value": "Return"
             },
                "LongConnectTime": {
                    "Min": 780,
                 "Max": 1200,
                    "Enable": true
              },
                "ExcludeCallDirectCarriers": {
                  "Enabled": true
             }
         }
     },
        "TravelerInfoSummary": {
            "SeatsRequested": [1],
          "AirTravelerAvail": [{
              "PassengerTypeQuantity": [{
                 "Code": "ADT",
                    "Quantity": 1
               }]
            }]
        },
        "TPA_Extensions": {
         "IntelliSellTransaction": {
             "RequestType": {
                    "Name": "50ITINS"
             }
         }
     }
 }
}
///===============================================================
{

 "OTA_AirLowFareSearchRQ": {
     "Target": "Production",
       "POS": {
            "Source": [{
                "PseudoCityCode":"F9CE",
                "RequestorID": {
                    "Type": "1",
                  "ID": "1",
                    "CompanyName": {                      
                  }
             }
         }]
        },
        "OriginDestinationInformation": [{
          "RPH": "1",
           "DepartureDateTime": "2016-10-07T11:00:00",
           "OriginLocation": {
             "LocationCode": "DFW"
         },
            "DestinationLocation": {
                "LocationCode": "CDG"
         },
            "TPA_Extensions": {
             "SegmentType": {
                    "Code": "O"
               }
         }
     },
        {
         "RPH": "2",
           "DepartureDateTime": "2016-10-08T11:00:00",
           "OriginLocation": {
             "LocationCode": "CDG"
         },
            "DestinationLocation": {
                "LocationCode": "DFW"
         },
            "TPA_Extensions": {
             "SegmentType": {
                    "Code": "O"
               }
         }
     }],
       "TravelPreferences": {
          "ValidInterlineTicket": true,
           "CabinPref": [{
             "Cabin": "Y",
             "PreferLevel": "Preferred"
            }],
           "TPA_Extensions": {
             "TripType": {
                   "Value": "Return"
             },
                "LongConnectTime": {
                    "Min": 780,
                 "Max": 1200,
                    "Enable": true
              },
                "ExcludeCallDirectCarriers": {
                  "Enabled": true
             }
         }
     },
        "TravelerInfoSummary": {
            "SeatsRequested": [1],
          "AirTravelerAvail": [{
                  "PassengerTypeQuantity": [{
                 "Code": "ADT",
                    "Quantity": 1
               }]
            }]
        },
        "TPA_Extensions": {
         "IntelliSellTransaction": {
             "RequestType": {
                    "Name": "50ITINS"
             }
         }
     }
 }
}


    */





















