using AhDai.Service.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Service
{
	/// <summary>
	/// 账号
	/// </summary>
	public interface IAccountService : IBaseService
	{
		/// <summary>
		/// 账号
		/// </summary>
		/// <returns></returns>
		Task<AccountOutput> GetAsync();

		/// <summary>
		/// 账号权限
		/// </summary>
		/// <returns></returns>
		Task<AccountPermissionOutput> GetPermissionAsync();

		/// <summary>
		/// 修改信息
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task ModifyAsync(AccountModifyInput input);

		/// <summary>
		/// 修改头像
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task ModifyAvatarAsync(AccountModifyAvatarInput input);

		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task ChangePasswordAsync(ChangePasswordInput input);
	}
}
