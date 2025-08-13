using System.Collections.Generic;

namespace AhDai.Base.Extensions
{
    /// <summary>
    /// SortedDictionaryExt
    /// </summary>
    public static class SortedDictionaryExtensions
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="sorted"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this SortedDictionary<TKey, TValue> sorted)
        {
            var dic = new Dictionary<TKey, TValue>();
            foreach (var kvp in sorted)
            {
                dic.Add(kvp.Key, kvp.Value);
            }
            return dic;
        }
    }
}
