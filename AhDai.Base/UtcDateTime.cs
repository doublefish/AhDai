using System;

namespace AhDai.Base
{
	/// <summary>
	/// UtcDateTime
	/// </summary>
	public class UtcDateTime
	{
		/// <summary>
		/// 表示 UtcDateTime 的最小值。 此字段为只读。
		/// </summary>
		public static readonly DateTime MinValue = new DateTime(1970, 1, 1);

		/// <summary>
		/// 获取时间戳（秒）
		/// </summary>
		/// <returns></returns>
		public static int GetTimestamp()
		{
			var ts = DateTime.UtcNow.Subtract(MinValue).TotalSeconds;
			return Convert.ToInt32(ts);
		}
	}
}
