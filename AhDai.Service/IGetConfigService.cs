using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IGetConfigService
/// </summary>
public interface IGetConfigService<TOutput>
{
	/// <summary>
	/// 查询配置
	/// </summary>
	/// <param name="includeDeleted">包括已删除的</param>
	/// <returns></returns>
	Task<TOutput> GetConfigAsync(bool includeDeleted = false);
}
