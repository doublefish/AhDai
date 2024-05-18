using AhDai.Service.Models;
using AhDai.Service.Sys.Models;
using System.Threading.Tasks;

namespace AhDai.Service.Sys;

/// <summary>
/// IRoleService
/// </summary>
public interface IRoleService
	: IBaseService<RoleInput, RoleOutput, RoleQueryInput>
	, IEnableDisableService
	, IGetByCodeService<RoleOutput>
	, ICodeExistService<CodeExistInput>
{
	/// <summary>
	/// 查询已有菜单
	/// </summary>
	/// <param name="ids"></param>
	/// <returns></returns>
	Task<MenuOutput[]> GetMenuAsync(params long[] ids);

	/// <summary>
	/// 查询已有菜单Id
	/// </summary>
	/// <param name="ids"></param>
	/// <returns></returns>
	Task<long[]> GetMenuIdAsync(params long[] ids);

	/// <summary>
	/// 分配菜单
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	Task SaveMenuAsync(long id, RoleMenuInput input);
}
