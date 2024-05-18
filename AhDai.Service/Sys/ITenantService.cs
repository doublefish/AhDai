using AhDai.Service.Sys.Models;
using System.Threading.Tasks;

namespace AhDai.Service.Sys;

/// <summary>
/// ITenantService
/// </summary>
public interface ITenantService
	: IBaseService<TenantInput, TenantOutput, TenantQueryInput>
	, IEnableDisableService
{
	/// <summary>
	/// 配置
	/// </summary>
	/// <param name="includeDeleted"></param>
	/// <returns></returns>
	Task<TenantConfigOutput> GetConfigAsync(bool includeDeleted = false);

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
	Task SaveMenuAsync(long id, TenantMenuInput input);

}
