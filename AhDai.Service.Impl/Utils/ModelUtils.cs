using AhDai.Base.Extensions;
using System;
using System.Collections.Generic;

namespace AhDai.Service.Impl.Utils;

internal static class ModelUtils
{
	/// <summary>
	/// ToString
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	public static string ToString(IEnumerable<long>? values)
	{
		return values != null ? string.Join(',', values) : "";
	}

	/// <summary>
	/// ToLongArray
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	public static long[] ToLongArray(string? values)
	{
		return !string.IsNullOrEmpty(values) ? values.Split(',').ToInt64() : [];
	}

	/// <summary>
	/// ToString
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	public static string ToString(IEnumerable<string>? values)
	{
		return values != null ? string.Join(',', values) : "";
	}

	/// <summary>
	/// ToLongArray
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	public static string[] ToArray(string? values)
	{
		return !string.IsNullOrEmpty(values) ? values.Split(',') : [];
	}

	/// <summary>
	/// ToString
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	public static string ToString(IEnumerable<string[]>? values)
	{
		if (values != null)
		{
			var temps = new List<string>();
			foreach (var temp in values)
			{
				temps.Add(string.Join('|', temp ?? []));
			}
			return string.Join(',', temps);
		}
		return "";
	}

	/// <summary>
	/// ToArray2
	/// </summary>
	/// <param name="values"></param>
	/// <returns></returns>
	public static string[][] ToArray2(string? values)
	{
		if (!string.IsNullOrEmpty(values))
		{
			var list = new List<string[]>();
			var temps = values.Split(',');
			foreach (var temp in temps)
			{
				list.Add(temp.Split('|'));
			}
			return [.. list];
		}
		return [];
	}
}
