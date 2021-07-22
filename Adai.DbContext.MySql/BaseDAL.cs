using System;

namespace Adai.DbContext.MySql
{
	/// <summary>
	/// BaseDAL
	/// </summary>
	/// <typeparam name="Model"></typeparam>
	public class BaseDAL<Model> : Adai.DbContext.BaseDAL<Model>
		where Model : BaseModel, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public BaseDAL(string dbName, string tableName) : base(dbName, tableName)
		{
			// 初始化数据库连接字符串
			DbHelper.Init(null);
		}

		/// <summary>
		/// InitDbContext
		/// </summary>
		/// <returns></returns>
		protected override IDbContext InitDbContext()
		{
			return new MySqlDbContext();
		}
	}
}
