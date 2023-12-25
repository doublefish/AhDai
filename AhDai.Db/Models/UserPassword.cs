using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 用户密码
/// </summary>
public partial class UserPassword : BaseModel
{
	public int UserId { get; set; }

	public string Password { get; set; }
}
