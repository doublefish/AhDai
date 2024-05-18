using AhDai.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace AhDai.Service.Impl;

internal static class ValueNameDataExtensions
{
	/// <summary>
	/// ToValueNameData
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="includeValues"></param>
	/// <param name="excludeValues"></param>
	/// <returns></returns>
	public static ValueNameData<T>[] FromEnum<T>(T[]? includeValues = null, T[]? excludeValues = null) where T : Enum
	{
		var type = typeof(T);
		var fields = type.GetFields();
		var list = new List<ValueNameData<T>>();
		foreach (var field in fields)
		{
			if (!field.IsLiteral) continue;
			var obj = field.GetValue(null);
			if (obj == null) continue;
			var value = (T)obj;
			if (includeValues != null && !includeValues.Contains(value)) continue;
			if (excludeValues != null && excludeValues.Contains(value)) continue;
			var attribute = field.GetCustomAttribute<DisplayAttribute>();
			list.Add(new ValueNameData<T>(value, attribute?.Name ?? value.ToString()));
		}
		return [.. list];
	}

	/// <summary>
	/// ToValueNameData
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="includeValues"></param>
	/// <param name="excludeValues"></param>
	/// <returns></returns>
	public static ValueNameData<string>[] FromClass<T>(string[]? includeValues = null, string[]? excludeValues = null) where T : class
	{
		var type = typeof(T);
		var fields = type.GetFields();
		var list = new List<ValueNameData<string>>();
		foreach (var field in fields)
		{
			if (!field.IsLiteral) continue;
			var obj = field.GetValue(null);
			if (obj == null) continue;
			var value = obj.ToString();
			if (string.IsNullOrEmpty(value)) continue;
			if (includeValues != null && !includeValues.Contains(value)) continue;
			if (excludeValues != null && excludeValues.Contains(value)) continue;
			var attribute = field.GetCustomAttribute<DisplayAttribute>();
			list.Add(new ValueNameData<string>(value, attribute?.Name ?? value));
		}
		return [.. list];
	}
}
