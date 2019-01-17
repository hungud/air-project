using System;
using System.Collections.Generic;
using System.Linq;
using Utililty = App.Common.CommonUtility;
namespace App.Models
{
    #region Models
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RoleId { get; set; }
        public List<Int64?> Roles { get; set; }
    }
    public class PaginationDataModels : App.Common.CommonUtility
    {
        // Summary: One-based index of the first item in the paged subset.
        //     
        public int FirstItemOnPage { get; set; }
        //
        // Summary: Returns true if this is NOT the last subset within the superset.
        //     
        public bool HasNextPage { get; set; }
        //
        // Summary: Returns true if this is NOT the first subset within the superset.
        //     
        public bool HasPreviousPage { get; set; }
        //
        // Summary: Returns true if this is the first subset within the superset.
        //     
        public bool IsFirstPage { get; set; }
        //
        // Summary: Returns true if this is the last subset within the superset.
        //     
        public bool IsLastPage { get; set; }
        //
        // Summary: One-based index of the last item in the paged subset.
        //     
        public int LastItemOnPage { get; set; }
        //
        // Summary: Total number of subsets within the superset.
        //     
        public int PageCount { get; set; }
        //
        // Summary: Maximum size any individual subset.
        //     
        public int PageSize { get; set; }
        //
        // Summary: Total number of objects contained within the superset.
        //     
        public int TotalItemCount { get; set; }
        //
        // Summary: Total number of objects contained within the superset.
        //     
        public string SortDirection { get; set; }
        //
        // Summary: Total number of objects contained within the superset.
        //     
        public string SortExpression { get; set; }
        //
        // Summary: Total number of objects contained within the superset.
        //
        public string QeryCondition { get; set; }
        //
        // Summary: Total number of objects contained within the superset.
        //
        public List<string> QeryConditions { get; set; }
    }
    public class ServiceReqModels 
    {
        public ServiceReqModels()
        {

        }
        public ServiceReqModels(string appAccessToken, string appServiceURI,List<string> qeryConditions=null)
        {
            this.AppAccessToken = appAccessToken;
            this.AppServiceURI = appServiceURI;
            this.QeryConditions = qeryConditions;
        }
        public string AppAccessToken { get; set; }
        public string AppServiceURI { get; set; }
        public List<string> QeryConditions { get; set; }

    }

    public class AirportData
    {
        public string country { get; set; }
        public string city { get; set; }
        public string dst { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
        public string name { get; set; }
        public string timezone { get; set; }
        public string latitude { get; set; }
        public string altitude { get; set; }
    }

    #region CommonService
    public class CommonService
    {
        public class Airline
        {
            public dynamic id { get; set; }
            public dynamic value { get; set; }
            public dynamic airlinecode { get; set; }
            public dynamic airlinename { get; set; }
            public dynamic country { get; set; }
            public dynamic city { get; set; }
            public dynamic iata { get; set; }
            public dynamic airportname { get; set; }
        }
    }
    #endregion CommonService

    #region AdventureWorks2008R2
    public class ProductionProduct : UserInfo
    {
        public string ProductID { get; set; }
    }
    public class AdventureWorks2008R2ProductionProduct
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string MakeFlag { get; set; }
        public string FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public string SafetyStockLevel { get; set; }
        public string ReorderPoint { get; set; }
        public string StandardCost { get; set; }
        public string ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public string Weight { get; set; }
        public string DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public string ProductSubcategoryID { get; set; }
        public string ProductModelID { get; set; }
        public string SellStartDate { get; set; }
        public string SellEndDate { get; set; }
        public string DiscontinuedDate { get; set; }
        public string rowguid { get; set; }
        public string ModifiedDate { get; set; }

    }
    #endregion AdventureWorks2008R2

    public class MVVMModels : App.Common.CommonUtility
    {
        public List<Models> allModels { get; set; }
        public List<ModelsOne> allModelsOne { get; set; }
        public List<ModelsTwo> allModelsTwo { get; set; }
    }
    public class MVVMModelsList : List<MVVMModels>
    {
        public List<MVVMModels> myBusinessObject { get; set; }
        public MVVMModelsList()
        {
            GetData();
        }
        private void GetData()
        {
            try
            {
                myBusinessObject = new List<MVVMModels>();
                myBusinessObject.Add(new MVVMModels()
                {
                    allModels = (List<Models>)new ModelsList().myBusinessObject,
                    allModelsOne = (List<ModelsOne>)new ModelsOneList().myBusinessOneObject,
                    allModelsTwo = (List<ModelsTwo>)new ModelsTwoList().myBusinessTwoObject,
                    Data = new ModelsTwoList().myBusinessTwoObject
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Models.MVVMModelsList", ex);
            }
        }
    }

    #region Models
    public class Models : App.Common.CommonUtility
    {
        public int USERID { get; set; }
        public string Token { get; set; }
        public string RoleId { get; set; }
        public List<int> RoleIds { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public string LOGINID { get; set; }
        public string PASSWPRD { get; set; }
        public string EMAILADDRESS { get; set; }
        public string FACEBOOKID { get; set; }
        public string JOBTITLE { get; set; }
        public Boolean EMAILVERIFIED { get; set; }
        public Boolean STATUS { get; set; }
        public string LOGINDATE { get; set; }
        public string ReqestURL { get; set; }

    }

   
    public class ModelsList : List<Models>
    {
        public List<Models> myBusinessObject { get; set; }
        public ModelsList()
        {
            GetData();
        }
        public ModelsList(App.Models.PaginationDataModels QeryCondition)
        {
            GetData(QeryCondition);
        }
        private void GetData()
        {
            myBusinessObject = new List<Models>();
            for (int i = 11; i < 111; i++)
            {
                myBusinessObject.Add(new Models()
                {
                    USERID = (10 + i),
                    FIRSTNAME = "RAKESH" + i.ToString(),
                    MIDDLENAME = "PAL" + i.ToString(),
                    LASTNAME = "RMS" + i.ToString(),
                    MOBILE = "9910523245" + i.ToString(),
                    PHONE = "9910523245" + i.ToString(),
                    ADDRESS = "DELHI" + i.ToString(),
                    LOGINID = "RMS" + i.ToString(),
                    PASSWPRD = "RMS" + i.ToString(),
                    EMAILADDRESS = "RMS" + i.ToString(),
                    FACEBOOKID = "RMS" + i.ToString(),
                    JOBTITLE = "RMS" + i.ToString(),
                    EMAILVERIFIED = false,
                    STATUS = false,
                    LOGINDATE = "02/02/2015:" + i.ToString()
                });
            }
        }
        private void GetData(App.Models.PaginationDataModels QeryCondition)
        {
            myBusinessObject = new List<Models>();
            for (int i = (QeryCondition.PageCount * QeryCondition.PageSize); i < ((QeryCondition.PageCount * QeryCondition.PageSize) + QeryCondition.PageSize); i++)
            {
                myBusinessObject.Add(new Models()
                {
                    USERID = i,
                    FIRSTNAME = "RAKESH" + i.ToString(),
                    MIDDLENAME = "PAL" + i.ToString(),
                    LASTNAME = "RMS" + i.ToString(),
                    MOBILE = "9910523245" + i.ToString(),
                    PHONE = "9910523245" + i.ToString(),
                    ADDRESS = "DELHI" + i.ToString(),
                    LOGINID = "RMS" + i.ToString(),
                    PASSWPRD = "RMS" + i.ToString(),
                    EMAILADDRESS = "RMS" + i.ToString(),
                    FACEBOOKID = "RMS" + i.ToString(),
                    JOBTITLE = "RMS" + i.ToString(),
                    EMAILVERIFIED = false,
                    STATUS = false,
                    LOGINDATE = "02/02/2015:" + i.ToString()
                });
            }
        }
    }
    #endregion Models

    #region ModelsOne
    public class ModelsOne : App.Common.CommonUtility
    {
        public int USERID { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public string LOGINID { get; set; }
        public string PASSWPRD { get; set; }
        public string EMAILADDRESS { get; set; }
        public string FACEBOOKID { get; set; }
        public string JOBTITLE { get; set; }
        public Boolean EMAILVERIFIED { get; set; }
        public Boolean STATUS { get; set; }
        public string LOGINDATE { get; set; }

    }
    public class ModelsOneList : List<ModelsOne>
    {
        public List<ModelsOne> myBusinessOneObject { get; set; }
        public ModelsOneList()
        {
            GetData();
        }

        private void GetData()
        {
            myBusinessOneObject = new List<ModelsOne>();
            for (int i = 11; i < 1111; i++)
            {
                myBusinessOneObject.Add(new ModelsOne()
                {
                    USERID = (10 + i),
                    FIRSTNAME = "RAKESH" + i.ToString(),
                    MIDDLENAME = "KUMAR" + i.ToString(),
                    LASTNAME = "RMS" + i.ToString(),
                    MOBILE = "9910523245" + i.ToString(),
                    PHONE = "9910523245" + i.ToString(),
                    ADDRESS = "DELHI" + i.ToString(),
                    LOGINID = "RMS" + i.ToString(),
                    PASSWPRD = "RMS" + i.ToString(),
                    EMAILADDRESS = "RMS" + i.ToString(),
                    FACEBOOKID = "RMS" + i.ToString(),
                    JOBTITLE = "RMS" + i.ToString(),
                    EMAILVERIFIED = false,
                    STATUS = false,
                    LOGINDATE = "02/02/2015:" + i.ToString()
                });
            }
        }
    }
    #endregion ModelsOne

    #region ModelsTwo
    public class ModelsTwo : App.Common.CommonUtility
    {
        public int USERID { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public string LOGINID { get; set; }
        public string PASSWPRD { get; set; }
        public string EMAILADDRESS { get; set; }
        public string FACEBOOKID { get; set; }
        public string JOBTITLE { get; set; }
        public Boolean EMAILVERIFIED { get; set; }
        public Boolean STATUS { get; set; }
        public string LOGINDATE { get; set; }

    }
    public class ModelsTwoList : List<ModelsTwo>
    {
        public List<ModelsTwo> myBusinessTwoObject { get; set; }
        public ModelsTwoList()
        {
            GetData();
        }

        private void GetData()
        {
            myBusinessTwoObject = new List<ModelsTwo>();
            for (int i = 11; i < 1111; i++)
            {
                myBusinessTwoObject.Add(new ModelsTwo()
                {
                    USERID = (10 + i),
                    FIRSTNAME = "RAKESH" + i.ToString(),
                    MIDDLENAME = "KUAMAR" + i.ToString(),
                    LASTNAME = "PAL" + i.ToString(),
                    MOBILE = "9910523245" + i.ToString(),
                    PHONE = "9910523245" + i.ToString(),
                    ADDRESS = "DELHI" + i.ToString(),
                    LOGINID = "RMS" + i.ToString(),
                    PASSWPRD = "RMS" + i.ToString(),
                    EMAILADDRESS = "RMS" + i.ToString(),
                    FACEBOOKID = "RMS" + i.ToString(),
                    JOBTITLE = "RMS" + i.ToString(),
                    EMAILVERIFIED = false,
                    STATUS = false,
                    LOGINDATE = "02/02/2015:" + i.ToString()
                });
            }
        }
    }
    #endregion ModelsTwo

    #region UserPerson
    public class UserPerson : App.Common.CommonUtility
    {
        public int Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Fullname { get; set; }
        public String Profession { get; set; }
        public int Age { get; set; }
    }
    public class UserPersonList : List<UserPerson>
    {
        public List<UserPerson> myUserPersonObject { get; set; }
        public UserPersonList()
        {
            GetData();
        }
        public List<UserPerson> SetData(App.Models.UserPerson User_Person)
        {
            myUserPersonObject.Add(new UserPerson()
            {
                Id = (111+1),
                Firstname = User_Person.Firstname,
                Lastname = User_Person.Lastname,
                Fullname = User_Person.Fullname,
                Profession = User_Person.Profession,
                Age = User_Person.Age
            });
            return myUserPersonObject;
        }
        private void GetData()
        {
            myUserPersonObject = new List<UserPerson>();
            for (int i = 11; i < 111; i++)
            {
                myUserPersonObject.Add(new UserPerson()
                {
                    Id = (10 + i),
                    Firstname = "RAKESH" + i.ToString(),
                    Lastname = "PAL" + i.ToString(),
                    Fullname = "RAKESH PAL" + i.ToString(),
                    Profession = "Project Manager" + i.ToString(),
                    Age = (18 + i)
                });
            }
        }
        public UserPerson GetData(Int32 U_ID)
        {
            UserPerson UPS = new UserPerson();
            foreach (var User_Person in myUserPersonObject.Where(mm => mm.Id == U_ID).ToList())
            {
                UPS = new UserPerson()
                {
                    Id = User_Person.Id,
                    Firstname = User_Person.Firstname,
                    Lastname = User_Person.Lastname,
                    Fullname = User_Person.Fullname,
                    Profession = User_Person.Profession,
                    Age = User_Person.Age
                };
            }
            return UPS;
        }
        public List<UserPerson> RemoveData(Int32 U_ID)
        {
            return myUserPersonObject;
        }
    }
    #endregion UserPerson

    public partial class BPDHCDMS
    {
        public class BPDHCDMSALLComments : Utililty
        {
            public List<APP_ROLE> allAPPROLE { get; set; }
            public List<APP_USER> allAPPUSER { get; set; }
            public List<APP_USERS_ROLES> allAPPUSERSROLES { get; set; }
            public List<HC_Request> allHCRequest { get; set; }
            public List<Pre_Survey_Request> allPreSurveyRequest { get; set; }
            public List<Survey_Data> allSurveyData { get; set; }
        }
    }
    public partial class BPDHCDMS
    {
        public class DeviceInformation
        {
            public string DeviceName { get; set; }
            public string DeviceVersion { get; set; }
            public string Device { get; set; }
        }
        public class LoginInfo
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string DeviceName { get; set; }
            public string DeviceVersion { get; set; }
            public string DeviceModel { get; set; }
            public DeviceInformation DeviceInfo { get; set; }
        }
        public class APP_ROLE
        {
            public int ROLEID { get; set; }
            public string ROLENAME { get; set; }
            public string DESCRIPTION { get; set; }
        }
        public class APP_USER
        {
            public int USERID { get; set; }
            public string QID { get; set; }
            public string USERNAME { get; set; }
            public string FIRSTNAME { get; set; }
            public string MIDDLENAME { get; set; }
            public string LASTNAME { get; set; }
            public string PWD { get; set; }
            public string EMAILID { get; set; }
            public string LOGINDATE { get; set; }
            public string LOGINIP { get; set; }
            public string STATUS { get; set; }
            public DateTime CREATEDATE { get; set; }
        }
        public class APP_USERS_ROLES
        {
            public int USER_ROLE_ID { get; set; }
            public string ROLEID { get; set; }
            public string USERID { get; set; }
        }
        public class HC_Request
        {
            public int App_ID { get; set; }
            public int QID { get; set; }
            public string OwnerName { get; set; }
            public int ContactNo { get; set; }
            public int Pin { get; set; }
            public string AppDate { get; set; }
            public string Area { get; set; }
            public string Zone { get; set; }
            public string City { get; set; }
            public string StreetName { get; set; }
            public int StreetNumber { get; set; }
        }
        public class Pre_Survey_Request
        {
            public int PS_ID { get; set; }
            public int App_ID { get; set; }
            public int Status { get; set; }
            public string Comments { get; set; }
            public int AssignedTo { get; set; }
        }
        public class Survey_Data
        {
            public int SP_ID { get; set; }
            public int APP_ID { get; set; }
            public string File_Path { get; set; }
            public string UpdateOn { get; set; }
            public string Lat { get; set; }
            public int Long { get; set; }
        }

    }

    public class PaymentModal
    {
        public long Id { get; set; }
        public string CardType { get; set; }
        public string CardFee { get; set; }
        public bool IsActive { get; set; }
        public string CardValue { get; set; }


    }

    #endregion Models


    #region UserRegister With XML Serializable
    [Serializable()]
    public class UserRegister
    {
        //XmlDataString = ValidateUserDetails.Serialize(UserDetails);
        public UserRegister()
        {
                
        }
        public UserRegister(String username, String password, int employeeid, String emailid, long phoneno, long mobileno)
        {
            UserName = username;
            UserPassWord = password;
            EmployeeID = employeeid;
            EMailID = emailid;
            PhoneNo = phoneno;
            MobileNo = mobileno;
        }
        public String UserName { get; set; }
        public String UserPassWord { get; set; }
        public String UserOldPassWord { get; set; }
        public int EmployeeID { get; set; }
        public String EMailID { get; set; }
        public long PhoneNo { get; set; }
        public long MobileNo { get; set; }
        public String ActionType { get; set; }
        public String ActionData { get; set; }
        public String XmlDataString { get; set; }
        public string Remarks { get; set; }


        #region Serialization
        /// <summary>
        /// Use generic XML serializer class
        /// </summary>
        private static App.Common.XMLSerializer<UserRegister> _serializer = new App.Common.XMLSerializer<UserRegister>();

        /// <summary>
        /// Convert address into XML
        /// </summary>
        /// <param name="myobject"></param>
        /// <returns></returns>
        public static String Serialize(UserRegister myobject)
        {
            return _serializer.Serialize(myobject);
        }

        /// <summary>
        /// Convert XML into Address
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static UserRegister Deserialize(String xml)
        {
            return _serializer.Deserialize(xml);
        }

        #endregion
        #region Non-serialized properties

        /// <summary>
        /// Convert address to HTML string
        /// </summary>
        public String DetailsAsHTML
        {
            get
            {
                System.Text.StringBuilder result = new System.Text.StringBuilder();
                result.Append(ToHTML(UserName));
                result.Append(ToHTML(UserPassWord));
                result.Append(ToHTML(EmployeeID.ToString()));
                result.Append(ToHTML(EMailID));
                result.Append(ToHTML(PhoneNo.ToString()));
                result.Append(ToHTML(MobileNo.ToString()));
                return result.ToString();
            }
        }

        private String ToHTML(String value)
        {
            if (String.IsNullOrEmpty(value))
            { return ""; }
            else
            { return String.Format("{0}<br/>", System.Web.HttpUtility.HtmlEncode(value)); }
        }

        #endregion

        #region XML XQueries

        /// <summary>
        /// How to query for a Town
        /// </summary>
        /// <remarks>
        /// This is located in the Address class as we may change 
        /// the Address class in future, and would not want to
        /// hard-code this into our other classes.
        /// </remarks>
        public static String XQueryTown()
        {
            return "'(/Address/Town)[1]', 'varchar(1000)'";
        }

        #endregion

    }

    [Serializable()]
    public class UserRegisterList : List<UserRegister>
    {

        /// <summary>
        /// This function returns the list of users who has leads contain limit.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<UserRegister> UserHaveLeadsGetLists(int param)
        {
            return this.FindAll(
                //implement an anonymous delegate that uses a User class 
                //as its parameter and then checks to see if the the items 
                //in our class is equal to that cuteness factor.
               delegate(UserRegister oUser)
               {
                   return oUser.EmployeeID > param;
               });
        }
        public List<UserRegister> UserHaveLeadsGetLists(string param)
        {
            return this.FindAll(
                //implement an anonymous delegate that uses a User class 
                //as its parameter and then checks to see if the the items 
                //in our class is equal to that cuteness factor.
               delegate(UserRegister oUser)
               {
                   return oUser.UserName.Equals(param);
               });
        }
    }
    #endregion UserRegister  With XML Serializable
}
