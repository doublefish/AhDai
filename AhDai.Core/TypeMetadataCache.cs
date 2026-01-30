using AhDai.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace AhDai.Core;

/// <summary>
/// 类型属性元数据中心
/// </summary>
public class TypeMetadataCache
{
    static readonly ConcurrentDictionary<Type, PropertyMetadata[]> _cache = new();

    /// <summary>
    /// 获取指定类型的属性元数据列表
    /// </summary>
    public static PropertyMetadata[] GetProperties(Type type)
    {
        return _cache.GetOrAdd(type, t =>
        {
            return [.. t.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => new PropertyMetadata(p))];
        });
    }

    /// <summary>
    /// 泛型版本，方便调用
    /// </summary>
    public static PropertyMetadata[] GetProperties<T>() => GetProperties(typeof(T));
}
