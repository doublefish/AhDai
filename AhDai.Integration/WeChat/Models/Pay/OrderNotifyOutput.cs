using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.Pay;

/// <summary>
/// 支付通知出参
/// </summary>
public class OrderNotifyOutput
{
    /// <summary>
    /// 通知的唯一ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    /// <summary>
    /// 通知创建的时间，遵循rfc3339标准格式，格式为yyyy-MM-DDTHH:mm:ss+TIMEZONE，例如：2015-05-20T13:29:35+08:00表示北京时间2015年05月20日13点29分35秒。
    /// </summary>
    [JsonPropertyName("create_time")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 通知的类型，支付成功通知的类型为TRANSACTION.SUCCESS
    /// </summary>
    [JsonPropertyName("event_type")]
    public string EventType { get; set; } = default!;
    /// <summary>
    /// 通知的资源数据类型，支付成功通知为encrypt-resource
    /// </summary>
    [JsonPropertyName("resource_type")]
    public string ResourceType { get; set; } = default!;
    /// <summary>
    /// 通知资源数据
    /// </summary>
    [JsonPropertyName("resource")]
    public OrderNotifyResourceEncryptOutput Resource { get; set; } = default!;
    /// <summary>
    /// 通知资源数据（解密后）
    /// </summary>
    [JsonIgnore]
    public OrderNotifyResourceDecryptOutput DecrypResource { get; set; } = default!;
    /// <summary>
    /// 回调摘要
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; set; } = default!;
}
