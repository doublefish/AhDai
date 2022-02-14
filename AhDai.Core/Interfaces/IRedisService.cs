using StackExchange.Redis;

namespace AhDai.Core.Interfaces
{
	/// <summary>
	/// Redis服务
	/// </summary>
	public interface IRedisService
	{
		/// <summary>
		/// GetDatabase
		/// </summary>
		/// <param name="db"></param>
		/// <param name="asyncState"></param>
		/// <returns></returns>
		public IDatabase GetDatabase(int db = -1, object asyncState = null);
	}
}
