namespace AhDai.Integration.Aliyun.Models;

/// <summary>
/// GetPlayInfoOutput
/// </summary>
public class GetPlayInfoOutput
{
    /// <summary>
    /// 请求ID
    /// </summary>
    public string RequestId { get; set; } = default!;
    /// <summary>
    /// 音/视频基本信息。
    /// </summary>
    public object VideoBase { get; set; } = default!;
    /// <summary>
    /// 音/视频播放信息（流信息）。
    /// </summary>
    public object PlayInfoList { get; set; } = default!;
}
