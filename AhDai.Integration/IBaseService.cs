using System.Threading.Tasks;

namespace AhDai.Integration;


/// <summary>
/// BaseService
/// </summary>
public interface IBaseService
{
}

/// <summary>
/// BaseService
/// </summary>
/// <typeparam name="TConfig"></typeparam>
public interface IBaseService<TConfig> : IBaseService where TConfig : class
{
    /// <summary>
    /// 获取配置
    /// </summary>
    /// <returns></returns>
    Task<TConfig> GetConfigAsync();
}
