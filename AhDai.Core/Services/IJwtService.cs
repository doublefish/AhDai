using AhDai.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace AhDai.Core.Services
{
    /// <summary>
    /// Jwt服务
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        TokenResult GenerateToken(TokenData data);

        /// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        TokenResult GenerateToken(params Claim[] claims);

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
        TokenResult RefreshToken(string token);

        /// <summary>
        /// 转数据
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        TokenData ToTokenData(Claim[] claims);
    }
}
