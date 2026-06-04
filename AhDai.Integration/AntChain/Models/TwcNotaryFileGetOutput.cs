using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcNotaryFileGetOutput
/// </summary>
public class TwcNotaryFileGetOutput : BaseTwcNotaryOutput
{
    /// <summary>
    /// 文件存证模式，有FILE_RAW和FILE_HASH两种模式
    /// </summary>
    [JsonPropertyName("file_notary_type")]
    public string FileNotaryType { get; set; } = default!;
    /// <summary>
    /// 文件哈希，当fileNotaryType为FILE_HASH时才有此值
    /// </summary>
    [JsonPropertyName("file_hash")]
    public string? FileHash { get; set; }
    /// <summary>
    /// 哈希算法，当存证模式为FILE_HASH时此值才有效
    /// </summary>
    [JsonPropertyName("hash_algorithm")]
    public string? HashAlgorithm { get; set; }
    /// <summary>
    /// 存证文件下载地址，当存证模式为FILE_RAW时才会返回此值
    /// </summary>
    [JsonPropertyName("oss_path")]
    public string? OssPath { get; set; } = default!;
}
