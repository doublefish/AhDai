using Nest;
using System;
using System.Collections.Generic;

namespace AhDai.Test.Models;

[ElasticsearchType(IdProperty = "id", RelationName = "")]
public class Bugsell : BaseModel
{
	/// <summary>
	/// 数码
	/// </summary>
	[Keyword]
	public string Code { get; set; } = "";
    /// <summary>
    /// 数码类型
    /// </summary>
    [Keyword]
	public string CodeType { get; set; } = "";
    /// <summary>
    /// 物流码
    /// </summary>
    [Keyword]
	public string LogisticsCode { get; set; } = "";
    /// <summary>
    /// 防伪查询事件Id
    /// </summary>
    [Keyword]
	public string EventId { get; set; } = "";
    /// <summary>
    /// 商品Id
    /// </summary>
    [Keyword]
	public string ProductId { get; set; } = "";
    /// <summary>
    /// 生产批次
    /// </summary>
    [Keyword]
	public string ProductionBatch { get; set; } = "";
    /// <summary>
    /// 生产日期
    /// </summary>
    [Date]
	public DateTime ProductionDate { get; set; }
	/// <summary>
	/// 最后物流单Id
	/// </summary>
	[Number]
	public long LogisticsOrderId { get; set; }
	/// <summary>
	/// 最后物流单号
	/// </summary>
	[Keyword]
	public string LogisticsOrderNo { get; set; } = "";
    /// <summary>
    /// 最后收货方Id
    /// </summary>
    [Number]
	public long LogisticsOrgId { get; set; }
	/// <summary>
	/// 应销区域代码
	/// </summary>
	[Keyword]
	public string[]? SalesDistrictCodes { get; set; }
    /// <summary>
    /// 查询用户所在地代码
    /// </summary>
    [Keyword]
	public string LocationCode { get; set; } = "";
    /// <summary>
    /// 所在地国家代码
    /// </summary>
    [Keyword]
	public string CountryCode { get; set; } = "";
    /// <summary>
    /// 所在地省份代码
    /// </summary>
    [Keyword]
	public string ProvinceCode { get; set; } = "";
    /// <summary>
    /// 所在地城市代码
    /// </summary>
    [Keyword]
	public string CityCode { get; set; } = "";
    /// <summary>
    /// 所在地区县代码
    /// </summary>
    [Keyword]
	public string DistrictCode { get; set; } = "";
    /// <summary>
    /// 所在地街道代码
    /// </summary>
    [Keyword]
	public string StreetCode { get; set; } = "";
    /// <summary>
    /// 查询渠道：Web-PC端网站，Wap-移动端网页，WeChat-微信，MinProgram-微信小程序，App-App，Tel-电话，Other-其他
    /// </summary>
    [Keyword]
	public string Channel { get; set; } = "";
    /// <summary>
    /// 查询类型：1-防伪/消费者，2-稽查
    /// </summary>
    [Number]
	public int Type { get; set; }
	/// <summary>
	/// 查询时间
	/// </summary>
	[Date]
	public DateTime Time { get; set; }
	/// <summary>
	/// 查询用户
	/// </summary>
	[Keyword]
	public string User { get; set; } = "";
    /// <summary>
    /// 查询用户名
    /// </summary>
    [Keyword]
	public string Username { get; set; } = "";
    /// <summary>
    /// 举证信息
    /// </summary>
    [Keyword]
	public string[]? Evidences { get; set; }
	/// <summary>
	/// 状态：1-正常销售，2-疑似窜货，3-稽查窜货，9-无法判定
	/// </summary>
	[Number]
	public int Status { get; set; }
	/// <summary>
	/// 稽查记录
	/// </summary>
	[Nested]
	public BugsellRecord[]? Records { get; set; }
}
