using System.Collections.Generic;

namespace Adai.Config
{
	/// <summary>
	/// ConfigIntHelper
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class ConfigIntHelper<T> where T : Model.Config<int>, new()
	{
		/// <summary>
		/// Config
		/// </summary>
		public static readonly T Config = new T();

		/// <summary>
		/// 所有
		/// </summary>
		public static IDictionary<int, string> All => Config;

		/// <summary>
		/// 是否包含指定的键
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool ContainsKey(int key) => Config.ContainsKey(key);

		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetValue(int key) => Config.GetValue(key);

		/// <summary>
		/// 获取指定键的值
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int GetKey(string value) => Config.GetKey(value);

		/// <summary>
		/// 获取指定键的内容
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public static IDictionary<int, string> Filter(params int[] keys) => Config.Filter(keys);
	}
}
