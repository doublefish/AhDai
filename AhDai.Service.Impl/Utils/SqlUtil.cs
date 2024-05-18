using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Service.Utils;

/// <summary>
/// SqlUtils
/// </summary>
internal static class SqlUtil
{
	/// <summary>
	/// GeneratePageSql
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="pageSize"></param>
	/// <param name="pageIndex"></param>
	/// <returns></returns>
	public static string GeneratePageSql(string sql, int pageSize, int pageIndex)
	{
		var skip = pageSize * (pageIndex - 1);
		var take = skip + pageSize;
		return $@"
SELECT * FROM (
SELECT ""m2"".*, ROWNUM r2 FROM ({sql}) ""m2"") ""m1""
WHERE ""m1"".r2 > {skip} AND ""m1"".r2 <= {take}
";
	}

	/// <summary>
	/// GeneratePageSql
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string GeneratePageSql<Q>(string sql, Q input) where Q : BaseQueryInput
	{
		return GeneratePageSql(sql, input.PageSize ?? 10, input.PageNo ?? 1);
	}

	/// <summary>
	/// GenerateTakeSql
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="take"></param>
	/// <returns></returns>
	public static string GenerateTakeSql(string sql, int take)
	{
		return $@"SELECT * FROM ({sql}) ""m1"" WHERE ROWNUM <= {take}";
	}

	/// <summary>
	/// GenerateTakeSql
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string GenerateTakeSql<Q>(string sql, Q input) where Q : BaseQueryInput
	{
		return GenerateTakeSql(sql, input.PageSize ?? 1000);
	}
}
