using AhDai.Core.Consts;
using AhDai.Core.Interfaces.Services;
using AhDai.Core.Models;
using AhDai.Core.Options;
using AhDai.Core.Utils;
using Microsoft.Extensions.Options;
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
public class BaseJwtService : IBaseJwtService, IDisposable
{
    readonly JwtOptions _options;
    readonly IBaseRedisService? _redisService;
    readonly Lazy<RSA> _rsa;
    readonly Lazy<SigningCredentials> _signingCredentials;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="redisService"></param>
    public BaseJwtService(IOptions<JwtOptions> options, IBaseRedisService? redisService)
    {
        _options = options.Value;
        _redisService = redisService;

        _rsa = new Lazy<RSA>(() =>
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(_options.PrivateKey), out _);
            return rsa;
        });
        _signingCredentials = new Lazy<SigningCredentials>(() =>
        {
            return new SigningCredentials(new RsaSecurityKey(_rsa.Value), SecurityAlgorithms.RsaSha256);
        });
    }


    /// <summary>
    /// RSA
    /// </summary>
    public SigningCredentials SigningCredentials => _signingCredentials.Value;
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
                claims.Add(new Claim(item.Key, string.Join("|", item.Value)));
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
        var expiryMinutes = expiration ?? _options.Expiration;
        var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: SigningCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        if (_options.EnableRedis)
        {
            var dict = claims.GroupBy(claim => claim.Type).ToDictionary(g => g.Key, g => string.Join(",", g.Select(c => c.Value)));
            dict.Add("Issuer", _options.Issuer);
            dict.Add("IssueTime", DateTime.UtcNow.ToString(DateTimeFormats.Standard));
            dict.Add("ExpirationTime", expires.ToString(DateTimeFormats.Standard));
            dict.Add("Token", token);
            if (!dict.TryGetValue("Username", out var username) || string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username不可为空");
            }
            var key = BuildRedisKey(username, token);
            var value = JsonUtil.Serialize(dict);
            var rdb = RedisService.GetDatabase();
            await rdb.StringSetAsync(key, value, TimeSpan.FromMinutes(expiryMinutes));
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
        if (securityToken.ValidTo < DateTime.UtcNow) throw new SecurityTokenExpiredException();
        return await GenerateTokenAsync([.. securityToken.Claims]);
    }

    /// <summary>
    /// 从缓存验证Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<bool> ValidateTokenAsync(string token)
    {
        if (!_options.EnableRedis) return false;
        var securityToken = ReadToken(token);
        var username = securityToken.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;
        if (string.IsNullOrEmpty(username)) return false;
        var key = BuildRedisKey(username, securityToken.RawData);
        var rdb = RedisService.GetDatabase();
        return await rdb.KeyExistsAsync(key);
    }

    /// <summary>
    /// 从缓存中移除Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveTokenAsync(string token)
    {
        if (!_options.EnableRedis || string.IsNullOrEmpty(token)) return false;
        var securityToken = ReadToken(token);
        var username = securityToken.Claims.FirstOrDefault(x => x.Type == "Username")?.Value ?? "";
        var key = BuildRedisKey(username, securityToken.RawData);
        var rdb = RedisService.GetDatabase();
        return await rdb.KeyDeleteAsync(key);
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

    /// <summary>
    /// BuildRedisKey
    /// </summary>
    /// <param name="username"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    protected string BuildRedisKey(string username, string token)
    {
        var tokenHash = Convert.ToHexString(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(token)));
        return $"{_options.RedisKey}:{username}:{tokenHash}";
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _rsa.Value.Dispose();
        GC.SuppressFinalize(this);
    }
}
