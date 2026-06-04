using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// SignFlowDocOutput
/// </summary>
public class SignFlowDocOutput
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonPropertyName("fileId")]
    public string? FileId { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }
    /// <summary>
    /// 是否需要密码，默认false
    /// </summary>
    [JsonPropertyName("neededPwd")]
    public bool? NeededPwd { get; set; }
    /// <summary>
    /// 文件编辑密码
    /// </summary>
    [JsonPropertyName("fileEditPwd")]
    public string? FileEditPwd { get; set; }
    /// <summary>
    /// 合同类型ID
    /// </summary>
    [JsonPropertyName("contractBizTypeId")]
    public string? ContractBizTypeId { get; set; }
    /// <summary>
    /// 文件在签署页面的展示顺序
    /// 按序展示时支持传入顺序值：1 - 50（值越小越靠前）
    /// </summary>
    [JsonPropertyName("order")]
    public int? Order { get; set; }
}
