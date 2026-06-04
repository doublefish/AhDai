namespace AhDai.Integration.ESign.Models;

/// <summary>
/// FileUploadOutput
/// </summary>
public class FileUploadOutput
{
    /// <summary>
    /// 业务码，0表示成功，非0表示异常
    /// </summary>
    public int ErrCode { get; set; }
    /// <summary>
    /// 业务信息
    /// </summary>
    public string Msg { get; set; } = default!;
}
