using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;

namespace register_functionlity.DB
{
    public static class Helper

    {
        public const string ControlName = "ltlErrCtrl";


        #region Error Logging

        public static string GetErrorText(Exception ex)
        {
            return ConvertExceptionToXML(ex);
        }

        public static string ConvertExceptionToXML(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("Exception cannot be null");
            }

            using (StringWriter sw = new StringWriter())
            {
                using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sw))
                {
                    WriteException(xw, "Exception", ex);
                }

                return sw.ToString();
            }
        }

        public static void WriteException(System.Xml.XmlWriter writer, String name, Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            writer.WriteStartElement(name);

            writer.WriteElementString("Source", ex.Source);
            writer.WriteElementString("Message", ex.Message);
            writer.WriteElementString("StackTrace", ex.StackTrace);
            if (ex.InnerException != null)
            {
                WriteException(writer, "InnerException", ex.InnerException);
            }

            writer.WriteEndElement();
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "ZOU62^HBG@@3$@%$!^HHG)(12890IK78";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "ZOU62^HBG@@3$@%$!^HHG)(12890IK78";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        #endregion

        #region XML Helpers
        public static Int32 GetIntFormXML(XElement element, string PropertyName)
        {
            Int32 retval = 0;
            if (element.Element(PropertyName) != null)
            {
                if (element.Element(PropertyName).Value != null)
                {
                    Int32.TryParse(element.Element(PropertyName).Value, out retval);

                }
            }
            return retval;
        }

        public static Double GetDoubleFormXML(XElement element, string PropertyName)
        {
            Double retval = 0;
            if (element.Element(PropertyName) != null)
            {
                if (element.Element(PropertyName).Value != null)
                {

                    Double.TryParse(element.Element(PropertyName).Value, out retval);

                }
            }
            return retval;
        }

        public static decimal GetDecimalFormXML(XElement element, string PropertyName)
        {
            Decimal retval = 0;
            if (element.Element(PropertyName) != null)
            {
                if (element.Element(PropertyName).Value != null)
                {

                    Decimal.TryParse(element.Element(PropertyName).Value, out retval);

                }
            }
            return retval;
        }

        public static string GetStringFormXML(XElement element, string PropertyName)
        {
            string retval = string.Empty;
            if (element.Element(PropertyName) != null)
            {
                if (element.Element(PropertyName).Value != null)
                {
                    retval = Convert.ToString(element.Element(PropertyName).Value);

                }
            }
            return retval;
        }

        public static string GetStringFormXMLAttribute(XElement element, string attributeName)
        {
            string retval = string.Empty;
            if (element.Attribute(attributeName) != null)
            {
                if (element.Attribute(attributeName).Value != null)
                {
                    retval = Convert.ToString(element.Attribute(attributeName).Value);

                }
            }
            return retval;
        }

        public static Decimal GetDecimalFormXMLAttribute(XElement element, string attributeName)
        {
            Decimal retval = 0M;
            if (element.Attribute(attributeName) != null)
            {
                if (element.Attribute(attributeName).Value != null)
                {
                    Decimal.TryParse(element.Attribute(attributeName).Value, out retval);

                }
            }
            return retval;
        }

        public static int GetIntFormXMLAttribute(XElement element, string attributeName)
        {
            int retval = 0;
            if (element.Attribute(attributeName) != null)
            {
                if (element.Attribute(attributeName).Value != null)
                {
                    Int32.TryParse(element.Attribute(attributeName).Value, out retval);

                }
            }
            return retval;
        }
        #endregion
        
    }
}