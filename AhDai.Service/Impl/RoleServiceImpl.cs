using AhDai.Db;
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
internal class RoleServiceImpl : BaseServiceImpl, IRoleService
{
	/// <summary>
	/// 获取
	/// </summary>
	/// <returns></returns>
	public async Task<ICollection<RoleOutput>> GetAsync()
	{
		using var db = new DefaultDbContext();
		var list = await db.Roles.Where(o => o.RowDeleted == false).ToListAsync();
		return list.ToOutputs();
	}

	public async Task AddAsync(RoleInput input)
	{
		var model = input.ToModel();
		using var db = new DefaultDbContext();
		db.Roles.Add(model);
		await db.SaveChangesAsync();
	}

	public async Task UpdateAsync(int id, RoleInput input)
	{
		await Task.Run(() =>
		{
			throw new NotImplementedException();
		});
	}
}
