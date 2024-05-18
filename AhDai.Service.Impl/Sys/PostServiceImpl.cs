using AhDai.Db;
using AhDai.Entity.Sys;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using AhDai.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// PostServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class PostServiceImpl(ILogger<PostServiceImpl> logger
	, IUserService userService)
	: BaseServiceImpl<Post, PostInput, PostOutput, PostQueryInput>(logger)
	, IPostService
{
	readonly IUserService _userService = userService;

	public async Task EnableAsync(long id)
	{
		await UpdateStatusAsync(id, GenericStatus.Enabled);
	}

	public async Task DisableAsync(long id)
	{
		await UpdateStatusAsync(id, GenericStatus.Disabled);
	}

	static async Task UpdateStatusAsync(long id, GenericStatus status)
	{
		ArgumentNullException.ThrowIfNull(id);
		ArgumentNullException.ThrowIfNull(status);
		using var db = await MyApp.GetDefaultDbAsync();
		await db.Posts.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
	}

	public async Task<bool> ExistAsync(long id, CodeExistInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var uniqueHelper = new Helpers.TenantUniqueHelper<Post>(db);
		return await uniqueHelper.ExistAsync(x => x.Code, input.Code, id, "编码已存在");
	}

	public async Task<PostOutput?> GetByCodeAsync(string code, int dataDepth = 1, bool includeDeleted = false)
	{
		ArgumentNullException.ThrowIfNull(code);
		return await GetOneAsync(x => x.Code == code, dataDepth, includeDeleted);
	}

	public async Task<PostOutput[]> GetByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false)
	{
		ArgumentNullException.ThrowIfNull(codes);
		return await GetAsync(x => codes.Contains(x.Code), dataDepth, includeDeleted);
	}

	protected override async Task<long> SaveAsync(DefaultDbContext db, long id, PostInput input, bool isUpdate)
	{
		var uniqueHelper = new Helpers.TenantUniqueHelper<Post>(db);
		return await uniqueHelper.ExistAsync(new Func<Task<long>>(() =>
		{
			return base.SaveAsync(db, id, input, isUpdate);
		}), x => x.Code, input.Code, id, "编码已存在");
	}

	protected override Task BeforeSaveAsync(DefaultDbContext db, Post entity, PostInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			entity.Code = input.Code;
			entity.Name = input.Name;
			entity.Remark = input.Remark;
			entity.Sort = input.Sort;
		}
		else
		{
			entity.Status = GenericStatus.Enabled;
		}
		return Task.CompletedTask;
	}

	protected override async Task DeleteByIdsAsync(DefaultDbContext db, long[] ids)
	{
		var hasEmployee = await db.Employees.Where(x => x.IsDeleted == false).WhereInArrayString(x => x.PostIds, ids).AnyAsync();
		if (hasEmployee) throw new Exception("该职位已关联员工，不可删除");
		await base.DeleteByIdsAsync(db, ids);
	}

	protected override Task<IQueryable<Post>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Post> query, PostQueryInput input)
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
		if (input.Status.HasValue)
		{
			query = query.Where(o => o.Status == input.Status.Value);
		}
		return base.GenerateQueryAsync(db, query, input);
	}

	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, PostOutput[] outputs, int dataDepth)
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

}
