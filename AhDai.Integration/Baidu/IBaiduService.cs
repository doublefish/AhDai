using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// IBaiduService
/// </summary>
public interface IBaiduService : IBaseService<BaiduConfig>
{
    /// <summary>
    /// 获取令牌
    /// </summary>
    /// <param name="enableCache"></param>
    /// <returns></returns>
    Task<AccessTokenOutput> GetAccessTokenAsync(bool enableCache = false);
}
