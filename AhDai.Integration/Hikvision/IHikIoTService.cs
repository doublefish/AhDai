using AhDai.Integration.Abstractions;
using AhDai.Integration.Hikvision.Configs;
using AhDai.Integration.Hikvision.Models.IoT;
using System.Threading.Tasks;

namespace AhDai.Integration.Hikvision;

/// <summary>
/// IHikIoTService
/// </summary>
public interface IHikIoTService : IBaseService<HikIoTConfig>
{
    /// <summary>
    /// 获取应用访问凭证
    /// </summary>
    /// <param name="enableCache"></param>
    /// <returns></returns>
    Task<AppAccessTokenOutput> GetAppAccessTokenAsync(bool enableCache = false);

    /// <summary>
    /// 申请授权码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<AuthCodeOutput> GetAuthCodeAsync(AuthCodeInput input);

    /// <summary>
    /// 授权码获取用户访问凭证
    /// </summary>
    /// <param name="appAccessToken"></param>
    /// <param name="authCode"></param>
    /// <returns></returns>
    Task<UserAccessTokenOutput> GetUserAccessTokenAsync(string appAccessToken, string authCode);

    /// <summary>
    /// 刷新用户授权凭证
    /// </summary>
    /// <param name="appAccessToken"></param>
    /// <param name="userAccessToken"></param>
    /// <param name="refreshUserToken"></param>
    /// <returns></returns>
    Task<UserAccessTokenOutput> RefreshUserAccessTokenAsync(string appAccessToken, string userAccessToken, string refreshUserToken);

    /// <summary>
    /// 设备分页查询
    /// </summary>
    /// <param name="context"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    Task<DeviceOutput[]> PageDeviceAsync(AccessContext context, int page = 1, int size = 20);

    /// <summary>
    /// 通道分页查询
    /// </summary>
    /// <param name="context"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    Task<CameraOutput[]> PageCameraAsync(AccessContext context, int page = 1, int size = 20);
}
