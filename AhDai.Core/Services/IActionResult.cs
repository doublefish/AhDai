namespace AhDai.Core.Services;

/// <summary>
/// IActionResult
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IActionResult<T> : Microsoft.AspNetCore.Mvc.IActionResult
{
    /// <summary>
    /// 动作Id
    /// </summary>
    string ActionId { get; set; }
    /// <summary>
    /// 状态代码
    /// </summary>
    int Code { get; set; }
    /// <summary>
    /// 消息
    /// </summary>
    string Message { get; set; }
    /// <summary>
    /// 结果
    /// </summary>
    T Content { get; set; }
    /// <summary>
    /// 内容类型
    /// </summary>
    string ContentType { get; set; }
}
