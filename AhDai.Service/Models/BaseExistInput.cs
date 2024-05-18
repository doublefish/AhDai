using System.ComponentModel.DataAnnotations;

namespace AhDai.Service.Models;

/// <summary>
/// BaseExistInput
/// </summary>
public class BaseExistInput
{

}

/// <summary>
/// CodeExistInput
/// </summary>
public class CodeExistInput : BaseExistInput
{
	/// <summary>
	/// 编码
	/// </summary>
	[Required]
	public string Code { get; set; } = "";
}
