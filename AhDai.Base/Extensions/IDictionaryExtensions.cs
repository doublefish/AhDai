﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AhDai.Base.Extensions
{
    /// <summary>
    /// IDictionaryExtensions
    /// </summary>
    public static class IDictionaryExtensions
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
        {
            if (dict == null)
            {
                return null;
            }
            var keys = new List<TKey>(dict.Keys);
            keys.Sort(comparer);
            var sorted = new Dictionary<TKey, TValue>();
            foreach (var key in keys)
            {
                sorted.Add(key, dict[key]);
            }
            return sorted;
        }

        /// <summary>
        /// ASCII排序
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static IDictionary<string, TValue> SortByASCII<TValue>(this IDictionary<string, TValue> dict)
        {
            if (dict == null)
            {
                return null;
            }
            var keys = dict.Keys.ToArray();
            Array.Sort(keys, string.CompareOrdinal);
            var sorted = new Dictionary<string, TValue>();
            foreach (var key in keys)
            {
                sorted.Add(key, dict[key]);
            }
            return sorted;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            if (dict == null)
            {
                return null;
            }
            var sorted = new SortedDictionary<TKey, TValue>();
            foreach (var kv in dict)
            {
                sorted.Add(kv.Key, kv.Value);
            }
            return sorted;
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
        {
            if (dict == null)
            {
                return null;
            }
            var sorted = new Dictionary<TKey, TValue>();
            foreach (var key in keys)
            {
                if (!dict.ContainsKey(key))
                {
                    continue;
                }
                sorted.Add(key, dict[key]);
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
        {
            if (dict == null)
            {
                return null;
            }
            foreach (var key in keys)
            {
                if (!dict.ContainsKey(key))
                {
                    continue;
                }
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
            if (dict == null || dict.Count == 0)
            {
                return "";
            }
            var builder = new StringBuilder();
            foreach (var kv in dict)
            {
                if ((ignoreNullOrEmpty == true && (kv.Value == null || kv.Value.Equals(""))) || (ignores != null && ignores.Contains(kv.Key)))
                {
                    continue;
                }
                builder.Append($"&{kv.Key}={kv.Value}");
            }
            return builder.Remove(0, 1).ToString();
        }

        /// <summary>
        /// 转换为连接字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="separator"></param>
        /// <param name="ignoreNullOrEmpty"></param>
        /// <param name="ignores"></param>
        /// <returns></returns>
        public static string ToJoinString(this IDictionary<string, string> dict, string separator = null, bool ignoreNullOrEmpty = false, params string[] ignores)
        {
            if (dict == null || dict.Count == 0)
            {
                return "";
            }
            var builder = new StringBuilder();
            foreach (var kv in dict)
            {
                if ((ignoreNullOrEmpty == true && string.IsNullOrEmpty(kv.Value)) || (ignores != null && ignores.Contains(kv.Key)))
                {
                    continue;
                }
                builder.Append($"{separator}{kv.Value}");
            }
            if (!string.IsNullOrEmpty(separator))
            {
                builder = builder.Remove(0, 1);
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
            if (dict == null || dict.Count == 0)
            {
                return "";
            }
            var builder = new StringBuilder();
            foreach (var kv in dict)
            {
                if (ignores != null && ignores.Contains(kv.Key))
                {
                    continue;
                }
                builder.Append($"<{kv.Key}>{kv.Value}</{kv.Key}>");
            }
            return builder.ToString();
        }
    }
}
