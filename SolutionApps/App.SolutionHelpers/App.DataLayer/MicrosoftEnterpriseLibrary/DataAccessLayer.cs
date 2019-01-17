#region Class Using References
using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
#endregion Class Using References

namespace App.DataLayer.MicrosoftEnterpriseLibrary
{
    public class DataAccessLayer : IDisposable
    {
        private string strConnection;
        /// <summary>
        /// Initializes a new instance of the DataAccessLayer class
        /// </summary>
        public DataAccessLayer()
        {
            //OracleSourceName = "OracleSource";
            OracleSourceName = "ShoppingConnectionString";
        }
        /// <summary>
        /// Gets or sets the string for used database
        /// </summary>
        public string OracleSourceName
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }
        /// <summary>
        /// Method is use to release the resources used by current class. 
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Connects to the database and attempts to get object. 
        /// Changed by Rakesh Pal on 30/07/2015
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sprocName, DbParameter[] paramArray)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sprocName);
                BuildParameters(ref command, paramArray);
                return db.ExecuteDataSet(command);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at: DataAccessLayer.GetDataSet", ex); }
            return new DataSet();
        }

        /// <summary>
        ///  Connects to the database
        ///  Changed by Rakesh Pal on 30/07/2015
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public object GetScalar(string sprocName, DbParameter[] paramArray)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sprocName);
                BuildParameters(ref command, paramArray);
                return db.ExecuteScalar(command);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at: DataAccessLayer.GetScalar", ex); }
            return new object();
        }

        /// <summary>
        ///Connects to the database and attempts to apply all adds, updates and deletes
        ///Changed by Rakesh Pal on 30/07/2015
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sprocName, DbParameter[] paramArray)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sprocName);
                BuildParameters(ref command, paramArray);
                return db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at: DataAccessLayer.ExecuteNonQuery", ex); }
            return new int();
        }

        /// <summary>
        /// Connects to the database and attempts to apply all adds, updates and deletes
        /// Changed by Rakesh Pal on 30/07/2015
        /// </summary>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sprocName)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase(OracleSourceName);
                //Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand(sprocName);
                return db.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            { throw new Exception("Exception occured at: DataAccessLayer.ExecuteNonQuery", ex); }
            return new int();
        }

        /// <summary>
        /// Add all parameter to command
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pArrParameters"></param>
        private void BuildParameters(ref DbCommand command, DbParameter[] pArrParameters)
        {
            if (pArrParameters != null)
            {
                foreach (IDataParameter dataP in pArrParameters)
                {
                    if (dataP != null)
                    {
                        command.Parameters.Add(dataP);
                    }
                }
            }
        }
    }
}
