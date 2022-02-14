using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Core.Utils
{
	/// <summary>
	/// ConfigurationHelper
	/// </summary>
	public static class ConfigurationHelper
	{
		/// <summary>
		/// 获取所有
		/// </summary>
		/// <param name="configuration"></param>
		public static IDictionary<string, string> GetAll(IConfiguration configuration)
		{
			var children = configuration.GetChildren();
			return GetAll(children.ToArray());
		}

		/// <summary>
		/// 获取所有
		/// </summary>
		/// <param name="sections"></param>
		public static IDictionary<string, string> GetAll(params IConfigurationSection[] sections)
		{
			var dict = new Dictionary<string, string>();
			foreach (var section in sections)
			{
				dict.Add(section.Path, section.Value);
				var children = GetAll(section.GetChildren().ToArray());
				foreach (var child in children)
				{
					dict.Add(child.Key, child.Value);
				}
			}
			return dict;
		}
	}
}
