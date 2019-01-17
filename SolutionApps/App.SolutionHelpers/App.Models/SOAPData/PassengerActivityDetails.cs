using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Model.SOAPData
{
    public class PassengerActivityDetails
    {
        /// <summary>
        /// Gets the passenger's given name.
        /// </summary>
        /// <value>
        /// The given name.
        /// </value>
        string GivenName { get; set; }

        /// <summary>
        /// Gets the passenger's surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        string Surname { get; set; }

        /// <summary>
        /// Gets the passenger's first phone location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        string Phone1LocationCode { get; set; }

        /// <summary>
        /// Gets the passenger's first phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        string Phone1 { get; set; }

        /// <summary>
        /// Gets the passenger's first phone use type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string Phone1UseType { get; set; }

        /// <summary>
        /// Gets the passenger's second phone location code.
        /// </summary>
        /// <value>
        /// The location code.
        /// </value>
        string Phone2LocationCode { get; set; }

        /// <summary>
        /// Gets the passenger's second phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        string Phone2 { get; set; }

        /// <summary>
        /// Gets the passenger's second phone use type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        string Phone2UseType { get; set; }

        /// <summary>
        /// Gets the passenger's email.
        /// </summary>
        /// <value>
        /// The passenger's email.
        /// </value>
        string PassengerEmail { get; set; }

        /// <summary>
        /// Gets the agency first address line.
        /// </summary>
        /// <value>
        /// The agency first address line.
        /// </value>
        string AgencyAddressLine { get; set; }

        /// <summary>
        /// Gets the name of the agency city.
        /// </summary>
        /// <value>
        /// The name of the agency city.
        /// </value>
        string AgencyCityName { get; set; }

        /// <summary>
        /// Gets the agency country code.
        /// </summary>
        /// <value>
        /// The agency country code.
        /// </value>
        string AgencyCountryCode { get; set; }

        /// <summary>
        /// Gets the agency postal code.
        /// </summary>
        /// <value>
        /// The agency postal code.
        /// </value>
        string AgencyPostalCode { get; set; }

        /// <summary>
        /// Gets the agency street number.
        /// </summary>
        /// <value>
        /// The agency street number.
        /// </value>
        string AgencyStreetNumber { get; set; }

        /// <summary>
        /// Gets the agency state code.
        /// </summary>
        /// <value>
        /// The agency state code.
        /// </value>
        string AgencyStateCode { get; set; }

        /// <summary>
        /// Gets the ticketing type.
        /// </summary>
        /// <value>
        /// The ticketing type.
        /// </value>
        string AgencyTicketType { get; set; }
    }
}
