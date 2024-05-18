using AhDai.Service.Sys.Models;
using System.Threading.Tasks;

namespace AhDai.Service.Sys;

/// <summary>
/// IUserService
/// </summary>
public interface IUserService
	: IBaseService<UserInput, UserOutput, UserQueryInput>
	, IEnableDisableService
	, IExistService<UserExistInput>
{
	/// <summary>
	/// 配置
	/// </summary>
	/// <param name="includeDeleted"></param>
	/// <returns></returns>
	Task<UserConfigOutput> GetConfigAsync(bool includeDeleted = false);

	/// <summary>
	/// 重置密码
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task ResetPasswordAsync(long id);

	/// <summary>
	/// 根据用户名和密码查询
	/// </summary>
	/// <param name="username"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	Task<UserOutput> GetByUsernameAsync(string username, string password);

	/// <summary>
	/// 查询数据权限
	/// </summary>
	/// <param name="id"></param>
	/// <param name="merge"></param>
	/// <returns></returns>
	Task<UserDataPermissionOutput[]> GetDataPermissionAsync(long id, bool merge = false);

}
