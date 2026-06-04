namespace AhDai.Integration.Models;

/// <summary>
/// 通用文字识别
/// </summary>
public class OcrAccurateBasicFriendlyOutput : BaseOcrFriendlyOutput
{
    /// <summary>
    /// 回单编号
    /// </summary>
    public string[]? Words { get; set; }
}
