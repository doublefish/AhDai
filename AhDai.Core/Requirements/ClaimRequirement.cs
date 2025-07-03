using Microsoft.AspNetCore.Authorization;

namespace AhDai.Core.Requirements;

/// <summary>
/// ClaimRequirement
/// </summary>
/// <param name="claimType"></param>
/// <param name="claimValue"></param>
public class ClaimRequirement(string claimType, string claimValue) : IAuthorizationRequirement
{
    /// <summary>
    /// 要验证的Claim类型
    /// </summary>
    public string ClaimType { get; } = claimType;

    /// <summary>
    /// 要验证的Claim值
    /// </summary>
    public string ClaimValue { get; } = claimValue;
}
