using AhDai.Core.Configs;
using AhDai.Core.Extensions;
using AhDai.Core.Models;
using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Core.Services.Impl;

/// <summary>
/// Jwt服务
/// </summary>
public class BaseJwtServiceImpl : IBaseJwtService
{
    /// <summary>
    /// 配置
    /// </summary>
    public JwtConfig Config { get; private set; }
    /// <summary>
    /// 日志
    /// </summary>
    public ILogger<BaseJwtServiceImpl> Logger { get; private set; }
    /// <summary>
    /// Services
    /// </summary>
    public IServiceProvider Services { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="logger"></param>
    public BaseJwtServiceImpl(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<BaseJwtServiceImpl> logger)
    {
        Config = configuration.GetJwtConfig();
        Services = serviceProvider;
        if (Config.Redis > 0)
        {
            serviceProvider.GetRequiredService<IBaseRedisService>();
        }
        Logger = logger;
        Logger.LogDebug("Init=>Config={Config}", JsonUtil.Serialize(Config));
    }

    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public virtual async Task<TokenResult> GenerateTokenAsync(TokenData data)
    {
        var claims = new List<Claim>()
        {
            new("Id", data.Id),
            new("Username", data.Username)
        };
        if (data.Name != null)
        {
            claims.Add(new Claim("Name", data.Name));
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
            foreach (var kv in data.Extensions)
            {
                claims.Add(new Claim($"Ext-{kv.Key}", kv.Value));
            }
        }
        var result = await GenerateTokenAsync([.. claims]);
        result.Id = data.Id;
        result.Username = data.Username;
        return result;
    }

    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public virtual async Task<TokenResult> GenerateTokenAsync(params Claim[] claims)
    {
        var expires = DateTime.UtcNow.AddMinutes(Config.Expiration);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.SigningKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            issuer: Config.Issuer,
            audience: Config.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: signingCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        if (Config.Redis > 0)
        {
            var redis = Services.GetRequiredService<IBaseRedisService>();
            var dict = claims.ToDictionary(o => o.Type, o => o.Value);
            dict.Add("Issuer", Config.Issuer);
            dict.Add("IssueTime", DateTime.Now.ToString(Const.DateTimeFormat));
            dict.Add("Expiration", expires.ToLocalTime().ToString(Const.DateTimeFormat));
            dict.Add("Token", token);
            var value = JsonUtil.Serialize(dict);
            var rdb = redis.GetDatabase();
            if (Config.Redis == 2)
            {
                //var entries = new HashEntry[] {
                //    new(token, value),
                //    new(id, value),
                //};
                await rdb.HashSetAsync($"{Config.RedisKey}", token, value);
            }
            else
            {
                await rdb.StringSetAsync($"{Config.RedisKey}:{token}", value, TimeSpan.FromMinutes(Config.Expiration));
            }
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
    public virtual IEnumerable<Claim> GetClaims(string token)
    {
        var data = token.Split(' ')[1];
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(data);
        return jwt.Claims;
    }

    /// <summary>
    /// 获取声明的数据
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual TokenData GetTokenData(string token)
    {
        var claims = GetClaims(token);
        return ToTokenData(claims.ToArray());
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<TokenResult> RefreshTokenAsync(string token)
    {
        var claims = GetClaims(token);
        var data = ToTokenData(claims.ToArray());
        return await GenerateTokenAsync(data);
    }

    /// <summary>
    /// 从缓存中判断Token是否存在
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<bool> ExistsTokenAsync(string token)
    {
        if (Config.Redis == 0 || string.IsNullOrEmpty(token)) return false;

        var data = token.Split(' ')[1];
        var redis = Services.GetRequiredService<IBaseRedisService>();
        var rdb = redis.GetDatabase();
        if (Config.Redis == 2)
        {
            return await rdb.HashExistsAsync($"{Config.RedisKey}", data);
        }
        else
        {
            return await rdb.KeyExistsAsync($"{Config.RedisKey}:{data}");
        }
    }

    /// <summary>
    /// 从缓存中移除Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async Task<bool> RemoveTokenAsync(string token)
    {
        if (Config.Redis == 0 || string.IsNullOrEmpty(token)) return false;

        var data = token.Split(' ')[1];
        var redis = Services.GetRequiredService<IBaseRedisService>();
        var rdb = redis.GetDatabase();
        if (Config.Redis == 2)
        {
            return await rdb.HashDeleteAsync($"{Config.RedisKey}", data);
        }
        else
        {
            return await rdb.KeyDeleteAsync($"{Config.RedisKey}:{data}");
        }
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
            Extensions = new Dictionary<string, string>()
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
                    if (c.Type.StartsWith("Ext-"))
                    {
                        data.Extensions.Add(c.Type[4..], c.Value);
                    }
                    break;
            }
        }
        return data;
    }
}
