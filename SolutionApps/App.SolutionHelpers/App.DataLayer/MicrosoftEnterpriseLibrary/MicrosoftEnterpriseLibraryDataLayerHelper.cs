using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace DataLayer.MicrosoftEnterpriseLibrary
{
    public class MicrosoftEnterpriseLibraryDataLayerHelper
    {
        public MicrosoftEnterpriseLibraryDataLayerHelper()
        {

        }

        public DataTable GetNorthWindProducts()
        {
            try
            {
                DataSet dsBankDetails = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                using (DbCommand cmd = db.GetStoredProcCommand("GetNorthWNDProducts"))
                {
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

        public DataTable GetNorthWindProductList()
        {
            try
            {
                DataSet dsBankDetails = new DataSet();
                Database db = DatabaseFactory.CreateDatabase();
                using (DbCommand cmd = db.GetStoredProcCommand("GetNorthWNDProducts"))
                {
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
        /// To get Bank Names OR IFSC Code OR Bank Branch Name OR Bank Address OR Bank City
        /// depends on strCommonParam which may be for Bank Names OR IFSC Code OR Bank Branch Name 
        /// OR Bank Address OR Bank City. Flag Paramater must contains "Search"
        /// </summary>
        /// <param name="strCommonParam,strContextKey"></param>
        /// <returns>Bank Names OR IFSC Code OR Bank Branch Name OR Bank Address OR Bank City</returns>
        public DataTable GetBankDetails(string strCommonParam, string strContextKey)
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
        public DataTable BindBankDetailsInGrid(string strBankName, string strBankBranch, string strBankIFSCCode, string strBankAddress, string strBankCity)
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

    }
}
