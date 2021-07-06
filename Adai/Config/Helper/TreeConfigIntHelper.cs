using System.Collections.Generic;

namespace Adai.Config
{
	/// <summary>
	/// TreeConfigIntHelper
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class TreeConfigIntHelper<T> where T : Model.TreeConfig<int>, new()
	{
		/// <summary>
		/// Config
		/// </summary>
		public static readonly T Config = new T();

		/// <summary>
		/// 所有
		/// </summary>
		public static IDictionary<string, IDictionary<int, string>> All => Config;

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
		/// 获取指定的项
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		public static IDictionary<string, IDictionary<int, string>> Filter(params int[] keys) => Config.Filter(keys);
	}
}
