using AhDai.DbContext;

namespace AhDai.Core.Services;

/// <summary>
/// 数据库服务
/// </summary>
public interface IBaseDbService
{
    /// <summary>
    /// GetSqlDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public IDbContext GetSqlDbContext(string eventId, string? dbName = null);

    /// <summary>
    /// GetMySqlDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public IDbContext GetMySqlDbContext(string eventId, string? dbName = null);

    /// <summary>
    /// GetOracleDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public IDbContext GetOracleDbContext(string eventId, string? dbName = null);

    /// <summary>
    /// GetSQLiteDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public IDbContext GetSQLiteDbContext(string eventId, string? dbName = null);

    /// <summary>
    /// GetSQLiteDbContext
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="fileName"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public IDbContext GetSQLiteDbContext(string eventId, string fileName, string version);
}
