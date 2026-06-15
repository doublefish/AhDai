using System.Text.Json.Serialization;

namespace AhDai.Integration.AntChain.Models.Notary;

/// <summary>
/// TransCreateInput
/// </summary>
public class TransCreateInput : BaseInput
{
    /// <summary>
    /// 存证关联实体（个人/企业）的身份识别信息
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("customer")]
    public TransIdentityInput Customer { get; set; } = default!;
    /// <summary>
    /// 扩展属性，长度不超过4096
    /// </summary>
    [JsonPropertyName("properties")]
    public string? Properties { get; set; }

}
