using System.Threading.Tasks;

namespace AhDai.Service;

/// <summary>
/// ITestService
/// </summary>
public interface ITestService
{
    /// <summary>
    /// 开始
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    Task StartAsync(string? action = null);
}
