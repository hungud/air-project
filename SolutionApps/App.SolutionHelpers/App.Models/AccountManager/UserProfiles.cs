using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Security.Claims;

using System.Threading;
namespace App.Models.AccountManager
{

    #region STSUserProfile

    public class Token 
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public TokenData UserTokenEntity { get; set; }
        public string AuthToken { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
    }
    public class TokenData
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PetName { get; set; }
        public bool IsAuthenticated { get; set; }
    }
    public class TokenDataManager
    {
        internal static Token GetTokenObject()
        {
            Token myToken = new Token();
            List<string> list = new List<string>();
            ClaimsIdentity identity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            foreach (System.Security.Claims.Claim claim in identity.Claims)
            {
                list.Add(claim.Type);
                list.Add(claim.Value);
                list.Add(claim.ValueType);
                list.Add(claim.Subject.Name);
                list.Add(claim.Issuer);
            }
            ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                //myToken.IsAuthenticated = claimsPrincipal.Identity.IsAuthenticated;
                //myToken.SurName = claimsPrincipal.FindFirst(ClaimTypes.Surname).Value;
                //myToken.NameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                //myToken.Name = claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
                //myToken.Role = claimsPrincipal.FindFirst(ClaimTypes.Role).Value;
                //myToken.Email = claimsPrincipal.FindFirst(ClaimTypes.Email).Value;
                //myToken.PetName = claimsPrincipal.FindFirst(ClaimTypes.UserData).Value;
            }
            return myToken;
        }
    }

    #endregion STSUserProfile


    #region UserProfileLibrary

    public class UserProfiles : App.Common.Util.ReturnMessages
    {
        public int IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsAuthenticatedUser { get; set; }

        public string UserID { get; set; }
        public string APPID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int32 RoleID { get; set; }
        public string RoleName { get; set; }
        public string EmailID { get; set; }
        public Int32 DeptID { get; set; }
        public List<UserPermissions> User_Permissions { get; set; }
        public string Token { get; set; }

        public UserProfiles()
        {

        }
        public UserProfiles(string userid, string username, string firstname, string lastname, int isapproved, bool islockedout, bool isauthenticateduser, Int32 roleid, string rolename)
        {
            this.UserID = userid;
            this.UserName = username;
            this.IsApproved = isapproved;
            this.IsAuthenticatedUser = isauthenticateduser;
            this.IsLockedOut = islockedout;
            this.RoleID = roleid;
            this.RoleName = rolename;
            this.FirstName = firstname;
            this.LastName = lastname;
        }
    }


    public class UserProfilesList : ArrayList
    {
        public new UserProfiles this[int i]
        {
            get
            {
                return (UserProfiles)base[i];
            }
            set
            {
                base[i] = value;
            }
        }
    }
    public class UserPermissions 
    {
        public UserPermissions()
        {

        }
       
        public int IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsAuthenticatedUser { get; set; }
        public Int32 UserID { get; set; }
        public Int32 APPID { get; set; }
        

    }


    #endregion



    namespace UserAccount
    {
        
        public class UserProfile
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
        }

        public class RegisterExternalLoginModel
        {
            public string UserName { get; set; }
            public string ExternalLoginData { get; set; }
        }

        public class LocalPasswordModel
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        public class RegisterModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class ExternalLogin
        {
            public string Provider { get; set; }
            public string ProviderDisplayName { get; set; }
            public string ProviderUserId { get; set; }
        }
    }


}
