using AhDai.Core.Extensions;
using AhDai.DbContext.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;

namespace AhDai.Core.Utils;

/// <summary>
/// DbContextUtil
/// </summary>
public static class DbContextUtil
{
    /// <summary>
    /// 配置
    /// </summary>
    public static IDictionary<string, DbConfig> Configs { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    static DbContextUtil()
    {
        Configs = new Dictionary<string, DbConfig>();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="configs"></param>
    public static void Init(ICollection<DbConfig> configs)
    {
        foreach (var config in configs)
        {
            Configs.Add(config.Name, config);
        }
        DbContext.DbHelper.Init(configs, BeforeExecute);
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DbConfig GetConfig(string name = "Default")
    {
        return Configs[name];
    }

    /// <summary>
    /// GetSqlDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public static DbContext.IDbContext GetSqlDbContext(string eventId, string? dbName = null)
    {
        var connStr = "";
        if (!string.IsNullOrEmpty(dbName))
        {
            var config = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.MSSQL, dbName);
            if (config == null || string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentException($"未配置类型是MSSQL，别名是{dbName}的连接字符串");
            }
            connStr = config.ConnectionString;
        }
        return new DbContext.SqlClient.SqlDbContext(eventId, connStr);
    }

    /// <summary>
    /// GetMySqlDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public static DbContext.IDbContext GetMySqlDbContext(string eventId, string? dbName = null)
    {
        var connStr = "";
        if (!string.IsNullOrEmpty(dbName))
        {
            var config = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.MySQL, dbName);
            if (config == null || string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentException($"未配置类型是MySQL，别名是{dbName}的连接字符串");
            }
            connStr = config.ConnectionString;
        }
        return new DbContext.MySql.MySqlDbContext(eventId, connStr);
    }

    /// <summary>
    /// GetOracleDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public static DbContext.IDbContext GetOracleDbContext(string eventId, string? dbName = null)
    {
        var connStr = "";
        if (!string.IsNullOrEmpty(dbName))
        {
            var config = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.Oracle, dbName);
            if (config == null || string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentException($"未配置类型是Oracle，别名是{dbName}的连接字符串");
            }
            connStr = config.ConnectionString;
        }
        return new DbContext.Oracle.OracleDbContext(eventId, connStr);
    }

    /// <summary>
    /// GetMySqlDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public static DbContext.IDbContext GetSQLiteDbContext(string eventId, string? dbName = null)
    {
        var connStr = "";
        if (!string.IsNullOrEmpty(dbName))
        {
            var config = DbContext.DbHelper.GetDbConfig(DbContext.Config.DbType.SQLite, dbName);
            if (config == null || string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentException($"未配置类型是SQLite，别名是{dbName}的连接字符串");
            }
            connStr = config.ConnectionString;
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
        var parameters = "";
        foreach (IDbDataParameter para in command.Parameters)
        {
            parameters += $"{para.ParameterName}={para.Value},";
        }
        var type = typeof(DbContextUtil);
        var logger = LoggerUtil.GetLogger(type.FullName ?? type.Name);
        logger.LogDebug(new EventId(0, eventId), "SQL=>{CommandText};Parameters=>{Parameters}", command.CommandText, parameters);
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static int ExecuteNonQuery(IDbConnection connection, string sql, IDictionary<string, object> parameters)
    {
        using var command = CreateCommand(connection, sql, parameters);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static ICollection<T> ExecuteReader<T>(IDbConnection connection, string sql, IDictionary<string, object> parameters, Func<IDataReader, T> converter)
    {
        using var command = CreateCommand(connection, sql, parameters);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        using var reader = command.ExecuteReader();
        var list = new List<T>();
        while (reader.Read())
        {
            var t = converter.Invoke(reader);
            list.Add(t);
        }
        return list;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static object? ExecuteScalar(IDbConnection connection, string sql, IDictionary<string, object> parameters)
    {
        using var command = CreateCommand(connection, sql, parameters);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return command.ExecuteScalar();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <param name="converter"></param>
    /// <returns></returns>
    public static T ExecuteScalar<T>(IDbConnection connection, string sql, IDictionary<string, object> parameters, Func<object?, T> converter)
    {
        var obj = ExecuteScalar(connection, sql, parameters);
        return converter.Invoke(obj);
    }

    /// <summary>
    /// CreateCommand
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static IDbCommand CreateCommand(IDbConnection connection, string sql, IDictionary<string, object> parameters)
    {
        var command = connection.CreateCommand();
        command.CommandText = sql;
        if (parameters != null)
        {
            foreach (var kv in parameters)
            {
                var p = command.CreateParameter();
                p.ParameterName = kv.Key;
                p.Value = kv.Value;
                command.Parameters.Add(p);
            }
        }
        return command;
    }
}
