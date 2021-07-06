using MySql.Data.MySqlClient;
using System.Data;

namespace Adai.DbContext.MySql
{
	/// <summary>
	/// MySqlDbContext
	/// </summary>
	public sealed class MySqlDbContext : DbContext
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public MySqlDbContext() : this(null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="connectionString"></param>
		public MySqlDbContext(string connectionString) : base(DbType.MySQL, connectionString)
		{
		}

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <returns></returns>
		public override IDbConnection CreateConnection()
		{
			return new MySqlConnection();
		}

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <returns></returns>
		public override IDbCommand CreateCommand()
		{
			return new MySqlCommand();
		}

		/// <summary>
		/// CreateDataAdapter
		/// </summary>
		/// <returns></returns>
		public override IDbDataAdapter CreateDataAdapter()
		{
			return new MySqlDataAdapter();
		}

		/// <summary>
		/// CreateParameter
		/// </summary>
		/// <returns></returns>
		public override IDbDataParameter CreateParameter()
		{
			return new MySqlParameter();
		}
	}
}
