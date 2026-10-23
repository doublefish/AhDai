using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace AhDai.Core.Extensions;

/// <summary>
/// ReflectionExtensions
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// 赋值-自动转换类型
    /// </summary>
    /// <param name="propertyInfo"></param>
    /// <param name="obj"></param>
    /// <param name="value"></param>
    public static void SetValueExt(this PropertyInfo propertyInfo, object obj, object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            propertyInfo.SetValue(obj, null);
            return;
        }

        var targetType = propertyInfo.PropertyType;
        var nullableType = Nullable.GetUnderlyingType(targetType);
        var underlyingType = nullableType ?? targetType;

        if (value is null || value is DBNull)
        {
            // 引用类型和可空值类型可以赋 null。
            // 非可空值类型保持对象当前值不变。
            if (!targetType.IsValueType || nullableType is not null)
            {
                propertyInfo.SetValue(obj, null);
            }
            return;
        }

        var stringValue = value as string;

        // 空字符串或纯空白字符串：
        // 对可空值类型赋 null，例如 DateTime?、decimal?。
        // string 属性保留原始字符串。
        if (stringValue is not null && string.IsNullOrWhiteSpace(stringValue) && nullableType is not null)
        {
            if (Nullable.GetUnderlyingType(targetType) != null)
            {
                propertyInfo.SetValue(obj, null);
                return;
            }
        }

        var valueType = value.GetType();

        // 类型一致时直接赋值
        if (targetType == valueType || underlyingType == valueType)
        {
            propertyInfo.SetValue(obj, value);
            return;
        }

        // 不在这里自动转换数组和集合类型
        if (underlyingType.IsArray || underlyingType.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(underlyingType))
        {
            return;
        }

        object? convertedValue;
        try
        {
            if (underlyingType.IsEnum)
            {
                convertedValue = stringValue is not null
                   ? Enum.Parse(underlyingType, stringValue, ignoreCase: true)
                   : Enum.ToObject(underlyingType, value);
            }
            else if (underlyingType == typeof(Guid))
            {
                convertedValue = stringValue is not null
                    ? Guid.Parse(stringValue)
                    : (Guid)value;
            }
            else if (stringValue is not null)
            {
                var typeCode = Type.GetTypeCode(underlyingType);
                if (typeCode != TypeCode.Object)
                {
                    convertedValue = Convert.ChangeType(stringValue, underlyingType, CultureInfo.InvariantCulture);
                }
                else
                {
                    var converter = TypeDescriptor.GetConverter(underlyingType);
                    convertedValue = converter.ConvertFromInvariantString(stringValue);
                }
            }
            else
            {
                convertedValue = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"无法将值 '{value}' ({valueType.Name}) 转换为属性 '{propertyInfo.Name}' 的目标类型 {targetType.Name}。内部错误: {ex.Message}", ex);
        }

        propertyInfo.SetValue(obj, convertedValue);
    }
}
