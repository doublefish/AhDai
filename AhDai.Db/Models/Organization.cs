using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 组织
/// </summary>
public partial class Organization : BaseModel
{
	public string Code { get; set; }

	public string Name { get; set; }

	public int ParentId { get; set; }

	public int Level { get; set; }

	public int Type { get; set; }

	public string Remark { get; set; }

	public int Status { get; set; }
}
