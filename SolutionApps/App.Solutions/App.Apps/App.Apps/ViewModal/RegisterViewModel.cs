using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Apps.ViewModal
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
    //public class LoginViewModel
    //{
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //}
}