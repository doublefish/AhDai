using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 角色功能
/// </summary>
public partial class RoleFunction : BaseModel
{
	public int RoleId { get; set; }

	public int MenuId { get; set; }

	public int FunctionId { get; set; }
}
