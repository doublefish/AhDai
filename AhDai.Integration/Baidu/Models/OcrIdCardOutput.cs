using AhDai.Core.Extensions;
using AhDai.Integration.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// OcrIdCardOutput
/// </summary>
public class OcrIdCardOutput : BaseOcrOutput<Dictionary<string, OcrCardWordsResult>>
{
    /// <summary>
    /// 图像方向，输入参数 detect_direction= true 时返回。
    /// - -1：未定义，
    /// - 0：正向，
    /// - 1：逆时针90度，
    /// - 2：逆时针180度，
    /// - 3：逆时针270度
    /// </summary>
    [JsonPropertyName("direction")]
    public int? Direction { get; set; }
    /// <summary>
    /// - normal-识别正常
    /// - reversed_side-身份证正反面颠倒
    /// - non_idcard-上传的图片中不包含身份证
    /// - blurred-身份证模糊
    /// - other_type_card-其他类型证照
    /// - over_exposure-身份证关键字段反光或过曝
    /// - over_dark-身份证欠曝（亮度过低）
    /// - unknown-未知状态
    /// </summary>
    [JsonPropertyName("image_status")]
    public string ImageStatus { get; set; } = default!;
    /// <summary>
    /// 输入参数 detect_ps = true 时，则返回该字段，判断身份证是否被PS，返回值：
    /// - 0：正常，
    /// - 1：PS，
    /// - -1：无效
    /// </summary>
    [JsonPropertyName("card_ps")]
    public int? CardPs { get; set; }
    /// <summary>
    /// 输入参数 detect_risk = true 时，则返回该字段识别身份证风险类型:
    /// - normal-正常身份证；
    /// - copy-复印件；
    /// - scan-扫描件；
    /// - temporary-临时身份证；
    /// - screen-翻拍；
    /// - screenshot-截屏（仅在开启 detect_screenshot 时返回）；
    /// - unknown-其他未知情况
    /// </summary>
    [JsonPropertyName("risk_type")]
    public string? RiskType { get; set; }
    /// <summary>
    /// 如果参数 detect_risk = true 时，则返回此字段。如果检测身份证被编辑过，该字段指定编辑软件名称，如:Adobe Photoshop CC 2014 (Macintosh)，如果没有被编辑过则返回值无此参数
    /// </summary>
    [JsonPropertyName("edit_tool")]
    public string? EditTool { get; set; }
    /// <summary>
    /// 输入参数 detect_quality = true 时，则返回该字段识别身份证质量类型
    /// </summary>
    [JsonPropertyName("card_quality")]
    public OcrIdCardQualityOutput? CardQuality { get; set; }
    /// <summary>
    /// 当请求参数 detect_photo = true时返回，头像切图的 base64 编码（无编码头，需自行处理）
    /// </summary>
    [JsonPropertyName("photo")]
    public string? Photo { get; set; }
    /// <summary>
    /// 当请求参数 detect_photo = true时返回，头像的位置信息（坐标0点为左上角）
    /// </summary>
    [JsonPropertyName("photo_location")]
    public OcrLocationOutput? PhotoLocation { get; set; }
    /// <summary>
    /// 当请求参数 detect_card = true时返回，身份证裁剪切图的 base64 编码（无编码头，需自行处理）
    /// </summary>
    [JsonPropertyName("card_image")]
    public string? CardImage { get; set; }
    /// <summary>
    /// 当请求参数 detect_card = true时返回，身份证裁剪切图的位置信息（坐标0点为左上角）
    /// </summary>
    [JsonPropertyName("card_location")]
    public OcrLocationOutput? CardLocation { get; set; }
    /// <summary>
    /// 用于校验身份证号码、性别、出生是否一致，输出结果及其对应关系如下：
    /// - -1： 身份证正面所有字段全为空
    /// - 0： 身份证证号不合法，此情况下不返回身份证证号
    /// - 1： 身份证证号和性别、出生信息一致
    /// - 2： 身份证证号和性别、出生信息都不一致
    /// - 3： 身份证证号和出生信息不一致
    /// - 4： 身份证证号和性别信息不一致
    /// </summary>
    [JsonPropertyName("idcard_number_type")]
    public int IdCardNumberType { get; set; }

    /// <summary>
    /// GetFriendlyOutput
    /// </summary>
    /// <returns></returns>
    public OcrIdCardFriendlyOutput? GetFriendlyOutput()
    {
        if (WordsResultNum == 0 || WordsResult == null) return null;

        var output = new OcrIdCardFriendlyOutput()
        {
            Name = WordsResult.GetValueOrDefault("姓名")?.Words,
            Sex = WordsResult.GetValueOrDefault("性别")?.Words,
            Nation = WordsResult.GetValueOrDefault("民族")?.Words,
            BirthDate = WordsResult.GetValueOrDefault("出生")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            Address = WordsResult.GetValueOrDefault("住址")?.Words,
            IdNumber = WordsResult.GetValueOrDefault("公民身份号码")?.Words,
            IssuingAuthority = WordsResult.GetValueOrDefault("签发机关")?.Words,
            IssueDate = WordsResult.GetValueOrDefault("签发日期")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
            //ExpirationDate = WordsResult.GetValueOrDefault("失效日期")?.Words?.ToDateOnlyExactOrNull(["yyyyMMdd"]),
        };

        if (WordsResult.TryGetValue("失效日期", out var result))
        {
            if (result.Words == "长期")
            {
                output.ExpirationDate = null;
                output.IsLongTerm = true;
            }
            else
            {
                output.ExpirationDate = result.Words.ToDateOnlyExactOrNull(["yyyyMMdd"]);
                output.IsLongTerm = false;
            }
        }

        return output;
    }
}
