using System;

namespace AhDai.Core.Attributes;

/// <summary>
/// AllowClaimBypassAttribute
/// </summary>
/// <param name="claimTypes"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AllowClaimBypassAttribute(params string[] claimTypes) : Attribute
{
    /// <summary>
    /// 允许跳过的特定 Claim 类型。
    /// 如果为 null 或空，则跳过所有 Claim 验证需求。
    /// </summary>
    public string[]? ClaimTypes { get; set; } = claimTypes;
}
