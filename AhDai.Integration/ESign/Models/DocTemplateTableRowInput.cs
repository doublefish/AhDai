using System.Text.Json.Serialization;

namespace AhDai.Integration.ESign.Models;

/// <summary>
/// 模板表格行
/// </summary>
public class DocTemplateTableRowInput
{
    /// <summary>
    /// column1
    /// </summary>
    [JsonPropertyName("column1")]
    public string Column1 { get; set; } = default!;
    /// <summary>
    /// column2
    /// </summary>
    [JsonPropertyName("column2")]
    public string? Column2 { get; set; }
    /// <summary>
    /// column3
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column3")]
    public string? Column3 { get; set; }
    /// <summary>
    /// column4
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column4")]
    public string? Column4 { get; set; }
    /// <summary>
    /// column5
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column5")]
    public string? Column5 { get; set; }
    /// <summary>
    /// column6
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column6")]
    public string? Column6 { get; set; }
    /// <summary>
    /// column7
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column7")]
    public string? Column7 { get; set; }
    /// <summary>
    /// column8
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column8")]
    public string? Column8 { get; set; }
    /// <summary>
    /// column9
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("column9")]
    public string? Column9 { get; set; }
}
