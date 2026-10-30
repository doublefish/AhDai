using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models.OfficialAccount;

/// <summary>
/// 模板消息小程序跳转配置
/// </summary>
/// <param name="AppId"></param>
/// <param name="PagePath"></param>
public record TemplateMessageMiniProgram(string AppId, string PagePath)
{
    /// <summary>
    /// AppId
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("appid")]
    public string AppId { get; init; } = AppId;
    /// <summary>
    /// 页面路径
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("pagepath")]
    public string PagePath { get; init; } = PagePath;
}
