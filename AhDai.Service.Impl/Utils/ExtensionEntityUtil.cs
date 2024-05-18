using AhDai.Entity;
using AhDai.Service.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AhDai.Service.Impl.Utils;

internal class ExtensionEntityUtil<TEntity>
	where TEntity : BaseExtensionEntity, new()
{
	/// <summary>
	/// ToDigitalDataEntities
	/// </summary>
	/// <param name="mainId"></param>
	/// <param name="digitalData"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static List<TEntity> ToDigitalDataEntities(long mainId, IEnumerable<long> digitalData, int type = 1)
	{
		var extensions = new List<TEntity>();
		foreach (var text in digitalData)
		{
			extensions.Add(new TEntity() { MainId = mainId, Type = type, DigitalData = text });
		}
		return extensions;
	}

	/// <summary>
	/// ToTextDataEntities
	/// </summary>
	/// <param name="mainId"></param>
	/// <param name="textData"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public static List<TEntity> ToTextDataEntities(long mainId, IEnumerable<string> textData, int type = 1)
	{
		var extensions = new List<TEntity>();
		foreach (var text in textData)
		{
			extensions.Add(new TEntity() { MainId = mainId, Type = type, TextData = text });
		}
		return extensions;
	}

	/// <summary>
	/// ToTextDataOutput
	/// </summary>
	/// <param name="entities"></param>
	/// <param name="dict"></param>
	/// <returns></returns>
	public static IDictionary<string, string> ToTextDataOutput(IEnumerable<TEntity> entities, IEnumerable<DictOutput> dict)
	{
		var results = new Dictionary<string, string>();
		foreach (var entity in entities)
		{
			var temp = dict.Where(x => x.Value == entity.TextData).FirstOrDefault();
			results.Add(entity.TextData ?? "", temp?.Name ?? "");
		}
		return results;
	}
}
