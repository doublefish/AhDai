using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models.Sms;

/// <summary>
/// SendOutput
/// </summary>
public class SendOutput : BaseOutput
{
    /// <summary>
    /// 发送回执ID
    /// </summary>
    [JsonPropertyName("BizId")]
    public string BizId { get; set; } = default!;
}
