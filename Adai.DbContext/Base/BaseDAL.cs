using Adai.DbContext.Ext;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Adai.DbContext
{
	/// <summary>
	/// BaseDAL
	/// </summary>
	/// <typeparam name="Model"></typeparam>
	public abstract class BaseDAL<Model>
		where Model : BaseModel
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public BaseDAL() : this(null, null)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dbName">数据库名</param>
		/// <param name="tableName">数据表名</param>
		public BaseDAL(string dbName, string tableName)
		{
			DbContext = InitDbContext();
			Init(dbName, tableName);
			Alias = "t";
			var tableAttr = DbHelper.GetTableAttribute<Model>();
			if (string.IsNullOrEmpty(tableName))
			{
				TableName = tableAttr.Name;
			}
			var primaryAttr = tableAttr.ColumnAttributes.Where(o => o.Type == Attribute.ColumnType.Primary).FirstOrDefault();
			if (primaryAttr != null)
			{
				PrimaryKey = primaryAttr.Name;
			}
		}
		string selectSql;

		/// <summary>
		/// 数据库名
		/// </summary>
		public string DbName { get; protected set; }
		/// <summary>
		/// 数据表名
		/// </summary>
		public string TableName { get; protected set; }
		/// <summary>
		/// 主键名称
		/// </summary>
		public string PrimaryKey { get; private set; }

		/// <summary>
		/// DbContext
		/// </summary>
		protected IDbContext DbContext { get; private set; }

		/// <summary>
		/// 查询语句
		/// </summary>
		protected string SelectSql
		{
			get
			{
				if (string.IsNullOrEmpty(selectSql))
				{
					selectSql = InitSelectSql();
				}
				return selectSql;
			}
		}

		/// <summary>
		/// 别名
		/// </summary>
		protected string Alias { get; set; }

		/// <summary>
		/// InitDbContext
		/// </summary>
		/// <returns></returns>
		protected abstract IDbContext InitDbContext();

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="tableName"></param>
		public virtual void Init(string dbName, string tableName)
		{
			if (!string.IsNullOrEmpty(dbName))
			{
				DbName = dbName;
			}
			if (!string.IsNullOrEmpty(tableName))
			{
				TableName = tableName;
			}
		}

		/// <summary>
		/// 初始化查询语句
		/// </summary>
		protected virtual string InitSelectSql()
		{
			return string.Format("SELECT {0}.* FROM {1} {0} WHERE 1=1", Alias, TableName);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual Model GetById(string id)
		{
			return GetByColumn(PrimaryKey, id);
		}

		/// <summary>
		/// 根据Id查询
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public virtual ICollection<Model> ListByIds(params string[] ids)
		{
			return ListByColumn(PrimaryKey, ids);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="filter"></param>
		/// <returns></returns>
		public virtual ICollection<Model> List<T>(T filter) where T : IFilter<Model>
		{
			var sql = SelectSql;
			var paras = new List<IDbDataParameter>();
			sql += CreateExtendQueryCondition(filter, out var paras1);
			if (paras1 != null)
			{
				paras.AddRange(paras1);
			}
			sql += DbContext.CreateQueryCondition(filter, Alias, out var paras0);
			if (paras0 != null)
			{
				paras.AddRange(paras0);
			}
			return GetList(sql, paras.ToArray());
		}

		/// <summary>
		/// 创建扩展查询条件扩展
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="filter"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		protected virtual string CreateExtendQueryCondition<T>(T filter, out IDbDataParameter[] parameters) where T : IFilter<Model>
		{
			parameters = null;
			return null;
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public virtual int Add(Model data)
		{
			var cmd = DbContext.CreateInsertCommand(data, TableName);
			return DbContext.ExecuteNonQuery(cmd);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="data"></param>
		/// <param name="updateColumns"></param>
		/// <param name="whereColumns">可为空，读取实体特性主键</param>
		/// <returns></returns>
		public virtual int Update(Model data, string[] updateColumns, params string[] whereColumns)
		{
			if (whereColumns == null || whereColumns.Length == 0)
			{
				whereColumns = new string[] { PrimaryKey };
			}
			var cmd = DbContext.CreateUpdateCommand(data, updateColumns, whereColumns, TableName);
			return DbContext.ExecuteNonQuery(cmd);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="column"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public virtual Model GetByColumn<T>(string column, T value)
		{
			var sql = SelectSql;
			sql += string.Format(" AND {0}.{1}=@{1} LIMIT 0,1", Alias, column);
			var para = DbContext.CreateParameter(column, value);
			return GetList(sql, para).FirstOrDefault();
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="column"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public virtual ICollection<Model> ListByColumn<T>(string column, T value)
		{
			var sql = SelectSql;
			sql += string.Format(" AND {0}.{1}=@{1}", Alias, column);
			var para = DbContext.CreateParameter(column, value);
			return GetList(sql, para);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="column"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public virtual ICollection<Model> ListByColumn<T>(string column, T[] values)
		{
			var sql = SelectSql;
			var sql_in = DbHelper.CreateInSql(string.Format("{0}.{1}", Alias, column), values);
			sql += string.Format(" AND {0}", sql_in);
			return GetList(sql);
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbName"></param>
		/// <param name="tableNames"></param>
		/// <param name="column"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public ICollection<Model> ListByColumn<T>(string dbName, string[] tableNames, string column, T value)
		{
			var sqls = new StringBuilder();
			foreach (var tableName in tableNames)
			{
				sqls.Append(string.Format("SELECT * FROM {0} WHERE {1}=@{1};\r\n", tableName, column));
			}
			var sql = sqls.ToString();
			var para = DbContext.CreateParameter(column, value);
			var ds = DbContext.GetDataSet(dbName, sql, para);
			var tableAttr = DbHelper.GetTableAttribute<Model>();
			var columns = tableAttr.ColumnAttributes;
			var list = new List<Model>();
			for (var i = 0; i < ds.Tables.Count; i++)
			{
				var _list = ds.Tables[i].ToList<Model>(columns);
				foreach (var data in _list)
				{
					data.DbName = dbName;
					data.TableName = tableNames[i];
					list.Add(data);
				}
			}
			return list;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="dict">dbName-tableName-values</param>
		/// <param name="column"></param>
		/// <returns></returns>
		protected ICollection<Model> ListByColumn<T>(IDictionary<string, IDictionary<string, ICollection<T>>> dict, string column)
		{
			var tableAttr = DbHelper.GetTableAttribute<Model>();
			var columns = tableAttr.ColumnAttributes;
			var list = new List<Model>();
			foreach (var kv in dict)
			{
				var dbName = kv.Key;
				var sqls = new StringBuilder();
				foreach (var _kv in kv.Value)
				{
					var tableName = _kv.Key;
					var values = _kv.Value;
					var sql_in = DbHelper.CreateInSql(column, values.ToArray());
					sqls.Append(string.Format("SELECT * FROM {0} WHERE {1};\r\n", tableName, sql_in));
				}
				var sql = sqls.ToString();
				var ds = DbContext.GetDataSet(dbName, sql);
				for (var i = 0; i < ds.Tables.Count; i++)
				{
					var _list = ds.Tables[i].ToList<Model>(columns);
					foreach (var data in _list)
					{
						data.DbName = dbName;
						data.TableName = kv.Value.ElementAt(i).Key;
						list.Add(data);
					}
				}
			}
			return list;
		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		ICollection<Model> GetList(string sql, params IDbDataParameter[] parameters)
		{
			var list = DbContext.GetList<Model>(DbName, sql, parameters);
			foreach (var data in list)
			{
				data.DbName = DbName;
				data.TableName = TableName;
			}
			return list;
		}
	}
}
