#region Usings

using System;

#endregion

namespace GraySystem
{
    /// <summary>
    /// Summary description for DateTimeExtended.
    /// </summary>
    public class DateTimeExtended
    {
        #region Fields

        DateTime _dtDateTime;

        #endregion

        #region Properties

        #region Now

        public static DateTimeExtended Now
        {
            get { return (new DateTimeExtended(DateTime.Now)); }
        } // end Now property

        #endregion

        #endregion

        #region Constructors

        public DateTimeExtended(string sDateTime)
        {
            _dtDateTime = new DateTime(Convert.ToInt32(sDateTime.Substring(0, 4)),
                                       Convert.ToInt32(sDateTime.Substring(5, 2)),
                                       Convert.ToInt32(sDateTime.Substring(8, 2)),
                                       Convert.ToInt32(sDateTime.Substring(11, 2)),
                                       Convert.ToInt32(sDateTime.Substring(14, 2)),
                                       Convert.ToInt32(sDateTime.Substring(17, 2)));

        }

        public DateTimeExtended(DateTime dateTime)
        {
            _dtDateTime = dateTime;
        } // end DateTimeExtended constructor

        #endregion

        public DateTimeExtended AddHours(double timeOffset)
        {
            return new DateTimeExtended(_dtDateTime.AddHours(timeOffset));
        }

        private string GetFormattedYear(string sFormat)
        {
            int iLength = sFormat.Length - sFormat.TrimStart('y').Length;

            if (iLength <= 2)
            {
                return (_dtDateTime.Year.ToString().Substring(2, 2));
            } // end if
            else
            {
                return (_dtDateTime.Year.ToString().Substring(0, 4));
            } // end else
        } // end GetFormattedYear

        private string GetFormattedMonth(string sFormat)
        {
            int iLength = sFormat.Length - sFormat.TrimStart('M').Length;

            if (iLength == 1)
            {
                return (_dtDateTime.Month.ToString());
            } // end if
            else if (iLength == 2)
            {
                return (_dtDateTime.Month.ToString().PadLeft(2, '0'));
            } // end else
            else if (iLength == 3)
            {
                // GetShortMonth
                return (_dtDateTime.Month.ToString().PadLeft(2, '0'));
            } // end else
            else
            {
                // GetLongMonth
                return (_dtDateTime.Month.ToString().PadLeft(2, '0'));
            } // end else
        } // end GetFormattedMonth

        private string GetFormattedDay(string sFormat)
        {
            int iLength = sFormat.Length - sFormat.TrimStart('d').Length;

            if (iLength == 1)
            {
                return (_dtDateTime.Day.ToString());
            } // end if
            else if (iLength == 2)
            {
                return (_dtDateTime.Day.ToString().PadLeft(2, '0'));
            } // end else
            else if (iLength == 3)
            {
                // GetShortDay
                return (_dtDateTime.Day.ToString().PadLeft(2, '0'));
            } // end else
            else
            {
                // GetLongDay
                return (_dtDateTime.Day.ToString().PadLeft(2, '0'));
            } // end else
        } // end GetFormattedDay

        private string GetFormattedHour(string sFormat)
        {
            int iLength = sFormat.Length - sFormat.TrimStart('h').Length;

            if (iLength == 1)
            {
                return (_dtDateTime.Hour.ToString());
            } // end if
            else
            {
                return (_dtDateTime.Hour.ToString().PadLeft(2, '0'));
            } // end else
        } // end GetFormattedHour

        private string GetFormattedMinute(string sFormat)
        {
            int iLength = sFormat.Length - sFormat.TrimStart('m').Length;

            if (iLength == 1)
            {
                return (_dtDateTime.Minute.ToString());
            } // end if
            else
            {
                return (_dtDateTime.Minute.ToString().PadLeft(2, '0'));
            } // end else
        } // end GetFormattedMinute

        private string GetFormattedSecond(string sFormat)
        {
            int iLength = sFormat.Length - sFormat.TrimStart('s').Length;

            if (iLength == 1)
            {
                return (_dtDateTime.Second.ToString());
            } // end if
            else
            {
                return (_dtDateTime.Second.ToString().PadLeft(2, '0'));
            } // end else
        } // end GetFormattedSecond

        public string ToString(string sFormat)
        {
            switch (sFormat[0])
            {
                case 'y':
                    return (GetFormattedYear(sFormat));
                case 'M':
                    return (GetFormattedMonth(sFormat));
                case 'd':
                    return (GetFormattedDay(sFormat));
                case 'h':
                    return (GetFormattedHour(sFormat));
                case 'm':
                    return (GetFormattedMinute(sFormat));
                case 's':
                    return (GetFormattedSecond(sFormat));
                default:
                    return ("");
            } // end switch
        } // end ToString
    }
}
