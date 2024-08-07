﻿using System.Linq;

namespace AhDai.Base.Extensions
{
    /// <summary>
    /// LinqExt
    /// </summary>
    public static class LinqExtensions
    {
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
    }
}
