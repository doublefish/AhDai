using System.Collections.Generic;

namespace Adai.Config.Model
{
	/// <summary>
	/// 树形配置基类
	/// </summary>
	public class TreeConfig : TreeConfig<int>
	{
	}

	/// <summary>
	/// 树形配置基类
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public class TreeConfig<TKey> : TreeConfig<TKey, string>
	{
	}

	/// <summary>
	/// 树形配置基类
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class TreeConfig<TKey, TValue> : TreeConfig<string, TKey, TValue>
	{
	}

	/// <summary>
	/// 树形配置基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class TreeConfig<T, TKey, TValue> : Config<T, IDictionary<TKey, TValue>>
	{
		/// <summary>
		/// 是否包含
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public virtual bool ContainsKey(TKey key)
		{
			var result = false;
			foreach (var kv in this)
			{
				result = kv.Value.ContainsKey(key);
				if (result)
				{
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public virtual TValue GetValue(TKey key)
		{
			TValue result = default;
			foreach (var kv in this)
			{
				if (kv.Value.TryGetValue(key, out result))
				{
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// 获取指定键的内容
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public virtual IDictionary<T, IDictionary<TKey, TValue>> Filter(params TKey[] keys)
		{
			if (keys == null || keys.Length == 0)
			{
				return this;
			}
			var dic = new Dictionary<T, IDictionary<TKey, TValue>>();
			foreach (var kv in this)
			{
				var _dic = new Dictionary<TKey, TValue>();
				foreach (var key in keys)
				{
					if (kv.Value.TryGetValue(key, out var value))
					{
						_dic.Add(key, value);
					}
				}
				dic.Add(kv.Key, _dic);
			}
			return dic;
		}
	}
}
