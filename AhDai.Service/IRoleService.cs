using AhDai.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IRoleService
/// </summary>
public interface IRoleService : IBaseService
{
	/// <summary>
	/// 获取
	/// </summary>
	/// <returns></returns>
	Task<ICollection<RoleOutput>> GetAsync();

	/// <summary>
	/// 新增
	/// </summary>
	/// <returns></returns>
	Task AddAsync(RoleInput input);

	/// <summary>
	/// 修改
	/// </summary>
	/// <returns></returns>
	Task UpdateAsync(int id, RoleInput input);
}
