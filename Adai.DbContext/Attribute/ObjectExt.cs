using System;

namespace Adai.Extend
{
	/// <summary>
	/// ObjectExt
	/// </summary>
	public static class ObjectExt
	{
		/// <summary>
		/// 是否最小值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsMinValue<T>(this T value)
		{
			var type = value.GetType();
			switch (type.FullName)
			{
				case "System.Byte": return value.Equals(byte.MinValue);
				case "System.SByte": return value.Equals(sbyte.MinValue);
				case "System.Char": return value.Equals(char.MinValue);
				case "System.String": return value.Equals(string.Empty);
				case "System.Int16": return value.Equals(short.MinValue);
				case "System.Int32": return value.Equals(int.MinValue);
				case "System.Int64": return value.Equals(long.MinValue);
				case "System.UInt16": return value.Equals(ushort.MinValue);
				case "System.UInt32": return value.Equals(uint.MinValue);
				case "System.UInt64": return value.Equals(ulong.MinValue);
				case "System.Boolean": return value.Equals(false);
				case "System.Single": return value.Equals(float.MinValue);
				case "System.Double": return value.Equals(double.MinValue);
				case "System.Decimal": return value.Equals(decimal.MinValue);
				case "System.DateTime": return value.Equals(DateTime.MinValue);
				default: return true;
			}
		}
	}
}
