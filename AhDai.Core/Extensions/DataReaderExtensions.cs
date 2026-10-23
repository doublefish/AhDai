using AhDai.Core.Metadata;
using System;
using System.Collections.Generic;
using System.Data;

namespace AhDai.Core.Extensions;

/// <summary>
/// DataReaderExtensions
/// </summary>
public static class DataReaderExtensions
{

    /// <summary>
    /// ToList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataReader"></param>
    /// <returns></returns>
    public static ICollection<T> ToList<T>(this IDataReader dataReader) where T : class, new()
    {
        var list = new List<T>();
        var propDict = TypeMetadataProvider.GetPropertyDict<T>();
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
