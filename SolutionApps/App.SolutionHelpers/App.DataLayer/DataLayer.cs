using System;
using System.Collections.Generic;
using App.Models;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace App.DataLayer
{
    public partial class DataLayer
    {
        public DataLayer()
        {
        }

        public String GetMarkUpService()
        {

            try
            {

                DataSet dsBankDetails = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                using (DbCommand cmd = db.GetSqlStringCommand("Select NetfareMarkup,PublishedFareMarkup From [QA_CommonDB].[dbo].[Markups] where PublishedMarkupType = 'value' and CompanyID =2"))
                {
                    dsBankDetails = db.ExecuteDataSet(cmd);
                }
                if (dsBankDetails != null)
                {


                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.GetCommonService", ex);
            }
            return "";
        }
        public String GetAirlineRestrictionAirService()
        {
            try
            {
                DataSet dsBankDetails = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                using (DbCommand cmd = db.GetSqlStringCommand("Select * From [CommonDB].[dbo].[AirlineRestrictionAir]"))
                {
                    dsBankDetails = db.ExecuteDataSet(cmd);
                }
                if (dsBankDetails != null)
                {

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.GetCommonService", ex);
            }
            return "";
        }
        public App.Common.CommonUtility GetCommonService(string CommonServiceType, string SearchText)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
              
                //SetGetXMLSerializer();
                if (!string.IsNullOrEmpty(CommonServiceType))
                {
                    DataSet dsBankDetails = new DataSet();
                    Database db = DatabaseFactory.CreateDatabase();
                    using (DbCommand cmd = db.GetStoredProcCommand("SP_GetCommonService"))
                    {
                        db.AddInParameter(cmd, "@CommonServiceType", DbType.String, CommonServiceType);
                        db.AddInParameter(cmd, "@SearchText", DbType.String, SearchText);
                        dsBankDetails = db.ExecuteDataSet(cmd);
                    }
                    if (dsBankDetails != null)
                    {
                        List<CommonService.Airline> Airline_List = new List<CommonService.Airline>();
                        List<CommonService.Airline> Airline_List1 = new List<CommonService.Airline>();
                        switch (CommonServiceType)
                        {
                            case "AirlinesListDetails":
                                Airline_List = new List<CommonService.Airline>();
                                foreach (DataRow item in dsBankDetails.Tables[0].Rows)
                                {
                                    Airline_List.Add(new CommonService.Airline()
                                    {
                                        airlinecode = item["airlinecode"],
                                        airlinename = item["airlinename"],
                                        country = item["country"].ToString()
                                    });
                                }
                                foreach (DataRow item in dsBankDetails.Tables[1].Rows)
                                {
                                    Airline_List1.Add(new CommonService.Airline()
                                    {
                                        airlinecode = item["ID"],
                                        airlinename = item["Value"],
                                        iata = item["iata"],
                                        city = item["city"],
                                        airportname = item["name"]
                                    });
                                }
                                Utility = new Common.CommonUtility() { Data = Airline_List, Data1 = Airline_List1, Status = true, Message = "Get CommonService Success Fully.", ErrorCode = "1" };
                                break;
                            case "AirlinesCodeSearch":
                                Airline_List = new List<CommonService.Airline>();
                                foreach (DataRow item in dsBankDetails.Tables[0].Rows)
                                {
                                    Airline_List.Add(new CommonService.Airline()
                                    {
                                        airlinecode = item["ID"],
                                        airlinename = item["Value"]
                                    });
                                }
                                Utility = new Common.CommonUtility() { Data = Airline_List, Status = true, Message = "Get CommonService Success Fully.", ErrorCode = "1" };

                                break;
                            case "AirlinesPaymentsCitySearch":
                                Airline_List = new List<CommonService.Airline>();
                                foreach (DataRow item in dsBankDetails.Tables[0].Rows)
                                {
                                    Airline_List.Add(new CommonService.Airline()
                                    {
                                        airlinecode = item["ID"],
                                        airlinename = item["Value"]
                                    });
                                }
                                Utility = new Common.CommonUtility() { Data = Airline_List, Status = true, Message = "Get CommonService Success Fully.", ErrorCode = "1" };

                                break;
                            case "AirlinesListDetails11":
                                Console.WriteLine("AirlinesListDetails11");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.GetCommonService", ex);
            }
            return Utility;
        }

        public String GetAirport_AirlineService(string Code, string SearchType)
        {
            String Utility = "";
            try
            {
                string SearchText  = "SearchText";
                string CommonServiceType = "AirlinesListDetails";
          
                if (!string.IsNullOrEmpty(CommonServiceType))
                {
                    DataSet dsBankDetails = new DataSet();
                    Database db = DatabaseFactory.CreateDatabase();
                    using (DbCommand cmd = db.GetStoredProcCommand("SP_GetCommonService"))
                    {
                        db.AddInParameter(cmd, "@CommonServiceType", DbType.String, CommonServiceType);
                        db.AddInParameter(cmd, "@SearchText", DbType.String, SearchText);
                        dsBankDetails = db.ExecuteDataSet(cmd);
                    }
                    if (dsBankDetails != null)
                    {
                        List<CommonService.Airline> Airline_List = new List<CommonService.Airline>();
                        List<CommonService.Airline> Airline_List1 = new List<CommonService.Airline>();
                        if (SearchType == "Airline")
                        {
                            foreach (DataRow item in dsBankDetails.Tables[0].Rows)
                            {
                                Airline_List.Add(new CommonService.Airline()
                                {
                                    airlinecode = item["airlinecode"],
                                    airlinename = item["airlinename"],
                                });
                                if (item["airlinecode"].ToString() == Code)
                                    Utility = item["airlinename"].ToString();

                            }
                        }
                        if (SearchType == "Airport")
                        {
                            foreach (DataRow item in dsBankDetails.Tables[1].Rows)
                            {
                                Airline_List1.Add(new CommonService.Airline()
                                {
                                    airlinecode = item["ID"],
                                    airlinename = item["Value"]
                                });
                                if (item["ID"].ToString() == Code)
                                    Utility = item["Value"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.GetCommonService", ex);
            }
            return Utility;
        }
        public App.Common.CommonUtility GetAdventureWorksProductionProduct(App.Models.ProductionProduct Product_ID)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                //SetGetXMLSerializer();
                if (!string.IsNullOrEmpty(Product_ID.ProductID))
                {
                    List<App.Models.AdventureWorks2008R2ProductionProduct> Production_Product = new List<App.Models.AdventureWorks2008R2ProductionProduct>();
                    DataSet dsBankDetails = new DataSet();
                    Database db = DatabaseFactory.CreateDatabase();
                    using (DbCommand cmd = db.GetStoredProcCommand("SP_GetProductDetails"))
                    {
                        db.AddInParameter(cmd, "@ProductID", DbType.String, Convert.ToInt16(Product_ID.ProductID));
                        dsBankDetails = db.ExecuteDataSet(cmd);

                    }
                    if (dsBankDetails != null)
                    {
                        foreach (DataRow item in dsBankDetails.Tables[0].Rows)
                        {
                            Production_Product.Add(new AdventureWorks2008R2ProductionProduct()
                            {
                                ProductID = item["ProductID"].ToString(),
                                Name = item["Name"].ToString(),
                                ProductNumber = item["ProductNumber"].ToString(),
                                MakeFlag = item["MakeFlag"].ToString(),
                                FinishedGoodsFlag = item["FinishedGoodsFlag"].ToString(),
                                Color = item["Color"].ToString(),
                                SafetyStockLevel = item["SafetyStockLevel"].ToString(),
                                ReorderPoint = item["ReorderPoint"].ToString(),
                                StandardCost = item["StandardCost"].ToString(),
                                ListPrice = item["ListPrice"].ToString(),
                                Size = item["Size"].ToString(),
                                SizeUnitMeasureCode = item["SizeUnitMeasureCode"].ToString(),
                                WeightUnitMeasureCode = item["WeightUnitMeasureCode"].ToString(),
                                Weight = item["Weight"].ToString(),
                                DaysToManufacture = item["DaysToManufacture"].ToString(),
                                ProductLine = item["ProductLine"].ToString(),
                                Class = item["Class"].ToString(),
                                Style = item["Style"].ToString(),
                                ProductSubcategoryID = item["ProductSubcategoryID"].ToString(),
                                ProductModelID = item["ProductModelID"].ToString(),
                                SellStartDate = item["SellStartDate"].ToString(),
                                SellEndDate = item["SellEndDate"].ToString(),
                                DiscontinuedDate = item["DiscontinuedDate"].ToString(),
                                rowguid = item["rowguid"].ToString(),
                                ModifiedDate = item["ModifiedDate"].ToString()
                            });
                        }

                        dynamic DataTableToModel = new App.Common.CommonDataTable().DataTableToJSONWithJavaScriptSerializer(dsBankDetails.Tables[0]);
                        //List<AdventureWorks2008R2ProductionProduct> Production_Product_Details = new List<AdventureWorks2008R2ProductionProduct>();
                        //Production_Product_Details = App.Common.Helper.ConvertDataTable<AdventureWorks2008R2ProductionProduct>(dsBankDetails.Tables[0]);  
                        Utility = new Common.CommonUtility() { Data = Production_Product, Data1 = DataTableToModel, Status = false, Message = "Invalid Credential.", ErrorCode = "113" };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.GetAdventureWorksProductionProduct", ex);
            }
            return Utility;
        }
        public App.Common.CommonUtility SetAirPortData(List<App.Models.AirportData> AirPort_Product)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                if (AirPort_Product.Count > 0)
                {
                    List<App.Models.ProductionProduct> Production_Product = new List<App.Models.ProductionProduct>();
                    List<int> instData = new List<int>();
                    Database db = DatabaseFactory.CreateDatabase();
                    foreach (App.Models.AirportData item in AirPort_Product)
                    {
                        using (DbCommand cmd = db.GetStoredProcCommand("SP_SetAirPortCodeDetails"))
                        {
                            db.AddInParameter(cmd, "@country", DbType.String, Convert.ToString(item.country).Trim());
                            db.AddInParameter(cmd, "@city", DbType.String, Convert.ToString(item.city).Trim());
                            db.AddInParameter(cmd, "@dst", DbType.String, Convert.ToString(item.dst).Trim());
                            db.AddInParameter(cmd, "@iata", DbType.String, Convert.ToString(item.iata).Trim());
                            db.AddInParameter(cmd, "@icao", DbType.String, Convert.ToString(item.icao).Trim());
                            db.AddInParameter(cmd, "@name", DbType.String, Convert.ToString(item.name).Trim());
                            db.AddInParameter(cmd, "@timezone", DbType.String, Convert.ToString(item.timezone).Trim());
                            db.AddInParameter(cmd, "@latitude", DbType.String, Convert.ToString(item.latitude).Trim());
                            db.AddInParameter(cmd, "@altitude", DbType.String, Convert.ToString(item.altitude).Trim());
                            instData.Add(db.ExecuteNonQuery(cmd));
                        }
                    }
                    Utility = new Common.CommonUtility() { Data = instData, Status = false, Message = "Invalid Credential.", ErrorCode = "113" };
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.AuthenticateUser", ex);
            }
            return Utility;
        }
        public App.Common.CommonUtility SetGetXMLSerializer()
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                App.Models.UserRegister UserDetails = new UserRegister("Rakesh Pal", "RMSILTD", 23232, "rakesh.pal@rmsi.com", 87878787, 6767556445);
                String XmlDataString = App.Models.UserRegister.Serialize(UserDetails);
                App.Models.UserRegister UserDetailsXmlDataString = App.Models.UserRegister.Deserialize(XmlDataString);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.AuthenticateUser", ex);
            }
            return Utility;
        }

    }

    public partial class DataLayer
    {
        #region MultiModelsList

        public App.Common.CommonUtility GetMultiModelInformation(App.Models.MultiModelsList ReqInfo)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                List<App.Models.MultiModelsList> Information_Req = new List<App.Models.MultiModelsList>();
                List<Models1> Models1List = new List<Models1>();
                Models1List.Add(new Models1() { USERID = 12345, FIRSTNAME = 12345, LASTNAME = 12345, MOBILE = 12345, STATUS = 12345, Make = 12345, Model = 12345, Year = 12345, Doors = 12345, Colour = 12345, Email = 12345, Price = 12345, Mileage = 12345 });
                List<Models2> Models2List = new List<Models2>();
                Models2List.Add(new Models2() { USERID = 12345, FIRSTNAME = 12345, LASTNAME = 12345, MOBILE = 12345, STATUS = 12345, Make = 12345, Model = 12345, Year = 12345, Doors = 12345, Colour = 12345, Email = 12345, Price = 12345, Mileage = 12345 });
                List<Models3> Models3List = new List<Models3>();
                Models3List.Add(new Models3() { USERID = 12345, FIRSTNAME = 12345, LASTNAME = 12345, MOBILE = 12345, STATUS = 12345, Make = 12345, Model = 12345, Year = 12345, Doors = 12345, Colour = 12345, Email = 12345, Price = 12345, Mileage = 12345 });
                List<Models4> Models4List = new List<Models4>();
                Models4List.Add(new Models4() { USERID = 12345, FIRSTNAME = 12345, LASTNAME = 12345, MOBILE = 12345, STATUS = 12345, Make = 12345, Model = 12345, Year = 12345, Doors = 12345, Colour = 12345, Email = 12345, Price = 12345, Mileage = 12345 });
                List<Models5> Models5List = new List<Models5>();
                Models5List.Add(new Models5() { USERID = 12345, FIRSTNAME = 12345, LASTNAME = 12345, MOBILE = 12345, STATUS = 12345, Make = 12345, Model = 12345, Year = 12345, Doors = 12345, Colour = 12345, Email = 12345, Price = 12345, Mileage = 12345 });
                for (int i = 0; i < 10; i++)
                {
                    Information_Req.Add(new App.Models.MultiModelsList()
                    {
                        MyModels1 = Models1List,
                        MyModels2 = Models2List,
                        MyModels3 = Models3List,
                        MyModels4 = Models4List,
                        MyModels5 = Models5List
                    });
                }
                Utility = new Common.CommonUtility() { Data = Information_Req, Status = true, Message = "Successfully.", ErrorCode = "" };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: Bussiness.AuthenticateUser", ex);
            }
            return Utility;
        }
        #endregion MultiModelsList


        #region Application Test Methods AsynchronousService
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login_Information"></param>
        /// <returns></returns>
        public App.Common.CommonUtility GetData(App.Models.LoginInformation Login_Information)
        {
            Common.CommonUtility Utililty = new Common.CommonUtility();
            try
            {
                string SQLSource = System.Configuration.ConfigurationManager.ConnectionStrings["SQLSource"].ToString();
                Utililty.Data = new GalleryContext().GetHumanResources();
                //Utililty.Data = new GalleryContext().GetHumanResourcesAsync();
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : GetData ", ex); }
            return Utililty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login_Information"></param>
        /// <returns></returns>
        public App.Common.CommonUtility PostData(App.Models.LoginInformation Login_Information)
        {
            Common.CommonUtility Utililty = new Common.CommonUtility();
            try
            {

            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : PostData ", ex); }
            return Utililty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login_Information"></param>
        /// <returns></returns>
        public App.Common.CommonUtility PutData(App.Models.LoginInformation Login_Information)
        {
            Common.CommonUtility Utililty = new Common.CommonUtility();
            try
            {

            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : PutData ", ex); }
            return Utililty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login_Information"></param>
        /// <returns></returns>
        public App.Common.CommonUtility DeleteData(App.Models.LoginInformation Login_Information)
        {
            Common.CommonUtility Utililty = new Common.CommonUtility();
            try
            {

            }
            catch (Exception ex)
            { throw new Exception("Exception occured at : DeleteData ", ex); }
            return Utililty;
        }
        #endregion Application Test Methods AsynchronousService

    }
    public static class Extensions
    {
        public static IEnumerable<T> Select<T>(this SqlDataReader reader, Func<SqlDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }

    public class GalleryContext
    {

        #region GalleryContext
        private string selectStatement = "SELECT * FROM Cars";
        public IEnumerable<Car> GetCars()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = selectStatement;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
            var asyncConnectionString = new SqlConnectionStringBuilder(connectionString)
            {
                AsynchronousProcessing = true
            }.ToString();
            using (var conn = new SqlConnection(asyncConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = selectStatement;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        return reader.Select(r => carBuilder(r)).ToList();
                    }
                }
            }
        }
        //private helpers
        private Car carBuilder(SqlDataReader reader)
        {
            return new Car
            {
                Id = int.Parse(reader["Id"].ToString()),
                Make = reader["Make"] is DBNull ? null : reader["Make"].ToString(),
                Model = reader["Model"] is DBNull ? null : reader["Model"].ToString(),
                Year = int.Parse(reader["Year"].ToString()),
                Doors = int.Parse(reader["Doors"].ToString()),
                Colour = reader["Colour"] is DBNull ? null : reader["Colour"].ToString(),
                Price = float.Parse(reader["Price"].ToString()),
                Mileage = int.Parse(reader["Mileage"].ToString())
            };
        }


        #endregion GalleryContext

        #region HumanResources
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HumanResources> GetHumanResources()
        {
            selectStatement = "SELECT [BusinessEntityID],[NationalIDNumber],[LoginID],[OrganizationNode],[OrganizationLevel],[JobTitle],[BirthDate],[MaritalStatus],[Gender],[HireDate],[SalariedFlag],[VacationHours],[SickLeaveHours],[CurrentFlag],[rowguid],[ModifiedDate] FROM [AdventureWorks2008R2].[HumanResources].[Employee]";
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = selectStatement;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Select(r => HumanResourcesBuilder(r)).ToList();
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HumanResources>> GetHumanResourcesAsync()
        {
            selectStatement = "SELECT [BusinessEntityID],[NationalIDNumber],[LoginID],[OrganizationNode],[OrganizationLevel],[JobTitle],[BirthDate],[MaritalStatus],[Gender],[HireDate],[SalariedFlag],[VacationHours],[SickLeaveHours],[CurrentFlag],[rowguid],[ModifiedDate] FROM [AdventureWorks2008R2].[HumanResources].[Employee]";
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLSource"].ConnectionString;
            var asyncConnectionString = new SqlConnectionStringBuilder(connectionString)
            {
                AsynchronousProcessing = true
            }.ToString();
            using (var conn = new SqlConnection(asyncConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = selectStatement;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        return reader.Select(r => HumanResourcesBuilder(r)).ToList();
                    }
                }
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="reader"></param>
       /// <returns></returns>
        private HumanResources HumanResourcesBuilder(SqlDataReader reader)
        {
            return new HumanResources
            {
                BusinessEntityID = reader["BusinessEntityID"].ToString(),
                NationalIDNumber = reader["NationalIDNumber"].ToString(),
                LoginID = reader["LoginID"].ToString(),
                OrganizationNode = reader["OrganizationNode"].ToString(),
                OrganizationLevel = reader["OrganizationLevel"].ToString(),
                JobTitle = reader["JobTitle"].ToString(),
                BirthDate = reader["BirthDate"].ToString(),
                MaritalStatus = reader["MaritalStatus"].ToString(),
                Gender = reader["Gender"].ToString(),
                HireDate = reader["HireDate"].ToString(),
                SalariedFlag = reader["SalariedFlag"].ToString(),
                VacationHours = reader["VacationHours"].ToString(),
                SickLeaveHours = reader["SickLeaveHours"].ToString(),
                CurrentFlag = reader["CurrentFlag"].ToString(),
                rowguid = reader["rowguid"].ToString(),
                ModifiedDate = reader["ModifiedDate"].ToString()
            };
        }

        #endregion HumanResources

    }
}
