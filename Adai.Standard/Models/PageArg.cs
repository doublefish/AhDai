using System;
using System.Collections.Generic;

namespace Adai.Standard.Models
{
	/// <summary>
	/// 查询条件
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PageArg<T> where T : class, new()
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="sortName"></param>
		/// <param name="sortType"></param>
		public PageArg(int? pageNumber = null, int? pageSize = null, string sortName = null, SortType sortType = 0)
		{
			//Filter = new T();
			PageNumber = pageNumber ?? 0;
			PageSize = pageSize ?? 20;
			SortName = sortName;
			SortType = sortType;
			CountFlag = StatisticFlag.Not;
		}

		#region 查询结果
		/// <summary>
		/// 总记录条数，如果此属性预设值大于零则表示不需要从数据库获取。
		/// </summary>
		public int TotalCount { get; set; }
		/// <summary>
		/// 分页结果集。
		/// </summary>
		public ICollection<T> Results { get; set; }
		#endregion

		#region 查询条件
		/// <summary>
		/// 筛选
		/// </summary>
		public T Filter { get; set; }
		/// <summary>
		/// 每页条数
		/// </summary>
		public int PageSize { get; set; }
		/// <summary>
		/// 页码（从0开始）
		/// </summary>
		public int PageNumber { get; set; }
		/// <summary>
		/// 排序字段
		/// </summary>
		public string SortName { get; set; }
		/// <summary>
		/// 排序方式：ASC/DESC
		/// </summary>
		public SortType SortType { get; set; }

		/// <summary>
		/// 只统计总数
		/// </summary>
		public bool OnlyCount { get; set; }
		/// <summary>
		/// 统计数量标识
		/// </summary>
		public StatisticFlag CountFlag { get; set; }
		/// <summary>
		/// 统计总和标识
		/// </summary>
		public StatisticFlag SumFlag { get; set; }
		/// <summary>
		/// 计算结果
		/// </summary>
		public IDictionary<string, decimal> SumResults { get; set; }
		/// <summary>
		/// 关键词
		/// </summary>
		public string Keyword { get; set; }
		/// <summary>
		/// 关键词组
		/// </summary>
		public string[] Keywords => string.IsNullOrEmpty(Keyword) ? null : Keyword.Split(' ');
		/// <summary>
		/// 用户Id
		/// </summary>
		public int? UserId { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// 平台标识
		/// </summary>
		public int? Platform { get; set; }
		#endregion
	}
}
