using System.Data;
using System.Dynamic;
using System.Reflection;
using Quicksilver.StringHelpers;

namespace Quicksilver.EnumerableHelpers
{
    public static class DynamicListExtensions
    {
        private const string colorCode = "colorcode";
        private const string NullColor = "#fff";
        public static List<dynamic> ToDynamicList(this DataTable table)
        {
            var columnNames = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            return table.ToDynamicList(columnNames);
        }
        public static List<dynamic> ToDynamicList(this DataTable table, List<string> columnNames)
        {
            persistColumn(table, columnNames, colorCode);
            List<dynamic> resultList = table.AsEnumerable().Select(row =>
            {
                dynamic obj = new ExpandoObject();
                dynamic optionsObj = new ExpandoObject();
                var optionColumns = new[] { colorCode };
                foreach (var columnName in columnNames.Where(x => !optionColumns.Contains(x)))
                {
                    ((IDictionary<string, object>)obj)[columnName.ToCamelCase()] = row[columnName];
                }
                if (columnNames.Any(x => x.Equals(colorCode, StringComparison.InvariantCulture)))
                {
                    string color = NullColor;
                    if (row[colorCode] != DBNull.Value && row[colorCode].ToString() != "0")
                    {
                        if (int.TryParse(row[colorCode].ToString(), out var intColorCode))
                            color = $"#{intColorCode:X}";
                        else
                            color = row[colorCode].ToString()!;
                    }
                    ((IDictionary<string, object>)optionsObj)["color"] = color;
                    ((IDictionary<string, object>)obj)["options"] = optionsObj;
                }
                return obj;
            }).ToList();
            return resultList;


        }
        public static List<dynamic> ToDynamicList<T>(this IEnumerable<T> objects)
        {
            var properties = typeof(T).GetProperties().Select(x => x.Name).ToList();
            return objects.ToDynamicList(properties);
        }
        public static List<dynamic> ToDynamicList<T>(this IEnumerable<T> objects, List<string> columnNames)
        {
            persistColumn<T>(columnNames, colorCode);
            List<dynamic> resultList = objects.Select(row =>
            {
                dynamic obj = new ExpandoObject();
                dynamic optionsObj = new ExpandoObject();
                var optionColumns = new[] { colorCode };
                foreach (var columnName in columnNames.Where(x => !optionColumns.Contains(x.ToLower())))
                {
                    var prop = row.GetType().GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    ((IDictionary<string, object>)obj)[columnName.ToCamelCase()] = prop.GetValue(row, null);
                }
                if (columnNames.Any(x => x.ToLower() == colorCode))
                {
                    var color = row.GetType().GetProperty(colorCode, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(row, null)?.ToString();
                    if (string.IsNullOrEmpty(color))
                        color = NullColor;
                    ((IDictionary<string, object>)optionsObj)["color"] = color;
                    ((IDictionary<string, object>)obj)["options"] = optionsObj;
                }
                return obj;
            }).ToList();
            return resultList;
        }
        static void persistColumn<T>(List<string> columnNames, string columnName)
        {
            var containsInObject = typeof(T).GetProperties().Any(x => x.Name.ToLower() == columnName.ToLower());
            if (containsInObject && !columnNames.Contains(columnName))
                columnNames.Add(columnName);
        }
        static void persistColumn(DataTable table, List<string> columnNames, string columnName)
        {
            if (table.Columns.Contains(columnName) && !columnNames.Contains(columnName))
                columnNames.Add(columnName);
        }


    }
}
