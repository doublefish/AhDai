using AhDai.Service.Models;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 字典
/// </summary>
public class DictSimpleOutput(string value, string name)
    : ValueNameData<string, string>(value, name)
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = "";
    /// <summary>
    /// 子节点
    /// </summary>
    public DictSimpleOutput[]? Children { get; set; }
}
