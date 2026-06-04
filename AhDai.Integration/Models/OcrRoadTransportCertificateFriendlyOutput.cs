using System;

namespace AhDai.Integration.Models;

/// <summary>
/// 道路运输证文字识别
/// </summary>
public class OcrRoadTransportCertificateFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 证号
    /// </summary>
    public string? Number { get; set; }
    /// <summary>
    /// 业户名称
    /// </summary>
    public string? OwnerName { get; set; }
    /// <summary>
    /// 住址
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// 车辆号牌
    /// </summary>
    public string? PlateNumber { get; set; }
    /// <summary>
    /// 经营许可证号
    /// </summary>
    public string? BusinessLicenseNumber { get; set; }
    /// <summary>
    /// 车辆类型
    /// </summary>
    public string? VehicleType { get; set; }
    /// <summary>
    /// 吨/座位
    /// </summary>
    public string? TonnageSeat { get; set; }
    /// <summary>
    /// 车辆长度（毫米）
    /// </summary>
    public decimal? Length { get; set; }
    /// <summary>
    /// 车辆宽度（毫米）
    /// </summary>
    public decimal? Width { get; set; }
    /// <summary>
    /// 车辆高度（毫米）
    /// </summary>
    public decimal? Height { get; set; }
    /// <summary>
    /// 经营范围
    /// </summary>
    public string? BusinessScope { get; set; }
    /// <summary>
    /// 经济类型
    /// </summary>
    public string? EconomyType { get; set; }
    /// <summary>
    /// 备注信息
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 发证机关
    /// </summary>
    public string? IssuingAuthority { get; set; }
    /// <summary>
    /// 初次领证日期
    /// </summary>
    public DateOnly? FirstIssueDate { get; set; }
    /// <summary>
    /// 发证日期
    /// </summary>
    public DateOnly? IssueDate { get; set; }
    /// <summary>
    /// 有效期至 
    /// </summary>
    public DateOnly? ExpiryDate { get; set; }
}
