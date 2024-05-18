namespace AhDai.Entity;

/// <summary>
/// IUniqueCodeEntity
/// </summary>
public interface IUniqueCodeEntity: IParentIdEntity
{
	/// <summary>
	/// 层级
	/// </summary>
	public int Level { get; set; }
	/// <summary>
	/// 序号
	/// </summary>
	public int Number { get; set; }
	/// <summary>
	/// 唯一编码
	/// </summary>
	public string UniqueCode { get; set; }

}
