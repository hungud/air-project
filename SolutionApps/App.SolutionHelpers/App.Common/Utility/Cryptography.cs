using System;
using System.IO;
using System.Text;
using System.Security;
using System.Web.Security;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Collections;

namespace App.Common.Util
{
    public class Cryptography
    {

        #region private Fields

        string Char_Set = "$%^NO1PQR(./~`CDEFG!@STUVWZghij}:pHB*#8uvwx?[]\',ef34590qrklmnoIJ)_+{st67abcdAyzKLM2Y";
        private static Byte[] m_Key = new Byte[8];
        private static Byte[] m_IV = new Byte[8];
        //private static SymmetricAlgorithm mobjCryptoService;  
        private static CryptoStream csCryptoStream;
        private static RijndaelManaged cspRijndael = new RijndaelManaged();
        private static FileStream fsInput;
        private static FileStream fsOutput;
        private static MemoryStream ms = new MemoryStream();
        private static int i, j;

        #endregion

        #region String One Time Encryption with MD5

        public static string EncryptOneTime(String strIn)
        {
            //It create object of HashAlgorithm Class, And initialise it with MD5 Algorithm. 
            HashAlgorithm hashMD5 = HashAlgorithm.Create("MD5");
            Byte[] textBytes, encryptBytes;
            StringBuilder strOut = new StringBuilder();

            try
            {
                //It rerieves input string bytes.
                textBytes = Encoding.ASCII.GetBytes(strIn);
                //It retrives encrypt cipher bytes.
                encryptBytes = hashMD5.ComputeHash(textBytes);

                //It converts each byte in Hexadecimal format and concatenate with strOut string.
                for (i = 0; i <= (encryptBytes.Length - 1); i++)
                {
                    //x2 suggest hexadecimal in block of two digits.
                    strOut.Append(String.Format("{0:x2}", encryptBytes[i]));
                }
                //It returns one time encrypt cipher text.
                return strOut.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region String Encryption / Decryption with DES

        #region Real Encryption

        /// <summary>
        /// Description: This function is used as wrapper for EncDec function ( which can encrypt
        ///              and decrypt string as well as file) to encrypt string.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path: 
        /// </summary>
        /// <param name="strIn">It is a string i.e. to be encrypted.</param>
        /// <param name="password">This is a password i.e. is need, when decrypting encrypted string.</param>
        /// <returns>Encrypted cipher in the form of hexadecimal.</returns>
        /// <remarks></remarks>
        public static String EncryptData(String strIn, String password)
        {
            int bytOutLen;
            StringBuilder strOut = new StringBuilder();
            //It retrieves bytes of input strings.
            Byte[] bytIn = ASCIIEncoding.ASCII.GetBytes(strIn);
            //It calls EncDec function by providing bytIn, password, and action.
            //Which gives encrypted string, that can be decrypt by passowrd i.e. used for encryption.
            Byte[] bytOut = EncDec(bytIn, password, CryptoAction.ActionEncrypt, String.Empty, String.Empty);
            bytOutLen = bytOut.Length - 1;

            for (int bytCntr = 0; bytCntr <= bytOutLen; bytCntr++)
            {
                strOut.Append(String.Format("{0:x2}", bytOut[bytCntr]));
            }

            return strOut.ToString();

        }

        /// <summary>
        /// Description: This function is used as wrapper for EncDec function ( which can encrypt
        ///              and decrypt string as well as file) to decrypt string.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path: 
        /// </summary>
        /// <param name="strIn">It is a string i.e. to be decrypted.</param>
        /// <param name="key">This is a password i.e. is need, when decrypting encrypted string.</param>
        /// <returns>Decrypted string.</returns>
        /// <remarks></remarks>
        public static String DecryptData(String strIn, String key)
        {
            int strInLen = 0;
            String strOut = String.Empty;

            try
            {
                //This constraint is used to calculate the size of byte array
                //i.e. is to be used to saved bytes of input encrypted string.
                if (strIn.Length % 2 == 0)
                {
                    strInLen = (strIn.Length / 2) - 1;
                }
                else
                {
                    strInLen = (strIn.Length / 2);
                }


                Byte[] bytIn = new Byte[strInLen + 1];
                Byte[] bytOut;
                j = 0;
                //Since encrypted strings bytes are stored in the form of hexadecimal
                //this iterative for loop retrieves bytes form hexadecimal numbers. 
                for (i = 0; i <= strInLen; i++)
                {
                    bytIn[i] = System.Convert.ToByte(strIn.Substring(j, 2), 16);
                    j = j + 2;
                }

                //It retrives bytes of decrypted strings by calling EncDec function.
                bytOut = EncDec(bytIn, key, CryptoAction.ActionDecrypt, String.Empty, String.Empty);
                //Retrieved bytes are converted in to ascii formated string.
                strOut = Encoding.UTF8.GetString(bytOut, 0, bytOut.Length);
            }
            catch
            {

            }

            return strOut;
        }
        /// <summary>
        /// Description: This function is used to encrypt or decrypt strings or files.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path: 
        /// </summary>
        /// <param name="input">If input is in the form of string, then this argument suggests bytes of input string</param>
        /// <param name="password">This is password i.e. used to encrypt or decrypt, string or files.</param>
        /// <param name="direction">This specifies this function is used to call encrypt or decrypt data.</param>
        /// <param name="inPath">If the file is to be encrypted or decrypted then, this parameter is used to pass URL of input file.</param>
        /// <param name="outPath">If the file is to be encrypted or decrypted then, this parameter is used to pass URL of output file.</param>
        /// <returns>If string is to be encrypted or decrypted, then this parameter is used to return encrypted or decrypted bytes. Other wise it used to pass succefully or error message.</returns>
        /// <remarks></remarks>
        public static Byte[] EncDec(Byte[] input, String password, CryptoAction direction, String inPath, String outPath)
        {

            long lngBytesProcessed = 0;
            long lngFileLength = 0;
            int intBytesInCurrentBlock = 0;

            try
            {
                //'If input is strings is not nothing then
                //'encryption or decryption is to be performed on the string,
                //'Else it is to be performed on the file.
                if (!(input == null))
                {
                    Byte[] bytBuffer;
                    //'Initialise memory stream
                    ms = new MemoryStream();
                    //'It passes password , direction and memory stream to function
                    //'And get back CryptoStream which is generated based on the input parameters.
                    csCryptoStream = getCryptoStream(password, direction, ms);
                    //'This step write string on the CryptoStream.
                    csCryptoStream.Write(input, 0, input.Length);
                    //'This flushes CryptoStream.
                    csCryptoStream.FlushFinalBlock();
                    //'It retrives bytes from MemoryStream.
                    bytBuffer = ms.ToArray();
                    return bytBuffer;
                }
                else
                {
                    //'This checks that input and output file URL is not blank.
                    if ((inPath != "") || (outPath != ""))
                    {
                        Byte[] bytBuffer = new Byte[4096];
                        //'It makes FileStreams for Input and Output files.
                        fsInput = new FileStream(inPath, FileMode.Open, FileAccess.Read);

                        fsOutput = new FileStream(outPath, FileMode.OpenOrCreate, FileAccess.Write);
                        fsOutput.SetLength(0);
                        //'It passes password , direction and memory stream to function
                        //'And get back CryptoStream which is generated based on the input parameters.
                        csCryptoStream = getCryptoStream(password, direction, fsOutput);
                        //'It gets the total length of file.
                        lngFileLength = fsInput.Length;
                        while (lngBytesProcessed < lngFileLength)
                        {
                            //'It retrives 4 KB of data from file input stream, and save that bytes in bytBuffer.
                            intBytesInCurrentBlock = fsInput.Read(bytBuffer, 0, 4096);
                            //'It retrieves bytes from bytBuffer
                            //'and writes it in to csCryptoStream.
                            csCryptoStream.Write(bytBuffer, 0, intBytesInCurrentBlock);
                            //'lngBytesProcessed is incremented on the basis of total number of retrieve bytes from
                            //'input file stream.
                            lngBytesProcessed = lngBytesProcessed + Convert.ToInt64(intBytesInCurrentBlock);
                        }

                        csCryptoStream.Close();
                        fsInput.Close();
                        fsOutput.Close();

                        return ASCIIEncoding.ASCII.GetBytes("1");
                    }
                    else
                    {
                        return ASCIIEncoding.ASCII.GetBytes("0");
                    }
                }
            }
            //Catch When Err.Number = 53
            //{
            //    return ASCIIEncoding.ASCII.GetBytes("Invalid Path or Filename");
            //}
            catch (Exception ex)
            {
                return ASCIIEncoding.ASCII.GetBytes(ex.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inPath"></param>
        /// <param name="password"></param>
        /// <param name="direction"></param>
        /// <returns></returns>

        public static byte[] FileToArray(string inPath, string password, CryptoAction direction)
        {

            long lngBytesProcessed = 0;
            long lngFileLength = 0;
            Byte[] bytBuffer = new Byte[4096];
            ArrayList plainText = new ArrayList(4096);

            //It makes FileStreams for Input files.
            FileStream fsInput = new FileStream(inPath, FileMode.Open, FileAccess.Read);



            // To Create Crypto Stream 
            csCryptoStream = new CryptoStream(fsInput,
                       cspRijndael.CreateDecryptor(CreateKey(password), CreateIV(password)),
                       CryptoStreamMode.Read);

            lngFileLength = fsInput.Length;
            MemoryStream ms = new MemoryStream();

            while (lngBytesProcessed < lngFileLength)
            {

                //It retrives 4 KB of data from Crypto Stream, and save that bytes in bytBuffer.
                csCryptoStream.Read(bytBuffer, 0, 4096);
                // To Write the bytes to Memory Stream
                ms.Write(bytBuffer, 0, 4096);
                //To Increase bytesProcessed Variable
                lngBytesProcessed = lngBytesProcessed + 4096;
            }

            csCryptoStream.Close();
            fsInput.Close();
            return ms.GetBuffer();

        }

        /// <summary>
        /// Description: This function returns CryptoStream.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path:  
        /// </summary>
        /// <param name="password">This is a password i.e. is need, when decrypting encrypted string.</param>
        /// <param name="direction">It specifies that string or file is to be encrypted or decrypted.</param>
        /// <param name="strmIn">It accepts the stream i.e. is used to generate crypto stream.</param>
        /// <returns>It is crypto stream.</returns>
        /// <remarks></remarks>
        private static CryptoStream getCryptoStream(String password, CryptoAction direction, Stream strmIn)
        {
            CryptoStream csCryptoStream = null;
            switch (direction)
            {
                case CryptoAction.ActionEncrypt:
                    csCryptoStream = new CryptoStream(strmIn,
                    cspRijndael.CreateEncryptor(CreateKey(password), CreateIV(password)),
                    CryptoStreamMode.Write);
                    break;
                case CryptoAction.ActionDecrypt:
                    csCryptoStream = new CryptoStream(strmIn,
                    cspRijndael.CreateDecryptor(CreateKey(password), CreateIV(password)),
                    CryptoStreamMode.Write);
                    //new CryptoStream(,,   
                    break;
            }
            return csCryptoStream;
        }

        /// <summary>
        /// Description: This function gives bytes of key which is generated on the basis of password string.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path:  
        /// </summary>
        /// <param name="passwordIn">This is password on which basis key is generated.</param>
        /// <returns>This is byte array element which can be used as a key.</returns>
        /// <remarks></remarks>
        private static Byte[] CreateKey(String passwordIn)
        {
            Byte[] passInByt = ASCIIEncoding.ASCII.GetBytes(passwordIn);
            SHA512Managed SHA512 = new SHA512Managed();
            //'This is used to compute hash on the basis of password.
            Byte[] bytResult = SHA512.ComputeHash(passInByt);
            Byte[] bytKey = new Byte[32];
            //'This saves first 32 bytes of array in bytResult byte array.
            Array.ConstrainedCopy(bytResult, 0, bytKey, 0, 32);
            return bytKey;
        }

        /// <summary>
        /// Description: This function gives bytes of IV ( Initial Vector ) which is generated on the basis of password string.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path:  
        /// </summary>
        /// <param name="passwordIn">This is password on which basis IV is generated</param>
        /// <returns>This is byte array element which can be used as a IV.</returns>
        /// <remarks></remarks>
        private static Byte[] CreateIV(String passwordIn)
        {
            Byte[] passInByt = ASCIIEncoding.ASCII.GetBytes(passwordIn);
            SHA512Managed SHA512 = new SHA512Managed();
            Byte[] bytResult = SHA512.ComputeHash(passInByt);
            Byte[] bytIV = new Byte[16];
            Array.ConstrainedCopy(bytResult, 32, bytIV, 0, 16);
            return bytIV;
        }

        /// <summary>
        /// Description: This enum is used to specifies actions.
        /// Author: Amber More
        /// Date: 12/09/2006
        /// Documentation Path:  
        /// </summary>
        /// <remarks></remarks>
        public enum CryptoAction
        {
            ActionEncrypt = 1,
            ActionDecrypt = 2,
        }



        #endregion

        #region Hex Only
        //'    Public Shared Function EncryptData(ByVal strIn As String, ByVal key As String) As String
        //'        Try
        //'            mobjCryptoService = new DESCryptoServiceProvider
        //'            Dim bytIn() As Byte = ASCIIEncoding.ASCII.GetBytes(strIn)
        //'            Dim strOut As String = ""
        //'            i = bytIn.Length
        //'            For j = 0 To (i - 1)
        //'                strOut &= String.Format("{0:x2}", bytIn(j))
        //'            Next

        //'            Return strOut
        //'        Catch ex As Exception
        //'            Throw ex
        //'        End Try
        //'    End Function

        //'    Public Shared Function DecryptData(ByVal strIn As String, ByVal key As String) As String
        //'        Try
        //'            Dim strInLen As Integer = 0
        //'            If strIn.Length Mod 2 = 0 Then
        //'                strInLen = (strIn.Length / 2) - 1
        //'            Else
        //'                strInLen = (strIn.Length / 2)
        //'            End If

        //'            Dim bytIn(strInLen) As Byte
        //'            j = 0

        //'            Dim strOut As String = ""
        //'            For i = 0 To strInLen
        //'                strOut &= Chr(Convert.ToInt32(strIn.Substring(j, 2), 16))
        //'                j = j + 2
        //'            Next

        //'            Return strOut
        //'        Catch ex As Exception
        //'            MsgBox(ex.ToString)
        //'        End Try
        //'    End Function

        #endregion

        #endregion


        public string Encrypt_data(string simple)
        {
            string input = simple;
            char[] searcharray = Char_Set.ToCharArray();
            char[] inputarray = input.ToCharArray();
            string output = "";
            Random nextval = new Random();
            int searchlen = Char_Set.Length;
            int char_code, nexval, space;

            for (int loop = 0; loop < input.Length; loop++)
            {
                if (Char_Set.IndexOf(inputarray[loop]) != -1)
                {

                    char_code = Char_Set.IndexOf(inputarray[loop]);
                    nexval = nextval.Next(99);
                    if (nexval > 9) nexval /= 10;
                    if (char_code + nexval > searchlen - 1)
                    {
                        space = searchlen - char_code;
                        char_code = nexval - space;
                    }
                    else
                        char_code += nexval;
                    output += searcharray[char_code];
                    output += nexval;

                }
            }
            return output;
        }
        public string Decrypt_data(string decrypt)
        {
            string input = decrypt;
            char[] inputarray = decrypt.ToCharArray();
            char[] input_real_encrypt = new char[200];
            int[] input_real_seed = new int[200];
            int char_code, seed = 0, space;
            for (int loop = 0; loop < input.Length; loop += 2)
            {
                input_real_encrypt[loop / 2] = input[loop];
                input_real_seed[loop / 2] = Int32.Parse(input[loop + 1] + "");
                seed = loop / 2;
            }
            char[] searcharray = Char_Set.ToCharArray();

            string output = "";
            int searchlen = Char_Set.Length;
            for (int loop = 0; loop <= seed; loop++)
            {
                if (Char_Set.IndexOf(input_real_encrypt[loop]) != -1)
                {
                    char_code = Char_Set.IndexOf(input_real_encrypt[loop]);

                    if (char_code - input_real_seed[loop] < 0)
                    {
                        space = input_real_seed[loop] - char_code;
                        char_code = Char_Set.Length - space;
                    }
                    else
                        char_code -= input_real_seed[loop];

                    if (char_code == 90) char_code = 0;
                    output += searcharray[char_code];
                }
            }
            return output;
        }
    }
}
