using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Apps.Controllers.UI
{
    public class TermsController:Controller
    {
        public ActionResult Privacypolicy()
        {

            return View();
        }

        public ActionResult Conditions()
        {
            return View();
        }
    }
}