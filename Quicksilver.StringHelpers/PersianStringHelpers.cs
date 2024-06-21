using System.Text;

namespace Quicksilver.StringHelpers
{
    public static class PersianStringHelpers
    {
        #region Helper Functions

        /// <summary>
        /// دریافت خارج قسمت
        /// </summary>
        /// <param name="i">صورت کسر</param>
        /// <param name="j">مخرج کسر</param>
        /// <returns></returns>
        private static long GetQuotient(long i, long j)
        {
            return (long)(i / (double)j);
        }
        /// <summary>
        /// دریافت باقی مانده
        /// </summary>
        /// <param name="i">صورت کسر</param>
        /// <param name="j">مخرج کسر</param>
        /// <returns></returns>
        private static long GetRemaining(long i, long j)
        {
            return i - j * GetQuotient(i, j);
        }

        private static string ToOnePersianText(long i)
        {
            //less than 20
            var str = i switch
            {
                0 => " ",
                1 => "یک",
                2 => "دو",
                3 => "سه",
                4 => "چهار",
                5 => "پنج",
                6 => "شش",
                7 => "هفت",
                8 => "هشت",
                9 => "نه",
                10 => "ده",
                11 => "یازده",
                12 => "دوازده",
                13 => "سیزده",
                14 => "چهارده",
                15 => "پانزده",
                16 => "شانزده",
                17 => "هفده",
                18 => "هجده",
                19 => "نوزده",
                20 => "بیست",
                _ => "***",
            };
            return str;
        }
        private static string ToDecimalPersianText(long i)
        {
            var str = i switch
            {
                0 => " ",
                10 => "ده",
                20 => "بیست",
                30 => "سی",
                40 => "چهل",
                50 => "پنجاه",
                60 => "شصت",
                70 => "هفتاد",
                80 => "هشتاد",
                90 => "نود",
                100 => "صد",
                110 => "صدوده",
                120 => "صدوبیست",
                130 => "صدوسی",
                140 => "صدوچهل",
                150 => "صدوپنجاه",
                160 => "صدوشست",
                170 => "صدوهفتاد",
                180 => "صدوهشتاد",
                190 => "صدونود",
                200 => "دویست",
                _ => "***",
            };
            return str;
        }
        private static string ToCenturyPersianText(long i)
        {
            var str = i switch
            {
                0 => " ",
                100 => "صد",
                200 => "دویست",
                300 => "سیصد",
                400 => "چهارصد",
                500 => "پانصد",
                600 => "ششصد",
                700 => "هفتصد",
                800 => "هشتصد",
                900 => "نهصد",
                1000 => "هزار",
                _ => "***",
            };
            return str;
        }
        private static string ToLess_10D_PersianText(long i)
        {
            if (i < 21)
                return ToOnePersianText(i);
            var decimalQ = 10 * GetQuotient(i, 10);
            var str = ToDecimalPersianText(decimalQ) + Space(ToOnePersianText(GetRemaining(i, 10))) + ToOnePersianText(GetRemaining(i, 10));
            return str;
        }
        private static string ToLess_1K_PersianText(long i)
        {
            if (i < 100)
                return ToLess_10D_PersianText(i);
            var str = ToCenturyPersianText(100 * GetQuotient(i, 100)) + Space(ToLess_10D_PersianText(GetRemaining(i, 100))) + ToLess_10D_PersianText(GetRemaining(i, 100));
            return str;
        }
        private static string ToLess_10K_PersianText(long i)
        {
            if (i < 1000)
                return ToLess_1K_PersianText(i);
            var str = ToLess_1K_PersianText(GetQuotient(i, 1000)) + " هزار" + Space(ToLess_1K_PersianText(GetRemaining(i, 1000))) + ToLess_1K_PersianText(GetRemaining(i, 1000));
            return str;
        }
        private static string ToLess_100K_PersianText(long i)
        {
            if (i < 10000)
                return ToLess_10K_PersianText(i);
            var str = ToLess_10K_PersianText(GetQuotient(i, 1000)) + " هزار" + Space(ToLess_1K_PersianText(GetRemaining(i, 1000))) + ToLess_1K_PersianText(GetRemaining(i, 1000));
            return str;
        }
        private static string ToLess_1M_PersianText(long i)
        {
            if (i < 100000)
                return ToLess_100K_PersianText(i);
            //less than 1000000;
            var str = ToLess_1K_PersianText(GetQuotient(i, 1000)) + " هزار" + Space(ToLess_100K_PersianText(GetRemaining(i, 1000))) + ToLess_100K_PersianText(GetRemaining(i, 1000));
            return str;
        }
        private static string ToLess_10M_PersianText(long i)
        {
            if (i < 1000000)
                return ToLess_1M_PersianText(i);
            var str = ToLess_1M_PersianText(GetQuotient(i, 1000000)) + " میلیون" + Space(ToLess_1M_PersianText(GetRemaining(i, 1000000))) + ToLess_1M_PersianText(GetRemaining(i, 1000000));
            return str;
        }
        internal static string ToLess_1B_PersianText(long i)
        {
            if (i < 1000000000)
                return ToLess_10M_PersianText(i);
            //less than 1000000;
            var str = ToLess_10M_PersianText(GetQuotient(i, 1000000000)) + " میلیارد" + Space(ToLess_10M_PersianText(GetRemaining(i, 1000000000))) + ToLess_10M_PersianText(GetRemaining(i, 1000000000));
            str = str.Replace("  ", " ");
            return str;
        }
        private static string Space(string digit)
        {
            return digit == "" || digit == " " ? " " : " و ";
        }
        #endregion

        /// <summary>
        /// Convert multiple Persian Commas to readable Persian comma/and phrase.
        /// </summary>
        /// <param name="text">a text enumerated with multiple words separated by ','</param>
        /// <returns></returns>
        public static string GetPluralComma(string text)
        {
            var texts = text.Split(',','،');
            if (texts.Length <= 1) return text;
            var sb = new StringBuilder();
            sb.Append(texts.FirstOrDefault());
            for (var i = 1; i < texts.Length; i++)
            {
                if (i == texts.Length - 1)
                    sb.Append($" و {texts[i]}");
                else
                    sb.Append($"، {texts[i]}");
            }
            return sb.ToString();
        }
        /// <summary>
        /// Convert multiple Persian Commas to readable Persian comma/and phrase.
        /// </summary>
        /// <param name="list">A list of objects to convert to plural comma</param>
        /// <returns></returns>
        public static string GetPluralComma<T>(List<T> list)
        {
            var sb = new StringBuilder();
            sb.Append(list?.FirstOrDefault()?.ToString() ?? string.Empty);
            for (var i = 1; i < list?.Count; i++)
            {
                if (i == list.Count - 1)
                    sb.Append($" و {list[i]}");
                else
                    sb.Append($"، {list[i]}");
            }
            return sb.ToString();
        }
        public static string ToRankingPersianText(int i)
        {
            var str = i switch
            {
                0 => " ",
                1 => "اول",
                2 => "دوم",
                3 => "سوم",
                4 => "چهارم",
                5 => "پنجم",
                6 => "ششم",
                7 => "هفتم",
                8 => "هشتم",
                9 => "نهم",
                10 => "دهم",
                11 => "یازدهم",
                12 => "دوازدهم",
                13 => "سیزدهم",
                14 => "چهاردهم",
                15 => "پانزدهم",
                16 => "شانزدهم",
                17 => "هفدهم",
                18 => "هجدهم",
                19 => "نوزدهم",
                20 => "بیستم",
                _ => $"{i}ام",
            };
            return str;
        }

    }
}
