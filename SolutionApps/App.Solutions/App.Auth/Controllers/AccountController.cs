using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using App.Auth.Models;
//using login_functionlity.db.Services;
using register_functionlity.DB.Service;
using CaptchaMvc.HtmlHelpers;
using register_functionlity.DB.Model;
using App.Auth.Helpers;
using System.Web.Http.Cors;

namespace App.Auth.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region Check User Login ID
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [AllowAnonymous]
        public JsonResult GetUsersData()
        {
            CheckUserLogin modal = new CheckUserLogin();


            if (User.Identity.IsAuthenticated)
            {
                modal.LoginStatus = "True";
                modal.UserID = Session["UserID"].ToString();
            }
            else
            {
                modal.LoginStatus = "False";
                modal.UserID = "NotAvailable";
            }
            return Json(modal, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [Authorize]
        public ActionResult Bookings()
        {
            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var identity1 = (ClaimsIdentity)User.Identity;

            IEnumerable<Claim> claims = identity1.Claims;
            var companyDetailId = claims.Where(c => c.Type == ClaimTypes.Sid)
                               .Select(c => c.Value).SingleOrDefault();
            long Userid = Convert.ToInt32(Session["UserID"]);
            ViewBag.Userid = Userid;
            //ViewBag.message = new BookingsService().GetBookings(Userid);
            return View(new BookingsService().GetBookings(Userid));
        }
        [Authorize]
        public ActionResult UpdateProfile()
        {
            var name = User.Identity.Name;
            return View(new UserService().GetUserByUserName(name));
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserModel model, string selBCICountry, string selPICCType, string ExpirationDateM, string ExpirationDateY)
        {
            var name = User.Identity.Name;
            model.UserName = name;
            model.PaymentMethod = selPICCType;
            model.UserLocation = selBCICountry;
            model.CreditCardExpDate = ExpirationDateM + "-" + ExpirationDateY;
            new UserService().UpdateUserProfile(model);
            return View("Submit");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            returnUrl = (returnUrl == null) ? "/" : returnUrl;

            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.Trim().ToLower().Contains("logoff"))
                returnUrl = "/";

            if (Session["quotepageurl"] == null)
            {
                Session["quotepageurl"] = returnUrl;
                ViewBag.ReturnUrl = Session["quotepageurl"].ToString();
            }
            else
            {
                ViewBag.ReturnUrl = Session["quotepageurl"].ToString();
            }
            //Session["quotepageurl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string homepageurl, string returnUrl)
        {
            returnUrl = returnUrl == "/" ? "/air" : (returnUrl == null ? "/" : returnUrl);

            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.Trim().ToLower().Contains("logoff"))
                returnUrl = "/";

            ViewBag.ReturnUrl = returnUrl;
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var getUser = new UserService().GetUser(model.UserName, model.Password);

                if (getUser.Id > 0)
                {
                    var ident = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Role, getUser.RoleName),
                        new Claim(ClaimTypes.Sid, getUser.CompanyDetailId.ToString())
                  },
                      DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication.SignIn(
                       new AuthenticationProperties { IsPersistent = true }, ident);

                    //return RedirectToAction("Index", "Search"); // auth succeed 
                    Session["quotepageurl"] = null;
                    Session["domain"] = homepageurl;
                    Session["UserID"] = getUser.Id;

                    returnUrl = (returnUrl.IndexOf("Search") > -1 ? returnUrl + "&LoginStatus=True&LoginUID=" + getUser.Id : returnUrl + "?LoginStatus=True&LoginUID=" + getUser.Id);
                    //returnUrl += "&LoginStatus=True&LoginUID=" + getUser.Id;
                    return Redirect(returnUrl);
                }
                // invalid username or password
                ModelState.AddModelError("", "invalid username or password");
            }

            ViewBag.ErrMessage = "Error: captcha is not valid.";

            return View();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model, string domain, string emailfrom)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var user = new UserService().GetUserByEmail(model.Email);
                    if (user != null)
                    {
                        var template = System.IO.File.ReadAllText(Server.MapPath("~/Helpers/EmailTemplate/ForgotPassword.html"));
                        var code = Guid.NewGuid().ToString();
                        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        template = template.Replace("###passwordurl###", callbackUrl + "&d=" + domain);
                        template = template.Replace("###passwordurltext###", "Click Here");
                        template = template.Replace("###sitename###", domain);
                        template = template.Replace("###siteurl###", domain);
                        string message = Email.Send(user.EmailAddress, template, "Forgot Password", emailfrom);
                        new UserService().SaveResetPasswordCode(user.Id, code);
                        ViewBag.message = message;
                        // Don't reveal that the user does not exist or is not confirmed
                        //return RedirectToAction("Login");
                        return View("ForgotPasswordConfirmation");

                    }

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                    // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    // return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
            }
            ModelState.Remove("CaptchaInputText");

            ViewBag.ErrMessage = "Error: captcha is not valid.";

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(int userId, string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            var model = new ResetPasswordViewModel
            {
                Id = userId,
                Code = code
            };
            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new UserService().GetUserByEmail(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            new UserService().ResetPassword(user.Id, model.Code, model.Password);
            return RedirectToAction("ResetPasswordConfirmation", "Account");
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            string domain = string.Empty;
            if (Session["domain"] != null)
            {
                domain = Session["domain"].ToString();
            }

            Session["UserID"] = null;
            Session.Clear();
            Session.Abandon();
            //if (!(string.IsNullOrEmpty(domain)))
            //{
            //    if (domain == "flypapa.com")
            //        domain = "http://flypapa.com/myindex.html";
            //    else if (domain == "skyflight.ca")
            //        domain = "http://skyflight.ca/"; 

            //    return Redirect(domain);
            //}
            //else
            //{
            return Redirect("/authentication/Account/login?r=1");
            //}
            ////return RedirectToAction("Index", "Search");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            returnUrl = (returnUrl == null) ? "/" : returnUrl;

            if (Session["quotepageurl"] == null)
            {
                Session["quotepageurl"] = returnUrl;
                ViewBag.ReturnUrl = Session["quotepageurl"].ToString();
            }
            else
            {
                ViewBag.ReturnUrl = Session["quotepageurl"].ToString();
            }

            UserModel model = new UserModel();
            model.DisabledUserFields = model.DisabledCompanyFields = true;
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserModel model, string btnSubmit, string CompanyType, int ParentCompanyID, string ReturnUrl)
        {
            ReturnUrl = (ReturnUrl == null ? "/" : ReturnUrl);
            ViewBag.ReturnUrl = ReturnUrl;
            if (btnSubmit.Equals("Find Company"))
            {
                ModelState.Remove("EmailAddress");
                ModelState.Remove("FirstName");
                ModelState.Remove("LastName");
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("CompanyName");
                ModelState.Remove("CompanyEmail");
                ModelState.Remove("ContactNumber");
                ModelState.Remove("GSTRegNo");
                ModelState.Remove("StreetAddress");
                ModelState.Remove("CompanyContactNumber");
                ModelState.Remove("Location");
                ModelState.Remove("Extension");

                var companyDetail = new RegisterService().GetCompanyByPhoneNumber(model.PhoneNumber);
                model.DisabledCompanyFields = false;
                //model.HideCompany = false;
                if (companyDetail != null)
                {
                    model.CompanyName = companyDetail.Name;
                    model.CompanyEmail = companyDetail.EMailName;
                    model.FaxNumber = companyDetail.Fax;
                    model.StreetAddress = companyDetail.StreetAddress;
                    model.CompanyContactNumber = companyDetail.PhoneNumber;
                    model.WebsiteURL = companyDetail.WebsiteURL;
                    model.GSTRegNo = companyDetail.GSTRegNo;
                    model.Location = companyDetail.Location;
                    model.CompanyDetailId = companyDetail.Id;
                    model.DisabledCompanyFields = true;
                }
                else
                {
                    ModelState.AddModelError("", "No company details found for the given phone number, please enter Company Details and User Details to create a new Company and User");
                }
            }
            else
            {

                model.CompanyType = CompanyType;
                model.ParentCompanyID = ParentCompanyID;
                new RegisterService().SaveCompanyUser(model);
                //return Redirect(ReturnUrl);

                //return RedirectToAction("Login");
                return View("RegisterConfirmation");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult MemberAgencyRegister(string returnUrl)
        {
            returnUrl = (returnUrl == null) ? "/" : returnUrl;

            if (Session["quotepageurl"] == null)
            {
                Session["quotepageurl"] = returnUrl;
                ViewBag.ReturnUrl = Session["quotepageurl"].ToString();
            }
            else
            {
                ViewBag.ReturnUrl = Session["quotepageurl"].ToString();
            }

            UserModel model = new UserModel();
            model.DisabledUserFields = model.DisabledCompanyFields = true;
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult MemberAgencyRegister(UserModel model, string btnSubmit, string CompanyType, int ParentCompanyID, string ReturnUrl)
        {
            ReturnUrl = (ReturnUrl == null ? "/" : ReturnUrl);
            ViewBag.ReturnUrl = ReturnUrl;
            model.CompanyType = CompanyType;
            model.ParentCompanyID = ParentCompanyID;
            model.CompanyDetailId = ParentCompanyID;
            new RegisterService().SaveCompanyUser(model);
            return View("RegisterConfirmation");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Search");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }


}