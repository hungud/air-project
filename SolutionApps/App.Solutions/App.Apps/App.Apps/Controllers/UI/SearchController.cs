using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using App.BusinessLayer.AccountManager;
using System.Configuration;
using App.Models.AccountManager;
using HomeModel = App.Models.BPDHCDMS.LoginInfo;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace App.Apps.Controllers
{
    public class SearchController : App.Base.BaseHomeController
    {
        public string UserIPAdress { get; set; }
        public string UserDomainAdress { get; set; }
        public  ActionResult Index()
        {
            ViewBag.Message = "Welcome Air Application.";
            return View();
        }
        public ActionResult BargainFinderMaxSearch()
        {
            ViewBag.Message = "Welcome Air Application.";
            return View();
        }

        public async Task<string> GetUserdata()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("http://test.nanojot.com/authentication/account/GetUsersData").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
              //  return Json(result,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return "";
                //return Json("", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
