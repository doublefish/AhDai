using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Faceprint;

/// <summary>
/// 核验详细信息
/// </summary>
public class VerifyDetail
{
    /// <summary>
    /// 本次核验流程中请求的人脸实名认证V4（方案配置权威数据源比对）、人脸对比V4（方案配置自建人脸库比对）后端接口logid 可在记录查询平台中通过logid查询到3天内的记录。 少数核验失败情况下，实际并未发生上述俩接口的请求，则该字段为空，如：用户拒绝摄像头授权且不允许降级
    /// </summary>
    [JsonPropertyName("verify_log_id")]
    public string VerifyLogId { get; set; } = default!;
    /// <summary>
    /// 本次核验流程中采集的人脸最佳质量图下载url
    /// </summary>
    [JsonPropertyName("face_image")]
    public string FaceImage { get; set; } = default!;
    /// <summary>
    /// 人脸相似度得分，大于等于方案设置阈值为同一人；当身份证姓名不匹配或活体不通过时，该项为空
    /// </summary>
    [JsonPropertyName("score")]
    public double Score { get; set; }
    /// <summary>
    /// 本次核验时，所使用的方案配置相似度阈值
    /// </summary>
    [JsonPropertyName("threshold")]
    public double Threshold { get; set; }
    /// <summary>
    /// 活体检测得分，注：预留功能字段，当前返回为0
    /// </summary>
    [JsonPropertyName("liveness_score")]
    public double LivenessScore { get; set; }
    /// <summary>
    /// 方案配置使用合成图功能，将返回合成图得分，注：预留功能字段，当前返回为0
    /// </summary>
    [JsonPropertyName("spoofing_score")]
    public double SpoofingScore { get; set; }
    /// <summary>
    /// 安全风控等级 方案配置启用安全风控，将返回该字段 值为1或2时表示触发了安全风险，值为3或4时无风险
    /// </summary>
    [JsonPropertyName("risk_level")]
    public string RiskLevel { get; set; } = default!;
    /// <summary>
    /// 安全风控标签 方案配置启用安全风控，将返回该字段 当risk_level值为1或2时，返回具体风险类型，risk_level值为3或4时，为空
    /// </summary>
    [JsonPropertyName("risk_tag")]
    public string RiskTag { get; set; } = default!;
    /// <summary>
    /// 方案配置为实时检测时，将返回该字段，代表本次核验是否出现降级 降级返回true，未降级返回false 注：当环境不支持实时检测，且方案配置允许降级时，将降级为录制视频上传
    /// </summary>
    [JsonPropertyName("is_demote")]
    public bool? IsDemote { get; set; }
    /// <summary>
    /// 是否App
    /// </summary>
    [JsonPropertyName("app")]
    public bool App { get; set; }

}
