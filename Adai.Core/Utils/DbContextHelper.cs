using Adai.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Adai.Core.Utils
{
	/// <summary>
	/// DbContextHelper
	/// </summary>
	public static class DbContextHelper
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="configs"></param>
		public static void Init(ICollection<DbContext.Models.DbConfig> configs)
		{
			DbContext.DbHelper.Init(configs, BeforeExecute);
		}

		/// <summary>
		/// GetSqlDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static DbContext.IDbContext GetSqlDbContext(string eventId, string dbName = null)
		{
			var connStr = "";
			if (!string.IsNullOrEmpty(dbName))
			{
				var dbConfig = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.MSSQL, dbName);
				if (dbConfig != null)
				{
					throw new ArgumentException($"未配置类型是MSSQL，别名是{dbName}的连接字符串");
				}
				connStr = dbConfig.ConnectionString;
			}
			return new DbContext.SqlClient.SqlDbContext(eventId, connStr);
		}

		/// <summary>
		/// GetMySqlDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static DbContext.IDbContext GetMySqlDbContext(string eventId, string dbName = null)
		{
			var connStr = "";
			if (!string.IsNullOrEmpty(dbName))
			{
				var dbConfig = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.MySQL, dbName);
				if (dbConfig != null)
				{
					throw new ArgumentException($"未配置类型是MySQL，别名是{dbName}的连接字符串");
				}
				connStr = dbConfig.ConnectionString;
			}
			return new DbContext.MySql.MySqlDbContext(eventId, connStr);
		}

		/// <summary>
		/// GetOracleDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static DbContext.IDbContext GetOracleDbContext(string eventId, string dbName = null)
		{
			var connStr = "";
			if (!string.IsNullOrEmpty(dbName))
			{
				var dbConfig = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.Oracle, dbName);
				if (dbConfig != null)
				{
					throw new ArgumentException($"未配置类型是Oracle，别名是{dbName}的连接字符串");
				}
				connStr = dbConfig.ConnectionString;
			}
			return new DbContext.Oracle.OracleDbContext(eventId, connStr);
		}

		/// <summary>
		/// GetMySqlDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static DbContext.IDbContext GetSQLiteDbContext(string eventId, string dbName = null)
		{
			var connStr = "";
			if (!string.IsNullOrEmpty(dbName))
			{
				var dbConfig = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.SQLite, dbName);
				if (dbConfig != null)
				{
					throw new ArgumentException($"未配置类型是SQLite，别名是{dbName}的连接字符串");
				}
				connStr = dbConfig.ConnectionString;
			}
			return new DbContext.SQLite.SQLiteDbContext(eventId, connStr);
		}

		/// <summary>
		/// GetSQLiteDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="fileName"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public static DbContext.IDbContext GetSQLiteDbContext(string eventId, string fileName, string version)
		{
			var connStr = $"Data Source={fileName};Version={version};Pooling=true;";
			return new DbContext.SQLite.SQLiteDbContext(eventId, connStr);
		}

		/// <summary>
		/// 执行前执行
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="command"></param>
		static void BeforeExecute(string eventId, IDbCommand command)
		{
			var message = $"SQL=>{command.CommandText};Paras=>";
			foreach (IDbDataParameter para in command.Parameters)
			{
				message += $"{para.ParameterName}={para.Value},";
			}

			var logger = LoggerHelper.GetLogger();
			logger.Debug(eventId, message);
		}
	}
}
