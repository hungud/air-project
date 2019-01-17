using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Collections.Specialized;
namespace App.Common
{
    namespace CommonLibrary.Utility
    {
        public enum XDateTimeType { Calendar = 0, Business = 1 };
        public class XDateTime
        {
            #region Private Class Member Variable

            //private string _format = "MM/dd/yyyy";
            private string _format = "dd/MM/yyyy";
            //private DateTime _date;
            private DateTime _date { get; set; }
            private XDateTimeType _type;
            private Hashtable _holidays;

            #endregion

            #region Constructor

            public XDateTime()
            {
                init(DateTime.Now, XDateTimeType.Calendar);
            }

            public XDateTime(string dateTime)
            {
                init(Convert.ToDateTime(dateTime), XDateTimeType.Calendar);
            }

            public XDateTime(string dateTime, XDateTimeType dateType)
            {
                init(Convert.ToDateTime(dateTime), dateType);
                check();
            }

            #endregion

            #region Public methods

            /// <summary>
            /// Gets or sets the type of the Smart Date which is a value of XDateTimeType enumeration
            /// and could be Calendar or Business type. 
            /// </summary>
            /// <remarks>
            /// If you set the DateType to Business type the value of the inner DateTime variable might be
            /// changed to the value of the closest next business date.
            /// </remarks>
            public XDateTimeType DateType
            {
                get { return _type; }
                set
                {
                    _type = value;
                    check();
                }
            }

            /// <summary>
            /// Gets or sets a value of the inner System.DateTime variable of the Smart Date.
            /// </summary>
            /// <remarks>
            /// If you set the Date and the DateType is a Business type the value of the inner DateTime variable
            /// might be changed to the value of the closest next business date.
            /// </remarks>
            public DateTime Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    check();
                }
            }

            /// <summary>
            /// Returns true if the value of the inner DateTime variable is a public Holiday, otherwise false.
            /// </summary>
            public bool IsHoliday
            {
                get
                {
                    return _holidays.ContainsValue(_date.ToString(_format));
                }
            }

            /// <summary>
            /// Returns true if the value of the inner DateTime variable is a work day, otherwise false.
            /// </summary>
            public bool IsWorkDay
            {
                get
                {
                    return !(_date.DayOfWeek == DayOfWeek.Saturday || _date.DayOfWeek == DayOfWeek.Sunday || this.IsHoliday);
                }
            }

            /// <summary>
            /// Adds given hours to the value of the inner DateTime variable considering the DateType value.
            /// </summary>
            /// <param name="hours">Hours to add.</param>
            public void AddHours(short hours)
            {
                _date = _date.AddHours(Convert.ToDouble(hours));
                check();
            }

            /// <summary>
            /// Adds one business or calendar day depending on the DateType value to the value of the inner DateTime variable.
            /// </summary>
            public void AddDay()
            {
                _date = _date.AddDays(1.0);
                check();
            }

            /// <summary>
            /// Adds given CALENDAR amount of days to the value of the inner DateTime variable. Always considers
            /// a value of DateType property and change the inner date according to this value.
            /// </summary>
            /// <param name="days">Calendar days to add.</param>
            public void AddDays(short days)
            {
                _date = _date.AddDays(Convert.ToDouble(days));
                check();
            }

            /// <summary>
            /// Adds given BUSINESS amount of days to the value of the inner DateTime variable. Always considers
            /// a value of DateType property and change the inner date according to this value.
            /// </summary>
            /// <param name="days">Business days to add.</param>
            public void AddBusinessDays(short days)
            {
                double sign = Convert.ToDouble(Math.Sign(days));
                int unsignedDays = Math.Sign(days) * days;
                for (int i = 0; i < unsignedDays; i++)
                {
                    do
                    {
                        _date = _date.AddDays(sign);
                    }
                    while (!this.IsWorkDay);
                }
            }

