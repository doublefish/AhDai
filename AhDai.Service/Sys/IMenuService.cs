using AhDai.Service.Models;
using AhDai.Service.Sys.Models;
using System.Threading.Tasks;

namespace AhDai.Service.Sys;

/// <summary>
/// IMenuService
/// </summary>
public interface IMenuService
	: IBaseService<MenuInput, MenuOutput, MenuQueryInput>
	, IEnableDisableService
	, IGetByCodeService<MenuOutput>
	, ICodeExistService<CodeExistInput>
{
	/// <summary>
	/// 配置
	/// </summary>
	/// <param name="includeDeleted"></param>
	/// <returns></returns>
	Task<MenuConfigOutput> GetConfigAsync(bool includeDeleted = false);

	/// <summary>
	/// 查询所有：树形结构
	/// </summary>
	/// <param name="dataDepth"></param>
	/// <param name="includeDeleted"></param>
	/// <returns></returns>
	Task<MenuOutput[]> GetAllAsync(int dataDepth = 0, bool includeDeleted = false);
}
