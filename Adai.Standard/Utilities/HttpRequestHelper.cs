using Adai.Extensions;
using System;

namespace Adai.Standard.Utilities
{
	/// <summary>
	/// HttpRequestHelper
	/// </summary>
	public static class HttpRequestHelper
	{
		static readonly string CacheKey = "RequestLimit";

		/// <summary>
		/// 验证请求频率限制
		/// </summary>
		/// <param name="ipAddress">IP地址</param>
		/// <param name="path">请求路径</param>
		/// <param name="limit">每秒限制次数</param>
		/// <returns></returns>
		public static void VerifyFrequencyLimit(string ipAddress, string path, double limit = 1D)
		{
			path = path.ToLower();
			var dateTime = DateTime.Now;
			var time = dateTime.TimeOfDay;
			var redis = RedisHelper.GetDatabase(15);
			var key = $"{CacheKey}-{dateTime:yyMMddHH}";
			var hashField = $"{ipAddress}-{path}";
			if (redis.KeyExists(key))
			{
				var value = redis.HashGet(key, hashField);
				if (value.IsNullOrEmpty)
				{
					value = time.ToString();
				}
				else
				{
					var array = value.ToString().Split(',');
					if (limit > 1D)
					{
						//每秒可访问次数大于1，则计算当前时间据上{limit}次访问时间的差是否小于1秒
						var i = Convert.ToInt32(limit);
						if (array.Length >= i)
						{
							//上{limit}次访问时间
							var lastTime = array[i - 1].ToTimeSpan();
							//距上{limit}次访问时间差
							var ts = time.Subtract(lastTime).TotalSeconds;
							if (ts < 1D)
							{
								throw new Exception("Requests are too frequent." + ts);
							}
						}
					}
					else
					{
						//每秒可访问次数小于等于1，等价于两次访问时间间隔小于{limit}秒，则计算当前时间据上次访问时间的差是否小于{limit}即可
						//上次访问时间
						var lastTime = array[0].ToTimeSpan();
						//距上次访问时间差
						var ts = time.Subtract(lastTime).TotalSeconds;
						if (ts < limit)
						{
							throw new Exception("Requests are too frequent." + ts);
						}
					}
					if (array.Length > 9)
					{
						//大于10次，删除旧数据
						value = string.Join(',', array, 0, 9);
					}
					//记录本次访问时间
					value = $"{time},{value}";
				}
				redis.HashSet(key, hashField, value);
			}
			else
			{
				//初始化并设置过期时间
				redis.HashSet(key, hashField, time.ToString());
				redis.KeyExpire(key, new TimeSpan(1, 1, 0));
			}
		}
	}
}
