using System.Text;

namespace AhDai.Service.Impl.Utils;

internal static class StringUtil
{
	const string base36Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

	/// <summary>
	/// 转为36进制字符串
	/// </summary>
	/// <param name="number"></param>
	/// <returns></returns>
	public static string ConvertToBase36(long number)
	{
		var builder = new StringBuilder();
		do
		{
			var remainder = (int)number % 36;
			builder.Insert(0, base36Chars[remainder]);
			number /= 36;
		}
		while (number > 0);
		return builder.ToString();
	}

}
