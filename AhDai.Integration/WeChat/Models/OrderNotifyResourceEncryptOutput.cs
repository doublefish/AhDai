using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 通知资源数据（加密）
/// </summary>
public class OrderNotifyResourceEncryptOutput
{
    /// <summary>
    /// 对开启结果数据进行加密的加密算法，目前只支持AEAD_AES_256_GCM。
    /// </summary>
    [JsonPropertyName("algorithm")]
    public string Algorithm { get; set; } = default!;
    /// <summary>
    /// Base64编码后的开启/停用结果数据密文
    /// </summary>
    [JsonPropertyName("ciphertext")]
    public string Ciphertext { get; set; } = default!;
    /// <summary>
    /// 附加数据。
    /// </summary>
    [JsonPropertyName("associated_data")]
    public string? AssociatedData { get; set; }
    /// <summary>
    /// 原始回调类型，为transaction。
    /// </summary>
    [JsonPropertyName("original_type")]
    public string OriginalType { get; set; } = default!;
    /// <summary>
    /// 加密使用的随机串。
    /// </summary>
    [JsonPropertyName("nonce")]
    public string Nonce { get; set; } = default!;
}
