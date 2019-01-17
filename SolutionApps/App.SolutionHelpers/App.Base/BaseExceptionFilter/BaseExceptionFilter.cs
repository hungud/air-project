using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace App.Base
{
    public class BaseExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext cntxt)
        {
            var exceptionType = cntxt.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                cntxt.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                //cntxt.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
        }
    }
}
