using System.Text.Json.Serialization;

namespace AhDai.Integration.Aliyun.Models.Vod;

/// <summary>
/// GetPlayInfoInput
/// </summary>
public class GetPlayInfoInput
{
    /// <summary>
    /// 音/视频 ID
    /// </summary>
    [JsonRequired]
    public string VideoId { get; set; } = default!;
    /// <summary>
    /// 媒体流格式。多个格式之间使用半角逗号（,）分隔。取值：mp4、m3u8、mp3、flv、mpd
    /// </summary>
    public string? Formats { get; set; }
    /// <summary>
    /// 设置播放地址的有效时间。单位：秒。
    /// </summary>
    public long? AuthTimeout { get; set; }
    /// <summary>
    /// 输出地址类型。取值：
    /// oss：回源地址。
    /// cdn（默认）：加速地址。
    /// </summary>
    public string? OutputType { get; set; }
    /// <summary>
    /// 媒体流类型。多个类型之间用半角逗号（,）分隔。支持类型：
    /// video：视频。
    /// audio：音频。
    /// 默认获取所有类型的流。
    /// </summary>
    public string? StreamType { get; set; }
    /// <summary>
    /// CDN 二次鉴权参数，为 JSON 字符串。当开启了 URL 鉴权的 A 方式鉴权功能时，可通过该参数设置鉴权 URL 的uid和rand
    /// </summary>
    public string? ReAuthInfo { get; set; }
    /// <summary>
    /// 视频流清晰度。多个清晰度之间用半角逗号（,）分隔。取值：
    /// 默认获取所有清晰度的流。
    /// </summary>
    public string? Definition { get; set; }
    /// <summary>
    /// 返回数据类型。取值：
    /// Single（默认）：每种清晰度和格式只返回一路最新转码完成的流。
    /// Multiple：每种清晰度和格式返回所有转码完成的流。
    /// </summary>
    public string? ResultType { get; set; }
    /// <summary>
    /// 播放自定义设置。为 JSON 字符串，支持指定域名播放设置。
    /// </summary>
    public string? PlayConfig { get; set; }
    /// <summary>
    /// 获取弹幕蒙版数据 URL 地址，取值：danmu。
    /// 仅当outputType取值为cdn时才会生效。
    /// </summary>
    public string? AdditionType { get; set; }
    /// <summary>
    /// 用户自定义的数字水印信息。
    /// </summary>
    public string? Trace { get; set; }
    /// <summary>
    /// 数字水印类型。取值：
    /// TraceMark：溯源水印。
    /// CopyrightMark：版权水印。
    /// </summary>
    public string? DigitalWatermarkType { get; set; }
}
