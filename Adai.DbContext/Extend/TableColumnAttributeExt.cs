using Adai.DbContext.Attribute;
using System;
using System.Collections.Generic;

namespace Adai.DbContext.Ext
{
	/// <summary>
	/// TableColumnAttributeExt
	/// </summary>
	public static class TableColumnAttributeExt
	{
		/// <summary>
		/// 查询：匹配列名或者属性名
		/// </summary>
		/// <param name="attributes"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static TableColumnAttribute Find(this IEnumerable<TableColumnAttribute> attributes, string name)
		{
			TableColumnAttribute attribute = null;
			foreach (var attr in attributes)
			{
				if (string.Compare(attr.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					attribute = attr;
					break;
				}
				if (string.Compare(attr.Property.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					attribute = attr;
					break;
				}
			}
			return attribute;
		}
	}
}
