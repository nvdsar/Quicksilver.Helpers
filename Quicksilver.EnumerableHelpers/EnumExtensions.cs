using System.Data;

namespace Quicksilver.EnumerableHelpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Check if all members of a reference list contains in source collection
        /// <br/>Method automatically checks for distinction, there is <b>no need</b> for distinction check
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source collection, which is going to be checked with a secondary list</param>
        /// <param name="list">a secondary list which we want to check if all members exist in <paramref name="source"/> </param>
        /// <returns></returns>
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> list) where T : struct
        {
            if (list.Any() == false) return true; return source.Distinct().Intersect(list.Distinct()).Count() == list.Distinct().Count();
        }
        public static bool InEnum<TEnum>(this int value, params TEnum[] enums) where TEnum : struct, Enum
        {
            if (enums == null || enums.Any() == false) return false;
            return enums.Select(x => Convert.ToInt32(x)).Contains(value);
        }
        public static bool InEnum<TEnum>(this int? value, params TEnum[] enums) where TEnum : struct, Enum => Convert.ToInt32(value ?? 0).InEnum(enums);
    }
}
