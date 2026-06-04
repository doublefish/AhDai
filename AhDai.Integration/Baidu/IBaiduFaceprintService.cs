using AhDai.Integration.Baidu.Models;
using System.Threading.Tasks;

namespace AhDai.Integration.Baidu;

/// <summary>
/// IBaiduFaceprintService
/// </summary>
public interface IBaiduFaceprintService : IBaiduService
{
    /// <summary>
    /// 获取验证令牌
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<VerifyTokenOutput> GetVerifyTokenAsync(string accessToken);

    /// <summary>
    /// 上报用户信息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task SubmitIdCardAsync(string accessToken, IdCardSumbitInput input);

    /// <summary>
    /// 获取认证人脸
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SimpleResultOutput> GetSimpleResultAsync(string token);

    /// <summary>
    /// 获取实时方案视频
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<MediaResultOutput> GetMediaResultAsync(string token);

    /// <summary>
    /// 获取认证结果
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<DetailResultOutput> GetDetailResultAsync(string token);

    /// <summary>
    /// 获取核验及计费信息
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<AllResultOutput> GetAllResultAsync(string token);

    /// <summary>
    /// 生成实名认证Url
    /// </summary>
    /// <param name="token"></param>
    /// <param name="callbackUrl"></param>
    /// <param name="successUrl"></param>
    /// <param name="failedUrl"></param>
    /// <returns></returns>
    Task<string> GenerateUrlAsync(string token, string? callbackUrl = null, string? successUrl = null, string? failedUrl = null);
}
