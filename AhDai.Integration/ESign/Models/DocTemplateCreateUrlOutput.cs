using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 获取制作合同模板页面出参
/// </summary>
public class DocTemplateCreateUrlOutput
{
    /// <summary>
    /// 合同模板ID
    /// </summary>
    [JsonPropertyName("docTemplateId")]
    public string DocTemplateId { get; set; } = default!;
    /// <summary>
    /// 制作合同模板的页面短链接（有效期24小时）
    /// </summary>
    [JsonPropertyName("docTemplateCreateUrl")]
    public string DocTemplateCreateUrl { get; set; } = default!;
    /// <summary>
    /// 制作合同模板的页面长链接（有效期24小时）
    /// </summary>
    [JsonPropertyName("docTemplateCreateLongUrl")]
    public string DocTemplateCreateLongUrl { get; set; } = default!;
}
