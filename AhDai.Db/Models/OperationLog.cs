using System;
using System.Collections.Generic;

namespace AhDai.Db.Models;

/// <summary>
/// 操作日志
/// </summary>
public partial class OperationLog : BaseModel
{
	public string MenuName { get; set; }

	public string FunctionName { get; set; }

	public string ApiUrl { get; set; }

	public string Type { get; set; }

	public string Content { get; set; }
}
