using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 获取编辑合同模板页面入参
/// </summary>
public class DocTemplateEditUrlInput
{
    /// <summary>
    /// 编辑模板完成后页面重定向跳转地址
    /// </summary>
    [JsonPropertyName("redirectUrl")]
    public string? RedirectUrl { get; set; }
    /// <summary>
    /// 是否隐藏原始控件，默认false
    /// </summary>
    [JsonPropertyName("hiddenOriginComponents")]
    public bool? HiddenOriginComponents { get; set; }
}