            /// <summary>
            /// Returns a new instance of the System.DateTime object with the value of Next Business Day counting from
            /// the value of the XDateTime object.
            /// </summary>
            public DateTime NextBusinessDay()
            {
                DateTime date = _date;
                do
                {
                    date = date.AddDays(1.0);
                }
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                    _holidays.ContainsValue(date.ToString(_format)));
                return date;
            }

            /// <summary>
            /// Returns a new instance of the System.DateTime object with the value of Previous Business Day counting from
            /// the value of the XDateTime object.
            /// </summary>
            public DateTime PreviousBusinessDay()
            {
                //DateTime date = _date;
                DateTime date = DateTime.Today;
                do
                {
                    date = date.AddDays(-1.0);
                }
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                    _holidays.ContainsValue(date.ToString(_format)));
                return date;
            }

            public int NumberOfBusinessDaysFrom(DateTime date)
            {
                double dayToAdd = -1;
                int numberOfBusinessDays = 0;

                if (date < _date)
                {
                    dayToAdd = 1;
                }
                while (_date != date)
                {
                    if (!(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || _holidays.ContainsValue(date.ToString(_format))))
                    {
                        // if date is a working (business) day - increase a counter
                        numberOfBusinessDays++;
                    }
                    date = date.AddDays(dayToAdd);
                }
                return numberOfBusinessDays;
            }
            public int NumberOfBusinessDaysHour(DateTime FromDate)
            {
                int numberOfBusinessDays = 0;
                DateTime ToDate = DateTime.Now;
                if (ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                }
                else if (!ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                }
                return numberOfBusinessDays;
            }
            public int NumberOfBusinessDaysMinute(DateTime FromDate)
            {
                int numberOfBusinessDays = 0;
                DateTime ToDate = DateTime.Now;
                if (ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Minute - FromDate.Minute);
                }
                else if (!ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Minute - FromDate.Minute);
                }
                return numberOfBusinessDays;
            }

            #endregion

            #region Private methods

            /// <summary>
            /// Initializes private members of the XDateTime object. For using in the class constructors only.
            /// </summary>
            /// <param name="dateTime">Initial date.</param>
            /// <param name="dateType">Date type.</param>
            private void init(DateTime dateTime, XDateTimeType dateType)
            {
                _date = dateTime;
                _type = dateType;
                initHolidays();
            }


            /// <summary>
            /// Loads a list of public holidays from .config file.
            /// </summary>
            private void initHolidays()
            {
                //String StrFilePath = System.Windows.Forms.Application.StartupPath.ToString() + @"\HoliDayFile.xml";
                //_holidays = new Hashtable();
                //_holidays =(new BusinessCommonLibrary.BusinessCommonLinqXmlManagement().GetHashtableXmlData(StrFilePath,"HoliDay"));

                //////Read holidays from .config file
                _holidays = new Hashtable();
                _holidays.Add("New Years Day", "01/03/2011");
                _holidays.Add("Good Friday", "03/25/2011");
                _holidays.Add("Victoria Day", "05/23/201");
                _holidays.Add("Canada Day", "07/01/2011");
                _holidays.Add("Civic Holiday", "08/01/2011");
                _holidays.Add("Thanksgiving", "10/10/2011");
                _holidays.Add("Christmas Day", "12/26/2011");
                _holidays.Add("Boxing Day", "12/27/2011");
                //_holidays = (Hashtable)ConfigurationManager.GetSection("Holidays");          

            }


            /// <summary>
            /// Tests a value of the inner DateTime variable and changes it if needed.
            /// </summary>
            private void check()
            {
                if (_type == XDateTimeType.Business && !this.IsWorkDay)
                {
                    _date = this.NextBusinessDay();
                }
            }

            #endregion
        }
    }

    namespace CommonLibrary.SubUtility
    {
        public enum XDateTimeType { Calendar = 0, Business = 1 };
        public class XDateTime
        {
            #region Private Class Member Variable

            private string _format = "MM/dd/yyyy";
            //private DateTime _date;
            private DateTime _date { get; set; }
            private XDateTimeType _type;
            private Hashtable _holidays;

            #endregion

            #region Constructor

            public XDateTime()
            {
                init(DateTime.Now, XDateTimeType.Calendar);
            }

            public XDateTime(string dateTime)
            {
                init(Convert.ToDateTime(dateTime), XDateTimeType.Calendar);
            }

            public XDateTime(string dateTime, XDateTimeType dateType)
            {
                init(Convert.ToDateTime(dateTime), dateType);
                check();
            }

            #endregion

            #region Public methods

            /// <summary>
            /// Gets or sets the type of the Smart Date which is a value of XDateTimeType enumeration
            /// and could be Calendar or Business type. 
            /// </summary>
            /// <remarks>
            /// If you set the DateType to Business type the value of the inner DateTime variable might be
            /// changed to the value of the closest next business date.
            /// </remarks>
            public XDateTimeType DateType
            {
                get { return _type; }
                set
                {
                    _type = value;
                    check();
                }
            }

            /// <summary>
            /// Gets or sets a value of the inner System.DateTime variable of the Smart Date.
            /// </summary>
            /// <remarks>
            /// If you set the Date and the DateType is a Business type the value of the inner DateTime variable
            /// might be changed to the value of the closest next business date.
            /// </remarks>
            public DateTime Date
            {
                get { return _date; }
                set
                {
                    _date = value;
                    check();
                }
            }

            /// <summary>
            /// Returns true if the value of the inner DateTime variable is a public Holiday, otherwise false.
            /// </summary>
            public bool IsHoliday
            {
                get
                {
                    return _holidays.ContainsValue(_date.ToString(_format));
                }
            }

            /// <summary>
            /// Returns true if the value of the inner DateTime variable is a work day, otherwise false.
            /// </summary>
            public bool IsWorkDay
            {
                get
                {
                    return !(_date.DayOfWeek == DayOfWeek.Saturday || _date.DayOfWeek == DayOfWeek.Sunday || this.IsHoliday);
                }
            }

            /// <summary>
            /// Adds given hours to the value of the inner DateTime variable considering the DateType value.
            /// </summary>
            /// <param name="hours">Hours to add.</param>
            public void AddHours(short hours)
            {
                _date = _date.AddHours(Convert.ToDouble(hours));
                check();
            }

            /// <summary>
            /// Adds one business or calendar day depending on the DateType value to the value of the inner DateTime variable.
            /// </summary>
            public void AddDay()
            {
                _date = _date.AddDays(1.0);
                check();
            }

            /// <summary>
            /// Adds given CALENDAR amount of days to the value of the inner DateTime variable. Always considers
            /// a value of DateType property and change the inner date according to this value.
            /// </summary>
            /// <param name="days">Calendar days to add.</param>
            public void AddDays(short days)
            {
                _date = _date.AddDays(Convert.ToDouble(days));
                check();
            }

            /// <summary>
            /// Adds given BUSINESS amount of days to the value of the inner DateTime variable. Always considers
            /// a value of DateType property and change the inner date according to this value.
            /// </summary>
            /// <param name="days">Business days to add.</param>
            public void AddBusinessDays(short days)
            {
                double sign = Convert.ToDouble(Math.Sign(days));
                int unsignedDays = Math.Sign(days) * days;
                for (int i = 0; i < unsignedDays; i++)
                {
                    do
                    {
                        _date = _date.AddDays(sign);
                    }
                    while (!this.IsWorkDay);
                }
            }

            /// <summary>
            /// Returns a new instance of the System.DateTime object with the value of Next Business Day counting from
            /// the value of the XDateTime object.
            /// </summary>
            public DateTime NextBusinessDay()
            {
                DateTime date = _date;
                do
                {
                    date = date.AddDays(1.0);
                }
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                    _holidays.ContainsValue(date.ToString(_format)));
                return date;
            }

            /// <summary>
            /// Returns a new instance of the System.DateTime object with the value of Previous Business Day counting from
            /// the value of the XDateTime object.
            /// </summary>
            public DateTime PreviousBusinessDay()
            {
                //DateTime date = _date;
                DateTime date = DateTime.Today;
                do
                {
                    date = date.AddDays(-1.0);
                }
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                    _holidays.ContainsValue(date.ToString(_format)));
                return date;
            }

            public int NumberOfBusinessDaysFrom(DateTime date)
            {
                double dayToAdd = -1;
                int numberOfBusinessDays = 0;

                if (date < _date)
                {
                    dayToAdd = 1;
                }
                while (_date != date)
                {
                    if (!(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || _holidays.ContainsValue(date.ToString(_format))))
                    {
                        // if date is a working (business) day - increase a counter
                        numberOfBusinessDays++;
                    }
                    date = date.AddDays(dayToAdd);
                }
                return numberOfBusinessDays;
            }
            public int NumberOfBusinessDaysHour(DateTime FromDate)
            {
                int numberOfBusinessDays = 0;
                DateTime ToDate = DateTime.Now;
                if (ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                }
                else if (!ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                }
                return numberOfBusinessDays;
            }
            public int NumberOfBusinessDaysMinute(DateTime FromDate)
            {
                int numberOfBusinessDays = 0;
                DateTime ToDate = DateTime.Now;
                if (ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Minute - FromDate.Minute);
                }
                else if (!ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Minute - FromDate.Minute);
                }
                return numberOfBusinessDays;
            }



            public int NumberOfBusinessDaysFrom(DateTime FromDate,DateTime ToDate)
            {
                double dayToAdd = -1;
                int numberOfBusinessDays = 0;
                if (ToDate.Day > FromDate.Day)
                {
                    dayToAdd = 1;
                    while (ToDate.Day != FromDate.Day)
                    {
                        if (!(FromDate.DayOfWeek == DayOfWeek.Saturday || FromDate.DayOfWeek == DayOfWeek.Sunday || _holidays.ContainsValue(FromDate.ToString(_format))))
                        {
                            // if date is a working (business) day - increase a counter
                            numberOfBusinessDays++;
                        }
                        FromDate = FromDate.AddDays(dayToAdd);
                    }
                }
                return numberOfBusinessDays;
            }
            public int NumberOfBusinessDaysHour(DateTime FromDate, DateTime ToDate)
            {
                int numberOfBusinessDays = 0;
                if (ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                }
                else if (!ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                }
                return numberOfBusinessDays;
            }
            public int NumberOfBusinessDaysMinute(DateTime FromDate, DateTime ToDate)
            {
                int numberOfBusinessDays = 0;
                if ((ToDate.Minute > FromDate.Minute) && ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = (ToDate.Minute - FromDate.Minute);
                }
                else if ((ToDate.Hour > FromDate.Hour) && ToDate.Date.Equals(FromDate.Date))
                {
                    numberOfBusinessDays = Convert.ToInt32(ToDate.TimeOfDay.TotalMinutes - FromDate.TimeOfDay.TotalMinutes);
                }
                return numberOfBusinessDays;
            }


            #endregion


            #region Public Methods for Days Hours Minuts Calculator


            public int CalculateNumberOfBusinessDaysFrom(DateTime FromDate, DateTime ToDate)
            {
                double dayToAdd = -1;
                int numberOfBusinessDays = 0;
                if (ToDate.Day > FromDate.Day)
                {
                    dayToAdd = 1;
                    while (ToDate.Day != FromDate.Day)
                    {
                        if (!(FromDate.DayOfWeek == DayOfWeek.Saturday || FromDate.DayOfWeek == DayOfWeek.Sunday || _holidays.ContainsValue(FromDate.ToString(_format))))
                        {
                            // if date is a working (business) day - increase a counter
                            numberOfBusinessDays++;
                        }
                        FromDate = FromDate.AddDays(dayToAdd);
                    }
                }
                return numberOfBusinessDays;
            }
            public int CalculateNumberOfBusinessDaysHour(DateTime FromDate, DateTime ToDate)
            {
                int numberOfBusinessDays = 0;
                numberOfBusinessDays = (ToDate.Hour - FromDate.Hour);
                return numberOfBusinessDays;
            }
            public int CalculateNumberOfBusinessDaysMinute(DateTime FromDate, DateTime ToDate)
            {
                int numberOfBusinessDays = 0;
                numberOfBusinessDays = Convert.ToInt32(ToDate.TimeOfDay.TotalMinutes - FromDate.TimeOfDay.TotalMinutes);
                return numberOfBusinessDays;
            }

            //(ToDate - FromDate).TotalDays

            #endregion

            #region Private methods

            /// <summary>
            /// Initializes private members of the XDateTime object. For using in the class constructors only.
            /// </summary>
            /// <param name="dateTime">Initial date.</param>
            /// <param name="dateType">Date type.</param>
            private void init(DateTime dateTime, XDateTimeType dateType)
            {
                _date = dateTime;
                _type = dateType;
                initHolidays();
            }


            /// <summary>
            /// Loads a list of public holidays from .config file.
            /// </summary>
            private void initHolidays()
            {
                ////String StrFilePath = System.Windows.Forms.Application.StartupPath.ToString() + @"\HoliDayFile.xml";
                ////_holidays = new Hashtable();
                ////_holidays = (new BusinessCommonLibrary.BusinessCommonLinqXmlManagement().GetHashtableXmlData(StrFilePath, "HoliDay"));

                //////Read holidays from .config file
                _holidays = new Hashtable();
                _holidays.Add("New Years Day", "01/03/2011");
                _holidays.Add("Good Friday", "03/25/2011");
                _holidays.Add("Victoria Day", "05/23/201");
                _holidays.Add("Canada Day", "07/01/2011");
                _holidays.Add("Civic Holiday", "08/01/2011");
                _holidays.Add("Thanksgiving", "10/10/2011");
                _holidays.Add("Christmas Day", "12/26/2011");
                _holidays.Add("Boxing Day", "12/27/2011");
                //////_holidays = (Hashtable)ConfigurationManager.GetSection("Holidays");


            }


            /// <summary>
            /// Tests a value of the inner DateTime variable and changes it if needed.
            /// </summary>
            private void check()
            {
                if (_type == XDateTimeType.Business && !this.IsWorkDay)
                {
                    _date = this.NextBusinessDay();
                }
            }

            #endregion
        }
    }
}