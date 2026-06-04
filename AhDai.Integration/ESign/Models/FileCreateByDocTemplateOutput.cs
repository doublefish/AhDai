using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 填写模板生成文件
/// </summary>
public class FileCreateByDocTemplateOutput
{
    /// <summary>
    /// 填充后生成的文件ID
    /// </summary>
    [JsonPropertyName("fileId")]
    public string FileId { get; set; } = default!;
    /// <summary>
    /// 文件下载地址（有效期为60分钟，过期后可以重新调用接口获取新的下载地址）
    /// </summary>
    [JsonPropertyName("fileDownloadUrl")]
    public string FileDownloadUrl { get; set; } = default!;

    /// <summary>
    /// 文件名：用于内部传递参数
    /// </summary>
    [JsonIgnore]
    public string FileName { get; set; } = default!;
    /// <summary>
    /// 模板Id：用于内部传递参数
    /// </summary>
    [JsonIgnore]
    public string TemplateId { get; set; } = default!;
}
