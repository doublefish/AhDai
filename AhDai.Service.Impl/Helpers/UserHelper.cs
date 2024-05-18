using AhDai.Db;
using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Helpers;

internal class UserHelper
{
	/// <summary>
	/// 保存
	/// </summary>
	/// <param name="db"></param>
	/// <param name="entity"></param>
	/// <returns></returns>
	internal static async Task AfterSaveAsync(DefaultDbContext db, User entity)
	{
		var employee = await db.Employees.Where(x => x.UserId == entity.Id).FirstOrDefaultAsync();
		if (employee != null)
		{
			employee.Name = entity.Name;
			employee.Birthday = entity.Birthday;
			employee.Gender = entity.Gender;
			employee.Email = entity.Email;
			employee.MobilePhone = entity.MobilePhone;
			employee.Telephone = entity.Telephone;
		}
	}

	/// <summary>
	/// 删除
	/// </summary>
	/// <param name="db"></param>
	/// <param name="entities"></param>
	/// <returns></returns>
	internal static async Task AfterDeleteAsync(DefaultDbContext db, User[] entities)
	{
		var ids = entities.Select(x => x.Id);
		var hasEmployee = await db.Employees.Where(x => ids.Contains(x.UserId) && x.IsDeleted == false).AnyAsync();
		if (hasEmployee) throw new Exception("用户存在关联的员工，请先删除员工");
	}

}
