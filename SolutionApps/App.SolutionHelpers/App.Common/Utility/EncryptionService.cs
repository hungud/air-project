using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace App.Common.Util
{

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:
    /// </summary>
    public static class EncryptionService
    {
        
        public static string EncryptString(string pPassword, string pKey, string pIV)
        {
            byte[] key = System.Convert.FromBase64String(pKey);
            byte[] iv = System.Convert.FromBase64String(pIV);
            string password = System.Convert.ToBase64String(EncryptStringToBytes_Aes(pPassword, key, iv));

            return password;
        }

        public static string DecryptString(string pPassword, string pKey, string pIV)
        {
            byte[] key = System.Convert.FromBase64String(pKey);
            byte[] iv = System.Convert.FromBase64String(pIV);
            byte[] password = System.Convert.FromBase64String(pPassword);
            string plainpassword = DecryptStringFromBytes_Aes(password, key, iv);

            return plainpassword;
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted = null;
            try
            {
                // Check arguments. 
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("Key");

                // Create an AesCryptoServiceProvider object 
                // with the specified key and IV. 
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {

                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            try
            {
                // Check arguments. 
                if (cipherText == null || cipherText.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                // Declare the string used to hold 
                // the decrypted text. 

                // Create an AesCryptoServiceProvider object 
                // with the specified key and IV. 
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption. 
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream 
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return plaintext;
        }

        
        /// <summary>
        /// Method is use to encrypt password.
        /// </summary>
        /// <param name="toEncrypt">String value which is to be encrypted</param>
        /// <param name="useHashing">Use hashcode corresponding to secret key</param>
        /// <returns>Return encrypted value.</returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] resultArray = null;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            try
            {
                string key = Convert.ToString(ConfigurationManager.AppSettings["SecurityKey"]);

                //If hashing use get hashcode regards to your key
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    //Always release the resources and flush data
                    //of the Cryptographic service provide. Best Practice
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                //mode of operation. there are other 4 modes.
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                //transform the specified region of bytes array to resultArray
                resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                //Release resources held by TripleDes Encryptor
                tdes.Clear();
            }
            catch (Exception objException)
            {
                throw new Exception("0#resc_exceptionEncryptDecryptHelper#resc_encrypt#Error in Encrypt.", objException);
            }
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>
        /// Method is use to decrypt the encrypted value.
        /// </summary>
        /// <param name="cipherString">Encrypted value which is to be decrypt or to get original value</param>
        /// <param name="useHashing">Use hashcode corresponding to secret key</param>
        /// <returns>Return original value</returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] resultArray = null;

            try
            {
                //get the byte code of the string
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                //Get your key from config file to open the lock!
                string key = Convert.ToString(ConfigurationManager.AppSettings["SecurityKey"]);

                if (useHashing)
                {
                    //if hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                    //release any resource held by the MD5CryptoServiceProvider
                    hashmd5.Clear();
                }
                else
                    //if hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                //Release resources held by TripleDes Encryptor                
                tdes.Clear();
            }
            catch (Exception objException)
            {
                throw new Exception("0#resc_exceptionEncryptDecryptHelper#resc_decrypt#Error in Decrypt.", objException);
            }
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        private static string GenerateSalt()
        {
            Random random = new Random();
            int saltSize = random.Next(2, 4);
            byte[] saltBytes = new byte[saltSize];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string encodedSalt, string password)
        {
            using (HashAlgorithm hashAlgorithm = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
                byte[] saltBytes = Convert.FromBase64String(encodedSalt);
                byte[] saltedPasswordBytes = new byte[(int)saltBytes.Length + (int)passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedPasswordBytes, 0, (int)saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, (int)saltBytes.Length, (int)passwordBytes.Length);
                byte[] hashedSaltedPasswordBytes = hashAlgorithm.ComputeHash(saltedPasswordBytes);
                string saltedHashedPassword = Convert.ToBase64String(hashedSaltedPasswordBytes);
                return saltedHashedPassword;
            }
        }
    }
}



