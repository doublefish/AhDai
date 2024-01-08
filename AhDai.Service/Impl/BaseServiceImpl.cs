using AhDai.Core.Models;
using AhDai.Db;
using AhDai.Db.Models;
using AhDai.Service.Converters;
using AhDai.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
/// <typeparam name="PK"></typeparam>
/// <typeparam name="Model"></typeparam>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
internal abstract class BaseServiceImpl<PK, Model, Input, Output, QueryInput> : BaseServiceImpl, IBaseService<PK, Input, Output, QueryInput>
	where Model : BaseModel<PK>
	where Input : BaseInput
	where Output : BaseOutput<PK>
	where QueryInput : BaseQueryInput
{
	/// <summary>
	/// 新增
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public virtual async Task AddAsync(Input input)
	{
		var model = ToModel(input);
		using var db = new DefaultDbContext();
		await db.Set<Model>().AddAsync(model);
		await AddAsync(db, model, input);
	}

	/// <summary>
	/// 新增
	/// </summary>
	/// <param name="db"></param>
	/// <param name="model"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	protected virtual async Task AddAsync(DefaultDbContext db, Model model, Input input)
	{
		await db.SaveChangesAsync();
	}

	/// <summary>
	/// 修改
	/// </summary>
	/// <param name="id"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public virtual async Task UpdateAsync(PK id, Input input)
	{
		using var db = new DefaultDbContext();
		var model = await db.Set<Model>().Where(o => o.Id.Equals(id) && o.RowDeleted == false).SingleOrDefaultAsync() ?? throw new ArgumentException("指定Id的记录不存在", nameof(id));
		await UpdateAsync(db, model, input);
	}

	/// <summary>
	/// 修改
	/// </summary>
	/// <param name="db"></param>
	/// <param name="model"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	protected virtual async Task UpdateAsync(DefaultDbContext db, Model model, Input input)
	{
		await db.SaveChangesAsync();
	}


	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public virtual async Task<Output> GetByIdAsync(PK id)
	{
		using var db = new DefaultDbContext();
		var query = db.Set<Model>().Where(o => o.Id.Equals(id) && o.RowDeleted == false);
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
		var models = await query.ToArrayAsync();
		var outputs = ToOutputs(models);
		await SetAssociateDataAsync(outputs);
		return outputs;
	}

	/// <summary>
	/// 查询
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public virtual async Task<ICollection<Output>> GetAsync(QueryInput input)
	{
		using var db = new DefaultDbContext();
		var query = db.Set<Model>().Where(o => o.RowDeleted == false);
		query = GenerateQuery(db, query, input);
		var models = await query.Take(input.PageSize ?? 1000).ToListAsync();
		var outputs = ToOutputs(models);
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
			var models = await query.ToListAsync();
			data.List = ToOutputs(models);
			await SetAssociateDataAsync(data.List);
		}
		return data;
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
	/// 转为实体对象
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	protected abstract Model ToModel(Input input);

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
/// <typeparam name="Input"></typeparam>
/// <typeparam name="Output"></typeparam>
/// <typeparam name="QueryInput"></typeparam>
internal abstract class BaseServiceImpl<Model, Input, Output, QueryInput> : BaseServiceImpl<long, Model, Input, Output, QueryInput>, IBaseService<Input, Output, QueryInput>
	where Model : BaseModel
	where Input : BaseInput
	where Output : BaseOutput
	where QueryInput : BaseQueryInput
{

}
