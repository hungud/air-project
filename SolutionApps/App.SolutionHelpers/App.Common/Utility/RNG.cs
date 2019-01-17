namespace App.Common.Util
{
    using System;
    using System.Security.Cryptography;

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


        public static class RNG
        {
            private static byte[] randb = new byte[4];
            private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            public static int Next()
            {
                rand.GetBytes(randb);
                int value = BitConverter.ToInt32(randb, 0);
                if (value < 0) value = -value;
                return value;
            }
            public static int Next(int max)
            {
                rand.GetBytes(randb);
                int value = BitConverter.ToInt32(randb, 0);
                value = value % (max + 1); // % calculates remainder
                if (value < 0) value = -value;
                return value;
            }
            public static int Next(int min, int max)
            {
                int value = Next(max - min) + min;
                return value;
            }
        }
    }
}