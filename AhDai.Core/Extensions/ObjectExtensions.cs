using AhDai.Core.Metadata;
using System;
using System.Collections.Generic;

namespace AhDai.Core.Extensions;

/// <summary>
/// ObjectExt
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// 克隆
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="obj"></param>
    /// <param name="ignores">忽略的属性</param>
    public static T Clone<T>(this T obj, params string[] ignores) where T : class, new()
    {
        if (obj == null) return new T();

        var data = new T();
        var ignoreSet = ignores != null && ignores.Length > 0 ? new HashSet<string>(ignores, StringComparer.OrdinalIgnoreCase) : null;
        var properties = TypeMetadataProvider.GetProperties<T>();
        foreach (var meta in properties)
        {
            var pi = meta.Info;
            if (pi.CanRead && pi.CanWrite)
            {
                if (ignoreSet != null && ignoreSet.Contains(pi.Name))
                {
                    continue;
                }

                var value = pi.GetValue(obj, null);
                pi.SetValue(data, value);
            }
        }
        return data;
    }

    /// <summary>
    /// 是否最小值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsMinValue(this object obj, Type? type = null)
    {
        var min = obj.GetDefaultMinValue(type);
        return obj.Equals(min);
    }

    /// <summary>
    /// 获取最小值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object? GetDefaultMinValue(this object obj, Type? type = null)
    {
        type ??= obj.GetType();

        var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
        return Type.GetTypeCode(underlyingType) switch
        {
            TypeCode.Byte => byte.MinValue,
            TypeCode.SByte => sbyte.MinValue,
            TypeCode.Char => char.MinValue,
            TypeCode.String => string.Empty,
            TypeCode.Int16 => short.MinValue,
            TypeCode.Int32 => int.MinValue,
            TypeCode.Int64 => long.MinValue,
            TypeCode.UInt16 => ushort.MinValue,
            TypeCode.UInt32 => uint.MinValue,
            TypeCode.UInt64 => ulong.MinValue,
            TypeCode.Boolean => false,
            TypeCode.Single => float.MinValue,
            TypeCode.Double => double.MinValue,
            TypeCode.Decimal => decimal.MinValue,
            TypeCode.DateTime => DateTime.MinValue,
            _ => null
        };
    }
}
