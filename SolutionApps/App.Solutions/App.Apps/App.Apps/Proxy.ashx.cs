using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Web.Caching;
using System.Net;

namespace App.SolutionApp
{
    namespace WebProxy
    {
        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL            
        /// Company Name:   RMSI            
        /// Created Date:   Developed on:      
        /// Summary     :   BaseProxy
        /// http://localhost:11011/Proxy.ashx?ping
        /// http://localhost:11011/Proxy.ashx?http://spatialvm:6080/arcgis/rest/services
        /// http://125.16.65.30/rmsigisapps/GISProxy/
        ///*************************************************
        /// </summary>
        public class Proxy : App.Base.BaseProxy.BaseProxy
        {
        }
    }
}