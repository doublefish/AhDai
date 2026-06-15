using System;
using System.Text.Json.Serialization;

namespace AhDai.Integration.Baidu.Models.Ocr;

/// <summary>
/// BaseFinancialOcrInput
/// </summary>
public abstract class BaseDocumentInput : BasePdfInput
{
    /// <summary>
    /// OFD文件，base64编码后进行urlencode，要求base64编码和urlencode后大小不超过8M，最短边至少15px，最长边最大4096px
    /// 优先级：image > url > pdf_file > ofd_file，当image、url、pdf_file字段存在时，ofd_file字段失效
    /// </summary>
    [JsonPropertyName("ofd_file")]
    public virtual string? OfdFile { get; set; }
    /// <summary>
    /// 需要识别的OFD文件的对应页码，当 ofd_file 参数有效时，识别传入页码的对应页面内容，若不传入，则默认识别第 1 页
    /// </summary>
    [JsonPropertyName("ofd_file_num")]
    public virtual int? OfdFileNum { get; set; }

    /// <summary>
    /// 设置文件内容
    /// </summary>
    /// <param name="fileString"></param>
    /// <param name="fileType">1-Image，2-PdfFile，3-OfdFile</param>
    public override void SetFile(string fileString, int fileType)
    {
        switch (fileType)
        {
            case 1:
                Image = fileString;
                break;
            case 2:
                PdfFile = fileString;
                break;
            case 3:
                OfdFile = fileString;
                break;
            default: throw new ArgumentException($"无法识别文件类型：{fileType}");
        }
    }
}
