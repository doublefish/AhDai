using System;

namespace AhDai.Integration.Models;

/// <summary>
/// 行驶证文字识别
/// </summary>
public class OcrVehicleLicenseFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 号牌号码
    /// </summary>
    public string? PlateNumber { get; set; }
    /// <summary>
    /// 车辆类型
    /// </summary>
    public string? VehicleType { get; set; }
    /// <summary>
    /// 所有人
    /// </summary>
    public string? Owner { get; set; }
    /// <summary>
    /// 住址
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// 使用性质
    /// </summary>
    public string? UseCharacter { get; set; }
    /// <summary>
    /// 品牌型号
    /// </summary>
    public string? Model { get; set; }
    /// <summary>
    /// 车辆识别代号
    /// </summary>
    public string? Vin { get; set; }
    /// <summary>
    /// 发动机号码
    /// </summary>
    public string? EngineNumber { get; set; }
    /// <summary>
    /// 签发机关
    /// </summary>
    public string? IssuingAuthority { get; set; }
    /// <summary>
    /// 注册日期
    /// </summary>
    public DateOnly? RegisterDate { get; set; }
    /// <summary>
    /// 发证日期
    /// </summary>
    public DateOnly? IssueDate { get; set; }

    /// <summary>
    /// 证芯编号（主页上的防伪编号）
    /// </summary>
    public string? CertificateSerialNumber { get; set; }
    /// <summary>
    /// 档案编号
    /// </summary>
    public string? FileNumber { get; set; }
    /// <summary>
    /// 核定载人数 (人)
    /// </summary>
    public string? AppraisedPassengerCapacity { get; set; }
    /// <summary>
    /// 总质量（千克）
    /// </summary>
    public string? TotalMass { get; set; }
    /// <summary>
    /// 整备质量（千克） - 车辆自重
    /// </summary>
    public string? CurbWeight { get; set; }
    /// <summary>
    /// 核定载质量（千克） - 允许装载的最大货物重量
    /// </summary>
    public string? AppraisedPayload { get; set; }
    /// <summary>
    /// 外廓尺寸（毫米） - 长x宽x高
    /// </summary>
    public string? ExternalDimensions { get; set; }
    /// <summary>
    /// 准牵引总质量（千克） - 牵引车特有字段
    /// </summary>
    public string? TractionMass { get; set; }
    /// <summary>
    /// 燃料类型
    /// </summary>
    public string? FuelType { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 检验记录（即年审截止日期说明）
    /// </summary>
    public string? InspectionRecord { get; set; }
}
