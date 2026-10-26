namespace AhDai.Core.Models.Authorization;

/// <summary>
/// AccessKey 访问凭据
/// </summary>
/// <param name="AccessKeyId">标识</param>
/// <param name="AccessKeySecret">明文密钥</param>
public sealed record AccessKeyCredential(string AccessKeyId, string AccessKeySecret)
{
    /// <summary>
    /// 标识
    /// </summary>
    public string AccessKeyId { get; init; } = AccessKeyId;
    /// <summary>
    /// 密钥
    /// </summary>
    public string AccessKeySecret { get; init; } = AccessKeySecret;

    /// <summary>
    /// 避免输出明文密钥
    /// </summary>
    public override string ToString() => $"{nameof(AccessKeyCredential)} {{ AccessKeyId = {AccessKeyId}, AccessKeySecret = [REDACTED] }}";
}
