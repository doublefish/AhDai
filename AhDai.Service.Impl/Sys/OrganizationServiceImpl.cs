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
/// OrgServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class OrganizationServiceImpl(ILogger<OrganizationServiceImpl> logger
	, IUserService userService)
	: BaseServiceImpl<Organization, OrganizationInput, OrganizationOutput, OrganizationQueryInput>(logger)
	, IOrganizationService
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
		await db.Organizations.Where(x => x.Id == id && x.IsDeleted == false).ExecuteUpdateAsync(s => s.SetProperty(x => x.Status, status));
	}

	public async Task<bool> ExistAsync(long id, CodeExistInput input)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var uniqueHelper = new Helpers.TenantUniqueHelper<Organization>(db);
		return await uniqueHelper.ExistAsync(x => x.Code, input.Code, id, "编码已存在");
	}

	public async Task<OrganizationOutput?> GetByCodeAsync(string code, int dataDepth = 1, bool includeDeleted = false)
	{
		ArgumentNullException.ThrowIfNull(code);
		return await GetOneAsync(x => x.Code == code, dataDepth, includeDeleted);
	}

	public async Task<OrganizationOutput[]> GetByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false)
	{
		ArgumentNullException.ThrowIfNull(codes);
		return await GetAsync(x => codes.Contains(x.Code), dataDepth, includeDeleted);
	}


	protected override async Task<long> SaveAsync(DefaultDbContext db, long id, OrganizationInput input, bool isUpdate)
	{
		var uniqueHelper = new Helpers.TenantUniqueHelper<Organization>(db);
		return await uniqueHelper.ExistAsync(async () =>
		{
			if (isUpdate && input.ParentId == id) throw new ArgumentException("不能选择自己作为上级");
			if (input.ParentId != 0)
			{
				var superiors = await Utils.EntityUtil.GetSuperiorsAsync<Organization>(db, input.ParentId);
				if (superiors.Length == 0) throw new ArgumentException("无效的父级Id");
				if (superiors.Any(x => x.Id == id)) throw new ArgumentException("不能选择下级作为上级");
			}
			return await base.SaveAsync(db, id, input, isUpdate);
		}, x => x.Code, input.Code, id, "编码已存在");
	}

	protected override async Task BeforeSaveAsync(DefaultDbContext db, Organization entity, OrganizationInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			entity.Code = input.Code;
			entity.Name = input.Name;
			//entity.ParentId = input.ParentId;
			entity.LeaderId = input.LeaderId;
			entity.Remark = input.Remark;
			entity.Remark = input.Remark;
			entity.Sort = input.Sort;
			if (entity.ParentId != input.ParentId)
			{
				entity.ParentId = input.ParentId;
				var oldUniqueCode = entity.UniqueCode;
				var oldLevel = entity.Level;
				var uniqueCode = await Utils.EntityUtil.GenerateUniqueCodeAsync<Organization>(db, entity.ParentId);
				entity.Level = uniqueCode.Level;
				entity.Number = uniqueCode.Number;
				entity.UniqueCode = uniqueCode.UniqueCode;

				var res = await db.Organizations.Where(x => x.UniqueCode.StartsWith(oldUniqueCode) && x.Level > oldLevel).ExecuteUpdateAsync(s
					=> s.SetProperty(x => x.UniqueCode, x => x.UniqueCode.Replace(oldUniqueCode, entity.UniqueCode))
					.SetProperty(x => x.Level, x => x.Level - oldLevel + entity.Level));
				Logger.LogDebug("修改子级唯一标识：{res}", res);
			}
		}
		else
		{
			var uniqueCode = await Utils.EntityUtil.GenerateUniqueCodeAsync<Organization>(db, entity.ParentId);
			entity.Level = uniqueCode.Level;
			entity.Number = uniqueCode.Number;
			entity.UniqueCode = uniqueCode.UniqueCode;
			entity.Status = GenericStatus.Enabled;
		}
	}

	protected override async Task DeleteByIdsAsync(DefaultDbContext db, long[] ids)
	{
		var hasChild = await db.Organizations.Where(x => ids.Contains(x.ParentId) && x.IsDeleted == false).AnyAsync();
		if (hasChild) throw new Exception("不允许删除有子级的数据");
		await base.DeleteByIdsAsync(db, ids);
	}

	protected override Task<IQueryable<Organization>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Organization> query, OrganizationQueryInput input)
	{
		if (!string.IsNullOrEmpty(input.Keyword))
		{
			query = query.Where(x => x.Code.Contains(input.Keyword) || x.Name.Contains(input.Keyword));
		}
		if (!string.IsNullOrEmpty(input.Code))
		{
			query = query.Where(x => x.Code == input.Code);
		}
		if (input.Codes != null && input.Codes.Length > 0)
		{
			query = query.Where(x => input.Codes.Contains(x.Code));
		}
		if (!string.IsNullOrEmpty(input.Name))
		{
			query = query.Where(x => x.Name.Contains(input.Name));
		}
		if (input.ParentId.HasValue)
		{
			query = query.Where(x => x.ParentId == input.ParentId.Value);
		}
		if (input.Status.HasValue)
		{
			query = query.Where(x => x.Status == input.Status.Value);
		}
		return base.GenerateQueryAsync(db, query, input);
	}

	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, OrganizationOutput[] outputs, int dataDepth)
	{
		if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

		var ids = new HashSet<long>();
		var userIds = new HashSet<long>();
		foreach (var output in outputs)
		{
			ids.Add(output.Id);
			if (output.LeaderId.HasValue) userIds.Add(output.LeaderId.Value);
			userIds.Add(output.CreatorId);
			if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
		}

		var getUserTask = _userService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
		await getUserTask;

		var users = getUserTask.Result;

		foreach (var output in outputs)
		{
			if (output.LeaderId.HasValue) output.LeaderName = users.Where(x => x.Id == output.LeaderId.Value).FirstOrDefault()?.Name;
			output.SetBaseInfo(users);
		}
		await base.GetAssociatedDataAsync(db, outputs, dataDepth);
	}

}
