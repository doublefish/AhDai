using AhDai.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IDictService
/// </summary>
public interface IDictService : IBaseService<DictInput, DictOutput, DictQueryInput>
{
	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
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
