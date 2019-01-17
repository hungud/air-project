using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace DataLayer.MicrosoftEnterpriseLibrary
{
    public class MicrosoftEnterpriseLibraryHelpDetails
    {
        /// <summary>
        /// To get Bank Names OR IFSC Code OR Bank Branch Name OR Bank Address OR Bank City
        /// depends on strCommonParam which may be for Bank Names OR IFSC Code OR Bank Branch Name 
        /// OR Bank Address OR Bank City. Flag Paramater must contains "Search"
        /// </summary>
        /// <param name="strCommonParam,strContextKey"></param>
        /// <returns>Bank Names OR IFSC Code OR Bank Branch Name OR Bank Address OR Bank City</returns>
        public static DataTable GetBankDetails(string strCommonParam,string strContextKey)
        {
            try
            {
                DataSet dsBankDetails = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                using (DbCommand cmd = db.GetStoredProcCommand("vendome_BankDetails_Search"))
                {
                    db.AddInParameter(cmd, "@vc_CommonParam", DbType.String, strCommonParam);
                    db.AddInParameter(cmd, "@ch_contextKey", DbType.String, strContextKey);
                    db.AddInParameter(cmd, "@Flag", DbType.String, "Search");
                    db.AddInParameter(cmd, "@vc_BankName", DbType.String, "");
                    db.AddInParameter(cmd, "@vc_IFSC_Code", DbType.String, "");
                    db.AddInParameter(cmd, "@vc_City", DbType.String, "");
                    db.AddInParameter(cmd, "@vc_Street", DbType.String, "");
                    db.AddInParameter(cmd, "@vc_Branch", DbType.String, "");

                    dsBankDetails = db.ExecuteDataSet(cmd);

                }
                return dsBankDetails.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataAccessLayer.Vendor.GetBankName", ex);
            }
            return new DataTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strBankName"></param>
        /// <param name="strBankBranch"></param>
        /// <param name="strBankIFSCCode"></param>
        /// <param name="strBankAddress"></param>
        /// <param name="strBankCity"></param>
        /// <returns></returns>
        public static DataTable BindBankDetailsInGrid(string strBankName,string strBankBranch, string strBankIFSCCode, string strBankAddress, string strBankCity)
        {
            try
            {
                DataSet dsBankDetails = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                using (DbCommand cmd = db.GetStoredProcCommand("vendome_BankDetails_Search"))
                {
                    db.AddInParameter(cmd, "@vc_CommonParam", DbType.String, "");
                    db.AddInParameter(cmd, "@ch_contextKey", DbType.String, "");
                    db.AddInParameter(cmd, "@Flag", DbType.String, "FSearch");
                    db.AddInParameter(cmd, "@vc_BankName", DbType.String, strBankName);
                    db.AddInParameter(cmd, "@vc_IFSC_Code", DbType.String, strBankIFSCCode);
                    db.AddInParameter(cmd, "@vc_City", DbType.String, strBankCity);
                    db.AddInParameter(cmd, "@vc_Street", DbType.String, strBankAddress);
                    db.AddInParameter(cmd, "@vc_Branch", DbType.String, strBankBranch);

                    dsBankDetails = db.ExecuteDataSet(cmd);

                }
                return dsBankDetails.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataAccessLayer.Vendor.GetBankName", ex);
            }
            return new DataTable();
        }

        /// <summary>
        /// This method is used to get List of all the request Acted by Admin
        /// </summary>
        /// <param name="objActedRequestDetails"></param>
        /// <returns>Object of ActedRequestDetails entity with attribute SapVendorCode,SapVendorName,BankName,BankAcNo,BankIfscCode,Status</returns>
        /*
        public static void ActedRequestListDetails(ActedRequestDetails objActedRequestDetails)
        {
            try
            {
                if (db == null)
                    db = DatabaseFactory.CreateDatabase();

                DbCommand cmd = db.GetStoredProcCommand("dbo.vendome_GetActedRequestDetails");
                db.AddInParameter(cmd, "@SapVendorCode", DbType.String, objActedRequestDetails.SapVendorCode);
                db.AddInParameter(cmd, "@Key", DbType.String, objActedRequestDetails.Key);

                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        objActedRequestDetails.SapVendorCode = reader.GetValue(reader.GetOrdinal("ch_SapVendorCode")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("ch_SapVendorCode"));
                        objActedRequestDetails.SapVendorName = reader.GetValue(reader.GetOrdinal("vc_SapVendorName")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_SapVendorName"));
                        objActedRequestDetails.BankName = reader.GetValue(reader.GetOrdinal("vc_BankName")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankName"));
                        objActedRequestDetails.BankAcNo = reader.GetValue(reader.GetOrdinal("vc_BankAcNo")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankAcNo"));
                        objActedRequestDetails.BankBranch = reader.GetValue(reader.GetOrdinal("vc_BankBranch")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankBranch"));
                        objActedRequestDetails.BankAddress = reader.GetValue(reader.GetOrdinal("vc_BankAddress")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankAddress"));
                        objActedRequestDetails.BankCity = reader.GetValue(reader.GetOrdinal("vc_BankCity")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankCity"));
                        objActedRequestDetails.BankAcType = reader.GetValue(reader.GetOrdinal("BankAcType")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("BankAcType"));
                        objActedRequestDetails.BankIfscCode = reader.GetValue(reader.GetOrdinal("vc_BankIfscCode")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankIfscCode"));
                        objActedRequestDetails.Status = reader.GetValue(reader.GetOrdinal("Status")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("Status"));
                        objActedRequestDetails.BankKey = reader.GetValue(reader.GetOrdinal("vc_BankKey")) == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal("vc_BankKey"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured at: DataAccessLayer.Vendor.ActedRequestDetailsDLL.ActedRequestList", ex);
            }
        }
        */
    }
}
