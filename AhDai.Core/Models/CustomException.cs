using System;

namespace AhDai.Core.Models;

/// <summary>
/// CustomException
/// </summary>
public class CustomException : Exception
{
    /// <summary>
    /// Code
    /// </summary>
    public int Code { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="code">建议使用1000-2000之间的值</param>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public CustomException(int code, string? message, Exception? innerException = null) : base(message, innerException)
    {
        Code = code;
    }
}
