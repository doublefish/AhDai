using Microsoft.AspNetCore.Authorization;

namespace AhDai.Core.Requirements;

/// <summary>
/// PermissionRequirement
/// </summary>
/// <param name="permissionType"></param>
/// <param name="permissionValue"></param>
/// <param name="message"></param>
public class PermissionRequirement(string permissionType, string permissionValue, string message) : IAuthorizationRequirement
{
    /// <summary>
    /// 要验证的权限类型
    /// </summary>
    public string PermissionType { get; } = permissionType;
    /// <summary>
    /// 要验证的权限值
    /// </summary>
    public string PermissionValue { get; } = permissionValue;
    /// <summary>
    /// 错误消息
    /// </summary>
    public string Message { get; } = message;
}
