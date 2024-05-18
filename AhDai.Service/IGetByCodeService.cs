using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IGetByCodeService
/// </summary>
public interface IGetByCodeService<TOutput>
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="code"></param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <returns></returns>
    Task<TOutput?> GetByCodeAsync(string code, int dataDepth = 1, bool includeDeleted = false);

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="codes"></param>
    /// <param name="dataDepth">数据深度</param>
    /// <param name="includeDeleted">包括已删除的</param>
    /// <returns></returns>
    Task<TOutput[]> GetByCodesAsync(string[] codes, int dataDepth = 1, bool includeDeleted = false);
}
