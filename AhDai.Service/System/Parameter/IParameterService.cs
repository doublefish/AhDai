using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.System.Parameter;

/// <summary>
/// IParameterService
/// </summary>
public interface IParameterService
{
    /// <summary>
    /// 根据类别查询
    /// </summary>
    /// <param name="category"></param>
    /// <param name="tenantId"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    Task<Dictionary<string, string>> GetValueByCategoryAsync(string category, long tenantId, bool includeDeleted = false);
}
