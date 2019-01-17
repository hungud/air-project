namespace App.Common.Util
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;
    using System.Configuration;

    namespace Security.Cryptography
    {

        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL (SSE)                
        /// Company Name:   NIIT Technologies GIS Ltd            
        /// Created Date:   Developed on: 15/10/2010           
        /// Summary :CaptchaDotNet
        ///*************************************************
        /// </summary>

        public static class Encryptor
        {
            public static string Encrypt(string password)
            {
                string clearText = string.Empty;
                byte[] salt = { 113 };
                return Encrypt(clearText, password, salt);
            }
            public static string Encrypt(string clearText, string password, byte[] salt)
            {
                // Turn text to bytes
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt);
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = pdb.GetBytes(32);
                alg.IV = pdb.GetBytes(16);
                CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
                byte[] EncryptedData = ms.ToArray();
                return Convert.ToBase64String(EncryptedData);
            }
            public static string Decrypt(string cipherText, string password, byte[] salt)
            {
                // Convert text to byte
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt);
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = pdb.GetBytes(32);
                alg.IV = pdb.GetBytes(16);
                CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
                byte[] DecryptedData = ms.ToArray();
                return Encoding.Unicode.GetString(DecryptedData);
            }

            public class CryptorEngine
            {

                private static string encrypted, decrypted;
                private static MD5CryptoServiceProvider hashmd5 { get; set; }
                private static TripleDESCryptoServiceProvider des { get; set; }
                private static byte[] pwdhash, buff;
                private static string Mysalt = "GISCool44D36517B0CCE797FF57118ABE264FD9ZeroCool";
                static byte[] GetBytes = ASCIIEncoding.ASCII.GetBytes("GISCoolD");
                
                public static string Encryptrator(string password)
                {
                    return Encryptor.Encrypt(password, Mysalt, Convert.FromBase64String("GISCoolD"));
                }
                public static string Decryptrator(string password)
                {
                    return Encryptor.Encrypt(password, Mysalt, Convert.FromBase64String("GISCoolD"));
                }

                public static string Encrypt(string password)
                {
                    return EncryptData(password);
                }
                public static string Decrypt(string password)
                {
                    return DecryptData(password);
                }

                public static string Encryption(string password)
                {
                    return Encrypt(password, true);
                }
                public static string Decryption(string password)
                {
                    return Decrypt(password, true);
                }

                public static string Encrypting(string original)
                {
                    return EncryptingData(original);
                }
                public static string Decrypting(string encrypted)
                {
                    return DecryptingData(encrypted);
                }

                public static string AppEncryption(string password)
                {
                    return Encrypt(Encrypting(Encryption(password)));
                }
                public static string AppDecryption(string password)
                {
                    return Decryption(Decrypting(Decrypt(password)));
                }

                public static string AppDoubleEncryption(string password)
                {
                    return AppEncryption(AppEncryption(password));
                }
                public static string AppDoubleDecryption(string password)
                {
                    return AppDecryption(AppDecryption(password));
                }

                public static string AppTripleEncryption(string password)
                {
                    return AppEncryption(AppEncryption(AppEncryption(password)));
                }
                public static string AppTripleDecryption(string password)
                {
                    return AppDecryption(AppDecryption(AppDecryption(password)));
                }



                #region String CryptographySecurity
                
                /// <summary>
                /// 
                /// </summary>
                /// <param name="strInput"></param>
                /// <returns></returns>
                private static byte[] bytes(string strInput)
                {
                    int intCounter; char[] arrChar;
                    arrChar = strInput.ToCharArray();
                    byte[] arrByte = new byte[arrChar.Length];
                    for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
                        arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

                    return arrByte;
                }
                //private static byte[] bytes = ASCIIEncoding.ASCII.GetBytes(Mysalt);
                /// <summary>
                /// Encrypt a string.
                /// </summary>
                /// <param name="originalString">The original string.</param>
                /// <returns>The encrypted string.</returns>
                /// <exception cref="ArgumentNullException">This exception will be thrown when the original string is null or empty.</exception>
                private static string EncryptData(string originalString)
                {
                    if (!String.IsNullOrEmpty(originalString))
                    {
                        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                        MemoryStream memoryStream = new MemoryStream();
                        CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(GetBytes, GetBytes), CryptoStreamMode.Write);
                        StreamWriter writer = new StreamWriter(cryptoStream);
                        writer.Write(originalString);
                        writer.Flush();
                        cryptoStream.FlushFinalBlock();
                        writer.Flush();
                        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                    }
                    return string.Empty;
                }

                /// <summary>
                /// Decrypt a crypted string.
                /// </summary>
                /// <param name="cryptedString">The crypted string.</param>
                /// <returns>The decrypted string.</returns>
                /// <exception cref="ArgumentNullException">This exception will be thrown when the crypted string is null or empty.</exception>
                private static string DecryptData(string cryptedString)
                {
                    if (!string.IsNullOrEmpty(cryptedString))
                    {
                        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                        MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
                        CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(GetBytes, GetBytes), CryptoStreamMode.Read);
                        StreamReader reader = new StreamReader(cryptoStream);
                        return reader.ReadToEnd();
                    }
                    return string.Empty;
                }

                #endregion String CryptographySecurity


                #region String CryptographySecurity
                /// <summary>
                /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
                /// </summary>
                /// <param name="toEncrypt">string to be encrypted</param>
                /// <param name="useHashing">use hashing? send to for extra secirity</param>
                /// <returns></returns>
                private static string Encrypt(string toEncrypt, bool useHashing)
                {
                    byte[] keyArray;
                    byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                    //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                    //// Get the key from config file
                    ////<add key="SecurityKey" value="GISCool44D36517B0CCE797FF57118ABE264FD9ZeroCool"/>
                    //string key = (string)settingsReader.GetValue(Mysalt, typeof(String));
                    string key = Mysalt;
                    //System.Windows.Forms.MessageBox.Show(key);
                    if (useHashing)
                    {
                        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                        hashmd5.Clear();
                    }
                    else
                        keyArray = UTF8Encoding.UTF8.GetBytes(key);

                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = tdes.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    tdes.Clear();
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
                /// <summary>
                /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
                /// </summary>
                /// <param name="cipherString">encrypted string</param>
                /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
                /// <returns></returns>
                private static string Decrypt(string cipherString, bool useHashing)
                {
                    byte[] keyArray;
                    byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                    //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                    ////Get your key from config file to open the lock!
                    ////<add key="SecurityKey" value="GISCool44D36517B0CCE797FF57118ABE264FD9ZeroCool"/>
                    //string key = (string)settingsReader.GetValue(Mysalt, typeof(String));
                    string key = Mysalt;
                    if (useHashing)
                    {
                        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                        hashmd5.Clear();
                    }
                    else
                        keyArray = UTF8Encoding.UTF8.GetBytes(key);

                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = tdes.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    tdes.Clear();
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }

                #endregion String CryptographySecurity


                #region String CryptographySecurity
                /// <summary>
                /// 
                /// </summary>
                /// <param name="original"></param>
                /// <returns></returns>
                private static string EncryptingData(string original)
                {
                    //string password = "GOXDML123456";//11111GOODMORNINGRESEARCH
                    string password = Mysalt;
                    hashmd5 = new MD5CryptoServiceProvider();
                    pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                    hashmd5 = null;
                    des = new TripleDESCryptoServiceProvider();
                    des.Key = pwdhash;
                    des.Mode = CipherMode.ECB;//Correct                 
                    buff = ASCIIEncoding.ASCII.GetBytes(original.Trim());
                    encrypted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));
                    return encrypted;
                }
                /// <summary>
                /// 
                /// </summary>
                /// <param name="encrypted"></param>
                /// <returns></returns>
                private static string DecryptingData(string encrypted)
                {
                    string password = Mysalt;
                    hashmd5 = new MD5CryptoServiceProvider();
                    pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
                    hashmd5 = null;
                    des = new TripleDESCryptoServiceProvider();
                    des.Key = pwdhash;
                    des.Mode = CipherMode.ECB;//Corect            
                    try
                    {
                        buff = Convert.FromBase64String(encrypted.Trim());
                        decrypted = ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length));
                    }
                    catch
                    {
                        goto End_of_Decryption;
                    }
                    des = null;
                    return decrypted;
                End_of_Decryption:
                    return encrypted;
                }
                #endregion String CryptographySecurity

            }
        }
    }
}