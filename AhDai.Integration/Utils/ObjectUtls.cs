using AhDai.Core.Metadata;
using System;
using System.Collections.Generic;

namespace AhDai.Integration.Utils;

internal class ObjectUtls
{
    /// <summary>
    /// ToSortedDictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static SortedDictionary<string, string?> ToSortedDictionary<T>(T obj, StringComparer? comparer = null) where T : class
    {
        var dict = new SortedDictionary<string, string?>(comparer ?? StringComparer.Ordinal);
        if (obj != null)
        {
            ProcessObject("", obj, dict);
        }
        return dict;
    }

    /// <summary>
    /// ProcessObject
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="obj"></param>
    /// <param name="dict"></param>
    static void ProcessObject(string prefix, object obj, IDictionary<string, string?> dict)
    {
        if (obj == null) return;
        var props = TypeMetadataProvider.GetProperties(obj.GetType());
        foreach (var p in props)
        {
            var value = p.Info.GetValue(obj);
            if (value == null) continue;
            var jsonName = p.JsonName ?? p.Info.Name;
            var key = string.IsNullOrEmpty(prefix) ? jsonName : $"{prefix}.{jsonName}";
            if (value.GetType().IsClass && value.GetType() != typeof(string))
            {
                ProcessObject(key, value, dict);
            }
            else
            {
                dict[key] = value.ToString() ?? string.Empty;
            }
        }
    }
}
