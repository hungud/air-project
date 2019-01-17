using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataLayer.WCFData
{
    public class WCFDataLayer
    {
        public WCFDataLayer()
        {

        }
        public System.Collections.Generic.List<string> GetData()
        {
            List<string> Data = new List<string>();
           
            return Data;
        }

        public App.Common.CommonUtility GetDataList()
        {
            Models.ProductionProduct User_Reqest = new Models.ProductionProduct();
            User_Reqest.ProductID = "1";
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                Utility = new Common.CommonUtility() { Data = new App.DataLayer.DataLayer().GetAdventureWorksProductionProduct(User_Reqest), Status = false, Message = "Succefull.", ErrorCode = "200" };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.WCFData", ex);
            }
            return Utility;
        }


        public App.Common.CommonUtility GetData(App.Models.ProductionProduct User_Reqest)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                Utility = new Common.CommonUtility() { Data = new App.DataLayer.DataLayer().GetAdventureWorksProductionProduct(User_Reqest), Status = false, Message = "Succefull.", ErrorCode = "200" };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.WCFData", ex);
            }
            return Utility;
        }

        public App.Common.CommonUtility PostData(App.Models.ProductionProduct User_Reqest)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                Utility = new Common.CommonUtility() { Data = new App.DataLayer.DataLayer().GetAdventureWorksProductionProduct(User_Reqest), Status = false, Message = "Succefull.", ErrorCode = "200" };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.WCFData", ex);
            }
            return Utility;
        }

        public App.Common.CommonUtility PutData(App.Models.ProductionProduct User_Reqest)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                Utility = new Common.CommonUtility() { Data = new App.DataLayer.DataLayer().GetAdventureWorksProductionProduct(User_Reqest), Status = false, Message = "Succefull.", ErrorCode = "200" };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.WCFData", ex);
            }
            return Utility;
        }

        public App.Common.CommonUtility DeleteData(App.Models.ProductionProduct User_Reqest)
        {
            App.Common.CommonUtility Utility = new Common.CommonUtility();
            try
            {
                Utility = new Common.CommonUtility() { Data = new App.DataLayer.DataLayer().GetAdventureWorksProductionProduct(User_Reqest), Status = false, Message = "Succefull.", ErrorCode = "200" };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataLayer.WCFData", ex);
            }
            return Utility;
        }
        public void AddPayee(string Name, string City)
        {

        }
        public string PayBill(string PayId)
        {
            return "Transaction having PayId " + PayId + " is successful";
        }
        public void UpdateBillPayment(string PayId, string TransId)
        {

        }
        public void RemovePayee(string Id)
        {

        }
        public System.Collections.Generic.List<Model.WCFData.Book> GetBooksList()
        {
            List<Model.WCFData.Book> MyList = new List<Model.WCFData.Book>();
            for (int i = 0; i < 150; i++)
            {
                MyList.Add(new Model.WCFData.Book() { ID = i.ToString(), BookName = "BookName : " + i.ToString() });
            }
            return MyList;
        }
        public Model.WCFData.Book GetBookById(string id)
        {
            return new Model.WCFData.Book();
        }
        public void AddBook(string name)
        {
        }
        public void UpdateBook(string id, string name)
        {
        }
        public void DeleteBook(string id)
        {
        }
        public System.Collections.Generic.List<string> GetBooksNames()
        {
            List<string> MyList = new List<string>();
            for (int i = 0; i < 150; i++)
            {
                MyList.Add(i.ToString());  
            }
            return MyList;
        }
        public string CreateHCRSR(Models.ProductionProduct User_Reqest)
        {
            return string.Empty;
        }
        public System.Collections.Generic.List<string> GetHCRMS(string SRno)
        {
            List<string> MyList = new List<string>();
            for (int i = 0; i < 150; i++)
            {
                MyList.Add(i.ToString());
            }
            return MyList;
        }
        public string UpdateHCMRSR(string SRNo, string STATUScode, string strstatusdesc)
        {
            return string.Empty;
        }
    }
}
