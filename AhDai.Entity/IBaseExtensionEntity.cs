namespace AhDai.Entity;

/// <summary>
/// 扩展
/// </summary>
public interface IBaseExtensionEntity : IBaseTenantEntity
{
	/// <summary>
	/// 主表Id
	/// </summary>
	public long MainId { get; set; }
	/// <summary>
	/// 类型
	/// </summary>
	public int Type { get; set; }
	/// <summary>
	/// 数字数据
	/// </summary>
	public long? DigitalData { get; set; }
	/// <summary>
	/// 数字数据
	/// </summary>
	public long? DigitalData1 { get; set; }
	/// <summary>
	/// 数字数据
	/// </summary>
	public long? DigitalData2 { get; set; }
	/// <summary>
	/// 文本数据
	/// </summary>
	public string? TextData { get; set; }
	/// <summary>
	/// 文本数据
	/// </summary>
	public string? TextData1 { get; set; }
	/// <summary>
	/// 文本数据
	/// </summary>
	public string? TextData2 { get; set; }
}
