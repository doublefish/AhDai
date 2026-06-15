using AhDai.Integration.Abstractions;
using AhDai.Integration.Tencent.Configs;
using AhDai.Integration.Tencent.Models.Map;
using System.Threading.Tasks;

namespace AhDai.Integration.Tencent;

/// <summary>
/// ITencentMapService
/// </summary>
public interface ITencentMapService : IBaseService<TencentMapConfig>
{
    /// <summary>
    /// 逆地址解析
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <returns></returns>
    Task<GeocoderOutput> GetGeocoderAsync(double lat, double lng)
        => GetGeocoderAsync(new GeocoderInput() { Location = $"{lat},{lng}" });

    /// <summary>
    /// 逆地址解析
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<GeocoderOutput> GetGeocoderAsync(GeocoderInput input);

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
