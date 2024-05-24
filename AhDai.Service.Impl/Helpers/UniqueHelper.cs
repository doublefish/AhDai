using AhDai.Core.Services;
using AhDai.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Helpers;

/// <summary>
/// UniqueHelper
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal class UniqueHelper<TEntity> where TEntity : class, IBaseEntity
{
    /// <summary>
    /// Db
    /// </summary>
    protected Microsoft.EntityFrameworkCore.DbContext Db { get; private set; }
    /// <summary>
    /// RedisDb
    /// </summary>
    protected IDatabase RedisDb { get; private set; }
    /// <summary>
    /// RedisKey后缀
    /// </summary>
    protected string? RedisSuffix { get; set; }
    /// <summary>
    /// 附件后缀
    /// </summary>
    public string? AdditionalSuffix { get; set; }
    /// <summary>
    /// 附件条件
    /// </summary>
    public Expression<Func<TEntity, bool>>? AdditionalConstraint { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="withTenant"></param>
    public UniqueHelper(Microsoft.EntityFrameworkCore.DbContext db)
    {
        ArgumentNullException.ThrowIfNull(db);
        Db = db;
        var redis = MyApp.Services.GetRequiredService<IBaseRedisService>();
        RedisDb = redis.GetDatabase();
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    /// <param name="id"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public async Task<bool> ExistAsync(Expression<Func<TEntity, string>> selector, string value, long id, string? errorMessage = null)
    {
        try
        {
            return await ExistAsync(new Func<Task<bool>>(() =>
            {
                return Task.FromResult(true);
            }), selector, value, id, errorMessage);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="success"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    /// <param name="id"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<TResult> ExistAsync<TResult>(Func<Task<TResult>> success, Expression<Func<TEntity, string>> selector, string value, long id, string? errorMessage = null)
    {
        ArgumentNullException.ThrowIfNull(success);
        ArgumentNullException.ThrowIfNull(selector);
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(id);

        var parameter = selector.Parameters.Single();
        var property = selector.Body;
        var member = (MemberExpression)property;

        errorMessage ??= parameter.Name + "已存在";

        var key = MyConst.Redis.GenerateKey<TEntity>("Unique", member.Member.Name, RedisSuffix ?? "") + (string.IsNullOrEmpty(AdditionalSuffix) ? "" : ":" + AdditionalSuffix);
        var key2 = $"{key}:Temp";

        var step = 0;
        try
        {
            var exist = await RedisDb.HashGetAsync(key, value);
            if (exist.HasValue && !exist.Equals(id))
            {
                throw new ArgumentException(errorMessage);
            }

            var increment = await RedisDb.HashIncrementAsync(key2, value, 1);
            step++;
            if (increment > 1)
            {
                throw new ArgumentException(errorMessage);
            }

            var constant = Expression.Constant(value);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);

            var query = Db.Set<TEntity>().Where(lambda).Where(x => x.Id != id);
            if (AdditionalConstraint != null)
            {
                query = query.Where(AdditionalConstraint);
            }
            var entity = await query.FirstOrDefaultAsync();
            if (entity != null)
            {
                await RedisDb.HashSetAsync(key, value, entity.Id);
                throw new ArgumentException(errorMessage);
            }

            return await success.Invoke();
        }
        finally
        {
            if (step > 0)
            {
                var temp = await RedisDb.HashDecrementAsync(key2, value, 1);
                // 极小概率的并发风险
                if (temp == 0) await RedisDb.HashDeleteAsync(key2, value);
            }
        }

    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="success"></param>
    /// <param name="selector"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task DeleteAsync(Expression<Func<TEntity, string>> selector, params string[] values)
    {
        ArgumentNullException.ThrowIfNull(selector);
        ArgumentNullException.ThrowIfNull(values);

        var property = selector.Body;
        var member = (MemberExpression)property;

        var key = MyConst.Redis.GenerateKey<TEntity>("Unique", member.Member.Name, RedisSuffix ?? "") + (string.IsNullOrEmpty(AdditionalSuffix) ? "" : ":" + AdditionalSuffix);
        var fields = new RedisValue[values.Length];
        for (var i = 0; i < values.Length; i++) fields[i] = values[i];
        await RedisDb.HashDeleteAsync(key, fields);
    }

}


/// <summary>
/// TenantUniqueHelper
/// </summary>
/// <typeparam name="TEntity"></typeparam>
internal class TenantUniqueHelper<TEntity> : UniqueHelper<TEntity> where TEntity : class, IBaseTenantEntity
{
    protected long TenantId { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="tenantId"></param>
    public TenantUniqueHelper(Microsoft.EntityFrameworkCore.DbContext db) : this(db, 0)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="tenantId"></param>
    public TenantUniqueHelper(Microsoft.EntityFrameworkCore.DbContext db, long tenantId) : base(db)
    {
        ArgumentNullException.ThrowIfNull(db);
        ArgumentNullException.ThrowIfNull(tenantId);
        TenantId = tenantId;
        if (tenantId == 0)
        {
            TenantId = MyApp.GetLoginDataAsync().Result.TenantId;
        }
        RedisSuffix = TenantId.ToString();
        // 构建属性访问表达式
        //var parameter = Expression.Parameter(type, "x");
        //var property = Expression.Property(parameter, "TenantId");
        //var constant = Expression.Constant(TenantId);
        //var equal = Expression.Equal(property, constant);
        //var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
        AdditionalConstraint = AdditionalConstraint != null ? AdditionalConstraint.And(x => x.TenantId == TenantId) : x => x.TenantId == TenantId;

    }

}