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

        var valueType = value.GetType();
        if (targetType == valueType)
        {
            propertyInfo.SetValue(obj, value);
            return;
        }

        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        if (underlyingType == valueType)
        {
            propertyInfo.SetValue(obj, value);
            return;
        }

        if (underlyingType.IsArray || underlyingType.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(underlyingType))
        {
            return;
        }

        object? convertedValue;
        try
        {
            if (underlyingType.IsEnum)
            {
                if (value is string strEnumValue)
                {
                    convertedValue = Enum.Parse(underlyingType, strEnumValue, true);
                }
                else
                {
                    convertedValue = Enum.ToObject(underlyingType, value);
                }
            }
            else if (underlyingType == typeof(Guid))
            {
                convertedValue = value is string strGuid ? Guid.Parse(strGuid) : (Guid)value;
            }
            else if (value is string strValue)
            {
                var typeCode = Type.GetTypeCode(underlyingType);
                if (typeCode != TypeCode.Object)
                {
                    convertedValue = Convert.ChangeType(strValue, underlyingType, CultureInfo.InvariantCulture);
                }
                else
                {
                    var converter = TypeDescriptor.GetConverter(underlyingType);
                    convertedValue = converter.ConvertFromInvariantString(strValue);
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
