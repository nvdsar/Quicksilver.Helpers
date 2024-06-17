using System.Text;

namespace Quicksilver.StringHelpers
{
    public static class StringHelpers
    {
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
    }
}
