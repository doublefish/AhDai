using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// TsrOutput
/// </summary>
public class TsrOutput
{
    /// <summary>
    /// 可信时间请求结果状态码
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    /// <summary>
    /// hash后的信息
    /// </summary>
    [JsonPropertyName("hashed_message")]
    public string? HashedMessage { get; set; }
    /// <summary>
    /// 哈希算法
    /// </summary>
    [JsonPropertyName("hash_algorithm")]
    public string? HashAlgorithm { get; set; }
    /// <summary>
    /// 请求失败时候的错误信息
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    /// <summary>
    /// 时间
    /// </summary>
    [JsonPropertyName("ts")]
    public string? Ts { get; set; }
    /// <summary>
    /// 精简后的时间戳完整编码（在校验时需要提交）
    /// </summary>
    [JsonPropertyName("ctsr")]
    public string? Ctsr { get; set; }
    /// <summary>
    /// 凭证序列号 （在校验的时需要先填写凭证编号）
    /// </summary>
    [JsonPropertyName("sn")]
    public string? Sn { get; set; }
}
