using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace AhDai.Core.Metadata;

/// <summary>
/// 高性能配置元数据与字典转换工具
/// </summary>
public static class OptionProvider
{
    /// <summary>
    /// 从枚举中极速提取字典项列表
    /// </summary>
    public static OptionItem<T>[] FromEnum<T>(T[]? includeValues = null, T[]? excludeValues = null) where T : struct, Enum
    {
        var allItems = EnumOptionCache<T>.CachedItems;

        if (includeValues == null && excludeValues == null)
            return allItems;

        var includeSet = includeValues?.ToFrozenSet();
        var excludeSet = excludeValues?.ToFrozenSet();

        var result = new List<OptionItem<T>>(allItems.Length);
        foreach (var item in allItems)
        {
            if (includeSet != null && !includeSet.Contains(item.Value)) continue;
            if (excludeSet != null && excludeSet.Contains(item.Value)) continue;
            result.Add(item);
        }

        return [.. result];
    }

    /// <summary>
    /// 从静态常量类中极速提取字符串字典项列表
    /// </summary>
    public static OptionItem<string>[] FromClass<TClass>(string[]? includeValues = null, string[]? excludeValues = null) where TClass : class
    {
        return FromClass<TClass, string>(includeValues, excludeValues);
    }

    /// <summary>
    /// 从静态常量类中极速提取指定类型的字典项列表
    /// </summary>
    public static OptionItem<TValue>[] FromClass<TClass, TValue>(TValue[]? includeValues = null, TValue[]? excludeValues = null) where TClass : class
    {
        var allItems = ClassOptionCache<TClass, TValue>.CachedItems;

        if (includeValues == null && excludeValues == null)
            return allItems;

        var includeSet = includeValues?.ToFrozenSet();
        var excludeSet = excludeValues?.ToFrozenSet();

        var result = new List<OptionItem<TValue>>(allItems.Length);
        foreach (var item in allItems)
        {
            if (includeSet != null && !includeSet.Contains(item.Value)) continue;
            if (excludeSet != null && excludeSet.Contains(item.Value)) continue;
            result.Add(item);
        }

        return [.. result];
    }

    #region JIT 运行时专属高速无锁缓存舱

    /// <summary>
    /// 针对枚举的强类型隔离缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    static class EnumOptionCache<T> where T : struct, Enum
    {
        public static readonly OptionItem<T>[] CachedItems;

        static EnumOptionCache()
        {
            var type = typeof(T);
            var fields = type.GetFields();
            var list = new List<OptionItem<T>>();

            foreach (var field in fields)
            {
                if (!field.IsLiteral) continue;

                var value = (T)field.GetRawConstantValue()!;
                var attribute = field.GetCustomAttribute<DisplayAttribute>();

                list.Add(new OptionItem<T>(
                    value,
                    attribute?.GetName() ?? field.Name,
                    attribute?.GetOrder() ?? 99
                ));
            }

            CachedItems = [.. list.OrderBy(x => x.Order).ThenBy(x => Convert.ToInt64(x.Value))];
        }
    }

    /// <summary>
    /// 针对静态常量类的强类型隔离缓存
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    private static class ClassOptionCache<TClass, TValue> where TClass : class
    {
        public static readonly OptionItem<TValue>[] CachedItems;

        static ClassOptionCache()
        {
            var type = typeof(TClass);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var list = new List<OptionItem<TValue>>();

            foreach (var field in fields)
            {
                var obj = field.IsLiteral ? field.GetRawConstantValue() : field.GetValue(null);
                if (obj == null) continue;
                if (obj is not TValue value) continue;

                var attribute = field.GetCustomAttribute<DisplayAttribute>();
                list.Add(new OptionItem<TValue>(value, attribute?.GetName() ?? field.Name ?? string.Empty, attribute?.GetOrder() ?? 99));
            }

            CachedItems = [.. list.OrderBy(x => x.Order).ThenBy(x => x.Name)];
        }
    }
    #endregion
}