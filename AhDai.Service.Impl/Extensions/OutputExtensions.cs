using AhDai.Service.Models;
using AhDai.Service.Sys.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Service.Impl;

/// <summary>
/// OutputExtensions
/// </summary>
internal static class OutputExtensions
{
	/// <summary>
	/// 设置关联基础数据 
	/// </summary>
	/// <param name="output"></param>
	/// <param name="users"></param>
	public static void SetBaseInfo<T>(this T output, UserOutput[] users) where T : IBaseOutput
	{
		var creator = users.Where(x => x.Id == output.CreatorId).FirstOrDefault();
		output.CreatorName = creator?.Name ?? "";
		if (output.ModifierId.HasValue)
		{
			var modifier = users.Where(x => x.Id == output.ModifierId.Value).FirstOrDefault();
			output.ModifierName = modifier?.Name ?? "";
		}
	}

	/// <summary>
	/// 设置关联基础数据 
	/// </summary>
	/// <param name="output"></param>
	/// <param name="users"></param>
	public static void SetBaseInfo<T>(this T output, UserOutput[] users, FileOutput[] files) where T : IAttachmentOutput, IBaseOutput
	{
		output.SetBaseInfo(users);
		if (output.AttachmentIds != null && output.AttachmentIds.Length > 0)
		{
			output.Attachments = files.Where(x => output.AttachmentIds.Contains(x.Id)).ToArray();
		}
	}

	/// <summary>
	/// 最低级别的下级（含本级）
	/// </summary>
	/// <param name="list"></param>
	/// <returns></returns>
	public static DictSimpleOutput[] GetLowestSubordinates(this DictSimpleOutput item)
	{
		var children = new List<DictSimpleOutput>();
		if (item.Children != null && item.Children.Length > 0)
		{
			children.AddRange(item.Children.GetLowestSubordinates());
		}
		else
		{
			children.Add(item);
		}
		return [.. children];
	}

	/// <summary>
	/// 最低级别的下级（含本级）
	/// </summary>
	/// <param name="list"></param>
	/// <returns></returns>
	public static DictSimpleOutput[] GetLowestSubordinates(this IEnumerable<DictSimpleOutput> list)
	{
		var children = new List<DictSimpleOutput>();
		foreach (var item in list)
		{
			if (item.Children == null || item.Children.Length == 0)
			{
				children.Add(item);
			}
			else
			{
				var temps = GetLowestSubordinates(item.Children);
				children.AddRange(temps);
			}
		}
		return [.. children];
	}

	/// <summary>
	/// ToTreeArray
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <returns></returns>
	public static T[] ToTreeArray<T>(this IEnumerable<T> list) where T : ITreeOutput<T>
	{
		var dict = new Dictionary<long, ICollection<T>>();
		foreach (var data in list)
		{
			if (!dict.TryGetValue(data.ParentId, out var children))
			{
				children = new List<T>();
				dict.Add(data.ParentId, children);
			}
			children.Add(data);
		}
		foreach (var data in list)
		{
			data.Children = dict.GetValueOrDefault(data.Id)?.ToArray();
		}
		return [.. dict[0]];
	}

}
