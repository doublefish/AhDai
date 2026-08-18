namespace AhDai.Core.Models;

/// <summary>
/// ValidationError
/// </summary>
public class ValidationError
{
    /// <summary>
    /// 字段名
    /// </summary>
    public required string Field { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    public required string Message { get; set; }
}
