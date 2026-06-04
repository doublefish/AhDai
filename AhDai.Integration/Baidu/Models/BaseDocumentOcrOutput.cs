using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BaseFinancialOcrOutput
/// </summary>
/// <typeparam name="TWordsResult"></typeparam>
public abstract class BaseDocumentOcrOutput<TWordsResult> : BasePdfOcrOutput<TWordsResult>, IBaseOutput
{
    /// <summary>
    /// 传入OFD文件的总页数，当 ofd_file 参数有效时返回该字段
    /// </summary>
    [JsonPropertyName("ofd_file_size")]
    public virtual int? OfdFileSize { get; set; }
}
