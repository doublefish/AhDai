using System;
using System.Collections.Generic;
using System.Net.Http;

namespace AhDai.Integration.Aliyun.Models.Oss;

internal class SignInput
{
    /// <summary>
    /// Method
    /// </summary>
    public HttpMethod Method { get; set; } = default!;
    /// <summary>
    /// Date
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Region
    /// </summary>
    public string Region { get; set; } = default!;
    /// <summary>
    /// Bucket
    /// </summary>
    public string Bucket { get; set; } = default!;
    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; set; } = default!;
    /// <summary>
    /// Query
    /// </summary>
    public Dictionary<string, string>? Query { get; set; }
    /// <summary>
    /// Headers
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
    /// <summary>
    /// 额外参与签名的头
    /// </summary>
    public string[]? AdditionalHeaders { get; set; }
    /// <summary>
    /// 签名版本
    /// </summary>
    public string Version { get; set; } = default!;
    /// <summary>
    /// 凭证
    /// </summary>
    public string Credential { get; set; } = default!;
}
