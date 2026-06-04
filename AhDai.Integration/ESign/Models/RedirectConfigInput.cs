using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 重定向配置项
/// </summary>
public class RedirectConfigInput
{
    /// <summary>
    /// 账号标识（手机号/邮箱）
    /// </summary>
    [JsonPropertyName("redirectUrl")]
    public string? RedirectUrl { get; set; }
    /// <summary>
    /// 操作完成重定向跳转延迟时间，单位秒（可选值0、3，默认值为 3）
    /// 传0时，签署完成直接跳转重定向地址；
    /// 传3时，展示签署完成结果页，倒计时3秒后，自动跳转重定向地址。
    /// </summary>
    [JsonPropertyName("redirectDelayTime")]
    public int? RedirectDelayTime { get; set; }
}
