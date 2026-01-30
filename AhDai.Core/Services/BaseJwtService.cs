using AhDai.Base;
using AhDai.Core.Configs;
using AhDai.Core.Models;
using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AhDai.Core.Services;

/// <summary>
/// Jwt服务
/// </summary>
/// <param name="configuration"></param>
/// <param name="redisService"></param>
public class BaseJwtService(IConfiguration configuration, IBaseRedisService? redisService) : IBaseJwtService
{
    readonly JwtConfig _config = configuration.GetJwtConfig();
    readonly IBaseRedisService? _redisService = redisService;
    SigningCredentials? _signingCredentials;

    /// <summary>
    /// 配置
    /// </summary>
    public JwtConfig Config => _config;
    /// <summary>
    /// RSA
    /// </summary>
    public SigningCredentials SigningCredentials
    {
        get
        {
            if (_signingCredentials == null)
            {
                var rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(Config.PrivateKey), out _);
                _signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
            }
            return _signingCredentials;
        }
    }
    /// <summary>
    /// RedisService
    /// </summary>
    public IBaseRedisService RedisService => _redisService ?? throw new ArgumentException("未注入IBaseRedisService");

    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="data"></param>
    /// <param name="expiration">分钟</param>
    /// <returns></returns>
    public virtual async Task<TokenResult> GenerateTokenAsync(TokenData data, int? expiration = null)
    {
        var claims = new List<Claim>()
        {
            new("Id", data.Id),
            new("Username", data.Username),
            new(ClaimTypes.NameIdentifier, data.Username)
        };
        if (data.Name != null)
        {
            claims.Add(new Claim("Name", data.Name));
            claims.Add(new Claim(ClaimTypes.Name, data.Name));
        }
        if (data.Type != null)
        {
            claims.Add(new Claim("Type", data.Type));
        }
        if (data.Platform != null)
        {
            claims.Add(new Claim("Platform", data.Platform));
        }
        if (data.Extensions != null)
        {
            foreach (var item in data.Extensions)
            {
                claims.Add(new Claim(item.Key, string.Join(",", item.Value)));
            }
        }
        var result = await GenerateTokenAsync([.. claims], expiration);
        result.Id = data.Id;
        result.Username = data.Username;
        return result;
    }

    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="expiration">分钟</param>
    /// <returns></returns>
    public virtual async Task<TokenResult> GenerateTokenAsync(Claim[] claims, int? expiration = null)
    {
        var expiryMinutes = expiration ?? Config.Expiration;
        var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var jwt = new JwtSecurityToken(
            issuer: Config.Issuer,
            audience: Config.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: SigningCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        if (Config.EnableRedis)
        {
            var redis = RedisService;
            var dict = claims.GroupBy(claim => claim.Type).ToDictionary(g => g.Key, g => string.Join(",", g.Select(c => c.Value)));
            dict.Add("Issuer", Config.Issuer);
            dict.Add("IssueTime", DateTime.Now.ToString(Const.StandardDateTimeFormat));
            dict.Add("ExpirationTime", expires.ToLocalTime().ToString(Const.StandardDateTimeFormat));
            dict.Add("Token", token);
            if (!dict.TryGetValue("Username", out var username) || string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username不可为空");
            }
            var value = JsonUtil.Serialize(dict);
            var rdb = redis.GetDatabase();
            await rdb.StringSetAsync($"{Config.RedisKey}:{username}:{token}", value, TimeSpan.FromMinutes(expiryMinutes));
        }
        return new TokenResult()
        {
            Token = token,
            Expiration = expires,
            Type = "Bearer"
        };
    }

    /// <summary>
    /// 获取声明的数据
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual JwtSecurityToken ReadToken(string token)
    {
        var data = token.Split(' ')[1];
        return new JwtSecurityTokenHandler().ReadJwtToken(data);
    }

    /// <summary>
    /// 获取声明的数据
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual TokenData GetTokenData(string token)
    {
        var securityToken = ReadToken(token);
        return ToTokenData([.. securityToken.Claims]);
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<TokenResult> RefreshTokenAsync(string token)
    {
        var securityToken = ReadToken(token);
        return await GenerateTokenAsync([.. securityToken.Claims]);
    }

    /// <summary>
    /// 从缓存验证Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<bool> ValidateTokenAsync(string token)
    {
        if (!Config.EnableRedis) return false;
        var securityToken = ReadToken(token);
        var username = securityToken.Claims.Where(x => x.Type == "Username").FirstOrDefault()?.Value;
        if (string.IsNullOrEmpty(username)) return false;
        var data = securityToken.RawData;
        var redis = RedisService;
        var rdb = redis.GetDatabase();
        return await rdb.KeyExistsAsync($"{Config.RedisKey}:{username}:{data}");
    }

    /// <summary>
    /// 从缓存中移除Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveTokenAsync(string token)
    {
        if (!Config.EnableRedis || string.IsNullOrEmpty(token)) return false;
        var securityToken = ReadToken(token);
        var username = securityToken.Claims.Where(x => x.Type == "Username").FirstOrDefault()?.Value;
        var data = securityToken.RawData;
        var redis = RedisService;
        var rdb = redis.GetDatabase();
        return await rdb.KeyDeleteAsync($"{Config.RedisKey}:{username}:{data}");
    }

    /// <summary>
    /// 转数据
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public virtual TokenData ToTokenData(Claim[] claims)
    {
        var data = new TokenData()
        {
            Extensions = new Dictionary<string, ICollection<string>>(),
        };
        foreach (var c in claims)
        {
            switch (c.Type)
            {
                case "Id": data.Id = c.Value; break;
                case "Username": data.Username = c.Value; break;
                case "Name": data.Name = c.Value; break;
                case "Type": data.Type = c.Value; break;
                case "Platform": data.Platform = c.Value; break;
                default:
                    if (!data.Extensions.TryGetValue(c.Type, out var values))
                    {
                        values = [];
                        data.Extensions.Add(c.Type, values);
                    }
                    values.Add(c.Value);
                    break;
            }
        }
        return data;
    }
}
