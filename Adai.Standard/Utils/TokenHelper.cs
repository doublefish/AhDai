using Adai.Security.Utils;
using Adai.Standard.Extensions;
using System;

namespace Adai.Standard.Utils
{
	/// <summary>
	/// TokenHelper
	/// </summary>
	public static class TokenHelper
	{
		/// <summary>
		/// CacheKey
		/// </summary>
		public static readonly string CacheKey = "Token";

		/// <summary>
		/// 过期时间
		/// </summary>
		public static readonly TimeSpan Expiry = new TimeSpan(0, 30, 0);

		/// <summary>
		/// 设置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static Models.Token<T> Set<T>(T data, string suffix = null) where T : Models.TokenData
		{
			var key = $"{CacheKey}-{suffix}";
			var sign = $"{data.Username}-{data.Platform}";
			var token = new Models.Token<T>()
			{
				Id = data.Id,
				Signature = MD5Helper.Encrypt(sign),
				Expiry = DateTime.UtcNow.Add(Expiry),
				Data = data
			};
			Redis.Helper.GetDatabase(15).HashSet(key, token.Signature, token);
			return token;
		}

		/// <summary>
		/// 获取
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="signature"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static Models.Token<T> Get<T>(string signature, string suffix = null) where T : Models.TokenData
		{
			if (string.IsNullOrEmpty(signature))
			{
				return null;
			}
			var key = $"{CacheKey}-{suffix}";
			return Redis.Helper.GetDatabase(15).HashGet<Models.Token<T>>(key, signature);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="signature"></param>
		/// <param name="token"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static bool Verify<T>(string signature, out Models.Token<T> token, string suffix = null) where T : Models.TokenData
		{
			if (string.IsNullOrEmpty(signature))
			{
				token = null;
				return false;
			}
			var redis = Redis.Helper.GetDatabase(15);
			var key = $"{CacheKey}-{suffix}";
			token = redis.HashGet<Models.Token<T>>(key, signature);
			if (token == null || token.Expiry < DateTime.UtcNow)
			{
				return false;
			}
			//续期
			token.Expiry = DateTime.UtcNow.Add(Expiry);
			redis.HashSet(key, token.Signature, token);
			return true;
		}
	}
}