using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adai.Extensions
{
	/// <summary>
	/// ObjectExt
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// 克隆
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="obj"></param>
		/// <param name="ignores">忽略的属性</param>
		public static T Clone<T>(this T obj, params string[] ignores) where T : class, new()
		{
			var type = typeof(T);
			var data = new T();
			foreach (var pi in type.GetProperties())
			{
				if (ignores.Contains(pi.Name) || pi.CanRead == false)
				{
					continue;
				}
				var targetPi = type.GetProperty(pi.Name);
				if (targetPi.CanWrite == false)
				{
					continue;
				}
				var value = pi.GetValue(obj, null);
				targetPi.SetValue(data, value, null);
			}
			return data;
		}

		/// <summary>
		/// 设置默认值为对应类型的最小值
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="values">例外的默认值，格式：属性名称-值</param>
		public static void SetMinValue(this object obj, IDictionary<string, object> values = null)
		{
			var propertyInfos = obj.GetType().GetProperties();
			foreach (var pi in propertyInfos)
			{
				if (pi.CanWrite == false)
				{
					continue;
				}
				if (values != null && values.TryGetValue(pi.Name, out var value))
				{
					pi.SetValue(obj, value);
					continue;
				}

				switch (pi.PropertyType.FullName)
				{
					case "System.Byte":
						pi.SetValue(obj, byte.MinValue);
						break;
					case "System.SByte":
						pi.SetValue(obj, sbyte.MinValue);
						break;
					case "System.Char":
						pi.SetValue(obj, char.MinValue);
						break;
					case "System.String":
						pi.SetValue(obj, string.Empty);
						break;
					case "System.Int16":
						pi.SetValue(obj, short.MinValue);
						break;
					case "System.Int32":
						pi.SetValue(obj, int.MinValue);
						break;
					case "System.Int64":
						pi.SetValue(obj, long.MinValue);
						break;
					case "System.UInt16":
						pi.SetValue(obj, ushort.MinValue);
						break;
					case "System.UInt32":
						pi.SetValue(obj, uint.MinValue);
						break;
					case "System.UInt64":
						pi.SetValue(obj, ulong.MinValue);
						break;
					case "System.Boolean":
						pi.SetValue(obj, false);
						break;
					case "System.Single":
						pi.SetValue(obj, float.MinValue);
						break;
					case "System.Double":
						pi.SetValue(obj, double.MinValue);
						break;
					case "System.Decimal":
						pi.SetValue(obj, decimal.MinValue);
						break;
					case "System.DateTime":
						pi.SetValue(obj, DateTime.MinValue);
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// 是否最小值
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsMinValue(this object obj)
		{
			var type = obj.GetType();
			switch (type.FullName)
			{
				case "System.Byte": return obj.Equals(byte.MinValue);
				case "System.SByte": return obj.Equals(sbyte.MinValue);
				case "System.Char": return obj.Equals(char.MinValue);
				case "System.String": return obj.Equals(string.Empty);
				case "System.Int16": return obj.Equals(short.MinValue);
				case "System.Int32": return obj.Equals(int.MinValue);
				case "System.Int64": return obj.Equals(long.MinValue);
				case "System.UInt16": return obj.Equals(ushort.MinValue);
				case "System.UInt32": return obj.Equals(uint.MinValue);
				case "System.UInt64": return obj.Equals(ulong.MinValue);
				case "System.Boolean": return obj.Equals(false);
				case "System.Single": return obj.Equals(float.MinValue);
				case "System.Double": return obj.Equals(double.MinValue);
				case "System.Decimal": return obj.Equals(decimal.MinValue);
				case "System.DateTime": return obj.Equals(DateTime.MinValue);
				default: return true;
			}
		}
	}
}
