using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AhDai.WebApi;

/// <summary>
/// ApiGroupName
/// </summary>
public static class ApiGroupName
{
    /// <summary>
    /// 账号
    /// </summary>
    [Description("账号")]
    public const string Account = "account";
    /// <summary>
    /// 全部
    /// </summary>
    [Description("全部")]
    public const string All = "all";


    static Dictionary<string, string>? _dict;

    /// <summary>
    /// 获取所有
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> Get()
    {
        if (_dict != null) return _dict;

        _dict = typeof(ApiGroupName)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && f.FieldType == typeof(string))
            .ToDictionary(k => k.GetRawConstantValue()?.ToString() ?? k.Name, v => v.GetCustomAttribute<DescriptionAttribute>()?.Description ?? v.Name);

        return _dict;
    }
}
