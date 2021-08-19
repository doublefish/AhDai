using System;
using System.Linq;

namespace Adai.Extension
{
	/// <summary>
	/// ArrayExt
	/// </summary>
	public static class ArrayExt
	{
		#region 类型转换
		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static byte[] ToByte(this string[] array)
		{
			var result = new byte[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToByte();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static sbyte[] ToSByte(this string[] array)
		{
			var result = new sbyte[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToSByte();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static short[] ToInt16(this string[] array)
		{
			var result = new short[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToInt16();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static int[] ToInt32(this string[] array)
		{
			var result = new int[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToInt32();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static long[] ToInt64(this string[] array)
		{
			var result = new long[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToInt64();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static float[] ToSingle(this string[] array)
		{
			var result = new float[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToSingle();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static double[] ToDouble(this string[] array)
		{
			var result = new double[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToDouble();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static decimal[] ToDecimal(this string[] array)
		{
			var result = new decimal[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToDecimal();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static DateTime[] ToDateTime(this string[] array)
		{
			var result = new DateTime[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToDateTime();
			}
			return result;
		}

		/// <summary>
		/// 类型转换
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static TimeSpan[] ToTimeSpan(this string[] array)
		{
			var result = new TimeSpan[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
				result[i] = array[i].ToTimeSpan();
			}
			return result;
		}
		#endregion

		/// <summary>
		/// 对比
		/// </summary>
		/// <param name="bytesA"></param>
		/// <param name="bytesB"></param>
		/// <returns></returns>
		public static bool Compare(this byte[] bytesA, byte[] bytesB)
		{
			if (bytesA.Length != bytesB.Length)
			{
				return false;
			}
			for (var i = 0; i < bytesA.Length; i++)
			{
				if (bytesA[i] != bytesB[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 是否存在指定属性
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool ExistByStartsWith(this string[] array, string value)
		{
			return array.Contains(value) || array.Any(item => item.StartsWith(value + "."));
		}

		/// <summary>
		/// 读取子级属性
		/// </summary>
		/// <param name="array"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string[] ListByStartsWith(this string[] array, string value)
		{
			var children = array.Where(item => item.StartsWith(value + ".")).Distinct().ToArray();
			for (var i = 0; i < children.Length; i++)
			{
				var child = children[i];
				var index = child.IndexOf('.');
				children[i] = child.Substring(index + 1);
			}
			return children;
		}
	}
}
