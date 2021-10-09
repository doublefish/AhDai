using System;

namespace Adai.Utilities
{
	/// <summary>
	/// DateTimeHelper
	/// </summary>
	public class DateTimeHelper
	{
		/// <summary>
		/// 时间戳
		/// </summary>
		public static TimeSpan Timestamp => DateTime.UtcNow.Subtract(UtcDateTime.MinValue);
		/// <summary>
		/// 时间戳（距1970-01-01的毫秒数）
		/// </summary>
		public static double TimestampOfMilliseconds => Timestamp.TotalMilliseconds;
		/// <summary>
		/// 时间戳（距1970-01-01的秒数）
		/// </summary>
		public static double TimestampOfSeconds => Timestamp.TotalSeconds;
	}
}
