using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AhDai.Core.Utils;

/// <summary>
/// LinqUtil
/// </summary>
public static class LinqUtil
{
    /// <summary>
    /// BuildRangedPredicates
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selector"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="includeMin"></param>
    /// <param name="includeMax"></param>
    /// <returns></returns>
    public static IList<Expression<Func<T, bool>>> BuildRangedPredicates<T>(Expression<Func<T, decimal>> selector, decimal? min, decimal? max, bool? includeMin = null, bool? includeMax = null)
    {
        var predicates = new List<Expression<Func<T, bool>>>();
        if (min.HasValue)
        {
            var body = includeMin == true
                ? Expression.GreaterThanOrEqual(selector.Body, Expression.Constant(min.Value))
                : Expression.GreaterThan(selector.Body, Expression.Constant(min.Value));
            var lambda = Expression.Lambda<Func<T, bool>>(body, selector.Parameters[0]);
            predicates.Add(lambda);
        }
        if (max.HasValue)
        {
            var body = includeMax == true
                ? Expression.LessThanOrEqual(selector.Body, Expression.Constant(max.Value))
                : Expression.LessThan(selector.Body, Expression.Constant(max.Value));
            var lambda = Expression.Lambda<Func<T, bool>>(body, selector.Parameters[0]);
            predicates.Add(lambda);
        }
        return predicates;
    }
}
