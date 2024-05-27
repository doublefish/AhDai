using AhDai.Service.Sys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.Sys;

/// <summary>
/// IDictService
/// </summary>
public interface IDictService
	: IBaseService<DictInput, DictOutput, DictQueryInput>
	, IEnableDisableService
	, IGetByCodeService<DictOutput>
	, ICodeExistService<DicitCodeExistInput>

{
	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="codes"></param>
	/// <param name="includeDeleted"></param>
	/// <returns></returns>
	Task<IDictionary<string, DictSimpleOutput[]>> GetSimpleDictByCodesAsync(string[] codes, bool includeDeleted = false);

}
