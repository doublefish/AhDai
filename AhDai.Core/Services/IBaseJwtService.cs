using AhDai.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// Jwt服务
/// </summary>
public interface IBaseJwtService
{
    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<TokenResult> GenerateTokenAsync(TokenData data);

    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    Task<TokenResult> GenerateTokenAsync(params Claim[] claims);

    /// <summary>
    /// 获取声明的数据
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    IEnumerable<Claim> GetClaims(string token);

    /// <summary>
    /// 获取声明的数据
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    TokenData GetTokenData(string token);

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<TokenResult> RefreshTokenAsync(string token);

    /// <summary>
    /// 从缓存中判断Token是否存在
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> ExistsTokenAsync(string token);

    /// <summary>
    /// 从缓存中移除Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> RemoveTokenAsync(string token);

    /// <summary>
    /// 转数据
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    TokenData ToTokenData(Claim[] claims);
}
