namespace AhDai.Integration.Aliyun.Models.Oss;

internal class SignOutput
{
    /// <summary>
    /// 额外参与签名的头
    /// </summary>
    public string AdditionalHeaders { get; set; } = default!;
    /// <summary>
    /// 签名
    /// </summary>
    public string Signature { get; set; } = default!;
}
