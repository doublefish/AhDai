using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 用户功能
/// </summary>
public partial class UserFunction : BaseModel
{
	/// <summary>
	/// 用户Id
	/// </summary>
	public int UserId { get; set; }
	/// <summary>
	/// 菜单Id
	/// </summary>
	public int MenuId { get; set; }
	/// <summary>
	/// 功能Id
	/// </summary>
	public int FunctionId { get; set; }
}
