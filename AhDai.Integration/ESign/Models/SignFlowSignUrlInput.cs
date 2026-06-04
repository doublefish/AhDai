using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 签署页面链接
/// </summary>
public class SignFlowSignUrlInput
{
    /// <summary>
    /// 是否需要登录打开链接（默认值 false）
    /// </summary>
    [JsonPropertyName("needLogin")]
    public bool? NeedLogin { get; set; }
    /// <summary>
    /// 链接类型（默认值 2）
    /// 1 - 预览链接（仅限查看，不能签署），2 - 签署链接
    /// </summary>
    [JsonPropertyName("urlType")]
    public int? UrlType { get; set; }
    /// <summary>
    /// 个人签署方（机构签署传经办人信息）
    /// 当获取签署链接场景，需传入当前流程流转到的签署操作人信息。
    /// psnAccount与psnId二选一传入（必须与发起签署时的账号保持一致）
    /// </summary>
    [JsonPropertyName("operator")]
    public PersonAccountInput Operator { get; set; } = default!;
    /// <summary>
    /// 重定向配置项
    /// </summary>
    [JsonPropertyName("redirectConfig")]
    public RedirectConfigInput? RedirectConfig { get; set; }
    /// <summary>
    /// 指定客户端类型，当urlType为2（签署链接）时生效
    /// H5 - 移动端适配
    /// PC - PC端适配
    /// ALL - 自动适配移动端或PC端（默认值）
    /// </summary>
    [JsonPropertyName("clientType")]
    public string? ClientType { get; set; }
    /// <summary>
    /// AppScheme，主要用于支付宝人脸认证重定向时跳回开发者自身App。
    /// 示例值：esign://demo/signBack
    /// </summary>
    [JsonPropertyName("appScheme")]
    public string? AppScheme { get; set; }
}
