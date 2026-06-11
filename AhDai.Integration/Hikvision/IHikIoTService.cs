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
    #region 身份及授权

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
    /// 获取用户访问凭证
    /// </summary>
    /// <param name="appAccessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<UserAccessTokenOutput> GetUserAccessTokenAsync(string appAccessToken, UserAccessTokenInput input);

    /// <summary>
    /// 刷新用户授权凭证
    /// </summary>
    /// <param name="appAccessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<UserAccessTokenOutput> RefreshUserAccessTokenAsync(string appAccessToken, RefreshUserAccessTokenInput input);

    #endregion

    #region 视频 - 取流/预览/对讲

    /// <summary>
    /// 获取设备token
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DeviceTokenOutput> GetDeviceTokenAsync(AccessContext context, DeviceTokenInput input);

    /// <summary>
    /// 批量获取设备token
    /// </summary>
    /// <param name="context"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>
    Task<DeviceTokenOutput[]> GetDeviceTokensAsync(AccessContext context, DeviceTokenInput[] inputs);

    /// <summary>
    /// 获取非设备操作token
    /// 非设备操作的token，互联SDK/取流插件等功能会用到，如：萤石token字段[ezAccessData]通过该接口获取：
    /// 注：无需字段解析，将整个json报文转成string透传
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<OpsTokenOutput> GetOpsTokenAsync(AccessContext context);

    /// <summary>
    /// 获取设备取流清晰度
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<StreamingMediaAttrsOutput> GetStreamingMediaAttrsAsync(AccessContext context, StreamingMediaAttrsInput input);

    #endregion

    #region 视频 - 通道

    /// <summary>
    /// 通道分页查询
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<CameraOutput[]> PageCameraAsync(AccessContext context, PageInput input);

    #endregion

    #region 硬件设备 - 设备/通道

    /// <summary>
    /// 设备分页查询
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DeviceOutput[]> PageDeviceAsync(AccessContext context, PageInput input);

    /// <summary>
    /// 设备/通道能力查询
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DeviceCapacitiesOutput> GetDeviceCapacitiesAsync(AccessContext context, DeviceCapacitiesQueryInput input);

    /// <summary>
    /// 设备获取资源详情
    /// </summary>
    /// <param name="context"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<DeviceResourceOutput> GetDeviceResourceAsync(AccessContext context, DeviceResourceQueryInput input);

    #endregion








}
