using AhDai.Core.Services;
using AhDai.Core.Utils;
using AhDai.Db;
using AhDai.Db.Models;
using AhDai.Service.Converters;
using AhDai.Service.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// DictServiceImpl
/// </summary>
internal class DictServiceImpl : BaseServiceImpl<Dict, DictInput, DictOutput, DictQueryInput>, IDictService
{
	readonly IRedisService _redisService;

	public DictServiceImpl(IRedisService redisService)
	{
		_redisService = redisService;
	}

	protected override async Task AddAsync(DefaultDbContext db, Dict model, DictInput input)
	{
		foreach (var d in model.Data)
		{
			d.DictId = model.Id;
		}
		await db.DictData.AddRangeAsync(model.Data);
		await db.SaveChangesAsync();
	}

	protected override async Task UpdateAsync(DefaultDbContext db, Dict model, DictInput input)
	{
		model.Data = await db.DictData.Where(o => o.DictId == model.Id && o.RowDeleted == false).ToArrayAsync();
		model.Code = input.Code;
		model.Name = input.Name;
		model.Remark = input.Remark;
		model.Status = input.Status;

		var ids = new List<long>();
		foreach (var item in input.Data)
		{
			if (item.Id == 0)
			{
				item.DictId = model.Id;
				await db.DictData.AddAsync(item.ToModel());
			}
			else
			{
				var datum = model.Data.Where(o => o.Id == item.Id).FirstOrDefault();
				datum.Code = item.Code;
				datum.Name = item.Name;
				datum.Value = item.Value;
				datum.Remark = item.Remark;
				ids.Add(datum.Id);
			}
		}
		foreach (var item in model.Data)
		{
			if (item.Id > 0 && !ids.Contains(item.Id))
			{
				db.DictData.Remove(item);
			}
		}
		await db.SaveChangesAsync();
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public async Task<DictOutput> GetByCodeAsync(string code)
	{
		var data = await SelectByCodeAsync(code);
		return data.ToOutput();
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="codes"></param>
	/// <returns></returns>
	public async Task<ICollection<DictOutput>> GetByCodeAsync(string[] codes)
	{
		var list = await SelectByCodeAsync(codes);
		return list.Values.ToOutputs();
	}

	/// <summary>
	/// 查询数据
	/// </summary>
	/// <param name="code"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public async Task<DictDatumOutput> GetDatumByValueAsync(string code, string value)
	{
		var dict = await SelectByCodeAsync(code);
		var datum = dict.Data.Where(o => o.Value == value).FirstOrDefault();
		return datum.ToOutput();
	}


	protected override IQueryable<Dict> GenerateQuery(DefaultDbContext db, IQueryable<Dict> query, DictQueryInput input)
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
		return query;
	}

	protected override Dict ToModel(DictInput input)
	{
		return input.ToModel();
	}

	protected override DictOutput ToOutput(Dict model)
	{
		return model.ToOutput();
	}


	private async Task<Dict> SelectByCodeAsync(string code)
	{
		var res = await SelectByCodeAsync(new string[] { code });
		if (!res.TryGetValue(code, out var dict))
		{
			throw new Exception("字典中不存在该编码的数据=>" + code);
		}
		return dict;
	}

	private async Task<IDictionary<string, Dict>> SelectByCodeAsync(string[] codes)
	{
		var redis = _redisService.GetDatabase();
		var key = $"{MyConst.Redis.ROOT}:{typeof(Dict).Name}";
		var res = new Dictionary<string, Dict>();
		var fields = new List<RedisValue>();
		foreach (var code in codes)
		{
			fields.Add(code);
		}
		var values = await redis.HashGetAsync(key, fields.ToArray());
		var notExistCodes = new List<string>();
		for (var i = 0; i < codes.Length; i++)
		{
			var value = values[i];
			if (value.HasValue)
			{
				var dict = JsonUtil.Deserialize<Dict>(value);
				res.Add(dict.Code, dict);
			}
			else
			{
				notExistCodes.Add(codes[i]);
			}
		}
		if (notExistCodes.Count > 0)
		{
			using var db = new DefaultDbContext();
			var dicts = await db.Dicts.Where(o => notExistCodes.Contains(o.Code) && o.RowDeleted == false).ToArrayAsync();
			if (dicts.Length < notExistCodes.Count)
			{
				var notExistCodes1 = notExistCodes.Except(dicts.Select(o => o.Code));
				throw new Exception("字典中不存在该编码的数据=>" + string.Join("/", notExistCodes1));
			}
			var dictIds = dicts.Select(o => o.Id).ToArray();
			var data = await db.DictData.Where(o => dictIds.Contains(o.DictId) && o.RowDeleted == false).OrderBy(o => o.Value).ToArrayAsync();
			var entries = new List<HashEntry>();
			foreach (var dict in dicts)
			{
				dict.Data = data.Where(o => o.DictId == dict.Id && o.RowDeleted == false).ToArray();
				entries.Add(new HashEntry(dict.Code, JsonUtil.Serialize(dict)));
				res.Add(dict.Code, dict);
			}
			await redis.HashSetAsync(key, entries.ToArray());
		}

		return res;
	}
}
