using System;
using System.Linq;
using System.Reflection;

namespace Adai.Base.Extensions
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
				propertyInfo.SetValue(obj, default);
				return;
			}

			var targetType = propertyInfo.PropertyType;
			if (targetType.FullName.StartsWith("System.Collections") || targetType.FullName.EndsWith("[]"))
			{
				return;
			}

			var sourceType = value.GetType();
			if (targetType.FullName != sourceType.FullName)
			{
				// Nullable类型和数据源类型和目标类型不一致
				var type = targetType.GenericTypeArguments.FirstOrDefault();
				if (type == null || type.FullName != sourceType.FullName)
				{
					value = Convert.ChangeType(value, type);
				}
			}
			propertyInfo.SetValue(obj, value);
		}
	}
}
