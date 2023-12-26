using AhDai.Db;
using AhDai.Db.Models;
using AhDai.Service.Converters;
using AhDai.Service.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// InterfaceServiceImpl
/// </summary>
internal class InterfaceServiceImpl : BaseServiceImpl<Interface, InterfaceInput, InterfaceOutput, InterfaceQueryInput>, IInterfaceService
{
	protected override async Task UpdateAsync(DefaultDbContext db, Interface model, InterfaceInput input)
	{
		model.Name = input.Name;
		model.Method = input.Method;
		model.Url = input.Url;
		model.Remark = input.Remark;
		model.Status = input.Status;
		await db.SaveChangesAsync();
	}

	protected override IQueryable<Interface> GenerateQuery(DefaultDbContext db, IQueryable<Interface> query, InterfaceQueryInput input)
	{
		if (!string.IsNullOrEmpty(input.Name))
		{
			query = query.Where(o => o.Name.Contains(input.Name));
		}
		if (!string.IsNullOrEmpty(input.Method))
		{
			query = query.Where(o => o.Method == input.Method);
		}
		if (!string.IsNullOrEmpty(input.Url))
		{
			query = query.Where(o => o.Url.Contains(input.Url));
		}
		if (input.Status.HasValue)
		{
			query = query.Where(o => o.Status == input.Status.Value);
		}
		return query;
	}

	protected override Interface ToModel(InterfaceInput input)
	{
		return input.ToModel();
	}

	protected override InterfaceOutput ToOutput(Interface model)
	{
		return model.ToOutput();
	}
}
