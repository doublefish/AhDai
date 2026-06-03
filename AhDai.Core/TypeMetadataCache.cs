using AhDai.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AhDai.Core;

/// <summary>
/// 类型属性元数据中心
/// </summary>
public class TypeMetadataCache
{
    static readonly ConcurrentDictionary<Type, PropertyMetadata[]> _arrayCache = new();
    static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyMetadata>> _dictCache = new();

    /// <summary>
    /// 获取属性元数据列表
    /// </summary>
    public static PropertyMetadata[] GetProperties(Type type)
    {
        return _arrayCache.GetOrAdd(type, t => [.. t.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => new PropertyMetadata(p))]);
    }

    /// <summary>
    /// 获取属性元数据列表
    /// </summary>
    public static PropertyMetadata[] GetProperties<T>() => GetProperties(typeof(T));

    /// <summary>
    /// 获取不区分大小写的属性名与属性元数据映射字典
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Dictionary<string, PropertyMetadata> GetPropertyDict(Type type)
    {
        var properties = GetProperties(type);
        return _dictCache.GetOrAdd(type, t => properties.ToDictionary(p => p.Info.Name, p => p, StringComparer.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 获取不区分大小写的属性名与属性元数据映射字典
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Dictionary<string, PropertyMetadata> GetPropertyDict<T>() => GetPropertyDict(typeof(T));
}
