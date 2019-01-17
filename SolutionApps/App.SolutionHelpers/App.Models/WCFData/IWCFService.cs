using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace App.Model.WCFData
{
    [ServiceContract]
    public interface IWCFService
    {

        #region DataService

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "GetData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        App.Common.CommonUtility GetData(App.Models.ProductionProduct User_Reqest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "PostData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        App.Common.CommonUtility PostData(App.Models.ProductionProduct User_Reqest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "PutData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        App.Common.CommonUtility PutData(App.Models.ProductionProduct User_Reqest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "DeleteData", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        App.Common.CommonUtility DeleteData(App.Models.ProductionProduct User_Reqest);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAllData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        App.Common.CommonUtility GetAllData();

        #endregion DataService

        #region PaymentService

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="City"></param>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddPayee/{Name}/{City}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void AddPayee(string Name, string City);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "PayBill/{PayId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string PayBill(string PayId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayId"></param>
        /// <param name="TransId"></param>
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "UpdateBillPayment/{PayId}/{TransId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateBillPayment(string PayId, string TransId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "RemovePayee/{Id}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void RemovePayee(string Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "GetDataList", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetDataList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "GetDataLists", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetDataLists();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet]
        List<Book> GetBooksList();
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetBookList", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Book> GetBookList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Book/{id}")]
        Book GetBookById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddBook/{name}")]
        void AddBook(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateBook/{id}/{name}")]
        void UpdateBook(string id, string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteBook/{id}")]
        void DeleteBook(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBooksNames")]
        List<string> GetBooksNames();
        #endregion PaymentService

        #region DHCService

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User_Reqest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateHCRSR")]
        string CreateHCRSR(App.Models.ProductionProduct User_Reqest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SRno"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetHCRMS/{SRno}")]
        List<string> GetHCRMS(string SRno);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SRNo"></param>
        /// <param name="STATUScode"></param>
        /// <param name="strstatusdesc"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateHCMRSR/{SRNo}/{STATUScode}/{strstatusdesc}")]
        string UpdateHCMRSR(string SRNo, string STATUScode, string strstatusdesc);

        #endregion DHCService

        #region BPService

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "WebMethodI1", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        string WebMethodI1(string MetaData, Dictionary<string, string> Attachments);

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "WebMethodI2", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        string WebMethodI2(string ID, Dictionary<string, string> Attachments);

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "WebMethodI3", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        string WebMethodI3(string ID, List<string> approvedAttachmentsIds);

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "WebMethodI4/{ID}/{StatusCode}", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        string WebMethodI4(string ID, string StatusCode);
        #endregion BPService
    }
}
