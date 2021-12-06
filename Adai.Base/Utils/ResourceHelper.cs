using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Adai.Base.Utils
{
	/// <summary>
	/// ResourceHelper
	/// </summary>
	public static class ResourceHelper
	{
		static readonly IDictionary<string, ResourceManager> Resources;
		static readonly object Locker;

		/// <summary>
		/// 构造函数
		/// </summary>
		static ResourceHelper()
		{
			Resources = new Dictionary<string, ResourceManager>();
			Locker = new object();
		}

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
			if (!Resources.TryGetValue(baseName, out var resource))
			{
				lock (Locker)
				{
					resource = new ResourceManager(baseName, assembly);
					Resources.Add(baseName, resource);
				}
			}
			return resource;
		}
	}
}
