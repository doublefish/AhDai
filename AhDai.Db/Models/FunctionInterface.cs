using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 功能接口
/// </summary>
public partial class FunctionInterface : BaseModel
{
	public int FunctionId { get; set; }

	public int InterfaceId { get; set; }
}
