using AhDai.Base.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AhDai.Base.Utils
{
	/// <summary>
	/// ReflectionHelper
	/// </summary>
	public static class ReflectionUtil
	{
		static readonly IDictionary<string, ICollection<PropertyInfo>> Properties;
		static readonly object Locker;

		/// <summary>
		/// 构造函数
		/// </summary>
		static ReflectionUtil()
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
			if (!Properties.TryGetValue(type.FullName, out var properties))
			{
				lock (Locker)
				{
					properties = type.GetProperties();
					Properties[type.FullName] = properties;
				}
			}
			return properties;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <typeparam name="T0">目标类型</typeparam>
		/// <typeparam name="T1">源类型</typeparam>
		/// <param name="source">源</param>
		/// <param name="mappings">属性名不同的映射关系（k=目标属性名，v=源属性名）</param>
		/// <returns></returns>
		public static T0 ConvertTo<T0, T1>(T1 source, IDictionary<string, string> mappings = null)
			where T0 : class, new()
			where T1 : class, new()
		{
			var targetProperties = GetProperties<T0>();
			var sourceProperties = GetProperties<T1>();
			var target = Activator.CreateInstance<T0>();

			foreach (var targetPi in targetProperties)
			{
				if (!targetPi.CanWrite)
				{
					continue;
				}
				var sourceName = targetPi.Name;
				if (mappings != null && mappings.TryGetValue(targetPi.Name, out var _sourceName))
				{
					sourceName = _sourceName;
				}
				var sourcePi = sourceProperties.Where(o => o.Name == sourceName).FirstOrDefault();
				if (sourcePi == null || !sourcePi.CanRead)
				{
					continue;
				}
				var value = sourcePi.GetValue(source);
				if (value == null)
				{
					continue;
				}
				targetPi.SetValueExt(target, value);
			}
			return target;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <typeparam name="T0">目标类型</typeparam>
		/// <typeparam name="T1">源类型</typeparam>
		/// <param name="sources">源</param>
		/// <param name="mappings">属性名不同的映射关系（k=目标属性名，v=源属性名）</param>
		/// <returns></returns>
		public static ICollection<T0> ConvertTo<T0, T1>(IEnumerable<T1> sources, IDictionary<string, string> mappings = null)
			where T0 : class, new()
			where T1 : class, new()
		{
			var list = new List<T0>();
			foreach (var source in sources)
			{
				list.Add(ConvertTo<T0, T1>(source, mappings));
			}
			return list;
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
#if NET5_0_OR_GREATER
			if (propertySelector.Body is not MemberExpression body)
			{
				throw new MissingMemberException("Something went wrong.");
			}
#else
			if (!(propertySelector.Body is MemberExpression body))
			{
				throw new MissingMemberException("Something went wrong.");
			}
#endif
			return body.Member as PropertyInfo;
		}
	}
}
