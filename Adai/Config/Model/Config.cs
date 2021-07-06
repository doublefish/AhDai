using System.Collections.Generic;

namespace Adai.Config.Model
{
	/// <summary>
	/// 配置基类
	/// </summary>
	public class Config : Config<int>
	{

	}

	/// <summary>
	/// 配置基类
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public class Config<TKey> : Config<TKey, string>
	{

	}

	/// <summary>
	/// 配置基类
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class Config<TKey, TValue> : Dictionary<TKey, TValue>, IDictionary<TKey, TValue>
	{
		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public virtual TValue GetValue(TKey key)
		{
			TryGetValue(key, out TValue value);
			return value;
		}
		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public virtual TKey GetKey(TValue value)
		{
			foreach (var kv in this)
			{
				if (kv.Value.Equals(value))
				{
					return kv.Key;
				}
			}
			return default;
		}

		/// <summary>
		/// 获取指定键的内容
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public virtual IDictionary<TKey, TValue> Filter(params TKey[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				return this;
			}
			var dic = new Dictionary<TKey, TValue>();
			foreach (var key in keys)
			{
				if (TryGetValue(key, out var value))
				{
					dic.Add(key, value);
				}
			}
			return dic;
		}
	}
}
