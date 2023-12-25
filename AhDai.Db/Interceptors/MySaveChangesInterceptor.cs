using AhDai.Core.Models;
using AhDai.Db.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AhDai.Db.Interceptors;

internal class MySaveChangesInterceptor : SaveChangesInterceptor
{

	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		BeforeSavingChanges(eventData);
		return base.SavingChanges(eventData, result);
	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		BeforeSavingChanges(eventData);
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}


	protected void BeforeSavingChanges(DbContextEventData eventData)
	{
		// TODO
		var token = new TokenData();
		foreach (var entry in eventData.Context.ChangeTracker.Entries())
		{
			if (entry.State is Microsoft.EntityFrameworkCore.EntityState.Added)
			{
				if (entry.Entity is BaseModel model)
				{
					Extensions.ModelExtensions.SetCreateInfo(model, token);
				}
			}
			else if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
			{
				if (entry.Entity is BaseModel model)
				{
					Extensions.ModelExtensions.SetUpdateInfo(model, token);
				}
			}
			else if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
			{
				if (entry.Entity is BaseModel model)
				{
					Extensions.ModelExtensions.SetUpdateInfo(model, token);
					model.RowDeleted = true;
					entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				}
			}
		}
	}
}
