using AhDai.Core.Services;
using AhDai.Core.Utils;
using AutoMapper;
using AhDai.Db;
using AhDai.Entity;
using AhDai.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// 构造函数
/// </summary>
internal abstract class BaseServiceImpl : IBaseService
{
    protected readonly ILogger Logger;
    protected readonly IMapper Mapper;
    protected readonly IBaseRedisService Redis;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger"></param>
    public BaseServiceImpl(ILogger? logger = null)
    {
        var type = GetType();
        Logger = logger ?? MyApp.GetLogger(type.FullName ?? type.Name);
        Mapper = MyApp.Services.GetRequiredService<IMapper>();
        Redis ??= MyApp.Services.GetRequiredService<IBaseRedisService>();
    }
}

/// <summary>
/// BaseServiceImpl
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
/// <param name="logger"></param>
/// <param name="enableDataPermission"></param>
/// <param name="enableCache"></param>
internal abstract class BaseServiceImpl<TEntity, TOutput, TQueryInput>(ILogger logger, bool enableDataPermission = false, bool enableCache = false)
    : BaseServiceImpl(logger)
    , IBaseService<TOutput, TQueryInput>
    where TEntity : class, IBaseEntity
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{
    protected readonly Type entityType = typeof(TEntity);
    protected readonly bool EnableDataPermission = enableDataPermission;
    protected readonly bool EnableCache = enableCache;
    protected readonly string RedisKey = MyConst.Redis.GenerateKey<TEntity>();

    /// <summary>
    /// 根据Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括关联数据</param>
    /// <returns></returns>
    public virtual async Task<TOutput> GetByIdAsync(long id, int dataDepth = 1, bool includeDeleted = false)
    {
        ArgumentNullException.ThrowIfNull(id);
        var outputs = await GetByIdsAsync([id], dataDepth, includeDeleted);
        var output = outputs.FirstOrDefault() ?? throw new ArgumentException("指定Id的记录不存在", nameof(id));
        return output;
    }

    /// <summary>
    /// 根据Id查询
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <returns></returns>
    public virtual async Task<TOutput[]> GetByIdsAsync(long[] ids, int dataDepth = 1, bool includeDeleted = false)
    {
        if (ids.Length == 0) return [];
        if (EnableCache)
        {
            var rdb = Redis.GetDatabase();
            var fields = Array.ConvertAll(ids, x => (RedisValue)x);
            var values = await rdb.HashGetAsync(RedisKey, fields);
            var dict = new Dictionary<long, TEntity>();
            var nullIds = new Dictionary<int, long>();
            for (var i = 0; i < ids.Length; i++)
            {
                var id = ids[i];
                var value = values[i];
                if (value.IsNullOrEmpty)
                {
                    nullIds.Add(i, id);
                }
                else
                {
                    var entity = JsonUtil.Deserialize<TEntity>(value);
                    dict.Add(id, entity);
                }
            }
            using var db = await MyApp.GetDefaultDbAsync();
            if (nullIds.Count > 0)
            {
                var temps = await SelectAsync(db, x => nullIds.Values.Contains(x.Id), true, false);
                var entries = new List<HashEntry>();
                foreach (var temp in temps)
                {
                    dict.Add(temp.Id, temp);
                    entries.Add(new HashEntry(temp.Id, JsonUtil.Serialize(temp)));
                }
                await rdb.HashSetAsync(RedisKey, [.. entries]);
            }
            var entities = includeDeleted ? dict.Values.ToArray() : dict.Values.Where(x => x.IsDeleted == false).ToArray();
            var outputs = Mapper.Map<TOutput[]>(entities);
            await GetAssociatedDataAsync(db, outputs, dataDepth);
            return outputs;
        }
        else
        {
            return await GetAsync(x => ids.Contains(x.Id), dataDepth, includeDeleted);
        }
    }


    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual async Task<TOutput[]> GetAsync(TQueryInput input)
    {
        ArgumentNullException.ThrowIfNull(input);
        using var db = await MyApp.GetDefaultDbAsync();
        var query = await GenerateQueryAsync(db, input.IncludeDeleted, input.WithDataPermission);
        query = await GenerateQueryAsync(db, query, input);
        query = GenerateQueryOrder(query);
        var entities = await query.Take(input.PageSize ?? 1000).ToArrayAsync();
        var outputs = Mapper.Map<TOutput[]>(entities);
        await GetAssociatedDataAsync(db, outputs, input.DataDepth ?? 1);
        return outputs;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual async Task<PageData<TOutput>> PageAsync(TQueryInput input)
    {
        ArgumentNullException.ThrowIfNull(input);
        using var db = await MyApp.GetDefaultDbAsync();
        var query = await GenerateQueryAsync(db, input.IncludeDeleted, input.WithDataPermission);
        query = await GenerateQueryAsync(db, query, input);
        query = GenerateQueryOrder(query);
        var count = await query.CountAsync();
        var data = new PageData<TOutput>(count);
        if (count > 0)
        {
            var pageNumber = input.PageNo ?? 1;
            var pageSize = input.PageSize ?? 10;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var entities = await query.ToArrayAsync();
            data.Rows = Mapper.Map<TOutput[]>(entities);
            await GetAssociatedDataAsync(db, data.Rows, input.DataDepth ?? 1);
        }
        else
        {
            data.Rows = [];
        }
        return data;
    }

    /// <summary>
    /// 统计数量
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual async Task<long> CountAsync(TQueryInput input)
    {
        ArgumentNullException.ThrowIfNull(input);
        using var db = await MyApp.GetDefaultDbAsync();
        var query = await GenerateQueryAsync(db, input.IncludeDeleted, EnableDataPermission);
        query = await GenerateQueryAsync(db, query, input);
        query = GenerateQueryOrder(query);
        var count = await query.CountAsync();
        return count;
    }

    /// <summary>
    /// 动态查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <param name="enableDataPermission">启用数据权限</param>
    /// <returns></returns>
    protected async Task<TOutput?> GetOneAsync(Expression<Func<TEntity, bool>> predicate, int dataDepth = 1, bool includeDeleted = false, bool enableDataPermission = false)
    {
        using var db = await MyApp.GetDefaultDbAsync();
        var entity = await SelectOneAsync(db, predicate, includeDeleted, enableDataPermission);
        var output = Mapper.Map<TOutput>(entity);
        await GetAssociatedDataAsync(db, [output], dataDepth);
        return output;
    }

    /// <summary>
    /// 动态查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <param name="enableDataPermission">启用数据权限</param>
    /// <returns></returns>
    protected async Task<TOutput[]> GetAsync(Expression<Func<TEntity, bool>> predicate, int dataDepth = 1, bool includeDeleted = false, bool enableDataPermission = false)
    {
        using var db = await MyApp.GetDefaultDbAsync();
        var entities = await SelectAsync(db, predicate, includeDeleted, enableDataPermission);
        var outputs = Mapper.Map<TOutput[]>(entities);
        await GetAssociatedDataAsync(db, outputs, dataDepth);
        return outputs;
    }

    /// <summary>
    /// 动态查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <param name="enableDataPermission">启用数据权限</param>
    /// <returns></returns>
    protected async Task<TEntity?> SelectOneAsync(DefaultDbContext db, Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, bool? enableDataPermission = null)
    {
        var query = await GenerateQueryAsync(db, includeDeleted, enableDataPermission);
        query = query.Where(predicate);
        query = GenerateQueryOrder(query);
        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// 动态查询
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <param name="enableDataPermission">启用数据权限</param>
    /// <returns></returns>
    protected async Task<TEntity[]> SelectAsync(DefaultDbContext db, Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, bool? enableDataPermission = null)
    {
        var query = await GenerateQueryAsync(db, includeDeleted, enableDataPermission);
        query = query.Where(predicate);
        query = GenerateQueryOrder(query);
        return await query.ToArrayAsync();
    }

    /// <summary>
    /// 生成查询语句
    /// </summary>
    /// <param name="db"></param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <param name="enableDataPermission">启用数据权限</param>
    /// <returns></returns>
    protected virtual async Task<IQueryable<TEntity>> GenerateQueryAsync(DefaultDbContext db, bool includeDeleted = false, bool? enableDataPermission = null)
    {
        var query = db.Set<TEntity>().Where(x => true);
        if (!includeDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        if (enableDataPermission ?? EnableDataPermission)
        {
            if ("AhDai.Entity.Oa".Equals(entityType.Namespace))
            {
                var loginData = await MyApp.GetLoginDataAsync();
                if (loginData.IsHr)
                {
                    enableDataPermission = false;
                }
            }
        }
        if (enableDataPermission ?? EnableDataPermission)
        {
            query = await query.QueryByDataPermissionAsync(db);
        }
        return query;
    }

    /// <summary>
    /// 生成查询排序规则
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    protected virtual IOrderedQueryable<TEntity> GenerateQueryOrder(IQueryable<TEntity> query)
    {
        return query.OrderByDescending(x => x.CreationTime);
    }

    /// <summary>
    /// 生成查询条件
    /// </summary>
    /// <param name="db"></param>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    protected virtual Task<IQueryable<TEntity>> GenerateQueryAsync(DefaultDbContext db, IQueryable<TEntity> query, TQueryInput input)
    {
        if (input.Ids != null && input.Ids.Length > 0)
        {
            query = query.Where(x => input.Ids.Contains(x.Id));
        }
        if (input.ExcludedIds != null && input.ExcludedIds.Length > 0)
        {
            query = query.Where(x => !input.ExcludedIds.Contains(x.Id));
        }
        if (input.CreatorId.HasValue)
        {
            query = query.Where(x => x.CreatorId == input.CreatorId.Value);
        }
        if (!string.IsNullOrEmpty(input.CreatorUsername))
        {
            query = query.Where(x => x.CreatorUsername.Contains(input.CreatorUsername));
        }
        if (!string.IsNullOrEmpty(input.CreatorName))
        {
            query = query.Where(x => db.Users.Any(u => u.Id == x.CreatorId && u.Name.Contains(input.CreatorName)));
        }
        return Task.FromResult(query);
    }

    /// <summary>
    /// 获取关联数据
    /// </summary>
    /// <param name="db"></param>
    /// <param name="outputs"></param>
    /// <param name="dataDepth">数据深度</param>
    /// <returns></returns>
    protected virtual Task GetAssociatedDataAsync(DefaultDbContext db, TOutput[] outputs, int dataDepth)
    {
        return Task.CompletedTask;
    }
}


/// <summary>
/// BaseServiceImpl
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
/// <param name="logger"></param>
/// <param name="enableDataPermission"></param>
/// <param name="enableCache"></param>
internal abstract class BaseServiceImpl<TEntity, TInput, TOutput, TQueryInput>(ILogger logger, bool enableDataPermission = false, bool enableCache = false)
    : BaseServiceImpl<TEntity, TOutput, TQueryInput>(logger, enableDataPermission, enableCache)
    , IBaseService<TInput, TOutput, TQueryInput>
    where TEntity : class, IBaseEntity
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual async Task<long> AddAsync(TInput input)
    {
        ArgumentNullException.ThrowIfNull(input);
        using var db = await MyApp.GetDefaultDbAsync();
        return await SaveAsync(db, 0, input, false);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public virtual async Task UpdateAsync(long id, TInput input)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(input);
        using var db = await MyApp.GetDefaultDbAsync();
        await SaveAsync(db, id, input, true);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="db"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    protected virtual async Task<long> SaveAsync(DefaultDbContext db, long id, TInput input, bool isUpdate)
    {
        TEntity entity;
        if (isUpdate)
        {
            entity = await SelectOneAsync(db, x => x.Id == id) ?? throw new ArgumentException("无效的Id", nameof(id));
        }
        else
        {
            entity = Mapper.Map<TEntity>(input);
            await db.Set<TEntity>().AddAsync(entity);
        }
        using var ts = await db.Database.BeginTransactionAsync();
        await BeforeSaveAsync(db, entity, input, isUpdate);
        await db.SaveChangesAsync();
        await AfterSaveAsync(db, entity, input, isUpdate);
        await DeleteCacheAsync(entity.Id);
        await ts.CommitAsync();
        await AfterSaveCommitAsync(db, entity, input, isUpdate);
        return entity.Id;
    }

    /// <summary>
    /// 保存变更之前
    /// </summary>
    /// <param name="db"></param>
    /// <param name="entity"></param>
    /// <param name="input"></param>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    protected virtual Task BeforeSaveAsync(DefaultDbContext db, TEntity entity, TInput input, bool isUpdate)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 保存变更之后
    /// </summary>
    /// <param name="db"></param>
    /// <param name="entity"></param>
    /// <param name="input"></param>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    protected virtual Task AfterSaveAsync(DefaultDbContext db, TEntity entity, TInput input, bool isUpdate)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 保存提交之后
    /// </summary>
    /// <param name="db"></param>
    /// <param name="entity"></param>
    /// <param name="input"></param>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    protected virtual Task AfterSaveCommitAsync(DefaultDbContext db, TEntity entity, TInput input, bool isUpdate)
    {
        //if (input is IAttachmentInput input2)
        //{
        //	await Helpers.AttachmentHelper.SaveAsync(input2.MenuId, id, input2.AttachmentIds);
        //}
        return Task.CompletedTask;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task DeleteByIdAsync(long id)
    {
        ArgumentNullException.ThrowIfNull(id);
        await DeleteByIdsAsync([id]);
    }

    /// <summary>
    /// 删除（批量）
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public virtual async Task DeleteByIdsAsync(long[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        using var db = await MyApp.GetDefaultDbAsync();
        await DeleteByIdsAsync(db, ids);
    }

    /// <summary>
    /// 删除（批量）
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    protected virtual async Task DeleteByIdsAsync(DefaultDbContext db, long[] ids)
    {
        using var ts = await db.Database.BeginTransactionAsync();
        var entities = await SelectAsync(db, x => ids.Contains(x.Id));
        db.Set<TEntity>().RemoveRange(entities);
        await BeforeDeleteAsync(db, entities, ids);
        await db.SaveChangesAsync();
        await AfterDeleteAsync(db, entities, ids);
        await DeleteCacheAsync(ids);
        await ts.CommitAsync();
    }

    /// <summary>
    /// 删除之前
    /// </summary>
    /// <param name="db"></param>
    /// <param name="entities"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    protected virtual Task BeforeDeleteAsync(DefaultDbContext db, TEntity[] entities, long[] ids)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 删除之后
    /// </summary>
    /// <param name="db"></param>
    /// <param name="entities"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    protected virtual Task AfterDeleteAsync(DefaultDbContext db, TEntity[] entities, long[] ids)
    {
        return Task.CompletedTask;
    }

    protected async Task<long> DeleteCacheAsync(params long[] ids)
    {
        if (!EnableCache) return 0;
        var fields = Array.ConvertAll(ids, x => (RedisValue)x);
        return await Redis.GetDatabase().HashDeleteAsync(RedisKey, fields);
    }
}


