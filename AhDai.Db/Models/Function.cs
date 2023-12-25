using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 功能
/// </summary>
public partial class Function : BaseModel
{
	public int MenuId { get; set; }

	public string Name { get; set; }

	public string Remark { get; set; }

	public int Status { get; set; }
}
