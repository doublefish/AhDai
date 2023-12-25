﻿using AhDai.DbContext;
using AhDai.Core.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AhDai.Core.Services.Impl
{
    /// <summary>
    /// 数据库服务
    /// </summary>
    public class DbService : IDbService
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
            Utils.DbContextUtil.Init(Configs);
        }

        /// <summary>
        /// GetSqlDbContext
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IDbContext GetSqlDbContext(string eventId, string dbName = null)
        {
            return Utils.DbContextUtil.GetSqlDbContext(eventId, dbName);
        }

        /// <summary>
        /// GetMySqlDbContext
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IDbContext GetMySqlDbContext(string eventId, string dbName = null)
        {
            return Utils.DbContextUtil.GetMySqlDbContext(eventId, dbName);
        }

        /// <summary>
        /// GetOracleDbContext
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IDbContext GetOracleDbContext(string eventId, string dbName = null)
        {
            return Utils.DbContextUtil.GetOracleDbContext(eventId, dbName);
        }

        /// <summary>
        /// GetSQLiteDbContext
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IDbContext GetSQLiteDbContext(string eventId, string dbName = null)
        {
            return Utils.DbContextUtil.GetSQLiteDbContext(eventId, dbName);
        }

        /// <summary>
        /// GetSQLiteDbContext
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="fileName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IDbContext GetSQLiteDbContext(string eventId, string fileName, string version)
        {
            return Utils.DbContextUtil.GetSQLiteDbContext(eventId, fileName, version);
        }
    }
}
