namespace AhDai.Entity;

/// <summary>
/// IBaseBidEntity
/// </summary>
public interface IBaseBidEntity
{
	/// <summary>
	/// 项目Id
	/// </summary>
	public long BidId { get; set; }
}

/// <summary>
/// IBaseNullableBidEntity
/// </summary>
public interface IBaseNullableBidEntity
{
	/// <summary>
	/// 项目Id
	/// </summary>
	public long? BidId { get; set; }
}