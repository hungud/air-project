using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace App.Auth.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this IIdentity identity)
        {
            //add logic to get id from user table using Username (User.Identity.Name)
            // Test for null to avoid issues during local testing\
            return "";   
        }
    }

}