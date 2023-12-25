using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 菜单
/// </summary>
public partial class Menu : BaseModel
{
	public string Name { get; set; }

	public int ParentId { get; set; }

	public int Type { get; set; }

	public string Path { get; set; }

	public string Remark { get; set; }

	public int Status { get; set; }
}
