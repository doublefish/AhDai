using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// IEnableDisableService
/// </summary>
public interface IEnableDisableService
{
    /// <summary>
    /// 启用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task EnableAsync(long id);

    /// <summary>
    /// 禁用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DisableAsync(long id);
}
