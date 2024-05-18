using AhDai.Core.Services;
using AhDai.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Db;

/// <summary>
/// MyIdGenerator
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class MyIdGenerator<TEntity>
	where TEntity : class, IKeyEntity
{
	readonly IBaseRedisService _redis;
	const string _key = "AhDai:IdGenerator";
	readonly string _field;
	readonly Stopwatch _stopwatch = new();
	readonly ILogger _logger;

	public MyIdGenerator()
	{
		_redis = AhDai.Core.Utils.ServiceUtil.Services.GetRequiredService<IBaseRedisService>();
		var type = typeof(TEntity);
		_field = type.FullName ?? type.Name;
		_logger = AhDai.Core.Utils.LoggerUtil.GetLogger(type.FullName ?? type.Name);
	}

	/// <summary>
	/// Next
	/// </summary>
	/// <param name="db"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public long Next(Microsoft.EntityFrameworkCore.DbContext db, long value = 1)
	{
		return NextAsync(db, value).Result;
	}

	/// <summary>
	/// NextAsync
	/// </summary>
	/// <param name="db"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public async Task<long> NextAsync(Microsoft.EntityFrameworkCore.DbContext db, long value = 1)
	{
		_stopwatch.Restart();
		var rdb = _redis.GetDatabase();
		var hasField = await rdb.HashExistsAsync(_key, _field);
		if (!hasField)
		{
			var last = await db.Set<TEntity>().OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
			await rdb.HashSetAsync(_key, _field, last > 9999 ? last : 9999);
		}
		var next = await rdb.HashIncrementAsync(_key, _field, value);
		_stopwatch.Stop();
		_logger.LogDebug("生成Id耗时=>{ms}ms,{next}", _stopwatch.ElapsedMilliseconds, next);
		return next;
	}

}
