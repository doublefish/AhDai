namespace AhDai.Integration.Aliyun.Models.Oss;

/// <summary>
/// PolicyTokenOutput
/// </summary>
public class PolicyTokenOutput
{
    /// <summary>
    /// AccessKeyId
    /// </summary>
    public string AccessKeyId { get; set; } = default!;
    /// <summary>
    /// Host
    /// </summary>
    public string Host { get; set; } = default!;
    /// <summary>
    /// Policy
    /// </summary>
    public string Policy { get; set; } = default!;
    /// <summary>
    /// Signature
    /// </summary>
    public string Signature { get; set; } = default!;
    /// <summary>
    /// Callback
    /// </summary>
    public string Callback { get; set; } = default!;
}
