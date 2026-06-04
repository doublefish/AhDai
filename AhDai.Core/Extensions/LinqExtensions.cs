using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AhDai.Core.Extensions;

/// <summary>
/// LinqExt
/// </summary>
public static class LinqExtensions
{
    class ReplaceParameterVisitor(ParameterExpression oldParam, ParameterExpression newParam) : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParam = oldParam;
        private readonly ParameterExpression _newParam = newParam;

        protected override Expression VisitParameter(ParameterExpression node) => node == _oldParam ? _newParam : node;
    }

    /// <summary>
    /// True
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> True<T>() => x => true;
    /// <summary>
    /// False
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> False<T>() => x => false;

    /// <summary>
    /// 添加多个条件
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public static IQueryable<TSource> WhereAll<TSource>(this IQueryable<TSource> source, IEnumerable<Expression<Func<TSource, bool>>> predicates) where TSource : class
    {
        foreach (var predicate in predicates)
        {
            source = source.Where(predicate);
        }
        return source;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="pageNumber">页码</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query.Skip(pageSize * pageNumber).Take(pageSize);
    }

    /// <summary>
    /// Or
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var leftBody = new ReplaceParameterVisitor(left.Parameters[0], parameter).Visit(left.Body)!;
        var rightBody = new ReplaceParameterVisitor(right.Parameters[0], parameter).Visit(right.Body)!;

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftBody, rightBody), parameter);
    }

    /// <summary>
    /// 动态排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="sortName"></param>
    /// <param name="sortType"></param>
    /// <returns></returns>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string? sortName, string? sortType)
        where T : class
    {
        return query.OrderByDynamic(sortName?.Split(','), sortType);
    }

    /// <summary>
    /// 动态排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="sortNames"></param>
    /// <param name="sortType"></param>
    /// <returns></returns>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string[]? sortNames, string? sortType)
        where T : class
    {
        if (sortNames == null || sortNames.Length == 0) return query;

        var entityType = typeof(T);
        var parameter = Expression.Parameter(entityType, "p");
        var expr = query.Expression;
        var isAsc = sortType == "asc";
        foreach (var sortName in sortNames)
        {
            var property = entityType.GetProperty(sortName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);// ?? throw new ArgumentException($"属性【{sortName}】不存在");
            if (property == null) continue;

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var method = (expr == query.Expression) ? (isAsc ? "OrderBy" : "OrderByDescending") : (isAsc ? "ThenBy" : "ThenByDescending");

            expr = Expression.Call(typeof(Queryable), method, [entityType, property.PropertyType], expr, Expression.Quote(orderByExp));
        }
        return query.Provider.CreateQuery<T>(expr);
    }
}
