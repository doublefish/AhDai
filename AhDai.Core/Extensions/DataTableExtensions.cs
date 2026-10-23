using AhDai.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Data;

namespace AhDai.Core.Extensions;

/// <summary>
/// DataTableExtensions
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
        if (dataTable == null || dataTable.Rows.Count == 0) return [];

        var list = new List<T>(dataTable.Rows.Count);

        var propDict = TypeMetadataProvider.GetPropertyDict<T>();
        foreach (DataRow dataRow in dataTable.Rows)
        {
            var data = dataRow.ToModel<T>(propDict);
            list.Add(data);
        }
        return list;
    }

    /// <summary>
    /// ToModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public static T ToModel<T>(this DataRow dataRow) where T : class, new()
    {
        var propDict = TypeMetadataProvider.GetPropertyDict<T>();
        return dataRow.ToModel<T>(propDict);
    }

    /// <summary>
    /// ToModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataRow"></param>
    /// <param name="propertyDict"></param>
    /// <returns></returns>
    static T ToModel<T>(this DataRow dataRow, Dictionary<string, PropertyMetadata> propertyDict) where T : class, new()
    {
        var data = new T();
        foreach (DataColumn dataColumn in dataRow.Table.Columns)
        {
            var name = dataColumn.ColumnName;
            if (propertyDict.TryGetValue(name, out var pi))
            {
                var value = dataRow[name];
                if (value != null && value != DBNull.Value)
                {
                    pi.Info.SetValueExt(data, value);
                }
            }
        }
        return data;
    }
}
