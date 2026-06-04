using System.Collections;
using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 发起签署流程
/// </summary>
public class SignFlowCreateByFileInput
{
    /// <summary>
    /// 待签署文件信息
    /// </summary>
    [JsonPropertyName("docs")]
    public ArrayList? Docs { get; set; }
    /// <summary>
    /// 签署流程配置项
    /// </summary>
    [JsonPropertyName("signFlowConfig")]
    public object SignFlowConfig { get; set; } = default!;
    /// <summary>
    /// 签署流程的发起方
    /// </summary>
    [JsonPropertyName("signFlowInitiator")]
    public SignFlowInitiatorInput SignFlowInitiator { get; set; } = default!;
    /// <summary>
    /// 签署方信息
    /// </summary>
    [JsonPropertyName("signers")]
    public ArrayList? Signers { get; set; }
}
