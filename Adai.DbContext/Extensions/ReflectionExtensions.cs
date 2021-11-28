using System;
using System.Reflection;

namespace Adai.DbContext.Extensions
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
				// System.Nullable`1[[System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]
				if (targetType.FullName.StartsWith("System.Nullable"))
				{
					//有值不需要处理
				}
				else
				{
					value = Convert.ChangeType(value, targetType);
				}
			}
			propertyInfo.SetValue(obj, value);
		}
	}
}
