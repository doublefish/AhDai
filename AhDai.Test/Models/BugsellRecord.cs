using Nest;
using System;

namespace AhDai.Test.Models;

public class BugsellRecord
{
	/// <summary>
	/// Id
	/// </summary>
	[Number]
	public long Id{ get; set; }
	/// <summary>
	/// 查询数码
	/// </summary>
	[Keyword]
	public string Code{ get; set; } = "";
    /// <summary>
    /// 查询用户所在地代码
    /// </summary>
    [Keyword]
	public string LocationCode{ get; set; } = "";
    /// <summary>
    /// 查询渠道
    /// </summary>
    [Keyword]
	public string Channel{ get; set; } = "";
    /// <summary>
    /// 查询类型：1-防伪/消费者，2-稽查
    /// </summary>
    [Number]
	public int Type{ get; set; }
	/// <summary>
	/// 查询时间
	/// </summary>
	[Date]
	public DateTime Time{ get; set; }
	/// <summary>
	/// 查询用户
	/// </summary>
	[Keyword]
	public string User{ get; set; } = "";
    /// <summary>
    /// 状态：1-正常销售，2-疑似窜货，3-稽查窜货，9-无法判定
    /// </summary>
    [Number]
	public int Status{ get; set; }
}
