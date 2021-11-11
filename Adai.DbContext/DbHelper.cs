using Adai.DbContext.Extensions;
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
		static object Locker { get; set; }

		/// <summary>
		/// 表特性
		/// </summary>
		public static IDictionary<string, Attributes.TableAttribute> TableAttributes { get; private set; }

		/// <summary>
		/// 数据库配置
		/// </summary>
		public static ICollection<Models.DbConfig> DbConfigs { get; private set; }

		/// <summary>
		/// 执行之前
		/// </summary>
		public static Action<string, IDbCommand> BeforeExecuteAction { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		static DbHelper()
		{
			Locker = new object();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="dbConfigs">数据库配置</param>
		/// <param name="beforeExecute">执行之前执行，可用于记录SQL，第一个参数是初始化时传入的EventId</param>
		public static void Init(ICollection<Models.DbConfig> dbConfigs, Action<string, IDbCommand> beforeExecute = null)
		{
			DbConfigs = dbConfigs;
			BeforeExecuteAction = beforeExecute;
		}

		/// <summary>
		/// 获取映射
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Attributes.TableAttribute GetTableAttribute<T>() where T : class
		{
			return GetTableAttribute(typeof(T));
		}

		/// <summary>
		/// 获取属性
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static Attributes.TableAttribute GetTableAttribute(Type type)
		{
			lock (Locker)
			{
				if (TableAttributes == null)
				{
					TableAttributes = new Dictionary<string, Attributes.TableAttribute>();
				}
				if (!TableAttributes.TryGetValue(type.FullName, out var data))
				{
					var typeA = typeof(Attributes.TableAttribute);
					var attrs = type.GetCustomAttributes(typeA, true);
					if (attrs != null && attrs.Length > 0)
					{
						data = attrs.FirstOrDefault() as Attributes.TableAttribute;
					}
					data.ColumnAttributes = type.GetColumnAttributes<Attributes.ColumnAttribute>();
					TableAttributes.Add(type.FullName, data);
				}
				return data;
			}
		}

		/// <summary>
		/// 获取数据库配置
		/// </summary>
		/// <param name="dbType"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static Models.DbConfig GetDbConfig(Config.DbType dbType, string dbName)
		{
			if (DbConfigs == null || DbConfigs.Count == 0)
			{
				throw new Exception("程序尚未初始化，请先执行Init");
			}
			if (string.IsNullOrEmpty(dbName))
			{
				throw new ArgumentNullException(nameof(dbName));
			}
			var dbConfig = DbConfigs.Where(o => o.DbType == dbType && o.Name == dbName).FirstOrDefault();
			return dbConfig;
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
