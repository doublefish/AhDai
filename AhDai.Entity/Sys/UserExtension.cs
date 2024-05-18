namespace AhDai.Entity.Sys;

/// <summary>
/// 用户扩展
/// </summary>
public class UserExtension : BaseTenantEntity
{
	/// <summary>
	/// 用户Id
	/// </summary>
	public long UserId { get; set; }
	/// <summary>
	/// 微信OpenId
	/// </summary>
	public string? WeChatOpenId { get; set; } 
    /// <summary>
    /// ErpId
    /// </summary>
    public string? ErpUserId { get; set; }
    /// <summary>
    /// 乐筑一站Id
    /// </summary>
    public string? LzezUserId { get; set; }
}
