using System.Reflection;

namespace AhDai.Core.Metadata;

/// <summary>
/// 属性元数据封装
/// </summary>
public class PropertyMetadata(PropertyInfo info)
{
    /// <summary>
    /// PropertyInfo
    /// </summary>
    public PropertyInfo Info { get; set; } = info;
    /// <summary>
    /// 预解析常用的 JsonPropertyName
    /// </summary>
    public string? JsonName { get; set; }
}
