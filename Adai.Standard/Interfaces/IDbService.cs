using Adai.DbContext;

namespace Adai.Standard.Interfaces
{
	/// <summary>
	/// 数据库服务
	/// </summary>
	public interface IDbService
	{
		/// <summary>
		/// GetDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public IDbContext GetDbContext(string eventId, string dbName = null);

		/// <summary>
		/// GetSQLiteDbContext
		/// </summary>
		/// <param name="eventId"></param>
		/// <param name="fileName"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public IDbContext GetSQLiteDbContext(string eventId, string fileName = null, string version = "3");
	}
}
