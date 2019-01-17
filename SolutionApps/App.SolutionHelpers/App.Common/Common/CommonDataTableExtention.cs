namespace App.Common
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Web.Script.Serialization;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2016
    /// Summary/Description:For Class to CommonDataTable to Class.
    /// </summary>
    public class CommonDataTable
    {

        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            List<Dictionary<string, object>> Parent_Row = new List<Dictionary<string, object>>();
            if (table != null)
            {
                foreach (DataRow Data_Row in table.Rows)
                {
                    Dictionary<string, object> childRow = new Dictionary<string, object>();
                    foreach (DataColumn Column_Name in table.Columns)
                    {
                        childRow.Add(Column_Name.ColumnName, Data_Row[Column_Name]);
                    }
                    Parent_Row.Add(childRow);
                }
            }
            return new JavaScriptSerializer().Serialize(Parent_Row);
        }
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("Education", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Rows.Add(1, "Satinder Singh", "Bsc Com Sci", "Mumbai");
            dt.Rows.Add(2, "Amit Sarna", "Mstr Com Sci", "Mumbai");
            dt.Rows.Add(3, "Andrea Ely", "Bsc Bio-Chemistry", "Queensland");
            dt.Rows.Add(4, "Leslie Mac", "MSC", "Town-ville");
            dt.Rows.Add(5, "Vaibhav Adhyapak", "MBA", "New Delhi");
            dt.Rows.Add(6, "Johny Dave", "MCA", "Texas");
            return dt;
        }

    }


    public static class Helper
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        //List< Student > studentDetails = new List< Student >();  
        //studentDetails = ConvertDataTable< Student >(dt);  

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}