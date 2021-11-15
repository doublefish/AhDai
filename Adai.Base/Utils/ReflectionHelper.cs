using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Adai.Base.Utils
{
	/// <summary>
	/// ReflectionHelper
	/// </summary>
	public static class ReflectionHelper
	{
		static readonly IDictionary<string, ICollection<PropertyInfo>> Properties;
		static readonly object Locker;

		/// <summary>
		/// 构造函数
		/// </summary>
		static ReflectionHelper()
		{
			Properties = new Dictionary<string, ICollection<PropertyInfo>>();
			Locker = new object();
		}

		/// <summary>
		/// 获取属性
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ICollection<PropertyInfo> GetProperties<T>() where T : class
		{
			var type = typeof(T);
			return GetProperties(type);
		}

		/// <summary>
		/// 获取属性
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static ICollection<PropertyInfo> GetProperties(Type type)
		{
			lock (Locker)
			{
				if (!Properties.TryGetValue(type.FullName, out var properties))
				{
					properties = type.GetProperties();
					Properties.Add(type.FullName, properties);
				}
				return properties;
			}
		}

		/// <summary>
		/// BuildGetter
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertySelector"></param>
		/// <returns></returns>
		public static Func<S, T> BuildGetter<S, T>(Expression<Func<S, T>> propertySelector)
		{
			return propertySelector.GetPropertyInfo().GetGetMethod().CreateDelegate<Func<S, T>>();
		}

		/// <summary>
		/// BuildSet
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertySelector"></param>
		/// <returns></returns>
		public static Action<S, T> BuildSetter<S, T>(Expression<Func<S, T>> propertySelector)
		{
			return propertySelector.GetPropertyInfo().GetSetMethod().CreateDelegate<Action<S, T>>();
		}

		/// <summary>
		/// CreateDelegate
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="method"></param>
		/// <returns></returns>
		public static T CreateDelegate<T>(this MethodInfo method) where T : class
		{
			return Delegate.CreateDelegate(typeof(T), method) as T;
		}

		/// <summary>
		/// GetPropertyInfo
		/// </summary>
		/// <typeparam name="S"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertySelector"></param>
		/// <returns></returns>
		public static PropertyInfo GetPropertyInfo<S, T>(this Expression<Func<S, T>> propertySelector)
		{
			if (!(propertySelector.Body is MemberExpression body))
			{
				throw new MissingMemberException("Something went wrong.");
			}
			return body.Member as PropertyInfo;
		}
	}
}
