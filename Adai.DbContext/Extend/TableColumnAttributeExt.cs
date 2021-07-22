using Adai.DbContext.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adai.DbContext.Extend
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
			var attribute = attributes.Where(o => string.Compare(o.Name, name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
			if (attribute == null)
			{
				attribute = attributes.Where(o => string.Compare(o.Property.Name, name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
			}
			return attribute;
		}
	}
}
