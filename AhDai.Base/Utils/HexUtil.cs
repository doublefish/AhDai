using System;
using System.Text;

namespace AhDai.Base.Utils
{
	/// <summary>
	/// HexHelper
	/// </summary>
	public static class HexUtil
	{
		/// <summary>
		/// 字符串转16进制字符串
		/// </summary>
		/// <param name="s"></param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string ToHexString(string s, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var bytes = encode.GetBytes(s);
			var result = string.Empty;
			for (var i = 0; i < bytes.Length; i++)
			{
				result += "%" + Convert.ToString(bytes[i], 16);
			}
			return result;
		}

		/// <summary>
		/// 字节数组转16进制字符串
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static string ToHexString(byte[] bytes)
		{
			var result = string.Empty;
			for (var i = 0; i < bytes.Length; i++)
			{
				result += bytes[i].ToString("x2");
			}
			return result;
		}

		/// <summary>
		/// 16进制字符串转字符串
		/// </summary>
		/// <param name="hex"></param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string ToString(string hex, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var chars = hex.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
			var bytes = new byte[chars.Length];
			for (var i = 0; i < chars.Length; i++)
			{
				bytes[i] = Convert.ToByte(chars[i], 16);
			}
			return encode.GetString(bytes);
		}

		/// <summary>
		/// 16进制字符串转字节数组
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		public static byte[] ToBytes(string hex)
		{
			hex = hex.Replace(" ", "");
			if (hex.Length % 2 != 0)
			{
				hex += " ";
			}
			var bytes = new byte[hex.Length / 2];
			for (var i = 0; i < bytes.Length; i++)
			{
				bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
			}
			return bytes;
		}
	}
}
