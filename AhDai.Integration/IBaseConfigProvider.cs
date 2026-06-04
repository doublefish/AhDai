using System.Threading.Tasks;

namespace AhDai.Integration;

/// <summary>
/// IBaseConfigProvider
/// </summary>
public interface IBaseConfigProvider<TConfig>
    where TConfig : class
{
    /// <summary>
    /// 获取配置
    /// </summary>
    /// <returns></returns>
    Task<TConfig> GetAsync();
}
