namespace AhDai.Core.Models;

/// <summary>
/// ValidationError
/// </summary>
public class ValidationError
{
    /// <summary>
    /// 字段名
    /// </summary>
    public string Field { get; set; } = null!;
    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; set; } = null!;
}
