using System;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Globalization;

namespace App.Common
{

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:
    /// </summary>
    public static class CommonVailidationLibrary
    {
        #region Folder and String Validation

        //Function to test for Positive Integers. 
        //Explanation of Regular Expressions:
        //"*" matches 0 or more patterns
        //"?" matches single character
        //"^" for ignoring matches.
        //"[]" for searching range patterns.

        // Function to test for Positive Integers. 
        public static bool IsNaturalNumber(String InputData)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(InputData) &&
            objNaturalPattern.IsMatch(InputData);
        }
        // Function to test for Positive Integers with zero inclusive 
        public static bool IsWholeNumber(String InputData)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(InputData);
        }
        // Function to Test for Integers both Positive & Negative 
        public static bool IsInteger(String InputData)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(InputData) && objIntPattern.IsMatch(InputData);
        }
        // Function to Test for Positive Number both Integer & Real 
        public static bool IsPositiveNumber(String InputData)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(InputData) &&
            objPositivePattern.IsMatch(InputData) &&
            !objTwoDotPattern.IsMatch(InputData);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(String InputData)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(InputData) &&
            !objTwoDotPattern.IsMatch(InputData) &&
            !objTwoMinusPattern.IsMatch(InputData) &&
            objNumberPattern.IsMatch(InputData);
        }
        // Function To test for Alphabets. 
        public static bool IsAlpha(String InputData)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(InputData);
        }
        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(String InputData)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(InputData);
        }


        /// <summary>
        /// Special Charector Validation
        /// </summary>
        /// <param name="StrData"></param>
        /// <returns></returns>
        public static string GetSpecialDataValidation(string InputData)
        {
            string StrSpecial = "~,`,!,@,#,$,%,^,&,*,',|,\\,||,?,<,>,.,:,;,(,),(),_,-,+,=,INSERT,UPDATE DELETE,TABLE,TRUNCATE,1=1";
            string[] split = null;
            split = StrSpecial.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in split)
            {
                if (InputData.ToUpper().Contains(str))
                {
                    InputData.ToUpper().Replace(str, "");
                    return InputData;
                }
            }
            return InputData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static bool GetSpecialCharValidation(string InputData)
        {
            string StrSpecial = "~,`,!,@,#,$,%,^,&,*,',|,\\,||,?,<,>,.,:,;";
            string[] split = null;
            split = StrSpecial.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in split)
            {
                if (InputData.Contains(str))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static string GetSpecialCharReplace(string InputData)
        {
            string StrSpecial = "~,`,!,@,#,$,%,^,&,*,',|,\\,||,?,<,>,.,:,;,(,),(),_,-,+,=";
            string[] split = null;
            split = StrSpecial.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in split)
            {
                InputData = InputData.Replace(str, "").Replace(",", "");
            }
            return InputData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChar(string InputData)
        {
            string StrSpecial = "~,`,!,@,#,$,%,^,&,*,',+,'";
            string[] split = null;
            split = StrSpecial.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in split)
            {
                InputData = InputData.Replace(str, "").Replace(",", "");
            }
            return InputData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static bool IsValidGuid(string InputData)
        {
            if (!(string.IsNullOrEmpty(InputData)))
            {
                Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$");
                Regex reg = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                return reg.IsMatch(InputData);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static bool GetValidCCNum(string InputData)
        {
            // This expression is looking for a series of numbers, which follow the pattern 
            // for Visa, MC, Discover and American Express. It also allows for dashes between sets of numbers 
            string pattern = @"^((4\d{3})|(5[1-5]\d{2})|(6011))-?\d{4}-?\d{4}-?\d{4}|3[4,7][\d\s-]{15}$";
            Regex match = new Regex(pattern);
            return match.IsMatch(InputData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static string CReateFolder(string InputData)
        {
            if (!Directory.Exists(InputData))
            {
                DirectoryInfo DI = Directory.CreateDirectory(InputData);
                return "FolderDirectory Created Successfuly..";
            }
            else
            {
                return "Directory Found....";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        public static bool GetValidatingUnicodeCharacters(string InputData)
        {
            if (!Regex.IsMatch(InputData, @"^[\p{L}\p{Zs}\p{Lu}\p{Ll}\']{1,40}$"))
                return false;
            else
                return true;
        }
        private static string SafeSqlLiteral(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }
        #endregion Folder and String Validation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public static bool ValidEmail(string EmailAddress)
        {
            if (!String.IsNullOrEmpty(EmailAddress))
            {
                Regex rx = new Regex(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$");
                Match m = rx.Match(EmailAddress);
                if (m.Success)
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetIP(HttpRequestBase request)
        {
            string ip = request.Headers["X-Forwarded-For"]; // AWS compatibility

            if (string.IsNullOrEmpty(ip))
            {
                ip = request.UserHostAddress;
            }

            return ip;
        }
        public static long GetTick()
        {
            return App.Common.AppConvert.ToLong(System.DateTime.Now.AddSeconds(360).Ticks);
        }
    }
}
