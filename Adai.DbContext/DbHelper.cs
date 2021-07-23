using Adai.DbContext.Extend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Adai.DbContext
{
	/// <summary>
	/// DbHelper
	/// </summary>
	public static class DbHelper
	{
		/// <summary>
		/// 表特性
		/// </summary>
		static IDictionary<string, Attribute.TableAttribute> TableAttributes;

		/// <summary>
		/// 获取映射
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Attribute.TableAttribute GetTableAttribute<T>() where T : class
		{
			return GetTableAttribute(typeof(T));
		}

		/// <summary>
		/// 获取属性
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Attribute.TableAttribute GetTableAttribute(Type type)
		{
			if (TableAttributes == null)
			{
				TableAttributes = new Dictionary<string, Attribute.TableAttribute>();
			}
			if (!TableAttributes.TryGetValue(type.FullName, out var attribute))
			{
				var attrs = type.GetCustomAttributes(true);
				if (attrs != null)
				{
					attribute = attrs.FirstOrDefault() as Attribute.TableAttribute;
					if (attribute != null)
					{
						attribute.ColumnAttributes = type.GetPropertyAttributes<Attribute.TableColumnAttribute>();
					}
					TableAttributes.Add(type.FullName, attribute);
				}
			}
			return attribute;
		}

		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		public static IDictionary<string, string> ConnectionStrings;

		/// <summary>
		/// 已初始化
		/// </summary>
		public static bool Initialized { get; internal set; }

		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static string GetConnectionString(string dbName)
		{
			if (!Initialized)
			{
				throw new Exception("程序尚未初始化，请先执行Startup.Init");
			}
			if (ConnectionStrings.TryGetValue(dbName, out var connStr))
			{
				return connStr;
			}
			throw new Exception($"获取数据库{dbName}的连接字符串失败");
		}

		/// <summary>
		/// AddRange
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="values"></param>
		public static void AddRange(this IDataParameterCollection collection, Array values)
		{
			foreach (var value in values)
			{
				collection.Add(value);
			}
		}

		/// <summary>
		/// 生成 In 语句，一千条一个In
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="column"></param>
		/// <param name="values"></param>
		/// <param name="notIn"></param>
		/// <returns></returns>
		public static string CreateInSql<T>(string column, T[] values, bool notIn = false)
		{
			var key_word0 = "OR";
			var key_word1 = "IN";
			if (notIn)
			{
				key_word0 = "AND";
				key_word1 = "NOT IN";
			}
			var type = typeof(T);
			var builder = new StringBuilder();
			int skip = 0, take = 1000;
			if (type.FullName == "System.String")
			{
				while (skip < values.Length)
				{
					var _values = values.Skip(skip).Take(take);
					builder.Append($"{key_word0} {column} {key_word1} ('{string.Join("','", _values)}')");
					skip += take;
				}
			}
			else
			{
				while (skip < values.Length)
				{
					var _values = values.Skip(skip).Take(take);
					builder.Append($"{key_word0} {column} {key_word1} ({string.Join(",", _values)})");
					skip += take;
				}
			}
			var sql = "";
			if (builder.Length > 0)
			{
				sql = builder.Remove(0, key_word0.Length + 1).ToString();
				if (skip > 0)
				{
					sql = $"({sql})";
				}
			}
			return sql;
		}

		#region 复杂方法
		#endregion
	}
}
