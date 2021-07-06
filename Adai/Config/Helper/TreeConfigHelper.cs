using System.Collections.Generic;

namespace Adai.Config
{
	/// <summary>
	/// ConfigHelper
	/// </summary>
	/// <typeparam name="TConfig"></typeparam>
	/// <typeparam name="TK"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public static class TreeConfigHelper<TConfig, TK, TKey, TValue> where TConfig : Model.TreeConfig<TK, TKey, TValue>, new()
	{
		/// <summary>
		/// Config
		/// </summary>
		public static readonly TConfig Config = new TConfig();

		/// <summary>
		/// 所有
		/// </summary>
		public static IDictionary<TK, IDictionary<TKey, TValue>> All => Config;

		/// <summary>
		/// 是否包含指定的键
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool ContainsKey(TKey key) => Config.ContainsKey(key);

		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue GetValue(TKey key) => Config.GetValue(key);

		/// <summary>
		/// 获取指定的项
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public static IDictionary<TK, IDictionary<TKey, TValue>> Filter(params TKey[] keys) => Config.Filter(keys);
	}
}
