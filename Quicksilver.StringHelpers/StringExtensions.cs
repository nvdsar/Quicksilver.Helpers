﻿using Quicksilver.DataConversion;

namespace Quicksilver.StringHelpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts to string and <see cref="string.PadLeft(int)"/> it in the desired manner
        /// </summary>
        /// <param name="str">the object which is going to be called <see cref="string.PadLeft(int)"/> on it</param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static string ToPadString(this object str, int totalWidth, char paddingChar)
        {
            if (str.IsNullObject()) str = string.Empty;
            return str.ToString().PadLeft(totalWidth, paddingChar);
        }
        /// <summary>
        /// Do <see cref="string.Substring(int,int)"/> and <see cref="string.Trim()"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubstringTrim(this string str, int startIndex, int length)
        {
            return str.Substring(startIndex, length).Trim();
        }
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return $"{char.ToLowerInvariant(str[0])}{str.Substring(1)}";
        }
        /// <summary>
        /// Remove characters like ' or " from string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveQuotation(this string str)
        {
            return str.ReplaceWithEmpty("'", "\"");
        }
        /// <summary>
        /// Removes any characters in <paramref name="values"/> from <paramref name="str"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="values">Characters to remove</param>
        /// <returns></returns>
        public static string ReplaceWithEmpty(this string str, params string[] values)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            foreach (var v in values)
            {
                str = str.Replace(v, "");
            }
            return str;
        }
        /// <summary>
        /// While there is a <paramref name="oldChar"/> in <paramref name="str"/> do a transformation to <paramref name="newChar"/>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldChar"></param>
        /// <param name="newChar"></param>
        /// <returns></returns>
        public static string ReplaceWhile(this string str, string oldChar, string newChar)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            while (str.Contains(oldChar))
                str = str.Replace(oldChar, newChar);
            return str;
        }
        /// <summary>
        /// Check if there are more than one space in the <paramref name="str"/> and change it to one space character
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SpaceTrim(this string str) => str.ReplaceWhile("  ", " ").Trim();
        /// <summary>
        /// Remove everything after the last character of <paramref name="trimWord"/> from the end of the <paramref name="source"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="trimWord"></param>
        /// <returns></returns>
        public static string TrimEnd(this string source, string trimWord)
        {
            return source.Remove(source.LastIndexOf(trimWord) + trimWord.Count());
        }
    }
}