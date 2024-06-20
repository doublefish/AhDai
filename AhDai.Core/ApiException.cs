using AhDai.Core.Attributes;
using Microsoft.OpenApi.Extensions;
using System;

namespace AhDai.Core;

/// <summary>
/// ApiException
/// </summary>
/// <param name="code"></param>
/// <param name="message"></param>
/// <param name="innerException"></param>
public class ApiException(int code, string? message, Exception? innerException) : Exception(message, innerException)
{
    /// <summary>
    /// Code
    /// </summary>
    public int Code { get; protected set; } = code;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public ApiException(int code, string? message) : this(code, message, null)
    {
    }

    /// <summary>
    /// 抛出异常
    /// </summary>
    /// <param name="code"></param>
    /// <param name="args"></param>
    public static ApiException Throw<E>(E code, params object[] args) where E : Enum
    {
        return Throw(null, code, args);
    }

    /// <summary>
    /// 抛出异常
    /// </summary>
    /// <param name="innerException"></param>
    /// <param name="code"></param>
    /// <param name="args"></param>
    public static ApiException Throw<E>(Exception? innerException, E code, params object[] args) where E : Enum
    {
        var attr = code.GetAttributeOfType<ErrorCodeAttribute>();
        var message = attr != null ? string.Format(attr.Message, args) : null;
        return new ApiException(Convert.ToInt32(code), message, innerException);
    }
}
