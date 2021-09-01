using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Adai.Standard
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
		/// <param name="db"></param>
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
		/// <param name="db"></param>
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
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="hashFields"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static T[] HashGet<T>(this IDatabase db, RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) where T : class
		{
			return db.HashGet(key, hashFields, flags).ToObjectArray<T>();
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
		public static bool HashSet<T>(this IDatabase db, RedisValue key, RedisValue hashField, T value, When when = When.Always, CommandFlags flags = CommandFlags.None) where T : class
		{
			var json = JsonHelper.SerializeObject(value);
			return db.HashSet(key, hashField, json, when, flags);
		}

		/// <summary>
		/// HashSet
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="hash"></param>
		/// <param name="flags"></param>
		public static void HashSet<T>(this IDatabase db, RedisKey key, IDictionary<RedisValue, T> hash, CommandFlags flags = CommandFlags.None) where T : class
		{
			var array = new HashEntry[hash.Count];
			var i = 0;
			foreach (var kv in hash)
			{
				array[i] = new HashEntry(kv.Key, JsonHelper.SerializeObject(kv.Value));
				i++;
			}
			db.HashSet(key, array, flags);
		}

		/// <summary>
		/// HashGetAll
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static IDictionary<string, T> HashGetAll<T>(this IDatabase db, RedisKey key, CommandFlags flags = CommandFlags.None) where T : class
		{
			return db.HashGetAll(key, flags).ToObjectDictionary<T>();
		}

		/// <summary>
		/// HashValues
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db"></param>
		/// <param name="key"></param>
		/// <param name="flags"></param>
		/// <returns></returns>
		public static T[] HashValues<T>(this IDatabase db, RedisKey key, CommandFlags flags = CommandFlags.None) where T : class
		{
			return db.HashValues(key, flags).ToObjectArray<T>();
		}

		/// <summary>
		/// 转换为整形
		/// </summary>
		/// <param name="value"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static int ToInt32(this RedisValue value, int error = int.MinValue)
		{
			if (!value.HasValue || !value.IsInteger)
			{
				return error;
			}
			return value.TryParse(out int result) ? result : error;
		}

		/// <summary>
		/// 转换为长整形
		/// </summary>
		/// <param name="value"></param>
		/// <param name="error"></param>
		/// <returns></returns>
		public static long ToInt64(this RedisValue value, long error = long.MinValue)
		{
			if (!value.HasValue || !value.IsInteger)
			{
				return error;
			}
			return value.TryParse(out long result) ? result : error;
		}

		/// <summary>
		/// 从RedisValue数组创建一个整形数组
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static int[] ToInt32Array(this RedisValue[] values)
		{
			if (values == null)
			{
				return null;
			}
			var array = new int[values.Length];
			for (var i = 0; i < values.Length; i++)
			{
				array[i] = values[i].ToInt32();
			}
			return array;
		}

		/// <summary>
		/// 从RedisValue数组创建一个长整型数组
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static long[] ToInt64Array(this RedisValue[] values)
		{
			if (values == null)
			{
				return null;
			}
			var array = new long[values.Length];
			for (var i = 0; i < values.Length; i++)
			{
				array[i] = values[i].ToInt64();
			}
			return array;
		}

		/// <summary>
		/// 从RedisValue数组创建一个长整型数组
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static string[] ToStringArray(this RedisValue[] values)
		{
			return ExtensionMethods.ToStringArray(values);
		}

		/// <summary>
		/// 从RedisValue数组（Json字符串）创建一个指定类型的对象数组
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="values"></param>
		/// <returns></returns>
		public static T[] ToObjectArray<T>(this RedisValue[] values) where T : class
		{
			if (values == null)
			{
				return null;
			}
			var array = new T[values.Length];
			for (var i = 0; i < values.Length; i++)
			{
				array[i] = JsonHelper.DeserializeObject<T>(values[i]);
			}
			return array;
		}

		/// <summary>
		/// 从HashEntry数组创建一个字典（value的类型是字符串）
		/// </summary>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static IDictionary<string, string> ToStringDictionary(this HashEntry[] hash)
		{
			return ExtensionMethods.ToStringDictionary(hash);
		}

		/// <summary>
		/// 从HashEntry数组（value的类型是Json字符串）创建一个字典（value的类型是指定对象）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static IDictionary<string, T> ToObjectDictionary<T>(this HashEntry[] hash) where T : class
		{
			var dict = new Dictionary<string, T>();
			foreach (var item in hash)
			{
				dict.Add(item.Name, JsonHelper.DeserializeObject<T>(item.Value));
			}
			return dict;
		}
	}
}
