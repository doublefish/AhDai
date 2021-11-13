using Adai.DbContext.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Adai.DbContext
{
	/// <summary>
	/// DbContextExtensions
	/// </summary>
	public static class DbContextExtensions
	{
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		static string GetConnectionString(this IDbContext dbContext, string dbName)
		{
			var dbConfig = DbHelper.GetDbConfig(dbContext.DbType, dbName);
			if (dbConfig == null || string.IsNullOrEmpty(dbConfig.ConnectionString))
			{
				throw new ArgumentException($"未配置类型是{dbContext.DbType.ToName()}，别名是{dbName}的数据库配置");
			}
			return dbConfig.ConnectionString;
		}

		/// <summary>
		/// 变更连接字符串
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		static void ChangeConnectionString(this IDbContext dbContext, string dbName)
		{
			var connStr = dbContext.GetConnectionString(dbName);
			dbContext.ConnectionString = connStr;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static DataSet GetDataSet(this IDbContext dbContext, string dbName, string sql, params IDbDataParameter[] parameters)
		{
			dbContext.ChangeConnectionString(dbName);
			return dbContext.GetDataSet(sql, parameters);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static T GetSingle<T>(this IDbContext dbContext, string dbName, string sql, params IDbDataParameter[] parameters) where T : class, new()
		{
			return dbContext.GetList<T>(dbName, sql, parameters).FirstOrDefault();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static ICollection<T> GetList<T>(this IDbContext dbContext, string sql, params IDbDataParameter[] parameters) where T : class, new()
		{
			var ds = dbContext.GetDataSet(sql, parameters);
			var tableAttr = DbHelper.GetTableAttribute<T>();
			if (tableAttr == null)
			{
				return ds.Tables[0].ToList<T>();
			}
			var mappings = tableAttr.ColumnAttributes.GetMappings();
			return ds.Tables[0].ToList<T>(mappings);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static ICollection<T> GetList<T>(this IDbContext dbContext, string dbName, string sql, params IDbDataParameter[] parameters) where T : class, new()
		{
			dbContext.ChangeConnectionString(dbName);
			return dbContext.GetList<T>(sql, parameters);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static object ExecuteScalar(this IDbContext dbContext, string dbName, string sql, params IDbDataParameter[] parameters)
		{
			dbContext.ChangeConnectionString(dbName);
			return dbContext.ExecuteScalar(sql, parameters);
		}

		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static int ExecuteNonQuery(this IDbContext dbContext, string dbName, string sql, params IDbDataParameter[] parameters)
		{
			dbContext.ChangeConnectionString(dbName);
			return dbContext.ExecuteNonQuery(sql, parameters);
		}

		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbName"></param>
		/// <param name="commands"></param>
		/// <returns></returns>
		public static int ExecuteNonQuery(this IDbContext dbContext, string dbName, params IDbCommand[] commands)
		{
			dbContext.ChangeConnectionString(dbName);
			return dbContext.ExecuteNonQuery(commands);
		}

		/// <summary>
		/// 跨库执行
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dict">connStr-cmds</param>
		/// <returns></returns>
		public static int ExecuteNonQuery(this IDbContext dbContext, IDictionary<string, ICollection<IDbCommand>> dict)
		{
			var result = 0;
			var conns = new List<IDbConnection>();
			var trans = new List<IDbTransaction>();
			try
			{
				foreach (var kv in dict)
				{
					var conn = dbContext.CreateConnection();
					conn.ConnectionString = kv.Key;
					var cmds = kv.Value;
					conn.Open();
					conns.Add(conn);
					var tran = conn.BeginTransaction();
					trans.Add(tran);
					foreach (var cmd in cmds)
					{
						cmd.Connection = conn;
						cmd.Transaction = tran;
						dbContext.BeforeExecute(cmd);
						result += cmd.ExecuteNonQuery();
					}
				}
				foreach (var tran in trans)
				{
					tran.Commit();
				}
			}
			catch
			{
				foreach (var tran in trans)
				{
					tran.Rollback();
				}
				throw;
			}
			finally
			{
				foreach (var conn in conns)
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 跨库执行
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dict">dbName-cmds</param>
		/// <returns></returns>
		public static int ExecuteNonQueryByDbName(this IDbContext dbContext, IDictionary<string, ICollection<IDbCommand>> dict)
		{
			var _dict = new Dictionary<string, ICollection<IDbCommand>>();
			foreach (var kv in dict)
			{
				var connStr = dbContext.GetConnectionString(kv.Key);
				_dict.Add(connStr, kv.Value);
			}
			return dbContext.ExecuteNonQuery(_dict);
		}

		/// <summary>
		/// 创建查询条件
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="filter"></param>
		/// <param name="alias"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string CreateQueryCondition<T>(this IDbContext dbContext, IFilter<T> filter, string alias, out IDbDataParameter[] parameters) where T : class
		{
			var tableAttr = DbHelper.GetTableAttribute(filter.Self.GetType());
			if (tableAttr == null)
			{
				throw new Exception("未设置表特性");
			}
			var columns = tableAttr.ColumnAttributes;
			var sql = new StringBuilder();
			var paras = new List<IDbDataParameter>();
			foreach (var column in columns)
			{
				if (column.Type == Attributes.ColumnType.External)
				{
					continue;
				}
				var pi = column.Property;
				var value = pi.GetValue(filter.Self);
				if (value == null)
				{
					continue;
				}
				if (value.IsMinValue())
				{
					continue;
				}
				sql.Append($" AND {alias}.{column.Name}=@{column.Name}");
				paras.Add(dbContext.CreateParameter(column.Name, value));
			}
			if (!string.IsNullOrEmpty(filter.SortName))
			{
				var sortName = filter.SortName;
				if (!sortName.Contains('.'))
				{
					var sortColumn = columns.Find(filter.SortName);
					sortName = $"{alias}.{sortColumn.Name}";
				}
				else
				{
					// 主表外的字段需要是实际的字段名，后面有空看怎么扩展成自动获取的吧
				}
				var sortType = filter.SortType == Config.SortType.DESC ? "DESC" : "ASC";
				sql.Append($" ORDER BY {sortName} {sortType}");
			}
			if (filter.Take > 0)
			{
				switch (dbContext.DbType)
				{
					case Config.DbType.MSSQL:
						// 2012版本以上
						sql.Append($" OFFSET {filter.Skip} ROWS FETCH NEXT {filter.Take} ROWS ONLY");
						break;
					case Config.DbType.MySQL:
					case Config.DbType.SQLite:
						//sql.Append($" LIMIT {filter.Skip},{filter.Take}");
						sql.Append($" LIMIT {filter.Take} OFFSET {filter.Skip}");
						break;
					case Config.DbType.Oracle:
						//select a.* from ( select t.*,rownum rowno from test t where rownum <= 20 ) a where a.rowno >= 11;
						sql.Insert(0, $"SELECT t1.* FROM (SELECT t0.*,ROWNUM ROWNO FROM (");
						sql.Append($") t0 WHERE ROWNUM <= {filter.Take + filter.Skip} ) t1 WHERE t1.ROWNO > {filter.Skip}");
						break;
					default:
						throw new Exception("无法识别的数据库类型");
				}
			}
			parameters = paras.ToArray();
			return sql.ToString();
		}

		/// <summary>
		/// 生成InsertCommand
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="data"></param>
		/// <param name="tableName">可为空，读取实体特性</param>
		/// <returns></returns>
		public static IDbCommand CreateInsertCommand<T>(this IDbContext dbContext, T data, string tableName = null) where T : class
		{
			var sql = dbContext.CreateInsertSql(data, tableName, out var paras);
			var cmd = dbContext.CreateCommand();
			cmd.CommandText = sql;
			if (paras?.Length > 0)
			{
				cmd.Parameters.AddRange(paras);
			}
			return cmd;
		}

		/// <summary>
		/// 生成UpdateCommand（根据主键修改）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="data"></param>
		/// <param name="updateColumns"></param>
		/// <param name="whereColumn"></param>
		/// <param name="tableName">可为空，读取实体特性</param>
		/// <returns></returns>
		public static IDbCommand CreateUpdateCommand<T>(this IDbContext dbContext, T data, string[] updateColumns, string whereColumn, string tableName = null) where T : class
		{
			return dbContext.CreateUpdateCommand(data, updateColumns, new string[] { whereColumn }, tableName);
		}

		/// <summary>
		/// 生成UpdateCommand
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="data"></param>
		/// <param name="updateColumns"></param>
		/// <param name="whereColumns"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public static IDbCommand CreateUpdateCommand<T>(this IDbContext dbContext, T data, string[] updateColumns, string[] whereColumns, string tableName = null) where T : class
		{
			var sql = dbContext.CreateUpdateSql(data, tableName, updateColumns, whereColumns, out var paras);
			var cmd = dbContext.CreateCommand();
			cmd.CommandText = sql;
			if (paras?.Length > 0)
			{
				cmd.Parameters.AddRange(paras);
			}
			return cmd;
		}

		/// <summary>
		/// 生成Insert语句
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="data"></param>
		/// <param name="tableName">可为空，读取实体特性</param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string CreateInsertSql<T>(this IDbContext dbContext, T data, string tableName, out IDbDataParameter[] parameters) where T : class
		{
			var tableAttr = DbHelper.GetTableAttribute<T>();
			if (tableAttr == null)
			{
				throw new Exception("未设置表特性");
			}
			if (string.IsNullOrEmpty(tableName))
			{
				tableName = tableAttr.Name;
			}
			var columns = tableAttr.ColumnAttributes;
			var builder0 = new StringBuilder();
			var builder1 = new StringBuilder();
			var paras = new List<IDbDataParameter>();
			foreach (var column in columns)
			{
				if (column.Type == Attributes.ColumnType.External)
				{
					continue;
				}
				var pi = column.Property;
				var value = pi.GetValue(data);
				if (value == null)
				{
					continue;
				}
				if (value.IsMinValue())
				{
					continue;
				}
				builder0.Append($",{column.Name}");
				builder1.Append($",@{column.Name}");
				paras.Add(dbContext.CreateParameter(column.Name, value));
			}
			builder0 = builder0.Remove(0, 1);
			builder1 = builder1.Remove(0, 1);
			parameters = paras.ToArray();
			var sql = $"INSERT INTO {tableName} ({builder0}) VALUES ({builder1})";
			return sql;
		}

		/// <summary>
		/// 生成Update语句
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbContext"></param>
		/// <param name="data"></param>
		/// <param name="tableName">可为空，读取实体特性</param>
		/// <param name="updateColumns"></param>
		/// <param name="whereColumns"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string CreateUpdateSql<T>(this IDbContext dbContext, T data, string tableName, string[] updateColumns, string[] whereColumns, out IDbDataParameter[] parameters) where T : class
		{
			var tableAttr = DbHelper.GetTableAttribute<T>();
			if (tableAttr == null)
			{
				throw new Exception("未设置表特性");
			}
			if (string.IsNullOrEmpty(tableName))
			{
				tableName = tableAttr.Name;
			}
			var columns = tableAttr.ColumnAttributes;
			if (whereColumns == null || whereColumns.Length == 0)
			{
				//throw new Exception("未指定参数whereColumns");
				throw new ArgumentNullException(nameof(whereColumns));
			}
			var builderSet = new StringBuilder();
			var builderWhere = new StringBuilder();
			var paras = new List<IDbDataParameter>();
			foreach (var updateColumn in updateColumns)
			{
				var column = columns.Find(updateColumn);
				if (column == null || column.Type == Attributes.ColumnType.External)
				{
					continue;
				}
				var pi = column.Property;
				var value = pi.GetValue(data);
				if (value == null)
				{
					continue;
				}
				if (value.IsMinValue())
				{
					continue;
				}
				builderSet.Append($",{column.Name}=@{column.Name}");
				paras.Add(dbContext.CreateParameter(column.Name, value));
			}
			builderSet = builderSet.Remove(0, 1);

			foreach (var whereColumn in whereColumns)
			{
				var column = columns.Find(whereColumn);
				if (column == null || column.Type == Attributes.ColumnType.External)
				{
					throw new ArgumentException($"找不到{whereColumn}对应的列");
				}
				var pi = column.Property;
				var value = pi.GetValue(data);
				if (value == null || value.IsMinValue())
				{
					throw new ArgumentNullException(whereColumn);
				}
				builderWhere.Append($"AND {column.Name}=@{column.Name}");
				paras.Add(dbContext.CreateParameter(column.Name, value));
			}
			builderWhere = builderWhere.Remove(0, 4);

			parameters = paras.ToArray();
			var sql = $"UPDATE {tableName} SET {builderSet} WHERE {builderWhere}";
			return sql;
		}

		#region 私有方法

		#endregion
	}
}
