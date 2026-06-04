using System.Text.Json.Serialization;

namespace AhDai.Integration.WeChat.Models;

/// <summary>
/// 创建二维码入参
/// </summary>
public class QrCodeCreateActionInfoInput
{
    /// <summary>
    /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
    /// </summary>
    [JsonPropertyName("scene_id")]
    public int SceneId { get; set; }
    /// <summary>
    /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64
    /// </summary>
    [JsonPropertyName("scene_str")]
    public string SceneStr { get; set; } = default!;

}
