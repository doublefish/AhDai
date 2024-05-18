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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// RoleServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class RoleServiceImpl(ILogger<RoleServiceImpl> logger
	, IMenuService menuService
	, IUserService userService)
	: BaseServiceImpl<Role, RoleInput, RoleOutput, RoleQueryInput>(logger)
	, IRoleService
{
	readonly IMenuService _menuService = menuService;
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
		await db.Roles.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
	}

	public async Task<bool> ExistAsync(long id, CodeExistInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var uniqueHelper = new Helpers.TenantUniqueHelper<Role>(db);
		return await uniqueHelper.ExistAsync(x => x.Code, input.Code, id, "编码已存在");
	}

	public async Task<RoleOutput?> GetByCodeAsync(string code, int dataDepth = 1, bool includeDeleted = false)
	{
		ArgumentNullException.ThrowIfNull(code);
		return await GetOneAsync(x => x.Code == code, dataDepth, includeDeleted);
	}

	public async Task<RoleOutput[]> GetByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false)
	{
		ArgumentNullException.ThrowIfNull(codes);
		return await GetAsync(x => codes.Contains(x.Code), dataDepth, includeDeleted);
	}

	public async Task<MenuOutput[]> GetMenuAsync(params long[] ids)
	{
		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var menuIds = await GetMenuIdAsync(ids);
		stopwatch.Stop();
		Logger.LogDebug("查询菜单Id=>{ms}ms", stopwatch.ElapsedMilliseconds);
		return await _menuService.GetByIdsAsync(menuIds, 1);
	}

	public async Task<long[]> GetMenuIdAsync(params long[] ids)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		return await db.RoleMenus.Where(x => ids.Contains(x.RoleId)).Select(x => x.MenuId).Distinct().ToArrayAsync();
	}

	public async Task SaveMenuAsync(long id, RoleMenuInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		using var ts = await db.Database.BeginTransactionAsync();
		await db.RoleMenus.Where(x => x.RoleId == id).ExecuteDeleteAsync();
		foreach (var menuId in input.MenuIds)
		{
			await db.RoleMenus.AddAsync(new RoleMenu(id, menuId));
		}
		await db.SaveChangesAsync();
		await ts.CommitAsync();
	}

	protected override async Task<long> SaveAsync(DefaultDbContext db, long id, RoleInput input, bool isUpdate)
	{
		var uniqueHelper = new Helpers.TenantUniqueHelper<Role>(db);
		return await uniqueHelper.ExistAsync(new Func<Task<long>>(() =>
		{
			return base.SaveAsync(db, id, input, isUpdate);
		}), x => x.Code, input.Code, id, "编码已存在");
	}

	protected override Task BeforeSaveAsync(DefaultDbContext db, Role entity, RoleInput input, bool isUpdate)
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

	protected override Task<IQueryable<Role>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Role> query, RoleQueryInput input)
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

	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, RoleOutput[] outputs, int dataDepth)
	{
		if (outputs == null || outputs.Length == 0) return;

		var loginData = await MyApp.GetLoginDataAsync();
		foreach (var output in outputs)
		{
			output.CanModify = output.Type == 0 || loginData.TenantType == TenantType.System;
		}

		if (dataDepth < 1) return;

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
