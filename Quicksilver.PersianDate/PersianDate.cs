using System.Globalization;
using System.Text.RegularExpressions;
using Quicksilver.DataConversion;
using Quicksilver.StringHelpers;

namespace Quicksilver.PersianDate
{
    public static class PersianDate
    {
        private static PersianCalendar p = new PersianCalendar();
        private static string patternPersianDate = @"((\d{4}|\d{2})[\/-_]?\d{2}[\/-_]?\d{2})";
        /// <summary>
        /// Convert <see langword="Gregorian"/> datetime to <see langword="Persian (Solar Hijri)"/> string 
        /// </summary>
        /// <param name="date"><see langword="Gregorian"/> datetime</param>
        /// <returns>YYYY/MM/dd</returns>
        public static string ToPersianDate(this DateTime date)
        {
            return string.Format("{0}/{1}/{2}", p.GetYear(date).ToPadString(4, '0'), p.GetMonth(date).ToPadString(2, '0'), p.GetDayOfMonth(date).ToPadString(2, '0'));
        }
        /// <summary>
        /// Convert <see langword="Gregorian"/> datetime to <see langword="Persian (Solar Hijri)"/> string 
        /// </summary>
        /// <param name="date"><see langword="Gregorian"/> datetime</param>
        /// <returns>YY/MM/dd</returns>
        public static string To2DigitYearPersianDate(this DateTime dt)
        {
            return string.Format("{0}/{1}/{2}", p.GetYear(dt).ToString().Substring(2, 2).PadLeft(2, '0'), p.GetMonth(dt).ToPadString(2, '0'), p.GetDayOfMonth(dt).ToPadString(2, '0'));
        }
        /// <summary>
        /// Convert <see langword="Gregorian"/> datetime to <see langword="Persian (Solar Hijri)"/> string 
        /// </summary>
        /// <param name="date"><see langword="Gregorian"/> datetime</param>
        /// <returns>YYMMdd</returns>
        public static string To6DigitPersianDate(this DateTime dt)
        {
            return string.Format("{0}{1}{2}", p.GetYear(dt).ToString().Substring(2, 2).PadLeft(2, '0'), p.GetMonth(dt).ToPadString(2, '0'), p.GetDayOfMonth(dt).ToPadString(2, '0'));
        }
        /// <summary>
        /// Convert <see langword="Gregorian"/> datetime to <see langword="Persian (Solar Hijri)"/> string 
        /// </summary>
        /// <param name="date"><see langword="Gregorian"/> datetime</param>
        /// <returns>YYYYMMdd</returns>
        public static string To8DigitPersianDate(this DateTime dt)
        {
            return string.Format("{0}{1}{2}", p.GetYear(dt).ToPadString(4, '0'), p.GetMonth(dt).ToPadString(2, '0'), p.GetDayOfMonth(dt).ToPadString(2, '0'));
        }
        /// <summary>
        /// Convert <see langword="Gregorian"/> datetime to <see langword="Persian (Solar Hijri)"/> string 
        /// </summary>
        /// <param name="date"><see langword="Gregorian"/> datetime</param>
        /// <returns>YY/MM/dd hh:mm:ss</returns>
        public static string ToPersianDateTime(this DateTime dt)
        {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}", p.GetYear(dt).ToPadString(4, '0'), p.GetMonth(dt).ToPadString(2, '0'),
                p.GetDayOfMonth(dt).ToPadString(2, '0'), dt.Hour.ToPadString(2, '0'), dt.Minute.ToPadString(2, '0'), dt.Second.ToPadString(2, '0'));
        }
        /// <summary>
        /// Convert <see langword="Gregorian"/> datetime to <see langword="Persian (Solar Hijri)"/> string 
        /// </summary>
        /// <param name="date"><see langword="Gregorian"/> datetime</param>
        /// <returns>YYYYMMdd HHmm</returns>
        public static string ToPersianShortDateTime(this DateTime date)
        {
            var persianDate = date.To8DigitPersianDate();
            return $"{persianDate} {date.ToString("HHmm")}";
        }

        /// <summary>
        /// Convert <see langword="Persian (Solar Hijri)"/> string to <see langword="Gregorian"/> datetime
        /// </summary>
        /// <param name="datePersian">A string representing the Solar Hijri date, which can be formatted as follows: 
        /// <br>yyyyMMdd</br>
        /// <br>yyyy/MM/dd</br>
        /// <br>yyyy\MM\dd</br>
        /// <br>yyyy-MM-dd</br>
        /// <br>yyyy_MM_dd</br>
        /// </param>
        /// <returns></returns>
        public static DateTime ToGregorianDate(this string datePersian)
        {
            if (Regex.IsMatch(datePersian, patternPersianDate) == false)
                throw new Exception("There is no Persian date in input parameter.");
            datePersian = Regex.Match(datePersian, patternPersianDate).Groups[1].Value;
            datePersian = datePersian.ReplaceWithEmpty("\\", "/", "_", "-");
            bool is_8_letterDate = datePersian.Length == 8;
            var i = is_8_letterDate ? 4 : 2;
            var bYear = datePersian.Substring(0, i).ToInt32();
            var bMonth = datePersian.Substring(i, 2).ToInt32();
            i += 2;
            var bDay = datePersian.Substring(i, 2).ToInt32();
            if (is_8_letterDate == false)
                bYear += bYear < 50 ? 1400 : 1300;
            return p.ToDateTime(bYear, bMonth, bDay, 0, 0, 0, 0);
        }
        /// <summary>
        /// Get time of the datetime as short format of hh:mm
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortTimeString(this DateTime? date) => date?.ShortTimeString();
        /// <summary>
        /// Get time of the datetime as short format of hh:mm
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortTimeString(this DateTime date) => date.ToString("hh:mm");
        /// <summary>
        /// Get yyMMdd format of the date as string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortDateString(this DateTime? date) => date?.ShortDateString();
        /// <summary>
        /// Get yyMMdd format of the date as string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortDateString(this DateTime date) => date.ToString("yyMMdd");
        /// <summary>
        /// Get yyyyMMdd format of the date as string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortDateString8(this DateTime? date) => date?.ShortDateString8();
        /// <summary>
        /// Get yyyyMMdd format of the date as string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ShortDateString8(this DateTime date) => date.ToString("yyyyMMdd");
        /// <summary>
        /// Get the first day of Solar Hijri (Persian) year
        /// </summary>
        /// <param name="year">Year as Persian</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfSolarHijri(int year)
        {
            var yearString = year.ToString();
            if (yearString.Length < 4)
            {
                var initialYear = year > 50 ? 1300 : 1400;
                year = initialYear + year.ToPadString(4, '0').Substring(2, 2).ToInt32();
            }
            return p.ToDateTime(year, 1, 1, 0, 0, 0, 0);
        }
        /// <summary>
        /// Get Persian name of week day based on number of the day
        /// </summary>
        /// <param name="dayNum">The number of the day</param>
        /// <returns></returns>
        /// <exception cref="Exception">In case of calling year less than 0 or more than 6 </exception>
        public static string GetDayOfWeek(int dayNum)
        {
            return dayNum switch
            {
                0 => "شنبه",
                1 => "يكشنبه",
                2 => "دوشنبه",
                3 => "سه شنبه",
                4 => "چهارشنبه",
                5 => "پنج شنبه",
                6 => "جمعه",
                _ => throw new Exception(string.Format("Invalid Day of week number {0}", dayNum)),
            };
        }
        /// <summary>
        /// Get Persian name of week day based on <see cref="DayOfWeek"></see> provided
        /// </summary>
        /// <param name="dayNum"><see cref="DayOfWeek"/></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DayOfWeek dayNum)
        {
            return dayNum switch
            {
                DayOfWeek.Sunday => "يكشنبه",
                DayOfWeek.Monday => "دوشنبه",
                DayOfWeek.Tuesday => "سه شنبه",
                DayOfWeek.Wednesday => "چهارشنبه",
                DayOfWeek.Thursday => "پنج شنبه",
                DayOfWeek.Friday => "جمعه",
                DayOfWeek.Saturday => "شنبه",
                _ => throw new Exception(string.Format("Invalid Day of week number {0}", dayNum)),
            };
        }
        /// <summary>
        /// Get Persian name of month based on number referenced to the month
        /// </summary>
        /// <param name="monthNum"></param>
        /// <returns></returns>
        public static string GetMonthName(object monthNum)
        {
            return monthNum.ToByte() switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => "نا معتبر",
            };
        }

    }
}
