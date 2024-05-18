using AhDai.Service.Models;

namespace AhDai.Service.Sys.Models;

/// <summary>
/// 用户
/// </summary>
public class UserExistInput : BaseExistInput
{
	/// <summary>
	/// 用户名
	/// </summary>
	public string? Username { get; set; }
	/// <summary>
	/// 手机号码
	/// </summary>
	public string? MobilePhone { get; set; }
}
