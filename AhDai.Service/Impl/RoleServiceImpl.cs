using AhDai.Db;
using AhDai.Db.Models;
using AhDai.Service.Converters;
using AhDai.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// RoleServiceImpl
/// </summary>
internal class RoleServiceImpl : BaseServiceImpl<Role, RoleInput, RoleOutput, RoleQueryInput>, IRoleService
{

	protected override async Task UpdateAsync(DefaultDbContext db, Role model, RoleInput input)
	{
		model.Code = input.Code;
		model.Name = input.Name;
		model.Remark = input.Remark;
		model.Status = input.Status;
		await db.SaveChangesAsync();
	}

	protected override IQueryable<Role> GenerateQuery(DefaultDbContext db, IQueryable<Role> query, RoleQueryInput input)
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

	protected override Role ToModel(RoleInput input)
	{
		return input.ToModel();
	}

	protected override RoleOutput ToOutput(Role model)
	{
		return model.ToOutput();
	}
}
