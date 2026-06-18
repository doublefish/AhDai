using System;

namespace AhDai.Core.Attributes;

/// <summary>
/// AllowPermissionBypassAttribute
/// </summary>
/// <param name="permissionTypes"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AllowPermissionBypassAttribute(params string[] permissionTypes) : Attribute
{
    /// <summary>
    /// 允许跳过的特定权限类型。
    /// 如果为空，则跳过所有权限验证需求。
    /// </summary>
    public string[]? PermissionTypes { get; } = permissionTypes;
}
