using AhDai.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// Jwt服务
/// </summary>
public interface IBaseJwtService
{
    /// <summary>
    /// 生成Token
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<TokenResult> GenerateTokenAsync(TokenData data);

    /// <summary>
    /// 生成Token
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    Task<TokenResult> GenerateTokenAsync(Claim[] claims);

    /// <summary>
    /// 读取Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    JwtSecurityToken ReadToken(string token);

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
    /// 从缓存验证Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> ValidateTokenAsync(string token);

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
