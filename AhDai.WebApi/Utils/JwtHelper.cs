using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AhDai.WebApi.Utils
{
	/// <summary>
	/// JwtHelper
	/// </summary>
	public static class JwtHelper
	{
		/// <summary>
		/// CreateToken-Bearer
		/// </summary>
		/// <param name="issuer">发行人</param>
		/// <param name="audience">受众人</param>
		/// <param name="sign">签名凭证，长度必须大于等于16个字符</param>
		/// <param name="claims">定义许多种的声明Claim，信息存储部分，Claims的实体一般包含用户和一些元数据</param>
		/// <returns></returns>
		public static string CreateToken(string issuer, string audience, string sign, params Claim[] claims)
		{
			// notBefore 生效时间
			// long nbf = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
			var notBefore = DateTime.UtcNow;
			// expires   //过期时间
			// long Exp = new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds();
			var expires = DateTime.UtcNow.AddSeconds(1000);
			var secret = Encoding.UTF8.GetBytes(sign);
			var key = new SymmetricSecurityKey(secret);
			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var jwt = new JwtSecurityToken(issuer, audience, claims, notBefore, expires, signingCredentials);
			var JwtHander = new JwtSecurityTokenHandler();
			var token = JwtHander.WriteToken(jwt);
			//return new
			//{
			//	access_token = token,
			//	token_type = "Bearer",
			//};
			return token;
		}

		/// <summary>
		/// 获取声明的值
		/// </summary>
		/// <param name="httpRequest"></param>
		/// <param name="claimName"></param>
		/// <returns></returns>
		public static string GetClaimValue(HttpRequest httpRequest, string claimName)
		{
			var userValues = httpRequest.Headers["Authorization"];
			if (userValues.Count == 0)
			{
				return null;
			}
			var token = userValues[0];
			if (string.IsNullOrEmpty(token) || token.Length < 15)
			{
				return null;
			}
			// token.Substring(7, token.Length - 7);
			token = token[7..];
			var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
			var claim = jwt.Claims.Where(o => o.Type == claimName).FirstOrDefault();
			return claim?.Value;
		}
	}
}
