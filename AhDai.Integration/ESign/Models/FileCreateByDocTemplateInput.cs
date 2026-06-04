using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 填写模板生成文件
/// </summary>
public class FileCreateByDocTemplateInput
{
    /// <summary>
    /// 待填充的模板ID
    /// </summary>
    [JsonPropertyName("docTemplateId")]
    public string DocTemplateId { get; set; } = default!;
    /// <summary>
    /// 填充后生成的文件名称
    /// </summary>
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = default!;
    /// <summary>
    /// 控件列表
    /// </summary>
    [JsonPropertyName("components")]
    public DocTemplateComponentInput[] Components { get; set; } = default!;
    /// <summary>
    /// 是否校验PDF模板中必填控件，默认：false
    /// </summary>
    [JsonPropertyName("requiredCheck")]
    public bool RequiredCheck { get; set; }
}
