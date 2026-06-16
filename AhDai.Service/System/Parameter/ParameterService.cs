using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.System.Parameter;

/// <summary>
/// ParameterService
/// </summary>
[Attributes.Service]
internal class ParameterService : IParameterService
{
    public Task<Dictionary<string, string>> GetValueByCategoryAsync(string category, long tenantId, bool includeDeleted = false)
    {
        var result = new Dictionary<string, string>();
        return Task.FromResult(result);
    }
}
