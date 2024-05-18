using System.Threading.Tasks;

namespace AhDai.Service.Test;

/// <summary>
/// ITestService
/// </summary>
public interface ITestService : IBaseService
{
    /// <summary>
    /// 测试
    /// </summary>
    /// <returns></returns>
    Task<object?> TestAsync();

}
