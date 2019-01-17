namespace App.Common
{
    using System;
    using System.IO; 

    public class CommonDateTimeFormat
    {
        
        public DateTime MyDateTime { get { return DateTime.Now; }}
        public enum DTimeFormatModes { d, D, f, F, g, G, m, M, o, O, s, t, T, u, U, ddd, dddd, tt, Y, y, yy, yyy, yyyy };
        public DTimeFormatModes DTMode  {get;set;}

        //DateTime now = DateTime.Now;
        //Console.WriteLine(now.ToString("d"));
        //Console.WriteLine(now.ToString("D"));
        //Console.WriteLine(now.ToString("f"));
        //Console.WriteLine(now.ToString("F"));
        //Console.WriteLine(now.ToString("g"));
        //Console.WriteLine(now.ToString("G"));
        //Console.WriteLine(now.ToString("m"));
        //Console.WriteLine(now.ToString("M"));
        //Console.WriteLine(now.ToString("o"));
        //Console.WriteLine(now.ToString("O"));
        //Console.WriteLine(now.ToString("s"));
        //Console.WriteLine(now.ToString("t"));
        //Console.WriteLine(now.ToString("T"));
        //Console.WriteLine(now.ToString("u"));
        //Console.WriteLine(now.ToString("U"));
        //Console.WriteLine(now.ToString("y"));
        //Console.WriteLine(now.ToString("Y"));

        //String.Format("{0:t}", dt);  // "4:05 PM"                         ShortTime
        //String.Format("{0:d}", dt);  // "3/9/2008"                        ShortDate
        //String.Format("{0:T}", dt);  // "4:05:07 PM"                      LongTime
        //String.Format("{0:D}", dt);  // "Sunday, March 09, 2008"          LongDate
        //String.Format("{0:f}", dt);  // "Sunday, March 09, 2008 4:05 PM"  LongDate+ShortTime
        //String.Format("{0:F}", dt);  // "Sunday, March 09, 2008 4:05:07 PM" FullDateTime
        //String.Format("{0:g}", dt);  // "3/9/2008 4:05 PM"                ShortDate+ShortTime
        //String.Format("{0:G}", dt);  // "3/9/2008 4:05:07 PM"             ShortDate+LongTime
        //String.Format("{0:m}", dt);  // "March 09"                        MonthDay
        //String.Format("{0:y}", dt);  // "March, 2008"                     YearMonth
        //String.Format("{0:r}", dt);  // "Sun, 09 Mar 2008 16:05:07 GMT"   RFC1123
        //String.Format("{0:s}", dt);  // "2008-03-09T16:05:07"             SortableDateTime
        //String.Format("{0:u}", dt);  // "2008-03-09 16:05:07Z"            UniversalSortableDateTime
        //DateTime dt = new DateTime(2008, 3, 9, 16, 5, 7, 123);
        //String.Format("{0:y yy yyy yyyy}", dt);  // "8 08 008 2008"   year
        //String.Format("{0:M MM MMM MMMM}", dt);  // "3 03 Mar March"  month
        //String.Format("{0:d dd ddd dddd}", dt);  // "9 09 Sun Sunday" day
        //String.Format("{0:h hh H HH}",     dt);  // "4 04 16 16"      hour 12/24
        //String.Format("{0:m mm}",          dt);  // "5 05"            minute
        //String.Format("{0:s ss}",          dt);  // "7 07"            second
        //String.Format("{0:f ff fff ffff}", dt);  // "1 12 123 1230"   sec.fraction
        //String.Format("{0:F FF FFF FFFF}", dt);  // "1 12 123 123"    without zeroes
        //String.Format("{0:t tt}",          dt);  // "P PM"            A.M. or P.M.
        //String.Format("{0:z zz zzz}",      dt);  // "-6 -06 -06:00"   time zone
        //String.Format("{0:M/d/yyyy}", dt);            // "3/9/2008"
        //String.Format("{0:MM/dd/yyyy}", dt);          // "03/09/2008"
        //// day/month names
        //String.Format("{0:ddd, MMM d, yyyy}", dt);    // "Sun, Mar 9, 2008"
        //String.Format("{0:dddd, MMMM d, yyyy}", dt);  // "Sunday, March 9, 2008"
        ////two/four digit year
        //String.Format("{0:MM/dd/yy}", dt);            // "03/09/08"
        //String.Format("{0:MM/dd/yyyy}", dt);          // "03/09/2008"


        public String GetDateTimeFormat(DTimeFormatModes DateTimeFormat)
        {
            DateTime now = DateTime.Now;
            String DataFormat=string.Empty;            
            switch (DTMode)
            {
                case DTimeFormatModes.d:
                    DataFormat = now.ToString("d").ToString();
                    break;
                case DTimeFormatModes.D:
                    DataFormat = "";
                    break;
                default:
                    DataFormat = "";
                    break;
            }            
            return DataFormat;
        }


        public DateTime GetDateTimeDDMMYYYYTOMMDDYYYY(String StrDateTime)
        {
            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            return DateTime.Parse(StrDateTime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        }

    }
}
