using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Adai.Standard.Extend
{
	/// <summary>
	/// RedisExt
	/// </summary>
	public static class RedisExt
	{
		/// <summary>
		/// Get
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static T Get<T>(this IDatabase db, RedisKey key, CommandFlags flags = CommandFlags.None) where T : class
		{
			var value = db.StringGet(key, flags);
			if (!value.HasValue)
			{
				return default;
			}
			return JsonHelper.DeserializeObject<T>(value);
		}

		/// <summary>
		/// Get
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="expiry"></param>
		/// <param name="when"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static bool Set<T>(this IDatabase db, RedisKey key, T value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
		{
			var json = JsonHelper.SerializeObject(value);
			return db.StringSet(key, json, expiry, when, flags);
		}

		/// <summary>
		/// HashGet
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="hashField"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static T HashGet<T>(this IDatabase db, RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) where T : class
		{
			var value = db.HashGet(key, hashField, flags);
			if (!value.HasValue)
			{
				return default;
			}
			return JsonHelper.DeserializeObject<T>(value);
		}

		/// <summary>
		/// HashGet
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="hashFields"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static ICollection<T> HashGet<T>(this IDatabase db, RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) where T : class
		{
			var values = db.HashGet(key, hashFields, flags);
			if (values == null || values.Length == 0)
			{
				return default;
			}
			var results = new List<T>();
			foreach (var value in values)
			{
				if (!value.HasValue)
				{
					continue;
				}
				results.Add(JsonHelper.DeserializeObject<T>(value));
			}
			return results;
		}

		/// <summary>
		/// HashValues
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="hashField"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static ICollection<T> HashValues<T>(this IDatabase db, RedisKey key, CommandFlags flags = CommandFlags.None) where T : class
		{
			var values = db.HashValues(key, flags);
			if (values == null || values.Length == 0)
			{
				return default;
			}
			var results = new List<T>();
			foreach (var value in values)
			{
				results.Add(JsonHelper.DeserializeObject<T>(value));
			}
			return results;
		}

		/// <summary>
		/// HashSet
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="hashField"></param>
		/// <param name="value"></param>
		/// <param name="when"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static bool HashSet<T>(this IDatabase db, RedisKey key, RedisValue hashField, T value, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
		{
			var json = JsonHelper.SerializeObject(value);
			return db.HashSet(key, hashField, json, when, flags);
		}

		/// <summary>
		/// HashSet
		/// </summary>
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="hashFields"></param>
		/// <param name="flags"></param>
		public static void HashSet<T>(this IDatabase db, RedisKey key, RedisValue[] hashFields, T[] values, CommandFlags flags = CommandFlags.None) where T : class
		{
			var hashEntries = new HashEntry[hashFields.Length];
			for (var i = 0; i < hashFields.Length; i++)
			{
				hashEntries[i] = new HashEntry(hashFields[i], JsonHelper.SerializeObject(values[i]));
			}
			db.HashSet(key, hashEntries, flags);
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="value"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static int ToInt32(this RedisValue value, int error = int.MinValue)
		{
			if (value.IsNullOrEmpty || !value.IsInteger)
			{
				return error;
			}
			return value.TryParse(out int result) ? result : error;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="value"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static long ToInt64(this RedisValue value, long error = long.MinValue)
		{
			if (value.IsNullOrEmpty)
			{
				return error;
			}
			return value.TryParse(out long result) ? result : error;
		}
	}
}
