using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLayer.WCFData
{
    public class WCFBusinessLayer
    {
        public WCFBusinessLayer()
        {

        }
        
        public System.Collections.Generic.List<string> GetData()
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetData();
        }
        public App.Common.CommonUtility GetDataList()
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetDataList();
        }
        public App.Common.CommonUtility GetData(App.Models.ProductionProduct User_Reqest)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetData(User_Reqest);
        }

        public App.Common.CommonUtility PostData(App.Models.ProductionProduct User_Reqest)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().PostData(User_Reqest);
        }

        public App.Common.CommonUtility PutData(App.Models.ProductionProduct User_Reqest)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().PutData(User_Reqest);
        }

        public App.Common.CommonUtility DeleteData(App.Models.ProductionProduct User_Reqest)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().DeleteData(User_Reqest);
        }
        public void AddPayee(string Name, string City)
        {
            new App.DataLayer.WCFData.WCFDataLayer().AddPayee(Name,City);
        }
        public string PayBill(string PayId)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().PayBill(PayId);
        }
        public void UpdateBillPayment(string PayId, string TransId)
        {
            new App.DataLayer.WCFData.WCFDataLayer().UpdateBillPayment(PayId,TransId);
        }
        public void RemovePayee(string Id)
        {
            new App.DataLayer.WCFData.WCFDataLayer().RemovePayee(Id);
        }
        public System.Collections.Generic.List<Model.WCFData.Book> GetBooksList()
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetBooksList();
        }
        public Model.WCFData.Book GetBookById(string id)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetBookById(id);
        }
        public void AddBook(string name)
        {
            new App.DataLayer.WCFData.WCFDataLayer().AddBook(name);
        }
        public void UpdateBook(string id, string name)
        {
            new App.DataLayer.WCFData.WCFDataLayer().UpdateBook(id,name);
        }
        public void DeleteBook(string id)
        {
            new App.DataLayer.WCFData.WCFDataLayer().DeleteBook(id);
        }
        public System.Collections.Generic.List<string> GetBooksNames()
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetBooksNames();
        }
        public string CreateHCRSR(Models.ProductionProduct User_Reqest)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().CreateHCRSR(User_Reqest);
        }
        public System.Collections.Generic.List<string> GetHCRMS(string SRno)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().GetHCRMS(SRno);
        }
        public string UpdateHCMRSR(string SRNo, string STATUScode, string strstatusdesc)
        {
            return new App.DataLayer.WCFData.WCFDataLayer().UpdateHCMRSR(SRNo, STATUScode,strstatusdesc)
;
        }
    }
}
