using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace App.Common
{
    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Description:For Convert the Data
    /// </summary>
    public class AppConvert
    {
        public static Int32 ToInt32(object integer)
        {
            Int32 output = 0;
            try
            {
                if (integer != null)
                {
                    if (integer != DBNull.Value)
                    {
                        output = Convert.ToInt32(integer);
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static String ToString(object str)
        {
            String output = String.Empty;
            try
            {
                if (str != null)
                {
                    if (str != DBNull.Value)
                    {
                        output = Convert.ToString(str);
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static Int64 ToLong(object longNumber)
        {
            Int64 output = 0;
            try
            {
                if (longNumber != null)
                {
                    if (longNumber != DBNull.Value)
                    {
                        output = Convert.ToInt64(longNumber);
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static float ToFloat(object floatNumber)
        {
            float output = 0;
            try
            {
                if (floatNumber != null)
                {
                    if (floatNumber != DBNull.Value)
                    {
                        output = Convert.ToInt64(floatNumber);
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static DateTime ToDateTime(object date)
        {
            DateTime output = default(DateTime);
            try
            {
                if (date != null)
                {
                    if (date != DBNull.Value)
                    {
                        output = Convert.ToDateTime(date);
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static DataSet ToDataSet(object data)
        {
            DataSet output = default(DataSet);
            try
            {
                if (data != null)
                {
                    if (data != DBNull.Value)
                    {
                        output = (DataSet)data;
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static DataTable ToDataTable(object data)
        {
            DataTable output = default(DataTable);
            try
            {
                if (data != null)
                {
                    if (data != DBNull.Value)
                    {
                        output = (DataTable)data;
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static bool ToBoolean(object data)
        {
            bool output = default(bool);
            try
            {
                if (data != null)
                {
                    if (data != DBNull.Value)
                    {
                        output = (bool)data;
                    }
                }
            }
            catch
            {

            }
            return output;
        }

        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

    }
}
