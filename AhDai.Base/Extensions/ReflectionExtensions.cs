using System;
using System.ComponentModel;
using System.Reflection;

namespace AhDai.Base.Extensions
{
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
        public static void SetValueExt(this PropertyInfo propertyInfo, object obj, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                propertyInfo.SetValue(obj, null);
                return;
            }

            var targetType = propertyInfo.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (underlyingType.IsInstanceOfType(value))
            {
                propertyInfo.SetValue(obj, value);
                return;
            }

            if (targetType.FullName.StartsWith("System.Collections") || targetType.FullName.EndsWith("[]"))
            {
                return;
            }

            object convertedValue;
            try
            {
                if (underlyingType.IsEnum)
                {
                    convertedValue = Enum.Parse(underlyingType, value.ToString(), true);
                }
                else if (underlyingType == typeof(Guid))
                {
                    convertedValue = Guid.Parse(value.ToString()!);
                }
                else if (value is string strValue)
                {
                    var converter = TypeDescriptor.GetConverter(underlyingType);
                    if (converter.CanConvertFrom(typeof(string)))
                    {
                        convertedValue = converter.ConvertFromInvariantString(strValue);
                    }
                    else
                    {
                        convertedValue = Convert.ChangeType(value, underlyingType);
                    }
                }
                else
                {
                    convertedValue = Convert.ChangeType(value, underlyingType);
                }
            }
            catch
            {
                throw new InvalidCastException($"无法将值 '{value}' ({value.GetType().Name}) 转换为属性 '{propertyInfo.Name}' 的目标类型 {targetType.Name}");
            }

            propertyInfo.SetValue(obj, convertedValue);
        }
    }
}
