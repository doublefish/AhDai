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
/// EmployeeServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class EmployeeServiceImpl(ILogger<EmployeeServiceImpl> logger
	, IOrganizationService orgService
	, IPostService postService
	, IUserService userService
	, IRoleService roleService)
	: BaseServiceImpl<Employee, EmployeeInput, EmployeeOutput, EmployeeQueryInput>(logger)
	, IEmployeeService
{
	readonly IOrganizationService _orgService = orgService;
	readonly IPostService _postService = postService;
	readonly IUserService _userService = userService;
	readonly IRoleService _roleService = roleService;

	protected override async Task BeforeSaveAsync(DefaultDbContext db, Employee entity, EmployeeInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			entity.Number = input.Number;
			entity.Name = input.Name;
			entity.Birthday = input.Birthday;
			entity.Gender = input.Gender;
			entity.Email = input.Email;
			//entity.MobilePhone = input.MobilePhone;
			entity.Telephone = input.Telephone;
            entity.IdNumber = input.IdNumber;
            entity.OrgId = input.OrgId;
			entity.PostIds = Utils.ModelUtils.ToString(input.PostIds);

			var user = await db.Users.Where(x => x.Id == entity.UserId).SingleOrDefaultAsync() ?? throw new Exception("无效的用户Id");
			user.Name = input.Name;
			user.Birthday = input.Birthday;
			user.Gender = input.Gender;
			user.Email = input.Email;
			user.Telephone = input.Telephone;
		}
		else
		{
			var role = await _roleService.GetByCodeAsync(Shared.Consts.RoleCode.Employee) ?? throw new Exception("没有找到角色：员工");
			var userId = await _userService.AddAsync(new UserInput()
			{
				Username = input.Username,
				Name = entity.Name,
				Birthday = input.Birthday,
				Gender = input.Gender,
				Email = input.Email,
				MobilePhone = input.MobilePhone,
				Telephone = input.Telephone,
				Orgs = [new UserOrgInput() { OrgId = input.OrgId, DataPermission = DataPermission.Self, IsDefault = true }],
				RoleIds = [role.Id]
			});
			entity.UserId = userId;
		}
	}

	protected override Task<IQueryable<Employee>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Employee> query, EmployeeQueryInput input)
	{
		if (!string.IsNullOrEmpty(input.Number))
		{
			query = query.Where(x => x.Number == input.Number);
		}
		if (input.Numbers != null && input.Numbers.Length > 0)
		{
			query = query.Where(x => input.Numbers.Contains(x.Number));
		}
		if (!string.IsNullOrEmpty(input.Name))
		{
			query = query.Where(x => x.Name.Contains(input.Name));
		}
		if (input.OrgId.HasValue)
		{
			query = query.Where(x => x.OrgId == input.OrgId.Value);
		}
		if (input.OrgIds != null && input.OrgIds.Length > 0)
		{
			query = query.Where(x => input.OrgIds.Contains(x.OrgId));
		}
		if (input.PostId.HasValue)
		{
			query = query.WhereInArrayString(x => x.PostIds, input.PostId.Value);
		}
		if (input.PostIds != null && input.PostIds.Length > 0)
		{
			query = query.WhereInArrayString(x => x.PostIds, input.PostIds);
		}
		if (input.UserId.HasValue)
		{
			query = query.Where(x => x.UserId == input.UserId.Value);
		}
		if (input.UserIds != null && input.UserIds.Length > 0)
		{
			query = query.Where(x => input.UserIds.Contains(x.UserId));
		}
		if (input.Status.HasValue)
		{
			query = query.Where(x => x.Status == input.Status.Value);
		}
		return base.GenerateQueryAsync(db, query, input);
	}


	protected override async Task GetAssociatedDataAsync(DefaultDbContext db, EmployeeOutput[] outputs, int dataDepth)
	{
		if (outputs == null || outputs.Length == 0 || dataDepth < 1) return;

		var ids = new HashSet<long>();
		var orgIds = new HashSet<long>();
		var postIds = new List<long>();
		var userIds = new HashSet<long>();
		foreach (var output in outputs)
		{
			ids.Add(output.Id);
			orgIds.Add(output.OrgId);
			postIds.AddRange(output.PostIds);
			userIds.Add(output.UserId);
			userIds.Add(output.CreatorId);
			if (output.ModifierId.HasValue) userIds.Add(output.ModifierId.Value);
		}

		var getOrgTask = _orgService.GetByIdsAsync([.. orgIds], dataDepth - 1, true);
		var getPostTask = _postService.GetByIdsAsync([.. postIds.Distinct()], dataDepth - 1, true);
		var getUserTask = _userService.GetByIdsAsync([.. userIds], dataDepth - 1, true);
		await Task.WhenAll(getOrgTask, getPostTask, getUserTask);

		var orgs = getOrgTask.Result;
		var posts = getPostTask.Result;
		var users = getUserTask.Result;

		foreach (var output in outputs)
		{
			output.Org = orgs.Where(x => x.Id == output.OrgId).FirstOrDefault();
			output.Posts = posts.Where(x => output.PostIds.Contains(x.Id)).ToArray();
			output.SetBaseInfo(users);
		}
		await base.GetAssociatedDataAsync(db, outputs, dataDepth);
	}
}
