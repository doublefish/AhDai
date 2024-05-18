namespace AhDai.Service.Models;

/// <summary>
/// ValueNameData
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TName"></typeparam>
/// <param name="value"></param>
/// <param name="name"></param>
public class ValueNameData<TValue, TName>(TValue value, TName name)
{
	/// <summary>
	/// 值
	/// </summary>
	public TValue Value { get; set; } = value;
	/// <summary>
	/// 名称
	/// </summary>
	public TName Name { get; set; } = name;
}

/// <summary>
/// ValueNameData
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <param name="value"></param>
/// <param name="name"></param>
public class ValueNameData<TValue>(TValue value, string name)
	: ValueNameData<TValue, string>(value, name)
{

}
