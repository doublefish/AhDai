using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Adai.Core.WebApi
{
	/// <summary>
	/// JwtHelper
	/// </summary>
	public static class JwtHelper
	{
		/// <summary>
		/// CreateToken
		/// </summary>
		/// <returns></returns>
		public static string CreateToken()
		{
			// 定义发行人issuer
			var issuer = "127.0.0.1";
			// 定义受众人audience
			var audience = "127.0.0.1";
			// signingCredentials 签名凭证，SecurityKey 的长度必须 大于等于 16个字符
			var sign = "a2DqeIJAneJKq9h6";

			// 定义许多种的声明Claim，信息存储部分，Claims的实体一般包含用户和一些元数据
			IEnumerable<Claim> claims = new Claim[]
			{
				new Claim("open-id", "123456789")
			};
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
	}
}
