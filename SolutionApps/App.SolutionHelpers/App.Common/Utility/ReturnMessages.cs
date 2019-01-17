using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Globalization;


namespace App.Common.Util
{

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:
    /// </summary>
    public class ReturnMessages : CommonUtility
    {
        /// <summary>
        /// Error Codes
        /// </summary>
        public enum AppErrorCodes
        {
            NoError,
            UnknownError,
            InvalidUserPwd,
            UnauthorisedIPLocation
        }
        /// </summary>
        public enum AppMessageCodes
        {
            NoMessage,
            SucessFullyComplete,
            SucessFullyLogin,
            Done
        }

        /// </summary>
        public enum LoggedInUserType
        {
            SYSAdmin,
            PWAUser,
            AgencyUser,
            CompanyUser,
            CommitteeUser,
            PublicUser,
            ValidUser,
            InvalidUser,
            None
        }

        public enum Severity
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,

            /// <summary>
            /// Low
            /// </summary>
            Low,

            /// <summary>
            /// High
            /// </summary>
            High,

            /// <summary>
            /// Fatal
            /// </summary>
            Fatal
        }
        public ReturnMessages()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="p_sucess"></param>
        public ReturnMessages(Boolean p_IsSucess)
        {
            returnMessageStringValue(p_IsSucess);
        }

        public ReturnMessages(Boolean p_IsSucess, AppMessageCodes p_messageCode)
        {
            returnMessageStringValue(p_IsSucess, p_messageCode);
        }

        public ReturnMessages(Boolean p_IsSucess, AppMessageCodes p_messageCode, AppErrorCodes p_errorCode, string p_Description)
        {
            returnMessageStringValue(p_IsSucess, p_messageCode, p_errorCode, p_Description);
        }

        
        //private ResourceManager res_man = new ResourceManager("GISAppCommonServices.Util.ErrorCodes", typeof(ReturnMessages).Assembly);
        private Boolean _IsSucess = false;
        private AppMessageCodes _messageCode = AppMessageCodes.NoMessage;
        private AppErrorCodes _errorCode = AppErrorCodes.NoError;
        private string _Description = string.Empty;
        private Exception _ExceptionMessage = null;

        public Boolean IsSucess { get { return _IsSucess; } set { _IsSucess = value; } }
        public AppMessageCodes returnMessageCode { get { return _messageCode; } set { _messageCode = value; } }
        public AppErrorCodes returnErrMessageCode { get { return _errorCode; } set { _errorCode = value; } }
        public Exception ExceptionMessage { get { return _ExceptionMessage; } set { _ExceptionMessage = value; } }


        private LoggedInUserType _loggedInUserType = LoggedInUserType.None;


        public LoggedInUserType getLoggedInUserType { get { return _loggedInUserType; } set { _loggedInUserType = value; } }


        public string returnMessage
        {
            get
            {
                return getErrMessage(_errorCode);
            }
        }

        public AppErrorCodes errorCode { get { return _errorCode; } }

        public string errorMessage
        {
            get
            {
                return "sss";
            }
        }

        public string Description { get { return _Description; } set { _Description = value; } }
        public object OtherDetails { get { return null; } }


        protected void returnMessageStringValue(Boolean p_IsSucess, AppMessageCodes p_messageCode = AppMessageCodes.NoMessage, AppErrorCodes p_errorCode = AppErrorCodes.NoError, string p_Description = "No Description!")
        {
            //GISApp.GISAppCommonServices.Util.ErrorCodes.ResourceManager.GetString("asd");
            _IsSucess = p_IsSucess;
            _messageCode = p_messageCode;
            _errorCode = p_errorCode;
            _Description = p_Description;
        }

        public string getErrMessage(AppErrorCodes p_errorCode)
        {
            string message = string.Empty;
            //GISApp.GISAppCommonServices.Properties.GISAppMessages.Culture = new CultureInfo("de-DE");
            // message = GISApp.GISAppCommonServices.Properties.GISAppMessages.Message1;
            //message = App.Common.Properties.GISAppErrMessages.ResourceManager.GetString(p_errorCode.ToString());
            message = App.Common.Resources.ErrorMessages.ResourceManager.GetString(p_errorCode.ToString());
            return message;
        }

        public string getMessage(AppMessageCodes p_messageCode)
        {
            string message = string.Empty;
            message = App.Common.Resources.ErrorMessages.ResourceManager.GetString(p_messageCode.ToString());
            return message;
        }
    }
}
