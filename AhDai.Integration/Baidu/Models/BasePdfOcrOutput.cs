using AhDai.Integration.Abstractions;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models;

/// <summary>
/// BasePdfOcrOutput
/// </summary>
/// <typeparam name="TWordsResult"></typeparam>
public abstract class BasePdfOcrOutput<TWordsResult> : BaseOcrOutput<TWordsResult>, IBaseOutput
{
    /// <summary>
    /// 传入PDF文件的总页数，当 pdf_file 参数有效时返回该字段
    /// </summary>
    [JsonPropertyName("pdf_file_size")]
    public virtual int? PdfFileSize { get; set; }
}
