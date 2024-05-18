using System.ComponentModel.DataAnnotations;

namespace AhDai.Shared.Consts;

/// <summary>
/// 流程流转条件比较器
/// </summary>
public class WorkflowTransitionConditionComparer
{
	/// <summary>
	/// 等于
	/// </summary>
	[Display(Name = "等于")]
	public const string Equal = "=";
	/// <summary>
	/// 不等于
	/// </summary>
	[Display(Name = "不等于")]
	public const string NotEqual = "!=";
	/// <summary>
	/// 大于
	/// </summary>
	[Display(Name = "大于")]
	public const string GreaterThan = ">";
	/// <summary>
	/// 大于等于
	/// </summary>
	[Display(Name = "大于等于")]
	public const string GreaterThanOrEqual = ">=";
	/// <summary>
	/// 小于
	/// </summary>
	[Display(Name = "小于")]
	public const string LessThan = "<";
	/// <summary>
	/// 小于等于
	/// </summary>
	[Display(Name = "小于等于")]
	public const string LessThanOrEqual = "<=";
}
