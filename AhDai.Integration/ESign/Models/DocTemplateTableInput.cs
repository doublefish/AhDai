using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 模板表格
/// </summary>
public class DocTemplateTableInput
{
    /// <summary>
    /// insertRow
    /// </summary>
    [JsonPropertyName("insertRow")]
    public string? InsertRow { get; set; }
    /// <summary>
    /// row
    /// </summary>
    [JsonPropertyName("row")]
    public DocTemplateTableRowInput Row { get; set; } = default!;
}
