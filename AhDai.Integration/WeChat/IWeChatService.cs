using AhDai.Integration.WeChat.Configs;
using AhDai.Integration.WeChat.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AhDai.Integration.WeChat;

/// <summary>
/// IWeChatService
/// </summary>
public interface IWeChatService<TConfig> : IBaseService<TConfig> where TConfig : WeChatConfig
{
    /// <summary>
    /// 验证Token
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<string?> VerifyTokenAsync(HttpContext httpContext);

    /// <summary>
    /// 获取接口调用凭据
    /// </summary>
    /// <param name="enableCache"></param>
    /// <returns></returns>
    Task<AccessTokenOutput> GetAccessTokenAsync(bool enableCache = false);

    /// <summary>
    /// 获取稳定版接口调用凭据
    /// </summary>
    /// <param name="enableCache"></param>
    /// <returns></returns>
    Task<AccessTokenOutput> GetStableAccessTokenAsync(bool enableCache = false);

    /// <summary>
    /// 获取Ticket
    /// </summary>
    /// <returns></returns>
    Task<TicketOutput> GetTicketAsync(string? type = null, bool enableCache = false);

    /// <summary>
    /// 生成签名
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<SignOutput> SignAsync(SignInput input);

    /// <summary>
    /// 生成用户授权地址
    /// </summary>
    /// <param name="redirectUrl"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    Task<string> GenerateAuthUrlAsync(string redirectUrl, string? scope = null);

    /// <summary>
    /// 生成用户登录二维码地址
    /// </summary>
    /// <param name="redirectUrl"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    Task<string> GenerateLoginQrCodeUrlAsync(string redirectUrl, string? scope = null);

    /// <summary>
    /// 根据用户授权码获取用户授权凭据
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<AuthTokenOutput> GetAuthTokenByCodeAsync(string code);

    /// <summary>
    /// 根据授权凭据获取用户信息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="openId"></param>
    /// <returns></returns>
    Task<UserOutput> GetUserByAccessTokenAsync(string accessToken, string openId);
}
