using AhDai.Db;
using AhDai.Entity.Sys;
using AhDai.Service.Sys;
using AhDai.Service.Sys.Models;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl.Sys;

/// <summary>
/// InterfaceServiceImpl
/// </summary>
internal class InterfaceServiceImpl(ILogger<InterfaceServiceImpl> logger)
	: BaseServiceImpl<Interface, InterfaceInput, InterfaceOutput, InterfaceQueryInput>(logger)
	, IInterfaceService
{
	protected override Task BeforeSaveAsync(DefaultDbContext db, Interface entity, InterfaceInput input, bool isUpdate)
	{
		if (isUpdate)
		{
			entity.Name = input.Name;
			entity.Method = input.Method;
			entity.Url = input.Url;
			entity.Remark = input.Remark;
			entity.Status = input.Status;
		}
		return Task.CompletedTask;
	}

	protected override Task<IQueryable<Interface>> GenerateQueryAsync(DefaultDbContext db, IQueryable<Interface> query, InterfaceQueryInput input)
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
		return base.GenerateQueryAsync(db, query, input);
	}

}
