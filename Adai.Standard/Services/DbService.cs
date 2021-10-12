using Adai.DbContext;
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
		/// 构造函数
		/// </summary>
		/// <param name="configuration"></param>
		public DbService(IConfiguration configuration)
		{
			Configuration = configuration;
			var dbs = new string[] { "db0", "db1" };
			var dbConfigs = new List<DbContext.Models.DbConfig>();
			foreach (var db in dbs)
			{
				dbConfigs.Add(new DbContext.Models.DbConfig()
				{
					DbType = DbContext.Config.DbType.MySQL,
					Name = db,
					ConnectionString = configuration.GetSection($"db:{db}").Value
				});
			}
			Utils.DbContextHelper.Init(dbConfigs);
		}

		/// <summary>
		/// Configuration
		/// </summary>
		public IConfiguration Configuration { get; private set; }

		/// <summary>
		/// GetDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public IDbContext GetDbContext(string eventId, string dbName = null)
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
