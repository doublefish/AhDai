using System.Collections.Generic;
using System.Data;

namespace Adai.DbContext
{
	/// <summary>
	/// IDbContext
	/// </summary>
	public interface IDbContext
	{
		/// <summary>
		/// 连接字符串
		/// </summary>
		string ConnectionString { get; set; }

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <returns></returns>
		IDbConnection CreateConnection();

		/// <summary>
		/// CreateDataAdapter
		/// </summary>
		/// <returns></returns>
		IDbDataAdapter CreateDataAdapter();

		/// <summary>
		/// CreateParameter
		/// </summary>
		/// <returns></returns>
		IDbDataParameter CreateParameter();

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <returns></returns>
		IDbCommand CreateCommand();

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		DataSet GetDataSet(string sql, params IDbDataParameter[] parameters);

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		object ExecuteScalar(string sql, params IDbDataParameter[] parameters);

		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		int ExecuteNonQuery(string sql, params IDbDataParameter[] parameters);

		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="commands"></param>
		/// <returns></returns>
		int ExecuteNonQuery(params IDbCommand[] commands);

		/// <summary>
		/// 跨库执行
		/// </summary>
		/// <param name="dict">connStr-cmds</param>
		/// <returns></returns>
		int ExecuteNonQuery(IDictionary<string, ICollection<IDbCommand>> dict);
	}
}
