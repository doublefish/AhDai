using AhDai.Core.Utils;
using AhDai.Db;
using AhDai.Entity.Sys;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// DictServiceImpl
/// </summary>
/// <param name="redisService"></param>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class DictServiceImpl(ILogger<DictServiceImpl> logger
    , IUserService userService)
    : BaseTenantServiceImpl<Dict, DictInput, DictOutput, DictQueryInput>(logger)
    , IDictService
{
    readonly IUserService _userService = userService;

    public async Task EnableAsync(long id)
    {
        await UpdateStatusAsync(id, Shared.Enums.GenericStatus.Enabled);
    }

    public async Task DisableAsync(long id)
    {
        await UpdateStatusAsync(id, Shared.Enums.GenericStatus.Disabled);
    }

    async Task UpdateStatusAsync(long id, Shared.Enums.GenericStatus status)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(status);
        using var db = await MyApp.GetDefaultDbAsync();
        var query = await GenerateQueryAsync(db, false, false);
        await query.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
    }

    public async Task<bool> ExistAsync(long id, DicitCodeExistInput input)
    {
        using var db = await MyApp.GetDefaultDbAsync();
        var query = await GenerateQueryAsync(db, false, false);
        return await query.Where(x => x.Code == input.Code && x.ParentId == input.ParentId && x.Id != id).AnyAsync();
    }

    public async Task<DictOutput?> GetByCodeAsync(string code, int dataDepth = 1, bool includeDeleted = false)
    {
        var res = await SelectDictByCodesAsync([code], dataDepth, includeDeleted);
        return res.TryGetValue(code, out var data) ? data : null;
    }

    public async Task<DictOutput[]> GetByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false)
    {
        var res = await SelectDictByCodesAsync(codes, dataDepth, includeDeleted);
        return [.. res.Values];
    }

    public async Task<IDictionary<string, DictSimpleOutput[]>> GetSimpleDictByCodesAsync(string[] codes, bool includeDeleted = false)
    {
        var res = await SelectDictByCodesAsync(codes, 0, includeDeleted);
        var list = new Dictionary<string, DictSimpleOutput[]>();
        foreach (var kv in res)
        {
            var children = Mapper.Map<DictSimpleOutput[]>(kv.Value.Children);
            list.Add(kv.Key, children);
        }
        return list;
    }



    protected override async Task<long> SaveAsync(DefaultDbContext db, long id, DictInput input, bool isUpdate)
    {
        if (input.ParentId == 0)
        {
            if (string.IsNullOrEmpty(input.Code)) throw new ArgumentException("一级节点编码不能为空");
        }
        if (input.ParentId == 0 || !string.IsNullOrEmpty(input.Code))
        {
            var query = await GenerateQueryAsync(db, false, false);
            var any = await query.Where(x => x.Code == input.Code && x.ParentId == input.ParentId && x.Id != id).AnyAsync();
            if (any) throw new ArgumentException("编码已存在");
        }
        return await base.SaveAsync(db, id, input, isUpdate);
    }

    protected override async Task BeforeSaveAsync(DefaultDbContext db, Dict entity, DictInput input, bool isUpdate)
    {
        if (isUpdate)
        {
            entity.Code = input.Code;
            entity.Name = input.Name;
            //entity.ParentId = input.ParentId;
            entity.Value = input.Value;
            entity.Remark = input.Remark;
            entity.Sort = input.Sort;
        }
        else
        {
            entity.Status = Shared.Enums.GenericStatus.Enabled;
        }

        var rdb = Redis.GetDatabase();
        var key = MyConst.Redis.GenerateKey<Dict>();
        if (entity.ParentId > 0)
        {
            var top = await Utils.EntityUtil.GetTopSuperiorAsync<Dict>(db, entity.ParentId);
            if (top != null)
            {
                await rdb.HashDeleteAsync(key, top.Code);
            }
        }
        else
        {
            await rdb.HashDeleteAsync(key, entity.Code);
        }
    }

    protected override async Task AfterDeleteAsync(DefaultDbContext db, Dict[] entities, long[] ids)
    {
        var rdb = Redis.GetDatabase();
        var key = $"{MyConst.Redis.ROOT}:{EntityType.Name}";
        var fields = new List<RedisValue>();
        foreach (var entity in entities)
        {
            if (entity.ParentId > 0)
            {
                var top = await Utils.EntityUtil.GetTopSuperiorAsync<Dict>(db, entity.ParentId);
                if (top != null)
                {
                    fields.Add(top.Code);
                }
            }
            else
            {
                fields.Add(entity.Code);
            }
        }
        await rdb.HashDeleteAsync(key, [.. fields]);
        await base.AfterDeleteAsync(db, entities, ids);
    }

    protected override Task<IQueryable<Dict>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Dict> query, DictQueryInput input)
    {
        if (!string.IsNullOrEmpty(input.Keyword))
        {
            query = query.Where(o => o.Code.Contains(input.Keyword) || o.Name.Contains(input.Keyword));
        }
        if (!string.IsNullOrEmpty(input.Code))
        {
            query = query.Where(o => o.Code == input.Code);
        }
        if (input.Codes != null && input.Codes.Length > 0)
        {
            query = query.Where(o => input.Codes.Contains(o.Code));
        }
        if (!string.IsNullOrEmpty(input.Name))
        {
            query = query.Where(o => o.Name.Contains(input.Name));
        }
        if (input.ParentId.HasValue)
        {
            query = query.Where(o => o.ParentId == input.ParentId.Value);
        }
        if (input.Status.HasValue)
        {
            query = query.Where(o => o.Status == input.Status.Value);
        }
        return base.GenerateQueryAsync(db, query, input);
    }

    protected override async Task GetAssociatedDataAsync(DefaultDbContext db, DictOutput[] outputs, int dataDepth)
    {
        if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

        var ids = new HashSet<long>();
        var userIds = new HashSet<long>();
        foreach (var output in outputs)
        {
            ids.Add(output.Id);
            userIds.Add(output.CreatorId);
            if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
        }

        var getUserTask = _userService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
        await getUserTask;

        var users = getUserTask.Result;

        foreach (var output in outputs)
        {
            output.SetBaseInfo(users);
        }
        await base.GetAssociatedDataAsync(db, outputs, dataDepth);
    }

    private async Task<IDictionary<string, DictOutput>> SelectDictByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false)
    {
        ArgumentNullException.ThrowIfNull(codes);
        var list = new List<DictOutput>();
        var rdb = Redis.GetDatabase();
        var key = $"{MyConst.Redis.ROOT}:{EntityType.Name}";

        var fields = new List<RedisValue>();
        foreach (var code in codes)
        {
            fields.Add(code);
        }
        var values = await rdb.HashGetAsync(key, [.. fields]);
        var notExistCodes = new List<string>();
        for (var i = 0; i < codes.Length; i++)
        {
            var value = values[i];
            if (value.HasValue)
            {
                var temp = JsonUtil.Deserialize<DictOutput>(value.ToString());
                if (temp == null) notExistCodes.Add(codes[i]);
                else list.Add(temp);
            }
            else
            {
                notExistCodes.Add(codes[i]);
            }
        }
        if (notExistCodes.Count > 0)
        {
            using var db = await MyApp.GetDefaultDbAsync();
            var entities = await db.Dicts.Where(x => x.ParentId == 0 && notExistCodes.Contains(x.Code)).ToArrayAsync();
            if (entities.Length > 0)
            {
                var outputs = Mapper.Map<DictOutput[]>(entities);
                await SelectChildrenAsync(db, outputs);

                var entries = new List<HashEntry>();
                foreach (var output in outputs)
                {
                    entries.Add(new HashEntry(output.Code, JsonUtil.Serialize(output)));
                    list.Add(output);
                }
                await rdb.HashSetAsync(key, [.. entries]);
            }
        }

        var res = list.ToArray();
        if (!includeDeleted)
        {
            res = RemoveDeleted(res);
        }
        if (res.Length < codes.Length)
        {
            var temps = codes.Except(list.Select(x => x.Code));
            throw new Exception("不存在该编码的字典数据=>" + string.Join("/", temps));
        }
        return list.ToDictionary(k => k.Code, v => v);
    }

    private DictOutput[] RemoveDeleted(DictOutput[]? data)
    {
        var res = new List<DictOutput>();
        if (data != null)
        {
            foreach (var d in data)
            {
                if (d.IsDeleted) continue;
                d.Children = RemoveDeleted(d.Children);
                res.Add(d);
            }
        }
        return [.. res];
    }

    private async Task SelectChildrenAsync(DefaultDbContext db, params DictOutput[] data)
    {
        var ids = data.Select(x => x.Id).ToArray();
        var entities = await db.Dicts.Where(x => ids.Contains(x.ParentId)).ToArrayAsync();
        var children = Mapper.Map<DictOutput[]>(entities);
        foreach (var d in data)
        {
            d.Children = children.Where(x => x.ParentId == d.Id).ToArray();
            await SelectChildrenAsync(db, d.Children);
        }
    }

}
