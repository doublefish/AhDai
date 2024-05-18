namespace AhDai.Entity;


/// <summary>
/// BaseTenantEntity
/// </summary>
public class BaseTenantEntity : BaseEntity, IBaseTenantEntity
{
	/// <summary>
	/// 租户Id
	/// </summary>
	public long TenantId { get; set; }
}
