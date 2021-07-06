using System;
using System.Collections.Generic;

namespace Adai.Standard.Model
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
			PageNumber = pageNumber ?? 0;
			PageSize = pageSize ?? 20;
			SortName = sortName;
			SortType = sortType;
			CountFlag = StatisticFlag.General;
		}

		/// <summary>
		/// 每页条数
		/// </summary>
		public int PageSize { get; set; }
		/// <summary>
		/// 页码（从0开始）
		/// </summary>
		public int PageNumber { get; set; }
		/// <summary>
		/// 总记录条数，如果此属性预设值大于零则表示不需要从数据库获取。
		/// </summary>
		public int TotalCount { get; set; }
		/// <summary>
		/// 分页结果集。
		/// </summary>
		public ICollection<T> Results { get; set; }
		/// <summary>
		/// 排序字段
		/// </summary>
		public string SortName { get; set; }
		/// <summary>
		/// 排序方式：ASC/DESC
		/// </summary>
		public SortType SortType { get; set; }
		/// <summary>
		/// 查询关联数据
		/// </summary>
		public string[] LinkProperties { get; set; }
		/// <summary>
		/// 只统计总数
		/// </summary>
		public bool _OnlyCount { get; set; }
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
		internal string[] Keywords => string.IsNullOrEmpty(Keyword) ? null : Keyword.Split(' ');

		/// <summary>
		/// 编码
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 全名
		/// </summary>
		public string FullName { get; set; }
		/// <summary>
		/// 编号
		/// </summary>
		public string Number { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public int? Type { get; set; }
		/// <summary>
		/// 类型名称
		/// </summary>
		public string TypeName { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public int[] Types { get; set; }
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
		/// <summary>
		/// 最小值
		/// </summary>
		public decimal? Min { get; set; }
		/// <summary>
		///最大值
		/// </summary>
		public decimal? Max { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? Start { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime? End { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public int[] Statuses { get; set; }
	}
}
