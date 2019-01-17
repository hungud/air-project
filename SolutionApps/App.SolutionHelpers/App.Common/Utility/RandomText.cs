namespace App.Common.Util
{
    using System;

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
        /// 

        public static class RandomText
        {
            public static string Generate()
            {
                // Generate random text
                string s = "";
                char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
                int index;
                int lenght = RNG.Next(4, 6);
                for (int i = 0; i < lenght; i++)
                {
                    index = RNG.Next(chars.Length - 1);
                    s += chars[index].ToString();
                }
                return s;
            }
        }
    }
}