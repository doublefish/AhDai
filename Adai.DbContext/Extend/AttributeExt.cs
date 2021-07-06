using System;
using System.Collections.Generic;

namespace Adai.DbContext.Ext
{
	/// <summary>
	/// AttributeExt
	/// </summary>
	public static class AttributeExt
	{
		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="attributes"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static T GetByName<T>(this ICollection<T> attributes, string name) where T : Attribute.CustomAttribute
		{
			T data = null;
			foreach (var attr in attributes)
			{
				if (string.Compare(attr.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					data = attr;
					break;
				}
				if (string.Compare(attr.Property.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					data = attr;
					break;
				}
			}
			return data;
		}
	}
}
