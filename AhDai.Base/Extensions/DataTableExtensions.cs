using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace AhDai.Base.Extensions
{
    /// <summary>
    /// DataTableExt
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// ToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static ICollection<T> ToList<T>(this DataTable dataTable) where T : class, new()
        {
            var list = new List<T>();
            var propertyInfos = typeof(T).GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var data = dataRow.ToModel<T>(propertyInfos);
                list.Add(data);
            }
            return list;
        }

        /// <summary>
        /// ToModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dataRow, PropertyInfo[]? propertyInfos = null) where T : class, new()
        {
            propertyInfos ??= typeof(T).GetProperties();
            var data = Activator.CreateInstance<T>();
            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                var name = dataColumn.ColumnName;
                var value = dataRow[name];
                var pi = propertyInfos.Where(o => o.Name == name).FirstOrDefault();
                if (pi == null || pi.CanWrite == false)
                {
                    continue;
                }
                pi.SetValueExt(data, value);
            }
            return data;
        }

        /// <summary>
        /// ToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static ICollection<T> ToList<T>(this IDataReader dataReader) where T : class, new()
        {
            var list = new List<T>();
            var properties = typeof(T).GetProperties();
            while (dataReader.Read())
            {
                var data = Activator.CreateInstance<T>();
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    var name = dataReader.GetName(i);
                    var value = dataReader[name];
                    var pi = properties.Where(o => o.Name == name).FirstOrDefault();
                    if (pi == null || pi.CanWrite == false)
                    {
                        continue;
                    }
                    pi.SetValueExt(data, value);
                }
                list.Add(data);
            }
            return list;
        }
    }
}
