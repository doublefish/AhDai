using System;

namespace AhDai.Core.Attributes;

/// <summary>
/// ErrorCodeAttribute
/// </summary>
/// <param name="message"></param>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ErrorCodeAttribute(string message) : Attribute
{
    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; private set; } = message;
}
