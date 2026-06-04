using AhDai.Integration.Amap.Configs;
using AhDai.Integration.Amap.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.Amap;

/// <summary>
/// IAmapService
/// </summary>
public interface IAmapService : IBaseService<AmapConfig>
{
    /// <summary>
    /// 逆地理编码
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lng"></param>
    /// <returns></returns>
    Task<ReverseGeocodeOutput> GetReverseGeocodeAsync(double lat, double lng)
        => GetReverseGeocodeAsync(new ReverseGeocodeInput() { Location = $"{lng},{lat}" });

    /// <summary>
    /// 逆地理编码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ReverseGeocodeOutput> GetReverseGeocodeAsync(ReverseGeocodeInput input);

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
