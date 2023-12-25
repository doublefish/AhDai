using AhDai.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IDictService
/// </summary>
public interface IDictService : IBaseService
{
	/// <summary>
	/// 新增
	/// </summary>
	/// <returns></returns>
	Task AddAsync(DictInput input);

	/// <summary>
	/// 修改
	/// </summary>
	/// <returns></returns>
	Task UpdateAsync(int id, DictInput input);

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	Task<DictOutput> GetByCodeAsync(string code);

	/// <summary>
	/// 获取
	/// </summary>
	/// <param name="codes"></param>
	/// <returns></returns>
	Task<ICollection<DictOutput>> GetByCodeAsync(string[] codes);

	/// <summary>
	/// 查询数据
	/// </summary>
	/// <param name="code"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	Task<DictDatumOutput> GetDatumByValueAsync(string code, string value);
}
