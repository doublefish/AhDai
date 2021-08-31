﻿using Adai.Security;
using System;

namespace Adai.Standard
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
		/// 有效时间
		/// </summary>
		static TimeSpan expiry;

		/// <summary>
		/// 有效时间
		/// </summary>
		public static TimeSpan Expiry
		{
			get
			{
				if (expiry == null || expiry == TimeSpan.Zero)
				{
					var config = JsonConfigHelper.Get("Token.Expiry");
					if (config != null)
					{
						expiry = config.ToObject<TimeSpan>();
					}
					else
					{
						throw new Exception("Not configured.");
					}
				}
				return expiry;
			}
		}

		/// <summary>
		/// 设置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static Model.Token<T> Set<T>(T data, string suffix = null) where T : Model.TokenData
		{
			var key = $"{CacheKey}-{suffix}";
			var sign = $"{data.Username}-{data.Platform}";
			var token = new Model.Token<T>()
			{
				Id = data.Id,
				Signature = MD5Helper.Encrypt(sign),
				Expiry = DateTime.UtcNow.Add(Expiry),
				Data = data
			};
			RedisHelper.GetDatabase(15).HashSet(key, token.Signature, token);
			return token;
		}

		/// <summary>
		/// 获取
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="signature"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static Model.Token<T> Get<T>(string signature, string suffix = null) where T : Model.TokenData
		{
			if (string.IsNullOrEmpty(signature))
			{
				return null;
			}
			var key = $"{CacheKey}-{suffix}";
			return RedisHelper.GetDatabase(15).HashGet<Model.Token<T>>(key, signature);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="signature"></param>
		/// <param name="token"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static bool Verify<T>(string signature, out Model.Token<T> token, string suffix = null) where T : Model.TokenData
		{
			if (string.IsNullOrEmpty(signature))
			{
				token = null;
				return false;
			}
			var redis = RedisHelper.GetDatabase(15);
			var key = $"{CacheKey}-{suffix}";
			token = redis.HashGet<Model.Token<T>>(key, signature);
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