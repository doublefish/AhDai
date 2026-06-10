using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// IBaseBaiduService
/// </summary>
public interface IBaseBaiduService<TConfig> : IBaseService<TConfig> where TConfig : BaseBaiduConfig
{
    /// <summary>
    /// 获取令牌
    /// </summary>
    /// <param name="enableCache"></param>
    /// <returns></returns>
    Task<AccessTokenOutput> GetAccessTokenAsync(bool enableCache = false);
}
