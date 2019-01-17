
using System.Collections.Generic;

namespace App.Common
{

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:
    /// </summary>
    public class CommonUtility : DataTraveler
    {
        public CommonUtility() { }
        public dynamic Data { get; set; }
        public dynamic Data1 { get; set; }
        public string AgencyDetails { get; set; }
        public string CCFEE { get; set; }
        public string PolicyNumber { get; set; }
        public string PurchaseDate { get; set; }
        public string Price { get; set; }
        //public dynamic Data2 { get; set; }
        //public dynamic Data3 { get; set; }
        //public dynamic Data4 { get; set; }
        //public dynamic Data5 { get; set; }
    }
    public class BookingDetailsRQModal
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PCC { get; set; }
        public string PNR { get; set; }
    }
    public class CreateCardAndOtherDetails
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CVVNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public string NameOnCard { get; set; }
        public string AirLineCode { get; set; }
        public string CurrCode { get; set; }
        public string PaymentAmount { get; set; }
        public string Address { get; set; }
        public string CCFee { get; set; }

    }
    public class CCAuthorizeUtility : DataTraveler
    {
        public CCAuthorizeUtility() { }
        public string Locator { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseStatus { get; set; }
    }


    public class TravelerUtility
    {
        public TravelerUtility()
        {
        }
        public dynamic UserInfo { get; set; }
        public dynamic ConfigurationInfo { get; set; }
        public dynamic Data { get; set; }
        public dynamic Data1 { get; set; }
        //public dynamic Data2 { get; set; }
        //public dynamic Data3 { get; set; }
        //public dynamic Data4 { get; set; }
        //public dynamic Data5 { get; set; }
    }
    public class DataTraveler
    {
        public dynamic Information { get; set; }
        public string ActionType { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        //public string Message1 { get; set; }
        //public string Message2 { get; set; }
        //public string Message3 { get; set; }
        //public string Message4 { get; set; }
        //public string Message5 { get; set; }
        public bool Status { get; set; }

        //public MessageType App_MessageType { get; set; }
        //public ActionStatusType App_ActionStatusType { get; set; }
        public enum MessageType
        {
            Action = 0,
            Status = 1,
            Success = 2,
            Failure = 3,
            Exception = 4,
            Message = 5,
            Validation = 6
        }
        public enum ActionStatusType
        {
            Select,
            Insert,
            Update,
            Delete,
            Submit,
            Cancel,
            Success,
            UnSuccess,
            Error
        }
        
        #region Error Code
        public const string HasNotRole = "1001";
        public const string HaveNotAccess = "1002";
        #endregion

        #region Common Pages
        public const string AdminDashBoard = @"~/AdminDashBoard.aspx";
        public const string UserDashBoard = @"~/UserDashBoard.aspx";
        public const string ErrorPage = @"~/Error.aspx";
        public const string LoginPage = @"~/login_default.aspx";
        public const string MyLead = @"~/Call/MyLead.aspx";
        public const string Campaign = "Campaign";
        public const string Calling = "Calling";
        #endregion
        
        /// <summary>
        /// For SaveToolbar User Control..
        /// </summary>
        public enum ModeType
        {
            None = 0, AddNew = 1, Modify = 2, Both = 3, OnlySave = 4
        }
        
        /// <summary>
        /// This enum is used to explain the parameter value return from procedure
        /// </summary>
        public enum DatabaseMessage
        {
            Success = 1,
            SystemFailure = -1,
            Duplicate = -2
        }

        /// <summary>
        /// This enum is used to explain the question type avialable in the system.
        /// </summary>
        public enum QuestionType
        {
            MultipleChoice = 1,
            Single = 2,
            FreeFormText = 3,
            YesOrNo = 4,
            MixType = 5
        }

        /// <summary>
        /// This enum is used to explain the allocation type avialable in the system.
        /// </summary>
        public enum AllocationType
        {
            Manually = 1,
            Auto = 2,
            Make = 3
        }

        /// <summary>
        /// This enum is used to explain the allocation strategy avialable in the system.
        /// </summary>
        public enum AllocationStrategy
        {
            Random = 1,
            RoundRobin = 2,
            Default = 3
        }

        /// <summary>
        /// This enum is used to explain user login success or failure status
        /// </summary>
        public enum LoginStatus
        {
            Success = 1,
            Failure = 2
        }

        public enum FetchColumn
        {
            CustName = 0, CustNo = 1, AppNo = 2, AppDate = 3, ProjectName = 4, UnitNo = 5,
            Address = 6, Country = 7, State = 8, City = 9, AppID = 11, CustID = 12, UnitID = 13,
            ReceiptID = 14, ReceiptNo = 15
        }
    }
    public class RequestAction
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Token { get; set; }
        public string UserQuery { get; set; }
        public App.Common.ActionType Action { get; set; }
    }
    public enum ActionType
    {
        Administrator = 1,
        SrManagers = 2,
        AshghalRoadDrainageEngineer = 3,
        PreliminarySurveyor = 4,
        FinalSurveyor = 5,
        QSTeam = 6,
        SectionHead = 7,
        Contractor = 8,
        User = 9,
        None = 10
    }


    public interface IDaobase<T>
    {
        void Save(T entity);
        void Update(T entity);
        void Merge(T entity);
        void Delete(T entity);
        T Get(object id);
        T Load(object id);
    }
}
