﻿using Adai.DbContext;
using Adai.Standard.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Adai.Standard.Services
{
	/// <summary>
	/// 数据库服务
	/// </summary>
	public class DbService : Interfaces.IDbService
	{
		/// <summary>
		/// 配置
		/// </summary>
		public ICollection<DbContext.Models.DbConfig> Configs { get; private set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public DbService(IConfiguration configuration)
		{
			Configs = configuration.GetDbConfigs();
			Utils.DbContextHelper.Init(Configs);
		}

		/// <summary>
		/// GetMySqlDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public IDbContext GetMySqlDbContext(string eventId, string dbName = null)
		{
			return Utils.DbContextHelper.GetMySqlDbContext(eventId, dbName);
		}

		/// <summary>
		/// GetSQLiteDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="fileName"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public IDbContext GetSQLiteDbContext(string eventId, string fileName = null, string version = "3")
		{
			return Utils.DbContextHelper.GetSQLiteDbContext(eventId, fileName, version);
		}
	}
}
