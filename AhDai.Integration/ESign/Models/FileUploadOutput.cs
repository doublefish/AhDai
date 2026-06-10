using AhDai.Integration.Abstractions;
using System;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// FileUploadOutput
/// </summary>
public class FileUploadOutput : IBaseOutput
{
    /// <summary>
    /// 业务码，0表示成功，非0表示异常
    /// </summary>
    public int ErrCode { get; set; }
    /// <summary>
    /// 业务信息
    /// </summary>
    public string Msg { get; set; } = default!;

    /// <summary>
    /// 确保结果
    /// </summary>
    public virtual void EnsureResult()
    {
        if (ErrCode != 0) throw new Exception($"请求电子签发生异常：[{ErrCode}]{Msg}，请联系管理员");
    }
}
