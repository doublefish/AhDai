using AhDai.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;

namespace AhDai.Core.Extensions;

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
        if (dataTable == null || dataTable.Rows.Count == 0) return [];

        var list = new List<T>(dataTable.Rows.Count);

        var propDict = TypeMetadataCache.GetPropertyDict<T>();
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
        var propDict = TypeMetadataCache.GetPropertyDict<T>();
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

    /// <summary>
    /// ToList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataReader"></param>
    /// <returns></returns>
    public static ICollection<T> ToList<T>(this IDataReader dataReader) where T : class, new()
    {
        var list = new List<T>();
        var propDict = TypeMetadataCache.GetPropertyDict<T>();
        var fieldCount = dataReader.FieldCount;
        
        var activeFields = new (PropertyMetadata Meta, int Index)[fieldCount];
        var activeFieldCount = 0;
        for (var i = 0; i < fieldCount; i++)
        {
            var name = dataReader.GetName(i);
            if (propDict.TryGetValue(name, out var pi))
            {
                activeFields[activeFieldCount++] = (pi, i);
            }
        }

        while (dataReader.Read())
        {
            var data = new T();
            for (var i = 0; i < activeFieldCount; i++)
            {
                var field = activeFields[i];
                var value = dataReader.GetValue(field.Index);

                if (value != null && value != DBNull.Value)
                {
                    field.Meta.Info.SetValueExt(data, value);
                }
            }
            list.Add(data);
        }
        return list;
    }
}
