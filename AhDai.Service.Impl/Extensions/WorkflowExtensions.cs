using AhDai.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhDai.Service.Impl;

internal static class WorkflowExtensions
{
	/// <summary>
	/// 验证目标值类型是否正确
	/// </summary>
	/// <param name="type"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static bool VerifyValueType(this WorkflowFormFieldValueType type, string value)
	{
		ArgumentNullException.ThrowIfNull(type);
		ArgumentException.ThrowIfNullOrEmpty(value);
		return type switch
		{
			WorkflowFormFieldValueType.Integer => long.TryParse(value, out _),
			WorkflowFormFieldValueType.Number => decimal.TryParse(value, out _),
			WorkflowFormFieldValueType.Boolean => bool.TryParse(value, out _),
			_ => true,
		};
	}

}
