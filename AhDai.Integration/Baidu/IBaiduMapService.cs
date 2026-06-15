using AhDai.Integration.Abstractions;
using AhDai.Integration.Baidu.Configs;
using AhDai.Integration.Baidu.Models.Map;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// IBaiduMapService
/// </summary>
public interface IBaiduMapService : IBaseService<BaiduMapConfig>
{
    /// <summary>
    /// 逆地理编码
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <returns></returns>
    Task<ReverseGeocodingOutput> GetReverseGeocodingAsync(double lat, double lng)
        => GetReverseGeocodingAsync(new ReverseGeocodingInput() { Location = $"{lat},{lng}" });

    /// <summary>
    /// 逆地理编码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ReverseGeocodingOutput> GetReverseGeocodingAsync(ReverseGeocodingInput input);

    /// <summary>
    /// IP定位
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="enableCache"></param>
    /// <returns></returns>
    Task<IpLocationOutput> GetIpLocationAsync(string ip, bool enableCache = false);

    /// <summary>
    /// IP定位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IpLocationOutput> GetIpLocationAsync(IpLocationInput input);
}
