using System.Reflection;
using System.Text.Json.Serialization;

namespace AhDai.Core.Models;

/// <summary>
/// 属性元数据封装
/// </summary>
/// <param name="info"></param>
public class PropertyMetadata(PropertyInfo info)
{
    /// <summary>
    /// PropertyInfo
    /// </summary>
    public PropertyInfo Info { get; } = info;
    /// <summary>
    /// 预解析常用的 JsonPropertyName
    /// </summary>
    public string? JsonName { get; } = info.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
}
