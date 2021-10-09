using System;
using System.Collections.Generic;
using System.Linq;

namespace Adai.DbContext.Extensions
{
	/// <summary>
	/// ObjectExtensions
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
			var res = type.FullName switch
			{
				"System.Byte" => obj.Equals(byte.MinValue),
				"System.SByte" => obj.Equals(sbyte.MinValue),
				"System.Char" => obj.Equals(char.MinValue),
				"System.String" => obj.Equals(string.Empty),
				"System.Int16" => obj.Equals(short.MinValue),
				"System.Int32" => obj.Equals(int.MinValue),
				"System.Int64" => obj.Equals(long.MinValue),
				"System.UInt16" => obj.Equals(ushort.MinValue),
				"System.UInt32" => obj.Equals(uint.MinValue),
				"System.UInt64" => obj.Equals(ulong.MinValue),
				"System.Boolean" => obj.Equals(false),
				"System.Single" => obj.Equals(float.MinValue),
				"System.Double" => obj.Equals(double.MinValue),
				"System.Decimal" => obj.Equals(decimal.MinValue),
				"System.DateTime" => obj.Equals(DateTime.MinValue),
				_ => false,
			};
			return res;
		}
	}
}
