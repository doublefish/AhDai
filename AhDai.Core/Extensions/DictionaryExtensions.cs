using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AhDai.Core;

/// <summary>
/// DictionaryExtensions
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// ToQueryString
    /// </summary>
    /// <param name="pairs"></param>
    /// <param name="ignores"></param>
    /// <returns></returns>
    public static string ToQueryString(this IEnumerable<KeyValuePair<string, StringValues>> pairs, params string[] ignores)
    {
        if (pairs == null || !pairs.Any())
        {
            return "";
        }
        var builder = new StringBuilder();
        foreach (var kvp in pairs)
        {
            if (kvp.Value.Count == 0 || (ignores != null && ignores.Contains(kvp.Key)))
            {
                continue;
            }
            if (builder.Length > 0) builder.Append('&');
            builder.Append(kvp.Key).Append('=').Append(kvp.Value);
        }
        return builder.ToString();
    }
}
