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
	: BaseServiceImpl<Dict, DictInput, DictOutput, DictQueryInput>(logger)
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

	static async Task UpdateStatusAsync(long id, Shared.Enums.GenericStatus status)
	{
		ArgumentNullException.ThrowIfNull(id);
		ArgumentNullException.ThrowIfNull(status);
		using var db = await MyApp.GetDefaultDbAsync();
		await db.Dicts.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
	}

	public async Task<bool> ExistAsync(long id, CodeExistInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var uniqueHelper = new Helpers.TenantUniqueHelper<Dict>(db);
		return await uniqueHelper.ExistAsync(x => x.Code, input.Code, id, "编码已存在");
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
		if (isUpdate)
		{
			if (input.ParentId > 0)
			{
				var parent = await db.Dicts.Where(x => x.Id == input.ParentId).FirstOrDefaultAsync() ?? throw new ArgumentException("无效的父级Id");
			}
			else
			{
				if (string.IsNullOrEmpty(input.Code)) throw new ArgumentException("一级节点编码不能为空");
			}
			return await base.SaveAsync(db, id, input, isUpdate);
		}
		else
		{
			var uniqueHelper = new Helpers.TenantUniqueHelper<Dict>(db);
			return await uniqueHelper.ExistAsync(async () =>
			{
				if (input.ParentId != 0)
				{
					var parent = await db.Dicts.Where(x => x.Id == input.ParentId && x.IsDeleted == false).AnyAsync();
					if (!parent) throw new ArgumentException("无效的父级Id");
				}
				return await base.SaveAsync(db, id, input, isUpdate);
			}, x => x.Code, input.Code, id, "编码已存在");
		}
	}

	protected override async Task BeforeSaveAsync(DefaultDbContext db, Dict entity, DictInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			if (input.ParentId > 0) entity.Code = input.Code;
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

		var redis = Redis.GetDatabase();
		var key = MyConst.Redis.GenerateKey<Dict>();
		if (entity.ParentId > 0)
		{
			var id = entity.Id;
			var code = entity.Code;
			while (true)
			{
				var parent = await db.Dicts.Where(x => x.Id == id).FirstOrDefaultAsync();
				if (parent == null) break;
				id = parent.Id;
				code = parent.Code;
			}
			await redis.HashDeleteAsync(key, code);
		}
		else
		{
			await redis.HashDeleteAsync(key, entity.Code);
		}
	}

	protected override Task<IQueryable<Dict>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Dict> query, DictQueryInput input)
	{
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
		var redis = Redis.GetDatabase();
		var key = $"{MyConst.Redis.ROOT}:{typeof(Dict).Name}";

		var fields = new List<RedisValue>();
		foreach (var code in codes)
		{
			fields.Add(code);
		}
		var values = await redis.HashGetAsync(key, [.. fields]);
		var notExistCodes = new List<string>();
		for (var i = 0; i < codes.Length; i++)
		{
			var value = values[i];
			if (value.HasValue)
			{
				list.Add(JsonUtil.Deserialize<DictOutput>(value));
			}
			else
			{
				notExistCodes.Add(codes[i]);
			}
		}
		if (notExistCodes.Count > 0)
		{
			using var db = await MyApp.GetDefaultDbAsync();
			var entities = await db.Dicts.Where(x => notExistCodes.Contains(x.Code)).ToArrayAsync();
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
				await redis.HashSetAsync(key, [.. entries]);
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
