using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AhDai.Core.Extensions;

/// <summary>
/// DictionaryExtensions
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// 排序
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dict"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dict, IComparer<TKey> comparer)
        where TKey : notnull
    {
        if (dict == null || dict.Count == 0) return new Dictionary<TKey, TValue>();

        return new SortedDictionary<TKey, TValue>(dict, comparer);
    }

    /// <summary>
    /// ASCII排序
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static IDictionary<string, TValue> SortByASCII<TValue>(this IDictionary<string, TValue> dict)
    {
        if (dict == null || dict.Count == 0) return new Dictionary<string, TValue>();
        return new SortedDictionary<string, TValue>(dict, StringComparer.Ordinal);
    }

    /// <summary>
    /// 按指定键值筛选并排序
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dict"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> FilterSort<TKey, TValue>(this IDictionary<TKey, TValue> dict, params TKey[] keys)
        where TKey : notnull
    {
        if (dict == null || dict.Count == 0 || keys == null || keys.Length == 0) return new Dictionary<TKey, TValue>();

        var sorted = new Dictionary<TKey, TValue>(keys.Length);
        foreach (var key in keys)
        {
            if (dict.TryGetValue(key, out var value))
            {
                sorted.Add(key, value);
            }
        }
        return sorted;
    }

    /// <summary>
    /// 删除指定键
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dict"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict, params TKey[] keys)
        where TKey : notnull
    {
        if (dict == null || dict.Count == 0 || keys == null || keys.Length == 0) return dict ?? new Dictionary<TKey, TValue>();

        foreach (var key in keys)
        {
            dict.Remove(key);
        }
        return dict;
    }

    /// <summary>
    /// 转换为QueryString
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="ignoreNullOrEmpty"></param>
    /// <param name="ignores"></param>
    /// <returns></returns>
    public static string ToQueryString<T>(this IDictionary<string, T> dict, bool ignoreNullOrEmpty = false, params string[] ignores)
    {
        if (dict.Count == 0) return string.Empty;

        var ignoreSet = ignores != null && ignores.Length > 0 ? new HashSet<string>(ignores, StringComparer.OrdinalIgnoreCase) : null;
        var builder = new StringBuilder(dict.Count * 16);
        foreach (var kvp in dict)
        {
            var valueStr = kvp.Value?.ToString();

            if (ignoreNullOrEmpty && string.IsNullOrEmpty(valueStr)) continue;
            if (ignoreSet != null && ignoreSet.Contains(kvp.Key)) continue;

            if (builder.Length > 0) builder.Append('&');
            builder.Append(kvp.Key).Append('=').Append(valueStr);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 转换为连接字符串
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="separator"></param>
    /// <param name="ignoreNullOrEmpty"></param>
    /// <param name="ignores"></param>
    /// <returns></returns>
    public static string ToJoinString(this IDictionary<string, string> dict, string separator = "", bool ignoreNullOrEmpty = false, params string[] ignores)
    {
        if (dict.Count == 0) return string.Empty;

        var ignoreSet = ignores != null && ignores.Length > 0 ? new HashSet<string>(ignores, StringComparer.OrdinalIgnoreCase) : null;
        var builder = new StringBuilder(dict.Count * 16);
        foreach (var kvp in dict)
        {
            if (ignoreNullOrEmpty && string.IsNullOrEmpty(kvp.Value)) continue;
            if (ignoreSet != null && ignoreSet.Contains(kvp.Key)) continue;

            if (builder.Length > 0 && !string.IsNullOrEmpty(separator)) builder.Append(separator);
            builder.Append(kvp.Value);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 转换为Xml字符串
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="ignores"></param>
    /// <returns></returns>
    public static string ToXmlString(this IDictionary<string, string> dict, params string[] ignores)
    {
        if (dict.Count == 0) return string.Empty;

        var ignoreSet = ignores != null && ignores.Length > 0 ? new HashSet<string>(ignores, StringComparer.OrdinalIgnoreCase) : null;
        var builder = new StringBuilder(dict.Count * 32);
        foreach (var kvp in dict)
        {
            if (ignoreSet != null && ignoreSet.Contains(kvp.Key)) continue;

            builder.Append('<').Append(kvp.Key).Append('>')
                   .Append(kvp.Value)
                   .Append("</").Append(kvp.Key).Append('>');
        }
        return builder.ToString();
    }

    /// <summary>
    /// ToQueryString
    /// </summary>
    /// <param name="pairs"></param>
    /// <param name="ignores"></param>
    /// <returns></returns>
    public static string ToQueryString(this IEnumerable<KeyValuePair<string, StringValues>> pairs, params string[] ignores)
    {
        if ( !pairs.Any()) return string.Empty;

        var ignoreSet = ignores != null && ignores.Length > 0 ? new HashSet<string>(ignores, StringComparer.OrdinalIgnoreCase) : null;
        var builder = new StringBuilder();
        foreach (var kvp in pairs)
        {
            if (kvp.Value.Count == 0) continue;
            if (ignoreSet != null && ignoreSet.Contains(kvp.Key)) continue;

            if (builder.Length > 0) builder.Append('&');
            builder.Append(kvp.Key).Append('=').Append(kvp.Value);
        }
        return builder.ToString();
    }
}
