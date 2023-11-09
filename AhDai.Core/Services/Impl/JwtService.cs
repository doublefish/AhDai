using AhDai.Core.Configs;
using AhDai.Core.Extensions;
using AhDai.Core.Models;
using AhDai.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AhDai.Core.Services.Impl
{
	/// <summary>
	/// Jwt服务
	/// </summary>
	public class JwtService : IJwtService
    {
        /// <summary>
        /// 配置
        /// </summary>
        public JwtConfig Config { get; private set; }
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger<JwtService> Logger { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
        {
            Config = configuration.GetJwtConfig();
            Logger = logger;
            Logger.Debug("", $"Init=>Config={JsonUtil.Serialize(Config)}");
        }

        /// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TokenResult GenerateToken(TokenData data)
        {
            var claims = new List<Claim>();
            if (data.Id != null)
            {
                claims.Add(new Claim("Id", data.Id));
            }
            if (data.Username != null)
            {
                claims.Add(new Claim("Username", data.Username));
            }
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
            var result = GenerateToken(claims.ToArray());
            result.Id = data.Id;
            result.Username = data.Username;

            return result;
        }

        /// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public TokenResult GenerateToken(params Claim[] claims)
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
        public IEnumerable<Claim> GetClaims(string token)
        {
            token = token[7..];
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt.Claims;
        }

        /// <summary>
        /// 获取声明的数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public TokenData GetTokenData(string token)
        {
            var claims = GetClaims(token);
            return ToTokenData(claims.ToArray());
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public TokenResult RefreshToken(string token)
        {
            var claims = GetClaims(token);
            var data = ToTokenData(claims.ToArray());
            return GenerateToken(data);
        }

        /// <summary>
        /// 转数据
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public TokenData ToTokenData(Claim[] claims)
        {
            var data = new TokenData();
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
                        data.Extensions = new Dictionary<string, string>();
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
}
