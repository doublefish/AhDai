using System.Collections.Generic;

namespace Adai.Config
{
	/// <summary>
	/// TreeConfigStringHelper
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class TreeConfigStringHelper<T> where T : Model.TreeConfig<string>, new()
	{
		/// <summary>
		/// Config
		/// </summary>
		public static readonly T Config = new T();

		/// <summary>
		/// 所有
		/// </summary>
		public static IDictionary<string, IDictionary<string, string>> All => Config;

		/// <summary>
		/// 是否包含指定的键
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool ContainsKey(string key) => Config.ContainsKey(key);

		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetValue(string key) => Config.GetValue(key);

		/// <summary>
		/// 获取指定的项
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public static IDictionary<string, IDictionary<string, string>> Filter(params string[] keys) => Config.Filter(keys);
	}
}
