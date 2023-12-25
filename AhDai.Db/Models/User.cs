using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 用户
/// </summary>
public partial class User : BaseModel
{
	public string Username { get; set; }

	public string Name { get; set; }

	public int Status { get; set; }
}
