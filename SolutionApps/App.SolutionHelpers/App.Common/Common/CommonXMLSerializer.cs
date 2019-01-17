namespace App.Common
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:For Class to XML and XML to Class.
    /// </summary>
    public class XMLSerializer<T>
    {

        #region Serialization support
        /// <summary>
        /// XML serializer for class
        /// </summary>
        private System.Xml.Serialization.XmlSerializer _serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        /// <summary>
        /// Serialize object into XML string
        /// </summary>
        /// <param name="myobject"></param>
        /// <returns></returns>
        public String Serialize(T myobject)
        {
            try
            {
                String result = null;
                if (myobject != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (XmlTextWriter xtw = new XmlTextWriter(ms, System.Text.Encoding.UTF8))
                        {
                            xtw.Formatting = Formatting.Indented;
                            _serializer.Serialize(xtw, myobject);
                            //rewind
                            ms.Seek(0, SeekOrigin.Begin);
                            using (StreamReader reader = new StreamReader(ms, System.Text.Encoding.UTF8))
                            {
                                result = reader.ReadToEnd();
                                xtw.Close();
                                reader.Close();
                            }
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deserialize object into an instance of T
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public T Deserialize(String xml)
        {
            try
            {
                if (!String.IsNullOrEmpty(xml))
                {
                    using (StringReader sr = new StringReader(xml))
                    {
                        return (T)_serializer.Deserialize(sr);
                    }
                }
                else
                    return default(T);
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}