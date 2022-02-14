using System;

namespace AhDai.Base.Utils
{
	/// <summary>
	/// StringHelper
	/// </summary>
	public static class StringHelper
	{
		const string Bound = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz2345678";
		const string BoundOfDigital = "0123456789";
		static readonly Random Random = new Random();

		/// <summary>
		/// 生成指定长度的随机数字字符串
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string GenerateRandomDigital(int length)
		{
			return GenerateRandom(BoundOfDigital, length);
		}

		/// <summary>
		/// 生成指定长度的随机字符串
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string GenerateRandom(int length)
		{
			return GenerateRandom(Bound, length);
		}

		/// <summary>
		/// 生成验指定长度的随机字符串
		/// </summary>
		/// <param name="bound"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string GenerateRandom(string bound, int length)
		{
			var code = string.Empty;
			for (var i = 0; i < length; i++)
			{
				code += bound[Random.Next(bound.Length - 1)];
			}
			return code;
		}
	}
}
