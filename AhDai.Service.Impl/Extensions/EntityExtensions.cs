using AhDai.Db;
using AhDai.Entity;
using AhDai.Entity.Sys;
using AhDai.Service.Models;
using AhDai.Service.Sys;
using AhDai.Shared;
using AhDai.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

/// <summary>
/// EntityExtensions
/// </summary>
internal static class EntityExtensions
{
    /// <summary>
    /// QueryByDataPermissionAsync
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="db"></param>
    /// <param name="loginData"></param>
    /// <returns></returns>
    public static async Task<IQueryable<TEntity>> QueryByDataPermissionAsync<TEntity>(this IQueryable<TEntity> query, DefaultDbContext db, LoginData? loginData = null) where TEntity : class, IBaseEntity
    {
        loginData ??= await MyApp.GetLoginDataAsync();
        if (loginData.IsAdmin)
        {
            return query;
        }
        var userId = loginData.Id;
        var userService = MyApp.Services.GetRequiredService<IUserService>();
        var userDataPermissions = await userService.GetDataPermissionAsync(userId, true);

        var orgIds = new HashSet<long>();
        var predicate = LinqExtensions.False<TEntity>();
        var predicate1 = LinqExtensions.False<UserOrg>();
        var predicate2 = LinqExtensions.False<Organization>();

        var q1 = db.UserOrgs.Where(x => x.IsDefault == true && x.IsDeleted == false);
        var q2 = db.Organizations.Where(x => true);

        foreach (var o in userDataPermissions)
        {
            switch (o.DataPermission)
            {
                case DataPermission.Self:
                    predicate = predicate.Or(x => q1.Any(y => y.UserId == x.CreatorId && y.OrgId == o.OrgId) && x.CreatorId == userId);
                    break;
                case DataPermission.Department:
                    //predicate0 = predicate0.Or(x => q1.Any(y => y.UserId == x.CreatorId && y.OrgId == o.OrgId));
                    orgIds.Add(o.OrgId);
                    break;
                case DataPermission.DepartmentAndSubordinates:
                case DataPermission.All:
                    predicate2 = predicate2.Or(x => x.UniqueCode.StartsWith(o.UniqueCode) && x.Level >= o.LevelFrom && x.Level <= o.LevelTo);
                    break;
            }
        }
        if (orgIds.Count > 0)
        {
            predicate1 = predicate1.Or(x => orgIds.Contains(x.OrgId));
        }
        predicate1 = predicate1.Or(x => q2.Where(predicate2).Select(y => y.Id).Contains(x.OrgId));
        predicate = predicate.Or(x => q1.Where(predicate1).Any(y => y.UserId == x.CreatorId));
        return query.Where(predicate);
    }

    /// <summary>
    /// QueryByNumber
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> QueryByNumber<TEntity, TQueryInput>(this IQueryable<TEntity> query, TQueryInput input)
        where TEntity : class, INumberEntity
        where TQueryInput : class, INumberQueryInput
    {
        if (!string.IsNullOrEmpty(input.Number))
        {
            query = query.Where(x => x.Number == input.Number);
        }
        if (input.Numbers != null && input.Numbers.Length > 0)
        {
            query = query.Where(x => input.Numbers.Contains(x.Number));
        }
        return query;
    }

    /// <summary>
    /// QueryByAmount
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> QueryByAmount<TEntity, TQueryInput>(this IQueryable<TEntity> query, TQueryInput input)
        where TEntity : class, IAmountEntity
        where TQueryInput : class, IAmountQueryInput
    {
        if (input.Amount.HasValue)
        {
            query = query.Where(x => x.Amount == input.Amount.Value);
        }
        if (input.AmountMin.HasValue)
        {
            query = query.Where(x => x.Amount >= input.AmountMin.Value);
        }
        if (input.AmountMax.HasValue)
        {
            query = query.Where(x => x.Amount <= input.AmountMax.Value);
        }
        return query;
    }

    /// <summary>
    /// QueryByDate
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> QueryByDate<TEntity, TQueryInput>(this IQueryable<TEntity> query, TQueryInput input)
        where TEntity : class, IDateEntity
        where TQueryInput : class, IDateQueryInput
    {
        if (input.StartDate.HasValue)
        {
            query = query.Where(x => x.Date >= input.StartDate.Value.Date);
        }
        if (input.EndDate.HasValue)
        {
            query = query.Where(x => x.Date < input.EndDate.Value.Date.AddDays(1));
        }
        return query;
    }

    /// <summary>
    /// QueryByNullableDate
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> QueryByNullableDate<TEntity, TQueryInput>(this IQueryable<TEntity> query, TQueryInput input)
        where TEntity : class, INullableDateEntity
        where TQueryInput : class, IDateQueryInput
    {
        if (input.StartDate.HasValue)
        {
            query = query.Where(x => x.Date >= input.StartDate.Value.Date);
        }
        if (input.EndDate.HasValue)
        {
            query = query.Where(x => x.Date < input.EndDate.Value.Date.AddDays(1));
        }
        return query;
    }

    /// <summary>
    /// QueryByOrg
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="input"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> QueryByOrg<TEntity, TQueryInput>(this IQueryable<TEntity> query, TQueryInput input, DefaultDbContext db)
        where TEntity : class, IOrgIdEntity
        where TQueryInput : class, IOrgIdQueryInput
    {
        if (input.OrgId.HasValue)
        {
            query = query.Where(o => o.OrgId == input.OrgId.Value);
        }
        if (!string.IsNullOrEmpty(input.OrgName))
        {
            query = query.Where(x => db.Organizations.Any(y => y.Id == x.OrgId && y.Name.Contains(input.OrgName)));
        }
        return query;
    }
   
}
