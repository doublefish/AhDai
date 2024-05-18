using AhDai.Db;
using AhDai.Entity.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// DictTestServiceImpl
/// </summary>
[Attributes.Service(ServiceLifetime.Singleton)]
internal class DictTestServiceImpl(ILogger<DictTestServiceImpl> logger)
	: BaseServiceImpl(logger)
	, IDictTestService
{
	public async Task<object> AddAsync()
	{
		try
		{
            //

            var dict = new HashSet<string>()
            {
                "工作累", "汇报类", "其他",
            };
            var res = await AddDictAsync(MyConst.Dict.WORK_TASK_TYPE, "工作任务类型", dict);

            return string.Join("\r\n", res);
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	protected async Task WashDataAsync()
	{
		var fields = typeof(MyConst.Dict).GetFields();
		var rootCodes = new HashSet<string>();
		foreach (var field in fields)
		{
			if (!field.IsLiteral) continue;
			var rootCode = field.GetValue(null);
			rootCodes.Add(rootCode?.ToString() ?? "");
		}

		using var db = await MyApp.GetDefaultDbAsync();
		var sql = "SELECT * FROM [Sys_Dict2]";
		var temps = await db.Database.SqlQueryRaw<Dict>(sql).ToArrayAsync();

		var all = Mapper.Map<DictOutput[]>(temps);
		var tree = all.ToTreeArray();

		foreach (var d1 in tree)
		{
			if (!rootCodes.Contains(d1.Code) || d1.Children == null || d1.Children.Length == 0)
			{
				Console.WriteLine("跳过：{0}", d1.Code);
				continue;
			}
			await AddAsync(db, d1, 0);
		}

		await db.SaveChangesAsync();

	}

	private async Task AddAsync(DefaultDbContext db, DictOutput output, long parentId)
	{
		var entity = new Dict()
		{
			Code = output.Code,
			Name = output.Name,
			ParentId = parentId,
			Value = output.Value,
		};
		await db.Dicts.AddAsync(entity);
		if (output.Children != null && output.Children.Length > 0)
		{
			foreach (var child in output.Children)
			{
				await AddAsync(db, child, entity.Id);
			}
		}
	}

	protected async Task<string> AddDictAsync(string rootCode, string rootName, IEnumerable<string> children)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var isExists = await db.Dicts.AnyAsync(x => x.Code == rootCode && x.IsDeleted == false);
		if (isExists)
		{
			throw new Exception("根节点编码已存在");
		}
		var root = new Dict()
		{
			Code = rootCode,
			Name = rootName,
			Value = ""
		};
		await db.Dicts.AddAsync(root);
		foreach (var child in children)
		{
			await db.Dicts.AddAsync(new Dict()
			{
				Code = "",
				Name = child,
				ParentId = root.Id,
				Value = child,
			});
		}
		var res = await db.SaveChangesAsync();
		return $"受影响行数：{res}";

	}

	protected async Task<string> AddDictAsync(string rootCode, string rootName, IDictionary<string, string> children)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var isExists = await db.Dicts.AnyAsync(x => x.Code == rootCode && x.IsDeleted == false);
		if (isExists)
		{
			throw new Exception("根节点编码已存在");
		}
		var root = new Dict()
		{
			Code = rootCode,
			Name = rootName,
			Value = ""
		};
		await db.Dicts.AddAsync(root);
		foreach (var child in children)
		{
			await db.Dicts.AddAsync(new Dict()
			{
				Code = "",
				Name = child.Key,
				ParentId = root.Id,
				Value = string.IsNullOrEmpty(child.Value) ? child.Key : child.Value,
			});
		}
		var res = await db.SaveChangesAsync();
		return $"受影响行数：{res}";

	}

	protected async Task<string> AddDictAsync(string rootCode, string rootName, IDictionary<string, IDictionary<string, string>> children)
	{
		using var db = await MyApp.GetDefaultDbAsync();
		var isExists = await db.Dicts.AnyAsync(x => x.Code == rootCode && x.IsDeleted == false);
		if (isExists)
		{
			throw new Exception("根节点编码已存在");
		}
		var root = new Dict()
		{
			Code = rootCode,
			Name = rootName,
			Value = ""
		};
		await db.Dicts.AddAsync(root);
		foreach (var child in children)
		{
			var data = new Dict()
			{
				Code = "",
				Name = child.Key,
				ParentId = root.Id,
				Value = ""
			};
			await db.Dicts.AddAsync(data);
			foreach (var child2 in child.Value)
			{
				await db.Dicts.AddAsync(new Dict()
				{
					Code = "",
					Name = child2.Key,
					ParentId = data.Id,
					//Value = string.IsNullOrEmpty(child2.Value) ? child2.Key : child2.Value,
					Value = child2.Key,
				});
			}
		}
		var res = await db.SaveChangesAsync();
		return $"受影响行数：{res}";

	}
}
