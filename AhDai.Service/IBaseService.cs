using AhDai.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IBaseService
/// </summary>
public interface IBaseService
{
}

/// <summary>
/// IBaseService
/// </summary>
public interface IBaseService<Output, QueryInput> : IBaseService
{
	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	Task<ICollection<Output>> GetAsync(QueryInput input);

	/// <summary>
	/// 分页查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	Task<PageData<Output>> PageAsync(QueryInput input);
}

/// <summary>
/// IBaseService
/// </summary>
public interface IBaseService<PK, Output, QueryInput> : IBaseService<Output, QueryInput>
{
	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<Output> GetByIdAsync(PK id);

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="ids"></param>
	/// <returns></returns>
	Task<ICollection<Output>> GetByIdsAsync(PK[] ids);
}
