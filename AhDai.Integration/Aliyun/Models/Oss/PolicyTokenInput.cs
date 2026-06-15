using System;

namespace AhDai.Integration.Aliyun.Models.Oss;

/// <summary>
/// PolicyTokenInput
/// </summary>
public class PolicyTokenInput
{
    /// <summary>
    /// 地域
    /// </summary>
    public string Region { get; set; } = default!;
    /// <summary>
    /// 容器
    /// </summary>
    public string Bucket { get; set; } = default!;
    /// <summary>
    /// 文件名
    /// </summary>
    public string Name { get; set; } = default!;
    /// <summary>
    /// 哈希
    /// </summary>
    public string Hash { get; set; } = default!;
    /// <summary>
    /// 对象全名
    /// </summary>
    public string KeyPrefix { get; set; } = default!;
    /// <summary>
    /// Expiration
    /// </summary>
    public DateTime Expiration { get; set; }
    /// <summary>
    /// 最大长度
    /// </summary>
    public long MaxLength { get; set; }
    /// <summary>
    /// 内容类型
    /// </summary>
    public string[]? ContentTypes { get; set; }
    /// <summary>
    /// 回调地址
    /// </summary>
    public string CallbackUrl { get; set; } = default!;
}
