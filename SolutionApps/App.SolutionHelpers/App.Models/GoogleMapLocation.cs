namespace App.Models
{
    using System;
    using System.Collections.Generic;
    
    public class GoogleMapLocation
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class GoogleMapLocationList : List<GoogleMapLocation>
    { }
}
