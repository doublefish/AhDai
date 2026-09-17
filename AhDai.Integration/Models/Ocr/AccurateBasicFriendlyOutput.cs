namespace AhDai.Integration.Models.Ocr;

/// <summary>
/// 通用文字识别
/// </summary>
public class AccurateBasicFriendlyOutput : BaseFriendlyOutput
{
    /// <summary>
    /// 回单编号
    /// </summary>
    public string[]? Words { get; set; }
}
