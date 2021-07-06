using System;
using System.Collections.Generic;

namespace Adai.DbContext.Ext
{
	/// <summary>
	/// CustomAttributeExt
	/// </summary>
	public static class CustomAttributeExt
	{
		/// <summary>
		/// 读取类的属性的的特性
		/// </summary>
		/// <typeparam name="T">类的属性的特性的类型</typeparam>
		/// <param name="type">类的Type</param>
		/// <returns></returns>
		public static T[] GetPropertyAttributes<T>(this Type type) where T : Attribute.CustomAttribute
		{
			var properties = type.GetProperties();
			var list = new List<T>();
			var typeA = typeof(T);
			foreach (var pi in properties)
			{
				var attrs = pi.GetCustomAttributes(typeA, true);
				if (attrs == null || attrs.Length == 0)
				{
					continue;
				}
				var attr = attrs[0] as T;
				attr.Property = pi;
				list.Add(attr);
			}
			return list.ToArray();
		}
	}
}
