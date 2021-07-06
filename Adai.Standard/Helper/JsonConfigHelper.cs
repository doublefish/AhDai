using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Adai.Standard
{
	/// <summary>
	/// JsonConfigHelper
	/// </summary>
	public static class JsonConfigHelper
	{
		/// <summary>
		/// configuration
		/// </summary>
		static JObject configuration;

		/// <summary>
		/// 最后写入时间
		/// </summary>
		public static DateTime LastWriteTime { get; private set; }
		/// <summary>
		/// 文件路径
		/// </summary>
		public static readonly string Path = AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";

		/// <summary>
		/// Configuration
		/// </summary>
		public static JObject Configuration
		{
			get
			{
				var file = new FileInfo(Path);
				if (!file.Exists)
				{
					throw new FileNotFoundException("Configuration file does not exist.");
				}
				if (LastWriteTime <= file.LastWriteTime)
				{
					LastWriteTime = file.LastWriteTime;
					var json = string.Empty;
					using (var sr = new StreamReader(Path, Encoding.UTF8))
					{
						json = sr.ReadToEnd();
					}
					configuration = JsonHelper.DeserializeObject<JObject>(json);
					if (configuration == null)
					{
						throw new ArgumentException("Configuration file parsing error.");
					}
				}
				return configuration;
			}
		}
		/// <summary>
		/// ConnectionStrings
		/// </summary>
		public static IDictionary<string, string> ConnectionStrings => Configuration["ConnectionStrings"].ToObject<IDictionary<string, string>>();

		/// <summary>
		/// 读取
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static JToken Get(string key)
		{
			return Configuration.SelectToken(key);
		}

		/// <summary>
		/// 读取
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Get<T>(string key)
		{
			var value = Get(key);
			return value.ToObject<T>();
		}
	}
}
