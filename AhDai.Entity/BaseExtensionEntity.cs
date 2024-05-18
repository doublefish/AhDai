namespace AhDai.Entity;

/// <summary>
/// 扩展
/// </summary>
public class BaseExtensionEntity : BaseTenantEntity, IBaseExtensionEntity
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

	/// <summary>
	/// 构造函数
	/// </summary>
	public BaseExtensionEntity()
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="mainId"></param>
	/// <param name="type"></param>
	public BaseExtensionEntity(long mainId, int type)
	{
		MainId = mainId;
		Type = type;
	}
}
