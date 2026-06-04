using AhDai.Core.Extensions;
using AhDai.Integration.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrVehicleLicenseOutput
/// </summary>
public class OcrVehicleLicenseOutput : BaseOcrOutput<Dictionary<string, OcrWordsResult>>
{
    /// <summary>
    /// 图像方向，当 detect_direction=true 时返回该字段。
    /// - -1：未定义，
    /// - 0：正向，
    /// - 1：逆时针90度，
    /// - 2：逆时针180度，
    /// - 3：逆时针270度
    /// </summary>
    [JsonPropertyName("direction")]
    public int? Direction { get; set; }
    /// <summary>
    /// 质量告警信息，当请求参数 vehicle_license_side=front 且 quality_warn=true 时输出，
    /// - shield：行驶证证照存在遮挡告警提示
    /// - incomplete：行驶证证照边框不完整告警提示
    /// - fuzzy：行驶证证照存在模糊告警提示
    /// </summary>
    [JsonPropertyName("warn_infos")]
    public string[]? WarnInfos { get; set; }
    /// <summary>
    /// 质量检测置信度，当请求参数 vehicle_license_side=front 且 quality_warn=true 时输出
    /// </summary>
    [JsonPropertyName("quality_propobility")]
    public OcrLicenseQualityPropobilityOutput? QualityPropobility { get; set; }
    /// <summary>
    /// 当输入参数 risk_warn=true 时输出，
    /// - normal：正常行驶证
    /// - copy：复印件
    /// - screen：翻拍
    /// </summary>
    [JsonPropertyName("risk_type")]
    public string? RiskType { get; set; }
    /// <summary>
    /// 当输入参数 risk_warn=true 时返回，如果检测行驶证被编辑过，该字段指定编辑软件名称，如：Adobe Photoshop CC 2014 (Macintosh)，如果没有被编辑过则返回值为空
    /// </summary>
    [JsonPropertyName("edit_tool")]
    public string? EditTool { get; set; }

    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrVehicleLicenseFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;
        var output = new OcrVehicleLicenseFriendlyOutput()
        {
            PlateNumber = WordsResult.GetValueOrDefault("号牌号码")?.Words,
            VehicleType = WordsResult.GetValueOrDefault("车辆类型")?.Words,
            Owner = WordsResult.GetValueOrDefault("所有人")?.Words,
            Address = WordsResult.GetValueOrDefault("住址")?.Words,
            UseCharacter = WordsResult.GetValueOrDefault("使用性质")?.Words,
            Model = WordsResult.GetValueOrDefault("品牌型号")?.Words,
            Vin = WordsResult.GetValueOrDefault("车辆识别代号")?.Words,
            EngineNumber = WordsResult.GetValueOrDefault("发动机号码")?.Words,
            IssuingAuthority = WordsResult.GetValueOrDefault("签发机关")?.Words,
            RegisterDate = WordsResult.GetValueOrDefault("注册日期")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            IssueDate = WordsResult.GetValueOrDefault("发证日期")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),

            // 副页
            CertificateSerialNumber = WordsResult.GetValueOrDefault("证芯编号")?.Words,
            FileNumber = WordsResult.GetValueOrDefault("档案编号")?.Words,
            AppraisedPassengerCapacity = WordsResult.GetValueOrDefault("核定载人数")?.Words,
            TotalMass = WordsResult.GetValueOrDefault("总质量")?.Words,
            CurbWeight = WordsResult.GetValueOrDefault("整备质量")?.Words,
            AppraisedPayload = WordsResult.GetValueOrDefault("核定载质量")?.Words,
            ExternalDimensions = WordsResult.GetValueOrDefault("外廓尺寸")?.Words,
            TractionMass = WordsResult.GetValueOrDefault("准牵引总质量")?.Words,
            FuelType = WordsResult.GetValueOrDefault("燃油类型")?.Words,
            Remark = WordsResult.GetValueOrDefault("备注")?.Words,
            InspectionRecord = WordsResult.GetValueOrDefault("检验记录")?.Words,
        };
        return output;
    }
}
