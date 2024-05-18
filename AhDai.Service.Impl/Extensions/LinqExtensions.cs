using System;
using System.Linq;
using System.Linq.Expressions;

namespace AhDai.Service.Impl;

internal static class LinqExtensions
{
	public static Expression<Func<T, bool>> True<T>() { return f => true; }
	public static Expression<Func<T, bool>> False<T>() { return f => false; }
	public static Expression<Func<T, T1, bool>> True<T, T1>() { return (t, t1) => true; }
	public static Expression<Func<T, T1, bool>> False<T, T1>() { return (t, t1) => false; }

	/// <summary>
	/// Or
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="expr1"></param>
	/// <param name="expr2"></param>
	/// <returns></returns>
	public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
	{
		var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
		return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
	}

	/// <summary>
	/// And
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="expr1"></param>
	/// <param name="expr2"></param>
	/// <returns></returns>
	public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
	{
		var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
		return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
	}


	private static Expression<Func<T, bool>> GenerateInArrayString<T, TValue>(ParameterExpression parameter, Expression property, TValue value)
	{
		ArgumentNullException.ThrowIfNull(value);
		var valueString = value?.ToString() ?? "";
		// 构建相等比较表达式
		var equal = Expression.Equal(property, Expression.Constant(valueString));
		// 构建 StartsWith 表达式
		var startsWith = Expression.Call(property, "StartsWith", null, Expression.Constant(valueString + ","));
		// 构建 EndsWith 表达式
		var endsWith = Expression.Call(property, "EndsWith", null, Expression.Constant("," + valueString));
		// 构建 Contains 表达式
		var contains = Expression.Call(property, "Contains", null, Expression.Constant("," + valueString + ","));
		// 构建 Or 表达式：(Equal || StartsWith || EndsWith || Contains)
		var body = Expression.OrElse(Expression.OrElse(Expression.OrElse(equal, startsWith), endsWith), contains);
		// 创建 Lambda 表达式
		return Expression.Lambda<Func<T, bool>>(body, parameter);
	}

	/// <summary>
	/// GenerateInArrayString
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="selector"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static Expression<Func<T, bool>> GenerateInArrayString<T, TValue>(Expression<Func<T, string?>> selector, TValue value)
	{
		ArgumentNullException.ThrowIfNull(selector);
		ArgumentNullException.ThrowIfNull(value);
		var parameter = selector.Parameters.Single();
		var property = selector.Body;
		return GenerateInArrayString<T, TValue>(parameter, property, value);
	}

	/// <summary>
	/// GenerateInArrayString
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="selector"></param>
	/// <param name="values"></param>
	/// <returns></returns>
	public static Expression<Func<T, bool>> GenerateInArrayString<T, TValue>(Expression<Func<T, string?>> selector, TValue[] values)
	{
		ArgumentNullException.ThrowIfNull(selector);
		ArgumentNullException.ThrowIfNull(values);
		var parameter = selector.Parameters.Single();
		var property = selector.Body;
		var predicate = False<T>();
		foreach (var value in values)
		{
			ArgumentNullException.ThrowIfNull(value);
			var lambda = GenerateInArrayString<T, TValue>(parameter, property, value);
			predicate = predicate.Or(lambda);
		}
		return predicate;
	}

	/// <summary>
	/// WhereInArrayString
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="queryable"></param>
	/// <param name="selector"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static IQueryable<T> WhereInArrayString<T, TValue>(this IQueryable<T> queryable, Expression<Func<T, string?>> selector, TValue value)
	{
		var lambda = GenerateInArrayString(selector, value);
		return queryable.Where(lambda);
	}

	/// <summary>
	/// WhereInArrayString
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <param name="queryable"></param>
	/// <param name="selector"></param>
	/// <param name="values"></param>
	/// <returns></returns>
	public static IQueryable<T> WhereInArrayString<T, TValue>(this IQueryable<T> queryable, Expression<Func<T, string?>> selector, TValue[] values)
	{
		var lambda = GenerateInArrayString(selector, values);
		return queryable.Where(lambda);
	}

}
