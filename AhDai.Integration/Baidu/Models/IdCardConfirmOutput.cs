using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// 用户二次确认的身份证信息
/// </summary>
public class IdCardConfirmOutput
{
    /// <summary>
    /// 姓名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 身份证号
    /// </summary>
    [JsonPropertyName("idcard_number")]
    public string IdCardNumber { get; set; } = default!;
}
