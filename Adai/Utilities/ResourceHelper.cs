using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Adai.Utilities
{
	/// <summary>
	/// ResourceHelper
	/// </summary>
	public static class ResourceHelper
	{
		static IDictionary<string, ResourceManager> resources;
		static readonly object locker = new object();

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
			lock (locker)
			{
				if (resources == null)
				{
					resources = new Dictionary<string, ResourceManager>();
				}
				if (!resources.TryGetValue(baseName, out var resource))
				{
					resource = new ResourceManager(baseName, assembly);
					resources.Add(baseName, resource);
				}
				return resource;
			}
		}
	}
}
