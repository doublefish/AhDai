using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// WeChatUserOutput
/// </summary>
public class UserOutput : BaseOutput
{
    /// <summary>
    /// OpenId
    /// </summary>
    [JsonPropertyName("openid")]
    public string OpenId { get; set; } = default!;
    /// <summary>
    /// 用户昵称
    /// </summary>
    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = default!;
    /// <summary>
    /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
    /// </summary>
    [JsonPropertyName("sex")]
    public int Sex { get; set; }
    /// <summary>
    /// 国家，如中国为CN
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; } = default!;
    /// <summary>
    /// 用户个人资料填写的省份
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; } = default!;
    /// <summary>
    /// 用户个人资料填写的城市
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = default!;
    /// <summary>
    /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），
    /// 用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
    /// </summary>
    [JsonPropertyName("headimgurl")]
    public string? HeadImgUrl { get; set; }
    /// <summary>
    /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
    /// </summary>
    [JsonPropertyName("privilege")]
    public string[]? Privilege { get; set; }
    /// <summary>
    /// UnionId，只有在用户将公众号绑定到微信开放平台账号后，才会出现该字段
    /// </summary>
    [JsonPropertyName("unionid")]
    public string? UnionId { get; set; }
}
