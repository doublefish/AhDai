namespace AhDai.Entity;

/// <summary>
/// IBaseProjectEntity
/// </summary>
public interface IBaseProjectEntity
{
	/// <summary>
	/// 项目Id
	/// </summary>
	public long ProjectId { get; set; }
}

/// <summary>
/// IBaseNullableProjectEntity
/// </summary>
public interface IBaseNullableProjectEntity
{
	/// <summary>
	/// 项目Id
	/// </summary>
	public long? ProjectId { get; set; }
}