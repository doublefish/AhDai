using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 获取制作合同模板页面入参
/// </summary>
public class DocTemplateCreateUrlInput
{
    /// <summary>
    /// 模板名称
    /// </summary>
    [JsonPropertyName("docTemplateName")]
    public string? DocTemplateName { get; set; }
    /// <summary>
    /// 模板类型，默认值为 0
    /// 0 - PDF模板
    /// 1 - HTML模板
    /// </summary>
    [JsonPropertyName("docTemplateType")]
    public int? DocTemplateType { get; set; }
    /// <summary>
    /// 底稿文件ID（原始文件的编号）
    /// </summary>
    [JsonPropertyName("fileId")]
    public string FileId { get; set; } = default!;
    /// <summary>
    /// 制作模板完成后页面重定向跳转地址
    /// </summary>
    [JsonPropertyName("redirectUrl")]
    public string? RedirectUrl { get; set; }
    /// <summary>
    /// 是否隐藏原始控件，默认false
    /// </summary>
    [JsonPropertyName("hiddenOriginComponents")]
    public bool? HiddenOriginComponents { get; set; }
    /// <summary>
    /// 要展示的基础控件类型列表（不传默认全部展示）
    /// 1 - 单行文本，2 - 数字，3 - 日期，5 - 骑缝签署区，6 - 普通签章区，8 - 多行文本，9 - 复选，10 - 单选，11 - 图片，14 -下拉框，15 - 勾选框，16 - 身份证，17 - 备注区域
    /// 注：hiddenOriginComponents=false时才生效
    /// </summary>
    [JsonPropertyName("basicComponentsType")]
    public int[]? BasicComponentsType { get; set; }
    /// <summary>
    /// 是否展示替换底稿按钮，默认值 false
    /// true - 展示替换底稿按钮
    /// false - 不展示替换底稿按钮
    /// </summary>
    [JsonPropertyName("showReplaceFraft")]
    public bool? ShowReplaceFraft { get; set; }
    /// <summary>
    /// 要展示的自定义控件组ID列表
    /// </summary>
    [JsonPropertyName("customComponentGroups")]
    public string[]? CustomComponentGroups { get; set; }
    /// <summary>
    /// 要展示的自定义控件ID列表
    /// </summary>
    [JsonPropertyName("customComponents")]
    public string[]? CustomComponents { get; set; }
    /// <summary>
    /// 签署方角色标识，只对签署区控件生效（可以自定义命名，如：甲方、乙方）
    /// </summary>
    [JsonPropertyName("signerRoles")]
    public string[]? SignerRoles { get; set; }


}
