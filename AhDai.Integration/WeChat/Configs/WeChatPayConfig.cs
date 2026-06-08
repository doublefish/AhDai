using AhDai.Integration.Attributes;
using AhDai.Integration.Models;

namespace AhDai.Integration.WeChat.Configs;

/// <summary>
/// WeChatPayConfig
/// </summary>
public class WeChatPayConfig : BaseConfig
{
    /// <summary>
    /// AppId
    /// </summary>
    public string AppId { get; set; } = default!;
    /// <summary>
    /// AppSecret
    /// </summary>
    [Sensitive]
    public string AppSecret { get; set; } = default!;
    /// <summary>
    /// 商户Id
    /// </summary>
    public string MchId { get; set; } = default!;
    /// <summary>
    /// Api密钥
    /// </summary>
    [Sensitive]
    public string ApiKey { get; set; } = default!;
    /// <summary>
    /// 商户证书序列号
    /// </summary>
    public string MchSerialNo { get; set; } = default!;
    /// <summary>
    /// 商户证书证书地址
    /// </summary>
    public string MchCertPath => $"certs/wechatpay/{MchSerialNo}/wechatpay_apiclient_cert.pem";
    /// <summary>
    /// 商户证书私钥地址
    /// </summary>
    public string MchKeyPath => $"certs/wechatpay/{MchSerialNo}/wechatpay_apiclient_key.pem";
    /// <summary>
    /// 平台证书序列号
    /// </summary>
    public string SerialNo { get; set; } = default!;
    /// <summary>
    /// 平台证书地址
    /// </summary>
    public string CertPath => $"certs/wechatpay/{SerialNo}/wechatpay_cert.pem";
    /// <summary>
    /// NotifyUrl
    /// </summary>
    public string NotifyUrl { get; set; } = default!;
}
