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
		static JObject configuration;

		/// <summary>
		/// Configuration
		/// </summary>
		public static JObject Configuration
		{
			get
			{
				if (configuration == null)
				{
					var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
					var json = string.Empty;
					using (var sr = new StreamReader(path, Encoding.UTF8))
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
