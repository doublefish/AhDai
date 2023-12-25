using AhDai.Core.Models;
using AhDai.Core.Utils;
using AhDai.Db;
using AhDai.Db.Models;
using AhDai.Service.Models;
using AhDai.Service.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// BaseServiceImpl
/// </summary>
internal class BaseServiceImpl : IBaseService
{
}

/// <summary>
/// BaseServiceImpl
/// </summary>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
internal abstract class BaseServiceImpl<Output, QueryInput> : BaseServiceImpl, IBaseService<Output, QueryInput>
	where Output : class, new()
	where QueryInput : BaseQueryInput
{


	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public virtual async Task<ICollection<Output>> GetAsync(QueryInput input)
	{
		var sql = GenerateQuerySql(input, out var paras);
		var selectSql = SqlUtils.GenerateTakeSql(sql + GenerateOrderSql(input), input);
		using var db = new DefaultDbContext();
		//db.Database.SqlQuery<DepartmentOutput>($"");
		var outputs = DbContextUtil.ExecuteReader(db.Database.GetDbConnection(), selectSql, paras, ToOutput);
		await SetAssociateDataAsync(outputs);
		return outputs;
	}

	/// <summary>
	/// 分页查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public virtual async Task<PageData<Output>> PageAsync(QueryInput input)
	{
		var sql = GenerateQuerySql(input, out var paras);
		var countSql = $"SELECT COUNT(*) FROM ({sql}) t";
		using var db = new DefaultDbContext();
		var count = DbContextUtil.ExecuteScalar(db.Database.GetDbConnection(), countSql, paras, (obj) =>
		{
			return obj != null && obj != DBNull.Value ? Convert.ToInt32(obj) : 0;
		});
		var data = new PageData<Output>(count);
		if (count > 0)
		{
			var selectSql = SqlUtils.GeneratePageSql(sql + GenerateOrderSql(input), input);
			data.List = DbContextUtil.ExecuteReader(db.Database.GetDbConnection(), selectSql, paras, ToOutput);
			await SetAssociateDataAsync(data.List);
		}
		return data;
	}

	/// <summary>
	/// 设置关联数据
	/// </summary>
	/// <param name="output"></param>
	protected virtual async Task SetAssociateDataAsync(Output output)
	{
		if (output == null)
		{
			return;
		}
		await SetAssociateDataAsync(new Output[] { output });
	}

	/// <summary>
	/// 设置关联数据
	/// </summary>
	/// <param name="outputs"></param>
	protected virtual Task SetAssociateDataAsync(ICollection<Output> outputs)
	{
		return Task.CompletedTask;
	}

	/// <summary>
	/// 生成查询语句
	/// </summary>
	/// <param name="input"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	protected abstract string GenerateQuerySql(QueryInput input, out IDictionary<string, object> parameters);

	/// <summary>
	/// 生成排序语句
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	protected abstract string GenerateOrderSql(QueryInput input);

	/// <summary>
	/// 转为输出对象
	/// </summary>
	/// <param name="reader"></param>
	/// <returns></returns>
	protected abstract Output ToOutput(IDataReader reader);

}

/// <summary>
/// BaseServiceImpl
/// </summary>
/// <typeparam name="PK"></typeparam>
/// <typeparam name="Model"></typeparam>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
internal abstract class BaseServiceImpl<PK, Model, Output, QueryInput> : BaseServiceImpl<Output, QueryInput>, IBaseService<PK, Output, QueryInput>
	where Model : BaseModel<PK>
	where Output : class, new()
	where QueryInput : BaseQueryInput
{
	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public virtual async Task<Output> GetByIdAsync(PK id)
	{
		using var db = new DefaultDbContext();
		var query = db.Set<Model>().Where(o => o.Id.Equals(id) && o.RowDeleted == false);
		//query = query.Where(o => o.RowCreateUsername == TokenData.Username || db.ProblemMembers.Any(m => m.ProblemId == o.Id && m.Username == TokenData.Username));
		// 子查询时 FirstOrDefault 生成了两个 where，可能是 Oracle.EFCore 类库的问题
		var model = await query.FirstOrDefaultAsync();
		var output = ToOutput(model);
		await SetAssociateDataAsync(output);
		return output;
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="ids"></param>
	/// <returns></returns>
	public virtual async Task<ICollection<Output>> GetByIdsAsync(PK[] ids)
	{
		using var db = new DefaultDbContext();
		var query = db.Set<Model>().Where(o => ids.Contains(o.Id) && o.RowDeleted == false);
		var model = await query.ToArrayAsync();
		var output = ToOutputs(model);
		await SetAssociateDataAsync(output);
		return output;
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public override async Task<ICollection<Output>> GetAsync(QueryInput input)
	{
		using var db = new DefaultDbContext();
		var query = db.Set<Model>().Where(o => o.RowDeleted == false);
		query = GenerateQuery(db, query, input);
		var list = await query.Take(input.PageSize ?? 1000).ToListAsync();
		var outputs = ToOutputs(list);
		await SetAssociateDataAsync(outputs);
		return outputs;
	}

	/// <summary>
	/// 分页查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public override async Task<PageData<Output>> PageAsync(QueryInput input)
	{
		using var db = new DefaultDbContext();
		var query = db.Set<Model>().Where(o => o.RowDeleted == false);
		query = GenerateQuery(db, query, input);
		var count = await query.CountAsync();
		var data = new PageData<Output>(count);
		if (count > 0)
		{
			var pageNumber = input.PageNumber ?? 1;
			var pageSize = input.PageSize ?? 10;
			query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			var list = await query.ToListAsync();
			data.List = ToOutputs(list);
			await SetAssociateDataAsync(data.List);
		}
		return data;
	}

	/// <summary>
	/// 生成查询语句
	/// </summary>
	/// <param name="input"></param>
	/// <param name="parameters"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	protected override string GenerateQuerySql(QueryInput input, out IDictionary<string, object> parameters)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// 生成排序语句
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	protected override string GenerateOrderSql(QueryInput input)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// 生成查询条件
	/// </summary>
	/// <param name="db"></param>
	/// <param name="query"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	protected abstract IQueryable<Model> GenerateQuery(DefaultDbContext db, IQueryable<Model> query, QueryInput input);

	/// <summary>
	/// 转为输出对象
	/// </summary>
	/// <param name="reader"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	protected override Output ToOutput(IDataReader reader)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// 转为输出对象
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	protected abstract Output ToOutput(Model model);

	/// <summary>
	/// 转为输出对象
	/// </summary>
	/// <param name="models"></param>
	/// <returns></returns>
	protected ICollection<Output> ToOutputs(ICollection<Model> models)
	{
		var outputs = new List<Output>();
		foreach (var model in models)
		{
			outputs.Add(ToOutput(model));
		}
		return outputs;
	}

}
/// <summary>
/// BaseServiceImpl
/// </summary>
/// <typeparam name="Model"></typeparam>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
internal abstract class BaseServiceImpl<Model, Output, QueryInput> : BaseServiceImpl<int, Model, Output, QueryInput>, IBaseService<int, Output, QueryInput>
	where Model : BaseModel
	where Output : class, new()
	where QueryInput : BaseQueryInput
{

}
