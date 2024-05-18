using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Db.Interceptors;

/// <summary>
/// MySaveChangesInterceptor
/// </summary>
public class MySaveChangesInterceptor : SaveChangesInterceptor
{
	readonly Func<OperatingUser>? GetOperatorHandler;
	readonly Func<Task<OperatingUser>>? GetOperatorHandlerAsync;

	async Task<OperatingUser?> GetOperatorAsync()
	{
		if (GetOperatorHandler != null)
		{
			return GetOperatorHandler.Invoke();
		}
		if (GetOperatorHandlerAsync != null)
		{
			return await GetOperatorHandlerAsync.Invoke();
		}
		return null;
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	public MySaveChangesInterceptor()
	{

	}

	public MySaveChangesInterceptor(Func<OperatingUser> handler)
	{
		GetOperatorHandler = handler;
	}

	public MySaveChangesInterceptor(Func<Task<OperatingUser>> handler)
	{
		GetOperatorHandlerAsync = handler;
	}

	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		BeforeSavingChangesAsync(eventData).Wait();
		return base.SavingChanges(eventData, result);
	}

	public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		await BeforeSavingChangesAsync(eventData);
		return await base.SavingChangesAsync(eventData, result, cancellationToken);
	}


	protected async Task BeforeSavingChangesAsync(DbContextEventData eventData)
	{
		if (eventData.Context == null)
		{
			return;
		}
		foreach (var entry in eventData.Context.ChangeTracker.Entries())
		{
			if (entry.Entity is Entity.IBaseEntity entity)
			{
				var user = await GetOperatorAsync() ?? throw new InvalidOperationException("未读取到用户信息");
				if (entry.State is Microsoft.EntityFrameworkCore.EntityState.Added)
				{
					Extensions.EntityExtensions.SetCreateInfo(entity, user);
				}
				else if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
				{
					Extensions.EntityExtensions.SetModifyInfo(entity, user);
				}
				else if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
				{
					Extensions.EntityExtensions.SetDeleteInfo(entity, user);
					entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				}
			}

			//if (entry.State is Microsoft.EntityFrameworkCore.EntityState.Added)
			//{
			//	foreach (var pi in entry.Properties)
			//	{
			//		if (pi == null)
			//		{
			//			continue;
			//		}
			//		if (pi.Metadata.Name == "TenantId")
			//		{
			//			pi.CurrentValue = user != null ? user.TenantId : 0;
			//		}
			//	}
			//}
		}
	}
}
