using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IDictTestService
/// </summary>
public interface IDictTestService : IBaseService
{

	/// <summary>
	/// 添加字典数据
	/// </summary>
	/// <returns></returns>
	Task<object> AddAsync();
	
}
