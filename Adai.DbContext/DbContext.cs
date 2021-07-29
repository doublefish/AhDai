using System;
using System.Collections.Generic;
using System.Data;

namespace Adai.DbContext
{
	/// <summary>
	/// DbContext
	/// </summary>
	public abstract class DbContext : IDbContext
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dbType"></param>
		public DbContext(DbType dbType) : this(dbType, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dbType"></param>
		/// <param name="connectionString"></param>
		public DbContext(DbType dbType, string connectionString)
		{
			DbType = dbType;
			ConnectionString = connectionString;
		}

		/// <summary>
		/// 数据库类型
		/// </summary>
		public DbType DbType { get; private set; }

		/// <summary>
		/// 数据库连接
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <returns></returns>
		public abstract IDbConnection CreateConnection();

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns></returns>
		public virtual IDbConnection CreateConnection(string connectionString)
		{
			var conn = CreateConnection();
			conn.ConnectionString = connectionString;
			return conn;
		}

		/// <summary>
		/// CreateDataAdapter
		/// </summary>
		/// <returns></returns>
		public abstract IDbDataAdapter CreateDataAdapter();

		/// <summary>
		/// CreateParameter
		/// </summary>
		/// <returns></returns>
		public abstract IDbDataParameter CreateParameter();

		/// <summary>
		/// CreateParameter
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public virtual IDbDataParameter CreateParameter(string name, object value)
		{
			var para = CreateParameter();
			para.ParameterName = name;
			para.Value = value;
			return para;
		}

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <returns></returns>
		public abstract IDbCommand CreateCommand();

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual IDbCommand CreateCommand(string sql, params IDbDataParameter[] parameters)
		{
			var cmd = CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddRange(parameters);
			return cmd;
		}

		/// <summary>
		/// CreateCommand
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual IDbCommand CreateCommand(IDbConnection connection, string sql, params IDbDataParameter[] parameters)
		{
			var cmd = connection.CreateCommand();
			cmd.CommandText = sql;
			cmd.Parameters.AddRange(parameters);
			return cmd;
		}

		/// <summary>
		/// CreateConnection
		/// </summary>
		/// <returns></returns>
		public IDbConnection GetConnection()
		{
			var conn = CreateConnection();
			conn.ConnectionString = ConnectionString;
			return conn;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual DataSet GetDataSet(string sql, params IDbDataParameter[] parameters)
		{
			var ds = new DataSet();
			using (var conn = GetConnection())
			{
				conn.Open();
				var adapter = CreateDataAdapter();
				adapter.SelectCommand = CreateCommand(conn, sql, parameters);
				BeforeExecute(adapter.SelectCommand);
				adapter.Fill(ds);
			}
			return ds;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public object ExecuteScalar(string sql, params IDbDataParameter[] parameters)
		{
			using (var conn = GetConnection())
			{
				conn.Open();
				var cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				cmd.Parameters.AddRange(parameters);
				BeforeExecute(cmd);
				return cmd.ExecuteScalar();
			}
		}

		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual int ExecuteNonQuery(string sql, params IDbDataParameter[] parameters)
		{
			var cmd = CreateCommand(sql, parameters);
			return ExecuteNonQuery(cmd);
		}

		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="commands"></param>
		/// <returns></returns>
		public virtual int ExecuteNonQuery(params IDbCommand[] commands)
		{
			var result = 0;
			var conn = GetConnection();
			IDbTransaction tran = null;
			var hasCommitted = false;
			try
			{
				conn.Open();
				tran = conn.BeginTransaction();
				foreach (var cmd in commands)
				{
					cmd.Connection = conn;
					cmd.Transaction = tran;
					BeforeExecute(cmd);
					result += cmd.ExecuteNonQuery();
				}
				hasCommitted = true;
				tran.Commit();
			}
			catch (Exception ex)
			{
				if (hasCommitted)
				{
					tran.Rollback();
				}
				throw ex;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
			return result;
		}

		/// <summary>
		/// 跨库执行
		/// </summary>
		/// <param name="dict">connStr-cmds</param>
		/// <returns></returns>
		public virtual int ExecuteNonQuery(IDictionary<string, ICollection<IDbCommand>> dict)
		{
			var result = 0;
			var conns = new List<IDbConnection>();
			//var trans = new List<IDbTransaction>();
			var trans = new Dictionary<string, IDbTransaction>();
			var hasCommitteds = new Dictionary<string, bool>();
			try
			{
				foreach (var kv in dict)
				{
					var conn = CreateConnection();
					conn.ConnectionString = kv.Key;
					var cmds = kv.Value;
					conn.Open();
					conns.Add(conn);
					var tran = conn.BeginTransaction();
					//trans.Add(tran);
					trans[kv.Key] = tran;
					hasCommitteds[kv.Key] = false;
					foreach (var cmd in cmds)
					{
						cmd.Connection = conn;
						cmd.Transaction = tran;
						BeforeExecute(cmd);
						result += cmd.ExecuteNonQuery();
					}
					hasCommitteds[kv.Key] = true;
					tran.Commit();
				}
			}
			catch (Exception ex)
			{
				foreach (var kv in dict)
				{
					if (!trans.TryGetValue(kv.Key, out var tran))
					{
						break;
					}
					var hasCommitted = hasCommitteds[kv.Key];
					if (hasCommitted)
					{
						tran.Rollback();
					}
				}
				throw ex;
			}
			finally
			{
				foreach (var conn in conns)
				{
					if (conn.State == ConnectionState.Open)
					{
						conn.Close();
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 执行之前
		/// </summary>
		/// <param name="command"></param>
		protected abstract void BeforeExecute(IDbCommand command);
	}
}
