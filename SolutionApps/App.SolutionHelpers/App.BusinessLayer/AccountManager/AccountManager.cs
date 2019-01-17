using App.Models.AccountManager;
using App.Common.Util;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.BusinessLayer.AccountManager
{
    public class AccountManager
    {
        public AccountManager()
        {
        }

        public AccountManager(CultureInfo p_ci)
        {
         
        }

        public string AuthenticateUser(UserProfiles loginUser)
        {
            string returnValue = string.Empty;
            try
            {
            }
            catch (Exception ex)
            {
            }
            return returnValue;
        }

        Boolean IsPWADomainUser(string userDomain)
        {
            string pwaDomainValue = System.Configuration.ConfigurationManager.AppSettings["PWADomain"];
            if (string.IsNullOrEmpty(pwaDomainValue))
                return false;
            if (userDomain.Contains(pwaDomainValue).Equals(true) && userDomain.IndexOf("\\") > 0)
            {
                string[] paramsLogin = userDomain.Split('\\');
                if (paramsLogin.Length > 0)
                {
                    if (paramsLogin.Length == 2 && String.IsNullOrEmpty(paramsLogin[1].ToString()) == false)
                    {
                        string domainName = paramsLogin[0].ToString();
                        if (string.Compare(domainName.ToUpper(), userDomain.ToUpper()) == 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            else
                return false;

            return false;
        }

        Boolean IsActiveDirectoryUser(string userEmailAddress)
        {
            string pwaDomainValue = System.Configuration.ConfigurationManager.AppSettings["PWADomain"].ToUpper();

            if (string.IsNullOrEmpty(pwaDomainValue))
                return false;

            if (userEmailAddress.ToUpper().IndexOf(pwaDomainValue) > -1)
                return true;
            else
                return false;
        }


        public UserProfiles ValidateUser(string emailAddress, string ePassword, string userLogonDomain, string IPAddress, Int32 appID)
        {
            string lmsg = string.Empty;
            UserProfiles returnValue = new UserProfiles();
            try
            {
               

            }
            catch (Exception ex)
            {
            }

            return returnValue;
        }



        #region Validate User

        public UserProfiles ValidateUser_User(string pUserID, string pPassword, string userLogonDomain, string IPAddress, Int32 appID)
        {
            string lmsg = string.Empty;
            UserProfiles returnValue = new UserProfiles();
            try
            {
                if (string.IsNullOrEmpty(pUserID) && string.IsNullOrEmpty(pPassword))
                {
                    returnValue.IsSucess = false;
                    returnValue.returnErrMessageCode = ReturnMessages.AppErrorCodes.UnknownError;
                    if (l_ValidateDomainUser(userLogonDomain))
                    {
                        returnValue = l_ValidateUserInAD(pUserID, pPassword, IPAddress, appID);
                        if (returnValue.IsSucess)
                        {
                        }
                        else
                        {
                            returnValue.returnErrMessageCode = ReturnMessages.AppErrorCodes.InvalidUserPwd;
                        }
                    }
                    else if (l_ValidateActiveDirectoryUser(pUserID))
                    {
                        returnValue = l_ValidateUserInAD(pUserID, pPassword, IPAddress, appID);
                        if (returnValue.IsSucess)
                        {
                        }
                        else
                        {
                            returnValue.returnErrMessageCode = ReturnMessages.AppErrorCodes.InvalidUserPwd;
                        }
                    }
                    else
                    {
                        //returnValue = l_ValidateUserInDB(pUserID, pPassword, IPAddress, appID);
                        if (returnValue.IsSucess)
                        {
                        }
                        else
                        {
                            returnValue.returnErrMessageCode = ReturnMessages.AppErrorCodes.InvalidUserPwd;
                        }
                    }
                }
                else
                {
                    returnValue.returnErrMessageCode = ReturnMessages.AppErrorCodes.UnauthorisedIPLocation;
                }
            }
            catch (Exception ex)
            {
            }
            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pPassword"></param>
        /// <param name="pIPAddress"></param>
        /// <returns></returns>
        protected UserProfiles l_ValidateUserInDB(string pUserID, string pPassword, string pIPAddress, string appID)
        {
            UserProfiles returnValue = new UserProfiles();
            try
            {
                return new App.DataLayer.AccountManager.AccountManagerDatabase().ValidateUser(pUserID, pPassword, pIPAddress, appID);
            }
            catch (Exception ex)
            {
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pPassword"></param>
        /// <param name="pIPAddress"></param>
        /// <returns></returns>
        protected UserProfiles l_ValidateUserInAD(string pUserID, string pPassword, string pIPAddress, Int32 appID)
        {
            UserProfiles returnValue = new UserProfiles();
            try
            {
                //return new App.DataLayer.AccountManager.AccountManagerActiveDirectory().ValidateUserFromAD(true, pUserID, pPassword, pIPAddress, appID);
                //bool status = new App.DataLayer.AccountManager.AccountManagerActiveDirectory().ValidateUserFromAD(true, pUserID, pPassword, pIPAddress, appID);
            }
            catch (Exception ex)
            {
            }
            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUserAddress"></param>
        /// <returns></returns>
        protected Boolean l_ValidateDomainUser(string pUserAddress)
        {
            string pwaDomainValue = System.Configuration.ConfigurationManager.AppSettings["PWADomain"];
            if (string.IsNullOrEmpty(pwaDomainValue))
                return false;
            if (pUserAddress.Contains(pwaDomainValue).Equals(true) && pUserAddress.IndexOf("\\") > 0)
            {
                string[] paramsLogin = pUserAddress.Split('\\');
                if (paramsLogin.Length > 0)
                {
                    if (paramsLogin.Length == 2 && String.IsNullOrEmpty(paramsLogin[1].ToString()) == false)
                    {
                        string domainName = paramsLogin[0].ToString();
                        if (string.Compare(domainName.ToUpper(), pUserAddress.ToUpper()) == 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            else
                return false;

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUserAddress"></param>
        /// <returns></returns>
        protected Boolean l_ValidateActiveDirectoryUser(string pUserAddress)
        {
            string pwaDomainValue = System.Configuration.ConfigurationManager.AppSettings["PWADomain"].ToUpper();
            if (string.IsNullOrEmpty(pwaDomainValue))
                return false;
            if (pUserAddress.ToUpper().IndexOf(pwaDomainValue) > -1)
                return true;
            else
                return false;
        }
        /// <summary>
        ///  This Method use for 
        /// </summary>
        /// <param name="pIPAddress"></param>
        /// <returns></returns>
        protected Boolean l_ValidateSourceIP(string pIPAddress)
        {
            Boolean returnValue = true;
            //to do restrict addresses to access
            return returnValue;
        }

        #endregion Validate User


        public List<string> getUserRoles(string pconfigField, string pUserID)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().getUserRoles(pconfigField, pUserID);
        }

        public string getUserDefaultProject(string pconfigField, string pUserID)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().getUserDefaultProject(pconfigField, pUserID);
        }

        public UserProfiles changePassword(string pOldPassword, string pNewPassword)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().changePassword(pOldPassword, pNewPassword);
        }

        public UserProfiles changeProfile(UserProfiles pUserData)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().changeProfile(pUserData);
        }

        public UserProfiles changeProfileByAdmin(UserProfiles pUserData)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().changeProfileByAdmin(pUserData);
        }

        public UserProfiles changeUserStatus(string pUserID, string pStatus)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().changeUserStatus(pUserID, pStatus);
        }

        public UserProfiles changeUserDefaultProject(string pUserID, string pProjectID)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().changeUserDefaultProject(pUserID, pProjectID);
        }

        public UserProfiles changeUserProjects(string pUserID, List<string> pProjectIDs)
        {
            return new App.DataLayer.AccountManager.AccountManagerDatabase().changeUserProjects(pUserID, pProjectIDs);
        }
    }
}
