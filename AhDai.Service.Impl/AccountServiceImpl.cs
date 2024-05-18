using AhDai.Service.Models;
using AhDai.Service.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// UserServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class AccountServiceImpl(ILogger<AccountServiceImpl> logger
	, IUserService userService
	, IRoleService roleService)
	: BaseServiceImpl(logger)
	, IAccountService
{
	readonly IUserService _userService = userService;
	readonly IRoleService _roleService = roleService;

	public async Task SignupAsync(SignupInput input)
	{
		ArgumentNullException.ThrowIfNull(input);
		await Task.Run(() =>
		{

		});
	}

	public async Task<AccountOutput> GetAsync()
	{
		var loginData = await MyApp.GetLoginDataAsync();
		var user = await _userService.GetByIdAsync(loginData.Id);
		return new AccountOutput(user);
	}

	public async Task<AccountPermissionOutput> GetPermissionAsync()
	{
		var loginData = await MyApp.GetLoginDataAsync();
		var user = await _userService.GetByIdAsync(loginData.Id);
		var output = new AccountPermissionOutput();
		if (user.RoleIds != null)
		{
			var menus = await _roleService.GetMenuAsync(user.RoleIds);
			menus = [.. menus.OrderBy(x => x.ParentId).ThenBy(x => x.Sort).ThenBy(x => x.CreationTime)];
			output.Menus = menus.ToTreeArray();
			output.Permissions = menus.Where(x => !string.IsNullOrEmpty(x.Permission)).Select(x => x.Permission).ToArray();
		}
		return output;
	}

	public async Task ModifyAsync(AccountModifyInput input)
	{
		var loginData = await MyApp.GetLoginDataAsync();
		using var db = await MyApp.GetDefaultDbAsync();
		var entity = await db.Users.Where(x => x.Id == loginData.Id && x.IsDeleted == false).SingleOrDefaultAsync() ?? throw new Exception("未查询到登录用户信息");
		//entity.AvatarId = input.AvatarId;
		entity.Nickname = input.Nickname;
		entity.Name = input.Name;
		entity.Birthday = input.Birthday;
		entity.Gender = input.Gender;
		entity.Email = input.Email;
		entity.MobilePhone = input.MobilePhone;
		entity.Telephone = input.Telephone;
		await db.SaveChangesAsync();
	}

	public async Task ModifyAvatarAsync(AccountModifyAvatarInput input)
	{
		var loginData = await MyApp.GetLoginDataAsync();
		using var db = await MyApp.GetDefaultDbAsync();
		var entity = await db.Users.Where(x => x.Id == loginData.Id && x.IsDeleted == false).SingleOrDefaultAsync() ?? throw new Exception("未查询到登录用户信息");
		entity.AvatarId = input.AvatarId;
		await db.SaveChangesAsync();
	}

	public async Task ChangePasswordAsync(ChangePasswordInput input)
	{
		var loginData = await MyApp.GetLoginDataAsync();
		using var db = await MyApp.GetDefaultDbAsync();
		var user = await db.Users.Where(x => x.Id == loginData.Id && x.IsDeleted == false).SingleOrDefaultAsync() ?? throw new Exception("未查询到登录用户信息");
		await Helpers.PasswordHelper.ChangeAsync(db, user.Id, user.Username, input.Password, input.NewPassword);
		await db.SaveChangesAsync();
	}

}
