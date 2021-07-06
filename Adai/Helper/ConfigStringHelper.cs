using System.Collections.Generic;

namespace Adai
{
	/// <summary>
	/// ConfigStringHelper
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class ConfigStringHelper<T> where T : Model.Config<string>, new()
	{
		/// <summary>
		/// Config
		/// </summary>
		public static readonly T Config = new T();

		/// <summary>
		/// 所有
		/// </summary>
		public static IDictionary<string, string> All => Config;

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
		/// 获取指定键的值
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetKey(string value) => Config.GetKey(value);

		/// <summary>
		/// 获取指定键的内容
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public static IDictionary<string, string> Filter(params string[] keys) => Config.Filter(keys);
	}
}
