using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 签署流程签署人
/// </summary>
public class SignFlowSignerOutput
{
    /// <summary>
    /// 个人签署方信息
    /// </summary>
    [JsonPropertyName("psnSigner")]
    public PersonSignerOutput? PsnSigner { get; set; }
    /// <summary>
    /// 机构签署方信息
    /// </summary>
    [JsonPropertyName("orgSigner")]
    public OrgSignerOutput? OrgSigner { get; set; }
    /// <summary>
    /// 签署方类型
    /// 0 -个人
    /// 1 -机构（包含法定代表人和经办人签)
    /// </summary>
    [JsonPropertyName("signerType")]
    public int SignerType { get; set; }
    /// <summary>
    /// 签署顺序
    /// </summary>
    [JsonPropertyName("signOrder")]
    public int SignOrder { get; set; }
    /// <summary>
    /// 当前签署状态
    /// 0 - 等待签署
    /// 1 - 签署中
    /// 2 - 已签署
    /// 3 - 等待审批
    /// 4 - 已拒签
    /// </summary>
    [JsonPropertyName("signStatus")]
    public int SignStatus { get; set; }
    /// <summary>
    /// 签署任务类型
    /// 0 -会签
    /// 1 -或签
    /// </summary>
    [JsonPropertyName("signTaskType")]
    public int SignTaskType { get; set; }
    /// <summary>
    /// 签署方在签署时上传的附件列表信息
    /// </summary>
    [JsonPropertyName("uploadFiles")]
    public object[]? UploadFiles { get; set; }
    /// <summary>
    /// 签署区信息
    /// </summary>
    [JsonPropertyName("signFields")]
    public object[]? SignFields { get; set; }

}
