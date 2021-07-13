using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Adai.DbContext.Ext
{
	/// <summary>
	/// DataTableExt
	/// </summary>
	public static class DataTableExt
	{
		/// <summary>
		/// ToList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataTable"></param>
		/// <param name="tableColumns"></param>
		/// <returns></returns>
		public static ICollection<T> ToList<T>(this DataTable dataTable, Attribute.TableColumnAttribute[] tableColumns) where T : class
		{
			var list = new List<T>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				var data = Activator.CreateInstance<T>();
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					var name = dataColumn.ColumnName;
					var value = dataRow[name];
					SetValue(data, name, value, tableColumns);
				}
				list.Add(data);
			}
			return list;
		}

		/// <summary>
		/// ToList
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataReader"></param>
		/// <param name="tableColumns"></param>
		/// <returns></returns>
		public static ICollection<T> ToList<T>(this IDataReader dataReader, Attribute.TableColumnAttribute[] tableColumns) where T : class
		{
			var list = new List<T>();
			while (dataReader.Read())
			{
				var data = Activator.CreateInstance<T>();
				for (var i = 0; i < dataReader.FieldCount; i++)
				{
					var name = dataReader.GetName(i);
					var value = dataReader[name];
					SetValue(data, name, value, tableColumns);
				}
				list.Add(data);
			}
			return list;
		}

		/// <summary>
		/// 赋值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="columns"></param>
		static void SetValue<T>(T data, string name, object value, ICollection<Attribute.TableColumnAttribute> columns) where T : class
		{
			var column = columns.Where(o => string.Compare(o.Name, name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
			if (column == null)
			{
				column = columns.Where(o => string.Compare(o.Property.Name, name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
			}
			if (column == null || column.Property == null)
			{
				return;
			}
			else
			{
				column.Property.SetValueExt(data, value);
			}
		}
	}
}
