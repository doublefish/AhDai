namespace AhDai.Service;

/// <summary>
/// 组织查询入参
/// </summary>
public interface IOrgIdQueryInput
{
    /// <summary>
    /// 组织Id
    /// </summary>
    public long? OrgId { get; set; }
    /// <summary>
    /// 组织名称
    /// </summary>
    public string? OrgName { get; set; }
}
