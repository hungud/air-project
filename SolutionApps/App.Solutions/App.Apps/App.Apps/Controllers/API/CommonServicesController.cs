using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace App.Apps.Controllers.API
{
    public class CommonServicesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllStudents()
        { 
            return Ok("");
        }
    }
}
