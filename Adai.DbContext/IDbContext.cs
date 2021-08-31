﻿using System.Data;

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
		/// CreateParameter
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		IDbDataParameter CreateParameter(string name, object value);

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <returns></returns>
		IDbCommand CreateCommand();

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		IDbCommand CreateCommand(string sql, params IDbDataParameter[] parameters);

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		IDbCommand CreateCommand(IDbConnection connection, string sql, params IDbDataParameter[] parameters);

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
		/// <param name="command"></param>
		/// <returns></returns>
		int ExecuteNonQuery(IDbCommand command);

		/// <summary>
		/// 执行之前
		/// </summary>
		/// <param name="command"></param>
		void BeforeExecute(IDbCommand command);
	}
}
