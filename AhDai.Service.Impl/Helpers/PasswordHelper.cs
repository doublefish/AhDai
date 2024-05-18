using AhDai.Base.Security.Utils;
using AhDai.Db;
using AhDai.Entity.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Helpers;


internal class PasswordHelper
{
	/// <summary>
	/// 生成密码
	/// </summary>
	/// <param name="username"></param>
	/// <param name="password"></param>
	/// <param name="salt"></param>
	/// <returns></returns>
	static string Generate(string username, string password = "123456", string salt = "digital-chain")
	{
		return MD5Util.Encrypt(username + password + salt);
	}

	/// <summary>
	/// 生成对象
	/// </summary>
	/// <param name="userId"></param>
	/// <param name="username"></param>
	/// <returns></returns>
	public static UserPassword Generate(long userId, string username)
	{
		return new UserPassword()
		{
			UserId = userId,
			Password = Generate(username),
		};
	}

	/// <summary>
	/// 验证
	/// </summary>
	/// <param name="db"></param>
	/// <param name="userId"></param>
	/// <param name="username"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	public static async Task<bool> VerifyAsync(DefaultDbContext db, long userId, string username, string password)
	{
		var encrypt = Generate(username, password);
		return await db.UserPasswords.Where(x => x.UserId == userId && x.Password == encrypt && x.IsDeleted == false).AnyAsync();
	}

	/// <summary>
	/// 变更
	/// </summary>
	/// <param name="db"></param>
	/// <param name="userId"></param>
	/// <param name="username"></param>
	/// <param name="password"></param>
	/// <param name="newPassword"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public static async Task ChangeAsync(DefaultDbContext db, long userId, string username, string password, string newPassword)
	{
		var current = Generate(username, password);
		var entity = await db.UserPasswords.Where(x => x.UserId == userId && x.IsDeleted == false).FirstOrDefaultAsync();
		if (entity == null || entity.Password != current) throw new Exception("密码错误");
		entity.Password = Generate(username, newPassword);
	}

	/// <summary>
	/// 重置
	/// </summary>
	/// <param name="db"></param>
	/// <param name="userId"></param>
	/// <param name="username"></param>
	/// <returns></returns>
	public static async Task ResetAsync(DefaultDbContext db, long userId, string username)
	{
		var entity = await db.UserPasswords.Where(x => x.UserId == userId && x.IsDeleted == false).FirstOrDefaultAsync();
		if (entity == null)
		{
			entity = Generate(userId, username);
			await db.UserPasswords.AddAsync(entity);
		}
		else
		{
			entity.Password = Generate(username);
		}
	}

}
