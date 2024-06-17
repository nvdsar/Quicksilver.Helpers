using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace Quicksilver.DataConversion
{
    public static class ValueTypeExtensions
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles };

        /// <summary>
        /// Check if object is null
        /// </summary>
        /// <param name="obj">The object to check nullability</param>
        /// <returns><see cref="bool"/></returns>
        public static bool IsNullObject(this object obj)
        {
            return obj == DBNull.Value || obj == null || string.IsNullOrEmpty(obj?.ToString()?.Trim());
        }
        /// <summary>
        /// Try to convert to <see cref="decimal"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static decimal? ToNullableDecimal(this object obj)
        {
            if (obj.IsNullObject()) return null;
            decimal.TryParse(obj.ToString(), NumberStyles.Any, null, out var result);
            return result;
        }
        /// <summary>
        /// Try to convert to <see cref="double"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static double? ToNullableDouble(this object obj)
        {
            if (obj.IsNullObject()) return null;
            double.TryParse(obj.ToString(), NumberStyles.Any, null, out var result);
            return result;
        }
        /// <summary>
        /// Try to convert to <see cref="bool"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static bool? ToNullableBoolean(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Convert.ToBoolean(obj);
        }
        /// <summary>
        /// Try to convert to <see cref="byte"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static byte? ToNullableByte(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Convert.ToByte(obj);
        }
        /// <summary>
        /// Try to convert to <see cref="short"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static short? ToNullableInt16(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Convert.ToInt16(obj);
        }
        /// <summary>
        /// Try to convert to <see cref="int"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static int? ToNullableInt32(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// Try to convert to <see cref="long"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static long? ToNullableInt64(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Convert.ToInt64(obj);
        }
        /// <summary>
        /// Try to convert to <see cref="byte[]"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return (byte[])Convert.ChangeType(obj, typeof(byte[]));
        }
        /// <summary>
        /// Try to convert to <see cref="byte[]"/> by serializing the object to <see href="JSON"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj, _options));
            //return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }
        /// <summary>
        /// Try to convert to <see cref="Guid"/>, if not possible return <seealso cref="Guid.Empty"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static Guid ToGuid(this object obj)
        {
            if (Guid.TryParse(obj?.ToString(), out Guid result))
                return result;
            return Guid.Empty;
        }
        /// <summary>
        /// Try to convert to <see cref="Guid"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static Guid? ToNullableGuid(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return obj.ToGuid();
        }
        /// <summary>
        /// Try to convert to <see cref="string"/>, if not possible return <seealso href="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static string ToNullableString(this object obj) => obj.IsNullObject() == false ? obj.ToString() : null;
        /// <summary>
        /// Try to convert to <see cref="string"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static string ToString(this object obj, string defaultValue)
        {
            return obj.ToNullableString() ?? defaultValue;
        }
        /// <summary>
        /// Try to convert to <see cref="DateTime"/>, if not possible return <seealso cref="null"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(this object obj)
        {
            if (obj.IsNullObject()) return null;
            return Convert.ToDateTime(obj);
        }
        /// <summary>
        /// Try to convert to <see cref="decimal"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0) => obj.ToNullableDecimal().GetValueOrDefault(defaultValue);
        /// <summary>
        /// Try to convert to <see cref="double"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defaultValue = 0) => obj.ToNullableDouble().GetValueOrDefault(defaultValue);
        /// <summary>
        /// Try to convert to <see cref="int"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static int ToInt32(this object obj, int defaultValue = 0) => obj.ToNullableInt32().GetValueOrDefault(defaultValue);
        /// <summary>
        /// Try to convert to <see cref="long"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static long ToInt64(this object obj, long defaultValue = 0) => obj.ToNullableInt64().GetValueOrDefault(defaultValue);
        /// <summary>
        /// Try to convert to <see cref="short"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static short ToInt16(this object obj, short defaultValue = 0) => obj.ToNullableInt16().GetValueOrDefault(defaultValue);
        /// <summary>
        /// Try to convert to <see cref="byte"/>, if not possible returns <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static byte ToByte(this object obj, byte defaultValue = 0) => obj.ToNullableByte().GetValueOrDefault(defaultValue);
        /// <summary>
        /// Try to convert to <see cref="bool"/>, if not possible returns <see href="false"/>
        /// </summary>
        /// <param name="obj">The object to convert</param>
        /// <param name="defaultValue">The default value for null possibilities</param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj) => obj.ToNullableBoolean().GetValueOrDefault(false);
        /// <summary>
        /// Try to convert to DateTime, if not possible then Add Days to <see cref="DateTime.MinValue"/>
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <param name="daysAfterToday">Add days to <see cref="DateTime.MinValue"/> if null</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj) => obj.IsNullObject() == false ? Convert.ToDateTime(obj) : DateTime.MinValue;
        /// <summary>
        /// Try to convert to DateTime, if not possible then Add Days to <see cref="DateTime.Now"/>
        /// </summary>
        /// <param name="obj">Object to convert</param>
        /// <param name="daysAfterToday">Add days to <see cref="DateTime.Now"/> if null</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, int daysAfterToday) => obj.IsNullObject() == false ? Convert.ToDateTime(obj) : DateTime.Now.AddDays(daysAfterToday);

    }
}
