using AhDai.Core;
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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// UserServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class UserServiceImpl(ILogger<UserServiceImpl> logger)
	: BaseServiceImpl<User, UserInput, UserOutput, UserQueryInput>(logger)
	, IUserService
{
	public Task<UserConfigOutput> GetConfigAsync(bool includeDeleted = false)
	{
		var config = new UserConfigOutput()
		{
			DataPermissions = ValueNameDataExtensions.FromEnum<DataPermission>(),
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
		await db.Users.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
	}

	public async Task ResetPasswordAsync(long id)
	{
		ArgumentNullException.ThrowIfNull(id);
		using var db = await MyApp.GetDefaultDbAsync();
		var user = await db.Users.Where(x => x.Id == id && x.IsDeleted == false).SingleOrDefaultAsync() ?? throw new Exception("无效的用户Id");
		await Helpers.PasswordHelper.ResetAsync(db, user.Id, user.Username);
		await db.SaveChangesAsync();
	}

	public async Task<bool> ExistAsync(long id, UserExistInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var uniqueHelper = new Helpers.UniqueHelper<User>(db);
		if (!string.IsNullOrEmpty(input.Username))
		{
			return await uniqueHelper.ExistAsync(x => x.Username, input.Username, id, "用户名已存在");
		}
		else if (!string.IsNullOrEmpty(input.MobilePhone))
		{
			return await uniqueHelper.ExistAsync(x => x.MobilePhone, input.MobilePhone, id, "手机号码已存在");
		}
		return false;
	}

	public async Task<UserOutput> GetByUsernameAsync(string username, string password)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var user = await db.Users.Where(x => x.Username == username && x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new ApiException(10000, "用户名或密码不正确");
		var verify = await Helpers.PasswordHelper.VerifyAsync(db, user.Id, user.Username, password);
		if (!verify) throw new ApiException(10000, "用户名或密码不正确");
		var output = Mapper.Map<UserOutput>(user);
		await GetAssociatedDataAsync(db, [output], 1);
		return output;
	}

	public async Task<UserDataPermissionOutput[]> GetDataPermissionAsync(long id, bool merge = false)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var stopwatch = new Stopwatch();
		stopwatch.Start();
		var userOrgs = await db.UserOrgs.Where(x => x.UserId == id && x.IsDeleted == false).ToArrayAsync();
		stopwatch.Stop();
		Logger.LogDebug("查询用户组织耗时=>{ms}ms", stopwatch.ElapsedMilliseconds);
		var orgIds = userOrgs.Select(x => x.OrgId).Distinct().ToArray();
		var orgService = MyApp.Services.GetRequiredService<IOrganizationService>();
		var orgs = await orgService.GetByIdsAsync(orgIds, 0);
		var list = new List<UserDataPermissionOutput>();
		foreach (var o in userOrgs)
		{
			var org = orgs.Where(x => x.Id == o.OrgId).FirstOrDefault();
			if (org == null) continue;
			var levelFrom = 0;
			var levelTo = 0;
			switch (o.DataPermission)
			{
				case DataPermission.Self:
					break;
				case DataPermission.Department:
					levelFrom = org.Level;
					levelTo = org.Level;
					break;
				case DataPermission.DepartmentAndSubordinates:
					levelFrom = org.Level;
					levelTo = 999;
					break;
				case DataPermission.All:
					levelFrom = 0;
					levelTo = 999;
					break;
			}
			list.Add(new UserDataPermissionOutput()
			{
				Id = o.Id,
				OrgId = o.OrgId,
				DataPermission = o.DataPermission,
				Level = org.Level,
				LevelFrom = levelFrom,
				LevelTo = levelTo,
				UniqueCode = org.UniqueCode,
			});
		}

		if (merge)
		{
			var list2 = new List<UserDataPermissionOutput>();
			foreach (var o in list)
			{
				var greater = list.Where(x => x.Id != o.Id && o.UniqueCode.StartsWith(x.UniqueCode) && o.LevelFrom >= x.LevelFrom && o.LevelTo <= x.LevelTo).ToArray();
				if (greater.Length > 0) continue;
				list2.Add(o);
			}
			return [.. list2];
		}

		return [.. list];
	}

	protected override async Task<long> SaveAsync(DefaultDbContext db, long id, UserInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			return await base.SaveAsync(db, id, input, isUpdate);
		}
		else
		{
			var uniqueHelper = new Helpers.UniqueHelper<User>(db);
			return await uniqueHelper.ExistAsync(new Func<Task<long>>(() =>
			{
				return uniqueHelper.ExistAsync(new Func<Task<long>>(() =>
				{
					return base.SaveAsync(db, id, input, isUpdate);
				}), x => x.MobilePhone, input.MobilePhone, id, "手机号码已存在");
			}), x => x.Username, input.Username, id, "用户名已存在");
		}
	}

	protected override async Task BeforeSaveAsync(DefaultDbContext db, User entity, UserInput input, bool isUpdate)
	{
		if (input.Orgs != null)
		{
			var defaultOrgs = input.Orgs.Where(x => x.IsDefault == true).Count();
			if (defaultOrgs > 1) throw new Exception("最多设置一个默认组织");
			else if (defaultOrgs < 1) throw new Exception("最少设置一个默认组织");
		}

		var deleteOrgs = new List<UserOrg>();
		var insertOrgs = new List<UserOrg>();
		var deleteRoles = new List<UserRole>();
		var insertRoles = new List<UserRole>();

		if (isUpdate)
		{
			//entity.Username = input.Username;
			//entity.AvatarId = input.AvatarId;
			//entity.Nickname = input.Nickname;
			entity.Name = input.Name;
			entity.Birthday = input.Birthday;
			entity.Gender = input.Gender;
			entity.Email = input.Email;
			//entity.MobilePhone = input.MobilePhone;
			entity.Telephone = input.Telephone;

			var extension = await db.UserExtensions.Where(x => x.UserId == entity.Id).FirstOrDefaultAsync();
			extension ??= new UserExtension()
			{
				UserId = entity.Id,
			};

			var orgs = await db.UserOrgs.Where(x => x.UserId == entity.Id).ToListAsync();
			deleteOrgs.AddRange(orgs);
			if (input.Orgs != null)
			{
				foreach (var org in input.Orgs)
				{
					var o = orgs.Where(x => x.OrgId == org.OrgId).FirstOrDefault();
					if (o != null)
					{
						o.DataPermission = org.DataPermission;
						o.IsDefault = org.IsDefault;
						deleteOrgs.Remove(o);
					}
					else
					{
						o = Mapper.Map<UserOrg>(org);
						o.UserId = entity.Id;
						insertOrgs.Add(o);
					}
				}
			}

			var roles = await db.UserRoles.Where(x => x.UserId == entity.Id).ToListAsync();
			deleteRoles.AddRange(roles);
			if (input.RoleIds != null)
			{
				foreach (var roleId in input.RoleIds)
				{
					var r = roles.Where(x => x.RoleId == roleId).FirstOrDefault();
					if (r != null)
					{
						deleteRoles.Remove(r);
					}
					else
					{
						insertRoles.Add(new UserRole(entity.Id, roleId));
					}
				}
			}
		}
		else
		{
			entity.Status = GenericStatus.Enabled;

			var password = Helpers.PasswordHelper.Generate(entity.Id, entity.Username);
			await db.UserPasswords.AddAsync(password);

			var extension = new UserExtension()
			{
				UserId = entity.Id,
			};
			await db.UserExtensions.AddAsync(extension);

			if (input.Orgs != null)
			{
				foreach (var org in input.Orgs)
				{
					var o = Mapper.Map<UserOrg>(org);
					o.UserId = entity.Id;
					insertOrgs.Add(o);
				}
			}
			if (input.RoleIds != null)
			{
				foreach (var roleId in input.RoleIds)
				{
					insertRoles.Add(new UserRole(entity.Id, roleId));
				}
			}
		}

		if (deleteOrgs.Count > 0) db.UserOrgs.RemoveRange(deleteOrgs);
		if (insertOrgs.Count > 0) await db.UserOrgs.AddRangeAsync(insertOrgs);
		if (deleteRoles.Count > 0) db.UserRoles.RemoveRange(deleteRoles);
		if (insertRoles.Count > 0) await db.UserRoles.AddRangeAsync(insertRoles);
	}

	protected override async Task AfterSaveAsync(DefaultDbContext db, User entity, UserInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			await Helpers.UserHelper.AfterSaveAsync(db, entity);
		}
		await base.AfterSaveAsync(db, entity, input, isUpdate);
	}

	protected override async Task AfterDeleteAsync(DefaultDbContext db, User[] entities, long[] ids)
	{
		await Helpers.UserHelper.AfterDeleteAsync(db, entities);
		await base.AfterDeleteAsync(db, entities, ids);
	}

	protected override Task<IQueryable<User>> GenerateQueryAsync(DefaultDbContext db, IQueryable<User> query, UserQueryInput input)
	{
		if (!string.IsNullOrEmpty(input.Username))
		{
			query = query.Where(x => x.Username.Contains(input.Username));
		}
		if (input.Usernames != null && input.Usernames.Length > 0)
		{
			query = query.Where(x => input.Usernames.Contains(x.Username));
		}
		if (!string.IsNullOrEmpty(input.Name))
		{
			query = query.Where(x => x.Name.Contains(input.Name));
		}
		if (input.OrgId.HasValue)
		{
			var q = db.UserOrgs.Where(x => x.OrgId == input.OrgId.Value && x.IsDeleted == false);
			query = query.Where(x => q.Any(y => y.UserId == x.Id));
		}
		if (input.OrgIds != null && input.OrgIds.Length > 0)
		{
			var q = db.UserOrgs.Where(x => input.OrgIds.Contains(x.OrgId) && x.IsDeleted == false);
			query = query.Where(x => q.Any(y => y.UserId == x.Id));
		}
		if (input.RoleId.HasValue)
		{
			query = query.Where(x => db.UserRoles.Any(y => y.UserId == x.Id && y.RoleId == input.RoleId.Value));
		}
		if (input.RoleIds != null && input.RoleIds.Length > 0)
		{
			query = query.Where(x => db.UserRoles.Any(y => y.UserId == x.Id && input.RoleIds.Contains(y.RoleId)));
		}
		if (!string.IsNullOrEmpty(input.RoleCode))
		{
			var q = from u in db.UserRoles
					join r in db.Roles on u.RoleId equals r.Id
					where r.Code == input.RoleCode
					select u;
			query = query.Where(x => q.Any(y => y.UserId == x.Id));
		}
		if (input.RoleCodes != null && input.RoleCodes.Length > 0)
		{
			var q = from u in db.UserRoles
					join r in db.Roles on u.RoleId equals r.Id
					where input.RoleCodes.Contains(r.Code)
					select u;
			query = query.Where(x => q.Any(y => y.UserId == x.Id));
		}
		if (input.Status.HasValue)
		{
			query = query.Where(x => x.Status == input.Status.Value);
		}
		return base.GenerateQueryAsync(db, query, input);
	}


	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, UserOutput[] outputs, int dataDepth)
	{
		if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

		var ids = new HashSet<long>();
		var userIds = new HashSet<long>();
		var fileIds = new HashSet<long>();
		var tenantIds = new HashSet<long>();
		foreach (var output in outputs)
		{
			ids.Add(output.Id);
			if (output.AvatarId.HasValue) fileIds.Add(output.AvatarId.Value);
			userIds.Add(output.CreatorId);
			if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
			tenantIds.Add(output.TenantId);
		}

		var userOrgs = await db.UserOrgs.Where(x => ids.Contains(x.UserId) && x.IsDeleted == false).ToArrayAsync();
		var userRoles = await db.UserRoles.Where(x => ids.Contains(x.UserId)).ToArrayAsync();
		var extensions = await db.UserExtensions.Where(x => ids.Contains(x.UserId) && x.IsDeleted == false).OrderBy(x => x.CreationTime).ToArrayAsync();
		var tenants = await db.Tenants.Where(x => tenantIds.Contains(x.Id)).ToArrayAsync();

		var orgIds = userOrgs.Select(x => x.OrgId).Distinct();
		var roleIds = userRoles.Select(x => x.RoleId).Distinct();

		var _orgService = MyApp.Services.GetRequiredService<IOrganizationService>();
		var _roleService = MyApp.Services.GetRequiredService<IRoleService>();
		var _fileService = MyApp.Services.GetRequiredService<IFileService>();

		var getOrgTask = _orgService.GetByIdsAsync([.. orgIds], dataDepth - 1, true);
		var getRoleTask = _roleService.GetByIdsAsync([.. roleIds], dataDepth - 1, true);
		var getUserTask = GetByIdsAsync([.. userIds], dataDepth - 1, true);
		var getFileTask = _fileService.GetByIdsAsync([.. fileIds], dataDepth - 1, true);
		await Task.WhenAll(getOrgTask, getRoleTask, getUserTask, getFileTask);

		var orgs = getOrgTask.Result;
		var roles = getRoleTask.Result;
		var users = getUserTask.Result;
		var files = getFileTask.Result;

		var userOrgOutputs = Mapper.Map<UserOrgOutput[]>(userOrgs);
		foreach (var o in userOrgOutputs)
		{
			o.OrgName = orgs.Where(x => x.Id == o.OrgId).FirstOrDefault()?.Name;
		}

		foreach (var output in outputs)
		{
			if (output.AvatarId.HasValue) output.AvatarUrl = files.Where(x => x.Id == output.AvatarId.Value).FirstOrDefault()?.FullPath;
			output.Orgs = userOrgOutputs.Where(x => x.UserId == output.Id).ToArray();
			output.RoleIds = userRoles.Where(x => x.UserId == output.Id).Select(x => x.RoleId).ToArray();
			output.Roles = roles.Where(x => output.RoleIds.Contains(x.Id)).ToArray();
			var thisExtension = extensions.Where(x => x.UserId == output.Id).FirstOrDefault();
			if (thisExtension != null)
			{
				output.WeChatOpenId = thisExtension.WeChatOpenId;
				output.ErpUserId = thisExtension.ErpUserId;
				output.LzezUserId = thisExtension.LzezUserId;
			}

			output.SetBaseInfo(users);
			var tenant = tenants.Where(x => x.Id == output.TenantId).FirstOrDefault();
			if (tenant != null)
			{
				output.TenantName = tenant.Name;
				output.TenantType = tenant.Type;
			}
		}
		await base.GetAssociatedDataAsync(db, outputs, dataDepth);
	}


}
