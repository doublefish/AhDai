using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcNotaryFileCreateInput
/// </summary>
public class TwcNotaryFileCreateInput : BaseTwcNotaryInput
{
    /// <summary>
    /// 存证链路的统一Id，全局唯一
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = default!;
    /// <summary>
    /// 存证文件模式
    /// FILE_HASH（文件哈希）
    /// FILE_RAW（源文件）
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("file_notary_type")]
    public string FileNotaryType { get; set; } = default!;
    /// <summary>
    /// 当文件存证模式为FILE_HASH时，通过该参数指定哈希算法。目前只支持SHA256模式
    /// </summary>
    [JsonPropertyName("hash_algorithm")]
    public string? HashAlgorithm { get; set; }
    /// <summary>
    /// 存证文件内容，当文件模式为FILE_HASH时，存证内容为文件哈希。当文件模式为FILE_RAW时，存证内容为文件的base64编码，源文件大小限制在1MB以内。 建议使用文件哈希类型。
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("notary_file")]
    public string NotaryFile { get; set; } = default!;
    /// <summary>
    /// 存证文件名称
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("notary_name")]
    public string NotaryName { get; set; } = default!;
    /// <summary>
    /// 描述本条存证在存证事务中的阶段，用户可自行维护
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("phase")]
    public string Phase { get; set; } = default!;
    /// <summary>
    /// 扩展属性，长度不超过4096
    /// </summary>
    [JsonPropertyName("properties")]
    public string? Properties { get; set; }

}
