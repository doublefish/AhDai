using AhDai.Core.Extensions;
using AhDai.Integration.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// DrivingLicenseOutput
/// </summary>
public class DrivingLicenseOutput : BaseOutput<Dictionary<string, WordsResult>>
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
    /// 质量告警信息，当 driving_license_side=front 且 quality_warn=true 时输出 quality_warn=true 时输出，
    /// - shield：驾驶证证照存在遮挡告警提示
    /// - incomplete：驾驶证证照边框不完整告警提示
    /// - fuzzy：驾驶证证照存在模糊告警提示
    /// </summary>
    [JsonPropertyName("warn_infos")]
    public string[]? WarnInfos { get; set; }
    /// <summary>
    /// 质量检测置信度，当 driving_license_side=front 且 quality_warn=true 时输出
    /// </summary>
    [JsonPropertyName("quality_propobility")]
    public LicenseQualityPropobilityOutput? QualityPropobility { get; set; }
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
    public OcrDrivingLicenseFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;
        var output = new OcrDrivingLicenseFriendlyOutput()
        {
            Name = WordsResult.GetValueOrDefault("姓名")?.Words,
            Number = WordsResult.GetValueOrDefault("证号")?.Words,
            Sex = WordsResult.GetValueOrDefault("性别")?.Words,
            Nationality = WordsResult.GetValueOrDefault("国籍")?.Words,
            Address = WordsResult.GetValueOrDefault("住址")?.Words,
            Birthday = WordsResult.GetValueOrDefault("注册日期")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            Class = WordsResult.GetValueOrDefault("准驾车型")?.Words,
            IssuingAuthority = WordsResult.GetValueOrDefault("签发机关")?.Words,
            FirstIssueDate = WordsResult.GetValueOrDefault("初次领证日期")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            IssueDate = WordsResult.GetValueOrDefault("有效期限")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            ExpiryDate = WordsResult.GetValueOrDefault("至")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
        };
        return output;
    }
}
