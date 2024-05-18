using AhDai.Db;
using AhDai.Entity.Sys;
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
/// TenantServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class TenantServiceImpl(ILogger<TenantServiceImpl> logger
	, IUserService userService)
	: BaseServiceImpl<Tenant, TenantInput, TenantOutput, TenantQueryInput>(logger)
	, ITenantService
{
	readonly IUserService _userService = userService;

	public Task<TenantConfigOutput> GetConfigAsync(bool includeDeleted = false)
	{
		var config = new TenantConfigOutput()
		{
			Types = ValueNameDataExtensions.FromEnum<TenantType>(),
		};
		return Task.FromResult(config);
	}

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
		await db.Tenants.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
	}

	public async Task<long[]> GetMenuIdAsync(params long[] ids)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		return await db.TenantMenus.Where(x => ids.Contains(x.TenantId)).Select(x => x.MenuId).Distinct().ToArrayAsync();
	}

	public async Task SaveMenuAsync(long id, TenantMenuInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		using var ts = await db.Database.BeginTransactionAsync();
		await db.TenantMenus.Where(x => x.TenantId == id).ExecuteDeleteAsync();
		foreach (var menuId in input.MenuIds)
		{
			await db.TenantMenus.AddAsync(new TenantMenu(id, menuId));
		}
		await db.SaveChangesAsync();
		await ts.CommitAsync();
	}

	protected override async Task<long> SaveAsync(DefaultDbContext db, long id, TenantInput input, bool isUpdate)
	{
		var uniqueHelper = new Helpers.UniqueHelper<Tenant>(db);
		if (isUpdate)
		{
			return await uniqueHelper.ExistAsync(new Func<Task<long>>(() =>
			{
				return base.SaveAsync(db, id, input, isUpdate);
			}), x => x.Name, input.Name, id, "名称已存在");
		}
		else
		{
			ArgumentNullException.ThrowIfNull(input.Admin);
			ArgumentException.ThrowIfNullOrEmpty(input.Admin.Username, "Admin.Username");
			ArgumentException.ThrowIfNullOrEmpty(input.Admin.Name, "Admin.Name");
			ArgumentException.ThrowIfNullOrEmpty(input.Admin.MobilePhone, "Admin.MobilePhone");

			var userUniqueHelper = new Helpers.UniqueHelper<User>(db);
			return await uniqueHelper.ExistAsync(new Func<Task<long>>(() =>
			{
				return userUniqueHelper.ExistAsync(new Func<Task<long>>(() =>
				{
					return userUniqueHelper.ExistAsync(new Func<Task<long>>(() =>
					{
						return base.SaveAsync(db, id, input, isUpdate);
					}), x => x.MobilePhone, input.Admin.MobilePhone, id, "手机号码已存在");
				}), x => x.Username, input.Admin.Username, id, "用户名已存在");

			}), x => x.Name, input.Name, id, "名称已存在");
		}
	}

	protected override async Task BeforeSaveAsync(DefaultDbContext db, Tenant entity, TenantInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			entity.Name = input.Name;
			entity.Remark = input.Remark;
		}
		else
		{
			ArgumentNullException.ThrowIfNull(input.Admin);

			entity.Type = TenantType.Normal;
			entity.Status = GenericStatus.Enabled;

			var roles = await db.Roles.Where(x => x.TenantId == 0 && x.IsDeleted == false).ToArrayAsync();
			foreach (var defaultRole in roles)
			{
				defaultRole.Id = 0;
				defaultRole.TenantId = entity.Id;
			}
			await db.Roles.AddRangeAsync(roles);

			var uniqueCode = await Utils.EntityUtil.GenerateUniqueCodeAsync<Organization>(db, 0);
			var org = new Organization()
			{
				Code = "",
				Name = input.Name,
				Level = uniqueCode.Level,
				Number = uniqueCode.Number,
				UniqueCode = uniqueCode.UniqueCode,
				TenantId = entity.Id,
			};
			await db.Organizations.AddAsync(org);

			var user = new User()
			{
				Username = input.Admin.Username,
				Name = input.Admin.Name,
				Email = input.Admin.Email,
				MobilePhone = input.Admin.MobilePhone,
			};
			await db.Users.AddAsync(user);
			var password = Helpers.PasswordHelper.Generate(user.Id, user.Username);
			await db.UserPasswords.AddAsync(password);
			await db.UserOrgs.AddAsync(new UserOrg()
			{
				UserId = user.Id,
				OrgId = org.Id,
				DataPermission = DataPermission.All,
				IsDefault = true,
			});
			await db.UserRoles.AddAsync(new UserRole()
			{
				UserId = user.Id,
				RoleId = roles.Where(x => x.Code == MyConst.Role.SYSTEM_ADMIN).First().Id
			});
			entity.AdminId = user.Id;
		}
	}

	protected override Task<IQueryable<Tenant>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Tenant> query, TenantQueryInput input)
	{
		if (!string.IsNullOrEmpty(input.Name))
		{
			query = query.Where(o => o.Name.Contains(input.Name));
		}
		if (input.Type.HasValue)
		{
			query = query.Where(o => o.Type == input.Type.Value);
		}
		if (input.Status.HasValue)
		{
			query = query.Where(o => o.Status == input.Status.Value);
		}
		return base.GenerateQueryAsync(db, query, input);
	}

	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, TenantOutput[] outputs, int dataDepth)
	{
		if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

		var ids = new HashSet<long>();
		var userIds = new HashSet<long>();
		foreach (var output in outputs)
		{
			ids.Add(output.Id);
			userIds.Add(output.AdminId);
			userIds.Add(output.CreatorId);
			if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
		}

		var getUserTask = _userService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
		var getConfigTask = GetConfigAsync(true);

		await Task.WhenAll(getUserTask, getConfigTask);

		var users = getUserTask.Result;

		foreach (var output in outputs)
		{
			output.Admin = users.Where(x => x.Id == output.AdminId).FirstOrDefault();
			output.SetBaseInfo(users);
		}
		await base.GetAssociatedDataAsync(db, outputs, dataDepth);
	}

}
