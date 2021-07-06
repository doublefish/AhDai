using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Adai
{
	/// <summary>
	/// ResourceHelper
	/// </summary>
	public static class ResourceHelper
	{
		static readonly IDictionary<string, ResourceManager> resources = new Dictionary<string, ResourceManager>();
		static readonly object lockOfResources = new object();

		/// <summary>
		/// 获取资源文件
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ResourceManager Get<T>() where T : class
		{
			var type = typeof(T);
			return Get(type);
		}

		/// <summary>
		/// 获取资源文件
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static ResourceManager Get(Type type)
		{
			return Get(type.Name, type.Assembly);
		}

		/// <summary>
		/// 获取指定类型的属性
		/// </summary>
		/// <param name="baseName"></param>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static ResourceManager Get(string baseName, Assembly assembly)
		{
			lock (lockOfResources)
			{
				if (!resources.ContainsKey(baseName))
				{
					resources.Add(baseName, new ResourceManager(baseName, assembly));
				}
				return resources[baseName];
			}
		}
	}
}
