using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// TicketOutput
/// </summary>
public class TicketOutput : BaseOutput
{
    /// <summary>
    /// 凭据
    /// </summary>
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; } = default!;
    /// <summary>
    /// 凭据有效时间，单位：秒
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

}
