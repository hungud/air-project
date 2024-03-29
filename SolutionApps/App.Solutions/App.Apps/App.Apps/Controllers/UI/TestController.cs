﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using App.BusinessLayer.AccountManager;
using System.Configuration;
using App.Models.AccountManager;
using HomeModel = App.Models.BPDHCDMS.LoginInfo;
namespace App.Apps.Controllers
{
    public class TestController : App.Base.BaseHomeController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult Home()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult About()
        {
            //GetTest();
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult ProductionProduct()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult WebAPITest()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult SOAPTest()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        public ActionResult WCFTest()
        {
            ViewBag.Message = "Welcome BP DHC Application.";
            return View();
        }
        
        #region Application Error Management
        // GET: Error/NotFound
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        // GET: Error/Error
        public ActionResult Error()
        {
            Response.StatusCode = 500;
            return View();
        }
        #endregion Application Error Management
    }
}
