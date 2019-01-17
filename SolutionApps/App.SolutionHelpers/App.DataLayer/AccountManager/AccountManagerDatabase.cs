using App.Models.AccountManager;
using System;
using System.Collections.Generic;
using System.Globalization;
namespace App.DataLayer.AccountManager
{
    public class AccountManagerDatabase
    {
        public AccountManagerDatabase()
        {
        }

        public AccountManagerDatabase(CultureInfo p_ci)
        {

        }


        public UserProfiles ValidateUser(string emailAddress, string ePassword, string IPAddress, string appID)
        {
            string lmsg = string.Empty;
            UserProfiles returnValue = new UserProfiles();
            return returnValue;
        }

        public List<string> getUserRoles(string pconfigField, string pUserID)
        {
            List<string> _roles = new List<string>();
            try
            {
            }
            catch (Exception ex)
            {
            }

            return _roles;
        }

        public string getUserDefaultProject(string pconfigField, string pUserID)
        {
            string returnProject = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
            }

            return returnProject;
        }

        public UserProfiles changePassword(string pOldPassword, string pNewPassword)
        {
            UserProfiles returnValue = new UserProfiles();

            return returnValue;
        }

        public UserProfiles changeProfile(UserProfiles pUserData)
        {
            UserProfiles returnValue = new UserProfiles();

            return returnValue;
        }

        public UserProfiles changeProfileByAdmin(UserProfiles pUserData)
        {
            UserProfiles returnValue = new UserProfiles();
            try
            {

                returnValue.IsSucess = true;
            }
            catch (Exception ex)
            {
            }
            return returnValue;
        }

        public UserProfiles changeUserStatus(string pUserID, string pStatus)
        {
            UserProfiles returnValue = new UserProfiles();
            return returnValue;
        }

        public UserProfiles changeUserDefaultProject(string pUserID, string pProjectID)
        {
            UserProfiles returnValue = new UserProfiles();
            return returnValue;
        }

        public UserProfiles changeUserProjects(string pUserID, List<string> pProjectIDs)
        {
            UserProfiles returnValue = new UserProfiles();
            return returnValue;
        }

    }
}
