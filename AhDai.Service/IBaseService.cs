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
/// <typeparam name="PK"></typeparam>
/// <typeparam name="Input"></typeparam>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
public interface IBaseService<PK, Input, Output, QueryInput> : IBaseService
{
	/// <summary>
	/// 新增
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	Task AddAsync(Input input);

	/// <summary>
	/// 修改
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	Task UpdateAsync(PK id, Input input);

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
/// <typeparam name="Input"></typeparam>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
public interface IBaseService<Input, Output, QueryInput> : IBaseService<long, Input, Output, QueryInput>
{
	
}
