using AhDai.Base.Extensions;
using AhDai.Core.Services;
using AhDai.Db;
using AhDai.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Utils;

internal class EntityUtil
{
    static readonly IDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();

    static object GetLocker<TEntity>(string key) where TEntity : class
    {
        var name = key + ":" + typeof(TEntity).Name;
        if (!_locks.TryGetValue(name, out var obj))
        {
            obj = new object();
            _locks.Add(name, obj);
        }
        return obj;
    }

    /// <summary>
    /// 生成流水号
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="lastOrDefault"></param>
    /// <param name="prefix"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    static async Task<string> GenerateNumberAsync<TEntity>(Func<string, Task<string?>> lastOrDefault, string prefix = "", int length = 4) where TEntity : class
    {
        //var user = await MyApp.GetOperatingUserAsync();
        var date = DateTime.Now.ToString("yyyyMMdd");
        var pref = prefix + date;

        var redis = MyApp.Services.GetRequiredService<IBaseRedisService>();
        var rdb = redis.GetDatabase();
        var key = MyConst.Redis.GenerateKey<TEntity>("Number", date);
        var field = "all";
        var keyExist = await rdb.KeyExistsAsync(key);
        if (!keyExist)
        {
            var _lock = GetLocker<TEntity>("Number");
            lock (_lock)
            {
                var last = lastOrDefault.Invoke(pref).Result;
                if (!string.IsNullOrEmpty(last))
                {
                    var temp = last[pref.Length..];
                    rdb.HashSet(key, field, temp.ToInt64());
                }
                else
                {
                    rdb.HashSet(key, field, 0);
                }
                rdb.KeyExpire(key, new TimeSpan(24, 0, 0));
            }
        }
        var number = await rdb.HashIncrementAsync(key, field, 1);
        return pref + number.ToString().PadLeft(length, '0');
    }

    /// <summary>
    /// 生成流水号
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="prefix">前缀</param>
    /// <param name="length">流水号长度</param>
    /// <returns></returns>
    public static async Task<string> GenerateCodeAsync<TEntity>(DefaultDbContext db, string prefix = "", int length = 4) where TEntity : class, ICodeEntity
    {
        return await GenerateNumberAsync<TEntity>(async (_prefix) =>
        {
            return await db.Set<TEntity>().Where(x => x.Code.StartsWith(_prefix)).OrderByDescending(x => x.Code).Select(x => x.Code).FirstOrDefaultAsync();
        }, prefix, length);
    }

    /// <summary>
    /// 生成流水号
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="prefix">前缀</param>
    /// <param name="length">流水号长度</param>
    /// <returns></returns>
    public static async Task<string> GenerateNumberAsync<TEntity>(DefaultDbContext db, string prefix = "", int length = 4) where TEntity : class, INumberEntity
    {
        return await GenerateNumberAsync<TEntity>(async (_prefix) =>
        {
            return await db.Set<TEntity>().Where(x => x.Number.StartsWith(_prefix)).OrderByDescending(x => x.Number).Select(x => x.Number).FirstOrDefaultAsync();
        }, prefix, length);
    }

    /// <summary>
    /// 查询所有上级
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<TEntity[]> GetSuperiorsAsync<TEntity>(DefaultDbContext db, long id) where TEntity : class, IParentIdEntity
    {
        var list = new List<TEntity>();
        var entity = await db.Set<TEntity>().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (entity != null)
        {
            list.Add(entity);
            var superiors = await GetSuperiorsAsync<TEntity>(db, entity.ParentId);
            list.AddRange(superiors);
        }
        return [.. list];
    }

    /// <summary>
    /// 生成唯一编码
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="db"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public static async Task<UniqueCodeEntity> GenerateUniqueCodeAsync<TEntity>(DefaultDbContext db, long parentId) where TEntity : class, IUniqueCodeEntity
    {
        var redis = MyApp.Services.GetRequiredService<IBaseRedisService>();
        var rdb = redis.GetDatabase();
        var key = MyConst.Redis.GenerateKey<TEntity>("UniqueCode");
        var field0 = parentId + "-Level";
        var field1 = parentId + "-Code";
        var field2 = parentId + "-Number";

        var parentLevel = 0;
        var parentCode = "";
        var lastNumber = 0;

        if (parentId > 0)
        {
            var value0 = await rdb.HashGetAsync(key, field0);
            var value1 = await rdb.HashGetAsync(key, field1);
            if (!value0.HasValue || !value1.HasValue)
            {
                var parent = db.Set<TEntity>().Where(x => x.Id == parentId).OrderByDescending(x => x.Number).FirstOrDefault() ?? throw new Exception("生成权限标识发生异常=>无效的父级Id");
                parentLevel = parent.Level;
                parentCode = parent.UniqueCode;
                rdb.HashSet(key, field0, parentLevel);
                rdb.HashSet(key, field1, parentCode);
            }
            else
            {
                parentLevel = value0.ToString().ToInt32();
                parentCode = value1.ToString();
            }
            parentCode += "-";
        }

        var exist2 = await rdb.HashExistsAsync(key, field2);
        if (!exist2)
        {
            var _lock = GetLocker<TEntity>("UniqueCode");
            lock (_lock)
            {
                var last = db.Set<TEntity>().Where(x => x.ParentId == parentId).OrderByDescending(x => x.Number).Select(x => x.Number).FirstOrDefault();
                lastNumber = last;
                rdb.HashSet(key, field2, lastNumber);
            }
        }

        var number = await rdb.HashIncrementAsync(key, field2, 1);
        var code = StringUtil.ConvertToBase36(number - 1);

        return new UniqueCodeEntity()
        {
            ParentId = parentId,
            Level = parentLevel + 1,
            Number = (int)number,
            UniqueCode = parentCode + code
        };
    }

}
