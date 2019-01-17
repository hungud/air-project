using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.Util
{

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:
    /// </summary>
    public class EncryptionKeyGenService
    {
        public string genKeyValue { get; set; }
        public string genIVValue { get; set; }


        public void genEncryptionService()
        {
            try
            {
                //string original = "Here is some data to encrypt!";

                // Create a new instance of the AesCryptoServiceProvider 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
                {
                    genKeyValue = System.Convert.ToBase64String(myAes.Key);

                    genIVValue = System.Convert.ToBase64String(myAes.IV);
                    // Encrypt the string to an array of bytes. 
                    //byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                    //// Decrypt the bytes to a string. 
                    //string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                    //Display the original data and the decrypted data.
                    //Console.WriteLine("Original:   {0}", original);
                    //Console.WriteLine("Round Trip: {0}", roundtrip);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

    }
}



