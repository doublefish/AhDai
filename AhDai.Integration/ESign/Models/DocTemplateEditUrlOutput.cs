using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 获取编辑合同模板页面出参
/// </summary>
public class DocTemplateEditUrlOutput
{
    /// <summary>
    /// 编辑文件模板的页面短链接（有效期24小时）
    /// </summary>
    [JsonPropertyName("docTemplateEditUrl")]
    public string DocTemplateEditUrl { get; set; } = default!;
    /// <summary>
    /// 编辑文件模板的页面长链接（有效期24小时）
    /// </summary>
    [JsonPropertyName("docTemplateEditLongUrl")]
    public string DocTemplateEditLongUrl { get; set; } = default!;
}
