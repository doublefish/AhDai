using AhDai.Db;
using AhDai.Entity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// BaseTenantServiceImpl
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TQueryInput"></typeparam>
/// <param name="logger"></param>
/// <param name="enableDataPermission"></param>
/// <param name="enableCache"></param>
internal abstract class BaseTenantServiceImpl<TEntity, TInput, TOutput, TQueryInput>(ILogger logger, bool enableDataPermission = false, bool enableCache = false)
    : BaseServiceImpl<TEntity, TInput, TOutput, TQueryInput>(logger, enableDataPermission, enableCache)
    , IBaseService<TInput, TOutput, TQueryInput>
    where TEntity : class, IBaseTenantEntity
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TQueryInput : class, IBaseQueryInput
{

    protected override async Task<IQueryable<TEntity>> GenerateQueryAsync(DefaultDbContext db, bool includeDeleted = false, bool? enableDataPermission = null)
    {
        var query = await base.GenerateQueryAsync(db, includeDeleted, enableDataPermission);
        var loginData = await MyApp.GetLoginDataAsync();
        query = query.Where(x => x.TenantId == loginData.TenantId);
        return query;
    }
}


