using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models;

/// <summary>
/// TwcNotaryTransCreateInput
/// </summary>
public class TwcNotaryTransCreateInput : BaseTwcNotaryInput
{
    /// <summary>
    /// 存证关联实体（个人/企业）的身份识别信息
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("customer")]
    public TwcNotaryTransIdentityInput Customer { get; set; } = default!;
    /// <summary>
    /// 扩展属性，长度不超过4096
    /// </summary>
    [JsonPropertyName("properties")]
    public string? Properties { get; set; }

}
